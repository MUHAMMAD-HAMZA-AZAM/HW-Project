import { isPlatformBrowser } from '@angular/common';
import { Component, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { loginsecurity } from '../../Enums/enums';

@Component({
  selector: 'app-UserApplayout',
  templateUrl: './userApplayout.component.html',
})
export class UsersApplayoutComponent implements OnInit {
  public loginCheck: boolean = false;
  public jwtHelperService: JwtHelperService = new JwtHelperService();

  constructor(
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    if (isPlatformBrowser(this.platformId)) {
      window.scroll(0, 0);
    }
  }

  ngOnInit() {
    //var head = document.getElementsByTagName('head')[0];
    //var link = document.createElement('link');
    //link.id = 'customStyle';
    //link.rel = 'stylesheet';
    //link.type = 'text/css';
    //link.href = 'assets/css/custom-style.css';
    //link.media = 'all';
    //head.appendChild(link);
    this.IsUserLogIn();
  }

  public scrollTop(event: Event) {
    //window.scroll(0, 0);
  }

  public IsUserLogIn() {
    var token = localStorage.getItem("auth_token");
    var decodedtoken = token!=null ? this.jwtHelperService.decodeToken(token):"";
    if (token != null && token != '' ) {
      decodedtoken.Role == loginsecurity.CRole
      if (decodedtoken.Role == loginsecurity.CRole){
        this.loginCheck = true;
      }
    }
    else {
      this.loginCheck = false;
    }
  }
}
