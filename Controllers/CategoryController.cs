using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShopApplication.Data;
using PetShopApplication.Models;

namespace PetShopApplication.Controllers
{
    public class CategoryController : Controller
    {
        private readonly PetShopDbContext _context;

        public CategoryController(PetShopDbContext context)
        {
            _context = context;
        }

        // List all categories
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }

        // Add category - GET
        public IActionResult Add()
        {
            var category = new Category();  // Create an empty model
            return View(category);
        }

        // Add category - POST
        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Category added successfully.";
                return RedirectToAction("Index");
            }
            return View(category);  //Returning a view with validation errors
        }

        // Edit category - GET
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        // Edit category - POST
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Category updated successfully.";
            return RedirectToAction("Index");
        }

        // Delete category - POST
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                TempData["Error"] = "Category not found.";
                return RedirectToAction("Index");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Category deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}

