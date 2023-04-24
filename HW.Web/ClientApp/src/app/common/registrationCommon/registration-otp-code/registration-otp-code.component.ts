import { Component, OnInit } from '@angular/core';
import { BasicRegistration, ResponseVm } from '../../../models/commonModels/commonModels';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonService } from '../../../shared/HttpClient/_http';
import { RegistrationErrorMessages, loginsecurity, httpStatus } from '../../../shared/Enums/enums';
import { JwtHelperService } from '@auth0/angular-jwt/src/jwthelper.service';

@Component({
  selector: 'app-registration-otp-code',
  templateUrl: './registration-otp-code.component.html',
  styleUrls: ['./registration-otp-code.component.css']
})
export class RegistrationOtpCodeComponent implements OnInit {
  public basicRegistrationVm: BasicRegistration = new BasicRegistration();
  public response: ResponseVm = new ResponseVm();
  public appValForm: FormGroup;
  public verficationCodeErrorMessage: string;
  public responseErrorMessageCheck: boolean = false;
  public responseErrorMessage: string;
  jwtHelperService: JwtHelperService = new JwtHelperService();

  constructor(private formBuilder: FormBuilder, private service: CommonService) {
    var data = localStorage.getItem("registrationData");
    this.basicRegistrationVm = JSON.parse(data);
    if (this.basicRegistrationVm.emailAddress == null) {
      this.basicRegistrationVm.emailAddress = "N/A"
    }
    this.GetOtp();
  }

  ngOnInit() {
    this.appValForm = this.formBuilder.group(
      {
        verificationCode: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(6)]],
      }
    );
  }

  get f() { return this.appValForm.controls; }

  GetOtp() {
    this.service.post(this.service.apiRoutes.Common.getOtp, this.basicRegistrationVm).subscribe(result => {
      var data = result.json();
    })
  }

  ResendCode() {
    this.GetOtp();
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
    
    this.service.post(this.service.apiRoutes.registration.verifyOtpAndRegisterUser, this.basicRegistrationVm).subscribe(result => {
      this.response = result.json();
      if (this.response.status == httpStatus.Ok) {
        debugger;
        this.basicRegistrationVm.emailOrPhoneNumber = this.basicRegistrationVm.phoneNumber+"";
        this.service.post(this.service.apiRoutes.Login.Login, this.basicRegistrationVm).subscribe(result => {
          this.response = result.json();
          localStorage.removeItem("registrationData");
          localStorage.setItem("auth_token", "Bearer " + this.response.resultData);
          if (this.response.status == httpStatus.Ok) {
            var decodeToken = this.jwtHelperService.decodeToken(this.response.resultData);
            if (decodeToken.Role == loginsecurity.CRole) {
              alert("customer Home");
            }
            if (decodeToken.Role == loginsecurity.ORole || decodeToken.Role == loginsecurity.TRole) {
              alert("Go to Registration Registration");
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
}
