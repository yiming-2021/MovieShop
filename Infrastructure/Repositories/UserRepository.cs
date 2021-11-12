using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.Extensions.Caching.Memory;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    
    public class UserRepository : EfRepository<User>, IUserRepository
    {

        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }


        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        //public async Task<User> AddUser(User user)
        //{
        //    await _dbContext.Users.AddAsync(user);
        //    await _dbContext.SaveChangesAsync();
        //    return user;
        //}

        //public async Task<Purchase> AddPurchase(Purchase purchase)
        //{
        //    await _dbContext.Purchases.AddAsync(purchase);
        //    await _dbContext.SaveChangesAsync();
        //    return purchase;
        //}

        //public async Task<Favorite> AddFavorite(Favorite favorite)
        //{
        //    await _dbContext.Favorites.AddAsync(favorite);
        //    await _dbContext.SaveChangesAsync();
        //    return favorite;
        //}


        public async Task<User> GetUserPurchaseById(int id)
        {
            var user = await _dbContext.Users.Include(u => u.Purchases).ThenInclude(u => u.Movie).
                FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<User> GetUserFavoriteById(int id)
        {
            var user = await _dbContext.Users.Include(u => u.Favorites).ThenInclude(u => u.Movie).
                FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }
    }
}