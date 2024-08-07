namespace InventoryManagementSystem
{
    public class ArtiklEditDTO
    {
        public int? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? MeasuringUnit { get; set; }
        public List<AtributUArtikluDTO>? Atributs { get; set; } = new List<AtributUArtikluDTO>();
    }
}
