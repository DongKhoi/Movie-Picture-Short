using Authentication.Domain.DTOs;
using Authentication.Application.Interfaces;
using Authentication.Core.Common.Response;
using Authentication.Domain.Entities;
using Authentication.Domain.IRepositories;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Authentication.Core.Common;

namespace Authentication.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IRecoveryTokenRepository _recoveryTokenRepository;

        public UserService(IUserRepository userRepository, IConfiguration configuration, IRecoveryTokenRepository recoveryTokenRepository)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _recoveryTokenRepository = recoveryTokenRepository;
        }
        public async Task<Response<UserResponse>> Authenticate(AuthenticateRequest request)
        {
            var response = new UserResponse();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:ScretKey"]);
            if (request.Email != null)
            {
                if(request.IsSSO == true)
                {
                    var userExist = await _userRepository.GetUserExistAsync(request.Email, request.TenantId);

                    if (userExist == null)
                    {
                        var user = new User(request);
                        await _userRepository.RegisterUserAsync(user);
                        response = new UserResponse()
                        {
                            Id = user.Id,
                            UserName = user.UserName,
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Role = user.Role,
                            TenantId = user.TenantId
                        };
                    }
                    else
                    {
                        response = new UserResponse()
                        {
                            Id = userExist.Id,
                            UserName = userExist.UserName,
                            Email = userExist.Email,
                            FirstName = userExist.FirstName,
                            LastName = userExist.LastName,
                            Role = userExist.Role,
                            TenantId = userExist.TenantId
                        };
                    }

                }
                else
                {
                    if(!string.IsNullOrEmpty(request.Password))
                    {
                        var encryptPass = Encryption.Encrypt(request.Password);
                        var verifyUser = await _userRepository.VerifyUser(request.Email, encryptPass);
                        if(verifyUser != null)
                        {
                            response = new UserResponse()
                            {
                                Id = verifyUser.Id,
                                UserName = verifyUser.UserName,
                                Email = verifyUser.Email,
                                FirstName = verifyUser.FirstName,
                                LastName = verifyUser.LastName,
                                Role = verifyUser.Role,
                                TenantId = verifyUser.TenantId
                            };
                        }
                        else
                        {
                            return Response<UserResponse>.Error("Email or Password invalid");
                        }
                    }
                }

                string token = "";
                if (string.IsNullOrEmpty(request.Jwt))
                {
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, response.Id!.Value.ToString()),
                    }),
                        Expires = DateTime.UtcNow.AddDays(10),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var jwtToken = tokenHandler.CreateToken(tokenDescriptor);
                    response.Jwt = tokenHandler.WriteToken(jwtToken);

                    token = tokenHandler.WriteToken(jwtToken);
                }
                else
                {
                    response.Jwt = request.Jwt;
                    token = request.Jwt;
                }

                var checkRefreshToken = await _recoveryTokenRepository.GetTokenAsync(response.Id!.Value);
                //refresh-token
                if (checkRefreshToken != null)
                {
                    if (checkRefreshToken.ExpiredDate <= DateTime.UtcNow)
                    {
                        await _recoveryTokenRepository.RefreshTokenAsync(new RecoveryToken(response.Id.Value, token, DateTime.UtcNow.AddDays(10)));
                    }
                }
                else
                {
                    await _recoveryTokenRepository.GenerateTokenAsync(new RecoveryToken(response.Id.Value, token, DateTime.UtcNow.AddDays(10)));
                }
            }
            return Response<UserResponse>.Success(response);
        }
        public async Task<Response<UserResponse>> ProviderRegister(AuthenticateRequest request)
        {
            var response = new UserResponse();
            if(!string.IsNullOrEmpty(request.Email))
            {
                var userExist = await _userRepository.GetUserExistAsync(request.Email, request.TenantId);

                if (userExist != null)
                {
                    return Response<UserResponse>.Error("Username or email already exists");
                }
                else
                {
                    if(!string.IsNullOrEmpty(request.Password))
                    {
                        var encryptPass = Encryption.Encrypt(request.Password);
                        request.Password = encryptPass;
                        User user = new User(request);
                        await _userRepository.RegisterUserAsync(user);
                    }
                    else
                    {
                        return Response<UserResponse>.Error("Password is not empty");
                    }
                }
            }
           
            return Response<UserResponse>.Success(response);
        }

    }
}
