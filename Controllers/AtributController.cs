using Microsoft.AspNetCore.Authorization;



namespace InventoryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   // [Authorize]
    public class AtributController(Context db) : Controller
    {
        [HttpPost("add")]
        public IActionResult AddAtribut(AtributDTO req)
        {
            var atributi = db.Atributi.Where(a => a.AttributName == req.Name);
            if (atributi.IsNullOrEmpty() && req.Name != null)
            {
                var atribut = new Atribut()
                {
                    AttributName = req.Name
                };

                db.Atributi.Add(atribut);
                db.SaveChanges();

                return Ok(atribut);
            }
            return BadRequest();
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAtributi()
        {
            return Ok(await db.Atributi.ToListAsync());
        }

    }
}
