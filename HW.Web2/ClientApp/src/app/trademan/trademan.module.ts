import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AgmCoreModule } from '@agm/core';

const routes: Routes = [
  {
    path: 'Profile',
    loadChildren: () => import('./profile/profile.module').then(m => m.ProfileModule)
  },
  {
    path: 'Skill/:id',
    loadChildren: () => import('./profile/skillprofile/skillprofile.component').then(m => m.SkillprofileComponent)
  },
  {
    path: 'LiveLeads',
    loadChildren: () => import('./liveLeads/liveLeads.module').then(m => m.LiveLeadsModule)
  },
  {
    path: 'MyBids',
    loadChildren: () => import('./myBids/myBids.module').then(m => m.MyBidsModule)
  },
  {
    path: 'MyJobs',
    loadChildren: () => import('./jobs/jobs.module').then(m => m.JobModule)
  },
  {
    path: 'Invoice',
    loadChildren: () => import('./invoice/invoice.module').then(m => m.InvoiceModule)
  },
  {
    path: 'Notifications',
    loadChildren: () => import('./notifications/notifications.module').then(m => m.NotificationsModule)
  },
  {
    path: 'Registration',
    loadChildren: () => import('./registration/registration.module').then(m => m.RegistrationModule)
  },
  
]




@NgModule({
 
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyBh_wL0Jb7M1m4xBLNWmOWkVPbE5vpBHck'
  
    })
  ],
  

  
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
})
export class TrademanModule { }
