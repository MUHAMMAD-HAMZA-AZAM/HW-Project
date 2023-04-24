import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ListComponent } from './list/list.component';

const routes: Routes = [
  { path: 'List', component: ListComponent },
]

@NgModule(
  {
    declarations: [
      ListComponent
    ],
    imports: [
      CommonModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      RouterModule.forChild(routes)
    ]
  })
export class NotificationsModule { }
