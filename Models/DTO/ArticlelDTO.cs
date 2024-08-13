using InventoryManagementSystem.Models.DTO;

namespace InventoryManagementSystem
{
    public record ArticlelDTO
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string MeasuringUnit { get; set; }
        public List<AttributeInArticleDTO> Attributes { get; set; } = [];
    }
}
