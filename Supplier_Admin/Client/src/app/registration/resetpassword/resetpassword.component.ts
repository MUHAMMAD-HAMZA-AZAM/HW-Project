import { HttpStatusCode } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { MustMatch } from '../../Shared/ApiRoutes/confirmed.validator';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-resetpassword',
  templateUrl: './resetpassword.component.html',
  styleUrls: ['./resetpassword.component.css']
})
export class ResetpasswordComponent implements OnInit {
  public resetPasswordVm: any;
  public forgotPasswrodVms: any;
  public showPassword: boolean = false;
  public showConfPassword: boolean = false;
  public resetPassword: any;
  public appValForm: FormGroup;
  public submitted: boolean = false;
  constructor(public _httpService: CommonService, public formBuilder: FormBuilder, public toast: ToastrService) {
    this.appValForm = {} as FormGroup;
  }

  ngOnInit(): void {
    var data = localStorage.getItem("resetPasswordToken");
    this.resetPasswordVm = data != null ? JSON.parse(data) : this.resetPasswordVm;


    this.appValForm = this.formBuilder.group({
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]],
    }, {
      validator: MustMatch('password', 'confirmPassword')
    });
  }
  showPasswordFunc() {
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
  get f() { return this.appValForm.controls; }
  Submit() {
    var data = localStorage.getItem("forgotPassword");
    this.forgotPasswrodVms = data != null ? JSON.parse(data) : this.forgotPasswrodVms;
    this.submitted = true;
    if (this.appValForm.valid) {
      if (this.appValForm.value.password != null) {
        localStorage.removeItem("resetPasswordToken");
        this.resetPasswordVm.password = this.appValForm.value.password ;
        this.resetPasswordVm.confirmPassword = this.appValForm.value.confirmPassword;

        this._httpService.PostData(this._httpService.apiUrls.Supplier.ForgetPassword.ResetPassword, this.resetPasswordVm, true).then(result => {
          let response = result;
          if (response.status == HttpStatusCode.Ok) {
            this.toast.success("Password Changed Sucessfully.", "Success")
            localStorage.removeItem("forgotPassword");
            this._httpService.NavigateToRoute(this._httpService.apiRoutes.Registration.Login)
          }
        }
        );
      }
    }
    else {

    }
  }
}
