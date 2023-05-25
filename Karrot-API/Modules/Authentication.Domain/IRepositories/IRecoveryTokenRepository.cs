using Authentication.Domain.Entities;

namespace Authentication.Domain.IRepositories
{
    public interface IRecoveryTokenRepository
    {
        Task<RecoveryToken?> GetTokenAsync(Guid userId);
        Task GenerateTokenAsync(RecoveryToken recoveryToken);
        Task RefreshTokenAsync(RecoveryToken recoveryToken);
        Task RevokeTokenAsync(Guid userId);
    }
}
