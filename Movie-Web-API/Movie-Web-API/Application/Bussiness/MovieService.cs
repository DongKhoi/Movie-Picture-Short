using Domain.Common;
using Domain.DTOs;
using Domain.Entities;
using Application.Interfaces;
using Application.IRepositories;

namespace Application.Bussiness
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<IEnumerable<Movie?>> GetAll()
        {
            return await _movieRepository.GetAllMovieAsync();
        }

        public async Task<Response<Guid>> Create(MovieDTO movieDTO)
        {
            Movie movie = new Movie(movieDTO);
            await _movieRepository.CreateMovieAsync(movie);
            return Response<Guid>.Success(movie.Id);
        }
    }
}
