
using cmet_backend.Attachment;

namespace cmet_backend.Content
{
    public class ArticleDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<AttachmentDto> Files { get; set; }
    }
}