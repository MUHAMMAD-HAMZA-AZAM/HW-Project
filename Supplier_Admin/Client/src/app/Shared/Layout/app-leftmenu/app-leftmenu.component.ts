import { HttpStatusCode } from '@angular/common/http';
import { Reference } from '@angular/compiler/src/render3/r3_ast';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ResponseVm, StatusCode } from '../../Enums/common';
import { IVerify } from '../../Enums/Interface';
import { CommonService } from '../../HttpClient/_http';

@Component({
  selector: 'app-app-leftmenu',
  templateUrl: './app-leftmenu.component.html',
  styleUrls: ['./app-leftmenu.component.css']
})
export class AppLeftmenuComponent implements OnInit {
  public response: ResponseVm = new ResponseVm();
  public suppId: string = "";
  public userId: string = "";
  public phoneNumber: string = "";
  public logoImage: string = "";
  public fullName: string = "";
  public companyName: string = "";
  public phoneNumberConfirmed: boolean = true;
  public appValFormVerify: FormGroup;
  public profileVerification: any = [];
  public verifyVM: IVerify;
  public code: number = 0;
  public ptimer: any;
  public errorMsg: string = "";
  public otpsubmitted: boolean = false;
  public inValidOTP: boolean = false;
  public IsPhoneNumberTimerRunning: boolean = false;
  public phoneNumberdisplay: string | null = "";
  public obj: any = {
    sellerAccountVerification: false,
    businessInformationVerification: false,
  };
  constructor(public _http: CommonService, public Loader: NgxSpinnerService, public toastr: ToastrService, public formBuilder: FormBuilder, private modalService: NgbModal) {
    this.appValFormVerify = {} as FormGroup;
  }

  ngOnInit(): void {
    var decodedtoken = this._http.decodedToken();
    this.suppId = decodedtoken.Id;
    this.userId = decodedtoken.UserId;
    this.appValFormVerify = this.formBuilder.group({
      otp: ['', [Validators.required]]
    });

    this.PopulateLogo();
    this._http.subject$.subscribe((isLogoChanged: any) => {
      if (isLogoChanged) {
        this.PopulateLogo();
      }
    }, error => {
      console.log(error);
    });

  }

  get g() { return this.appValFormVerify.controls; }

  public PopulateLogo() {
    let supplierId = this.suppId;
    this._http.GetData(this._http.apiUrls.Supplier.Profile.GetProfile + "?supplierId=" + this.suppId, true).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        if (this.response.resultData[0].profileImage) {
          this.logoImage = this.response.resultData[0].profileImage;
        }
        this.fullName = this.response.resultData[0].fullName
        this.companyName = this.response.resultData[0].companyName
        this.phoneNumberConfirmed = this.response.resultData[0].phoneNumberConfirmed
        this.phoneNumber = this.response.resultData[0].phoneNumber
      }

    }, error => {
      console.log(error);
    });
  }
  logOut() {
    localStorage.clear();
    this._http.NavigateToRoute(this._http.apiRoutes.Registration.Login);
  }
  AddNewProduct() {
    this.Loader.show();
    this._http.GetData(this._http.apiUrls.Supplier.Profile.GetProfileVerification + "?supplierId=" + this.suppId, true).then(response => {
      this.profileVerification = response;
      if (this.profileVerification.status == HttpStatusCode.Ok) {
        this.obj = this.profileVerification.resultData[0];
        if (this.obj.sellerAccountVerification && this.obj.businessInformationVerification)
          this._http.NavigateToRoute(this._http.apiRoutes.Product.AddNewProduct);
        else {
          this.toastr.warning("Please complete your 'Seller Account' & 'Business Info' in profile.")
          this._http.NavigateToRouteWithQueryString(this._http.apiRoutes.ProfileManagement.ProfileManagement, {
            queryParams: {
              "tabId": this.obj.sellerAccountVerification ? 'business-information' : 'seller-account'
            }
          });
        }
      }
      else {
        this._http.NavigateToRoute(this._http.apiRoutes.Product.AddNewProduct);
      }
      this.Loader.hide();
    })
  }
  ShowCard(tabName: string) {
    this._http.subjectNavigateToProfile$.next(true)
    this._http.subjectNavigateToProfile$.next(false)
    this._http.NavigateToRouteWithQueryString(this._http.apiRoutes.ProfileManagement.ProfileManagement, {
      queryParams: {
        "tabId": tabName
      }
    });
  }
  // OTP Section
  verifyAccount(content: any) {
    this.SentOtpCode();
    if (!this.IsPhoneNumberTimerRunning) {
      this.phoneNumbertimer(1);
    }
    this.modalService.open(content);
  }
  phoneNumbertimer(minute: number) {
    this.IsPhoneNumberTimerRunning = true;
    let seconds: number = minute * 60;
    let textSec: any = "0";
    let statSec: number = 60;

    const prefix = minute < 10 ? "0" : "";

    if (minute == 0) {
      clearInterval(this.ptimer);
      this.IsPhoneNumberTimerRunning = false;
      this.phoneNumberdisplay = null;
    }
    else {
      this.ptimer = setInterval(() => {
        seconds--;
        if (statSec != 0) statSec--;
        else statSec = 59;

        if (statSec < 10) {
          textSec = "0" + statSec;
        } else textSec = statSec;

        this.phoneNumberdisplay = `OTP will be Expired in: ${prefix}${Math.floor(seconds / 60)}:${textSec}`;

        if (seconds == 0) {
          this.IsPhoneNumberTimerRunning = false;
          this.phoneNumberdisplay = null;
          console.log("finished");
          clearInterval(this.ptimer);
        }
      }, 1000);
    }
  }
  public SentOtpCode() {
    this.verifyVM = {
      id: this.userId,
      phoneNumber: this.phoneNumber
    }
    this._http.PostData(this._http.apiUrls.Supplier.Registration.GetOtp, this.verifyVM, true).then(result => {
      let response = result;
      if (response.status == HttpStatusCode.Ok) {

      }
    });
  }
  public VerifyOTP() {
    this.otpsubmitted = true;
    if (this.appValFormVerify.valid) {
      this.code = this.appValFormVerify.value.otp;
      this._http.GetData(this._http.apiUrls.Supplier.Registration.VerifyOtpWithoutToken + "?code=" + this.code + "&phoneNumber=" + this.phoneNumber + "&email=" + "" + "&userId=" + this.userId, true).then(result => {
        let response = result;
        if (response.status == HttpStatusCode.Ok) {
          this.phoneNumberConfirmed = true;
          this.modalService.dismissAll();
        }
        else {
          if (response.status == HttpStatusCode.Forbidden)
            this.errorMsg = "Your account has been temporarily blocked. Please try again later."
          else
            this.errorMsg = "Invalid OTP"
          this.inValidOTP = true;
        }
      });
    }
  }
  ResendOTP() {
    this.appValFormVerify.reset();
    this.inValidOTP = false;
    this.otpsubmitted = false;
    if (!this.IsPhoneNumberTimerRunning) {
      this.phoneNumbertimer(1);
    }
    this.SentOtpCode();
  }
  navigateToLogo() {
    debugger;
    this._http.subjectNavigateToLogo$.next(true);
    this._http.subjectNavigateToLogo$.next(false);
    this._http.NavigateToRouteWithQueryString(this._http.apiRoutes.ProfileManagement.ProfileManagement, {
      queryParams: {
        "tabId": "seller-logo"

      }
    });
  }

}
