using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IPurchaseRepository: IAsyncRepository<Purchase>
    {
        // Task<List<Purchase>> GetAllPurchasesForUser(int userId, int pageSize = 30, int pageIndex = 1);
        Task<Purchase> GetPurchaseByUserMovieId(int userId, int movieId);

    }
}
