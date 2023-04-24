import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
//import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { AppComponent } from './app.component';
import { AppLayoutComponent } from './shared/layout/app-layout/app-layout.component';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppDasboardHeaderComponent } from './shared/layout/app-dasboard-header/app-dasboard-header.component';
import { AppDasboardFooterComponent } from './shared/layout/app-dasboard-footer/app-dasboard-footer.component';
import { AppLeftmenuComponent } from './shared/layout/app-leftmenu/app-leftmenu.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { AppcommonfooterComponent } from './shared/layout/appcommonfooter/appcommonfooter.component';
import { AppRoutingModule } from './app-routing.module';
import { DynamicScriptLoaderService } from './shared/CommonServices/dynamicScriptLoaderService';
import { CommonService } from './shared/HttpClient/_http';
import { AuthGuardCustomerService } from './shared/Gaurds/auth.gaurd';
import { LoginComponent } from './common/login/login.component';
import { Ng4LoadingSpinnerModule } from 'ng4-loading-spinner';
import { SupplierModule } from './supplier/supplier.module';
//import { MarketPlaceIndexComponent } from './user/market-place-index/market-place-index.component';
import { UserApplayoutComponent } from './shared/layout/userAppLayout/userAppLayout.component';
import { TrademanLayoutComponent } from './shared/layout/tradeManLayout/trademanLayout.component';
import { SupplierLayoutComponent } from './shared/layout/supplierLayout/supplierLayout.component';
import { TrademenuLeftComponent } from './shared/layout/tradeManLayout/tradeManLeftmenu/tradeManLeftmenu.component';
import { SupplierLeftmenuComponent } from './shared/layout/supplierLayout/supplierLeftmenu/supplierLeftmenu.component';
import { DataPassService } from './shared/CommonServices/app.datapass.service';
import { NotificationService } from './shared/notifications/toastr-notification.service';
import { ToastrNotificationComponent } from './shared/notifications/component/toastr-notification.component';
import { HttpClientModule } from '@angular/common/http';
import { MarketplaceModule } from './marketplace/marketplace.module';
const routes: Routes = [
  {
    path: '', component: AppLayoutComponent,
    children: [
      { path: '', component: LandingPageComponent },
      { path: 'Index', component: LandingPageComponent }
    ],
  },
  {
    path: '', component: AppLayoutComponent,
    children: [
      { path: 'login', component: LoginComponent }
    ]
  }
  ,
  {
    path: 'resetpassword',
    component: AppLayoutComponent,
    loadChildren: './common/resetPassword/resetpassword.module#ResetpasswordModule',
  },
  {
    path: 'Supplier',
    component: SupplierLayoutComponent,
    loadChildren: () => import('./supplier/supplier.module').then(m => m.SupplierModule)
  },

  {
    path: 'User',
    component: UserApplayoutComponent,
    loadChildren: () => import('./user/user.module').then(m => m.UserModule),
    //canActivate: [AuthGuardCustomerService],
    //canActivateChild: [AuthGuardCustomerService]

  },
  {
    path: 'MarketPlace',
    component: UserApplayoutComponent,
    loadChildren: () => import('./marketplace/marketplace.module').then(m => m.MarketplaceModule),
    //canActivate: [AuthGuardCustomerService],
    //canActivateChild: [AuthGuardCustomerService]

  },

  
   {
     path: 'User/Agrements',
     component: AppLayoutComponent,
     loadChildren: () => import('./agrements/agrements.module').then(m => m.AgrementsModule)
  },
   {
     path: 'HWUser',
     component: AppLayoutComponent,
     loadChildren: () => import('./HelpAndFAQ/helpQuestion.module').then(m => m.HelpModule)
  },



  {
    path: 'Trademan',
    component: TrademanLayoutComponent,
    loadChildren: () => import('./trademan/trademan.module').then(m => m.TrademanModule)
  },

  {
    path: "registration",
    component: AppLayoutComponent,
    loadChildren: () => import('./common/registrationCommon/registrationbasic.module').then(m => m.RegistrationBasicModule)
  },
  {
    path: 'User/Home',
    component: AppLayoutComponent,
    loadChildren: () => import('./user/home/home.module').then(m => m.HomeModule)
  },

  {
    path: 'Contacts',
    component: AppLayoutComponent,
    loadChildren: () => import('./contactUs/contactUs.module').then(m => m.ContactUsModule)
  },

  
]

@NgModule({
  declarations: [
    AppComponent,
    AppLayoutComponent,
    AppDasboardHeaderComponent,
    AppDasboardFooterComponent,
    AppLeftmenuComponent,
    LandingPageComponent,
    UserApplayoutComponent,
    LoginComponent,
    AppcommonfooterComponent,
    SupplierLayoutComponent,
    TrademanLayoutComponent,
    TrademenuLeftComponent,
    SupplierLeftmenuComponent,
    ToastrNotificationComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    Ng4LoadingSpinnerModule.forRoot(),
    RouterModule.forRoot(routes),
    ToastrModule.forRoot({
      timeOut: 5000000,
      positionClass: 'toast-center-custom',
      preventDuplicates: true,
      tapToDismiss: true,
      closeButton: true,
      progressBar: true,
      onActivateTick: true,
      enableHtml: true
    })
  ],
  providers: [
    DynamicScriptLoaderService,
    CommonService,
    AuthGuardCustomerService,
    DataPassService,
    NotificationService
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
