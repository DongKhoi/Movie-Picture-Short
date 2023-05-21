
using Authentication.Core.Common.Response;
using Authentication.Domain.DTOs;

namespace Authentication.Application.Interfaces
{
    public interface IUserService
    {
        Task<Response<UserResponse>> Authenticate(AuthenticateRequest request);

        Task<Response<UserResponse>> ProviderRegister(AuthenticateRequest request);
    }
}
