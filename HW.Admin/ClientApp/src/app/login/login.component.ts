import { Component, OnInit, ElementRef, ViewChild, AfterViewInit } from '@angular/core';
import { NgxSpinnerService } from "ngx-spinner";
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '../Shared/HttpClient/_http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { apiUrls } from '../Shared/ApiRoutes/ApiRoutes';
import { httpStatus, loginsecurity, LoginValidation } from '../Shared/Enums/enums';
import { LoginVM } from '../Shared/Models/HomeModel/HomeModel';
import { elementAt } from 'rxjs/operators';
//import { debug } from 'util';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, AfterViewInit {
  @ViewChild('inputPassword') inputPassword: ElementRef;
  public toggle = true;
  
  public emailErrorMessage: string;
  public passwordErrorMessage: string;
  public unauthorizedErrorMessage: string;
  public unauthorizeduser: boolean = false;
  public submittedApplicationForm = false;
  public appValForm: FormGroup;
  public requestno = 0;
  emailorPassword: string;
  password: string;
  loginstatus: boolean = false;
  loginVm: LoginVM = new LoginVM();
  jwtHelperService: JwtHelperService = new JwtHelperService();
  rechaptcha: any[];
  constructor(public httpService: CommonService, public Loader: NgxSpinnerService, private router: Router, private formBuilder: FormBuilder) {
    this.appValForm = this.formBuilder.group({
      emailOrPhoneNumber: ['', Validators.required],
      password: ['', Validators.required]
    })
  }
  resolved(rechaptchares: any[]) {
    
    this.rechaptcha = rechaptchares;
    console.log(this.rechaptcha);
  }
  ngOnInit() {
    localStorage.clear();
    sessionStorage.clear();
  }
  ngAfterViewInit() {
    console.log();
  }
  changePasswordType() {
    this.toggle = !this.toggle;
    if (this.toggle) {
      this.inputPassword.nativeElement.type = 'password';
    }
    else {
      this.inputPassword.nativeElement.type = 'text';
    }
  }
  get f() {
    return this.appValForm.controls;
  }

  login() {
    
    this.emailErrorMessage = LoginValidation.emailOrPhoneNumber;
    this.passwordErrorMessage = LoginValidation.password;
    this.unauthorizedErrorMessage = LoginValidation.unauthorizedUser;
    this.submittedApplicationForm = true;
    var data = null;
    if (this.rechaptcha == undefined && this.rechaptcha == null) {
      this.unauthorizeduser = true;
      this.unauthorizedErrorMessage = LoginValidation.Invalidrecaptcha;
      return false;
    }


    this.loginVm = this.appValForm.value;
    if (this.appValForm.valid) {
      this.Loader.show();
      debugger;
      this.httpService.post(apiUrls.Login.Login, this.loginVm).subscribe(result => {
        debugger;
        data = result.json();
        console.log(data);
        if (data.status == httpStatus.Ok) {
          this.requestno = 0;
          var decodedtoken = this.jwtHelperService.decodeToken(data.resultData.accessToken);
          console.log(decodedtoken)
          if (decodedtoken.Role == loginsecurity.Role) {
            this.loginstatus = true;
            localStorage.setItem("auth_token", "Bearer" + data.resultData.accessToken);
            localStorage.setItem("SecurityRole", JSON.stringify(data.resultData.getSecurityRoleDetails));
            this.Loader.hide();
            this.router.navigateByUrl(this.httpService.apiRoutes.Jobs.url);
            var formData = JSON.parse(localStorage.getItem("SecurityRole"));
          }
          else {
            this.unauthorizeduser = true;
          }
        }
        else {
          if (data.message == "Blocked") {
            this.requestno = this.requestno + 1;
            this.unauthorizeduser = true;
            this.unauthorizedErrorMessage = LoginValidation.BlockedUSer;

          }
          else {
            this.requestno = this.requestno + 1;
            this.unauthorizeduser = true;
            this.unauthorizedErrorMessage = LoginValidation.InvalidCredentials;
          }
        }
        this.Loader.hide();
      },
        error => {
          this.Loader.hide();
          alert(error);
        }
      );
    }
    

  }
  forgotPassword() {
    this.router.navigateByUrl(this.httpService.apiRoutes.Login.forgotPassword);
  }
}
