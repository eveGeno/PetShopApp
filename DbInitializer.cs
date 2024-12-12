using PetShopApplication.Data;
using PetShopApplication.Models;
using System.Xml.Linq;

namespace PetShopApplication
{
    public static class DbInitializer
    {
        public static void Initialize(PetShopDbContext context)
        {
            if (context.Animals.Any() || context.Categories.Any())
            {
                Console.WriteLine("Database already has data.");
                return;
            }

            Console.WriteLine("Seeding database...");

            // categories
            var categories = new List<Category>
            {
                new Category { Name = "Mammal" },
                new Category { Name = "Reptile" },
                new Category { Name = "Aquatic" },
                new Category { Name = "Bird" }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();

            // animals
            var animals = new List<Animal>
            {
                new Animal
                {
                    Name = "Dog",
                    Age = 3,
                    Picture = "/images/dog.jpg",
                    Description = "Friendly dog.",
                    CategoryId = categories.First(c => c.Name == "Mammal").Id,
                    Comments = new List<Comment>
                    {
                        new Comment { Text = "Great dog!", CreatedAt = DateTime.Now },
                        new Comment { Text = "So cute!", CreatedAt = DateTime.Now }
                    }
                },
                new Animal
                {
                    Name = "Cat",
                    Age = 2,
                    Picture = "/images/cat.jpg",
                    Description = "Cute cat.",
                    CategoryId = categories.First(c => c.Name == "Mammal").Id,
                    Comments = new List<Comment>
                    {
                        new Comment { Text = "Lovely cat!", CreatedAt = DateTime.Now },
                        new Comment { Text = "W0w!", CreatedAt = DateTime.Now },
                        new Comment { Text = "Best cat ever", CreatedAt = DateTime.Now }
                    }
                },
                new Animal
                {
                    Name = "Turtle",
                    Age = 5,
                    Picture = "/images/turtle.jpg",
                    Description = "Slow but steady.",
                    CategoryId = categories.First(c => c.Name == "Reptile").Id
                }
            };

            context.Animals.AddRange(animals);
            context.SaveChanges();

            Console.WriteLine("Database seeding complete.");
        }
    }
}
