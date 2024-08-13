using InventoryManagementSystem.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json;

namespace InventoryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(Context db, IConfiguration configuration) : Controller
    {
        private string? ValidateUserRegistration(UserDTO request)
        {
            if (request.Email is null || request.Password is null || request.RepeatedPassword is null)
            {
                return "All fields must be filled.";
            }

            if (!request.Password.Equals(request.RepeatedPassword))
            {
                return "Passwords do not match.";
            }

            if (db.Users.FirstOrDefault(u => u.Email == request.Email) is not null)
            {
                return "A user with the provided email address already exists.";
            }

            if (request.FirstName is null || request.LastName is null || request.PhoneNumber is null)
            {
                return "All fields must be filled.";
            }

            return null;
        }


        [HttpPost("registration")]
        public async Task<IActionResult> AddUser(UserDTO request)
        {

            var validationError = ValidateUserRegistration(request);
            if (validationError is not null)
            {
                return BadRequest(validationError);
            }

            var hash = Hash.HashPasword(request.Password, out byte[] salt);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Password = hash,
                PasswordSalt = salt
            };

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO request)
        {
            if (db.Users.Where(u => u.Email == request.Email).FirstOrDefault() is User user && request.Password is not null)
            {

                if (Hash.VerifyPassword(request.Password, user.Password, user.PasswordSalt))
                {
                    string token = CreateToken(user);
                    var refreshToken = GenerateRefreshToken();
                    await SetRefreshToken(token, user);

                    return Ok(token);

                }
            }
            return BadRequest("Incorrect email or password.");
        }

        private async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var user = await db.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user == null || user.TokenExpires < DateTime.Now)
                return Unauthorized("Invalid refresh token.");


            string newToken = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            await SetRefreshToken(newRefreshToken, user);

            return Ok(new { Token = newToken, RefreshToken = newRefreshToken });
        }

        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        }

        private async Task SetRefreshToken(string newRefreshToken, User user)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7)

            };
            Response.Cookies.Append("refreshToken", newRefreshToken, cookieOptions);
            user.RefreshToken = newRefreshToken;
            user.TokenExpires = DateTime.Now.AddDays(7);

            await db.SaveChangesAsync();
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = [new Claim(ClaimTypes.Email, user.Email)];

            var token = configuration["AppSettings:Token"] ?? throw new InvalidOperationException("Token is not configured.");
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(token));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var jwt = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }

        [HttpGet("logout")]
        public async Task<ActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var user = await db.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user is null)
            {
                return Unauthorized("Invalid refresh token.");
            }


            user.RefreshToken = null;
            user.TokenExpires = DateTime.MinValue;
            await db.SaveChangesAsync();

            // Brisanje refresh tokena sa klijenta
            Response.Cookies.Delete("refreshToken");

            return Ok("Logout successful");
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            return Ok(await db.Users.AsNoTracking().ToListAsync());

        }
    }
}
