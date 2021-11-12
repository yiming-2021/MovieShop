using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CastRepository : EfRepository<Cast>, ICastRepository
    {
        public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public new async Task<Cast> GetById(int id)
        {
            var cast = await _dbContext.Casts.Include(c => c.Movies).ThenInclude(c => c.Movie).FirstOrDefaultAsync(c => c.Id == id);
            if (cast == null)
            {
                throw new Exception($"No Cast Found for the id {id}");
            }

            return cast;

        }

    }
}