using Authentication.Domain.Entities;
using Authentication.Domain.IRepositories;
using Authentication.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories
{
    public class RecoveryTokenRepository : IRecoveryTokenRepository
    {
        private readonly AuthenticationDbContext _dbContext;
        public RecoveryTokenRepository(AuthenticationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<RecoveryToken?> GetTokenAsync(Guid userId)
        {
            return await _dbContext.RecoveryTokens.Where(x => x.UserId == userId).SingleOrDefaultAsync();
        }
        public async Task GenerateTokenAsync(RecoveryToken recoveryToken)
        {
            _dbContext.RecoveryTokens.Add(recoveryToken);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RefreshTokenAsync(RecoveryToken recoveryToken)
        {
            _dbContext.RecoveryTokens.Update(recoveryToken);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RevokeTokenAsync(Guid userId)
        {
            var token = await _dbContext.RecoveryTokens.Where(x => x.Equals(userId)).FirstOrDefaultAsync();
            if (token != null)
            {
                _dbContext.RecoveryTokens.Remove(token);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
