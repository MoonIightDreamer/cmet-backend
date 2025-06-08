using cmet_backend.Content;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cmet_backend.Attachment
{
    [Table(name: "attachment")]
    public class AttachmentEntity
    {
        [Key]
        [Column(name: "id")]
        public string Id { get; set; }

        [Required]
        [Column(name: "name")]
        public string Name { get; set; }

        [Required]
        [Column(name: "reference_id")]
        public string ReferenceId { get; set; } // ID сущности, к которой привязано вложение

        [Required]
        [Column(name: "reference_type")]
        public string ReferenceType { get; set; }

        [Required]
        [Column(name: "uploaded_at")]
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
