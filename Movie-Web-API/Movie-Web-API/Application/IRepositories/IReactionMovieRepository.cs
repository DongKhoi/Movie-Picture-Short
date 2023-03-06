using Domain.Entities;

namespace Application.IRepositories
{
    public interface IReactionMovieRepository
    {
        Task<ReactionMovie> GetReactionMovieAsync(Guid id, Guid UserId);
        Task CreateReactionMovieAsync(ReactionMovie reactionMovie);
        Task RemoveReactionMovieAsync(ReactionMovie reactionMovie);

    }
}
