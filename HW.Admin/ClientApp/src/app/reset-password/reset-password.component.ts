import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ResetPasswordVM } from '../Shared/Models/HomeModel/HomeModel';
import { CommonService } from '../Shared/HttpClient/_http';
import { resetPassword } from '../Shared/Enums/enums';
import { NgxSpinnerService } from "ngx-spinner";
import { Router, ActivatedRoute, Params } from '@angular/router';
import { apiUrls } from '../Shared/ApiRoutes/ApiRoutes';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  public appValForm: FormGroup;
  resetPasswordVm: ResetPasswordVM = new ResetPasswordVM();
  public submittedApplicationForm = false;
  public passwordErrorMessage: string;
  public confirmPasswordErrorMessage: string;
  public unauthorizedErrorMessage: string;
  public unauthorizeduser: boolean = false;
  public userId: string;
  constructor(public httpService: CommonService, public Loader: NgxSpinnerService, private router: Router, private route: ActivatedRoute, private formBuilder: FormBuilder) {
    this.appValForm = this.formBuilder.group({
      passwords: ['', Validators.required],
      confirmPassword: ['', Validators.required]
    });
    this.route.queryParams.subscribe((params: Params) => {
      this.userId = params['UserId'];
    });
  }

  ngOnInit() {
  }
  get f() {
    return this.appValForm.controls;
  }
  resetPassword() {
    this.submittedApplicationForm = true;
    this.passwordErrorMessage = resetPassword.password;
    this.confirmPasswordErrorMessage = resetPassword.confirmPassword;
    this.unauthorizedErrorMessage = resetPassword.passwordMatch;
    this.resetPasswordVm = this.appValForm.value;
    if (this.appValForm.valid) {
      if (this.resetPasswordVm.passwords == this.resetPasswordVm.confirmPassword) {
        this.resetPasswordVm.userId = this.userId;
        var data;
        this.Loader.show();
        this.httpService.post(apiUrls.Login.resetPassword, this.resetPasswordVm).subscribe(result => {
          data = result.ok;
          
          this.Loader.hide();
          if (data) {
            this.router.navigateByUrl('login');
          }
          else {
            this.unauthorizeduser = true;
            this.unauthorizedErrorMessage = "You can't change password. Conatct with development team.";
          }
        }, error => {
          this.unauthorizeduser = true;
          this.unauthorizedErrorMessage = "You can't change password. Conatct with development team.";
          this.Loader.hide();
          }
        );
      }
      else {
        this.unauthorizeduser = true;
      }
    }
  }
}
