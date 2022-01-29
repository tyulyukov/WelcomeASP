using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WelcomeASP.Data;
using WelcomeASP.Data.Entities.Trello;

namespace WelcomeASP.Controllers.Admin.Trello
{
    [Authorize]
    public class AdminBoardsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AdminBoardsController(ApplicationDbContext context, IWebHostEnvironment appEnvironment, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: AdminBoards
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            // check admin role

            return View(await _context.Boards.Include(b => b.Tags).ToListAsync());
        }

        // GET: AdminBoards/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Boards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (board == null)
            {
                return NotFound();
            }

            return View(board);
        }

        // GET: AdminBoards/Create
        public IActionResult Create()
        {
            ViewData["TagsId"] = new MultiSelectList(_context.Tags, "Id", "Name");

            ViewBag.TagsAll = _context.Tags;

            return View();
        }

        // POST: AdminBoards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] Board board, Guid[] Tags, IFormFile fileToStorage = null, IFormFile fileToDB = null)
        {
            if (ModelState.IsValid)
            {
                board.Id = Guid.NewGuid();

                if (Tags != null)
                    if (Tags.Length > 0)
                        board.Tags = _context.Tags.Where(t => Tags.Contains(t.Id)).ToList();

                if (fileToStorage != null)
                {
                    var date = DateTime.Today;

                    StringBuilder builder = new StringBuilder();
                    String[] fileNameSplitted = fileToStorage.FileName.Split('.');

                    for (int i = 0; i < fileNameSplitted.Length - 1; i++)
                    {
                        if (i != 0)
                            builder.Append('.');

                        builder.Append(fileNameSplitted[i]);
                    }

                    String fileExtension = fileNameSplitted[fileNameSplitted.Length - 1];
                    String fileName = builder.ToString();

                    
                    String imagesPath = _appEnvironment.WebRootPath + "\\storage\\boards\\";

                    if (!Directory.Exists(imagesPath + date.Year))
                        Directory.CreateDirectory(imagesPath + date.Year);

                    if (!Directory.Exists(imagesPath + date.Year + "\\" + date.Month))
                        Directory.CreateDirectory(imagesPath + date.Year + "\\" + date.Month);

                    String basePath = imagesPath + date.Year + "\\" + date.Month + "\\" + fileName;

                    int countCopies = 0;
                    String correctPath;

                    while (true)
                    {
                        if (countCopies != 0)
                            correctPath = basePath + " (" + countCopies + " copy)." + fileExtension;
                        else
                            correctPath = basePath + "." + fileExtension;

                        if (!System.IO.File.Exists(correctPath))
                        {
                            using (var fileStream = new FileStream(correctPath, FileMode.Create))
                                await fileToStorage.CopyToAsync(fileStream);

                            board.ImgUrl = correctPath;

                            break;
                        }

                        countCopies++;
                    }
                }

                if (fileToDB != null)
                {
                    byte[] imageData;

                    using (var binaryReader = new BinaryReader(fileToDB.OpenReadStream()))
                        imageData = binaryReader.ReadBytes((int)fileToDB.Length);

                    board.Logo = imageData;
                }

                _context.Add(board);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(board);
        }

        // GET: AdminBoards/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Boards.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }
            return View(board);
        }

        // POST: AdminBoards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title")] Board board)
        {
            if (id != board.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(board);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardExists(board.Id))
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
            return View(board);
        }

        // GET: AdminBoards/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Boards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (board == null)
            {
                return NotFound();
            }

            return View(board);
        }

        // POST: AdminBoards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var board = await _context.Boards.FindAsync(id);
            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoardExists(Guid id)
        {
            return _context.Boards.Any(e => e.Id == id);
        }
    }
}
