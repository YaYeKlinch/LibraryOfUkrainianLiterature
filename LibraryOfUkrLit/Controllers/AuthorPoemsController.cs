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
    public class AuthorPoemsController : Controller
    {
        private readonly Authorship _context;

        public AuthorPoemsController(Authorship context)
        {
            _context = context;
        }

        // GET: AuthorPoems
        public async Task<IActionResult> Index()
        {
            var authorship = _context.AuthorPoem.Include(a => a.Author).Include(a => a.Poem);
            return View(await authorship.ToListAsync());
        }

        // GET: AuthorPoems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorPoem = await _context.AuthorPoem
                .Include(a => a.Author)
                .Include(a => a.Poem)
                .FirstOrDefaultAsync(m => m.AuthorPoemID == id);
            if (authorPoem == null)
            {
                return NotFound();
            }

            return View(authorPoem);
        }

        // GET: AuthorPoems/Create
        public IActionResult Create()
        {
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "LastName");
            ViewData["PoemID"] = new SelectList(_context.Poem, "PoemID", "Name");
            return View();
        }

        // POST: AuthorPoems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorPoemID,AuthorID,PoemID")] AuthorPoem authorPoem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(authorPoem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "ID", authorPoem.AuthorID);
            ViewData["PoemID"] = new SelectList(_context.Poem, "PoemID", "PoemID", authorPoem.PoemID);
            return View(authorPoem);
        }

        // GET: AuthorPoems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorPoem = await _context.AuthorPoem.FindAsync(id);
            if (authorPoem == null)
            {
                return NotFound();
            }
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "ID", authorPoem.AuthorID);
            ViewData["PoemID"] = new SelectList(_context.Poem, "PoemID", "PoemID", authorPoem.PoemID);
            return View(authorPoem);
        }

        // POST: AuthorPoems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorPoemID,AuthorID,PoemID")] AuthorPoem authorPoem)
        {
            if (id != authorPoem.AuthorPoemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorPoem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorPoemExists(authorPoem.AuthorPoemID))
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
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "ID", authorPoem.AuthorID);
            ViewData["PoemID"] = new SelectList(_context.Poem, "PoemID", "PoemID", authorPoem.PoemID);
            return View(authorPoem);
        }

        // GET: AuthorPoems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorPoem = await _context.AuthorPoem
                .Include(a => a.Author)
                .Include(a => a.Poem)
                .FirstOrDefaultAsync(m => m.AuthorPoemID == id);
            if (authorPoem == null)
            {
                return NotFound();
            }

            return View(authorPoem);
        }

        // POST: AuthorPoems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authorPoem = await _context.AuthorPoem.FindAsync(id);
            _context.AuthorPoem.Remove(authorPoem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorPoemExists(int id)
        {
            return _context.AuthorPoem.Any(e => e.AuthorPoemID == id);
        }
    }
}
