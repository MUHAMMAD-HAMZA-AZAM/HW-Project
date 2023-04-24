import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CommonService } from '../../Shared/HttpClient/_http';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { IPayementHistoryDetail } from '../../Shared/Enums/Interface';


@Component({
  selector: 'app-payment-history-details',
  templateUrl: './payment-history-details.component.html',
  styleUrls: ['./payment-history-details.component.css']
})
export class PaymentHistoryDetailsComponent implements OnInit {
  jwtHelperService: JwtHelperService = new JwtHelperService();
  //public paymentList = [];
  public paymentDetailList: IPayementHistoryDetail[] = [];
  public pageNumber = 1;
  public decodedToken: any;
  public pageSize = 50;
  public noOfRecords = 0;
  public orderId: number = 0;
  constructor(public service: CommonService, public modelService: NgbModal,private route: ActivatedRoute) { }

  ngOnInit(): void {
 this.route.queryParams.subscribe((param: Params) => {
      this.orderId = param['orderId'];
      this.getPaymentHistoryDetails();
    });
  }
  getPaymentHistoryDetails() {
    var token = localStorage.getItem('auth_token');
    this.decodedToken = token != null ? this.jwtHelperService.decodeToken(token) : "";
    this.service.GetData(this.service.apiUrls.Supplier.Payment.GetPaymentDetail + "?supplierId=" + this.decodedToken.Id + "&orderId=" + this.orderId).then(response => {
      this.paymentDetailList = (<any>response).resultData;
      this.noOfRecords = this.paymentDetailList[0].noOfRecords;
    })
  }
  getPostByPage(page: number) {
    this.pageNumber = page;
    this.getPaymentHistoryDetails();
  }
}
