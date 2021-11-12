using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MovieShopMVC.Controllers
{
    // all the action methods in User Controller should work only when user is Authenticated (login success)
    public class UserController : Controller
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserService _userService;
        private readonly IMovieService _movieService;
        
        public UserController(IUserService userService, ICurrentUserService currentUserService, IMovieService movieService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
            _movieService = movieService;
        }


        // [HttpPost]
        // public async Task<IActionResult> Purchase(PurchaseRequestModel requestModel)
        // {
        //     // purchase a movie when user clicks on Buy button on Movie Details Page
        //     var newPurchase = await _userService.PurchaseMovie(requestModel);
        //     return View();
        // }

        [HttpPost]
        public async Task<IActionResult> Favorite(FavoriteRequestModel requestModel)
        {
            // favorite a movie when user clicks on Favorite Button on Movie Details Page
            // var newFavorite = await _userService.FavoriteMovie(requestModel);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Review()
        {
            // add a new review done by the user for that movie
            return View();
        }

        [HttpGet]
        // Filters in ASP.NET
        [Authorize] // built-in filter

        public async Task<IActionResult> Purchases()
        {
            // get the id from HttpCOntext.User.Claims
            /*var userIdentity = this.User.Identity;
            if (userIdentity != null && userIdentity.IsAuthenticated)
            {
                // call the databsae to get the data
                return View();
            }
            

            RedirectToAction("Login", "Account");*/
            // get all the movies purchased by user => List<MovieCard> 

            // int userId = Convert.ToInt32((HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value));
            // call userService that will give list of moviesCard Models that this user purchased
            // Purchase, dbContext.Purchase.where(u=> u.UserId == id);

            var userId = _currentUserService.UserId;
            var movieCards = await _userService.GetPurchaseMovies(userId);

            return View(movieCards);

        }


        [HttpGet]
        public async Task<IActionResult> Favorites()
        {

            int userId = Convert.ToInt32((HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value));

            var movieCards = await _userService.GetFavoriteMovies(userId);

            return View(movieCards);
            // get all movies favorited by that user

        }

        public async Task<IActionResult> Reviews(int id)
        {
            // get all the reviews done by this user
            return View();
        }
        
        
        // call this method when user clicks on Buy Movie in Movie details page
        // Filters,
        [HttpGet]
        public async Task<IActionResult> BuyMovie(int id)
        {
            var movie = await _movieService.GetMovieById(id);
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> BuyMovie(PurchaseRequestModel purchase)
        {
            await _userService.PurchaseMovie(purchase);
            return LocalRedirect("~/User/Purchases");
        }

    }
}