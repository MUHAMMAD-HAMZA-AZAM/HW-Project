import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { RatingModule } from 'ng-starrating';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AgmCoreModule } from '@agm/core';
import { RevieweComponent } from './reviewe/reviewe.component';
import { JobDetailComponent } from './detail/detail.component';
import { JobListComponent } from './list/list.component';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';

const routes: Routes = [
  { path: 'List', component: JobListComponent, },
  { path: 'Detail', component: JobDetailComponent },
  { path: 'Review', component: RevieweComponent },
]



@NgModule({
  declarations: [
    JobListComponent,
    JobDetailComponent,
    RevieweComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    InfiniteScrollModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    RatingModule,
    ModalModule.forRoot(),
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyBh_wL0Jb7M1m4xBLNWmOWkVPbE5vpBHck&callback=initMap'
    })
  ]
}) export class JobModule { }
