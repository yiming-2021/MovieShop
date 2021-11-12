import { Component, OnInit } from '@angular/core';
import { MovieService } from "/Users/lbq/RiderProjects/MovieShop 2/MovieShopSPA/src/app/core/services/movie.service";
import { MovieCard } from '/Users/lbq/RiderProjects/MovieShop 2/MovieShopSPA/src/app/shared/models/moviecard';

@Component({
  selector: 'app-toprated',
  templateUrl: './toprated.component.html',
  styleUrls: ['./toprated.component.css']
})
export class TopratedComponent implements OnInit {

  mypageTitle = "Movie Shop SPA Top Rated 30 Movies";

  // moviecards to display
  movieCards!: MovieCard[];

  constructor(private movieService: MovieService) { }

  ngOnInit(): void {
    // ngOnInit is one of the most important life cycle hooks method in angular
    // It is recommended to use this method to call the API and initilize any data properties
    // Will be called automatically by your angular component after calling constructor
    // only when u subscribe to the observable you get the data
    // Observable<MovieCard[]>

    this.movieService.getTopRatedMovies().subscribe(
      m => {
        this.movieCards = m;
        console.log('inside the ngOnInit method of Home Component');
                
        // to print an array of items in console window 
        // console.log(this.movieCards);
        console.table(this.movieCards);
        
      }
    );
  }

}