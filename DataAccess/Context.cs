using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DataAccess
{
    public class Context : DbContext
    {
        protected Context() { }
        public Context(DbContextOptions options) : base(options) { }
        public DbSet<Korisnik> Users { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<AtributUArtiklu> AttributesInArticle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AtributUArtiklu>()
                .HasKey(a => new { a.AtributID, a.ArtiklID });
        }

    }
}
