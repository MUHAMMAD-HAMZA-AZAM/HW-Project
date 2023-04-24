import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonService } from '../../shared/HttpClient/_http';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt/src/jwthelper.service';
import { LoginVM, ResponseVm } from '../../models/commonModels/commonModels';
import { LoginValidation, loginsecurity, httpStatus } from '../../shared/Enums/enums';
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public emailErrorMessage: string;
  public passwordErrorMessage: string;
  public invalidcredentials: string;
  public unauthorizeduser: boolean = false;
  public submittedApplicationForm = false;
  public appValForm: FormGroup;
  emailorPassword: string;
  password: string;
  loginstatus: boolean = false;
  loginVm: LoginVM = new LoginVM();
  responseVm: ResponseVm = new ResponseVm();
  jwtHelperService: JwtHelperService = new JwtHelperService();

  constructor(public router: Router, public service: CommonService, public Loader: Ng4LoadingSpinnerService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.appValForm = this.formBuilder.group({
      emailOrPhoneNumber: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(8)]]
    });
  }
  get f() {
    return this.appValForm.controls;
  }

  Login() {
    debugger;
    this.emailErrorMessage = LoginValidation.emailOrPhoneNumber;
    this.passwordErrorMessage = LoginValidation.password;
    this.submittedApplicationForm = true;
    this.loginVm = this.appValForm.value;
    if (this.appValForm.valid) {
      //var url = this.service.apiRoutes.Login.Login;
      debugger;
      //  this.Loader.show();
      this.service.post(this.service.apiRoutes.Login.Login, this.loginVm).subscribe(result => {
        debugger;
        this.responseVm = result.json();
        if (this.responseVm != null) {
          if (this.responseVm.status == httpStatus.Ok) {
            var decodedtoken = this.jwtHelperService.decodeToken(this.responseVm.resultData);
            if (decodedtoken.Role == loginsecurity.CRole || decodedtoken.Role == loginsecurity.ORole || decodedtoken.Role == loginsecurity.TRole || decodedtoken.Role == loginsecurity.SRole) {
              this.loginstatus = true;
              localStorage.setItem("auth_token", "Bearer " + this.responseVm.resultData);
             if(decodedtoken.Role == loginsecurity.CRole) 
               this.service.NavigateToRoute(this.service.apiUrls.User.UserDefault);
             else if (decodedtoken.Role == loginsecurity.SRole)
               this.service.NavigateToRoute(this.service.apiUrls.Supplier.Home);
            }
            else {
              this.unauthorizeduser = true;
             // this.Loader.hide();
            }
          }
          else {
            this.unauthorizeduser = true;
          //  this.Loader.hide();
            this.invalidcredentials = LoginValidation.InvalidCredentials;
          }
        }
        else {
          // this.Loader.hide();
          this.service.Notification.warning("Go To Registration");
        }
      });
    }
  }
  ForgotPassword() {
    this.service.NavigateToRouteWithQueryString(this.service.pagesUrl.CommonRegistrationPages.forgotPassword);
  }
}
