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
    public class AdminPortfolioCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminPortfolioCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminPortfolioCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.PortfolioCategories.ToListAsync());
        }

        // GET: AdminPortfolioCategories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioCategory = await _context.PortfolioCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portfolioCategory == null)
            {
                return NotFound();
            }

            return View(portfolioCategory);
        }

        // GET: AdminPortfolioCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminPortfolioCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Slug")] PortfolioCategory portfolioCategory)
        {
            if (ModelState.IsValid)
            {
                portfolioCategory.Id = Guid.NewGuid();
                _context.Add(portfolioCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(portfolioCategory);
        }

        // GET: AdminPortfolioCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioCategory = await _context.PortfolioCategories.FindAsync(id);
            if (portfolioCategory == null)
            {
                return NotFound();
            }
            return View(portfolioCategory);
        }

        // POST: AdminPortfolioCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Slug")] PortfolioCategory portfolioCategory)
        {
            if (id != portfolioCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portfolioCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioCategoryExists(portfolioCategory.Id))
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
            return View(portfolioCategory);
        }

        // GET: AdminPortfolioCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioCategory = await _context.PortfolioCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portfolioCategory == null)
            {
                return NotFound();
            }

            return View(portfolioCategory);
        }

        // POST: AdminPortfolioCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var portfolioCategory = await _context.PortfolioCategories.FindAsync(id);
            _context.PortfolioCategories.Remove(portfolioCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioCategoryExists(Guid id)
        {
            return _context.PortfolioCategories.Any(e => e.Id == id);
        }
    }
}
