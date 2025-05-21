using System.ComponentModel.DataAnnotations;

namespace cmet_backend.Entities
{
    public class VideoMaterial
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public string link { get; set; }
    }
}
