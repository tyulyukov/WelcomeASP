using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WelcomeASP.Data;
using WelcomeASP.Data.Entities.Trello;

namespace WelcomeASP.Controllers
{
    public class TrelloController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrelloController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Boards.ToListAsync());
        }

        public async Task<IActionResult> ShowColumns(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            var board = _context.Boards.First(b => b.Id == id);

            ViewData["ParallaxTitle"] = board.Title;
            ViewData["ParallaxText"] = "Trello board";
            ViewData["BoardId"] = id;

            var columns = _context.Columns
                .Include(c => c.Cards)
                .Where(c => c.BoardId == id);

            if (columns == null)
                return NotFound();

            return View(await columns.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBoard([Bind("Id,Title")] Board board)
        {
            if (ModelState.IsValid)
            {
                board.Id = Guid.NewGuid();
                _context.Add(board);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ShowColumns), new { Id = board.Id });
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateColumn([Bind("Id,BoardId,Title")] Column column)
        {
            if (ModelState.IsValid)
            {
                column.Id = Guid.NewGuid();
                _context.Add(column);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ShowColumns), new { Id = column.BoardId });
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCard([Bind("Id,ColumnId,Title,Body")] Card card, Guid boardId)
        {
            if (ModelState.IsValid)
            {
                card.Id = Guid.NewGuid();
                _context.Add(card);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ShowColumns), new { Id = boardId });
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCard(Guid? id, Guid boardId)
        {
            var card = await _context.Cards.FindAsync(id);
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ShowColumns), new { Id = boardId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteColumn(Guid? id, Guid boardId)
        {
            var column = await _context.Columns.FindAsync(id);
            _context.Columns.Remove(column);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ShowColumns), new { Id = boardId });
        }
    }
}
