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
    public class BookProsesController : Controller
    {
        private readonly Authorship _context;

        public BookProsesController(Authorship context)
        {
            _context = context;
        }

        // GET: BookProses
        public async Task<IActionResult> Index()
        {
            var authorship = _context.BookProse.Include(b => b.Book).Include(b => b.Prose);
            return View(await authorship.ToListAsync());
        }

        // GET: BookProses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookProse = await _context.BookProse
                .Include(b => b.Book)
                .Include(b => b.Prose)
                .FirstOrDefaultAsync(m => m.BookProseID == id);
            if (bookProse == null)
            {
                return NotFound();
            }

            return View(bookProse);
        }

        // GET: BookProses/Create
        public IActionResult Create()
        {
            ViewData["BooksID"] = new SelectList(_context.Book, "BooksID", "Name");
            ViewData["ProseID"] = new SelectList(_context.Prose, "ProseID", "Name");
            return View();
        }

        // POST: BookProses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookProseID,BooksID,ProseID")] BookProse bookProse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookProse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BooksID"] = new SelectList(_context.Book, "BooksID", "BooksID", bookProse.BooksID);
            ViewData["ProseID"] = new SelectList(_context.Prose, "ProseID", "ProseID", bookProse.ProseID);
            return View(bookProse);
        }

        // GET: BookProses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookProse = await _context.BookProse.FindAsync(id);
            if (bookProse == null)
            {
                return NotFound();
            }
            ViewData["BooksID"] = new SelectList(_context.Book, "BooksID", "BooksID", bookProse.BooksID);
            ViewData["ProseID"] = new SelectList(_context.Prose, "ProseID", "ProseID", bookProse.ProseID);
            return View(bookProse);
        }

        // POST: BookProses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookProseID,BooksID,ProseID")] BookProse bookProse)
        {
            if (id != bookProse.BookProseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookProse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookProseExists(bookProse.BookProseID))
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
            ViewData["BooksID"] = new SelectList(_context.Book, "BooksID", "BooksID", bookProse.BooksID);
            ViewData["ProseID"] = new SelectList(_context.Prose, "ProseID", "ProseID", bookProse.ProseID);
            return View(bookProse);
        }

        // GET: BookProses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookProse = await _context.BookProse
                .Include(b => b.Book)
                .Include(b => b.Prose)
                .FirstOrDefaultAsync(m => m.BookProseID == id);
            if (bookProse == null)
            {
                return NotFound();
            }

            return View(bookProse);
        }

        // POST: BookProses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookProse = await _context.BookProse.FindAsync(id);
            _context.BookProse.Remove(bookProse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookProseExists(int id)
        {
            return _context.BookProse.Any(e => e.BookProseID == id);
        }
    }
}
