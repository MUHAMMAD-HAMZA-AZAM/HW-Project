import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../shared/HttpClient/_http';
import { ActivatedRoute, Params } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-success-message',
  templateUrl: './success-message.component.html',
  styleUrls: ['./success-message.component.css']
})
export class SuccessMessageComponent implements OnInit {
  public ResponseCode: string="";
  //public response: ResponseVm = {} as ResponseVm,
  public Billrefrence: number=0;
  public pp_ResponseMessage: string="";
  public role:any;
  public customerId: number=0;
  public jwtHelperService: JwtHelperService = new JwtHelperService();

  constructor(
    private route: ActivatedRoute,public common: CommonService) { }

  ngOnInit() {

    var token = localStorage.getItem("auth_token");
    var decodedtoken =token!=null ? this.jwtHelperService.decodeToken(token):"";
    console.log(decodedtoken);
    this.role = decodedtoken.Role;
    this.customerId = decodedtoken.Id;

    this.route.queryParams.subscribe((params: Params) => {
      
      this.ResponseCode = params['pp_ResponseCode'];
      this.Billrefrence = params['pp_BillReference'];
      this.pp_ResponseMessage = params['pp_ResponseMessage'];
      if (this.ResponseCode) {
      this.redirectPage();
      }
    });
  }
  redirectPage() {
    ;
    if (this.customerId == this.Billrefrence && this.role=='Customer') {
      setTimeout(() => {
        this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.topup, { queryParams: { responseCode: this.ResponseCode, responseMessage: this.pp_ResponseMessage } })
      }, 3000)
    }
    else if (this.customerId == this.Billrefrence && this.role == 'Tradesman') {
      setTimeout(() => {
        this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Tradesman.topup, { queryParams: { responseCode: this.ResponseCode, responseMessage: this.pp_ResponseMessage } })
      }, 3000)
    }
    else if (this.customerId == this.Billrefrence && this.role == 'Organization') {
      setTimeout(() => {
        this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Tradesman.topup, { queryParams: { responseCode: this.ResponseCode, responseMessage: this.pp_ResponseMessage } })
      }, 3000)
    }
    else {
      setTimeout(() => {
        this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.Payments, { queryParams: { responseCode: this.ResponseCode, responseMessage: this.pp_ResponseMessage } })
      }, 3000)
    }
    }

  //public redirectURL() {
    
  //  if (this.ResponseCode == '000' && this.Billrefrence != "" && this.Billrefrence != null) {
  //  }
  //}

//  public GoToHome() {
//    if (this.role == "Customer") {
//      this.common.NavigateToRoute(this.common.apiUrls.User.InProgressJobList);
//    }
//    else {
//      this.common.NavigateToRoute(this.common.apiUrls.Tradesman.LiveLeads);
//    }
//  }
}
