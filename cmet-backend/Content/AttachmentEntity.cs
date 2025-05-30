using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cmet_backend.Content
{
    [Table(name: "attachment")]
    public class AttachmentEntity
    {
        [Key]
        [Column(name: "id")]
        public required string Id { get; set; }

        [ForeignKey("fk_attachment_to_content")]
        [Column(name: "content_id")]
        public required ArticleEntity Content { get; set; }

        [Required]
        [Column(name: "name")]
        public required string Name { get; set; }

        [Required]
        [Column(name: "created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
