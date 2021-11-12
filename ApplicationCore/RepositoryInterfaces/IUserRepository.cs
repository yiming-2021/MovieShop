using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        // Task<User> AddUser(User user);

        Task<User> GetUserPurchaseById(int id);
        Task<User> GetUserFavoriteById(int id);
        //Task<Purchase> AddPurchase(Purchase purchase);
        //Task<Favorite> AddFavorite(Favorite favorite);

    }
}
