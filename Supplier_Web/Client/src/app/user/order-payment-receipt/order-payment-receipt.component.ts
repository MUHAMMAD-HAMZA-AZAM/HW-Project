import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { bool } from 'aws-sdk/clients/redshiftdata';
import { IPageSeoVM, IPayementHistory } from '../../Shared/Enums/Interface';
import { PaymentStatus } from '../../Shared/Enums/enum';
import { CommonService } from '../../Shared/HttpClient/HttpClient';
import { metaTagsService } from '../../Shared/HttpClient/seo-dynamic.service';

@Component({
  selector: 'app-order-payment-receipt',
  templateUrl: './order-payment-receipt.component.html',
  styleUrls: ['./order-payment-receipt.component.css']
})
export class OrderPaymentReceiptComponent implements OnInit {
  public paymentList: IPayementHistory[] = [];
  public dataNotFound: boolean = false;
  public orderPaymentStatus  = PaymentStatus;
  jwtHelperService: JwtHelperService = new JwtHelperService();

  constructor(public service: CommonService, private _metaService: metaTagsService,) { }

  ngOnInit(): void {
    this.getPaymentReceipt();
    this.bindMetaTags();
  }

  getPaymentReceipt() {
    let token = localStorage.getItem('web_auth_token');
    let decodedToken = token != null ? this.jwtHelperService.decodeToken(token):"";
          let obj = {
            CustomerId: decodedToken.Id
    }
    this.service.PostData(this.service.apiUrls.Supplier.Product.GetPaymentHistory, obj, true).then(response => {
      let res = (<any>response).resultData;
      console.log(res);
     if(res){
       this.paymentList = res;
     }
     else{
       this.dataNotFound = true;
     }
    });
  }

   //------------- Show Customer OrderedProduct Details
  public orderDetails(orderId:number) : void {
    this.service.NavigateToRouteWithQueryString(this.service.apiRoutes.User.OrderDetails,{queryParams :{orderId:orderId}});
  }
  public bindMetaTags() {
    this.service.get(this.service.apiUrls.CMS.GetSeoPageById + "?pageId=30").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description, res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }

}
