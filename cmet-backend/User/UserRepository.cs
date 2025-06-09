using Microsoft.EntityFrameworkCore;

namespace cmet_backend.User
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<UserEntity?> GetByIdAsync(string id)
        {
            return await dbContext.Users.FindAsync(id);
        }

        public async Task<bool> ExistsByIdAsync(string id)
        {
            return await dbContext.Users.AnyAsync(u => u.Id == id);
        }

        public async Task AddAsync(UserEntity user)
        {
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserEntity user)
        {
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                dbContext.Users.Remove(user);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
