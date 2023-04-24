import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TradesmanlistComponent } from './tradesmanlist/tradesmanlist.component';
import { LandingPageComponent } from './landing-page.component';
import { apiRoute } from '../shared/ApiRoutes/ApiRoutes';
import { TradesmanProfileComponent } from './tradesman-profile/tradesman-profile.component';
import { RatingModule } from 'ng-starrating';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AutocompleteLibModule } from 'angular-ng-autocomplete';
import { CostGuideComponent } from './cost-guide/cost-guide.component';
import { IconSpriteModule } from 'ng-svg-icon-sprite';

const routes: Routes = [
  
  //{ path: 'tradesman-profile/:id', component: TradesmanProfileComponent, },
  { path: 'tradesman-profile', component: TradesmanProfileComponent, },
  { path: 'searchtradesmanbyskill', component: TradesmanlistComponent, },
  { path: 'costGuides/:id/:slug/:nf/:name', component: CostGuideComponent }
]

@NgModule({
  declarations: [TradesmanlistComponent, TradesmanProfileComponent, CostGuideComponent],

  imports: [
    //BrowserModule,
    CommonModule,
    NgbModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RatingModule,
    AutocompleteLibModule,
    RouterModule.forChild(routes),
    IconSpriteModule.forRoot({ path: 'assets/sprites/sprite.svg' }),
  ]
})
export class LandingPageModule { }
