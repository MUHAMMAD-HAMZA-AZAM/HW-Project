import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalModule } from 'ngx-bootstrap/modal';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InProgressDetailComponent } from './detail/detail.component';
import { InProgressListComponent } from './list/list.component';
const routes: Routes = [
  { path: 'Details', component: InProgressDetailComponent, },
  { path: 'List', component: InProgressListComponent, }
]

@NgModule({
  declarations: [
    InProgressDetailComponent,
    InProgressListComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ModalModule.forRoot(),
    RouterModule.forChild(routes)
  ]
})
export class InProgressJobModule { }
