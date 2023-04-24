import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiUrls } from '../ApiRoutes/ApiUrls';
import { ApiRoutes } from '../ApiRoutes/ApiRoutes';
import { Deferred } from 'ts-deferred';
import { NgxSpinnerService } from 'ngx-spinner';
import { DomSanitizer } from '@angular/platform-browser';
import * as moment from 'moment-mini-ts';
import { ValidationMessages } from '../ApiRoutes/validation-message';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
@Injectable()
export class CommonService {
  public baseUrl: string |null="";
  public subject$ = new BehaviorSubject(false);
  AWSPATH: string = "https://homwork-bucket2.s3.ap-south-1.amazonaws.com/";
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  //private headers = new Headers({ 'Content-Type': 'application/json' });
  public webBaseUrl: string | null;
  constructor(
    private sanitizer: DomSanitizer,
    private http: HttpClient, public Loader: NgxSpinnerService, private router: Router) {
    this.baseUrl = 'http://172.16.1.36:15790/api/'//localStorage.getItem("baseUrl");
    //this.baseUrl = 'https://test2.hoomwork.com/gateway/'//localStorage.getItem("baseUrl");
    this.webBaseUrl = localStorage.getItem("webBaseUrl");
  }

  public apiRoutes = ApiRoutes;
  public apiUrls = ApiUrls;
  public validationMessage = ValidationMessages;
  public loginCheck: boolean = false;
  decodedToken(): any {
    let data=localStorage.getItem("web_auth_token");
    let token = data!=null ? this.jwtHelperService.decodeToken(data): "";
    return token;
  }
  get(api: string) {
    //const httpOptions = {
    //  headers: new HttpHeaders({
    //    'Content-Type': 'application/json'
    //  })
    //};
    var token = localStorage.getItem('web_auth_token');
    if (token == null) {
      token = "";
    }
    let headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8', 'Access-Control-Allow-Origin': '*', 'Access-Control-Expose-Headers': '*', 'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS', 'Access-Control-Allow-Headers': '*', 'Authorization': token  });
    // console.log(token);
    //headers.set('Authorization', token);

    let options = { headers: headers };
    return this.http
      .get(this.baseUrl + api, options)
      .pipe(map(response => {
        if (typeof response == "string") {
          return this.JsonParseData(response);
        }
        else {
          return response;
        }
      }));
    // .map(response => response.json());
  }

  post(api: string, data: any, toWeb?: boolean) {
    let Url: any;
    if (toWeb)
      Url = this.webBaseUrl;
    else
      Url = this.baseUrl;
    var token = localStorage.getItem('web_auth_token');
    if (token == null) {
      token = "";
    }
    let headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8', 'Access-Control-Allow-Origin': '*', 'Access-Control-Expose-Headers': '*', 'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS', 'Access-Control-Allow-Headers': '*', 'Authorization': token  });
    //headers.append('Authorization', String(localStorage.getItem('web_auth_token')));
    let options = { headers: headers };
    return this.http
      .post(Url + api, JSON.stringify(data), options)
      .pipe(map(response => {
        if (typeof response == "string") {
          return this.JsonParseData(response);
        }
        else {
          return response;
        }
      }));
    //.map(response => response.json());

  }
  public GetData1(apiUrl: string, showLoader?: boolean) {
    let d = new Deferred<any>();
    let p = d.promise;
    if (apiUrl == 'undefined' || apiUrl == null || apiUrl == "")
    {
      return p;
    }
    if (showLoader)
      this.Loader.show();
    this.get(apiUrl).subscribe(data => {
      var result: any;
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
  public GetData(apiUrl: any, showLoader?: any) {
    let d = new Deferred<any>();
    let p = d.promise;
    if (apiUrl == 'undefined' || apiUrl == null || apiUrl == "") {
      return p;
    }
    if (showLoader)
      this.Loader.show();
    this.get(apiUrl).subscribe(data => {
      var result = JSON.parse(JSON.stringify(data));
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

  public PostData(apiUrl: any, data: any, showLoader?: boolean, toWeb?: boolean) {
        let d = new Deferred<any>();
        let p = d.promise;
    
    if (apiUrl == 'undefined' || apiUrl == null || apiUrl == "") {
      return p;
    }
    if (showLoader)
      this.Loader.show();
    this.post(apiUrl, data, toWeb).subscribe(data => {
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
  public NavigateToRouteWithQueryString(routeName: string, queryParams?: any) {
    if (queryParams == undefined || queryParams == null)
      this.router.navigate([routeName]);
    else
      this.router.navigate([routeName], queryParams);
  }

  public NavigateToRoute(routeName: string, params?: any) {

    if (params == undefined || params == null)
      this.router.navigate([routeName]);
    else
      this.router.navigate([routeName, params]);
  }
  transform(base64Image: string) {
    return this.sanitizer.bypassSecurityTrustResourceUrl('data:image/png;base64,' + base64Image);
  }
  public formatDate(d: any, format?: any) {
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
  numberOnly(event: any): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;

  }

  charOnly(event:any): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if ((charCode > 47 && charCode < 58)) {
      return false;
    }
    return true;
  }

  omit_special_char(event: any):boolean {
    var k;
    k = event.charCode;
    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k <= 48 && k >= 57));
  }
  public convertToSlug(Text: any) {
    var toLower = Text.toLowerCase();
    let slug = toLower.replace(/[^a-z0-9 -]/g, '').replace(/\s+/g, '-').replace(/-+/g, '-')
    return slug;
  }
  public checkInputValidity(value:any) {
    const pattern = /^[0-9]{1,5}$/;
    return pattern.test(value);
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
  public IsUserLogIn() {
    var token = localStorage.getItem("web_auth_token");
    if (token != null && token != '') {
      this.loginCheck = true;
    }
    else {
      this.loginCheck = false;
    }
    return this.loginCheck;

  }
}
