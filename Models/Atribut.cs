namespace InventoryManagementSystem.Models
{
    public class Atribut
    {
        public int Id { get; set; }
        public string? AttributName { get; set; }
        public List<AtributUArtiklu>? AtributiUArtiklu { get; set; }
    }
}
