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
    public class AdminTrelloTagsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminTrelloTagsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminTrelloTags
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tags.ToListAsync());
        }

        // GET: AdminTrelloTags/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trelloTag = await _context.Tags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trelloTag == null)
            {
                return NotFound();
            }

            return View(trelloTag);
        }

        // GET: AdminTrelloTags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminTrelloTags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TrelloTag trelloTag)
        {
            if (ModelState.IsValid)
            {
                trelloTag.Id = Guid.NewGuid();
                _context.Add(trelloTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trelloTag);
        }

        // GET: AdminTrelloTags/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trelloTag = await _context.Tags.FindAsync(id);
            if (trelloTag == null)
            {
                return NotFound();
            }
            return View(trelloTag);
        }

        // POST: AdminTrelloTags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] TrelloTag trelloTag)
        {
            if (id != trelloTag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trelloTag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrelloTagExists(trelloTag.Id))
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
            return View(trelloTag);
        }

        // GET: AdminTrelloTags/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trelloTag = await _context.Tags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trelloTag == null)
            {
                return NotFound();
            }

            return View(trelloTag);
        }

        // POST: AdminTrelloTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var trelloTag = await _context.Tags.FindAsync(id);
            _context.Tags.Remove(trelloTag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrelloTagExists(Guid id)
        {
            return _context.Tags.Any(e => e.Id == id);
        }
    }
}
