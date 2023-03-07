using Application.Interfaces;
using Domain.Common;
using Domain.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Movie_Web_API.Controllers
{
    [Route("api/identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IRecoveryTokenService _recoveryTokenService;
        private readonly IUserService _userService;
        private readonly string m_tokenKeyName = "refreshtoken";

        public IdentityController(IRecoveryTokenService recoveryTokenService, IUserService userService)
        {
            _recoveryTokenService = recoveryTokenService;
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate([FromBody] AuthenticateRequest authenticate)
        {
            var result = _recoveryTokenService.Authenticate(authenticate);
            if(result == null)
                SetTokenCookie(result.Result.JwtToken);
            return await result;
        }

        [HttpPost("revoke-token/{userId}")]
        public async Task<ActionResult<Response<Guid>>> Logout([FromRoute] Guid userId)
        {
            return await _recoveryTokenService.Logout(userId);
        }
       
        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),//time of cookie life is seven day
                SameSite = SameSiteMode.Strict,
            };
            Response.Cookies.Append(m_tokenKeyName, token, cookieOptions);
        }
    }
}
