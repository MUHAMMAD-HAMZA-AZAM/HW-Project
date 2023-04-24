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
import { AutocompleteLibModule } from 'angular-ng-autocomplete';
import { SupplierProductDetailsComponent } from './supplier-product-details/supplier-product-details.component';
import { SuppliershopComponent } from './suppliershop/suppliershop.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';



const routes: Routes = [
  
  { path: 'ProductCategoryHome', component: ProductCategoryHomeComponent },  
  { path: 'SupplierAdDetails/:id', component: SupplierProductDetailsComponent },
  { path: 'SupplierShop/:id', component: SuppliershopComponent },
  { path: '', component: MarketPlaceIndexComponent }, 
]

@NgModule({
  declarations: [
    MarketPlaceIndexComponent,
    ProductCategoryHomeComponent,
    SupplierProductDetailsComponent,
    SuppliershopComponent,
  ],
  imports: [
    Ng2SearchPipeModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
    NgMultiSelectDropDownModule,
    ReactiveFormsModule,
    RatingModule,
    AutocompleteLibModule,
    RouterModule.forChild(routes),
    NgbModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyBh_wL0Jb7M1m4xBLNWmOWkVPbE5vpBHck&callback=initMap'
    })
    ,
    NgMultiSelectDropDownModule   
  ]
})
export class MarketplaceModule { }
