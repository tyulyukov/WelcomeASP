using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WelcomeASP.Data;
using WelcomeASP.Data.Entities.Trello;

namespace WelcomeASP.Controllers.Admin.Trello
{
    public class AdminColumnsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminColumnsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminColumns
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Columns.Include(c => c.Board);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AdminColumns/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var columns = _context.Columns
                .Include(c => c.Cards)
                .Where(m => m.BoardId == id);
            if (columns == null)
            {
                return NotFound();
            }

            return View(await columns.ToListAsync());
        }

        // GET: AdminColumns/Create
        public IActionResult Create()
        {
            ViewData["BoardId"] = new SelectList(_context.Boards, "Id", "Title");
            return View();
        }

        // POST: AdminColumns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BoardId,Title")] Column column)
        {
            if (ModelState.IsValid)
            {
                column.Id = Guid.NewGuid();
                _context.Add(column);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BoardId"] = new SelectList(_context.Boards, "Id", "Title", column.BoardId);
            return View(column);
        }

        // GET: AdminColumns/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var column = await _context.Columns.FindAsync(id);
            if (column == null)
            {
                return NotFound();
            }
            ViewData["BoardId"] = new SelectList(_context.Boards, "Id", "Title", column.BoardId);
            return View(column);
        }

        // POST: AdminColumns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,BoardId,Title")] Column column)
        {
            if (id != column.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(column);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColumnExists(column.Id))
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
            ViewData["BoardId"] = new SelectList(_context.Boards, "Id", "Title", column.BoardId);
            return View(column);
        }

        // GET: AdminColumns/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var column = await _context.Columns
                .Include(c => c.Board)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (column == null)
            {
                return NotFound();
            }

            return View(column);
        }

        // POST: AdminColumns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var column = await _context.Columns.FindAsync(id);
            _context.Columns.Remove(column);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColumnExists(Guid id)
        {
            return _context.Columns.Any(e => e.Id == id);
        }
    }
}
