import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonService } from '../../../shared/HttpClient/_http';
import { forgotPassword, loginsecurity, httpStatus } from '../../../shared/Enums/enums';
import { ResponseVm } from '../../../models/commonModels/commonModels';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  public emailOrPhoneNumber: string="";
  public emailOrPhoneNumberErrorMessage: string="";
  public response: ResponseVm = {} as ResponseVm;
  public submittedApplicationForm: boolean = false;
  public errormessage: string="";
  public roleErrorMessage: string="";
  public roleType: string|null = "";
  public role: string="";
  public roleerror= false;
  constructor(public router: Router, public service: CommonService, public Loader: NgxSpinnerService) { }

  ngOnInit() {
    
    this.roleType = localStorage.getItem("Role");
    
    this.roleerror = false;
  }
  FindAccount() {
    this.emailOrPhoneNumber = this.emailOrPhoneNumber;
    
    if (this.emailOrPhoneNumber != null) {
    
      if (this.roleType == "1" && (this.role == null || this.role == '' || this.role == undefined)) {
        this.roleerror = true;
        this.Loader.hide();
        this.submittedApplicationForm = true;
        this.roleErrorMessage = forgotPassword.roleErrorMessage;
        setTimeout(() => {
          this.submittedApplicationForm = false;
        }, 2000);
        return;
      }
      this.Loader.show();
      if (this.role) {
        this.roleType = this.role;
      }
      
      var urls = this.service.apiRoutes.forgotPassword.forgotPassword + "?emailOrPhoneNumber=" + this.emailOrPhoneNumber + "&userRoles=" + this.roleType;
      
      this.service.get(urls).subscribe(result => {

        this.response = <ResponseVm>result;

        this.Loader.hide();
        if (this.response.status == httpStatus.Ok) {

          localStorage.setItem("forgotPassword", JSON.stringify(this.response.resultData));
          this.service.NavigateToRouteWithQueryString(this.service.pagesUrl.CommonRegistrationPages.forgotPasswordCode);
        }
        else {
          this.submittedApplicationForm = true;
          this.errormessage = this.response.message;

        }
      });
    }
    else {
      this.Loader.hide();
      this.submittedApplicationForm = true;
      this.errormessage = forgotPassword.forgotPasswordEmailOrPhoneError;
      setTimeout(() => {
        this.submittedApplicationForm = false;
      }, 2000);
    }
  }

  selectChange(event: Event) {

    this.role = (<HTMLInputElement>event.target).value;
    this.roleerror = false;
   // this.submittedApplicationForm = false;
  }
  numberOnly(event: KeyboardEvent): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
   this.submittedApplicationForm = false;
    return true;

  }
}
