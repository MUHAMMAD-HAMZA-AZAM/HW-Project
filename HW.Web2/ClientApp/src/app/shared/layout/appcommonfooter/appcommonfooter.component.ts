import { Component, Input, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CommonService } from '../../HttpClient/_http';

@Component({
  selector: 'app-appcommonfooter',
  templateUrl: './appcommonfooter.component.html',
  styleUrls: ['./appcommonfooter.component.css']
})
export class AppcommonfooterComponent implements OnInit {
  public loginCheck: boolean = false;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  public check: {
    T: { role: number, Show: boolean },
    O: { role: number, Show: boolean },
    U: { role: number, Show: boolean },
    S: { role: number, Show: boolean }
  } = {
      T: { role: 1, Show: true },
      O: { role: 2, Show: true },
      U: { role: 3, Show: true },
      S: { role: 4, Show: true }
    };
  @Input() customStyle: {} = {};
  constructor(public common: CommonService) { }

  ngOnInit() {
  }
  public IsUserLogIn() {
    var token = localStorage.getItem("auth_token");
    if (token != null && token != '') {
      this.loginCheck = true;
    }
    else {
      this.loginCheck = false;
    }
  }

  public PostAd() {
    
    if (this.common.loginCheck) {
      this.common.NavigateToRoute(this.common.apiUrls.Supplier.EditAd);
    }
    else {
      localStorage.setItem("Role", '4');
      localStorage.setItem("Show", 'true');
      this.common.NavigateToRoute(this.common.apiUrls.Login.customer);
    }
  }

  public PostJob() {
    
    if (this.common.loginCheck) {
      this.common.NavigateToRoute(this.common.apiUrls.User.Quotations.getQuotes1);
    }
    else {
      localStorage.setItem("Role", '3');
      localStorage.setItem("Show", 'true');
      this.common.NavigateToRoute(this.common.apiUrls.Login.customer);
    }
  }




}
