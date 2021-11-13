import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user.component';
import { PurchasesComponent } from './purchases/purchases.component';
import { FavoritesComponent } from './favorites/favorites.component';
import { EditUserComponent } from './edit-user/edit-user.component';
import { ReviewsComponent } from './reviews/reviews.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    UserComponent,
    PurchasesComponent,
    FavoritesComponent,
    EditUserComponent,
    ReviewsComponent,
    UserDetailsComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    SharedModule
  ]
})
export class UserModule { }
