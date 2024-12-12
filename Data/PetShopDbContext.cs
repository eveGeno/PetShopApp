using Microsoft.EntityFrameworkCore;
using PetShopApplication.Models;

namespace PetShopApplication.Data
{
    public class PetShopDbContext : DbContext
    {
        public PetShopDbContext(DbContextOptions<PetShopDbContext> options) : base(options) { }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Animal)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.AnimalId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete
        }
    }
}
