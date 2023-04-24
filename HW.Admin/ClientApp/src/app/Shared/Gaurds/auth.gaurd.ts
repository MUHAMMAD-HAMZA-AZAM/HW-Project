import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, CanLoad } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { loginsecurity } from '../Enums/enums';

@Injectable()
export class AuthGuardService implements CanActivate {
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    var token = localStorage.getItem("auth_token");
    var decodedtoken = this.jwtHelperService.decodeToken(token);
    if (token == null || token == '' || decodedtoken.Role != loginsecurity.Role) {
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
