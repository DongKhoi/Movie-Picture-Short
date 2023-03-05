using Application.Interfaces;
using Application.IRepositories;
using Domain.Common;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Bussiness
{
    public class RecoveryTokenService : IRecoveryTokenService
    {
        private readonly IRecoveryTokenRepository _recoveryTokenRepository;
        private readonly IUserRepository _userRepository;
        public RecoveryTokenService(IRecoveryTokenRepository recoveryTokenRepository, IUserRepository userRepository)
        {
            _recoveryTokenRepository = recoveryTokenRepository;
            _userRepository = userRepository;
        }
        public async Task<Response<string>> Authenticate(AuthenticateRequest authenticateRequest)
        {
            var user = await _userRepository.GetUserAsync(authenticateRequest.UserName, authenticateRequest.Password);
            if (user == null)
            {
                return Response<string>.Error("username or password is incorrect");
            }
            var recoveryToken = await _recoveryTokenRepository.GetTokenAsync(user.Id.Value);

            return Response<string>.Success(await GenerateJwtToken(user, recoveryToken));

        }

        private async Task<string> GenerateJwtToken(UserDTO user, RecoveryToken recoveryToken)
        {
            var l_tokenHandler = new JwtSecurityTokenHandler();
            var l_key = Encoding.ASCII.GetBytes("S#$33ab654te^#^$KD%^64");//secret string
            var l_tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, ((int)user.Role).ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(l_key), SecurityAlgorithms.HmacSha256Signature)
            };
            var jwt_token = l_tokenHandler.CreateToken(l_tokenDescriptor);
            var tokenDTO = new TokenDTO()
            {
                Token = l_tokenHandler.WriteToken(jwt_token),
                ExpiredDate = l_tokenDescriptor.Expires.Value,
                UserId = user.Id.Value
            };
            //generate token
            if(recoveryToken == null)
            {
                tokenDTO.ExpiredDate.AddDays(10);
                await _recoveryTokenRepository.GenerateTokenAsync(new RecoveryToken(tokenDTO));
            }
            //refresh token
            else if (recoveryToken != null && recoveryToken.ExpiredDate >= DateTime.Now)
            {
                tokenDTO.ExpiredDate.AddDays(10);
                await _recoveryTokenRepository.RefreshTokenAsync(recoveryToken);
            }
            return l_tokenHandler.WriteToken(jwt_token);
        }
    }
}
