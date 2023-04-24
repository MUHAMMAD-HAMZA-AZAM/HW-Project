import { animation } from '@angular/animations';
import { HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { HttpResponse } from 'aws-sdk/lib/http_response';
import { ToastrService } from 'ngx-toastr';
import { PaymentMethod } from '../../Shared/Enums/enum';
import { IOrderDetail, IPageSeoVM } from '../../Shared/Enums/Interface';
import { CommonService } from '../../Shared/HttpClient/HttpClient';
import { metaTagsService } from '../../Shared/HttpClient/seo-dynamic.service';
@Component({
  selector: 'app-order-payment',
  templateUrl: './order-payment.component.html',
  styleUrls: ['./order-payment.component.css']
})
export class OrderPaymentComponent implements OnInit {
  public orderId: string="";
  public orderDetail: IOrderDetail;
  public paymentmethod = PaymentMethod;
  public ResponseCode:string="";
  public pp_ResponseMessage:string="";
  public Billrefrence:string="";
  public isPaymentStatus: boolean = false;
  constructor(public _httpService: CommonService, private _metaService: metaTagsService, private route: ActivatedRoute, public toastr: ToastrService, public _modelService: NgbModal)
    {
        this.orderDetail = {} as IOrderDetail;
    }

  ngOnInit(): void {
    let data = this.route.snapshot.paramMap.get('id');
    this.orderId = data != null ? data : "";
    debugger;
    ////let ResponseCode1 = '000';
    //  if (this.ResponseCode == '000') {
    //     this.orderId = this.Billrefrence;
    //   // this.orderId = '50660';
    //    this._httpService.get(this._httpService.apiUrls.Promotion.
    //      AddUpdateAccountReceivable + "?orderId=" + this.orderId + "&paymentMethodId=" + this.paymentmethod.JazzCash + "&userName=" + this._httpService.decodedToken().sub + "&isPaymentRecived=" + true)
    //      .subscribe(response => {
    //        if ((<any>response).status == HttpStatusCode.Ok) {
    //          this._httpService.NavigateToRoute(this._httpService.apiRoutes.User.OrderPaymentReceipt);
    //        }
    //      });
    //  }
    
    this.route.queryParams.subscribe((params: Params) => {
      this.ResponseCode = params['responseCode'];
      this.pp_ResponseMessage = params['responseMessage'];
      this.Billrefrence = params['pp_BillReference'];
      
        if (this.ResponseCode == '000') {
           this.orderId = this.Billrefrence;
          this._httpService.get(this._httpService.apiUrls.Promotion.
            AddUpdateAccountReceivable + "?orderId=" + this.orderId + "&paymentMethodId=" + this.paymentmethod.JazzCash + "&userName=" + this._httpService.decodedToken().sub + "&isPaymentRecived=" + true)
            .subscribe(response => {
              if ((<any>response).status == HttpStatusCode.Ok) {
                this._httpService.NavigateToRoute(this._httpService.apiRoutes.User.OrderPaymentReceipt);
              }
            });
        }
        else {
          this.isPaymentStatus = true;
          setTimeout(() => {
            this.isPaymentStatus = false;
          }, 8000);
        }
    });

    this.getCustomerById();
    this.bindMetaTags();
  }
  getCustomerById() {
    this._httpService.GetData(this._httpService.apiUrls.Supplier.Product.GetOrderDetailById + "?orderId=" + this.orderId)
      .then(response => {
        this.orderDetail = (<any>response).resultData[0];
        console.log(this.orderDetail);
      });
  }
  payNow(confimrdOrderContent: TemplateRef<any>) {
    this._modelService.open(confimrdOrderContent);
  }
  confirmPaymnet() {
    this._modelService.dismissAll();
    this._httpService.get(this._httpService.apiUrls.Promotion.
      AddUpdateAccountReceivable + "?orderId=" + this.orderId + "&paymentMethodId=" + this.paymentmethod.CashPayment + "&userName=" + this._httpService.decodedToken().sub + "&isPaymentRecived=" + false)
      .subscribe(response => {
        if ((<any>response).status == HttpStatusCode.Ok) {
          this._httpService.NavigateToRoute(this._httpService.apiRoutes.User.OrderPaymentReceipt);
        }
      });
  }

  proceedJazzCash() {
    var origin = window.location.origin;
    var d = new Date();
    var n = d.toString();
    var datTime = new Date(n).getTime() / 1000;
    var PaymentRefNo = "OP" + datTime;
    window.location.href = origin + "/Message/JazzCashPayment?amount=" + (this.orderDetail.totalAmount * 100) + "&paymentRefNo=" + PaymentRefNo + "&billReference=" + this.orderId + "&transactionType=MWALLET";
  }
  public bindMetaTags() {
    this._httpService.get(this._httpService.apiUrls.CMS.GetSeoPageById + "?pageId=29").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description, res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }
}
