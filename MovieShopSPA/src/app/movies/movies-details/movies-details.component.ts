import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { MovieService } from 'src/app/core/services/movie.service';
import { Movie } from 'src/app/shared/models/movie';

@Component({
  selector: 'app-movies-details',
  templateUrl: './movies-details.component.html',
  styleUrls: ['./movies-details.component.css']
})
export class MoviesDetailsComponent implements OnInit {

  movie!: Movie;
  id: number = 0;
  isLoggedIn = false;
  currentMoviePurchased = false;

  constructor(private activeRoute: ActivatedRoute, private movieService: MovieService, private authService: AuthenticationService) { }

  ngOnInit(): void {
    this.authService.isLoggedIn.subscribe(isLoggedIn => {
      this.isLoggedIn = isLoggedIn;
    });

    // ActivatedRoute => that will give us all the information of the current route/url
    // get the id from the URL 1, 2
    // then call our api, getMovieDetails method
    this.activeRoute.paramMap.subscribe
      (
        p => {
          this.id = Number(p.get('id'));
          console.log('movieId= ' + this.id);
          // call the api
          this.movieService.getMovieDetails(this.id)
            .subscribe(
              m => {
                this.movie = m;
                console.log(this.movie);
              }
            );
        }
      )

      
    console.log('inside movie details component');
  }

}