import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
//import { MarketPlaceIndexComponent } from './market-place-index/market-place-index.component';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
//import { ProductCategoryHomeComponent } from './product-category-home/product-category-home.component';
import { DefaultComponent } from './default/default.component';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { SupplierAdDetailComponent } from './supplier-ad-detail/supplier-ad-detail.component';
import { SupplierShopComponent } from './supplier-shop/supplier-shop.component';
import { AgmCoreModule } from '@agm/core';
import { RatingModule } from 'ng-starrating';

const routes: Routes = [
  //{ path: 'MarketPlaceIndex', component: MarketPlaceIndexComponent },
  //{ path: 'MarketPlaceIndex/ProductCategoryHome/:id', component: ProductCategoryHomeComponent },
  { path: 'MarketPlaceIndex/SupplierAdDetail/:id', component: SupplierAdDetailComponent },
  { path: 'MarketPlaceIndex/SupplierShop/:id', component: SupplierShopComponent },
  { path: 'Default', component: DefaultComponent },



  {
    path: 'Profile',
    loadChildren: () => import('./profile/profile.module').then(m => m.ProfileModule)
  },
  {
    path: 'Job',
    loadChildren: () => import('./jobDetail/jobDetail.module').then(m => m.JobDetailModule)
  },
  {
    path: 'InProgressJob',
    loadChildren: () => import('./inProgressJob/inProgressJob.module').then(m => m.InProgressJobModule)
  },

  {
    path: 'Quote',
    loadChildren: () => import('./quote/quote.module').then(m => m.QuoteModule)
  },

  {
    path: 'Payments',
    loadChildren: () => import('./payments/payments.module').then(m => m.PaymentsModule)
  },

  {
    path: 'Notifications',
    loadChildren: () => import('./notifications/notifications.module').then(m => m.NotificationsModule)
  },

  //{ path: 'MarketPlaceIndex', component: MarketPlaceIndexComponent },
  //{ path: 'MarketPlaceIndex/ProductCategoryHome/:id', component: ProductCategoryHomeComponent },
  {
    path: 'Profile',
    loadChildren: () => import('./profile/profile.module').then(m => m.ProfileModule)
  },
  {
    path: 'JobQuotes',
    loadChildren: () => import('./quotes/quotes.module').then(m => m.QuotesModule)
  },
  {
    path: 'Profile',
    loadChildren: () => import('./profile/profile.module').then(m => m.ProfileModule)
  },
  {
    path: 'Bids',
    loadChildren: () => import('./bids/bids.module').then(m => m.BidsModule)
  },
  {
    path: 'Home',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule)
  },
  
]



@NgModule({
  declarations: [
    //MarketPlaceIndexComponent,
    //ProductCategoryHomeComponent,
    SupplierAdDetailComponent,
    SupplierShopComponent,
    DefaultComponent,
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
export class UserModule { }
