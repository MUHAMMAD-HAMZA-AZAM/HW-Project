import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { ListComponent } from './list/list.component';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
//import { DetailComponent } from './detail/detail.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AgmCoreModule } from '@agm/core';
import { LeadsDetailComponent } from './detail/detail.component';
import { MakeAndEditBidComponent } from './makeBid/makeAndEditBid.component';




const routes: Routes = [
  { path: 'list', component: ListComponent, },
  //{ path: 'BlogOneDetail', component: BlogDetailsComponent, },
  //{ path: 'BlogTwoDetail', component: BlogDetails1Component, },
  //{ path: 'BlogThreeDetail', component: BlogDetails2Component, },
  //{ path: 'BlogThreeDetail', component: BlogDetails2Component, },
  { path: 'MakeBid', component: MakeAndEditBidComponent, },
  { path: 'Detail', component: LeadsDetailComponent },
]



@NgModule({
  declarations: [
    ListComponent,
    //BlogsComponent,
    //BlogDetailsComponent,
    //BlogDetails1Component,
    //BlogDetails2Component,
    MakeAndEditBidComponent,
    LeadsDetailComponent,
   // DetailComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    InfiniteScrollModule,
    ModalModule.forRoot(),
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyBXL-PpLFMRj9h1GhosFJ3eWCwL3r5MvGI&callback=initMap'
    })


  ]
})
export class LiveLeadsModule { }
