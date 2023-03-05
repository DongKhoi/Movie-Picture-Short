using Application.Interfaces;
using Domain.Common;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Movie_Web_API.Controllers
{
    [Route("api/identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IRecoveryTokenService _recoveryTokenService;
        private readonly string m_tokenKeyName = "refreshtoken";

        public IdentityController(IRecoveryTokenService recoveryTokenService)
        {
            _recoveryTokenService = recoveryTokenService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<Response<string>>> Authenticate([FromBody] AuthenticateRequest authenticate)
        {
            var result = _recoveryTokenService.Authenticate(authenticate);
            if(result.Result.ErrorMessage == null)
                SetTokenCookie(result.Result.Data);
            return await result;
        }
        //[Authorize]
        //[HttpPost("authenticateGoogle")]
        //public async Task<ActionResult<Response<string>>> AuthenticateGoogle([FromBody] AuthenticateRequest authenticate)
        //{
            
        //}
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
