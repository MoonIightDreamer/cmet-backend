namespace cmet_backend.Content
{
    public interface IArticleRepository
    {
        Task<IEnumerable<ArticleEntity>> GetAllAsync();
        Task<ArticleEntity?> GetByIdAsync(string id);
        Task<bool> ExistsById(string id);
        Task AddAsync(ArticleEntity article);
        Task UpdateAsync(ArticleEntity article);
        Task DeleteAsync(string id);
    }
}
