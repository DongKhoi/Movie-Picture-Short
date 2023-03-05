using Domain.Common;
using Domain.DTOs;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO?> GetUser(Guid id);
        Task<Response<Guid>> Register(UserDTO userDTO);
    }
}
