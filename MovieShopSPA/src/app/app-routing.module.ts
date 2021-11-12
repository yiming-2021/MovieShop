import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { TopratedComponent } from './movies/toprated/toprated.component';
import { MoviesDetailsComponent } from './movies/movies-details/movies-details.component';
import { AuthGuard } from './core/guards/auth.guard';


// specify all the routes required by the angular application
const routes: Routes = [
  // path/route for my home page http://localhost:4200/
  {path:"", component: HomeComponent},

  // lazily load the modules, define main route for lazy modules
  {
    path: "movies", 
    loadChildren: () => 
    import("./movies/movies.module").then(mod => mod.MoviesModule)
  },
  // {path:"movies/:id", component: MoviesDetailsComponent},
  // {path:"movies/toprated", component: TopratedComponent}
  // {path:"admin/createmovie", component:CreateMovieComponent}

  {
    path: "account", 
    loadChildren: () => 
    import("./account/account.module").then(mod => mod.AccountModule)
  },
  {
    path: "user", 
    loadChildren: () => 
    import("./user/user.module").then(mod => mod.UserModule), canLoad:[AuthGuard]
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
