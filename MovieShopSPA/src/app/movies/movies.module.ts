import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MoviesRoutingModule } from './movies-routing.module';
import { MoviesDetailsComponent } from './movies-details/movies-details.component';
import { CastDetailsComponent } from './cast-details/cast-details.component';
import { TopratedComponent } from './toprated/toprated.component';
import { MoviesComponent } from './movies.component';
import { SharedModule } from '../shared/shared.module';
import { MovieCardComponent } from '../shared/components/movie-card/movie-card.component';


@NgModule({
  declarations: [
    MoviesDetailsComponent,
    CastDetailsComponent,
    TopratedComponent,
    MoviesComponent,
  ],
  imports: [
    CommonModule,
    MoviesRoutingModule,
    SharedModule
  ]
})
export class MoviesModule { }
