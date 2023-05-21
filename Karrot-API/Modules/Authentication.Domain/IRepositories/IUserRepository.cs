using Authentication.Domain.Entities;

namespace Authentication.Domain.IRepositories
{
    public interface IUserRepository
    {
        Task RegisterUserAsync(User user);
        Task<User?> GetUserExistAsync(string email, Guid tenantId);
        Task<User?> VerifyUser(string email, string password);
    }
}
