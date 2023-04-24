import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { BasicRegistration, ResponseVm } from '../../../../models/commonModels/commonModels';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '../../../HttpClient/_http';
import { httpStatus, RegistrationErrorMessages } from '../../../Enums/enums';
import { SupplierProfile } from '../../../../models/supplierModels/supplierModels';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-supplierLeftmenu',
  templateUrl: './supplierLeftmenu.component.html',
})
export class SupplierLeftmenuComponent implements OnInit {
  public loginCheck: boolean = false;
  public show: boolean = false;
  public token: string | null="";
  public isVerified: boolean = false;
  public phoneNumber: number=0;
  public code: number=0;
  public submitted: boolean = false;
  public submitt: boolean = false;
  public basicRegistrationVm: BasicRegistration = {} as BasicRegistration;
  public response: ResponseVm = {} as ResponseVm;
  public appValForm: FormGroup;
  public appVal: FormGroup;
  public status: boolean = false;
  public statusCode: boolean = false;
  public responseMessage: string | undefined = "";
  public responseMessageCode: string="";
  public verficationMobileNumberErrorMessage: string="";
  public verficationCodeErrorMessage: string="";
  public profile: SupplierProfile = {} as SupplierProfile;
  public noNotCorrect: boolean = false;
  @ViewChild('varifyMobileNumberModel', { static: true }) varifyMobileNumberModel: ModalDirective;
  @ViewChild('varifyAcountModel', { static: true }) varifyAcountModel: ModalDirective;
  constructor(private common: CommonService, private formBuilder: FormBuilder) {
    this.appVal = {} as FormGroup;
    this.appValForm = {} as FormGroup;
    this.varifyMobileNumberModel = {} as ModalDirective;
    this.varifyAcountModel = {} as ModalDirective;
  }

  ngOnInit() {
    this.token = localStorage.getItem("auth_token");
    if (this.token != null && this.token != '') {
      this.loginCheck = true;
    }
    else {
      this.loginCheck = false;
    }

    this.appValForm = this.formBuilder.group(
      {
        verificationMobileNumber: ['', [Validators.required, Validators.pattern("^[0-9]{4}[0-9]{7}$")]],
      }
    );
     this.noNotCorrect = false;
    this.appVal = this.formBuilder.group(
      {
        verificationCode: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(6)]],
      }
    );
    this.GetVerifyStatus();
  }
  showHideNav() {
    this.show = !this.show;
  }
  public GetVerifyStatus() {
    if (this.token != null) {
      this.common.GetData(this.common.apiRoutes.IdentityServer.GetPhoneNumberVerificationStatus + "?userId=" + this.token, true).then(result => {
        this.isVerified = result ;
      });
    }
  }

  get f() { return this.appValForm.controls; }

  get g() { return this.appVal.controls; }


  public VarifyMobileNumber() {
    
    this.verficationMobileNumberErrorMessage = RegistrationErrorMessages.verificationPhoneNumberErrorMessage;
    this.submitted = true;
    if (this.appValForm.invalid) {
      return;
    }

    this.common.GetData(this.common.apiRoutes.Supplier.GetBusinessAndPersnalProfileWeb, true).then(result => {
      if (status = httpStatus.Ok) {
        this.profile = result ;
        
        if (this.profile.persnalDetails.mobileNumber == this.appValForm.value.verificationMobileNumber) {
          this.noNotCorrect = false;
          this.phoneNumber = this.appValForm.value.verificationMobileNumber;
          if (this.phoneNumber != null && this.noNotCorrect ==false) {
            this.SentOtpCode();
          }
        }
        else {
          this.noNotCorrect = true;
          setTimeout(() => {
            this.noNotCorrect = false;
          }, 2000);
        }
      }
    })
  }

  public SentOtpCode() {
    ;
    this.basicRegistrationVm.phoneNumber = this.phoneNumber.toString();
    this.common.PostData(this.common.apiRoutes.Common.getOtp, this.basicRegistrationVm, true).then(result => {
      if (status = httpStatus.Ok) {
        this.varifyMobileNumberModel.hide();
        this.varifyAcountModel.show();
      }
      ;
      this.response = result ;
      this.status = true;
      this.responseMessage = this.response.message;
    });
  }

  public VarifyAccount() {
    
    this.verficationCodeErrorMessage = RegistrationErrorMessages.verificationErrorMessage;
    this.submitt = true;
    if (this.appVal.invalid) {
      return;
    }
    this.code = this.appVal.value.verificationCode;
    this.common.GetData(this.common.apiRoutes.Common.VerifyOtp + "?code=" + this.code + "&phoneNumber=" + this.phoneNumber + "&userId=" + this.token, true).then(result => {
      if (status = httpStatus.Ok) {
        
        this.response = result ;
        if (this.response.status == 400) {
          this.statusCode = true;
        }
        else {
          
          this.statusCode = false;
          this.GetVerifyStatus();
          this.varifyAcountModel.hide();
        }
       
      }
    });
  }

  public ResendCode() {
    this.SentOtpCode();
  }
  public ShowPopup() {
    this.statusCode = false;
    this.varifyMobileNumberModel.show();
  }

  public CloseModal() {
    this.appValForm.reset();
    this.varifyMobileNumberModel.hide();
  }
  numberOnly(event: KeyboardEvent): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }

    return true;

  }
}
