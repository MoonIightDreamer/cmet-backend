using cmet_backend.Video;
using Microsoft.EntityFrameworkCore;

namespace cmet_backend.Content
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _context;

        public ArticleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ArticleEntity>> GetAllAsync()
        {
            return await _context.Articles
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<ArticleEntity?> GetByIdAsync(string id)
        {
            return await _context.Articles.FindAsync(id);
        }

        public async Task<bool> ExistsById(string id)
        {
            return await _context.Articles.AnyAsync(a => a.Id == id);
        }

        public async Task AddAsync(ArticleEntity article)
        {
            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ArticleEntity article)
        {
            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Articles.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
