using Domain.DTOs;
using Domain.Entities;

namespace Application.IRepositories
{
    public interface IUserRepository
    {
        Task<UserDTO?> GetDetailUserAsync(Guid id);
        Task<UserDTO?> GetUserAsync(string userName, string passWord);
        Task RegisterUserAsync(User user);
    }
}
