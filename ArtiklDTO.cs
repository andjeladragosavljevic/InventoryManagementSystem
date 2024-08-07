using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem
{
    public class ArtiklDTO
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? MeasuringUnit { get; set; }
        public List<AtributUArtikluDTO>? Atributs { get; set; } = new List<AtributUArtikluDTO>();
    }
}
