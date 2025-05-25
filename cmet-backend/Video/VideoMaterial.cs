using System.ComponentModel.DataAnnotations;

namespace cmet_backend.Video
{
    public class VideoMaterial
    {
        [Key]
        public required string Id { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public required string link { get; set; }
    }
}
