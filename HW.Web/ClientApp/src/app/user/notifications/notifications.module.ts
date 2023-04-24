import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { NotificationsComponent } from './notification/notifications.component';



const routes: Routes = [
  { path: 'NotificationList', component: NotificationsComponent, }
]

@NgModule({
  declarations: [
    NotificationsComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forChild(routes)
  ]
})
export class NotificationsModule { }
