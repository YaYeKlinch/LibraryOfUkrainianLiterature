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
    public class PublisherBooksController : Controller
    {
        private readonly Authorship _context;

        public PublisherBooksController(Authorship context)
        {
            _context = context;
        }

        // GET: PublisherBooks
        public async Task<IActionResult> Index()
        {
            var authorship = _context.PublisherBook.Include(p => p.Book).Include(p => p.Publisher);
            return View(await authorship.ToListAsync());
        }

        // GET: PublisherBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisherBook = await _context.PublisherBook
                .Include(p => p.Book)
                .Include(p => p.Publisher)
                .FirstOrDefaultAsync(m => m.PublisherBookID == id);
            if (publisherBook == null)
            {
                return NotFound();
            }

            return View(publisherBook);
        }

        // GET: PublisherBooks/Create
        public IActionResult Create()
        {
            ViewData["BookID"] = new SelectList(_context.Book, "BooksID", "Name");
            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "Name");
            return View();
        }

        // POST: PublisherBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PublisherBookID,BookID,PublisherID")] PublisherBook publisherBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publisherBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookID"] = new SelectList(_context.Book, "BooksID", "BooksID", publisherBook.BookID);
            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "ID", publisherBook.PublisherID);
            return View(publisherBook);
        }

        // GET: PublisherBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisherBook = await _context.PublisherBook.FindAsync(id);
            if (publisherBook == null)
            {
                return NotFound();
            }
            ViewData["BookID"] = new SelectList(_context.Book, "BooksID", "BooksID", publisherBook.BookID);
            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "ID", publisherBook.PublisherID);
            return View(publisherBook);
        }

        // POST: PublisherBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PublisherBookID,BookID,PublisherID")] PublisherBook publisherBook)
        {
            if (id != publisherBook.PublisherBookID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publisherBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublisherBookExists(publisherBook.PublisherBookID))
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
            ViewData["BookID"] = new SelectList(_context.Book, "BooksID", "BooksID", publisherBook.BookID);
            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "ID", publisherBook.PublisherID);
            return View(publisherBook);
        }

        // GET: PublisherBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisherBook = await _context.PublisherBook
                .Include(p => p.Book)
                .Include(p => p.Publisher)
                .FirstOrDefaultAsync(m => m.PublisherBookID == id);
            if (publisherBook == null)
            {
                return NotFound();
            }

            return View(publisherBook);
        }

        // POST: PublisherBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publisherBook = await _context.PublisherBook.FindAsync(id);
            _context.PublisherBook.Remove(publisherBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublisherBookExists(int id)
        {
            return _context.PublisherBook.Any(e => e.PublisherBookID == id);
        }
    }
}
