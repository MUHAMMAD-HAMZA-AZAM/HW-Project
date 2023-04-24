import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, CanLoad } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CommonService } from '../HttpClient/HttpClient';

@Injectable()
export class AuthGuardSupplierService implements CanActivate {
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(public common: CommonService) { }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    var token = localStorage.getItem("web_auth_token");
    var decodedtoken =token!=null ? this.jwtHelperService.decodeToken(token) :"";
    if ((token == null || token == '') && (decodedtoken == null || decodedtoken == "")) {
      this.common.NavigateToRoute(this.common.apiRoutes.Login.login);
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
