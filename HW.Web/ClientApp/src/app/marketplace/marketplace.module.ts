import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MarketPlaceIndexComponent } from './market-place-index/market-place-index.component';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductCategoryHomeComponent } from './product-category-home/product-category-home.component';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { AgmCoreModule } from '@agm/core';
import { RatingModule } from 'ng-starrating';

const routes: Routes = [
  { path: 'MarketPlaceIndex', component: MarketPlaceIndexComponent },
  { path: 'MarketPlaceIndex/ProductCategoryHome/:id', component: ProductCategoryHomeComponent },

  
]



@NgModule({
  declarations: [
    MarketPlaceIndexComponent,
    ProductCategoryHomeComponent
  ],
  imports: [
    Ng2SearchPipeModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RatingModule,
    RouterModule.forChild(routes),
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyBXL-PpLFMRj9h1GhosFJ3eWCwL3r5MvGI&callback=initMap'
    })
    ,
    NgMultiSelectDropDownModule
  ]
})
export class MarketplaceModule { }
