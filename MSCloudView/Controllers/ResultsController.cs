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
    public class ResultsController : Controller
    {
        private readonly MSCloudDBContext _context;

        public ResultsController(MSCloudDBContext context)
        {
            _context = context;
        }

        // GET: Results
        public async Task<IActionResult> Index()
        {
            var mSCloudDBContext = _context.Results.Include(r => r.IdAthleteNavigation);
            return View(await mSCloudDBContext.ToListAsync());
        }

        // GET: Results/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Results == null)
            {
                return NotFound();
            }

            var result = await _context.Results
                .Include(r => r.IdAthleteNavigation)
                .FirstOrDefaultAsync(m => m.IdResult == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // GET: Results/Create
        public IActionResult Create()
        {
            ViewData["IdAthlete"] = new SelectList(_context.Athletes, "IdAthlete", "AthleteName");
            return View();
        }

        // POST: Results/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdResult,IdAthlete,ArranqueKg,EnvionKg,TotalPesoKg")] Result result)
        {
            _context.Add(result);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            ViewData["IdAthlete"] = new SelectList(_context.Athletes, "IdAthlete", "AthleteName", result.IdAthlete);
            return View(result);
        }

        // GET: Results/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Results == null)
            {
                return NotFound();
            }

            var result = await _context.Results.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            ViewData["IdAthlete"] = new SelectList(_context.Athletes, "IdAthlete", "AthleteName", result.IdAthlete);
            return View(result);
        }

        // POST: Results/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdResult,IdAthlete,ArranqueKg,EnvionKg,TotalPesoKg")] Result result)
        {
            if (id != result.IdResult)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(result);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultExists(result.IdResult))
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
            ViewData["IdAthlete"] = new SelectList(_context.Athletes, "IdAthlete", "AthleteName", result.IdAthlete);
            return View(result);
        }

        // GET: Results/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Results == null)
            {
                return NotFound();
            }

            var result = await _context.Results
                .Include(r => r.IdAthleteNavigation)
                .FirstOrDefaultAsync(m => m.IdResult == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Results == null)
            {
                return Problem("Entity set 'MSCloudDBContext.Results'  is null.");
            }
            var result = await _context.Results.FindAsync(id);
            if (result != null)
            {
                _context.Results.Remove(result);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultExists(int id)
        {
          return (_context.Results?.Any(e => e.IdResult == id)).GetValueOrDefault();
        }
    }
}
