using Domain.Entities;
using Application.IRepositories;
using Microsoft.EntityFrameworkCore;
using Movie_Web_API_Data_Migration;

namespace Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieWebApiDbContext _dbContext;
        public MovieRepository(MovieWebApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Movie?> GetDetailMovieAsync(Guid id)
        {
            return await _dbContext.Movies.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Movie?>> GetAllMovieAsync()
        {
            return await _dbContext.Movies.ToListAsync();
        }

        public async Task CreateMovieAsync(Movie movie)
        {
            _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync();
        }
    }
}
