using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MSCloudView.Models;

namespace MSCloudView.Controllers
{
    public class AthletesController : Controller
    {
        private readonly MSCloudDBContext _context;

        public AthletesController(MSCloudDBContext context)
        {
            _context = context;
        }

        // GET: Athletes
        public async Task<IActionResult> Index()
        {
            var mSCloudDBContext = _context.Athletes.Include(a => a.IdCountryNavigation);
            return View(await mSCloudDBContext.ToListAsync());
        }

        // GET: Athletes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Athletes == null)
            {
                return NotFound();
            }

            var athlete = await _context.Athletes
                .Include(a => a.IdCountryNavigation)
                .FirstOrDefaultAsync(m => m.IdAthlete == id);
            if (athlete == null)
            {
                return NotFound();
            }

            return View(athlete);
        }

        // GET: Athletes/Create
        public IActionResult Create()
        {
            ViewData["IdCountry"] = new SelectList(_context.Countries, "IdCountry", "CountryName");
            return View();
        }

        // POST: Athletes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAthlete,AthleteName,IdCountry")] Athlete athlete)
        {
            _context.Add(athlete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            ViewData["IdCountry"] = new SelectList(_context.Countries, "IdCountry", "CountryName", athlete.IdCountry);
            return View(athlete);
        }

        // GET: Athletes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Athletes == null)
            {
                return NotFound();
            }

            var athlete = await _context.Athletes.FindAsync(id);
            if (athlete == null)
            {
                return NotFound();
            }
            ViewData["IdCountry"] = new SelectList(_context.Countries, "IdCountry", "CountryName", athlete.IdCountry);
            return View(athlete);
        }

        // POST: Athletes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAthlete,AthleteName,IdCountry")] Athlete athlete)
        {
            if (id != athlete.IdAthlete)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(athlete);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AthleteExists(athlete.IdAthlete))
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
            ViewData["IdCountry"] = new SelectList(_context.Countries, "IdCountry", "CountryName", athlete.IdCountry);
            return View(athlete);
        }

        // GET: Athletes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Athletes == null)
            {
                return NotFound();
            }

            var athlete = await _context.Athletes
                .Include(a => a.IdCountryNavigation)
                .FirstOrDefaultAsync(m => m.IdAthlete == id);
            if (athlete == null)
            {
                return NotFound();
            }

            return View(athlete);
        }

        // POST: Athletes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Athletes == null)
            {
                return Problem("Entity set 'MSCloudDBContext.Athletes'  is null.");
            }
            var athlete = await _context.Athletes.FindAsync(id);
            if (athlete != null)
            {
                _context.Athletes.Remove(athlete);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AthleteExists(int id)
        {
          return (_context.Athletes?.Any(e => e.IdAthlete == id)).GetValueOrDefault();
        }
    }
}
