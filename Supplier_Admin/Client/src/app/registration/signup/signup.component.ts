import { HttpStatusCode } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { isVariableDeclaration } from 'typescript';
import { MustMatch } from '../../Shared/ApiRoutes/confirmed.validator';
import { ICityList, IPersonalDetails, IValForm, IVerify, IValidationError } from '../../Shared/Enums/Interface';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  public sellerFlag: boolean = false;
  public appValForm: FormGroup;
  public submitted = false;
  public otpsubmitted = false;
  public sellerType: number=0;
  public showPassword: boolean = false;
  public showConfPassword: boolean = false;
  public role: string = "Supplier";
  public existedAccount: boolean = false;
  public isVerified: boolean = true;
  public inValidOTP: boolean = false;
  public appValFormVerify: FormGroup;
  public phoneNumber: number=0;
  public profile: IPersonalDetails;
  public verifyVM: IVerify;
  public code: number = 0;
  public obj: IValForm ;
  public errorMsg: string="";
  public cityList : ICityList[] = [];
  public IsPhoneNumberTimerRunning: boolean = false;
  public ptimer: any;
  public phoneNumberdisplay: string | null = "";
  public validationError: IValidationError[] = [];
  public errorMessage: string = "";
  constructor(public _httpService: CommonService,
    public formBuilder: FormBuilder,
    private route: ActivatedRoute, private _modalService: NgbModal) {
    this.obj = {} as IValForm;
    this.verifyVM = {} as IVerify;
    this.profile = {} as IPersonalDetails;
    this.appValFormVerify = {} as FormGroup;
    this.appValForm = {} as FormGroup;
  }

  ngOnInit(): void {

    this.route.queryParams.subscribe((param: Params) => {
      this.sellerType = param['sellerType'];
    });

    this.appValForm = this.formBuilder.group({
      accountType: [''],
      mobileNo: ['', [Validators.required]],
      code: [''],
      email: ['', [Validators.required,Validators.pattern("^[a-zA-Z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]],
      shopName: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]],
      cityId:[null,],
      terms: [false, [Validators.requiredTrue]]
    },{
      validator: MustMatch('password', 'confirmPassword')
    });
    this.appValFormVerify = this.formBuilder.group({
      otp: ['', [Validators.required]]
    });
    this.getAllCities();
  }
  public getAllCities() {
    this._httpService.get(this._httpService.apiUrls.Supplier.City.getCityList).subscribe(result => {
      this.cityList = <any>result;
      console.log(this.cityList);
    });
  }
  get f() { return this.appValForm.controls; }
  get g() { return this.appValFormVerify.controls; }

  Register() {
    //
    this.submitted = true;
    if (this.appValForm.valid) {
      let formValue = this.appValForm.value;
       this.obj = {
        email: formValue.email,
        phoneNumber: formValue.mobileNo,
        city:formValue?.cityId ? formValue.cityId:0,
        role: this.role,
        accountType: formValue.accountType,
        sellerType: this.sellerType,
        password: formValue.password,
        termsAndConditions: true,
        userName: formValue.email,
        emailOrPhoneNumber: formValue.email,
         shopName: formValue.shopName,
         isAllGoodStatus:false
      }
      console.log(this.obj);

      this._httpService.PostData(this._httpService.apiUrls.Supplier.Registration.CheckEmailandPhoneNumberAvailability, this.obj, true)?.then(res => {
        let response = res;
        if (response.status == HttpStatusCode.Ok) {

          this._httpService.PostData(this._httpService.apiUrls.Supplier.Registration.VerifyOtpAndRegisterUser, this.obj, true)?.then(result => {
            response = result;
            if (response.status == HttpStatusCode.Ok) {
              this._httpService.PostData(this._httpService.apiUrls.Supplier.Registration.Login, this.obj, true)?.then(x => {
                let response = x;
                if (response.status == HttpStatusCode.Ok) {
                  localStorage.setItem("auth_token", "Bearer " + response.resultData);
                  let role = this._httpService.decodedToken().Role;
                  let userId = this._httpService.decodedToken().UserId;

                  this._httpService.PostData(this._httpService.apiUrls.Supplier.PackagesAndPayments.AddSubAccountWithoutToken + "?userId=" + userId + "&role=" + role, true)?.then(result => {
                    let subAccountResponse = result;
                  });
                  this._httpService.NavigateToRoute(this._httpService.apiRoutes.Home.Dashboard);
                }
                else {
                }
              });
            }
          });
        }
        else {
          this.validationError = response.resultData;
          if (this.validationError.length == 2)
            this.errorMessage = 'Email & Phone Number Already Exist.'
          else if (this.validationError[0].key == 'DuplicateEmail')
            this.errorMessage = 'Email Already Exist.'
          else
            this.errorMessage = 'Phone Number Already Exist.';
          this.existedAccount = true;
          setTimeout(() => {
            this.existedAccount = false;
          },3000);
        }

      });

    }
  }
  phoneNumbertimer(minute:number) {
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
      id: this.profile.userId,
      phoneNumber: this.profile.mobileNumber
    }
  
    this._httpService.PostData(this._httpService.apiUrls.Supplier.Registration.GetOtp, this.verifyVM, true).then(result => {
      let response = result;
      if (response.status == HttpStatusCode.Ok) {
        
      }
      
    });
  }
  public VerifyOTP() {
    debugger;
    this.otpsubmitted = true;
    if (this.appValFormVerify.invalid) {
      return;
    }
    this.code = this.appValFormVerify.value.otp;
    this._httpService.GetData(this._httpService.apiUrls.Supplier.Registration.VerifyOtpWithoutToken + "?code=" + this.code + "&phoneNumber=" + this.phoneNumber + "&email=" + "" + "&userId=" + this.profile.userId, true).then(result => {
      let response = result;
      
      if (response.status == HttpStatusCode.Ok) {
        this.inValidOTP = false;
        this._httpService.PostData(this._httpService.apiUrls.Supplier.Registration.Login, this.obj, true)?.then(x => {
          let response = x;
          
          if (response.status == HttpStatusCode.Ok) {
            localStorage.setItem("auth_token", "Bearer " + response.resultData);
            let role = this._httpService.decodedToken().Role;
            let userId = this._httpService.decodedToken().UserId;

            this._httpService.PostData(this._httpService.apiUrls.Supplier.PackagesAndPayments.AddSubAccountWithoutToken + "?userId=" + userId + "&role=" + role, true)?.then(result => {
              let subAccountResponse = result;
            });
            this._httpService.NavigateToRoute(this._httpService.apiRoutes.Home.Dashboard);
          }
          else {
          }
        });
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
  ResendOTP() {
    this.appValFormVerify.reset();
    this.inValidOTP = false;
    this.otpsubmitted = false;
    if (!this.IsPhoneNumberTimerRunning) {
      this.phoneNumbertimer(1);
    }
    this.SentOtpCode();
  }
  showPasswordFunch() {
    if (this.showPassword) {
      this.showPassword = false;
    }
    else {
      this.showPassword = true;
    }
  }
  showConfPasswordFunc(){
    if (this.showConfPassword) {
      this.showConfPassword = false;
    }
    else {
      this.showConfPassword = true;
    }
  }
  //----------------- Show Modal for Terms & Conditions
  public showModal(modalName: TemplateRef<any>) {
    this._modalService.open(modalName, { size: 'lg', scrollable: true });
  }
}
