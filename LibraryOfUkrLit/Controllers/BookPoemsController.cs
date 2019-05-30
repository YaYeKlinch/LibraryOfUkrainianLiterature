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
    public class BookPoemsController : Controller
    {
        private readonly Authorship _context;

        public BookPoemsController(Authorship context)
        {
            _context = context;
        }

        // GET: BookPoems
        public async Task<IActionResult> Index()
        {
            var authorship = _context.BookPoem.Include(b => b.Book).Include(b => b.Poem);
            return View(await authorship.ToListAsync());
        }

        // GET: BookPoems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookPoems = await _context.BookPoem
                .Include(b => b.Book)
                .Include(b => b.Poem)
                .FirstOrDefaultAsync(m => m.BookPoemsID == id);
            if (bookPoems == null)
            {
                return NotFound();
            }

            return View(bookPoems);
        }

        // GET: BookPoems/Create
        public IActionResult Create()
        {
            ViewData["BooksID"] = new SelectList(_context.Book, "BooksID", "Name");
            ViewData["PoemID"] = new SelectList(_context.Poem, "PoemID", "Name");
            return View();
        }

        // POST: BookPoems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookPoemsID,BooksID,PoemID")] BookPoems bookPoems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookPoems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BooksID"] = new SelectList(_context.Book, "BooksID", "BooksID", bookPoems.BooksID);
            ViewData["PoemID"] = new SelectList(_context.Poem, "PoemID", "PoemID", bookPoems.PoemID);
            return View(bookPoems);
        }

        // GET: BookPoems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookPoems = await _context.BookPoem.FindAsync(id);
            if (bookPoems == null)
            {
                return NotFound();
            }
            ViewData["BooksID"] = new SelectList(_context.Book, "BooksID", "BooksID", bookPoems.BooksID);
            ViewData["PoemID"] = new SelectList(_context.Poem, "PoemID", "PoemID", bookPoems.PoemID);
            return View(bookPoems);
        }

        // POST: BookPoems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookPoemsID,BooksID,PoemID")] BookPoems bookPoems)
        {
            if (id != bookPoems.BookPoemsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookPoems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookPoemsExists(bookPoems.BookPoemsID))
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
            ViewData["BooksID"] = new SelectList(_context.Book, "BooksID", "BooksID", bookPoems.BooksID);
            ViewData["PoemID"] = new SelectList(_context.Poem, "PoemID", "PoemID", bookPoems.PoemID);
            return View(bookPoems);
        }

        // GET: BookPoems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookPoems = await _context.BookPoem
                .Include(b => b.Book)
                .Include(b => b.Poem)
                .FirstOrDefaultAsync(m => m.BookPoemsID == id);
            if (bookPoems == null)
            {
                return NotFound();
            }

            return View(bookPoems);
        }

        // POST: BookPoems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookPoems = await _context.BookPoem.FindAsync(id);
            _context.BookPoem.Remove(bookPoems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookPoemsExists(int id)
        {
            return _context.BookPoem.Any(e => e.BookPoemsID == id);
        }
    }
}
