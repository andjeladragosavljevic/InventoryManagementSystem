using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Artikl
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string? Code { get; set; } // Sifra

        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength(20)]
        public string? MeasuringUnit { get; set; }

        public List<AtributUArtiklu>? AtributiUArtiklu { get; set; }

    }
}
