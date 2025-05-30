namespace cmet_backend.Video
{
    public interface IVideoRepository
    {
        Task<IEnumerable<VideoMaterialEntity>> GetAllAsync();
        Task<VideoMaterialEntity?> GetByIdAsync(string id);
        Task<bool> ExistsById(string id);
        Task AddAsync(VideoMaterialEntity material);
        Task UpdateAsync(VideoMaterialEntity material);
        Task DeleteAsync(string id);
    }
}
