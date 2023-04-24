import { HttpStatusCode } from '@angular/common/http';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { IPersonalDetails } from 'src/app/Shared/Enums/Interface';
import { StatusCode } from '../../Shared/Enums/common';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-withdrawal',
  templateUrl: './withdrawal.component.html',
  styleUrls: ['./withdrawal.component.css']
})
export class WithdrawalComponent implements OnInit {
  public isAmountValid: boolean = false;
  public response: any;
  public suppId: number = 0;
  public walletAmount: any;
  public withdrawRange = { minValue: 0, maxValue: 0 };
  public withdrawRangeInvalid: boolean = false;
  public withdrawRangeInvalidError: string = "";
  public isUserBlocked: boolean = false;
  public amount: Number = 0;
  public invalidAmount: boolean = false;
  public isEmptyAmount: boolean = false;
  public loginCheck: boolean = false;
  public isPendingRequest: boolean = false;
  public submittedApplicationForm: boolean = false;
  public isProceedPayment: boolean = false;
  public appValForm: FormGroup;
  public userName:string|null='';
  public threshold: Number = 0;
  public userId: string='';
  public userRole:string='';
  public isUserExist:boolean = true;
  public loggedUserDetails: IPersonalDetails;


  @ViewChild('blockAccountMessageModal', { static: false }) blockAccountMessageModal: ModalDirective;
  @ViewChild('blockSupplierMessageModal',{static:true}) blockSupplierMessageModal :ElementRef;
  constructor(private common: CommonService,
    private formBuilder: FormBuilder,
    public Loader: NgxSpinnerService,public _modalService: NgbModal) {
    this.appValForm = {} as FormGroup;
    this.blockAccountMessageModal = {} as ModalDirective;
  }
  ngOnInit(): void {
    var decodedtoken = this.common.decodedToken();
    console.log(decodedtoken);
    this.suppId = decodedtoken.Id;
   this.userId = decodedtoken.UserId;
    this.userRole = decodedtoken.Role;
    this.userName = localStorage.getItem("user_userName");
    this.appValForm = this.formBuilder.group({
      WithdrawalRequestId: [0],
      mobileNumber: ['', [Validators.required, Validators.minLength(11)]],
      cnic: ['', [Validators.required, Validators.minLength(13)]]
    });
    this.getWalletAmount();
    this.getLoggedUserDetails(this.userRole,this.userId);
  }
  get f() {
    return this.appValForm.controls;
  }

  public getLoggedUserDetails(role:string, userId:string) {

    this.common.GetData(this.common.apiUrls.Supplier.Profile.GetUserDetailsByUserRole + `?userId=${userId}&userRole=${role}`,true).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        this.loggedUserDetails = <any>this.response.resultData;
        debugger;
        console.log(this.loggedUserDetails);
       this.appValForm.patchValue(this.loggedUserDetails);
      }
    }, error => {
      this.common.Loader.show();
      console.log(error);
    });
  }
  
  public checkLoggedUserExist() {
    let obj = {
      id: this.suppId,
      
    };
    this.common.PostData(this.common.apiUrls.Supplier.Profile.CheckUserExist, JSON.stringify(obj),true).then(res => {
      let response = res;
      if (response.status == StatusCode.OK) {
        if (response.resultData.supplierId) {
          this.isUserExist = true;
          console.log("Logged User exist & withdrawn his amount ");
          this.withdraw();
        }
        else{
          this.isUserExist = false;
          this.common.logOut();
        }
      }
      this.Loader.hide();
    }, error => {
      console.log(error);
    });
  }
  public getWalletAmount() {
    this.common.GetData(this.common.apiUrls.Supplier.Profile.GetSupplierWallet + "?refSupplierId=" + this.suppId, true).then(res => {
      
      this.response = res;
      
      if (this.response.status == StatusCode.OK) {
        if (this.response.resultData) {
          this.walletAmount = this.response.resultData;
        }
      }

    }, error => {
      console.log(error);
    });
  }

  public AllowNonZeroIntegers(e: any): boolean {

    var val = e.keyCode;
    // var target = event.target ? event.target : event.srcElement;
    if (val == 48 && e.target.value == "" || val == 101 || val == 45 || val == 46 || ((val >= 65 && val <= 90)) || ((val >= 97 && val <= 122))) {
      return false;
    }
    else if ((val >= 48 && val < 58) || ((val > 96 && val < 106)) || val == 8 || val == 127 || val == 189 || val == 109 || val == 9) {
      return true;
    }
    else {
      return false;
    }
  }

  numberOnly(event: any): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }

  public getEnteredAmount(value: number) {
    let amount = Number(value);
    this.amount = amount;
    //if (amount >= Number(this.withdrawRange.minValue) && amount <= Number(this.withdrawRange.maxValue)) {
    //  this.withdrawRangeInvalid = false;
    //}
    //else {
    //  this.withdrawRangeInvalid = true;
    //  this.withdrawRangeInvalidError = `Please enter amount between ${this.withdrawRange.minValue} and ${this.withdrawRange.maxValue}`
    //}

  }
  public withdraw() {
    
    if (this.amount) {
      
      this.threshold = this.walletAmount - 50;
      if (this.walletAmount > this.amount && this.threshold >= this.amount) {
        this.isAmountValid = true;
      }
      else {
        this.invalidAmount = true;
        setTimeout(() => {
          this.invalidAmount = false;
        }, 4000)
      }
    }
    else {
      this.isEmptyAmount = true;
      setTimeout(() => {
        this.isEmptyAmount = false;
      }, 4000)
    }
  }

  public proceedPayment() {
    let obj = {
      id: this.suppId,
    };
    this.common.PostData(this.common.apiUrls.Supplier.Profile.CheckUserExist, JSON.stringify(obj),true).then(res => {
      let response = res;
      if (response.status == StatusCode.OK) {
        if (response.resultData.supplierId) {
          this.isUserExist = true;
          console.log("Logged User exist & withdrawn his amount ");
          this.common.GetData(this.common.apiUrls.Supplier.PackagesAndPayments.GetPaymentWithdrawalRequestByTradesmanId, true).then(d => {
          
            var data = d;
            
                if (data != null && data.paymentStatusId == 1) {
                  this.isPendingRequest = true;
                  setTimeout(() => {
                    this.isPendingRequest = false;
                  }, 4000);
                }
                else {
                  
                  this.submittedApplicationForm = true;
                  if (this.appValForm.valid) {
                    var data = this.appValForm.value;
                    let obj = {
                      supplierName: this.loggedUserDetails.firstName,
                      phoneNumber: this.appValForm.controls.mobileNumber.value,
                      amount: this.amount,
                      cnic: this.appValForm.controls.cnic.value,
                      paymentStatusId: 1,
                      role: this.loggedUserDetails.role
                     }
                    
                    this.common.PostData(this.common.apiUrls.Supplier.PackagesAndPayments.addSupplierPaymentWithdrawalRequest, obj, true).then(res => {
                    
                    var data = res;
                    
                      if (data.status == HttpStatusCode.Ok) {
                         
                        this.isProceedPayment = true;
                        setTimeout(() => {
                          this.common.NavigateToRoute(this.common.apiRoutes.Home.Dashboard);
                          this.isProceedPayment = false;
                        }, 3000);
                      }
                    });
      
              }
            }
          });
        }
        else{
          this.isUserExist = false;
          this.common.logOut();
        }
      }
      this.Loader.hide();
    }, error => {
      console.log(error);
    });

  }
}
