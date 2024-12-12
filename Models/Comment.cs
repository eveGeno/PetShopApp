namespace PetShopApplication.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Comment text is required")]
        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public int AnimalId { get; set; }

        public Animal Animal { get; set; } = null!;
    }
}
