using System;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.RepositoryInterfaces;


namespace Infrastructure.Services
{
	//public class MovieService: IMovieService
	//{
	//	private readonly IMovieRepository _movieRepository;

	//public MovieService


	//public List<MovieCardResponseModel> GetTop30RevenueMovies()
	//{// method should call movie repository and get ther data from movie table
	// //	var movieCards = new List<MovieCardResponseModel>
	// //{ new MovieCardResponseModel { Id=1, Title = "Inception", PosterUrl="https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg" },
	// //			new MovieCardResponseModel{Id = 3, Title="The Dark Knight", PosterUrl="https://image.tmdb.org/t/p/w342//qJ2tW6WMUDux911r6m7haRef0WH.jpg" },
	// //			new MovieCardResponseModel { Id =2, Title="Interstellar", PosterUrl ="https://image.tmdb.org/t/p/w342//gEU2QniE6E77NI6lCU6MxlNBvIx.jpg"}
	// //		};
	// //	return movieCards;

	//	// calling MovieRepository with DI based on IMovieRepository
	//	var movies = _movieRepository.GetTop30RevenueMovies();

	//	var movieCards = new List<MovieCardResponseModel>();
	//          foreach (var movie in movies)
	//          {
	//		movieCards.Add(new MovieCardResponseModel
	//		{
	//			Id = movie.Id,
	//			PosterUrl = movie.PosterUrl,
	//			Title = movie.Title

	//		}) ; ;
	//          }

	//	return movieCards;
	//}


	public class MovieService : IMovieService
	{
		private readonly IMovieRepository _movieRepository;

		public MovieService(IMovieRepository movieRepository)
		{
			_movieRepository = movieRepository;
		}
		public async Task<List<MovieCardResponseModel>> GetTop30RevenueMovies()
		{
			// calling MovieRepository with DI based on IMovieRepository
			// I/O bound operation
			var movies = await _movieRepository.GetTop30RevenueMovies();

			var movieCards = new List<MovieCardResponseModel>();
			foreach (var movie in movies)
			{
				movieCards.Add(new MovieCardResponseModel
				{
					Id = movie.Id,
					PosterUrl = movie.PosterUrl,
					Title = movie.Title
				});
			}

			return movieCards;
		}

		public async Task<List<MovieCardResponseModel>> GetTop30RatedMovies()
		{
			var movies = await _movieRepository.GetTop30RatedMovies();

			var movieCards = new List<MovieCardResponseModel>();
			foreach (var movie in movies)
			{
				movieCards.Add(new MovieCardResponseModel
				{
					Id = movie.Id,
					PosterUrl = movie.PosterUrl,
					Title = movie.Title
				});
			}

			return movieCards;
		}
		
		public async Task<MovieDetailsResponseModel> GetMovieDetails(int id) 
		{
			var movie = await _movieRepository.GetMovieById(id);
			if(movie == null)
            {
				throw new Exception($"No Movie Found for this {id}");

            }

			var movieDetails = new MovieDetailsResponseModel
			{
				Id = movie.Id,
				Budget = movie.Budget,
				Overview = movie.Overview,
				Price = movie.Price,
				PosterUrl = movie.PosterUrl,
				Revenue = movie.Revenue,
				ReleaseDate = movie.ReleaseDate.GetValueOrDefault(),
				Rating = movie.Rating,
				Tagline = movie.Tagline,
				Title = movie.Title,
				RunTime = movie.RunTime,
				BackdropUrl = movie.BackdropUrl,
				ImdbUrl = movie.ImdbUrl,
				TmdbUrl = movie.TmdbUrl
			};
            foreach (var genre in movie.Genres)
            {
				movieDetails.Genres.Add(
					new GenreModel 
					{ 
						Id = genre.GenreId, 
						Name = genre.Genre.Name 
					});
            }

            foreach (var cast in  movie.Casts)
            {
				movieDetails.Casts.Add(
					new CastResponseModel
					{
						Id = cast.CastId,
						Character = cast.Character,
						Name = cast.Cast.Name,
						ProfilePath = cast.Cast.ProfilePath
					});
            }

			return movieDetails;
		}

		public async Task<List<MovieCardResponseModel>> GetAllMovies()
		{
			var movies = await _movieRepository.GetAll();
			var movieCards = new List<MovieCardResponseModel>();
			foreach (var movie in movies)
			{
				movieCards.Add(new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title });
			}
			return movieCards;
		}
		
		public async Task<MovieDetailsResponseModel> GetMovieById(int id)
		{
			var movieDetails = new MovieDetailsResponseModel();
			var movie = await _movieRepository.GetById(id);

			// map movie entity to MovieDetailsResponseModel
			movieDetails.Id = movie.Id;
			movieDetails.PosterUrl = movie.PosterUrl;
			movieDetails.Title = movie.Title;
			movieDetails.Overview = movie.Overview;
			movieDetails.Tagline = movie.Tagline;
			movieDetails.Budget = movie.Budget;
			movieDetails.Revenue = movie.Revenue;
			movieDetails.ImdbUrl = movie.ImdbUrl;
			movieDetails.TmdbUrl = movie.TmdbUrl;
			movieDetails.BackdropUrl = movie.BackdropUrl;
			movieDetails.ReleaseDate = movie.ReleaseDate;
			movieDetails.RunTime = movie.RunTime;
			movieDetails.Price = movie.Price;

			movieDetails.Genres = new List<GenreModel>();
			movieDetails.Casts = new List<CastResponseModel>();

			// foreach (var genre in movie.Genres)
			// {
			// 	movieDetails.Genres.Add(new GenreModel { Id = genre.Id, Name = genre.Name });
			// }
			//
			// foreach (var cast in movie.MovieCasts)
			// {
			// 	movieDetails.Casts.Add(new CastResponseModel
			// 	{
			// 		Id = cast.CastId,
			// 		Character = cast.Character,
			// 		Name = cast.Cast.Name,
			// 		ProfilePath = cast.Cast.ProfilePath
			// 	});
			// }

			return movieDetails;
		}
	}
}



