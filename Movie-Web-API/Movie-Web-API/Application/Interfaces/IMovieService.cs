using Domain.Common;
using Domain.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie?>> GetAll();
        Task<Response<Guid>> Create(MovieDTO movieDTO);
    }
}
