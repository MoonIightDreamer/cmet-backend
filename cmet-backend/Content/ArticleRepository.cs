using cmet_backend.Video;

namespace cmet_backend.Content
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _context;

        public ArticleRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(ArticleEntity content)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ArticleEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ArticleEntity?> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ArticleEntity material)
        {
            throw new NotImplementedException();
        }
    }
}
