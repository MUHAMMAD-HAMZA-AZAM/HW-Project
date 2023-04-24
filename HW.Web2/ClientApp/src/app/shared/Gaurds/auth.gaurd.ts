import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, CanLoad } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { loginsecurity } from '../Enums/enums';
import { CommonService } from '../HttpClient/_http';

@Injectable()
export class AuthGuardCustomerService implements CanActivate {
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(private router: Router, public common: CommonService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    var token = localStorage.getItem("auth_token");
    var decodedtoken =token!=null ? this.jwtHelperService.decodeToken(token):"";
    if (token == null || token == '' || decodedtoken.Role != loginsecurity.CRole) {
      localStorage.setItem("Role", '3');
      localStorage.setItem("Show", 'true');
      this.common.NavigateToRoute(this.common.apiUrls.Login.customer);
      return false;
    }
    else {
      return true;
    }
  }
  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.canActivate(route, state);
  }
}

@Injectable()
export class AuthGuardTradesmanService implements CanActivate {
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(private router: Router, public common: CommonService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    var token = localStorage.getItem("auth_token");
    var decodedtoken =token!=null ? this.jwtHelperService.decodeToken(token):"";
    
    if (token == null || token == '' || !(decodedtoken.Role != loginsecurity.TRole || decodedtoken.Role != loginsecurity.ORole)) {
      localStorage.setItem("Role", '1');
      localStorage.setItem("Show", 'true');
      this.common.NavigateToRoute(this.common.apiUrls.Login.tradesman);
      return false;
    }
    else {
      return true;
    }
  }
  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.canActivate(route, state);
  }
}

@Injectable()
export class AuthGuardSupplierService implements CanActivate {
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(private router: Router, public common: CommonService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    var token = localStorage.getItem("auth_token");
    var decodedtoken =token!=null ? this.jwtHelperService.decodeToken(token):"";
    if (token == null || token == '' || decodedtoken.Role != loginsecurity.SRole) {
      localStorage.setItem("Role", '4');
      localStorage.setItem("Show", 'true');
      this.common.NavigateToRoute(this.common.apiUrls.Login.supplier);
      return false;
    }
    else {
      return true;
    }
  }
  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.canActivate(route, state);
  }
}
