using Application.IRepositories;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Movie_Web_API_Data_Migration;

namespace Infrastructure.Repositories
{
    public class RecoveryTokenRepository : IRecoveryTokenRepository
    {
        private readonly MovieWebApiDbContext _dbContext;
        public RecoveryTokenRepository(MovieWebApiDbContext dbContext)
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
    }
}
