import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { BlogsComponent } from './blog/blogs/blogs.component';
import { BlogDetailsComponent } from './blog/blogDetails/blogDetails.component';
import { BlogDetails1Component } from './blog/blogDetails1/blogDetails1.component';
import { BlogDetails2Component } from './blog/blogDetails2/blogDetails2.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AgmCoreModule } from '@agm/core';
import { LeadsDetailComponent } from './detail/detail.component';
import { MakeAndEditBidComponent } from './makeBid/makeAndEditBid.component';
import { LeadsListComponent } from './list/list.component';
import { BidAudioComponent } from './audio/audio.component';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AudioRecordingService } from '../../shared/CommonServices/audioRecording.service';
const routes: Routes = [
  { path: 'list', component: LeadsListComponent, },
  { path: 'BlogOneDetail', component: BlogDetailsComponent, },
  { path: 'BlogTwoDetail', component: BlogDetails1Component, },
  { path: 'BlogThreeDetail', component: BlogDetails2Component, },
  { path: 'BlogThreeDetail', component: BlogDetails2Component, },
  { path: 'MakeBid', component: MakeAndEditBidComponent, },
  { path: 'Details', component: LeadsDetailComponent },
  { path: 'Audio', component: BidAudioComponent },
]



@NgModule({
  declarations: [
    LeadsListComponent,
    BlogsComponent,
    BlogDetailsComponent,
    BlogDetails1Component,
    BlogDetails2Component,
    MakeAndEditBidComponent,
    LeadsDetailComponent,
    BidAudioComponent,
  ],
  imports: [
    NgbModule,
    CommonModule,
    HttpClientModule,

    FormsModule,
    InfiniteScrollModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    ModalModule.forRoot(),
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyBh_wL0Jb7M1m4xBLNWmOWkVPbE5vpBHck&callback=initMap'
    })
  ],
  providers: [
    AudioRecordingService
  ],

})
export class LiveLeadsModule { }
