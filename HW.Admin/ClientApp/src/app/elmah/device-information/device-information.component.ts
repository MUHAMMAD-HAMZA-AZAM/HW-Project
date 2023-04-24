import { Component, OnInit } from '@angular/core';
//import { DeviceDetectorService } from 'ngx-device-detector';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CommonService } from '../../Shared/HttpClient/_http';
import { catchError, retry } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';
import { Http } from '@angular/http';

@Component({
  selector: 'app-device-information',
  templateUrl: './device-information.component.html',
  styleUrls: ['./device-information.component.css']
})
export class DeviceInformationComponent implements OnInit {

  public deviceInfo = null;
  public isMobileDevice: boolean;
  public isTabletDevice: boolean;
  public isDesktopDevice: boolean;
  public userAgentInfo: string;
  public osInfo: string;
  public browserInfo: string;
  public osVersionInfo: string;
  public browserVersionInfo: string;
  public deviceTypeInfo: string;

  public ipAddress: string;
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


  constructor(/*private deviceService: DeviceDetectorService,*/ private _modalService: NgbModal, public service: CommonService, private http: Http) {
    this.detectDevice();
    
  }

  ngOnInit() {
    this.getClientIpAddress();
  }

  public detectDevice() {

    //this.deviceInfo = this.deviceService.getDeviceInfo();
    // this.isMobileDevice = this.deviceService.isMobile();
    //this.isTabletDevice = this.deviceService.isTablet();
    //this.isDesktopDevice = this.deviceService.isDesktop();
    this.userAgentInfo = this.deviceInfo.userAgent;
    this.osInfo = this.deviceInfo.os;
    this.osVersionInfo = this.deviceInfo.os_version;
    this.browserInfo = this.deviceInfo.browser;
    this.browserVersionInfo = this.deviceInfo.browser_version;
    console.log(this.deviceInfo);
    
  }

  public getClientIpAddress() {

    this.ipAddress = '';
    this.service.get(this.service.apiRoutes.Elmah.ClientIpAddress + "?clientIPAddress" + this.ipAddress).subscribe(result => {

      console.log(result);
      this.ipAddress = result.json();
      this.ipAddress = '111.119.187.22';
      this.getGEOLocation(this.ipAddress);
      
    });
  }

  public showModal(geoLocationModal) {

    this._modalService.open(geoLocationModal, { size: 'lg' });

  }

  public getGEOLocation(ip) {
    
    let url = "https://api.ipgeolocation.io/ipgeo?apiKey=0f4e06b0721a4a2b82bce38abc80b31a&ip=" + ip;
    console.log(url);
    this.http.get(url).subscribe(res => {
      
      var result = res.json();

      this.latitude = result['latitude'];
      this.longitude = result['longitude'];
      this.callingCode = result['calling_code'];
      this.continentCode = result['continent_code'];
      this.continentName = result['continent_name'];
      this.countryName = result['country_code3'];
      this.countryCapital = result['country_capital'];
      this.city = result['city'];
      this.district = result['district'];
      this.currency = result['currency']['code'];
      this.currencyName = result['currency']['name'];
      this.currencysymbol = result['currency']['symbol'];
      this.isp = result['isp'];
      console.log(result);
      
    });

  }

}
