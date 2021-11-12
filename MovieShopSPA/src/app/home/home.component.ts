import { Component, OnInit } from '@angular/core';
import { MovieService } from '../core/services/movie.service';
import { MovieCard } from '../shared/models/moviecard';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  mypageTitle = "Movie Shop SPA";

  // moviecards to display
  movieCards!: MovieCard[];

  constructor(private movieService: MovieService) { }

  ngOnInit(): void {
    // ngOnInit is one of the most important life cycle hooks method in angular
    // It is recommended to use this method to call the API and initilize any data properties
    // Will be called automatically by your angular component after calling constructor
    // only when u subscribe to the observable you get the data
    // Observable<MovieCard[]>
    // http://localhost:4200/ => HomeComponent
    this.movieService.getTopRevenueMovies().subscribe(
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