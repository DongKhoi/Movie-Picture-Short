using Domain.Common;
using Domain.DTOs;

namespace Application.Interfaces
{
    public interface IRecoveryTokenService
    {
        Task<Response<Guid>> Logout(Guid userId);
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticate);
        Task<Response<string>> AuthenticateG(string email);
    }
}
