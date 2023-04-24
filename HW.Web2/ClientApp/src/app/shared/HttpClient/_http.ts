import { Injectable, Inject } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Deferred } from 'ts-deferred';
import { NgxSpinnerService } from 'ngx-spinner';
import * as moment from 'moment-mini-ts'
import * as CryptoJS from 'crypto-js';
import {NavigationExtras, Router } from '@angular/router';
import { apiRoute, pagesUrls } from '../ApiRoutes/ApiRoutes';
import { ApiUrls } from '../ApiRoutes/ApiUrls';
import { NotificationService } from '../notifications/toastr-notification.service';
import { httpStatus } from '../Enums/enums';
import { DomSanitizer } from '@angular/platform-browser';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
@Injectable({ providedIn: 'root'})
export class CommonService {
  public pagesUrl = pagesUrls;
  public apiUrls = ApiUrls;
  public loginCheck: boolean = false;
  public baseUrl: string|null;
  public webBaseUrl: string|null ="";
  private headers = new Headers({ 'Content-Type': 'application/json' });
  constructor(
    private http: Http,
    private http1: HttpClient,
  public Loader: NgxSpinnerService,
  private router: Router,
    public Notification: NotificationService
    , private sanitizer: DomSanitizer
  ) {
    this.baseUrl = "http://172.16.1.36:15790/api/" //"https://hoomwork.com/GatewayNet5/api/"; //"https://test2.hoomwork.com/gateway/api/"; //"http://172.16.1.52:15790/api/";   //localStorage.getItem("baseUrl");
    //this.baseUrl = 'https://www.hoomwork.com/gateway/api/';
    //this.webBaseUrl = localStorage.getItem("webBaseUrl");
    this.webBaseUrl = "https://www.hoomwork.com/weblivedll/api/";

  }

  public apiRoutes = apiRoute;
  public ArticleDate = 'Posted:' + this.formatDate(new Date(),'DD MMM YYYY') +" "+ 'by'+" ";

  get(api: string) {
    var token = localStorage.getItem('auth_token');
    if (!token) {
      token = "";
    }
    let headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8', 'Access-Control-Allow-Origin': '*', 'Access-Control-Expose-Headers': '*', 'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS', 'Access-Control-Allow-Headers': '*', 'Authorization': token });
    let options = { headers: headers };
    return this.http1
      .get(this.baseUrl + api, options)
      .map(response => {
        if (typeof response == "string") {
          return this.JsonParseData(response);
        }
        else {
          return response;
        }
      });
  }
  get123(api: string):Observable<any> {
    var token = localStorage.getItem('auth_token');
    if (!token) {
      token = "";
    }
    let headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8', 'Access-Control-Allow-Origin': '*', 'Access-Control-Expose-Headers': '*', 'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS', 'Access-Control-Allow-Headers': '*', 'Authorization': token });
    let options = { headers: headers };
    return this.http1
      .get(this.baseUrl + api, options)
      .map(response => {
        if (typeof response == "string") {
          return this.JsonParseData(response);
        }
        else {
          return response;
        }
      });
  }
  encrypt(value: string, IntegritySalt: string): string {
    return CryptoJS.HmacSHA256(value, IntegritySalt).toString();
  }

  post(api: string, data: any, toWeb?: string) {
    let Url: any;
    if (toWeb)
      Url = this.webBaseUrl;
    else
      Url = this.baseUrl;
    var token = localStorage.getItem('auth_token');
    if (!token) {
      token = "";
    }
    let headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8', 'Access-Control-Allow-Origin': '*', 'Access-Control-Expose-Headers': '*', 'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS', 'Access-Control-Allow-Headers': '*', 'Authorization': token });
    let options = { headers: headers };
    return this.http1
      .post(Url + api, JSON.stringify(data), options)
      .map(response => {
        if (typeof response == "string") {
          return this.JsonParseData(response);
        }
        else {
          return response;
        }
      });

  }

  save(api: string, data: any) {

    var token = localStorage.getItem('auth_token');
    if (!token) {
      token = "";
    }
    let headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8', 'Access-Control-Allow-Origin': '*', 'Access-Control-Expose-Headers': '*', 'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS', 'Access-Control-Allow-Headers': '*', 'Authorization': token });
    let options = { headers: headers };
    if (data.id > 0) {
      return this.http1
        .put(this.baseUrl + api + "/" + data.id, JSON.stringify(data), options)
        .map(response => {
          if (typeof response == "string") {
            return this.JsonParseData(response);
          }
          else {
            return response;
          }
        });
    }
    else {
      return this.http1
        .post(api, JSON.stringify(data), options)
        .map(response => {
          if (typeof response == "string") {
            return this.JsonParseData(response);
          }
          else {
            return response;
          }
        });
    }
  }

  //public SaveAndNotify(apiUrl: string, data: any, navigateUrl?: string, navigateParams?: NavigationExtras, messageKey?: string) {
  //  let d = new Deferred<any>();
  //  let p = d.promise;
  //  this.Loader.show();
  //  this.post(this.baseUrl + apiUrl, data).subscribe(data => {

  //    var result = JSON.parse(data );
  //    if (result.status == httpStatus.Ok) {
  //      messageKey = (messageKey != null && messageKey != undefined) ? messageKey : "Successfully Saved.";
  //      this.Notification.success(messageKey);
  //      if (navigateUrl != null && navigateUrl != undefined)
  //        this.NavigateToRoute(navigateUrl, navigateParams);
  //    }
  //    else {
  //      this.Notification.error("Some thing went wrong.");
  //    }
  //    this.Loader.hide();
  //    d.resolve(result);
  //  }, error => {
  //    this.Loader.hide();
  //    this.Notification.error("Some thing went wrong."); console.log("erre", error);
  //  });
  //  return p;
  //}

  public GetData(apiUrl: string, showLoader?: boolean) {
    let d = new Deferred<any>();
    let p = d.promise;
    if (apiUrl == 'undefined' || apiUrl == null || apiUrl == "") {
      return p;
    }
    if (showLoader)
      this.Loader.show();
    this.get(apiUrl).subscribe(data => {
      var result = data;
      if (typeof data == "string") {
        result = this.JsonParseData(data);
      }
      else {
        result = data;
      }
      d.resolve(result);
      if (showLoader)
        this.Loader.hide();
    }, error => {
      console.log("erre", error)
      this.Loader.hide();
    });
    return p;
  }

  transform(base64Image: any) {
    return this.sanitizer.bypassSecurityTrustResourceUrl('data:image/png;base64,' + base64Image);
  }

  public PostData(apiUrl: string, data: any, showLoader?: boolean, toWeb?: string) {
    let d = new Deferred<any>();
    let p = d.promise;
    if (apiUrl == 'undefined' || apiUrl == null || apiUrl == "") {
      return p; 
    }
    if (showLoader)
      this.Loader.show();
    this.post(apiUrl, data, toWeb).subscribe(data => {
      var result = data;
      if (typeof data == "string") {
        result = this.JsonParseData(data);
      }
      else {
        result = data;
      }
      d.resolve(result);
      if (showLoader)
        this.Loader.hide();
    },
      error => {

        console.log("error", error);
        this.Loader.hide();
        d.reject(error);
      }
    );
    return p;
  }


  public SaveData(apiUrl: string, data: any, navigateUrl?: string, queryParams?: NavigationExtras, messageKey?: string) {
    let d = new Deferred<any>();
    let p = d.promise;
    this.Loader.show();
    this.PostData(apiUrl, data, true).then(data => {
      var result = data;
      if (result.status == httpStatus.Ok) {
        messageKey = (messageKey != null && messageKey != undefined) ? messageKey : "Successfully Saved.";
        this.Notification.success(messageKey);
        if (navigateUrl != null && navigateUrl != undefined)
          this.NavigateToRouteWithQueryString(navigateUrl, queryParams);
      }
      else {
        this.Notification.error("Some thing went wrong.");
      }
      this.Loader.hide();
      d.resolve(result);
    });
    return p;
  }

  update(api: string, id: number, data: any) {
    var token = localStorage.getItem('auth_token');
    if (!token) {
      token = "";
    }
    let headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8', 'Access-Control-Allow-Origin': '*', 'Access-Control-Expose-Headers': '*', 'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS', 'Access-Control-Allow-Headers': '*', 'Authorization': token });
    let options = { headers: headers };
    return this.http1
      .put(this.baseUrl + api + "/" + id, JSON.stringify(data), options);
    //.map(response => response );
  }

  delete(api: string, data: any) {
    var token = localStorage.getItem('auth_token');
    if (!token) {
      token = "";
    }
    let headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8', 'Access-Control-Allow-Origin': '*', 'Access-Control-Expose-Headers': '*', 'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS', 'Access-Control-Allow-Headers': '*', 'Authorization': token });
    let options = { headers: headers };
    return this.http1
      .delete(this.baseUrl + api + "/" + data.id, options);
    //.map(response => response );
  }

  public NavigateToRouteWithQueryString(routeName: string, queryParams?: NavigationExtras) {
    if (queryParams == undefined || queryParams == null)
      this.router.navigate([routeName]);
    else
      this.router.navigate([routeName], queryParams);
  }

  public NavigateToRoute(routeName: string, params?: NavigationExtras) {
    if (params == undefined || params == null)
      this.router.navigate([routeName]);
    else
      this.router.navigate([routeName, params]);
  }
  public JsonParseData(data: any) {
    let result = data;
    try {
      result = JSON.parse(data);
    } catch (e) {
      console.log(e)
    }
    return result;
  }

  public formatDate(d: Date, format?: string) {
    if (format == null || format == "")
      format = "DD-MMM-YYYY";
    if (d == null || d.toString() == "") {
      return "";
    }
    else {
      if (new Date(d).getFullYear().toString() == "1900")
        return "";
      const date = moment(d);
      let dateInFormat = date.format(format);
      return dateInFormat;
    }
  }


  public IsUserLogIn() {
    var token = localStorage.getItem("auth_token");
    if (token != null && token != '') {
      this.loginCheck = true;
    }
    else {
      this.loginCheck = false;
    }
    return this.loginCheck;

  }

  public UserHome() {
    this.NavigateToRoute(this.apiUrls.User.UserDefault);
  }
  public loadScript(url: string) {
    const body = <HTMLDivElement>document.body;
    const script = document.createElement('script');
    document.getElementsByTagName('body')
    script.innerHTML = '';
    script.src = url;
    //script.async = false;
    //script.defer = true;
    body.appendChild(script);
  }

  numberOnly(event: KeyboardEvent): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }

  public charOnly(event: KeyboardEvent): boolean {

    const charCode = (event.which) ? event.which : event.keyCode;
    if ((charCode > 47 && charCode < 58) || ((charCode > 32 && charCode <= 47)) || ((charCode >= 58 && charCode <= 64)) || ((charCode >= 91 && charCode <= 96)) || ((charCode >= 123 && charCode <= 126))) {
      return false;
    }

    return true;
  }
}
