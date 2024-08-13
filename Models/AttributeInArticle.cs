namespace InventoryManagementSystem.Models
{
    public class AttributeInArticle
    {
        public int AttributeID { get; set; }
        public Attribute? Attribute { get; set; }
        public int ArticleID { get; set; }
        public Article? Article { get; set; }
        public string? Value { get; set; }

    }
}
