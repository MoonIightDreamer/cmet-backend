using Microsoft.EntityFrameworkCore;
using System;

namespace cmet_backend.Video
{
    public class VideoRepository : IVideoRepository
    {
        private readonly AppDbContext _context;

        public VideoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VideoMaterialEntity>> GetAllAsync()
        {
            return await _context.VideoMaterials.ToListAsync();
        }

        public async Task<VideoMaterialEntity?> GetByIdAsync(string id)
        {
            return await _context.VideoMaterials.FindAsync(id);
        }

        public async Task AddAsync(VideoMaterialEntity material)
        {
            _context.VideoMaterials.Add(material);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(VideoMaterialEntity material)
        {
            _context.VideoMaterials.Update(material);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _context.VideoMaterials.FindAsync(id);
            if (entity != null)
            {
                _context.VideoMaterials.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsById(string id)
        {
            return await _context.Set<VideoMaterialEntity>().AnyAsync(e => e.Id == id);
        }
    }
}
