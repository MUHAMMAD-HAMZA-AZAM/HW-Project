import { HttpStatusCode } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { IPersonalDetails, IVerify, } from '../../Shared/Enums/Interface';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public appValForm: FormGroup;
  public submitted: boolean = false;
  public role: string = 'Supplier';
  public loginStatus: boolean = false;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  public profile: IPersonalDetails;
  public isVerified: boolean = true;
  public verifyVM: IVerify;
  public code: number = 0;
  public otpsubmitted = false;
  public appValFormVerify: FormGroup;
  public inValidOTP: boolean = false;
  public errorMsg: string = "";
  public showPassword: boolean = false;
  public IsPhoneNumberTimerRunning: boolean = false;
  public ptimer: any;
  public phoneNumberdisplay: any;
  public supplierId: number = 0;
  constructor(public _httpService: CommonService, public formBuilder: FormBuilder) {
    this.appValForm = {} as FormGroup;
  }
  ngOnInit(): void {
    this.appValForm = this.formBuilder.group({
      emailOrPhoneNumber: ['', [Validators.required]],
      password: ['', [Validators.required]],
      rememberMe: [false]
    });
    this.populateData();
  }
  get f() {
    return this.appValForm.controls;
  }
  populateData(){
    if(localStorage.getItem('supplierUserName') !=  "null" && localStorage.getItem('supplierPassword') != "null") {
      this.appValForm.controls.emailOrPhoneNumber.setValue(localStorage.getItem('supplierUserName'));
      this.appValForm.controls.password.setValue(localStorage.getItem('supplierPassword'));
    }
  }
  login() {
    this.submitted = true;
    if (this.appValForm.valid) {
      let form = this.appValForm.value;
      var expireDate = new Date();
      expireDate.setFullYear(expireDate.getFullYear() + 1);
      let obj = {
        emailOrPhoneNumber: form.emailOrPhoneNumber,
        password: form.password,
        role: this.role
      }
      if(form.rememberMe) {
        localStorage.setItem('supplierUserName', obj.emailOrPhoneNumber);
        localStorage.setItem('supplierPassword', obj.password);
        localStorage.setItem('supplierRole', obj.role);
        localStorage.setItem('rememberMe', form.rememberMe);
      }
      else {
        localStorage.clear();
        localStorage.setItem('rememberMe', form.rememberMe);
      }
      this._httpService.PostData(this._httpService.apiUrls.Supplier.Registration.Login, obj, true)?.then(res => {
        let response = res;
        if (response.status == HttpStatusCode.Ok) {
          var decodedtoken = this.jwtHelperService.decodeToken(response.resultData);
          localStorage.setItem("auth_token", "Bearer " + response.resultData);
          this.supplierId = this._httpService.decodedToken().Id;
          this._httpService.NavigateToRoute(this._httpService.apiRoutes.Home.Dashboard);
        }
        else {
          this.loginStatus = true;
          setTimeout(() => {
            this.loginStatus = false;
          }, 3000)
        }
      });
    }
  }
  showPasswordFunch() {
    if (this.showPassword) {
      this.showPassword = false;
    }
    else {
      this.showPassword = true;
    }
  }

}
