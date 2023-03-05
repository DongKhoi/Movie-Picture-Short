
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
        public ReactionMovieService(IReactionMovieRepository reactionRepository)
        {
            _reactionRepository = reactionRepository;
        }
        public async Task<int> GetDetail(Guid id)
        {
            return await _reactionRepository.GetReactionMovieAsync(id);
        }

        public async Task<Response<Guid>> Create(ReactionMovieDTO movieDTO)
        {
            ReactionMovie reaction = new ReactionMovie(movieDTO);
            await _reactionRepository.CreateReactionMovieAsync(reaction);
            return Response<Guid>.Success(reaction.MovieId);
        }
    }
