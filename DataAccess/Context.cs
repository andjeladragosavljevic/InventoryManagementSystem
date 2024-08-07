using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DataAccess
{
    public class Context : DbContext
    {
        protected Context() { }
        public Context(DbContextOptions options) : base(options) { }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Atribut> Atributi { get; set; }
        public DbSet<Artikl> Artikli { get; set; }
        public DbSet<AtributUArtiklu> AtributiUArtiklu { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AtributUArtiklu>()
                .HasKey(a => new { a.AtributID, a.ArtiklID });
        }

    }
}
