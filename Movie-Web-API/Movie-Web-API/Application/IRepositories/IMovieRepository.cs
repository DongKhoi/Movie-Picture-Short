using Domain.Entities;

namespace Application.IRepositories
{
    public interface IMovieRepository
    {
        Task<Movie?> GetDetailMovieAsync(Guid id);
        Task<Movie?> GetMovieAsync();
        Task CreateMovieAsync(Movie movie);
        Task AddNumberLike(Guid id);
        Task MinusNumberLike(Guid id);
    }
}
