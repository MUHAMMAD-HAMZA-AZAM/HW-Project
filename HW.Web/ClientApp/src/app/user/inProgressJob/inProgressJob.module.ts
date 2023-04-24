import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalModule } from 'ngx-bootstrap/modal';
import { InprogressJobListComponent } from './inProgressJobList/inProgressJobList.component';
import { InProgreesJobDetailsComponent } from './inProgreesJobDetails/inProgreesJobDetails.component';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxMyDatePickerModule } from 'ngx-mydatepicker';
const routes: Routes = [
  { path: 'InProgreesJobDetails', component: InProgreesJobDetailsComponent, },
  { path: 'InProgressJobList', component: InprogressJobListComponent, }
]

@NgModule({
  declarations: [
    InProgreesJobDetailsComponent,
    InprogressJobListComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ModalModule.forRoot(),
    NgxMyDatePickerModule.forRoot(),
    RouterModule.forChild(routes)
  ]
})
export class InProgressJobModule { }
