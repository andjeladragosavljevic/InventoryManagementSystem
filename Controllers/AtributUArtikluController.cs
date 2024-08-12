namespace InventoryManagementSystem.Controllers
{

    [ApiController]
    [Route("api/controller")]
   // [Authorize]
    public class AtributUArtikluController(Context db) : Controller
    {
        [HttpGet("atribut/get")]
        public async Task<ActionResult> GetAtributUArtiklu()
        {
            var artikli = await db.AttributesInArticle.ToListAsync();
            return Ok(artikli);
        }

        [HttpDelete("atribut/delete")]
        public ActionResult deleteAtributUArtiklu(int AtributID, int ArtiklID)
        {
            var atribut = db.AttributesInArticle.Find(AtributID, ArtiklID);
            if (atribut == null)
                return NotFound();
            db.AttributesInArticle.Remove(atribut);
            db.SaveChanges(true);
            return Ok();
        }

    }

}
