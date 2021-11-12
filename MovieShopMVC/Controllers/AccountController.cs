using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        // will execute when user clicks on Register button in the view
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel requestModel)
        {
            // check if the model is valid*
            if (!ModelState.IsValid)
            {
                return View();
            }

            // save the user registration information to the database
            // receive the model from view
            var newUser = await _userService.RegisterUser(requestModel);

            return RedirectToAction("Login");
        }


        [HttpGet]
        // use this action method to display empty view
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestModel requestModel)
        {
            var user = await _userService.LoginUser(requestModel);
            if (user == null)
            {
                // username/password is wrong
                // show message to user saying email/password is wrong

                return View();
            }

            // we create the cookie and store some information in the cookie and cookie will have expiration time
            // We need to tell the ASP.NET Application that we are gonna use Cookie Based Authentication and we can specify
            // the details of the cookie like name, how long the cookie is valid, where to re-direct when cookie expired

            // Claims => 
            // Driving licence => Name, Daof, Expire, 
            // create all the necessary claims inside claims object
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email ),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToShortDateString()),
                new Claim("FullName", user.FirstName + " " + user.LastName)
            };

            // Identity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // print out our Card
            // creating the cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
            return LocalRedirect("~/");
            // logout=> 
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // invalidate the cookie and re-direct to Login
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
    
}
