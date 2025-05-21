using cmet_backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace cmet_backend.Repositories
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
