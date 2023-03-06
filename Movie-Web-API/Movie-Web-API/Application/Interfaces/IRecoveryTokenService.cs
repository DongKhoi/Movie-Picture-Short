using Domain.Common;
using Domain.DTOs;

namespace Application.Interfaces
{
    public interface IRecoveryTokenService
    {
        Task<Response<Guid>> Logout(Guid userId);
        Task<Response<string>> Authenticate(AuthenticateRequest authenticate);
        Task<Response<string>> AuthenticateG(string email, string ipAddress);
    }
}
