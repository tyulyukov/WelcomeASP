using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WelcomeASP.Data;

namespace WelcomeASP.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PortfolioController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PortfolioItems.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }
    }
}
