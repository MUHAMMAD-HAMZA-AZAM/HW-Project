import { Injectable, Inject } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { apiUrls } from '../ApiRoutes/ApiRoutes';
import { Deferred } from 'ts-deferred';
import { NgxSpinnerService } from "ngx-spinner";
import { DomSanitizer } from '@angular/platform-browser';
import * as moment from 'moment-mini-ts'

import { Router } from '@angular/router';
@Injectable({
  providedIn:'root'
})
export class CommonService {
  public baseUrl: any;
  private headers = new Headers({ 'Content-Type': 'application/json' });

  constructor(
      private sanitizer: DomSanitizer ,
    private http: Http, public Loader: NgxSpinnerService, private router: Router) {
    this.baseUrl = localStorage.getItem("baseUrl");
  }
 
  public apiRoutes = apiUrls;
  get(api: string) {
    
    let headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8', 'Access-Control-Allow-Origin': '*', 'Access-Control-Expose-Headers': '*', 'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS', 'Access-Control-Allow-Headers': '*' });
    var token = localStorage.getItem('auth_token');
    if (token == null) {
      token="";
    }
    // console.log(token);
    headers.append('Authorization', token);

    let options = new RequestOptions({ headers: headers });
    return this.http
      .get(this.baseUrl + api, options);
    // .map(response => response.json());
  }

  post(api: string, data: any) {
    let headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8', 'Access-Control-Allow-Origin': '*', 'Access-Control-Expose-Headers': '*', 'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS', 'Access-Control-Allow-Headers': '*' });
    headers.append('Authorization', localStorage.getItem('auth_token'));
    let options = new RequestOptions({ headers: headers });
    return this.http
      .post(this.baseUrl + api, JSON.stringify(data), options);
    //.map(response => response.json());
  }

  save(api: string, data: any) {

    let headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8', 'Access-Control-Allow-Origin': '*', 'Access-Control-Expose-Headers': '*', 'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS', 'Access-Control-Allow-Headers': '*' });
    headers.append('Authorization', localStorage.getItem('auth_token'));
    let options = new RequestOptions({ headers: headers });
    if (data.id > 0) {
      return this.http
        .put(this.baseUrl + api + "/" + data.id, JSON.stringify(data), options);
        //.map(response => response.json());
    }
    else {
      return this.http
        .post(this.baseUrl + api, JSON.stringify(data), options);
        //.map(response => response.json());
    }
  }

  public SaveAndNotify(apiUrl, data, navigateUrl?, navigateParams?, messageKey?) {
    let d = new Deferred<any>();
    let p = d.promise;
    this.Loader.show();
    this.post(apiUrl, data).subscribe(data => {

      var result = JSON.parse(data.json());
      //if (result.status == StatusCode.OK) {
      //  messageKey = (messageKey != null && messageKey != undefined) ? messageKey : "Messages.Save";
      //  this.Notification.success(this.GetTranslation(messageKey));
      //  if (navigateUrl != null && navigateUrl != undefined)
      //    this.NavigateToRoute(navigateUrl, navigateParams);
      //}
      //else {
      //  this.Notification.error(this.GetTranslation("Messages.Error"));
      //}
      this.Loader.hide();
      d.resolve(result);
    }, error => {
      this.Loader.hide();
      //this.Notification.error(this.GetTranslation("Messages.Error")); console.log("erre", error);
    });
    return p;
  }

  public GetData(apiUrl, showLoader?) {
    debugger;
    if (apiUrl == 'undefined' || apiUrl == null || apiUrl == "")
      return;
    let d = new Deferred<any>();
    let p = d.promise;
    if (showLoader)
      this.Loader.show();
    this.get(apiUrl).subscribe(data => {

      var result = JSON.parse(data.json());

      d.resolve(result);
      if (showLoader)
        this.Loader.hide();
    }, error => {
      console.log("erre", error)
      this.Loader.hide();
    });
    return p;
  }

  public JsonParseData(data: any) {
    let result = data;
    try {
      result = JSON.parse(data);
    } catch (e) {
      console.log(e);
    }
    return result;
  }

  public PostData(apiUrl, data, showLoader?) {
    if (apiUrl == 'undefined' || apiUrl == null || apiUrl == "")
      return;
    let d = new Deferred<any>();
    let p = d.promise;
    if (showLoader)
      this.Loader.show();
    this.post(apiUrl, data).subscribe(data => {
      let result: any;
      try {
        if (typeof data == "string") {
          result = this.JsonParseData(data);
        }
        else {
          result = data;
        }
         
        
      } catch (e) {

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

  public SaveData(apiUrl, data, navigateUrl?, queryParams?, messageKey?) {
    let d = new Deferred<any>();
    let p = d.promise;
    this.Loader.show();
    this.PostData(apiUrl, data, true).then(data => {
      var result = data;
      //if (result.status == StatusCode.OK) {
      //  messageKey = (messageKey != null && messageKey != undefined) ? messageKey : "Messages.Save";
      //  this.Notification.success(this.GetTranslation(messageKey));
      //  if (navigateUrl != null && navigateUrl != undefined)
      //    this.NavigateToRouteWithQueryString(navigateUrl, queryParams);
      //}
      //else {
      //  this.Notification.error(this.GetTranslation("Messages.Error"));
      //}
      this.Loader.hide();
      d.resolve(result);
    });
    return p;
  }

  update(api: string, id: number, data: any) {
    let headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8', 'Access-Control-Allow-Origin': '*', 'Access-Control-Expose-Headers': '*', 'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS', 'Access-Control-Allow-Headers': '*' });
    headers.append('Authorization', localStorage.getItem('auth_token'));
    let options = new RequestOptions({ headers: headers });
    return this.http
      .put(this.baseUrl + api + "/" + id, JSON.stringify(data), options);
      //.map(response => response.json());
  }

  delete(api: string, data: any) {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    headers.append('Authorization', localStorage.getItem('auth_token'));

    let options = new RequestOptions({ headers: headers, body: data });
    return this.http
      .delete(this.baseUrl + api + "/" + data.id, options);
      //.map(response => response.json());
  }
  public NavigateToRouteWithQueryString(routeName: string, queryParams?) {
    if (queryParams == undefined || queryParams == null)
      this.router.navigate([routeName]);
    else
      this.router.navigate([routeName], queryParams);
  }

  public NavigateToRoute(routeName: string, params?) {
    
    if (params == undefined || params == null)
      this.router.navigate([routeName]);
    else
      this.router.navigate([routeName, params]);
  }
  transform(base64Image) {
    return this.sanitizer.bypassSecurityTrustResourceUrl('data:image/png;base64,' + base64Image);
  }
  public formatDate(d, format?) {
    if (format == null || format == "")
      format = "DD-MMM-YYYY";
    if (d == null || d == "") {
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

  charOnly(event): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if ((charCode > 47 && charCode < 58)) {
      return false;
    }
    return true;
  }

  numberOnly(event): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;

  }
  omit_special_char(event) {
    var k;
    k = event.charCode; 
    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k <= 48 && k >= 57));
  }
  public convertToSlug(Text) {
    var toLower = Text.toLowerCase();
    let slug = toLower.replace(/[^a-z0-9 -]/g, '').replace(/\s+/g, '-').replace(/-+/g, '-')
    return slug;
  }
  public loadScript(url: string) {
    const body = <HTMLDivElement>document.body;
    const script = document.createElement('script');
    script.innerHTML = '';
    script.src = url;
    //script.async = false;
    //script.defer = true;
    body.appendChild(script);
  }

}
