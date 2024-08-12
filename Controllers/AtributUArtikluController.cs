namespace InventoryManagementSystem.Controllers
{

    [ApiController]
    [Route("api/controller")]
   // [Authorize]
    public class AtributUArtikluController : Controller
    {
        private readonly Context _db;

        public AtributUArtikluController(Context db)
        {
            _db = db;
        }

        [HttpGet("atribut/get")]
        public async Task<ActionResult> GetAtributUArtiklu()
        {
            var artikli = await _db.AtributiUArtiklu.ToListAsync();
            return Ok(artikli);
        }

        [HttpDelete("atribut/delete")]
        public ActionResult deleteAtributUArtiklu(int AtributID, int ArtiklID)
        {
            var atribut = _db.AtributiUArtiklu.Find(AtributID, ArtiklID);
            if (atribut == null)
                return NotFound();
            _db.AtributiUArtiklu.Remove(atribut);
            _db.SaveChanges(true);
            return Ok();
        }

    }

}
