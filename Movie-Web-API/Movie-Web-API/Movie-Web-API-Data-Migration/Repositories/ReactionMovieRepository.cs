
using Application.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Movie_Web_API_Data_Migration;

namespace Infrastructure.Repositories
{
    public class ReactionMovieRepository : IReactionMovieRepository
    {
        private readonly MovieWebApiDbContext _dbContext;
        public ReactionMovieRepository(MovieWebApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetReactionMovieAsync(Guid id)
        {
            return await _dbContext.ReactionMovies.Where(x => x.MovieId == id).CountAsync();
        }

        public async Task CreateReactionMovieAsync(ReactionMovie reactionMovie)
        {
            _dbContext.ReactionMovies.Add(reactionMovie);
            await _dbContext.SaveChangesAsync();
        }
    }
}
