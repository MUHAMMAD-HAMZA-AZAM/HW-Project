import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RatingModule } from 'ng-starrating';
import { RouterModule, Routes } from '@angular/router';
import { GetFinishedJobsComponent } from './GetFinishedJobs/GetFinishedJobs.component';
import { GetFinishedJobDetailComponent } from './GetFinishedJobDetail/getFinishedJobDetail.component';
import { AgmCoreModule } from '@agm/core';
import { ReceivedBidDetailComponent } from './receivedBidDetail/receivedBidDetail.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { googleApiKey } from '../../shared/Enums/enums';
const routes: Routes = [
  { path: 'GetFinishedJobs', component: GetFinishedJobsComponent, },
  { path: 'GetFinishedJobDetail', component: GetFinishedJobDetailComponent, },
  { path: 'ReceivedBidDetail', component: ReceivedBidDetailComponent, },
]



@NgModule({
  declarations: [
    GetFinishedJobsComponent,
    GetFinishedJobDetailComponent,
    ReceivedBidDetailComponent,
  ],
  imports: [
    AgmCoreModule.forRoot({
      apiKey: googleApiKey.mapApiKey,
      libraries: ['places', 'geometry']
    }),
    CommonModule,
    RatingModule ,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ModalModule.forRoot(),
    RouterModule.forChild(routes)
  ],

  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
}) export class JobDetailModule { }
