import { isPlatformBrowser } from '@angular/common';
import { Component, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-mypackageslayout',
  templateUrl: './mypackageslayout.component.html',
  styleUrls: ['./mypackageslayout.component.css']
})
export class MypackageslayoutComponent implements OnInit {
  public roleType: any;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(@Inject(PLATFORM_ID) private platformId: Object) {
    if (isPlatformBrowser(this.platformId)) {
      window.scroll(0, 0);
    }}

  ngOnInit() {
    this.getUserRole();
  }
  //public scrollTop(event: Event) {
  //  window.scroll(0, 0);
  //}
  public getUserRole() {
    
    var token = localStorage.getItem("auth_token");
    var decodedtoken =token!=null ? this.jwtHelperService.decodeToken(token):"";
    
    if (decodedtoken.Role == 'Customer')
      this.roleType = 3;
    else if (decodedtoken.Role == 'Orgnization')
      this.roleType = 2
    else if (decodedtoken.Role == 'Tradesman')
      this.roleType = 1
    else this.roleType = 4
    console.log(decodedtoken);
    console.log(this.roleType);
  }

}
