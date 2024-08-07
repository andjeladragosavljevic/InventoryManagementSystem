using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Korisnik
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [MaxLength(200)]
        public string Email { get; set; }

        public string Password { get; set; }

        public byte[] PasswordSalt { get; set; }

        [NotMapped]
        public string RefreshToken { get; set; }

        [NotMapped]
        public DateTime TokenExpires { get; set; }

    }
}
