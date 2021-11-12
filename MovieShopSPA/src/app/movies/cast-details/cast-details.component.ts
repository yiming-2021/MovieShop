import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MovieService } from 'src/app/core/services/movie.service';
import { Cast } from 'src/app/shared/models/Cast';

@Component({
  selector: 'app-cast-details',
  templateUrl: './cast-details.component.html',
  styleUrls: ['./cast-details.component.css']
})
export class CastDetailsComponent implements OnInit {

  cast!: Cast;
  id: number = 0;

  constructor(private activeRoute: ActivatedRoute, private movieService: MovieService) { }

  ngOnInit(): void {
    // ActivatedRoute => that will give us all the information of the current route/url
    // get the id from the URL 1, 2
    // then call our api, getMovieDetails method
    this.activeRoute.paramMap.subscribe
      (
        p => {
          this.id = Number(p.get('id'));
          console.log('castId= ' + this.id);
          // call the api
          this.movieService.getCastDetails(this.id)
            .subscribe(
              c => {
                this.cast = c;
                console.log(this.cast);
              }
            );
        }
      )
    console.log('inside movie details component');
  }

}
