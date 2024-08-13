namespace InventoryManagementSystem.Models.DTO
{
    public record AttributeInArticleDTO
    {
        public required int AtributID { get; set; }
        public string? Value { get; set; }
    }
}
