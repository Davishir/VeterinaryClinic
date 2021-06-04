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
    public class OwnersController : Controller
    {
        private readonly VeterinaryContext _context;

        public OwnersController(VeterinaryContext context)
        {
            _context = context;
        }

        // GET: Owners
        public async Task<IActionResult> Index(
         string sortOrder,
         string currentFilter,
         string searchString,
         int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var owners = from o in _context.Owners
                           select o;
            if (!String.IsNullOrEmpty(searchString))
            {
                owners = owners.Where(o => o.Fio.Contains(searchString)
                                       || o.Address.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    owners = owners.OrderByDescending(o => o.Fio);
                    break;
                case "Date":
                    owners = owners.OrderBy(o => o.Address);
                    break;
                case "date_desc":
                    owners = owners.OrderByDescending(o => o.Address);
                    break;
                default:
                    owners = owners.OrderBy(o => o.Fio);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Owner>.CreateAsync(owners.AsNoTracking(), pageNumber ?? 1, pageSize));
            
        }

        // GET: Owners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners
                .Include(o => o.Visits)
                     .ThenInclude(v => v.Animal)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.OwnerID == id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("OwnerID,Fio,Address,PhoneNumber")] Owner owner)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(owner);
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
            return View(owner);
        }

        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }
            return View(owner);
        }

        // POST: Owners/Edit/5
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
            var ownerToUpdate = await _context.Owners.FirstOrDefaultAsync(o => o.OwnerID == id);
            if (await TryUpdateModelAsync<Owner>(
                ownerToUpdate,
                "",
                o => o.Fio, o => o.Address, o => o.PhoneNumber))
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
            return View(ownerToUpdate);
        }

        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners
                 .AsNoTracking()
                .FirstOrDefaultAsync(m => m.OwnerID == id);
            if (owner == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Не удалось удалить. Попробуйте еще раз, и если проблема не исчезнет " +
                    "обратитесь к вашему системному администратору.";
            }
            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Owners.Remove(owner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool OwnerExists(int id)
        {
            return _context.Owners.Any(e => e.OwnerID == id);
        }
    }
}
