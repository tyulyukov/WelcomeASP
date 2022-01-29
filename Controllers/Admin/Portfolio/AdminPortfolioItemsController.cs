using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WelcomeASP.Data;
using WelcomeASP.Data.Entities.Portfolio;

namespace WelcomeASP.Controllers.Admin.Portfolio
{
    public class AdminPortfolioItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminPortfolioItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminPortfolioItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PortfolioItems.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AdminPortfolioItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = await _context.PortfolioItems
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // GET: AdminPortfolioItems/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.PortfolioCategories, "Id", "Name");
            return View();
        }

        // POST: AdminPortfolioItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Slug,CategoryId")] PortfolioItem portfolioItem)
        {
            if (ModelState.IsValid)
            {
                portfolioItem.Id = Guid.NewGuid();
                _context.Add(portfolioItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.PortfolioCategories, "Id", "Name", portfolioItem.CategoryId);
            return View(portfolioItem);
        }

        // GET: AdminPortfolioItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = await _context.PortfolioItems.FindAsync(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.PortfolioCategories, "Id", "Name", portfolioItem.CategoryId);
            return View(portfolioItem);
        }

        // POST: AdminPortfolioItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Slug,CategoryId")] PortfolioItem portfolioItem)
        {
            if (id != portfolioItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portfolioItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioItemExists(portfolioItem.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.PortfolioCategories, "Id", "Name", portfolioItem.CategoryId);
            return View(portfolioItem);
        }

        // GET: AdminPortfolioItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = await _context.PortfolioItems
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // POST: AdminPortfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var portfolioItem = await _context.PortfolioItems.FindAsync(id);
            _context.PortfolioItems.Remove(portfolioItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioItemExists(Guid id)
        {
            return _context.PortfolioItems.Any(e => e.Id == id);
        }
    }
}
