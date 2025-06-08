namespace cmet_backend.Attachment
{
    public interface IAttachmentRepository
    {
        Task<List<AttachmentEntity>> GetByReferenceAsync(string referenceId, string referenceType);
        Task<List<AttachmentEntity>> GetByReferencesAsync(List<string> referenceIds, string referenceType);
        Task AddRangeAsync(List<AttachmentEntity> attachments);
        Task DeleteByIdsAsync(List<string> ids);
    }
}
