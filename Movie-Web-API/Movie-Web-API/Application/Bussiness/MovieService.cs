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
        public async Task<Movie?> GetMovie()
        {
            return await _movieRepository.GetMovieAsync();
        }

        public async Task<Movie?> GetDetailMovie(Guid id)
        {
            return await _movieRepository.GetDetailMovieAsync(id);
        }

        public async Task<Response<Guid>> Create(MovieDTO movieDTO)
        {
            Movie movie = new Movie(movieDTO);
            await _movieRepository.CreateMovieAsync(movie);
            return Response<Guid>.Success(movie.Id);
        }
    }
}
