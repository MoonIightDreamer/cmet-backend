using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cmet_backend.Content
{
    [Table(name: "article")]
    public class ArticleEntity
    {
        [Key]
        [Column(name: "id")]
        public required string Id { get; set; }

        [Required]
        [Column(name: "text")]
        public required string text { get; set; }

        [Required]
        [Column(name: "created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [Column(name: "link")]
        public required string link { get; set; }
    }
}
