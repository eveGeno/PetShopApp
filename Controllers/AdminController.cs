using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShopApplication.Data;
using PetShopApplication.Models;

namespace PetShopApplication.Controllers
{
    public class AdminController : Controller
    {
        private readonly PetShopDbContext _context;

        public AdminController(PetShopDbContext context)
        {
            _context = context;
        }

        // List all animals
        public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
        {
            var animals = _context.Animals.Include(a => a.Category);

            int totalItems = await animals.CountAsync();
            var items = await animals
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            return View(items);
        }

        // Add animal - GET
        public async Task<IActionResult> Add()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(new Animal());
        }

        // Add animal - POST
        [HttpPost]
        public async Task<IActionResult> Add(Animal animal, string PictureOption, IFormFile? PictureFile)
        {
            // removing the check for the Picture field, since it will be filled in during the process
            ModelState.Remove("Picture");

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View(animal);
            }

            //Image processing logic
            if (PictureOption == "file" && PictureFile != null)
            {
                //Generate a path to download a file (pic)
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", PictureFile.FileName);

                //save pic
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await PictureFile.CopyToAsync(stream);
                }

                //Set the path for the Picture field
                animal.Picture = "/images/" + PictureFile.FileName;
            }
            else if (PictureOption == "url" && !string.IsNullOrWhiteSpace(animal.Picture))
            {
                // Use the specified URL
                animal.Picture = animal.Picture;
            }
            else
            {
                //If URL & file is not specified, return an error
                ModelState.AddModelError("", "Please provide a valid image.");
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View(animal);
            }

            //We save the animal to the database
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Animal added successfully.";
            return RedirectToAction("Index");
        }

        // Edit animal - GET
        public async Task<IActionResult> Edit(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null) return NotFound();

            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(animal);
        }

        // Edit animal - POST
        [HttpPost]
        public async Task<IActionResult> Edit(Animal animal, IFormFile? pictureFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View(animal);
            }

            //If the file (pic) was downloaded
            if (pictureFile != null && pictureFile.Length > 0)
            {
                //Generate a path to save the file
                var fileName = Path.GetFileName(pictureFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                //Save a new image file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await pictureFile.CopyToAsync(stream);
                }

                //Update the path to the image in the database
                animal.Picture = "/images/" + fileName;
            }

            //update DB
            _context.Animals.Update(animal);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Animal updated successfully.";
            return RedirectToAction("Index");
        }

        // Delete animal
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var animal = await _context.Animals
                .Include(a => a.Comments) // Ensure related comments are loaded
                .FirstOrDefaultAsync(a => a.Id == id);

            if (animal == null)
            {
                TempData["Error"] = "Animal not found.";
                return RedirectToAction("Index");
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Animal deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}


