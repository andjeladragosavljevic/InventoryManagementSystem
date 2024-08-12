namespace InventoryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class ArticleController(Context db) : Controller
    {

        [HttpPost]
        public IActionResult AddArticle(ArticlelDTO request)
        {

            Article article = new()
            {
                Code = request.Code,
                Name = request.Name,
                MeasuringUnit = request.MeasuringUnit
            };

            db.Add(article);
            db.SaveChanges();

            if (request.Attributes is not null)
            {
                var attributes = new List<AtributUArtiklu>();
                foreach (var attribute in request.Attributes)
                {
                  
                    if (db.Attributes.Find(attribute.AtributID) is var existingAttribute and not null && attribute.Value is not null)
                    {
                        var attributeInArticle = new AtributUArtiklu()
                        {
                            Atribut = existingAttribute,
                            AtributID = attribute.AtributID,
                            Artikl = article,
                            ArtiklID = article.Id,
                            Value = attribute.Value

                        };

                        attributes.Add(attributeInArticle);
                    };

                }

                db.AddRange(attributes);
            }

            db.SaveChanges();
            return Ok(article);
        }

        [HttpDelete]
        public IActionResult DeleteArticle(int id)
        {
            var article = db.Articles.Find(id);

            if (article is null)
                return NotFound();

            db.Articles.Remove(article);
            db.SaveChanges(true);
            return Ok(article);
        }

        [HttpPut]
        public IActionResult EditArtikl(ArticleEditDTO request)
        {
            
            var oldArticle = db.Articles.FirstOrDefault(a => a.Id == request.Id);

            if (oldArticle is null)
                return BadRequest();

            oldArticle.Code = request.Code;
            oldArticle.Name = request.Name;
            oldArticle.MeasuringUnit = request.MeasuringUnit;

            if (request.Attributes is not null)
            {

                foreach (var attribute in request.Attributes)
                {
                    var oldAttribute = db.AttributesInArticle.Find(attribute.AtributID, request.Id);

                    switch (oldAttribute)
                    {
                        case not null when attribute.Value is not null: oldAttribute.Value = attribute.Value; break;
                        case null when attribute.Value is not null && db.Attributes.Find(attribute.AtributID) is var existingAttribute and not null:
                            var newAttributeInArticle = new AtributUArtiklu()
                            {
                                Artikl = oldArticle,
                                Atribut = existingAttribute,
                                ArtiklID = oldArticle.Id,
                                AtributID = existingAttribute.Id,
                                Value = attribute.Value
                            };
                            db.AttributesInArticle.Add(newAttributeInArticle);
                            break;
                    }

                }
            }

            db.SaveChanges();
            return Ok();


        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var articles = await db.Articles.Include(a => a.AtributiUArtiklu).AsNoTracking().ToListAsync();
            return Ok(articles);
        }

        [HttpGet("/id/{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var article = await db.Articles.Include(a => a.AtributiUArtiklu).AsNoTracking().SingleOrDefaultAsync(a => a.Id == id);

            if(article is null) 
                return NotFound();

            return Ok(article);
        }

        [HttpGet("/code/{code}")]
        public async Task<ActionResult> GetByCodeAsync(string code)
        {
            var article = await db.Articles.Include(a => a.AtributiUArtiklu).AsNoTracking().SingleOrDefaultAsync(a => code.Equals(a.Code));

            if (article is null)
                return NotFound();

            return Ok(article);
        }

        [HttpGet("/measuringUnit")]
        public async Task<ActionResult> GetByMeasuringUnitAsync(string value)
        {
            var articles = await db.Articles.Include(a => a.AtributiUArtiklu).Where(a => value.Equals(a.MeasuringUnit)).AsNoTracking().ToListAsync();

            if (articles is null || articles.Count == 0)
                return NotFound();

            return Ok(articles);
        }

        [HttpGet("/name")]
        public async Task<ActionResult> GetByNameAsync(string value)
        {
            var articles = await db.Articles.Include(a => a.AtributiUArtiklu).Where(a => value.Equals(a.Name)).AsNoTracking().ToListAsync();

            if (articles is null || articles.Count == 0)
                return NotFound();

            return Ok(articles);
        }

        [HttpGet("/attribute/{id}/{value}")]
        public async Task<ActionResult> GetByAttributeAsync(int id, string value)
        {
            var articles = await db.AttributesInArticle.Include(a => a.Artikl).Where(atr => atr.AtributID.Equals(id) && value.Equals(atr.Value)).Include(a => a.Artikl).AsNoTracking().ToListAsync();
           
            if (articles is null || articles.Count == 0)
                return NotFound();

            return Ok(articles);
        }
    }
}
