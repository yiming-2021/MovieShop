import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CastDetailsComponent } from './cast-details/cast-details.component';
import { MoviesDetailsComponent } from './movies-details/movies-details.component';
import { MoviesComponent } from './movies.component';
import { TopratedComponent } from './toprated/toprated.component';

const routes: Routes = [

  // localhost:4200/movies
  {
    path: '', component: MoviesComponent,
    children: [
      {path: 'toprated', component: TopratedComponent},
      {path: ':id', component: MoviesDetailsComponent},
      {path: 'cast/:id', component: CastDetailsComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MoviesRoutingModule { }