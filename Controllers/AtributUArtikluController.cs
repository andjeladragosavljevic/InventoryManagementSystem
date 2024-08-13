namespace InventoryManagementSystem.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
   // [Authorize]
    public class AttributeInArticleController(Context db) : Controller
    {
        [HttpGet]
        public async Task<ActionResult> GetAttributesInArticleAsync()
        {
            var articles = await db.AttributesInArticle.ToListAsync();
            return Ok(articles);
        }

        [HttpDelete]
        public ActionResult DeleteAttributeInArticle(int AttributeID, int ArticleID)
        {
            var attribute = db.AttributesInArticle.Find(AttributeID, ArticleID);

            if (attribute is null)
                return NotFound();

            db.AttributesInArticle.Remove(attribute);
            db.SaveChanges();
            return Ok();
        }

    }

}
