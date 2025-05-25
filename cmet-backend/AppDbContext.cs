using cmet_backend.Video;
using Microsoft.EntityFrameworkCore;

namespace cmet_backend
{
    public class AppDbContext : DbContext
    {
        public DbSet<VideoMaterial> VideoMaterials { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
