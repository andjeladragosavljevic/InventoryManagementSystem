namespace InventoryManagementSystem
{
    public class ArticleEditDTO
    {
        public required int Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string MeasuringUnit { get; set; }
        public List<AttributeInArticleDTO>? Attributes { get; set; } = [];
    }
}
