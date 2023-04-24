import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CommonService } from '../../Shared/HttpClient/_http';
import { FormBuilder, FormGroup } from '@angular/forms';
import { IPayementHistoryDetail } from '../../Shared/Enums/Interface';

@Component({
  selector: 'app-payment-history',
  templateUrl: './payment-history.component.html',
  styleUrls: ['./payment-history.component.css']
})
export class PaymentHistoryComponent implements OnInit {
  jwtHelperService: JwtHelperService = new JwtHelperService();
  public paymentDetailList: IPayementHistoryDetail[] = [];
  public pageNumber: number = 1;
  public decodedToken: any="";
  public userId: string="";
  public dataNotfond: boolean=false;
  public pageSize: number = 50;
  public noOfRecords:number = 0;
  constructor(public service: CommonService, public modelService: NgbModal) { }

  ngOnInit(): void {
    this.getPaymentHistory(); 
  }

  getPaymentHistory() {
    var data = localStorage.getItem('auth_token');
    this.decodedToken = data != null ? this.jwtHelperService.decodeToken(data) : this.decodedToken;
    debugger
    this.service.GetData(this.service.apiUrls.Supplier.Payment.GetPaymentDetail + "?supplierId=" + this.decodedToken.Id).then(response => {
      this.paymentDetailList = (<any>response).resultData;
if( this.paymentDetailList){
      console.log(this.paymentDetailList );
      this.noOfRecords = this.paymentDetailList[0].noOfRecords;
    this.dataNotfond=false;
}
else{
this.dataNotfond=true;
}
    })
  }

  getPostByPage(page:any) {
    this.pageNumber = page;
    this.getPaymentHistory();
  }

ShowDetails(orderId: number){
    this.service.NavigateToRouteWithQueryString(this.service.apiRoutes.Payment.PaymentHistoryDetails, {
      queryParams: {
        "orderId": orderId,
      }
    });
}
}
