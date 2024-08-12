namespace InventoryManagementSystem.Models
{
    public class Attribute
    {
        public int Id { get; set; }
        public required string AttributeName { get; set; }
        public List<AtributUArtiklu>? AttributesInArticle { get; set; }
    }
}
