using Authentication.Application.Interfaces;
using Authentication.Application.Services;
using Authentication.Core.Common.Response;
using Authentication.Domain.DTOs;
using ITfoxtec.Identity.Saml2;
using ITfoxtec.Identity.Saml2.MvcCore;
using ITfoxtec.Identity.Saml2.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        const string relayStateReturnUrl = "https://localhost:7219/";
        private readonly Saml2Configuration config;
        private readonly IUserService _userService;
        private readonly ITenantService _tenantService;

        public IdentityController(IOptions<Saml2Configuration> configAccessor, IUserService userService, ITenantService tenantService)
        {
            config = configAccessor.Value;
            _userService = userService;
            _tenantService = tenantService;
        }

        [HttpGet("Login")]
        public async Task<IActionResult> LoginAsync(string? returnUrl = null)
        {
            var binding = new Saml2RedirectBinding();
            var uri = new Uri(HttpContext.Items["Url"].ToString());
            binding.SetRelayStateQuery(new Dictionary<string, string> {
                { relayStateReturnUrl, returnUrl ?? Url.Content("~/") },
                { "domain", Convert.ToString(uri.Host + (uri.IsDefaultPort ? "" : ":" + uri.Port)) ?? string.Empty }
            });
            return binding.Bind(new Saml2AuthnRequest(config)).ToActionResult();
        }

        [HttpPost("AssertionConsumerService")]
        public async Task<Response<UserResponse>> AssertionConsumerService()
        {
            var binding = new Saml2PostBinding();
            var saml2AuthnResponse = new Saml2AuthnResponse(config);

            binding.ReadSamlResponse(Request.ToGenericHttpRequest(), saml2AuthnResponse);
            if (saml2AuthnResponse.Status != Saml2StatusCodes.Success)
            {
                throw new AuthenticationException($"SAML Response status: {saml2AuthnResponse.Status}");
            }

            var saml2SecurityToken = saml2AuthnResponse.Saml2SecurityToken.Assertion;

            var jwtPayload = new JwtPayload
            {
                { ClaimTypes.NameIdentifier, saml2SecurityToken.Subject.NameId.Value },
            };
            var key = Encoding.ASCII.GetBytes(saml2SecurityToken.Signature.SignatureValue);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: saml2SecurityToken.Issuer.Value,
                audience: "Karrot",
                claims: jwtPayload.Claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature));

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            var userRequest = new AuthenticateRequest()
            {
                UserName = saml2SecurityToken.Subject.NameId.Value,
                Email = saml2SecurityToken.Subject.NameId.Value,
                FirstName = null,
                LastName = null,
                Password = null,
                Jwt = jwtToken,
                IsSSO = true
            };

            string? url = null;
            if (!string.IsNullOrEmpty(HttpContext.Items["Url"] as string))
                url = HttpContext.Items["Url"] as string;
            else
            {
                var relayStateQuery = binding.GetRelayStateQuery();
                url = relayStateQuery["domain"].ToString();
            }
            if(!string.IsNullOrEmpty(url))
            {
                userRequest.TenantId = await _tenantService.GetTenantIdByDomain(url);
                return await _userService.Authenticate(userRequest);
            }
            return Response<UserResponse>.Error("Domain is invalid !");
        }

        [HttpPost("provider-login")]
        public async Task<Response<UserResponse>> AuthenticateAsync([FromBody] AuthenticateRequest request)
        {
            request.IsSSO = false;
            string? url = HttpContext.Items["Url"] as string;

            if (!string.IsNullOrEmpty(url))
            {
                request.TenantId = await _tenantService.GetTenantIdByDomain(url);
                return await _userService.Authenticate(request);

            }
            return Response<UserResponse>.Error("Domain is invalid !");
        }

        [HttpPost("provider-register")]
        public async Task<Response<UserResponse>> RegisterAsync([FromBody] AuthenticateRequest request)
        {
            string? url = HttpContext.Items["Url"] as string;

            if (!string.IsNullOrEmpty(url))
            {
                request.TenantId = await _tenantService.GetTenantIdByDomain(url);
                return await _userService.ProviderRegister(request);

            }
            return Response<UserResponse>.Error("Domain is invalid !");
        }
    }
}
