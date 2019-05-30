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
    public class AuthorProsesController : Controller
    {
        private readonly Authorship _context;

        public AuthorProsesController(Authorship context)
        {
            _context = context;
        }

        // GET: AuthorProses
        public async Task<IActionResult> Index()
        {
            var authorship = _context.AuthorProse.Include(a => a.Author).Include(a => a.Prose);
            return View(await authorship.ToListAsync());
        }

        // GET: AuthorProses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorProse = await _context.AuthorProse
                .Include(a => a.Author)
                .Include(a => a.Prose)
                .FirstOrDefaultAsync(m => m.AuthorProseID == id);
            if (authorProse == null)
            {
                return NotFound();
            }

            return View(authorProse);
        }

        // GET: AuthorProses/Create
        public IActionResult Create()
        {
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "LastName");
            ViewData["ProseID"] = new SelectList(_context.Prose, "ProseID", "Name");
            return View();
        }

        // POST: AuthorProses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorProseID,AuthorID,ProseID")] AuthorProse authorProse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(authorProse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "ID", authorProse.AuthorID);
            ViewData["ProseID"] = new SelectList(_context.Prose, "ProseID", "ProseID", authorProse.ProseID);
            return View(authorProse);
        }

        // GET: AuthorProses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorProse = await _context.AuthorProse.FindAsync(id);
            if (authorProse == null)
            {
                return NotFound();
            }
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "ID", authorProse.AuthorID);
            ViewData["ProseID"] = new SelectList(_context.Prose, "ProseID", "ProseID", authorProse.ProseID);
            return View(authorProse);
        }

        // POST: AuthorProses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorProseID,AuthorID,ProseID")] AuthorProse authorProse)
        {
            if (id != authorProse.AuthorProseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorProse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorProseExists(authorProse.AuthorProseID))
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
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "ID", authorProse.AuthorID);
            ViewData["ProseID"] = new SelectList(_context.Prose, "ProseID", "ProseID", authorProse.ProseID);
            return View(authorProse);
        }

        // GET: AuthorProses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorProse = await _context.AuthorProse
                .Include(a => a.Author)
                .Include(a => a.Prose)
                .FirstOrDefaultAsync(m => m.AuthorProseID == id);
            if (authorProse == null)
            {
                return NotFound();
            }

            return View(authorProse);
        }

        // POST: AuthorProses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authorProse = await _context.AuthorProse.FindAsync(id);
            _context.AuthorProse.Remove(authorProse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorProseExists(int id)
        {
            return _context.AuthorProse.Any(e => e.AuthorProseID == id);
        }
    }
}
