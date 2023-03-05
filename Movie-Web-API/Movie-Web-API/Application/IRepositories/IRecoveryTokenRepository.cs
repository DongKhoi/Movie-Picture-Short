using Domain.DTOs;
using Domain.Entities;

namespace Application.IRepositories
{
    public interface IRecoveryTokenRepository
    {
        Task<RecoveryToken?> GetTokenAsync(Guid userId);
        Task GenerateTokenAsync(RecoveryToken recoveryToken);
        Task RefreshTokenAsync(RecoveryToken recoveryToken);
    }
}
