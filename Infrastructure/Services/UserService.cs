using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using ApplicationCore.Entities;



namespace Infrastructure.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPurchaseRepository _purchaseRepository;

        public UserService(IUserRepository userRepository, IPurchaseRepository purchaseRepository)
        {
            _userRepository = userRepository;
            _purchaseRepository = purchaseRepository;
        }


        private string GetSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }


        private string GetHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

        public async Task<int> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // check whether email exists in the database
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser != null)
                //email exists in the database
                throw new Exception("Email already exists, please login");

            // generate a random unique salt
            var salt = GetSalt();

            // create the hashed password with salt generated in the above step
            var hashedPassword = GetHashedPassword(requestModel.Password, salt);

            // save the user object to db
            // create user entity object
            var user = new User
            {
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                Email = requestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                DateOfBirth = requestModel.DateOfBirth
            };

            // use EF to save this user in the user table
            var newUser = await _userRepository.Add(user);
            return newUser.Id;
        }

        public async Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel requestModel)
        {
            // get the salt and hashedpassword from databse for this user
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser == null) throw null;

            // hash the user entered password with salt from the database

            var hashedPassword = GetHashedPassword(requestModel.Password, dbUser.Salt);
            // check the hashedpassword with database hashed password
            if (hashedPassword == dbUser.HashedPassword)
            {
                // user entered correct password
                var userLoginResponseModel = new UserLoginResponseModel
                {
                    Id = dbUser.Id,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName,
                    DateOfBirth = dbUser.DateOfBirth,//.GetValueOrDefault(),
                    Email = dbUser.Email
                };
                return userLoginResponseModel;
            }
            return null;
        }

        public async Task<UserLoginResponseModel> ValidateUser(string email, string password)
        {
            // get the user info from database by email
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
                // we dont have the email in the database
                return null;

            // we need to hash the user entered password along with salt from database.
            var hashedPassword = GetHashedPassword(password, user.Salt);

            if (hashedPassword != user.HashedPassword) return null;
            // user entered correct password
            var userLoginResponseModel = new UserLoginResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth
            };
            return userLoginResponseModel;

        }
        
        public async Task<PurchaseResponseModel> PurchaseMovie(PurchaseRequestModel model)
        {
            // check if the movie already existes
            var dbPurchase = await _purchaseRepository.GetPurchaseByUserMovieId(model.UserId, model.MovieId);
            if (dbPurchase != null)
            {
                throw new Exception("Movie already purchased.");
            }

            var purchase = new Purchase
            {
                MovieId = model.MovieId,
                PurchaseDateTime = DateTime.Now,
                PurchaseNumber = Guid.NewGuid(),
                TotalPrice = model.TotalPrice,
                UserId = model.UserId
            };


            var newPurchase = await _purchaseRepository.Add(purchase);



            var newPurchaseResponse = new PurchaseResponseModel
            {
                Id = newPurchase.Id,
                MovieId = newPurchase.MovieId,
                PurchaseDateTime = newPurchase.PurchaseDateTime,
                UserId = newPurchase.UserId,
                PurchaseNumber = newPurchase.PurchaseNumber,
                TotalPrice = newPurchase.TotalPrice
            };
            return newPurchaseResponse;
        }


        // public async Task<FavoriteResponseModel> FavoriteMovie(FavoriteRequestModel model)
        // {
        //     //for movies already favorited, throw message (do later
        //     var favorite = new Favorite
        //     {
        //         MovieId = model.MovieId,
        //         UserId = model.UserId
        //     };
        //
        //     var newFavorite = await _userRepository.AddFavorite(favorite);
        //     var newFavoriteResponse = new FavoriteResponseModel
        //     {
        //         Id = newFavorite.Id,
        //         MovieId = newFavorite.MovieId,
        //         UserId = newFavorite.UserId,
        //     };
        //
        //     return newFavoriteResponse;
        // }

        public async Task<List<MovieCardResponseModel>> GetPurchaseMovies(int userId)
        {
            var user = await _userRepository.GetUserPurchaseById(userId);
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in user.Purchases)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.MovieId,
                    Title = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl
                });
            }
            return movieCards;
        }

        // public async Task<List<MovieCardResponseModel>> GetPurchasedMoviesByUser(int userId)
        // {
        //     var purchases = await _purchaseRepository.GetAllPurchasesForUser(userId);
        //     var movieCards = new List<MovieCardResponseModel>();
        //     foreach (var purchase in purchases)
        //     {
        //         movieCards.Add(new MovieCardResponseModel
        //         {
        //             Id = purchase.MovieId,
        //             Title = purchase.Movie.Title,
        //             PosterUrl = purchase.Movie.PosterUrl
        //         });
        //     }
        //     if (movieCards == null)
        //     {
        //         throw new Exception($"No Purchase Found for the id {userId}");
        //     }
        //
        //     return movieCards;
        // }


        public async Task<IEnumerable<MovieCardResponseModel>> GetFavoriteMovies(int userId)
        {
            var user = await _userRepository.GetUserFavoriteById(userId);
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in user.Favorites)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.MovieId,
                    Title = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl
                });
            }
            return movieCards;
        }

    }
}
