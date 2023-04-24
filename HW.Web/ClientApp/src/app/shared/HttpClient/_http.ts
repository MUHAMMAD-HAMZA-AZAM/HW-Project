import { Injectable, Inject } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Deferred } from 'ts-deferred';
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';
import * as moment from 'moment-mini-ts'
import { Router } from '@angular/router';
import { apiRoute, pagesUrls } from '../ApiRoutes/ApiRoutes';
import { ApiUrls } from '../ApiRoutes/ApiUrls';
import { NotificationService } from '../notifications/toastr-notification.service';
import { httpStatus } from '../Enums/enums';
import { DomSanitizer } from '@angular/platform-browser';
@Injectable()
export class CommonService {
  public pagesUrl = pagesUrls;
  public apiUrls = ApiUrls;
  public loginCheck: boolean = false;
  public baseUrl: string;
  private headers = new Headers({ 'Content-Type': 'application/json' });
  constructor(
    private http: Http,
    public Loader: Ng4LoadingSpinnerService,
    private router: Router,
    public Notification: NotificationService
    , private sanitizer: DomSanitizer
  ) {
    this.baseUrl = localStorage.getItem("baseUrl");
  }

  public apiRoutes = apiRoute;

  get(api: string) {
    let headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8', 'Access-Control-Allow-Origin': '*', 'Access-Control-Expose-Headers': '*', 'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS', 'Access-Control-Allow-Headers': '*' });
    var token = localStorage.getItem('auth_token');
    if (token == null) {
      token = "";
    }
    // console.log(token);
    headers.append('Authorization', token);

    let options = new RequestOptions({ headers: headers });
    return this.http
      .get(this.baseUrl + api, options);
    //.map(response => response.json());
  }

  post(api: string, data: any) {
    debugger;
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
        .post(api, JSON.stringify(data), options);
      //.map(response => response.json());
    }
  }

  public SaveAndNotify(apiUrl, data, navigateUrl?, navigateParams?, messageKey?) {
    let d = new Deferred<any>();
    let p = d.promise;
    this.Loader.show();
    this.post(this.baseUrl + apiUrl, data).subscribe(data => {

      var result = JSON.parse(data.json());
      if (result.status == httpStatus.Ok) {
        messageKey = (messageKey != null && messageKey != undefined) ? messageKey : "Successfully Saved.";
        this.Notification.success(messageKey);
        if (navigateUrl != null && navigateUrl != undefined)
          this.NavigateToRoute(navigateUrl, navigateParams);
      }
      else {
        this.Notification.error("Some thing went wrong.");
      }
      this.Loader.hide();
      d.resolve(result);
    }, error => {
      this.Loader.hide();
      this.Notification.error("Some thing went wrong."); console.log("erre", error);
    });
    return p;
  }

  public GetData(apiUrl, showLoader?) {
    if (apiUrl == 'undefined' || apiUrl == null || apiUrl == "")
      return;
    let d = new Deferred<any>();
    let p = d.promise;
    if (showLoader)
      this.Loader.show();
    this.get(apiUrl).subscribe(data => {
      var result = data;
      d.resolve(result);
      if (showLoader)
        this.Loader.hide();
    }, error => {
      console.log("erre", error)
      this.Loader.hide();
    });
    return p;
  }
  transform(base64Image) {
    return this.sanitizer.bypassSecurityTrustResourceUrl('data:image/png;base64,' + base64Image);
  }

  public PostData(apiUrl, data, showLoader?) {
    if (apiUrl == 'undefined' || apiUrl == null || apiUrl == "")
      return;
    let d = new Deferred<any>();
    let p = d.promise;
    if (showLoader)
      this.Loader.show();
    debugger;
    this.post(apiUrl, data).subscribe(data => {
      var result = data;
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
    debugger
    if (queryParams == undefined || queryParams == null)
      this.router.navigate([routeName]);
    else
      this.router.navigate([routeName], queryParams);
  }

  public NavigateToRoute(routeName: string, params?) {
    debugger
    if (params == undefined || params == null)
      this.router.navigate([routeName]);
    else
      this.router.navigate([routeName, params]);
  }


  public formatDate(d, format?) {
    if (format == null || format == "")
      format = "DD/MMM/YYYY";
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

}
