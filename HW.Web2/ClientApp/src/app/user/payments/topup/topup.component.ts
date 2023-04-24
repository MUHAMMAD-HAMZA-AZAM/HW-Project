import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { httpStatus } from '../../../shared/Enums/enums';
import { CommonService } from '../../../shared/HttpClient/_http';

@Component({
  selector: 'app-topup',
  templateUrl: './topup.component.html',
  styleUrls: ['./topup.component.css']
})
export class TopupComponent implements OnInit {
  public walletAmount: string="";
  public amount: number=0;
  public isEmptyAmount: boolean = false;
  public isjazzCash: boolean = false;
  public customerId: string="";
  public isProceedPayment: boolean = false;
  public appValForm: FormGroup;
  public submittedApplicationForm: boolean = false;
  public isPendingRequest: boolean = false;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  public ResponseCode: string="";
  public pp_ResponseMessage: string="";
  public isPaymentStatus: boolean = false;
  public successMessage: boolean = false;
  public transactionType: string = "MWALLET";

  constructor(private common: CommonService, private formBuilder: FormBuilder, private route: ActivatedRoute) {
    this.appValForm = {} as FormGroup;
  }

  ngOnInit() {
    this.route.queryParams.subscribe((params: Params) => {
      this.ResponseCode = params['responseCode'];
      this.pp_ResponseMessage = params['responseMessage'];
      if (this.ResponseCode) {
        if (this.ResponseCode == '000') {
          this.successMessage = true;
          setTimeout(() => {
            this.successMessage = false;
          }, 8000);
        }
        else {
          this.isPaymentStatus = true;
          setTimeout(() => {
            this.isPaymentStatus = false;
          }, 8000);
        }
      }
    });

    if (this.common.IsUserLogIn()) {
      ;
      var token = localStorage.getItem("auth_token");
      var decode = token!=null ? this.jwtHelperService.decodeToken(token):"";
      this.customerId = decode.Id;
    }

  }
  jazzCash(type: string) {
    this.isjazzCash = true;
    this.transactionType = type;
  }

  proceedPayment() {
    ;
    if (this.amount) {
      var origin = window.location.origin;
      var d = new Date();
      var n = d.toString();
      var datTime = new Date(n).getTime() / 1000;
      var PaymentRefNo = "CT" + datTime;
      window.location.href = origin + "/Message/JazzCashPayment?amount=" + this.amount * 100 + "&paymentRefNo=" + PaymentRefNo + "&billReference=" + this.customerId + "&transactionType=" + this.transactionType;
    }
    else {
      this.isEmptyAmount = true;
      setTimeout(() => {
        this.isEmptyAmount = false;
      },4000)
    }
  }
  public AllowNonZeroIntegers(event: KeyboardEvent): boolean {

    //var val = e.keyCode;
    var val = event.keyCode;
    // var target = event.target ? event.target : event.srcElement;
    if (val == 48 && (<HTMLInputElement>event.target).value == "" || val == 101 || val == 45 || val == 46 || ((val >= 65 && val <= 90)) || ((val >= 97 && val <= 122))) {
      return false;
    }
    else if ((val >= 48 && val < 58) || ((val > 96 && val < 106)) || val == 8 || val == 127 || val == 189 || val == 109 || val == 9) {
      return true;
    }
    else {
      return false;
    }
  }

}
