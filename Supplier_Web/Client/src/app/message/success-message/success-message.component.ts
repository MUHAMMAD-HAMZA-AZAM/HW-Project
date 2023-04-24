import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { IPageSeoVM } from '../../Shared/Enums/Interface';
import { CommonService } from '../../Shared/HttpClient/HttpClient';
import { metaTagsService } from '../../Shared/HttpClient/seo-dynamic.service';

@Component({
  selector: 'app-success-message',
  templateUrl: './success-message.component.html',
  styleUrls: ['./success-message.component.css']
})
export class SuccessMessageComponent implements OnInit {
  public ResponseCode: string="";
  //public response: ResponseVm = new ResponseVm(),
  public Billrefrence:string='';
  public pp_ResponseMessage:string='';
  public role:string="";
  public customerId:number;
  public jwtHelperService: JwtHelperService = new JwtHelperService();

  constructor(
    private route: ActivatedRoute, public common: CommonService, private _metaService: metaTagsService,) { }

  ngOnInit() {

    var token = localStorage.getItem("auth_token");
    var decodedtoken = token!=null ? this.jwtHelperService.decodeToken(token):"";
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
    this.bindMetaTags();
  }
  redirectPage() {

    setTimeout(() => {
      this.common.NavigateToRouteWithQueryString(this.common.apiRoutes.User.OrderPayment + "/" + this.Billrefrence, { queryParams: { responseCode: this.ResponseCode, responseMessage: this.pp_ResponseMessage, pp_BillReference: this.Billrefrence } })
      }, 3000)

    //if (this.customerId == this.Billrefrence && this.role == 'Customer') {
    //  setTimeout(() => {
    //    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.topup, { queryParams: { responseCode: this.ResponseCode, responseMessage: this.pp_ResponseMessage } })
    //  }, 3000)
    //}
    //else if (this.customerId == this.Billrefrence && this.role == 'Tradesman') {
    //  setTimeout(() => {
    //    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Tradesman.topup, { queryParams: { responseCode: this.ResponseCode, responseMessage: this.pp_ResponseMessage } })
    //  }, 3000)
    //}
    //else {
    //  setTimeout(() => {
    //    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.Payments, { queryParams: { responseCode: this.ResponseCode, responseMessage: this.pp_ResponseMessage } })
    //  }, 3000)
    //}
  }
  public bindMetaTags() {
    this.common.get(this.common.apiUrls.CMS.GetSeoPageById + "?pageId=20").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description, res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }
}
