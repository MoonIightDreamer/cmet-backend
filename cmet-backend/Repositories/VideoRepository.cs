using cmet_backend.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace cmet_backend.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly AppDbContext _context;

        public VideoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VideoMaterial>> GetAllAsync()
        {
            return await _context.VideoMaterials.ToListAsync();
        }

        public async Task<VideoMaterial?> GetByIdAsync(int id)
        {
            return await _context.VideoMaterials.FindAsync(id);
        }

        public async Task AddAsync(VideoMaterial material)
        {
            _context.VideoMaterials.Add(material);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(VideoMaterial material)
        {
            _context.VideoMaterials.Update(material);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.VideoMaterials.FindAsync(id);
            if (entity != null)
            {
                _context.VideoMaterials.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public Task<VideoMaterial?> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> existsById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
