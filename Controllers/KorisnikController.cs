using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using InventoryManagementSystem.DataAccess;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json;

namespace InventoryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KorisnikController : Controller
    {
        private readonly Context _db;
        private readonly IConfiguration _configuration;

        public static Korisnik LoggedinUser = new Korisnik();

        public KorisnikController(Context db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        [HttpPost("registration")]
        public async Task<IActionResult> AddKorisnik(KorisnikDTO req)
        {

            if (req.Email != null && req.Password != null && req.RepeatedPassword != null && req.Password.Equals(req.RepeatedPassword))
            {
                var userExists = _db.Korisnici.Where(k => k.Email == req.Email).FirstOrDefault();
                if (userExists == null)
                {
                    byte[] salt;
                    var hash = Hash.HashPasword(req.Password, out salt);
                    if (req.FirstName != null && req.LastName != null && req.PhoneNumber != null)
                    {
                        var korisnik = new Korisnik()
                        {
                            FirstName = req.FirstName,
                            LastName = req.LastName,
                            PhoneNumber = req.PhoneNumber,
                            Email = req.Email,
                            Password = hash,
                            PasswordSalt = salt
                        };

                        await _db.Korisnici.AddAsync(korisnik);

                        await _db.SaveChangesAsync();

                        return Ok(korisnik);
                    }
                    return BadRequest("Nisu unesena sva polja!");
                }
                return BadRequest("Postoji korisnik sa unesenom email adresom.");
            }
            return BadRequest("Lozinke se ne poklapaju.");
        }

        [HttpPost("login")]
        public IActionResult Login(KorisnikLoginDTO req)
        {

            var korisnik = _db.Korisnici.Where(k => k.Email == req.Email).FirstOrDefault() as Korisnik;

            if (korisnik != null && req.Password != null)
            {

                if (Hash.VerifyPassword(req.Password, korisnik.Password, korisnik.PasswordSalt))
                {
                    LoggedinUser = korisnik;
                    string token = CreateToken(korisnik);
                    var refreshToken = GenerateRefreshToken();
                    SetRefreshToken(token, korisnik);

                     return Ok(JsonSerializer.Serialize(token));
                    //return Ok(token);
                }
            }
            return BadRequest("Email ili lozinka nisu dobro uneseni");
        }

        private IActionResult RefreshToken()
        {
            var user = LoggedinUser;
            var refreshToken = Request.Cookies["refreshToken"];

            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Nepravilan refresh token.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token je istekao.");
            }

            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken, user);

            return Ok(token);
        }

        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        }

        private void SetRefreshToken(string newRefreshToken, Korisnik user)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7)

            };
            Response.Cookies.Append("refreshToken", newRefreshToken, cookieOptions);
            user.RefreshToken = newRefreshToken;

        }

        private string CreateToken(Korisnik korisnik)
        {
            List<Claim> claims = new List<Claim>
             {
                 new Claim(ClaimTypes.Email, korisnik.Email)

             };

            var exp = DateTime.Now.AddMinutes(60);
            var value = _configuration.GetSection("AppSettings:Token").Value;
            if (value != null)
            {
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: exp,
                    signingCredentials: creds,
                    issuer: value
                    );
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                LoggedinUser.RefreshToken = jwtToken;
                LoggedinUser.TokenExpires = exp;
                return jwtToken;
            }

            return "";

        }

        [HttpGet("logout")]
        public ActionResult Logout()
        {
            return Ok();
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetKorisnici()
        {
            return Ok(await _db.Korisnici.ToListAsync());

        }
    }
}
