using cmet_backend.Attachment;
using cmet_backend.Content;
using cmet_backend.Video;
using Microsoft.EntityFrameworkCore;

namespace cmet_backend
{
    public class AppDbContext : DbContext
    {
        public DbSet<VideoMaterialEntity> VideoMaterials { get; set; }

        public DbSet<ArticleEntity> Articles { get; set; }

        public DbSet<AttachmentEntity> Attachments { get; set; }

        public DbSet<UserEntity> Users { get; set; }    

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
