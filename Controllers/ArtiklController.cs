using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem.DataAccess;
using InventoryManagementSystem.Models;

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
        public IActionResult EditArtikl(ArtiklEditDTO req)
        {
            var old = _db.Artikli.Where(a => a.Id == req.Id).SingleOrDefault();
            var atributi = _db.AtributiUArtiklu.Where(a => a.ArtiklID == req.Id).ToList();
            if (old != null)
            {
                old.Code = req.Code;
                old.Name = req.Name;
                old.MeasuringUnit = req.MeasuringUnit;

                if (req.Atributs != null)
                {
                    foreach (var atr in req.Atributs)
                    {
                        var oldAtribut = _db.AtributiUArtiklu.Find(atr.AtributID, req.Id);

                        if (oldAtribut != null && atr.Value != null)
                        {
                            oldAtribut.Value = atr.Value;
                        }
                        else
                        {
                            var atribut = _db.Atributi.Find(atr.AtributID);

                            if (atribut != null && atr.Value != null)
                            {
                                var newAtribut = new AtributUArtiklu()
                                {
                                    Artikl = old,
                                    Atribut = atribut,
                                    ArtiklID = old.Id,
                                    AtributID = atribut.Id,
                                    Value = atr.Value
                                };
                                _db.AtributiUArtiklu.Add(newAtribut);
                            }


                        }
                    }
                }

                _db.SaveChanges();
                return Ok();
            }
            return BadRequest();

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
