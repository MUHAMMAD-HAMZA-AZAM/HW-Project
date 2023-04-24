import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { GetPostedJobsComponent } from './getPostedJobs/getPostedJobs.component';
import { GetPostedJobDetailComponent } from './getPostedJobDetail/getPostedJobDetail.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgxMyDatePickerModule } from 'ngx-mydatepicker';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxImageCompressService } from 'ngx-image-compress';
import { AmazingTimePickerModule } from 'amazing-time-picker';
const routes: Routes = [
  { path: 'GetPostedJobs', component: GetPostedJobsComponent, },
  { path: 'GetPostedJobDetail', component: GetPostedJobDetailComponent, }
]

@NgModule({
  declarations: [
    GetPostedJobsComponent,
    GetPostedJobDetailComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AmazingTimePickerModule ,
    NgbModule,
    NgxMyDatePickerModule.forRoot(),
    ModalModule.forRoot(),
    RouterModule.forChild(routes)
  ],
  providers: [
    NgxImageCompressService
  ],
})
export class QuoteModule { }
