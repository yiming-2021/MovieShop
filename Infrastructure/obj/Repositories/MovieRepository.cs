using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {

        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Movie>> GetTop30RevenueMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }
        
        public async Task<IEnumerable<Movie>> GetTop30RatedMovies()
        {
            var movies = await _dbContext.Reviews.Include(r => r.Movie)
                .GroupBy(r => new { r.Movie.Id, r.Movie.PosterUrl, r.Movie.Title })
                .OrderByDescending(g => g.Average(r => r.Rating))
                .Select(m => new Movie { Id = m.Key.Id, PosterUrl = m.Key.PosterUrl, Title = m.Key.Title })
                .Take(30).ToListAsync();
            return movies;
        }

        
        public async Task<Movie> GetMovieById(int id)
        {
            var movie = await _dbContext.Movies.Include(m => m.Casts).ThenInclude(m => m.Cast)
                .Include(m => m.Genres).ThenInclude(m => m.Genre).Include(m => m.Trailers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null) throw new Exception("Movie Not found");

            var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty()
                .AverageAsync(r => r == null ? 0 : r.Rating);
            if (movieRating > 0) movie.Rating = movieRating;

            return movie;
        }
    }
}
