import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { AsyncPipe, CommonModule } from '@angular/common';
import { AppHeaderComponent } from './Shared/Layout/app-header/app-header.component';
import { AppLeftmenuComponent } from './Shared/Layout/app-leftmenu/app-leftmenu.component';
import { AppFooterComponent } from './Shared/Layout/app-footer/app-footer.component';
import { AppLayoutComponent } from './Shared/Layout/app-layout/app-layout.component';
import { EmptyLayoutComponent } from './Shared/Layout/empty-layout/empty-layout.component';
import { CommonService } from './Shared/HttpClient/_http';
import { HttpClientModule } from '@angular/common/http';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ToastrModule } from 'ngx-toastr';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UploadFileService } from './Shared/HttpClient/upload-file.service';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AuthGuardSupplierService } from './Shared/Gaurds/auth.gaurd';
import { AngularFireMessagingModule } from '@angular/fire/messaging';
import { AngularFireDatabaseModule } from '@angular/fire/database';
import { AngularFireAuthModule } from '@angular/fire/auth';
import { AngularFireModule } from '@angular/fire';
import { environment } from '../environments/environment';
import { MessagingService } from './Shared/HttpClient/messaging.service';
import { NgxDateRangeModule } from 'ngx-daterange';
import { ImageCropperModule } from 'ngx-image-cropper';

const routes: Routes = [
  {
    path: 'home',
    component: AppLayoutComponent,
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
    canActivate: [AuthGuardSupplierService],
    canActivateChild: [AuthGuardSupplierService],
  },
  {
    path: 'product',
    component: AppLayoutComponent,
    loadChildren: () => import('./product/product.module').then(m => m.ProductModule),
    canActivate: [AuthGuardSupplierService],
    canActivateChild: [AuthGuardSupplierService],
  },
  {
    path: 'payment',
    component: AppLayoutComponent,
    loadChildren: () => import('./payment/payment.module').then(m => m.PaymentModule),
    canActivate: [AuthGuardSupplierService],
    canActivateChild: [AuthGuardSupplierService],
  },
  {
    path: '', redirectTo: '/login', pathMatch: 'full'
  },
  {
    path: '',
    component: EmptyLayoutComponent,
    loadChildren: () => import('src/app/registration/registration.module').then(m => m.RegistrationModule)
  },
  {
    path: 'profile',
    component: AppLayoutComponent,
    loadChildren: () => import('src/app/profile-management/profile-management.module').then(m => m.ProfileManagementModule),
    canActivate: [AuthGuardSupplierService],
    canActivateChild: [AuthGuardSupplierService],
  },
  {
    path: 'promotions',
    component: AppLayoutComponent,
    loadChildren: () => import('src/app/promotions/promotions.module').then(m => m.PromotionsModule),
    canActivate: [AuthGuardSupplierService],
    canActivateChild: [AuthGuardSupplierService],
  },
  {
    path: 'inventory',
    component: AppLayoutComponent,
    loadChildren: () => import('src/app/inventory-management/inventory-management.module').then(m => m.InventoryManagementModule),
    canActivate: [AuthGuardSupplierService],
    canActivateChild: [AuthGuardSupplierService],
  },
  {
    path: 'freeshipping',
    component: AppLayoutComponent,
    loadChildren: () => import('src/app/freeshipping/freeshipping.module').then(m => m.FreeshippingModule),
    canActivate: [AuthGuardSupplierService],
    canActivateChild: [AuthGuardSupplierService],
  }
];
@NgModule({
  declarations: [
    AppComponent,
    AppHeaderComponent,
    AppLeftmenuComponent,
    AppFooterComponent,
    AppLayoutComponent,
    EmptyLayoutComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AngularFireDatabaseModule,
    AngularFireAuthModule,
    AngularFireMessagingModule,
    AngularFireModule.initializeApp(environment.firebase),
    HttpClientModule,
    NgxSpinnerModule,
    ReactiveFormsModule,
    ImageCropperModule,
    NgbModule,
    FormsModule,
    ModalModule.forRoot(),
    ToastrModule.forRoot({
        timeOut: 3000,
        positionClass: 'toast-top-right',
        preventDuplicates: true
      }
    ),
    RouterModule.forRoot(routes),
    
  ],
  providers: [CommonService, UploadFileService, AuthGuardSupplierService,MessagingService, AsyncPipe,
],
  bootstrap: [AppComponent]
})
export class AppModule { }
