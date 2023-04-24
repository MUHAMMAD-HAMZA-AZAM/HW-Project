import { HttpStatusCode } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-forgotpasswordcode',
  templateUrl: './forgotpasswordcode.component.html',
  styleUrls: ['./forgotpasswordcode.component.css']
})
export class ForgotpasswordcodeComponent implements OnInit {
  public submitted = false;
  public errorCode: boolean = false
  public appValForm: FormGroup;
  public forgotPasswrodVms: any;
  public resetPasswordVm: any;
  public errorMsg: string="";
  constructor(public _httpService: CommonService, public formBuilder: FormBuilder) {
    this.appValForm = this.formBuilder.group(
      {
        otp: ['', Validators.required]
      });
  }

  ngOnInit(): void {
    
    var data = localStorage.getItem("forgotPassword");
    this.forgotPasswrodVms = data != null ? JSON.parse(data) : this.forgotPasswrodVms;
    this.GetOtp();
    
  }
  GetOtp() {
    this._httpService.PostData(this._httpService.apiUrls.Supplier.Registration.GetOtp, this.forgotPasswrodVms, true).then(result => {

      var res = result;
    });
  }
  ResendOtpCode() {
    this.GetOtp();
    this.errorCode = false;
    this.submitted = false;
  }
  get f() { return this.appValForm.controls; }
  Verify() {
    this.submitted = true;
    var data = this.appValForm.value;
    
    if (this.appValForm.valid) {
      if (data.otp != null && data.otp != '') {
        if (data.otp.length == 6) {
          this.errorCode = false;
          
          this.forgotPasswrodVms.verificationCode = data.otp;
          this._httpService.PostData(this._httpService.apiUrls.Supplier.ForgetPassword.VerifyOtpAndGetPasswordResetToken, this.forgotPasswrodVms, true).then(result => {
            let response = result;
            if (response.status == HttpStatusCode.Ok) {
              
              this.resetPasswordVm = {
                userId : this.forgotPasswrodVms.id,
                passwordResetToken: response.resultData
              }
              localStorage.setItem("resetPasswordToken", JSON.stringify(this.resetPasswordVm))
              this._httpService.NavigateToRoute(this._httpService.apiRoutes.ForgotPassword.ResetPassword)
            }
            else {
              if (response.status == HttpStatusCode.Forbidden)
                this.errorMsg = "Your account has been temporarily blocked. Please try again later."
              else
                this.errorMsg = "Invalid OTP"
              this.errorCode = true
            }
          });
        }
        else {
          this.errorMsg = "Invalid OTP"
          this.errorCode = true;
        }
      }
      else {
       
      }
    }
    else {
     
    }
  }
}
