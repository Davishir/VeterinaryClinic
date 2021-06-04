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
    public class VisitsController : Controller
    {
        private readonly VeterinaryContext _context;

        public VisitsController(VeterinaryContext context)
        {
            _context = context;
        }

        // GET: Visits
        public async Task<IActionResult> Index(
         string sortOrder,
         string currentFilter,
         string searchString,
         int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["ComplaintsSortParm"] = String.IsNullOrEmpty(sortOrder) ? "complain_desc" : "";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            
            ViewData["CurrentFilter"] = searchString;
            var visits = from v in _context.Visits.Include(i => i.Animal)
                         select v;
            if (!String.IsNullOrEmpty(searchString))
            {
                visits = visits.Where(v => v.Complaints.Contains(searchString)
                                       || v.Diagnosis.Contains(searchString)
                                       ||v.Animal.NickName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    visits = visits.OrderByDescending(v => v.AttendinDoctor);
                    break;
                case "Date":
                    visits = visits.OrderBy(v => v.Dateofvisit);
                    break;
                case "date_desc":
                    visits = visits.OrderByDescending(v => v.Dateofvisit);
                    break;
                case "complain_desc":
                    visits = visits.OrderByDescending(v => v.Complaints);
                    break;
                default:
                    visits = visits.OrderBy(v => v.AttendinDoctor);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Visit>.CreateAsync(visits.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Visits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits
                .Include(v => v.Animal)
                .Include(v => v.Owner)
                .FirstOrDefaultAsync(m => m.VisitID == id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // GET: Visits/Create
        public IActionResult Create()
        {
            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "Breed");
            ViewData["OwnerID"] = new SelectList(_context.Owners, "OwnerID", "Address");
            return View();
        }

        // POST: Visits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisitID,AnimalID,OwnerID,Complaints,Diagnosis,AttendinDoctor,Price,Dateofvisit,Duration")] Visit visit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "Breed", visit.AnimalID);
            ViewData["OwnerID"] = new SelectList(_context.Owners, "OwnerID", "Address", visit.OwnerID);
            return View(visit);
        }

        // GET: Visits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits.FindAsync(id);
            if (visit == null)
            {
                return NotFound();
            }
            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "Breed", visit.AnimalID);
            ViewData["OwnerID"] = new SelectList(_context.Owners, "OwnerID", "Address", visit.OwnerID);
            return View(visit);
        }

        // POST: Visits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VisitID,AnimalID,OwnerID,Complaints,Diagnosis,AttendinDoctor,Price,Dateofvisit,Duration")] Visit visit)
        {
            if (id != visit.VisitID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitExists(visit.VisitID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "Breed", visit.AnimalID);
            ViewData["OwnerID"] = new SelectList(_context.Owners, "OwnerID", "Address", visit.OwnerID);
            return View(visit);
        }

        // GET: Visits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits
                .Include(v => v.Animal)
                .Include(v => v.Owner)
                .FirstOrDefaultAsync(m => m.VisitID == id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // POST: Visits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visit = await _context.Visits.FindAsync(id);
            _context.Visits.Remove(visit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitExists(int id)
        {
            return _context.Visits.Any(e => e.VisitID == id);
        }
    }
}
