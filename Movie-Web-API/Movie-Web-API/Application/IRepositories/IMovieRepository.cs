using Domain.Entities;

namespace Application.IRepositories
{
    public interface IMovieRepository
    {
        Task<Movie?> GetDetailMovieAsync(Guid id);
        Task<IEnumerable<Movie?>> GetAllMovieAsync();
        Task CreateMovieAsync(Movie movie);
    }
}
