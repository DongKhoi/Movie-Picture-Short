using Domain.Common;
using Domain.DTOs;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO?> GetUser(Guid id);
        Task<Response<string>> Register(UserDTO userDTO);
        Task<bool> CheckUserExist(string email);
        Task<Guid> GetIdUserExist(string email);

    }
}
