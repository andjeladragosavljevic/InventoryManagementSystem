

namespace InventoryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    public class AttributeController(Context db) : Controller
    {
        [HttpPost]
        public IActionResult AddAttributeAsync(AttributeDTO request)
        {

            if (request.Name is not null && !db.Attributes.Any(a => a.AttributeName == request.Name))
            {
                var attribute = new Attribute()
                {
                    AttributeName = request.Name
                };

                db.Attributes.Add(attribute);
                db.SaveChanges();

                return Ok(attribute);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAttributesAsync()
        {
            return Ok(await db.Attributes.ToListAsync());
        }

    }
}
