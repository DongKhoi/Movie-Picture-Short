using Authentication.Domain.Entities;
using Authentication.Domain.IRepositories;
using Authentication.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthenticationDbContext _dbContext;
        public UserRepository(AuthenticationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task RegisterUserAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetUserExistAsync(string email, Guid tenantId)
        {
            return await _dbContext.Users.Where(x => x.Email == email && x.TenantId == tenantId).FirstOrDefaultAsync();
        }

        public async Task<User?> VerifyUser(string email, string password)
        {
            return await _dbContext.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
        }
    }
}
