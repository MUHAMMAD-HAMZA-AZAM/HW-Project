import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ResetPassword, ResetPasswordVm, ResponseVm } from '../../../models/commonModels/commonModels';
import { CommonService } from '../../../shared/HttpClient/_http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { resetPassword, httpStatus } from '../../../shared/Enums/enums';
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  public resetPasswordVm: ResetPassword = new ResetPassword();
  public resetPassword: ResetPasswordVm = new ResetPasswordVm();
  public responseVm: ResponseVm = new ResponseVm();
  public confirmPassword: string;

  public password: string;
  public appValForm: FormGroup;
  public passwordErrorMessage: string;
  public confirmPasswordErrorMessage: string;
  public confirmPasswordErrorMessageMustMatch: string;
  public submittedApplicationForm: boolean = false;
  public mustmatchcheck: boolean = false;
  public error: boolean = false;

  constructor(public router: Router, public service: CommonService, private formBuilder: FormBuilder, public Loader: Ng4LoadingSpinnerService) {
    this.appValForm = this.formBuilder.group({
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(8)]]
    });
  }

  ngOnInit() {
    var data = localStorage.getItem("resetPasswordToken");
    this.resetPasswordVm = JSON.parse(data);
  }

  get f() {
    return this.appValForm.controls;
  }

  SaveNewPassword() {
    debugger;
    this.passwordErrorMessage = resetPassword.password;
    this.confirmPasswordErrorMessage = resetPassword.confirmPassword;
    this.confirmPasswordErrorMessageMustMatch = resetPassword.passwordMatch;
    this.resetPassword = this.appValForm.value;

    this.submittedApplicationForm = true;
    if (this.appValForm.valid) {
      if (this.resetPassword.password == this.resetPassword.confirmPassword) {
        localStorage.removeItem("resetPasswordToken");
        this.resetPasswordVm.confirmPassword = this.resetPassword.confirmPassword;
        this.resetPasswordVm.password = this.resetPassword.password;
        this.Loader.show();
        this.service.post(this.service.apiRoutes.resetPassword.resetPassword, this.resetPasswordVm).subscribe(result => {
          this.responseVm = result.json();
          this.Loader.hide();
          if (this.responseVm.status == httpStatus.Ok) {
            this.service.NavigateToRouteWithQueryString(this.service.pagesUrl.CommonRegistrationPages.login);
          }
          else {
            this.error = true;
          }
        })
      }
      else {
        this.mustmatchcheck = true;
      }
    }
  }
}
