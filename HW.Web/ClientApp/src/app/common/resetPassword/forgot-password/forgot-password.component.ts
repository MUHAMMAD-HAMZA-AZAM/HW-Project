import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonService } from '../../../shared/HttpClient/_http';
import { forgotPassword, loginsecurity, httpStatus } from '../../../shared/Enums/enums';
import { ResponseVm } from '../../../models/commonModels/commonModels';
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  private emailOrPhoneNumber: string;
  private emailOrPhoneNumberErrorMessage: string;
  private response: ResponseVm = new ResponseVm();
  public submittedApplicationForm: boolean = false;
  public errormessage: string;
  constructor(public router: Router, public service: CommonService, public Loader: Ng4LoadingSpinnerService) { }

  ngOnInit() {
  }
  FindAccount() {
    debugger;
    if (this.emailOrPhoneNumber != null) {
      this.Loader.show();
      var urls = this.service.apiRoutes.forgotPassword.forgotPassword + "?emailOrPhoneNumber=" + this.emailOrPhoneNumber;
      this.service.get(urls).subscribe(result => {
        this.response = result.json();
        this.Loader.hide();
        if (this.response.status == httpStatus.Ok) {
          localStorage.setItem("forgotPassword", JSON.stringify(this.response.resultData));
          this.service.NavigateToRouteWithQueryString(this.service.pagesUrl.CommonRegistrationPages.forgotPasswordCode);
        }
        else {
          this.submittedApplicationForm = true;
          this.errormessage = this.response.message;
        }
      })
    }
    else {
      this.Loader.hide();
      this.submittedApplicationForm = true;
      this.errormessage = forgotPassword.forgotPasswordEmailOrPhoneError;
    }
  }
}
