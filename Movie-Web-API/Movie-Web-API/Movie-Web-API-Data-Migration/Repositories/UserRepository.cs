using Application.IRepositories;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Movie_Web_API_Data_Migration;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MovieWebApiDbContext _dbContext;
        public UserRepository(MovieWebApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDTO?> GetDetailUserAsync(Guid id)
        {
            return await _dbContext.Users.Where(x => x.Id == id).Select(y => new UserDTO {
                FirstName = y.FirstName,
                UserName = y.UserName,
                LastName = y.LastName,
                Email = y.Email,
                Role = y.Role
            }).SingleOrDefaultAsync();
        }

        public async Task<UserDTO?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.Where(x => x.Email == email).Select(y => new UserDTO
            {
                FirstName = y.FirstName,
                UserName = y.UserName,
                LastName = y.LastName,
                Email = y.Email,
                Role = y.Role
            }).SingleOrDefaultAsync();
        }

        public async Task<UserDTO?> GetUserAsync(string userName, string passWord)
        {
            return await _dbContext.Users.Where(x => x.UserName == userName && x.Password == passWord).Select(y => new UserDTO
            {
                Id = y.Id,
                FirstName = y.FirstName,
                UserName = y.UserName,
                LastName = y.LastName,
                Email = y.Email,
                Role = y.Role
            }).SingleOrDefaultAsync();
        }
        public async Task RegisterUserAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
