using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TerryPratchettSite
{
    public class DatabaseHelper : DbContext
    {
        public DbSet<Review> Reviews { get; set; }

        public DatabaseHelper(DbContextOptions<DatabaseHelper> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the model
            modelBuilder.Entity<Review>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Review>()
                .Property(r => r.Email)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Review>()
                .Property(r => r.Content)
                .IsRequired()
                .HasMaxLength(1000);
        }

        public static void InitializeDatabase(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<DatabaseHelper>();
            context.Database.Migrate();
        }
    }

    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
