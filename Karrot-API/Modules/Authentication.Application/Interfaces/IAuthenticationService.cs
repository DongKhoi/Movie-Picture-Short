using System.Security.Claims;

namespace Authentication.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ClaimsPrincipal?> ValidateTokenAsync(string token);
    }
}
