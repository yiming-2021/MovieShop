using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PurchaseRepository : EfRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        // public async Task<List<Purchase>> GetAllPurchasesForUser(int userId, int pageSize = 30,int pageIndex = 1)
        // {
        //     var purchase = await _dbContext.Purchases.Where(p => p.Id == userId)?.ToListAsync();
        //     if (purchase == null)
        //     {
        //         throw new Exception($"No Purchase Found for the id {userId}");
        //     }
        //
        //     return purchase;
        // }
        //
        
        public async Task<Purchase> GetPurchaseByUserMovieId(int userId, int movieId)
        {
            var purchase = await _dbContext.Purchases.FirstOrDefaultAsync(p => p.MovieId == movieId & p.UserId == userId);
            return purchase;
        }

        
    }
}
