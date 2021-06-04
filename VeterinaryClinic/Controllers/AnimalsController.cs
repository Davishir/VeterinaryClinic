using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Data;
using VeterinaryClinic.Models;

namespace VeterinaryClinic.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly VeterinaryContext _context;

        public AnimalsController(VeterinaryContext context)
        {
            _context = context;
        }

        // GET: Animals
        public async Task<IActionResult> Index(
        string sortOrder,
        string currentFilter,
        string searchString,
        int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["BreedSortParm"] = String.IsNullOrEmpty(sortOrder) ? "breed_desc" : "";
            if(searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString; 
            var animals = from a in _context.Animals
                           select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                animals = animals.Where(a => a.NickName.Contains(searchString)
                                       || a.Kind.Contains(searchString)
                                       || a.Breed.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    animals = animals.OrderByDescending(a => a.NickName);
                    break;
                case "Date":
                    animals = animals.OrderBy(a => a.DateOfBirth);
                    break;
                case "date_desc":
                    animals = animals.OrderByDescending(a => a.DateOfBirth);
                    break;
                case "breed_desc":
                    animals = animals.OrderByDescending(a => a.Breed);
                    break;

                default:
                    animals = animals.OrderBy(a => a.NickName);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Animal>.CreateAsync(animals.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .Include(a => a.Visits)
                    .ThenInclude(v => v.Owner)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.AnimalID == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // GET: Animals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("AnimalID,NickName,Kind,Breed,DateOfBirth,Sex,Color,Length,Weight")] Animal animal)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(animal);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Не удалось сохранить изменения. " +
                    "Попробуйте еще раз, и если проблема не исчезнет " +
                    "обратитесь к системному администратору.");
            }
            return View(animal);
        }

        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalToUpdate = await _context.Animals.FirstOrDefaultAsync(a => a.AnimalID == id);
            if (await TryUpdateModelAsync<Animal>(
                animalToUpdate,
                "",
                a => a.NickName, a => a.Kind, a => a.Breed, a => a.DateOfBirth, a => a.Sex, a => a.Color, a => a.Length, a => a.Weight))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Не удалось сохранить изменения. " +
                    "Попробуйте еще раз, и если проблема не исчезнет " +
                    "обратитесь к системному администратору.");
                }
            }
            return View(animalToUpdate);
        }

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.AnimalID == id);
            if (animal == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                     "Не удалось удалить. Попробуйте еще раз, и если проблема не исчезнет " +
                    "обратитесь к вашему системному администратору.";
            }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Animals.Remove(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool AnimalExists(int id)
        {
            return _context.Animals.Any(e => e.AnimalID == id);
        }
    }
}
