using Microsoft.AspNetCore.Authorization;



namespace InventoryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   // [Authorize]
    public class AtributController : Controller
    {
        private readonly Context _db;

        public AtributController(Context db)
        {
            _db = db;
        }

        [HttpPost("add")]
        public IActionResult AddAtribut(AtributDTO req)
        {
            var atributi = _db.Atributi.Where(a => a.AttributName == req.Name);
            if (atributi.IsNullOrEmpty() && req.Name != null)
            {
                var atribut = new Atribut()
                {
                    AttributName = req.Name
                };

                _db.Atributi.Add(atribut);
                _db.SaveChanges();

                return Ok(atribut);
            }
            return BadRequest();
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAtributi()
        {
            return Ok(await _db.Atributi.ToListAsync());
        }

    }
}
