import { Component, Inject, OnInit, PLATFORM_ID } from "@angular/core";
import { DomSanitizer } from "@angular/platform-browser";
import { Router, NavigationStart, NavigationEnd, NavigationCancel, NavigationError, ActivatedRoute } from '@angular/router';
import { metaTagsService } from "./shared/CommonServices/seo-dynamictags.service";
import { DeviceDetectorService, DeviceInfo } from "ngx-device-detector";
import { CommonService } from "./shared/HttpClient/_http";
import { Http } from "@angular/http";
import { ResponseVm } from "./models/commonModels/commonModels";
import { NgxSpinnerService } from "ngx-spinner";
import { isPlatformBrowser } from "@angular/common";
import { filter, map, mergeMap } from "rxjs/operators";
import { IPageSeoVM } from "./shared/Enums/Interface";
import { HttpClient } from "@angular/common/http";
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';
  //template: string = `<div class="loader-container">
  //  <div class="loader-logo">
  //    <div class="loader-circle">
  //     <img src="../assets/hoomwork-loader.gif" />
  //   </div>
  //  </div>
  //</div>`;
  template: string = `<img height=80px; wigth=50px
   src="../assets/hoomwork-loader.gif"/>`;

  //Device Information Code
  public deviceInfo: DeviceInfo = {} as DeviceInfo;
  public isMobileDevice: boolean = false;
  public isTabletDevice: boolean=false;
  public isDesktopDevice: boolean=false;
  public userAgentInfo: string="";
  public osInfo: string="";
  public browserInfo: string="";
  public osVersionInfo: string="";
  public browserVersionInfo: string="";
  public deviceTypeInfo: string="";
  public ipAddress: string="";
  public clientIpAddress: string="";
  public latitude: string = '';
  public longitude: string = '';
  public currency: string = '';
  public currencysymbol: string = '';
  public isp: string = '';
  public city: string = '';
  public callingCode = '';
  public continentCode = '';
  public continentName = '';
  public countryCapital = '';
  public countryName = '';
  public countryTId = '';
  public district = '';
  public currencyName = '';
  public result1: any;
  //public pipe;
  public CreatedDate: Date = new Date;
  public loginCheck: boolean = false;
  public responseVM: ResponseVm = {} as ResponseVm;
  //Device Information Code End

  constructor(
    public Loader: NgxSpinnerService,
    public sanitizer: DomSanitizer,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private _metaService: metaTagsService,
    private deviceService: DeviceDetectorService,
    public service: CommonService,
    private http: HttpClient,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    if (isPlatformBrowser(this.platformId)) {
      window.scroll();
    }
  }
  ngOnInit() {
    this.detectDevice();
    this.getClientIpAddress();
    this.bindMetaTags();
  }
  public getChild(activatedRoute: ActivatedRoute): ActivatedRoute {
    if (activatedRoute.firstChild) {
      return this.getChild(activatedRoute.firstChild);
    } else {
      return activatedRoute;
    }
  }
  public bindMetaTags() {
    this._metaService.updateTitle("Hoomwork");
    this._metaService.updateTag({ name: "robots", content: 'index,follow' })
    this._metaService.updateKeyWords("Home Maintenance Services | Home Repair Services | Hoomwork");
    this._metaService.updateDescription("Hoomwork Is Pakistan's Biggest Trusted Marketplace That Connects People With Rated Suppliers And Skilled Electricians, Plumbers, Cleaners And More. .");
  }

  //public scrollTop(event: Event) {
  //  window.scroll(0, 0);
  //}


  //--------------- Device Information Code 
  public detectDevice() {

    this.deviceInfo = this.deviceService.getDeviceInfo();
    this.isMobileDevice = this.deviceService.isMobile();
    this.isTabletDevice = this.deviceService.isTablet();
    this.isDesktopDevice = this.deviceService.isDesktop();
    this.userAgentInfo = this.deviceInfo.userAgent;
    this.osInfo = this.deviceInfo.os;
    this.osVersionInfo = this.deviceInfo.os_version;
    this.browserInfo = this.deviceInfo.browser;
    this.browserVersionInfo = this.deviceInfo.browser_version;
  }

  //------------------ Get Client Ip Address
  public getClientIpAddress() {
    let getIp_ApiURL = "https://api.ipify.org/?format=json";
    this.http.get(getIp_ApiURL).subscribe(result => {
      this.clientIpAddress = <any>result ;
      this.getGeoLocationDetails();
    }, error => {
      console.log(error);
    });
  }

 //------------------- Get GeoLocation Details From Client Ip Address
  public getGeoLocationDetails() {
    let getGeoLocation_ApiURL = "https://api.ipgeolocation.io/ipgeo?apiKey=0f4e06b0721a4a2b82bce38abc80b31a&ip=";
    this.http.get(getGeoLocation_ApiURL).subscribe(res => {
      var result = <any>res ;
      let obj = {
        MobileDevice: this.isMobileDevice,
        TabletDevice: this.isTabletDevice,
        DesktopDevice: this.isDesktopDevice,
        userAgentInfo: this.userAgentInfo,
        Os: this.osInfo,
        Browser: this.browserInfo,
        OsVersion: this.osVersionInfo,
        BrowserVersion: this.browserVersionInfo,
        Ip: result['ip'],
        currency: result['currency']['code'],
        currencysymbol: result['currency']['symbol'],
        isp: result['isp'],
        City: result['city'],
        callingCode: result['calling_code'],
        continentCode: result['continent_code'],
        continentName: result['continent_name'],
        countryCapital: result['country_capital'],
        Country: result['country_code3'],
        district: result['district'],
        currencyName: result['currency']['name'],
        latitude: result['latitude'],
        longitude: result['longitude'],
        CreatedOn: result['time_zone']['current_time'],
        Platform: "WEB"
      };
       //console.log(obj);
      this.service.post(this.service.apiRoutes.Analaytics.SaveAnalytics, obj).subscribe(result => {
        this.responseVM = <ResponseVm>result;

      });

    }, error => {
      let obj = {
        MobileDevice: this.isMobileDevice,
        TabletDevice: this.isTabletDevice,
        DesktopDevice: this.isDesktopDevice,
        userAgentInfo: this.userAgentInfo,
        Os: this.osInfo,
        Browser: this.browserInfo,
        OsVersion: this.osVersionInfo,
        BrowserVersion: this.browserVersionInfo,
        Platform: "WEB",
        Ip: "Network is Private",
        CreatedOn: "2021 01 10 "
      };

      this.service.post(this.service.apiRoutes.Analaytics.SaveAnalytics, obj).subscribe(response => {
        this.responseVM = <ResponseVm>response ;
      });
    });

  }

  //Device Information Code End

  // To Check Is User Login Or not 
  public IsUserLogIn() {
    var token = localStorage.getItem("auth_token");
    if (token != null && token != '') {
      this.loginCheck = true;

    }
    else {
      this.loginCheck = false;
    }
  }
}
