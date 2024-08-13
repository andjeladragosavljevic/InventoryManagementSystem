using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public required string FirstName { get; set; }

        [MaxLength(50)]
        public required string LastName { get; set; }

        [MaxLength(20)]
        public required string PhoneNumber { get; set; }

        [MaxLength(200)]
        public required string Email { get; set; }

        public required string Password { get; set; }

        public required byte[] PasswordSalt { get; set; }

        [NotMapped]
        public string? RefreshToken { get; set; }

        [NotMapped]
        public DateTime TokenExpires { get; set; } = DateTime.MinValue;

    }
}
