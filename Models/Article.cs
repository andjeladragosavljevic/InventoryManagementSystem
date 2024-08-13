using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Article
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public required string Code { get; set; } 

        [MaxLength(50)]
        public required string Name { get; set; }

        [MaxLength(20)]
        public required string MeasuringUnit { get; set; }

        public List<AttributeInArticle>? AttributeInArticles { get; set; }

    }
}
