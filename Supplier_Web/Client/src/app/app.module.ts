import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AsyncPipe, CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule, BrowserTransferStateModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RegistrationModule } from './registration/registration.module';
import { RouterModule, Routes } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { AppComponent } from './app.component';
import { CommonService } from './Shared/HttpClient/HttpClient';
import { AppHeaderComponent } from './Shared/Layout/app-header/app-header.component';
import { AppFooterComponent } from './Shared/Layout/app-footer/app-footer.component';
import { AppLayoutComponent } from './Shared/Layout/app-layout/app-layout.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UploadFileService } from './Shared/HttpClient/upload-file.service';
import { FacebookLoginProvider, GoogleLoginProvider, SocialAuthService, SocialAuthServiceConfig } from 'angularx-social-login';
import { AuthGuardSupplierService } from './Shared/Gaurds/auth.gaurd';
import { NotFoundComponent } from './Shared/not-found/not-found.component';
import { ClickOutsideModule } from 'ng-click-outside';
import { AngularFireMessagingModule } from '@angular/fire/messaging';
import { AngularFireDatabaseModule } from '@angular/fire/database';
import { AngularFireAuthModule } from '@angular/fire/auth';
import { AngularFireModule } from '@angular/fire';
import { environment } from '../environments/environment';
import { MessagingService } from './Shared/HttpClient/messaging.service';
import { metaTagsService } from './Shared/HttpClient/seo-dynamic.service';
import { BrowserStateInterceptor } from './Shared/Interceptors/browser-state.interceptor';
import { TransferHttpCacheModule } from '@nguniversal/common';
import { ShippingService } from './Shared/HttpClient/shipping.service';

import { NgxSliderModule } from '@angular-slider/ngx-slider';
const routes: Routes = [
  //{
  //  path: '', redirectTo: '/login', pathMatch: 'full'
  //},
  {
    path: '',
    component: AppLayoutComponent,
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule)
  },
  {
    path: 'user',
    component: AppLayoutComponent,
    loadChildren: () => import('./registration/registration.module').then(m => m.RegistrationModule)
  },
  {
    path: '',
    component: AppLayoutComponent,
    loadChildren: () => import('./product/product.module').then(m => m.ProductModule)
  },
  {
    path: 'cart',
    component: AppLayoutComponent,
    loadChildren: () => import('./cart/cart.module').then(m => m.CartModule),
    canActivate: [AuthGuardSupplierService],
    canActivateChild: [AuthGuardSupplierService],
  },
  {
    path: 'user',
    component: AppLayoutComponent,
    loadChildren: () => import('./user/user.module').then(m => m.UserModule),
    canActivate:[AuthGuardSupplierService],
    canActivateChild: [AuthGuardSupplierService],
  },
  {
    path: 'Message',
    loadChildren: () => import('./message/message.module').then(m => m.MessageModule)
  },
  {
    path: '**',
    component: AppLayoutComponent,
    children: [
      { path: "**", component: NotFoundComponent}
    ]
  }
  
]

@NgModule({
  declarations: [
    AppComponent,
    AppHeaderComponent,
    AppFooterComponent,
    AppLayoutComponent,
    NotFoundComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'SupplierWeb' }),
    BrowserTransferStateModule,
    CommonModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    FormsModule,
    NgbModule,
    NgxSliderModule,
    HttpClientModule,
    RegistrationModule,
    NgxSpinnerModule,
    TransferHttpCacheModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-top-right',
      preventDuplicates: true
    }
    ),
    RouterModule.forRoot(routes, {
    initialNavigation: 'enabled'
}),
    ClickOutsideModule,
    AngularFireDatabaseModule,
    AngularFireAuthModule,
    AngularFireMessagingModule,
    AngularFireModule.initializeApp(environment.firebase),
  ],
  providers: [CommonService, UploadFileService, AuthGuardSupplierService, metaTagsService,
    SocialAuthService,
    MessagingService, ShippingService, AsyncPipe,
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              '299568931003-lt5ksh26pg8cqijg3ku04s6b622ke5ri.apps.googleusercontent.com',
              //'776497134452-todd0hqprmsutmumnojrdtm63f0mfart.apps.googleusercontent.com'
            )
          },
          {
            id: FacebookLoginProvider.PROVIDER_ID,
            provider: new FacebookLoginProvider(
              '206071961734533'
            )
          }
        ]
      } as SocialAuthServiceConfig,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: BrowserStateInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
