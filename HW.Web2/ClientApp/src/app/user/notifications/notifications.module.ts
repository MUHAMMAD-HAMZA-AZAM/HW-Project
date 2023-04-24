import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { NotificationsListComponent } from './list/list.component';



const routes: Routes = [
  { path: 'List', component: NotificationsListComponent, }
]

@NgModule({
  declarations: [
    NotificationsListComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forChild(routes)
  ]
})
export class NotificationsModule { }
