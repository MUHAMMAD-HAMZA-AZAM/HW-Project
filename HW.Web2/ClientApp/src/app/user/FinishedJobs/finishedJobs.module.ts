import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RatingModule } from 'ng-starrating';
import { RouterModule, Routes } from '@angular/router';
import { AgmCoreModule } from '@agm/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { googleApiKey } from '../../shared/Enums/enums';
import { FinishedJobDetailComponent } from './detail/detail.component';
import { FinishedJobListComponent } from './list/list.component';
import { ReceivedBidDetailComponent } from './receivedBidDetail/receivedBidDetail.component';
const routes: Routes = [
  { path: 'List', component: FinishedJobListComponent, },
  { path: 'Detail', component: FinishedJobDetailComponent, },
]



@NgModule({
  declarations: [
    FinishedJobListComponent,
    FinishedJobDetailComponent,
    ReceivedBidDetailComponent
  ],
  imports: [
    AgmCoreModule.forRoot({
      apiKey: googleApiKey.mapApiKey,
      libraries: ['places', 'geometry']
    }),
    CommonModule,
    RatingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ModalModule.forRoot(),
    RouterModule.forChild(routes)
  ],

  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
}) export class FinishedJobsModule { }
