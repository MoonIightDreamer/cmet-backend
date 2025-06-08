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
        [Column(name: "title")]
        public required string Title { get; set; }

        [Required]
        [Column(name: "text")]
        public required string Text { get; set; }

        [Required]
        [Column(name: "created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
