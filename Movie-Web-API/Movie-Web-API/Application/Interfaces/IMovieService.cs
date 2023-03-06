using Domain.Common;
using Domain.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMovieService
    {
        Task<Movie?> GetMovie();
        Task<Movie?> GetDetailMovie(Guid id);
        Task<Response<Guid>> Create(MovieDTO movieDTO);
    }
}
