using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository: IAsyncRepository<Movie>
    {
        // method that get 30 top
        Task<IEnumerable<Movie>> GetTop30RevenueMovies();
        Task<IEnumerable<Movie>> GetTop30RatedMovies();
        Task<Movie> GetMovieById(int id);


    }
}
