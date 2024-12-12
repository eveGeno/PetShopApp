using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShopApplication.Data;
using PetShopApplication.Models;

namespace PetShopApplication.Controllers
{
    public class CommentController : Controller
    {
        private readonly PetShopDbContext _context;

        public CommentController(PetShopDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int animalId, string commentText)
        {
            if (string.IsNullOrWhiteSpace(commentText))
            {
                TempData["Error"] = "Comment text cannot be empty.";
                return RedirectToAction("Details", "Animal", new { id = animalId });
            }

            var animal = await _context.Animals.FindAsync(animalId);
            if (animal == null)
            {
                TempData["Error"] = "Animal not found.";
                return RedirectToAction("Index", "Catalog");
            }

            var comment = new Comment
            {
                AnimalId = animalId,
                Text = commentText,
                CreatedAt = DateTime.Now
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Comment added successfully.";
            return RedirectToAction("Details", "Animal", new { id = animalId });
        }
    }
}


