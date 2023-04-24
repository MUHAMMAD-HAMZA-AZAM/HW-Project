import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, CanLoad } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { loginsecurity } from '../Enums/enums';

@Injectable()
export class AuthGuardCustomerService implements CanActivate {
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    var token = localStorage.getItem("auth_token");
    var decodedtoken = this.jwtHelperService.decodeToken(token);
    if (token == null || token == '' || decodedtoken.Role != loginsecurity.CRole) {
      this.router.navigate(['/login']);
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

export class AuthGuardTradesmanService implements CanActivate {
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    var token = localStorage.getItem("auth_token");
    var decodedtoken = this.jwtHelperService.decodeToken(token);
    if (token == null || token == '' || decodedtoken.Role != loginsecurity.TRole || decodedtoken.Role != loginsecurity.ORole) {
      this.router.navigate(['/']);
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

export class AuthGuardSupplierService implements CanActivate {
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    var token = localStorage.getItem("auth_token");
    var decodedtoken = this.jwtHelperService.decodeToken(token);
    if (token == null || token == '' || decodedtoken.Role != loginsecurity.SRole) {
      this.router.navigate(['/']);
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
