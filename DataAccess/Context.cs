using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DataAccess
{
    public class Context : DbContext
    {
        protected Context() { }
        public Context(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<AttributeInArticle> AttributesInArticle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Korisnik");
            modelBuilder.Entity<Attribute>().ToTable("Atribut");
            modelBuilder.Entity<Article>().ToTable("Articl");
            modelBuilder.Entity<AttributeInArticle>().ToTable("AtributUArtiklu");

            modelBuilder.Entity<AttributeInArticle>()
                    .HasKey(a => new { a.AttributeID, a.ArticleID });
        }

    }
}
