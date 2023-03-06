using Domain.Common;
using Domain.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IReactionMovieService
    {
        Task<ReactionMovie> GetDetail(ReactionMovieDTO reactionDTO);
        Task<Response<Guid>> Create(ReactionMovieDTO movieDTO);
        Task<Response<Guid>> Remove(ReactionMovieDTO movieDTO);

    }
}
