import { Component, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { loginsecurity, httpStatus } from '../../Enums/enums';
import { CommonService } from '../../HttpClient/_http';
import { ResponseVm } from '../../../models/commonModels/commonModels';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-SupplierLayout',
  templateUrl: './supplierLayout.component.html',
})
export class SupplierLayoutComponent implements OnInit {
  public loginCheck: boolean = false;
  public Businesscheck: boolean = true;
  public responseVm: ResponseVm = {} as ResponseVm;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(public common: CommonService, @Inject(PLATFORM_ID) private platformId: Object) {
    if (isPlatformBrowser(this.platformId)) {
      window.scroll(0, 0);
    }
  }

  ngOnInit() {
    this.IsUserLogIn();
    this.IsBusinessDetails();

  }
  //public scrollTop(event: Event) {
  //  window.scroll(0, 0);
  //}
  public IsUserLogIn() {
    var token = localStorage.getItem("auth_token");
    var decodedtoken = token!=null ? this.jwtHelperService.decodeToken(token):"";
    if (token != null && token != '') {
      decodedtoken.Role == loginsecurity.CRole
      if (decodedtoken.Role == loginsecurity.CRole) {
        this.loginCheck = true;
      }
    }
    else {
      this.loginCheck = false;
    }
  }
  public IsBusinessDetails() {
    
    this.common.GetData(this.common.apiRoutes.Supplier.GetBusinessDetailsStatus, true).then(result => {
      
      this.responseVm = result ;
      if (this.responseVm.status == httpStatus.Ok) {
        this.Businesscheck = true;
      }
      else {
        this.Businesscheck = false;
      }
    });
  }

}
