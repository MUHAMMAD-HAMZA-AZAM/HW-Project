import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { AppLeftMenuComponet } from './Shared/Layout/app-leftmenu/app-leftmenu.component';
import { AppHeaderComponent } from './Shared/Layout/app-header/app-header.component';
import { AppFooterComponent } from './Shared/Layout/app-footer/app-footer.component';
import { AppPageTitleComponent } from './Shared/Layout/app-pageTitle/app-pageTitle.component';
import { AppLayoutComponent } from './Shared/Layout/app-layout/app-layout.component';
import { AppLogoutModelComponent } from './Shared/Layout/app-logout-model/app-logout-model.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonService } from './Shared/HttpClient/_http';
//import { Ng4LoadingSpinnerModule } from 'ng4-loading-spinner';
import { NgxSpinnerModule  } from "ngx-spinner";
import { LoginComponent } from './login/login.component';
import { AppEmptyComponent } from './Shared/Layout/app-empty/app-empty.component';
import { ForgotPasswordComponent } from './login/forgot-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { AuthGuardService } from './Shared/Gaurds/auth.gaurd';
import { SortList } from './Shared/Sorting/sortList';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { DynamicScriptLoaderService } from './Shared/CommonServices/dynamicScriptLoaderService';
import { ActiveJobsDetailsComponent } from './Jobs/active-jobs-details/active-jobs-details.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
//import { ClickOutsideModule } from 'ng-click-outside';
import { ToastrModule } from 'ngx-toastr';
import { RecaptchaModule } from 'ng-recaptcha';
import { NgxSpinnerService } from "ngx-spinner";
import { AngularEditorModule } from '@kolkov/angular-editor';
import { ImageCropperModule } from 'ngx-image-cropper';
//import { NgDragDropModule } from 'ng-drag-drop';
import { EventsService } from './Shared/events.service';
import { RequestCallBackComponent } from './request-call-back/request-call-back.component';
//import { DeviceDetectorService } from 'ngx-device-detector';
import { MatTreeModule } from '@angular/material/tree';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { AutocompleteLibModule } from 'angular-ng-autocomplete';
import { AngularFireMessagingModule } from '@angular/fire/messaging';
import { AngularFireDatabaseModule } from '@angular/fire/database';
import { AngularFireAuthModule } from '@angular/fire/auth';
import { AngularFireModule } from '@angular/fire';
import { MessagingService } from './shared/CommonServices/messaging.service';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  {
    path: 'login', component: LoginComponent
  },
  {
    path: 'forgotPassword', component: ForgotPasswordComponent
  },
  {
    path: 'resetPassword', component: ResetPasswordComponent
  },
  {
    path: 'JobDetails', component: ActiveJobsDetailsComponent
  },
  
  //{
  //  path: 'reports', component: ReportComponent
  //},
  
  {
    path: 'cms',
    component: AppLayoutComponent,
    loadChildren: () => import('src/app/cms/cms.module').then(m => m.CMSModule),
    canActivate: [AuthGuardService],
    canActivateChild: [AuthGuardService]
  },
  {
    path: '',
    component: AppLayoutComponent,
    children: [
      {
        path: 'request-CallBack', component: RequestCallBackComponent
      },
    ],
    canActivate: [AuthGuardService],
    canActivateChild: [AuthGuardService]
  },
  {
    path: 'elmah',
    component: AppLayoutComponent,
    loadChildren: () => import('src/app/elmah/elmah.module').then(m => m.ElmahModule),
    canActivate: [AuthGuardService],
    canActivateChild: [AuthGuardService]
  },
  {
    path: 'Jobmodule',
    component: AppLayoutComponent,
    loadChildren: () => import('src/app/Jobs/jobmodule.module').then(m => m.JobmoduleModule),
    canActivate: [AuthGuardService],
    canActivateChild: [AuthGuardService]
  },
  {
    path: 'home',
    component: AppLayoutComponent,
    loadChildren: () => import('src/app/Home/homemodule.module').then(m => m.HomemoduleModule),
    canActivate: [AuthGuardService],
    canActivateChild: [AuthGuardService]
  },
  {
    path: 'reports',
    component: AppLayoutComponent,
    loadChildren: () => import('src/app/Reports/reportsmodule.module').then(m => m.ReportsModule),
    canActivate: [AuthGuardService],
    canActivateChild: [AuthGuardService],
  },
  {
    path: 'Settings',
    component: AppLayoutComponent,
    loadChildren: () => import('src/app/Settings/settingsmodule.module').then(m => m.SettingsModule),
    canActivate: [AuthGuardService],
    canActivateChild: [AuthGuardService]
  },
  {
    path: 'Usermodule',
    component: AppLayoutComponent,
    loadChildren: () => import('src/app/Users/usermodule.module').then(m => m.UsermoduleModule),
    canActivate: [AuthGuardService],
    canActivateChild: [AuthGuardService]
  },
  {
    path: 'errormodule',
    component: AppLayoutComponent,
    loadChildren: () => import('src/app/error-page/errormodule.module').then(m => m.ErrormoduleModule),
    canActivate: [AuthGuardService],
    canActivateChild: [AuthGuardService]
  },
  {
    path: 'accountsmodule',
    component: AppLayoutComponent,
    loadChildren: () => import('src/app/accounts/accounts.module').then(m => m.AccountsModule),
    canActivate: [AuthGuardService],
    canActivateChild: [AuthGuardService]
  },
  {
    path: 'supplier',
    component: AppLayoutComponent,
    loadChildren: () => import('src/app/supplier/supplier.module').then(m => m.SupplierModule),
    canActivate: [AuthGuardService],
    canActivateChild: [AuthGuardService]
  },
];

@NgModule({
  declarations: [
    AppComponent,
    AppLeftMenuComponet,
    AppHeaderComponent,
    AppFooterComponent,
    AppPageTitleComponent,
    AppLayoutComponent,
    AppLogoutModelComponent,
    LoginComponent,
    AppEmptyComponent,
    SortList,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    ActiveJobsDetailsComponent,
    ActiveJobsDetailsComponent,
    RequestCallBackComponent,
   ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    NgxSpinnerModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    HttpModule,
    BrowserAnimationsModule,
    AngularFireDatabaseModule,
    AngularFireAuthModule,
    AngularFireMessagingModule,
    AngularFireModule.initializeApp(environment.firebase),
    //ClickOutsideModule,
    ToastrModule.forRoot({
      maxOpened: 1,
      timeOut: 2000,
      preventDuplicates: true,
    }),
    //Ng4LoadingSpinnerModule.forRoot(),
    NgxSpinnerModule,
    RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' }),
    ModalModule.forRoot(),
    PopoverModule.forRoot(),
    //NgDragDropModule.forRoot(),
    NgbModule,
    RecaptchaModule,
    AngularEditorModule,
    ImageCropperModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),
    //SelectDropDownModule,
    MatTreeModule,
    MatIconModule,
    MatButtonModule,
    AutocompleteLibModule
  ], 
  providers: [
    CommonService,
    MessagingService,
    SortList,
    AuthGuardService,
    DynamicScriptLoaderService,
    EventsService,
    //DeviceDetectorService,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA], 
  bootstrap: [AppComponent]
})

export class AppModule { }
