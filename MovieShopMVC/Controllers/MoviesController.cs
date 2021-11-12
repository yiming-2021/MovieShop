﻿using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // localhost:24234/movies/details/343
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var moviedetails = await _movieService.GetMovieDetails(id);
            return View(moviedetails);
        }
    }
}