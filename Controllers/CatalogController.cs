using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShopApplication.Data;
using PetShopApplication.Models;

namespace PetShopApplication.Controllers
{
    public class CatalogController : Controller
    {
        private readonly PetShopDbContext _context;

        public CatalogController(PetShopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string category, int page = 1, int pageSize = 8)
        {
            try
            {
                var categories = await _context.Categories.ToListAsync();
                var animalsQuery = _context.Animals.Include(a => a.Category).AsQueryable();

                //Filter by category
                if (!string.IsNullOrEmpty(category))
                {
                    animalsQuery = animalsQuery.Where(a => a.Category != null && a.Category.Name == category);
                }

                // Paging
                int totalItems = await animalsQuery.CountAsync();
                var animals = await animalsQuery
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                ViewBag.Categories = categories;
                ViewBag.SelectedCategory = category;
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

                return View(animals);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CatalogController: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}