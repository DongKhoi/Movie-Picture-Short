using Application.Interfaces;
using Application.IRepositories;
using Domain.Common;
using Domain.DTOs;
using Domain.Entities;

namespace Application.Bussiness
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDTO?> GetUser(Guid id)
        {
            return await _userRepository.GetDetailUserAsync(id);
        }

        public async Task<Response<Guid>> Register(UserDTO userDTO)
        {
            User user = new User(userDTO);
            await _userRepository.RegisterUserAsync(user);
            return Response<Guid>.Success(user.Id);
        }
    }
}
