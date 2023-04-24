import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PromotadComponent } from './promotad/promotad.component';
import { AdsComponent } from './ads/ads.component';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';

const routes: Routes = [
  { path: 'Ads', component: AdsComponent },
  { path: 'PromoteAd', component: PromotadComponent },
]


@NgModule({
  declarations: [
    AdsComponent,
    PromotadComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    InfiniteScrollModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes)
  ]
})
export class ManageAdsModule { }
