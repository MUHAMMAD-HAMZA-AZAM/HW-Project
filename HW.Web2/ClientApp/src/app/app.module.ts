import { BrowserModule, BrowserTransferStateModule, HammerGestureConfig, HAMMER_GESTURE_CONFIG, Title } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { HttpModule } from '@angular/http';
import { AppComponent } from './app.component';
import { AppLayoutComponent } from './shared/layout/app-layout/app-layout.component';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA, ModuleWithProviders } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ResponsiveModule } from 'ngx-responsive';
import { AppDasboardHeaderComponent } from './shared/layout/app-dasboard-header/app-dasboard-header.component';
import { AppDasboardFooterComponent } from './shared/layout/app-dasboard-footer/app-dasboard-footer.component';
import { AppLeftmenuComponent } from './shared/layout/app-leftmenu/app-leftmenu.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { AppcommonfooterComponent } from './shared/layout/appcommonfooter/appcommonfooter.component';
import { AppRoutingModule } from './app-routing.module';
import { DynamicScriptLoaderService } from './shared/CommonServices/dynamicScriptLoaderService';
import { CommonService } from './shared/HttpClient/_http';
import { AuthGuardCustomerService, AuthGuardSupplierService, AuthGuardTradesmanService } from './shared/Gaurds/auth.gaurd';
import { LoginComponent } from './common/login/login.component';
import { TrademanLayoutComponent } from './shared/layout/tradeManLayout/trademanLayout.component';
import { SupplierLayoutComponent } from './shared/layout/supplierLayout/supplierLayout.component';
import { TrademenuLeftComponent } from './shared/layout/tradeManLayout/tradeManLeftmenu/tradeManLeftmenu.component';
import { SupplierLeftmenuComponent } from './shared/layout/supplierLayout/supplierLeftmenu/supplierLeftmenu.component';
import { DataPassService } from './shared/CommonServices/app.datapass.service';
import { NotificationService } from './shared/notifications/toastr-notification.service';
import { ToastrNotificationComponent } from './shared/notifications/component/toastr-notification.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MarketplaceModule } from './marketplace/marketplace.module';
import { UsersApplayoutComponent } from './shared/layout/usersAppLayout/userApplayout.component';
import { AudioRecordingService } from './shared/CommonServices/audioRecording.service';
import { LiveleadsComponent } from './landing-page/liveleads/liveleads.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { AppPublicLayoutComponent } from './shared/layout/app-public-layout/app-public-layout.component';
import { LandingPage2Component } from './landing-page2/landing-page2.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Events } from './common/events';
import { Tradesmanapplayout2Component } from './shared/layout/tradesmanapplayout2/tradesmanapplayout2.component';
import { Supplierapplayout2Component } from './shared/layout/supplierapplayout2/supplierapplayout2.component';
import { Userapplayout2Component } from './shared/layout/userapplayout2/userapplayout2.component';
import { AppDasboardHeader2Component } from './shared/layout/app-dasboard-header2/app-dasboard-header2.component';
import { AppDasboardFooter2Component } from './shared/layout/app-dasboard-footer2/app-dasboard-footer2.component';
import { SocialLoginModule, SocialAuthServiceConfig } from 'angularx-social-login';
import { ImageCropperModule } from 'ngx-image-cropper';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { MypackageslayoutComponent } from './shared/layout/mypackageslayout/mypackageslayout.component';
import { NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { ModalModule } from 'ngx-bootstrap/modal';
import { SkillcategoryComponent } from './skills-category/skillcategory/skillcategory.component';
import { AppHeaderLayoutComponent } from './shared/layout/app-header-layout/app-header-layout.component';
import { BlogListComponent } from './Blog/blog-list/blog-list.component';
import { BlogDetailsComponent } from './Blog/blog-details/blog-details.component';
import { metaTagsService } from './shared/CommonServices/seo-dynamictags.service';
import { NgxImageCompressService } from 'ngx-image-compress';
import { ShareButtonModule } from 'ngx-sharebuttons/button';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ShareService } from 'ngx-sharebuttons';
import { NgxSocialShareModule } from 'ngx-social-share';
import { ClickOutsideModule } from 'ng-click-outside';
import { AutocompleteLibModule } from 'angular-ng-autocomplete';
import { DeviceDetectorService } from 'ngx-device-detector';
import { IconSpriteModule } from 'ng-svg-icon-sprite';
import { SkilllistComponent } from './skilllist/skilllist.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import {GoogleLoginProvider,FacebookLoginProvider} from 'angularx-social-login';
import { TextemailComponent } from './textemail/textemail.component';
import { SkillsmetatagsResolverService } from './shared/resolvers/skills-metatags-resolver.service';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { CookieService } from 'ngx-cookie-service';
import { AngularFireMessagingModule } from '@angular/fire/messaging';
import { AngularFireDatabaseModule } from '@angular/fire/database';
import { AngularFireAuthModule } from '@angular/fire/auth';
import { AngularFireModule } from '@angular/fire';
import { SwiperModule } from 'swiper/angular';
import { MessagingService } from './shared/CommonServices/messaging.service';

import { TransferHttpCacheModule } from '@nguniversal/common';
import { SafePipe } from './shared/Pipes/safe.pipe';
import { BrowserStateInterceptor } from './shared/Interceptors/browser-state.interceptor';
const routes: Routes = [
  {
    path: '', component: AppPublicLayoutComponent,
    children: [
      { path: 'Index', component: LandingPage2Component },
      { path: '', component: LandingPage2Component },
    ],
  },
  {
    path: '', component: AppHeaderLayoutComponent,
    children: [
      {
        path: 'skill/:id/:slug',
        component: SkillcategoryComponent,
        //data: {
          //title: 'Title for First Component',
          //description: 'Description of First Component',
          //robots: 'noindex, nofollow',
          //ogTitle: 'Description of First Component for social media',
        //}
       //resolve: { skillCategoryDetails: SkillsmetatagsResolverService }
      },
      //{ path: 'costGuides/:id/:slug', component: CostGuidesComponent },
      { path: 'blog/:id/:slug', component: BlogDetailsComponent },
      { path: 'blog', component: BlogListComponent },
      { path: 'skillList', component: SkilllistComponent },
    ],
  },

  {
    //path: '', component: AppLayoutComponent,
    path: '', component: AppHeaderLayoutComponent,
    children: [
      { path: 'login/Tradesman', component: LoginComponent },
      { path: 'login/Organization', component: LoginComponent },
      { path: 'login/Customer', component: LoginComponent },
      { path: 'login/Supplier', component: LoginComponent }
    ]
  }
  ,
  {
    path: 'resetpassword',
    component: AppHeaderLayoutComponent,
    loadChildren: () => import('./common/resetPassword/resetpassword.module').then(m => m.ResetpasswordModule)
  },
  //{
  //  path: 'Supplier',
  //  component: SupplierLayoutComponent,
  //  children: [
  //    { path: 'mypackages', component: MypackagesComponent }
  //  ]
  //},
  {
    path: 'Supplier',
    component: Userapplayout2Component,
    loadChildren: () => import('./supplier/supplier.module').then(m => m.SupplierModule),
    canActivate: [AuthGuardSupplierService],
    canActivateChild: [AuthGuardSupplierService]
  },
  {
    path: 'User',
    component: Userapplayout2Component,
    loadChildren: () => import('./user/user.module').then(m => m.UserModule),
    canActivate: [AuthGuardCustomerService],
    canActivateChild: [AuthGuardCustomerService]
  },
  {
    path: 'MarketPlace',
    //component: UsersApplayoutComponent,
    //component: AppHeaderLayoutComponent,
    component: Userapplayout2Component,
    loadChildren: () => import('./marketplace/marketplace.module').then(m => m.MarketplaceModule),
  },
  {
    path: 'User/Agrements',
    //component: AppLayoutComponent,
    component: AppHeaderLayoutComponent,

    loadChildren: () => import('./agrements/agrements.module').then(m => m.AgrementsModule)
  },
  {
    path: 'HWUser',
    //component: AppLayoutComponent,
    component: AppHeaderLayoutComponent,
    loadChildren: () => import('./HelpAndFAQ/helpQuestion.module').then(m => m.HelpQuestionModule)
  },
  {
    path: 'Tradesman',
    component: Userapplayout2Component,
    loadChildren: () => import('./trademan/trademan.module').then(m => m.TrademanModule),
    canActivate: [AuthGuardTradesmanService],
    canActivateChild: [AuthGuardTradesmanService]
  },
  {
    path: "registration",
    //component: AppLayoutComponent,
    component: AppHeaderLayoutComponent,
    loadChildren: () => import('./common/registrationCommon/registrationbasic.module').then(m => m.RegistrationBasicModule)
  },
  //{
  //  path: 'User/Home',
  //  component: AppLayoutComponent,
  //  loadChildren: () => import('./user/home/home.module').then(m => m.HomeModule)
  //},
  {
    path: 'ContactUs',
    //component: AppLayoutComponent,
    component: AppHeaderLayoutComponent,
    loadChildren: () => import('./contactUs/contactUs.module').then(m => m.ContactUsModule)
  },
  {
    path: 'landing-page/liveleads',
    //component: AppLayoutComponent,
    component: AppHeaderLayoutComponent,
    loadChildren: () => import('./landing-page/liveleads/liveleads.module').then(m => m.LiveleadsModule)
  },
  {
    path: 'landing-page',
    component: AppHeaderLayoutComponent,
    loadChildren: () => import('./landing-page/landing-page.module').then(m => m.LandingPageModule)
  },

  {
    path: 'Message',
    loadChildren: () => import('./message/message.module').then(m => m.MessageModule)
  },
  {
    path: 'packagesplans',
    // component: AppLayoutComponent,
    component: Userapplayout2Component,

    loadChildren: () => import('./PackagesPlan/packages/packages.module').then(m => m.PackagesModule)
  },
  {
    path: 'packages',
    component: Userapplayout2Component,
    loadChildren: () => import('./my-packages/my-packages.module').then(m => m.MyPackagesModule)
  },
  {
    path: 'promotions',
    component: AppHeaderLayoutComponent,
    loadChildren: () => import('./promotion/promotion.module').then(m => m.PromotionModule)
  },
  {
    path: 'email/sendemail',
    component: TextemailComponent,
  }
]
const config = {
  breakPoints: {
    xs: { max: 575.98 },
    sm: { min: 576, max: 767.98 },
    md: { min: 768, max: 991.98 },
    lg: { min: 992, max: 1919.98 },
    xl: { min: 1920 }
  },
  debounceTime: 100
};

//let configs = new AuthServiceConfig([
//  {
//    id: FacebookLoginProvider.PROVIDER_ID,
//    provider: new FacebookLoginProvider("206071961734533")
//  },
//  {
//    id: GoogleLoginProvider.PROVIDER_ID,
//    provider: new GoogleLoginProvider("299568931003-lt5ksh26pg8cqijg3ku04s6b622ke5ri.apps.googleusercontent.com")
//  }
//]);
//export function provideConfig() {
//  return configs;
//}
@NgModule({
  declarations: [
    AppComponent,
    AppLayoutComponent,
    AppDasboardHeaderComponent,
    AppDasboardFooterComponent,
    AppLeftmenuComponent,
    LandingPageComponent,
    UsersApplayoutComponent,
    LoginComponent,
    AppcommonfooterComponent,
    SupplierLayoutComponent,
    TrademanLayoutComponent,
    TrademenuLeftComponent,
    SupplierLeftmenuComponent,
    ToastrNotificationComponent,
    AppPublicLayoutComponent,
    LandingPage2Component,
    Tradesmanapplayout2Component,
    Supplierapplayout2Component,
    Userapplayout2Component,
    AppDasboardHeader2Component,
    AppDasboardFooter2Component,
    MypackageslayoutComponent,
    SkillcategoryComponent,
    AppHeaderLayoutComponent,
    BlogListComponent,
    BlogDetailsComponent,
    SkilllistComponent,
    TextemailComponent,
    SafePipe
    //CostGuidesComponent
  ],
  imports: [
    //BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserModule.withServerTransition({ appId: 'HW.Web' }),
    BrowserTransferStateModule,
    NgbModule,
    ModalModule.forRoot(),
    HttpModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    CarouselModule,
    SocialLoginModule,
    NgMultiSelectDropDownModule,
    ImageCropperModule,
    ReactiveFormsModule,
    ClickOutsideModule,
    NgxSpinnerModule,
    ModalModule.forRoot(),
    TransferHttpCacheModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
      tapToDismiss: true,
      closeButton: true,
      progressBar: true,
      onActivateTick: true,
      enableHtml: true,
    }),
    ResponsiveModule.forRoot(config),
    ShareButtonModule,
    FontAwesomeModule,
    NgxSocialShareModule,
    AutocompleteLibModule,
    //ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),
    IconSpriteModule.forRoot({ path: 'assets/sprites/sprite.svg' }),
    RouterModule.forRoot(routes, { enableTracing: false }),
    TranslateModule.forRoot(),
    BrowserModule,
    AngularFireDatabaseModule,
    AngularFireAuthModule,
    AngularFireMessagingModule,
    AngularFireModule.initializeApp(environment.firebase),
    SwiperModule,
    //RouterModule.forRoot(routes, { initialNavigation: 'enabled', relativeLinkResolution: 'legacy' }),
    //JwtModule.forRoot({
    //  config: {
    //    tokenGetter: () => {
    //      return localStorage.getItem('auth_token');
    //    },
    //  },
    //})
  ],
  providers: [
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              '299568931003-lt5ksh26pg8cqijg3ku04s6b622ke5ri.apps.googleusercontent.com'
            )
          },
          {
            id: FacebookLoginProvider.PROVIDER_ID,
            provider: new FacebookLoginProvider('206071961734533')
            //provider: new FacebookLoginProvider('321587559918779')
          }
        ]
      } as SocialAuthServiceConfig,
    },
    //{
    //  provide: HAMMER_GESTURE_CONFIG, useClass: CustomHammerConfig
    //},    
    DynamicScriptLoaderService,
    CommonService,
    CookieService,
    DeviceDetectorService,
    AuthGuardCustomerService,
    AuthGuardSupplierService,
    AuthGuardTradesmanService,
    NgxImageCompressService,
    DataPassService,
    NotificationService,
    DeviceDetectorService,
    AudioRecordingService,
    Events,
    Title,
    metaTagsService,
    ShareService,
    SkillsmetatagsResolverService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: BrowserStateInterceptor,
      multi: true
    },
    //TranslateService
      MessagingService
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
