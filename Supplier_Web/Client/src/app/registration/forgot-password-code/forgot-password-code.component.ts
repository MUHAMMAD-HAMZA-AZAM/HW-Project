import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ResponseVm, StatusCode } from '../../Shared/Enums/enum';
import { IPageSeoVM } from '../../Shared/Enums/Interface';
import { CommonService } from '../../Shared/HttpClient/HttpClient';
import { metaTagsService } from '../../Shared/HttpClient/seo-dynamic.service';

@Component({
  selector: 'app-forgot-password-code',
  templateUrl: './forgot-password-code.component.html',
  styleUrls: ['./forgot-password-code.component.css']
})
export class ForgotPasswordCodeComponent implements OnInit {
  public otpDigits: number = 6;
  public inValidOTPCode: boolean = false;
  public resetPasswordVM: any="";
  public forgetPasswordVM: any="";
  public appValForm: FormGroup;
  public response: ResponseVm = new ResponseVm();
  public otpTimerRunning: boolean = false;
  public ptimer: any;
  public phoneNumberdisplay: any;
  constructor(
    public _httpService: CommonService,
    public formBuilder: FormBuilder,
    private router: ActivatedRoute,
    private _metaService: metaTagsService,
    private toastr: ToastrService
  ) {
this.appValForm={} as FormGroup;
}

  ngOnInit(): void {
    var data = localStorage.getItem("forgotPassword");
    this.forgetPasswordVM = data!=null? JSON.parse(data) :"";
    this.GetOtpCode();
    this.appValForm = this.formBuilder.group({
      otpCode: ['', Validators.required]
    });
    this.bindMetaTags();
  }

  //--------- Get OTP Code From User
  public GetOtpCode() {
    if (!this.otpTimerRunning) {
      this.otpVerificationTimer(1);
    }
    this._httpService.PostData(this._httpService.apiUrls.ForgetPassword.GetOtp, this.forgetPasswordVM, true).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        console.log(this.response.message);
      }
    }, error => {
      console.log(error);
    });
  }

  //---------- Resend OTP Code If User Did't Get the OTP on First Time
  public ResendOtpCode() {
    this.GetOtpCode();
    if (!this.otpTimerRunning) {
      this.otpVerificationTimer(1);
    }
  }

  //-------------------  Customer Verification for Forgot Password

  get f() { return this.appValForm.controls; }

  public verifyForgetPasswordOTP() {
    if (this.appValForm.invalid) {
      this.appValForm.markAllAsTouched();
      return;
    }
    if (this.otpTimerRunning) {
      let formData = this.appValForm.value;
      if (formData.otpCode.length == this.otpDigits) {
        this.forgetPasswordVM.verificationCode = formData.otpCode;
        //console.log(this.forgetPasswordVM);

        this._httpService.PostData(this._httpService.apiUrls.ForgetPassword.VerifyOtpAndGetPasswordResetToken, this.forgetPasswordVM, true).then(res => {
          this.response = res;
          console.log(this.response.message);
          if (this.response.status == StatusCode.OK) {
            this.resetPasswordVM = {
              userId: this.forgetPasswordVM.id,
              passwordResetToken: this.response.resultData
            }
            //console.log(this.resetPasswordVM);
            localStorage.setItem("resetPasswordToken", JSON.stringify(this.resetPasswordVM))
            this._httpService.NavigateToRoute(this._httpService.apiRoutes.ForgotPassword.ResetPassword)
          }
          else {
            this.inValidOTPCode = true;
            setTimeout(() => {
              this.inValidOTPCode = false;
            }, 3000);
          }
        });

        console.log("OTP is Valid");
      }

   
    
     
  }
    else {
      //---- If OTP Is Invalid 
      this.inValidOTPCode = true;
      setTimeout(() => {
        this.inValidOTPCode = false;
        this._httpService.NavigateToRoute(this._httpService.apiRoutes.ForgotPassword.EnterMobileNumber);
      },3000);
    }
  }
  
 public  otpVerificationTimer(minute: any) : void  {
   this.otpTimerRunning = true;
    let seconds: number = minute * 60;
    let textSec: any = "0";
    let statSec: number = 60;
    const prefix = minute < 10 ? "0" : "";
    if (minute == "0") {
      clearInterval(this.ptimer);
      this.otpTimerRunning = false;
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
          this.otpTimerRunning = false;
          this.phoneNumberdisplay = null;
          console.log("finished");
          clearInterval(this.ptimer);
        }
      }, 1000);
    }
  }
  public bindMetaTags() {
    this._httpService.get(this._httpService.apiUrls.CMS.GetSeoPageById + "?pageId=23").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description, res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }
}
