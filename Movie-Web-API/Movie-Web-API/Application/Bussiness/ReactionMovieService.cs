
using Application.Interfaces;
using Application.IRepositories;
using Domain.Common;
using Domain.DTOs;
using Domain.Entities;

namespace Application.Bussiness
{
    public class ReactionMovieService : IReactionMovieService
    {
        private readonly IReactionMovieRepository _reactionRepository;
        private readonly IMovieRepository _movieRepository;

        public ReactionMovieService(IReactionMovieRepository reactionRepository, IMovieRepository movieRepository)
        {
            _reactionRepository = reactionRepository;
            _movieRepository = movieRepository;
        }
        public async Task<ReactionMovie> GetDetail(ReactionMovieDTO reactionDTO)
        {
            return await _reactionRepository.GetReactionMovieAsync(reactionDTO.MovieId, reactionDTO.UserId);
        }

        public async Task<Response<Guid>> Create(ReactionMovieDTO movieDTO)
        {
            ReactionMovie reaction = new ReactionMovie(movieDTO);
            await _reactionRepository.CreateReactionMovieAsync(reaction);
            await _movieRepository.AddNumberLike(movieDTO.MovieId);
            return Response<Guid>.Success(reaction.MovieId);
        }

        public async Task<Response<Guid>> Remove(ReactionMovieDTO movieDTO)
        {
            ReactionMovie reaction = new ReactionMovie(movieDTO);
            await _reactionRepository.RemoveReactionMovieAsync(reaction);
            await _movieRepository.MinusNumberLike(movieDTO.MovieId);
            return Response<Guid>.Success(reaction.MovieId);
        }
    }
}
