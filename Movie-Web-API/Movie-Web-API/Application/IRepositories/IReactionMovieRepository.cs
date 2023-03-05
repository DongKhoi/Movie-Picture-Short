using Domain.Entities;

namespace Application.IRepositories
{
    public interface IReactionMovieRepository
    {
        Task<int> GetReactionMovieAsync(Guid id);
        Task CreateReactionMovieAsync(ReactionMovie reactionMovie);
    }
}
