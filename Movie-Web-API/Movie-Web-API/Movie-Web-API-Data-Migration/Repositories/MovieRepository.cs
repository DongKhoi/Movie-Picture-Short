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

        public async Task<Movie?> GetMovieAsync()
        {
            var num = _dbContext.Movies.ToListAsync().Result.Count();
            Random rand = new Random();
            int toSkip = rand.Next(0, num);
            return await _dbContext.Movies.Skip(toSkip).Take(1).FirstOrDefaultAsync();
        }

        public async Task CreateMovieAsync(Movie movie)
        {
            _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddNumberLike(Guid id)
        {
            var movie = await _dbContext.Movies.Where(x => x.Id == id).SingleOrDefaultAsync();
            movie.AddLike();
            _dbContext.Movies.Update(movie);
            await _dbContext.SaveChangesAsync();
        }

        public async Task MinusNumberLike(Guid id)
        {
            var movie = await _dbContext.Movies.Where(x => x.Id == id).SingleOrDefaultAsync();
            movie.MinusLike();
            _dbContext.Movies.Update(movie);
            await _dbContext.SaveChangesAsync();
        }
    }
}
