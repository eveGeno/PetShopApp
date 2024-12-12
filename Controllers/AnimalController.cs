using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShopApplication.Data;
using PetShopApplication.Models;

namespace PetShopApplication.Controllers
{
    public class AnimalController : Controller
    {
        private readonly PetShopDbContext _context;

        public AnimalController(PetShopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(int id)
        {
            var animal = await _context.Animals
                .Include(a => a.Category)
                .Include(a => a.Comments) // Load related comments
                .FirstOrDefaultAsync(a => a.Id == id);

            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }
    }
}
