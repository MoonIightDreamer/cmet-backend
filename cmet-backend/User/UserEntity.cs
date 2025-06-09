using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace cmet_backend.User
{
    [Table(name: "\"user\"")]
    public class UserEntity
    {
        [Key]
        [Column(name: "id")]
        public string Id { get; set; }

        [Required]
        [Column(name: "username")]
        public string Username { get; set; }

        [Column(name: "email")]
        public string Email { get; set; }

        [Required]
        [Column(name: "password")]
        public string Password { get; set; }

        [Required]
        [Column(name: "role")]
        public string Role { get; set; } = "student";

        [Column(name: "created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
