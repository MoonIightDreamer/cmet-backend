using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cmet_backend.Video
{
    [Table(name: "video_material")]
    public class VideoMaterial
    {
        [Key]
        [Column(name: "id")]
        public required string Id { get; set; }

        [Required]
        [Column(name: "description")]
        public required string Description { get; set; }

        [Required]
        [Column(name: "created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [Column(name: "link")]
        public required string link { get; set; }
    }
}
