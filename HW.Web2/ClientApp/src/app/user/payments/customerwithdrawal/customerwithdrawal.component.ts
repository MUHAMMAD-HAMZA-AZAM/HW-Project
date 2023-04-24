import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { httpStatus } from '../../../shared/Enums/enums';
import { CommonService } from '../../../shared/HttpClient/_http';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { JwtHelperService } from '@auth0/angular-jwt';
import { SocialAuthService } from 'angularx-social-login';
import { NgxSpinnerService } from 'ngx-spinner';
import { IApplicationSetting, IPersonalDetails } from '../../../shared/Interface/tradesman';

@Component({
  selector: 'app-Customerwithdrawal',
  templateUrl: './Customerwithdrawal.component.html',
  styleUrls: ['./Customerwithdrawal.component.css']
})
export class CustomerwithdrawalComponent implements OnInit {

  public walletAmount: number = 0;
  public amount: number = 0;
  public isEmptyAmount: boolean = false;
  public invalidAmount: boolean = false;
  public isAmountValid: boolean = false;
  public isjazzCash: boolean = false;
  public userName: string | null = "";
  public isProceedPayment: boolean = false;
  public appValForm: FormGroup;
  public submittedApplicationForm: boolean = false;
  public isPendingRequest: boolean = false;
  public loginCheck: boolean = false;
  public loggedUserDetails: IPersonalDetails;
  public userId: string = "";
  public customerId: string = "";
  public isUserBlocked: boolean = false;
  public settingList: IApplicationSetting[] = [];
  public settingDetailsList: IApplicationSetting[] = [];
  public withdrawRange = { minValue: 0, maxValue: 0 };
  public withdrawRangeInvalid: boolean = false;
  public withdrawRangeInvalidError: string = "";
  @ViewChild('blockAccountMessageModal', { static: false }) blockAccountMessageModal: ModalDirective;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(private common: CommonService,
    private formBuilder: FormBuilder,
    private authService: SocialAuthService, public Loader: NgxSpinnerService,) {
    this.loggedUserDetails = {} as IPersonalDetails;
    this.appValForm = {} as FormGroup;
    this.blockAccountMessageModal = {} as ModalDirective;
  }

  ngOnInit() {
    var token = localStorage.getItem("auth_token");
    var decodedtoken = token != null ? this.jwtHelperService.decodeToken(token) : "";
    debugger;
    this.userId = decodedtoken.UserId;
    this.customerId = decodedtoken.Id;
    this.checkUserStatus();

    this.userName = localStorage.getItem("user_userName");
    this.appValForm = this.formBuilder.group({
      WithdrawalRequestId: [0],
      mobileNo: ['', [Validators.required, Validators.minLength(11)]],
      cnic: ['', [Validators.required, Validators.minLength(13)]]
    });
    this.getSettingList();
  }
  get f() {
    return this.appValForm.controls;
  }
  public getEnteredAmount(value: string) {
    let amount = Number(value);
    if (amount >= Number(this.withdrawRange.minValue) && amount <= Number(this.withdrawRange.maxValue)) {
      this.withdrawRangeInvalid = false;
    }
    else {
      this.withdrawRangeInvalid = true;
      this.withdrawRangeInvalidError = `Please enter amount between ${this.withdrawRange.minValue} and ${this.withdrawRange.maxValue}`
    }

  }

  public getSettingDetailsList() {
    this.Loader.show();
    this.common.get(this.common.apiRoutes.UserManagement.GetSettingDetailsList).subscribe(res => {
      this.settingDetailsList = <IApplicationSetting[]>res;
      for (var i in this.settingDetailsList) {
        if (this.settingDetailsList[i].settingName == "Cash Withdrawl") {
          if (this.settingDetailsList[i].settingKeyName == "MinValue") {
            this.withdrawRange.minValue = this.settingDetailsList[i].settingKeyValue
          }
          else if (this.settingDetailsList[i].settingKeyName == "MaxValue") {
            this.withdrawRange.maxValue = this.settingDetailsList[i].settingKeyValue
          }
        }
      }
    })
    this.Loader.hide();
  }
  public getSettingList() {
    this.common.get(this.common.apiRoutes.UserManagement.GetSettingList).subscribe(res => {
      this.settingList = <IApplicationSetting[]>res;
      let withDrawSetting = this.settingList.filter(x => x.settingName == "Cash Withdrawl" && x.isActive);
      if (withDrawSetting.length > 0) {
        this.getSettingDetailsList();
      }
    })
  }
  public checkUserStatus() {
    this.common.GetData(this.common.apiRoutes.Login.GetUserBlockStatus + "?userId=" + this.userId).then(result => {
      this.isUserBlocked = result
      if (!this.isUserBlocked) {
        this.getWalletAmount();
      }
      else {
        this.isUserBlocked = true;
        this.blockAccountMessageModal.show();
        setTimeout(() => {
          this.isUserBlocked = false;
          this.blockAccountMessageModal.hide();
          this.logout();
        }, 5000);
      }
    }, error => {

      console.log(error);
    });

  }

  public logout() {
    this.loggedUserDetails = {} as IPersonalDetails;
    localStorage.clear();
    this.authService.signOut();
    this.loginCheck = false;
    this.common.NavigateToRoute("");

  }

  public hideBlockModal() {
    this.blockAccountMessageModal.hide();
    this.logout();
  }

  public getWalletAmount() {
    this.common.GetData(this.common.apiRoutes.PackagesAndPayments.GetLedgerTransactionCustomer + "?refercustomerId=" + this.customerId, true).then(result => {
      if (status = httpStatus.Ok) {
        var data = result;
        if (data.status == httpStatus.Ok) {
          this.walletAmount = data.resultData;
        }
      }
    });
  }

  public withdraw() {
    this.common.GetData(this.common.apiRoutes.Login.GetUserBlockStatus + "?userId=" + this.userId).then(result => {
      this.isUserBlocked = result
      if (!this.isUserBlocked) {
        if (this.amount) {
          var threshold = this.walletAmount - 50;
          if (this.walletAmount > this.amount && threshold >= this.amount) {
            this.isAmountValid = true;
          }
          else {
            this.invalidAmount = true;
            setTimeout(() => {
              this.invalidAmount = false;
            }, 4000)
            this.invalidAmount = false;
          }
        }
        else {
          this.isEmptyAmount = true;
          setTimeout(() => {
            this.isEmptyAmount = false;
          }, 4000)
        }
      }
      else {
        this.isUserBlocked = true;
        this.blockAccountMessageModal.show();
        setTimeout(() => {
          this.isUserBlocked = false;
          this.blockAccountMessageModal.hide();
          this.logout();
        }, 5000);
      }
    }, error => {

      console.log(error);
    });


  }

  public jazzCash() {
    this.isjazzCash = true;
  }

  public proceedPayment() {
    this.common.GetData(this.common.apiRoutes.Login.GetUserBlockStatus + "?userId=" + this.userId).then(result => {
      this.isUserBlocked = result
      if (!this.isUserBlocked) {
        this.common.GetData(this.common.apiRoutes.PackagesAndPayments.GetPaymentWithdrawalRequestByTradesmanId, true).then(d => {
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
                tradesmanName: this.userName,
                phoneNumber: this.appValForm.controls.mobileNo.value,
                amount: this.amount,
                cnic: this.appValForm.controls.cnic.value,
                paymentStatusId: 1
              }
              this.common.PostData(this.common.apiRoutes.PackagesAndPayments.addPaymentWithdrawalRequest, obj, true).then(res => {
                var data = res;
                if (data.status == httpStatus.Ok) {
                  this.isProceedPayment = true;
                  setTimeout(() => {
                    this.common.NavigateToRoute(this.common.apiUrls.User.UserDefault);
                    this.isProceedPayment = false;
                  }, 3000);
                }
              });

            }
          }
        });
      }
      else {
        this.isUserBlocked = true;
        this.blockAccountMessageModal.show();
        setTimeout(() => {
          this.isUserBlocked = false;
          this.blockAccountMessageModal.hide();
          this.logout();
        }, 5000);
      }
    }, error => {
      console.log(error);
    });

  }

  public AllowNonZeroIntegers(e: KeyboardEvent): boolean {

    var val = e.keyCode;
    // var target = event.target ? event.target : event.srcElement;
    if (val == 48 && (<HTMLInputElement>e.target).value == "" || val == 101 || val == 45 || val == 46 || ((val >= 65 && val <= 90)) || ((val >= 97 && val <= 122))) {
      return false;
    }
    else if ((val >= 48 && val < 58) || ((val > 96 && val < 106)) || val == 8 || val == 127 || val == 189 || val == 109 || val == 9) {
      return true;
    }
    else {
      return false;
    }
  }

  numberOnly(event: KeyboardEvent): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }
}


