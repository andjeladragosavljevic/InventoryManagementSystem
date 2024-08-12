namespace InventoryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/controller")]
    //[Authorize]
    public class ArtiklController : Controller
    {
        private readonly Context _db;

        public ArtiklController(Context db)
        {
            _db = db;
        }


        [HttpPost("add")]
        public IActionResult AddArtikl(ArtiklDTO request)
        {
            var atributs = new List<AtributUArtiklu>();

            Artikl artikl = new Artikl()
            {
                Code = request.Code,
                Name = request.Name,
                MeasuringUnit = request.MeasuringUnit
            };

            _db.Add(artikl);
            _db.SaveChanges();

            if (request.Atributs != null)
            {
                foreach (var atr in request.Atributs)
                {
                    var atribut = _db.Atributi.Find(atr.AtributID);
                    if (atribut != null && atr.Value != null)
                    {
                        var atrUArtiklu = new AtributUArtiklu()
                        {
                            Atribut = atribut,
                            AtributID = atr.AtributID,
                            Artikl = artikl,
                            ArtiklID = artikl.Id,
                            Value = atr.Value

                        };

                        atributs.Add(atrUArtiklu);
                    }


                }

                _db.AddRange(atributs);
            }
            _db.SaveChanges();
            return Ok(artikl);
        }

        [HttpDelete("delete")]
        public IActionResult DeleteArtikl(int id)
        {
            var artikl = _db.Artikli.Find(id);
            if (artikl == null)
                return NotFound();
            _db.Artikli.Remove(artikl);
            _db.SaveChanges(true);
            return Ok(artikl);
        }

        [HttpPut("edit")]
        public IActionResult EditArtikl(ArtiklEditDTO request)
        {
            var oldArticle = _db.Artikli.FirstOrDefault(a => a.Id == request.Id);

            if (oldArticle is null)
                return BadRequest();

            oldArticle.Code = request.Code;
            oldArticle.Name = request.Name;
            oldArticle.MeasuringUnit = request.MeasuringUnit;

            if (request.Attributes is not null)
            {

                foreach (var attribute in request.Attributes)
                {
                    var oldAttribute = _db.AtributiUArtiklu.Find(attribute.AtributID, request.Id);

                    switch (oldAttribute)
                    {
                        case not null when attribute.Value is not null: oldAttribute.Value = attribute.Value; break;
                        case null when attribute.Value is not null && _db.Atributi.Find(attribute.AtributID) is var existingAttribute and not null:
                            var newAttributeInArticle = new AtributUArtiklu()
                            {
                                Artikl = oldArticle,
                                Atribut = existingAttribute,
                                ArtiklID = oldArticle.Id,
                                AtributID = existingAttribute.Id,
                                Value = attribute.Value
                            };
                            _db.AtributiUArtiklu.Add(newAttributeInArticle);
                            break;
                    }

                }
            }

            _db.SaveChanges();
            return Ok();


        }

        [HttpGet("get")]
        public async Task<ActionResult> Get()
        {
            var artikli = await _db.Artikli.Include(a => a.AtributiUArtiklu).ToListAsync();
            return Ok(artikli);
        }

        [HttpGet("get/id")]
        public async Task<ActionResult> GetById(int id)
        {
            var artikli = await _db.Artikli.Include(a => a.AtributiUArtiklu).Where(a => a.Id == id).ToListAsync();
            return Ok(artikli);
        }

        [HttpGet("get/code")]
        public async Task<ActionResult> GetByCode(string value)
        {
            var artikli = await _db.Artikli.Include(a => a.AtributiUArtiklu).Where(a => value.Equals(a.Code)).ToListAsync();
            return Ok(artikli);
        }

        [HttpGet("get/measuringUnit")]
        public async Task<ActionResult> GetByMeasuringUnit(string value)
        {
            var artikli = await _db.Artikli.Include(a => a.AtributiUArtiklu).Where(a => value.Equals(a.MeasuringUnit)).ToListAsync();
            return Ok(artikli);
        }

        [HttpGet("get/name")]
        public async Task<ActionResult> GetByName(string value)
        {
            var artikli = await _db.Artikli.Include(a => a.AtributiUArtiklu).Where(a => value.Equals(a.Name)).ToListAsync();
            return Ok(artikli);
        }

        [HttpGet("get/atribut")]
        public async Task<ActionResult> GetByAtribut(int id, string value)
        {
            var artikli = await _db.AtributiUArtiklu.Where(atr => atr.AtributID.Equals(id) && value.Equals(atr.Value)).Include(a => a.Artikl).ToListAsync();
            return Ok(artikli);
        }
    }
}
