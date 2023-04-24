import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { forgotPasswrodVm, ResetPassword, ResponseVm } from '../../../models/commonModels/commonModels';
import { forgotPassword, httpStatus } from '../../../shared/Enums/enums';
import { CommonService } from '../../../shared/HttpClient/_http';
import { Location } from '@angular/common'
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-forgot-password-code',
  templateUrl: './forgot-password-code.component.html',
  styleUrls: ['./forgot-password-code.component.css']
})
export class ForgotPasswordCodeComponent implements OnInit {
  public appValForm: FormGroup;
  private forgotPasswrodVms: forgotPasswrodVm = {} as  forgotPasswrodVm;
  public verificationCode: string="";
  public resetPasswordVm: ResetPassword = {} as  ResetPassword;
  private response: ResponseVm = {} as ResponseVm;
  public submitted = false;
  public incorrectcode = false;
  constructor(public router: Router, public routeParams: ActivatedRoute, private formBuilder: FormBuilder,
    public service: CommonService, private location: Location, public Loader: NgxSpinnerService
    , public toaster: ToastrService) {

    this.appValForm = {} as FormGroup;
  }

  ngOnInit() {
   // document.getElementById("headerText").innerHTML = "Get Code";
    
    var data = localStorage.getItem("forgotPassword");
    this.forgotPasswrodVms = data != null ? JSON.parse(data) : {} as forgotPasswrodVm;
    this.GetOtp();
    this.appValForm = this.formBuilder.group(
      {
        verficationcode: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(6)]],
      });
    this.incorrectcode = false;
  }
  get f() { return this.appValForm.controls; }
  ResendOtpCode() {
    this.GetOtp();
  }
  Cancel() {
    this.location.back();
  }
  VerifyForgotOtpAndResetPassword() {
    //
    // localStorage.removeItem("forgotPassword");
    
    this.submitted = true;
    if (this.appValForm.valid) {
      var data = this.appValForm.value;
      if (data.verficationcode != null && data.verficationcode != '') {
        if (data.verficationcode.length == 6) {
          this.resetPasswordVm.userId = this.forgotPasswrodVms.id;
          this.forgotPasswrodVms.verificationCode = data.verficationcode;
          this.Loader.show();
          this.service.PostData(this.service.apiRoutes.forgotPassword.verifyOtpAndGetResetPasswordToken, this.forgotPasswrodVms, true).then(result => {
            this.response = result ;
            this.Loader.hide();
            if (this.response.status == httpStatus.Ok) {
              this.resetPasswordVm.passwordResetToken = this.response.resultData;
              localStorage.setItem("resetPasswordToken", JSON.stringify(this.resetPasswordVm))
              this.service.NavigateToRouteWithQueryString(this.service.pagesUrl.CommonRegistrationPages.resetPassword)
            }
            else {
              this.Loader.hide();
            //  this.toaster.error('Enter 6 digit is incorrect', 'Error');
              this.incorrectcode = true
            }
          })
        }
        else {
          this.toaster.error('Enter 6 digit code', 'Error');
        }
      }
      else {
        this.toaster.error('Enter 6 digit code', 'Error');
      }
    }
    //this.router.navigate(['/resetpassword/app-reset-password']);
  }
  GetOtp() {
    this.Loader.show();
    this.service.PostData(this.service.apiRoutes.Common.getOtp, this.forgotPasswrodVms, true).then(result => {
      
      var res = result ;
      this.Loader.hide();
    });
  }
  numberOnly(event: KeyboardEvent): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    this.incorrectcode = false;
    return true;

  }
}
