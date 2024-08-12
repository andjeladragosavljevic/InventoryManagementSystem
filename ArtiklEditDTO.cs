namespace InventoryManagementSystem
{
    public class ArtiklEditDTO
    {
        public required int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? MeasuringUnit { get; set; }
        public List<AtributUArtikluDTO>? Attributes { get; set; } = new List<AtributUArtikluDTO>();
    }
}
