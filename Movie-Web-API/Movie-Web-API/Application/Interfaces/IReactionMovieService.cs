using Domain.Common;
using Domain.DTOs;

namespace Application.Interfaces
{
    public interface IReactionMovieService
    {
        Task<int> GetDetail(Guid id);
        Task<Response<Guid>> Create(ReactionMovieDTO movieDTO);
    }
}
