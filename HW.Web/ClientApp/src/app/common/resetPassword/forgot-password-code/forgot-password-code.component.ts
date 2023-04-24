import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { forgotPasswrodVm, ResetPassword, ResponseVm } from '../../../models/commonModels/commonModels';
import { forgotPassword, httpStatus } from '../../../shared/Enums/enums';
import { CommonService } from '../../../shared/HttpClient/_http';
import { Location } from '@angular/common'
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';
@Component({
  selector: 'app-forgot-password-code',
  templateUrl: './forgot-password-code.component.html',
  styleUrls: ['./forgot-password-code.component.css']
})
export class ForgotPasswordCodeComponent implements OnInit {
  private forgotPasswrodVms: forgotPasswrodVm = new forgotPasswrodVm();
  public verificationCode: string;
  public resetPasswordVm: ResetPassword = new ResetPassword();
  private response: ResponseVm = new ResponseVm();
  constructor(public router: Router, public routeParams: ActivatedRoute, public service: CommonService, private location: Location, public Loader: Ng4LoadingSpinnerService) {


  }

  ngOnInit() {
    debugger;
    var data = localStorage.getItem("forgotPassword");
    this.forgotPasswrodVms = JSON.parse(data);
    this.GetOtp();
  }
  ResendOtpCode() {
    debugger;
    this.GetOtp();
  }
  Cancel() {
    this.location.back();
  }
  VerifyForgotOtpAndResetPassword() {
    localStorage.removeItem("forgotPassword");
    if (this.verificationCode != null && this.verificationCode != '') {
      this.resetPasswordVm.userId = this.forgotPasswrodVms.id;
      this.forgotPasswrodVms.verificationCode = this.verificationCode;
      this.Loader.show();
      this.service.post(this.service.apiRoutes.forgotPassword.verifyOtpAndGetResetPasswordToken, this.forgotPasswrodVms).subscribe(result => {
        this.response = result.json();
        this.Loader.hide();
        if (this.response.status == httpStatus.Ok) {
          this.resetPasswordVm.passwordResetToken = this.response.resultData;
          localStorage.setItem("resetPasswordToken", JSON.stringify(this.resetPasswordVm))
          this.service.NavigateToRouteWithQueryString(this.service.pagesUrl.CommonRegistrationPages.resetPassword)
        }
        else {
          this.Loader.hide();
        }
      })
    }
    //this.router.navigate(['/resetpassword/app-reset-password']);
  }
  GetOtp() {
    this.Loader.show();
    this.service.post(this.service.apiRoutes.Common.getOtp, this.forgotPasswrodVms).subscribe(result => {
      var res = result.json();
      this.Loader.hide();
    });
  }
}
