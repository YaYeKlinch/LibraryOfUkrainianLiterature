using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryOfUkrLit.Data;
using LibraryOfUkrLit.Models;

namespace LibraryOfUkrLit.Controllers
{
    public class ProsesController : Controller
    {
        private readonly Authorship _context;

        public ProsesController(Authorship context)
        {
            _context = context;
        }

        // GET: Proses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Prose.ToListAsync());
        }

        // GET: Proses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prose = await _context.Prose
                .FirstOrDefaultAsync(m => m.ProseID == id);
            if (prose == null)
            {
                return NotFound();
            }

            return View(prose);
        }

        // GET: Proses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProseID,Name,Year,Section")] Prose prose)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prose);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prose);
        }

        // GET: Proses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prose = await _context.Prose.FindAsync(id);
            if (prose == null)
            {
                return NotFound();
            }
            return View(prose);
        }

        // POST: Proses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProseID,Name,Year,Section")] Prose prose)
        {
            if (id != prose.ProseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prose);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProseExists(prose.ProseID))
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
            return View(prose);
        }

        // GET: Proses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prose = await _context.Prose
                .FirstOrDefaultAsync(m => m.ProseID == id);
            if (prose == null)
            {
                return NotFound();
            }

            return View(prose);
        }

        // POST: Proses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prose = await _context.Prose.FindAsync(id);
            _context.Prose.Remove(prose);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProseExists(int id)
        {
            return _context.Prose.Any(e => e.ProseID == id);
        }
    }
}
