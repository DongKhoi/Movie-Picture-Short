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
    [ApiController]
    public class GoogleAuthenticationController : ControllerBase
    {
        private readonly IRecoveryTokenService _recoveryTokenService;
        private readonly IUserService _userService;

        public GoogleAuthenticationController(IRecoveryTokenService recoveryTokenService, IUserService userService)
        {
            _recoveryTokenService = recoveryTokenService;
            _userService = userService;
        }
        [HttpGet("google-login")]
        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties 
            {
                //RedirectUri = "http://localhost:5000/callback-google-login" 
                RedirectUri = "http://localhost:5000/callback-google-login" 
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("callback-google-login")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (result.Ticket == null)
            {
                return Redirect("google-login");
            }

            var claims = result.Principal.Identities
                    .FirstOrDefault().Claims.Select(claim => new
                    {
                        claim.Issuer,
                        claim.OriginalIssuer,
                        claim.Type,
                        claim.Value
                    });

            string fname = "";
            string lname = "";
            string email = "";
            //var jwt = "";
            if (claims.Count() > 3)
            {
                fname = claims.ToList()[2].Value.ToString();
                lname = claims.ToList()[3].Value.ToString();
                email = claims.ToList()[4].Value.ToString();
            }

            var user = await _userService.CheckUserExist(email);
            var userId = await _userService.GetIdUserExist(email);
            Response<string> jwt = null;

            if (user == true)
            {
                jwt = _recoveryTokenService.AuthenticateG(email).Result;
            }
            else
            {
                UserDTO userDTO = new UserDTO()
                {
                    Id = new Guid(),
                    Email = email,
                    FirstName = fname,
                    LastName = lname,
                    UserName = email,
                    Role = Role.Guest
                };
                await _userService.Register(userDTO);

                jwt = _recoveryTokenService.AuthenticateG(email).Result;
            }
            return Redirect(@"http://localhost:4200/login?token=" + jwt.Data +"&userId=" + userId);
        }
    }
}
