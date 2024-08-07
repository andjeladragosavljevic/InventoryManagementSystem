namespace InventoryManagementSystem.Models
{
    public class AtributUArtiklu
    {
        public int AtributID { get; set; }
        public Atribut? Atribut { get; set; }
        public int ArtiklID { get; set; }
        public Artikl? Artikl { get; set; }
        public string? Value { get; set; }

    }
}
