import { Routes, RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";

const routes: Routes = [
  {
    path: 'PostAd',
    loadChildren: () => import('./postAd/postAd.module').then(m => m.PostAdModule)
  },

  {
    path: 'Home',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule)
  },
  {
    path: 'ManageAd',
    loadChildren: () => import('./manageMyAds/managemyads.module').then(m => m.ManagemyadsModule)
  },
  { 
    path: 'Payments',
    loadChildren: () => import('./payments/payments.module').then(m => m.PaymentsModule)
  },
  {
    path: 'Ratings',
    loadChildren: () => import('./ratings/ratings.module').then(m => m.RatingsModule)
  },
  {
    path: 'Profile',
    loadChildren: () => import('./profile/profile.module').then(m => m.ProfileModule)
  },
  {
    path: 'Registration',
    loadChildren: () => import('./registration/registration.module').then(m => m.RegistrationModule)
  },

  {
    path: 'Notifications',
    loadChildren: () => import('./notifications/notifications.module').then(m => m.NotificationsModule)
  }

]

@NgModule(
  {
    declarations: [
    ],
    imports: [
      CommonModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      RouterModule.forChild(routes),
      
    ]
  }
)
export class SupplierModule { }
