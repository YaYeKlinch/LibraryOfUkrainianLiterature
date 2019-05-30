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
    public class PoemsController : Controller
    {
        private readonly Authorship _context;

        public PoemsController(Authorship context)
        {
            _context = context;
        }

        // GET: Poems
        public async Task<IActionResult> Index()
        {
            return View(await _context.Poem.ToListAsync());
        }

        // GET: Poems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poem = await _context.Poem
                .FirstOrDefaultAsync(m => m.PoemID == id);
            if (poem == null)
            {
                return NotFound();
            }

            return View(poem);
        }

        // GET: Poems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Poems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PoemID,Name,Year,CountLine")] Poem poem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(poem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(poem);
        }

        // GET: Poems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poem = await _context.Poem.FindAsync(id);
            if (poem == null)
            {
                return NotFound();
            }
            return View(poem);
        }

        // POST: Poems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PoemID,Name,Year,CountLine")] Poem poem)
        {
            if (id != poem.PoemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoemExists(poem.PoemID))
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
            return View(poem);
        }

        // GET: Poems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poem = await _context.Poem
                .FirstOrDefaultAsync(m => m.PoemID == id);
            if (poem == null)
            {
                return NotFound();
            }

            return View(poem);
        }

        // POST: Poems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var poem = await _context.Poem.FindAsync(id);
            _context.Poem.Remove(poem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PoemExists(int id)
        {
            return _context.Poem.Any(e => e.PoemID == id);
        }
    }
}
