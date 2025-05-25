namespace cmet_backend.Video
{
    public interface IVideoRepository
    {
        Task<IEnumerable<VideoMaterial>> GetAllAsync();
        Task<VideoMaterial?> GetByIdAsync(string id);
        Task<bool> ExistsById(string id);
        Task AddAsync(VideoMaterial material);
        Task UpdateAsync(VideoMaterial material);
        Task DeleteAsync(string id);
    }
}
