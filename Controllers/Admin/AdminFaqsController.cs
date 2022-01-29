using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WelcomeASP.Data;
using WelcomeASP.Data.Entities;

namespace WelcomeASP.Controllers.Admin
{
    public class AdminFaqsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminFaqsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminFaqs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Faqs.ToListAsync());
        }

        // GET: AdminFaqs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await _context.Faqs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faq == null)
            {
                return NotFound();
            }

            return View(faq);
        }

        // GET: AdminFaqs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminFaqs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Question,Answer")] Faq faq)
        {
            if (ModelState.IsValid)
            {
                faq.Id = Guid.NewGuid();
                _context.Add(faq);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faq);
        }

        // GET: AdminFaqs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await _context.Faqs.FindAsync(id);
            if (faq == null)
            {
                return NotFound();
            }
            return View(faq);
        }

        // POST: AdminFaqs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Question,Answer")] Faq faq)
        {
            if (id != faq.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faq);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaqExists(faq.Id))
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
            return View(faq);
        }

        // GET: AdminFaqs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await _context.Faqs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faq == null)
            {
                return NotFound();
            }

            return View(faq);
        }

        // POST: AdminFaqs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var faq = await _context.Faqs.FindAsync(id);
            _context.Faqs.Remove(faq);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaqExists(Guid id)
        {
            return _context.Faqs.Any(e => e.Id == id);
        }
    }
}
