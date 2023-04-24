import { Component, OnInit } from '@angular/core';
import { BasicRegistration, ResponseVm } from '../../../models/commonModels/commonModels';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonService } from '../../../shared/HttpClient/_http';
import { RegistrationErrorMessages, loginsecurity, httpStatus } from '../../../shared/Enums/enums';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-registration-otp-code',
  templateUrl: './registration-otp-code.component.html',
  styleUrls: ['./registration-otp-code.component.css']
})
export class RegistrationOtpCodeComponent implements OnInit {
  public basicRegistrationVm: BasicRegistration = {} as BasicRegistration;
  public response: ResponseVm = {} as ResponseVm;
  public appValForm: FormGroup;
  public verficationCodeErrorMessage: string="";
  public responseErrorMessageCheck: boolean = false;
  public responseErrorMessage: string="";
  public status = false;
  jwtHelperService: JwtHelperService = new JwtHelperService();

  constructor(private formBuilder: FormBuilder, private service: CommonService) {
    var data = localStorage.getItem("registrationData");
    this.basicRegistrationVm = data != null ? JSON.parse(data) : {} as BasicRegistration;
    if (this.basicRegistrationVm.emailAddress == null) {
      this.basicRegistrationVm.emailAddress = "N/A"
    }
    else {
      this.basicRegistrationVm.email = this.basicRegistrationVm.emailAddress;
    }
    this.appValForm = {} as FormGroup;
    this.GetOtp();
  }

  ngOnInit() {
   // document.getElementById("headerText").innerHTML = "Get Code";
    this.appValForm = this.formBuilder.group(
      {
        verificationCode: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(6)]],
      }
    );
  }

  get f() { return this.appValForm.controls; }

  GetOtp() {
    this.service.post(this.service.apiRoutes.Common.getOtp, this.basicRegistrationVm).subscribe(result => {
      var data = result ;
    })
  }

  ResendCode() {
    this.GetOtp();
    this.status = true;

  }
  VerifyOtpAndRegisterUser() {
    this.verficationCodeErrorMessage = RegistrationErrorMessages.verificationErrorMessage;
    
    switch (this.basicRegistrationVm.role) {
      case '1':
        this.basicRegistrationVm.role = loginsecurity.TRole;
        break;
      case '2':
        this.basicRegistrationVm.role = loginsecurity.ORole;
        break;
      case '3':
        this.basicRegistrationVm.role = loginsecurity.CRole;
        break;
      case '4':
        this.basicRegistrationVm.role = loginsecurity.SRole;
      default:
    }
    var code = this.appValForm.value;
    this.basicRegistrationVm.verificationCode = code.verificationCode;

    this.service.PostData(this.service.apiRoutes.registration.verifyOtpAndRegisterUser, this.basicRegistrationVm, true).then(result => {
      this.response = result ;
      if (this.response.status == httpStatus.Ok) {
        this.basicRegistrationVm.emailOrPhoneNumber = this.basicRegistrationVm.phoneNumber + "";
        this.service.PostData(this.service.apiRoutes.Login.Login, this.basicRegistrationVm, true).then(result => {
          this.response = result ;
          localStorage.removeItem("registrationData");
          localStorage.setItem("auth_token", "Bearer " + this.response.resultData);
          if (this.response.status == httpStatus.Ok) {
            var decodeToken = this.jwtHelperService.decodeToken(this.response.resultData);
            if (decodeToken.Role == loginsecurity.CRole) {
              this.service.NavigateToRoute(this.service.apiUrls.User.UserDefault);
            }
            if (decodeToken.Role == loginsecurity.ORole || decodeToken.Role == loginsecurity.TRole) {
              this.service.NavigateToRoute(this.service.apiUrls.Tradesman.BussinessRegistration);
            }
            if (decodeToken.Role == loginsecurity.SRole) {
              this.service.NavigateToRoute(this.service.apiUrls.Supplier.BussinesDetail);
            }
          }
        })
      }
      else {
        this.responseErrorMessageCheck = true;
        this.responseErrorMessage = this.response.message;
      }
    })

  }

  numberOnly(event: KeyboardEvent): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }


}
