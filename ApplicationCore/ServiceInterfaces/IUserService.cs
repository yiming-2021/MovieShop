using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.Entities;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<int> RegisterUser(UserRegisterRequestModel requestModel);
        Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel requestModel);


        Task<PurchaseResponseModel> PurchaseMovie(PurchaseRequestModel model);
        // Task<FavoriteResponseModel> FavoriteMovie(FavoriteRequestModel model);
        Task<List<MovieCardResponseModel>> GetPurchaseMovies(int userId);
        Task<IEnumerable<MovieCardResponseModel>> GetFavoriteMovies(int userId);

        // Task<List<MovieCardResponseModel>> GetPurchasedMoviesByUser(int userId);

        Task<UserLoginResponseModel> ValidateUser(string email, string password);
    }
}
