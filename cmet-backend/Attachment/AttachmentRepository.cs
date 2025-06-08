using Microsoft.EntityFrameworkCore;

namespace cmet_backend.Attachment
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly AppDbContext _context;

        public AttachmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AttachmentEntity>> GetByReferenceAsync(string referenceId, string referenceType)
        {
            return await _context.Attachments
                .Where(a => a.ReferenceId == referenceId && a.ReferenceType == referenceType)
                .ToListAsync();
        }

        public async Task<List<AttachmentEntity>> GetByReferencesAsync(List<string> referenceIds, string referenceType)
        {
            return await _context.Attachments
                .Where(a => referenceIds.Contains(a.ReferenceId) && a.ReferenceType == referenceType)
                .ToListAsync();
        }

        public async Task AddRangeAsync(List<AttachmentEntity> attachments)
        {
            _context.Attachments.AddRange(attachments);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdsAsync(List<string> ids)
        {
            var attachments = await _context.Attachments
                .Where(a => ids.Contains(a.Id))
                .ToListAsync();

            _context.Attachments.RemoveRange(attachments);
            await _context.SaveChangesAsync();
        }
    }
}
