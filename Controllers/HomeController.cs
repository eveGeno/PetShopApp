using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShopApplication.Data;
using PetShopApplication.Models;

namespace PetShopApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly PetShopDbContext _context;

        public HomeController(PetShopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var topAnimals = await _context.Animals
                .Include(a => a.Comments)
                .OrderByDescending(a => a.Comments.Count)
                .Take(2)
                .ToListAsync();

            if (!topAnimals.Any())
            {
                ViewBag.Message = "No animals available at the moment.";
                return View(new List<Animal>());
            }

            return View(topAnimals);
        }
    }
}
