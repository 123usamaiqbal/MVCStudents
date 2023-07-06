    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCStudents.Data;
using MVCStudents.Models;

namespace MVCStudents.Controllers
{
    public class GradingsController : Controller
    {
        private readonly MVCStudentsContext _context;

        public GradingsController(MVCStudentsContext context)
        {
            _context = context;
        }

        //REDIRECT to Students/Index
        public IActionResult StdIndex()
        {
            return LocalRedirect("/Students/Index");
        }

        // GET: Gradings
        public async Task<IActionResult> Index()
        {
              return _context.Grading != null ? 
                          View(await _context.Grading.ToListAsync()) :
                          Problem("Entity set 'MVCStudentsContext.Grading'  is null.");
        }

        // GET: Gradings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Grading == null)
            {
                return NotFound();
            }

            var grading = await _context.Grading
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grading == null)
            {
                return NotFound();
            }

            return View(grading);
        }

        // GET: Gradings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gradings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598. GradingId,Studentsid
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Computer,Physics,Science,GradingId")] Grading grading)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grading);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grading);
        }

        // GET: Gradings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Grading == null)
            {
                return NotFound();
            }

            var grading = await _context.Grading.FindAsync(id);
            if (grading == null)
            {
                return NotFound();
            }
            return View(grading);
        }

        // POST: Gradings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Computer,Physics,Science,GradingId")] Grading grading)
        {
            if (id != grading.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grading);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradingExists(grading.Id))
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
            return View(grading);
        }

        // GET: Gradings/Delete/5  GradingId,Studentsid
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Grading == null)
            {
                return NotFound();
            }

            var grading = await _context.Grading
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grading == null)
            {
                return NotFound();
            }

            return View(grading);
        }

        // POST: Gradings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Grading == null)
            {
                return Problem("Entity set 'MVCStudentsContext.Grading'  is null.");
            }
            var grading = await _context.Grading.FindAsync(id);
            if (grading != null)
            {
                _context.Grading.Remove(grading);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradingExists(int id)
        {
          return (_context.Grading?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
