namespace cmet_backend.Content
{
    public interface IArticleRepository
    {
        Task<IEnumerable<ArticleEntity>> GetAllAsync();
        Task<ArticleEntity?> GetByIdAsync(string id);
        Task<bool> ExistsById(string id);
        Task AddAsync(ArticleEntity material);
        Task UpdateAsync(ArticleEntity material);
        Task DeleteAsync(string id);
    }
}
