using System.Xml.Linq;

namespace PetShopApplication.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Animal
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Range(0, 100, ErrorMessage = "Age must be between 0 and 100")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Picture URL is required")]
        public string Picture { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
        public List<Comment> Comments { get; set; } = new();
    }
}