 import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DefaultComponent } from './default/default.component';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { SupplierAdDetailComponent } from './supplier-ad-detail/supplier-ad-detail.component';
import { SupplierShopComponent } from './supplier-shop/supplier-shop.component';
import { AgmCoreModule } from '@agm/core';
import { RatingModule } from 'ng-starrating';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


const routes: Routes = [
  { path: 'MarketPlaceIndex/SupplierAdDetail/:id', component: SupplierAdDetailComponent },
  { path: 'MarketPlaceIndex/SupplierShop/:id', component: SupplierShopComponent },
  { path: 'Default', component: DefaultComponent },
  { path: 'Default', component: DefaultComponent },

  {
    path: 'Profile',
    loadChildren: () => import('./profile/profile.module').then(m => m.ProfileModule)
  },
  {
    path: 'FinishedJobs',
    loadChildren: () => import('./finishedJobs/finishedJobs.module').then(m => m.FinishedJobsModule)
  },
  {
    path: 'MyBids',
    loadChildren: () => import('./mybids/mybids.module').then(m => m.MybidsModule)
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

  //{
  //  path: 'Profile',
  //  loadChildren: () => import('./profile/profile.module').then(m => m.ProfileModule)
  //},
  {
    path: 'JobQuotes',
    loadChildren: () => import('./quotes/quotes.module').then(m => m.QuotesModule)
  },
  //{
  //  path: 'Profile',
  //  loadChildren: () => import('./profile/profile.module').then(m => m.ProfileModule)
  //},
  {
    path: 'Bids',
    loadChildren: () => import('./bids/bids.module').then(m => m.BidsModule)
  }
  //{
  //  path: 'Home',
  //  loadChildren: () => import('./home/home.module').then(m => m.HomeModule)
  //},
  
]



@NgModule({
  declarations: [
    SupplierAdDetailComponent,
    SupplierShopComponent,
    DefaultComponent,

  ],
  imports: [
    NgbModule,
    Ng2SearchPipeModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RatingModule,
    CarouselModule,
    RouterModule.forChild(routes),
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyBh_wL0Jb7M1m4xBLNWmOWkVPbE5vpBHck'
    })
    ,
    NgMultiSelectDropDownModule
  ]
})
export class UserModule { }
