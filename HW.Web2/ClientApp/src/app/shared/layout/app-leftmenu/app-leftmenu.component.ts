import { Component, Input, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonService } from '../../HttpClient/_http';
import { BasicRegistration, ResponseVm } from '../../../models/commonModels/commonModels';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { httpStatus, RegistrationErrorMessages } from '../../Enums/enums';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { debounce, delay } from 'rxjs/operators';
import { pipe, Subscription } from 'rxjs';
import { Customer } from '../../../models/userModels/userModels';
import { ToastrService } from 'ngx-toastr';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Events } from '../../../common/events';
import { IApplicationSetting } from '../../Interface/tradesman';
import { keyValueAutoComplete } from '../../../models/commonModels/keyValue';
import { ISkill } from '../../Enums/Interface';

@Component({
  selector: 'app-leftmenu',
  templateUrl: './app-leftmenu.component.html',
  styleUrls: ['./app-leftmenu.component.css']
})
export class AppLeftmenuComponent implements OnInit {
  public loginCheck: boolean = false;
  public show: boolean = false;
  public token: string|null="";
  public userRole: string="";
  public userId: string="";
  public isVerified: boolean = true;
  public phoneNumber: number=0;
  public code: number=0;
  public submitted: boolean = false;
  public IsRunning: boolean = false;
  public submitt: boolean = false;
  public basicRegistrationVm: BasicRegistration = {} as BasicRegistration;
  public response: ResponseVm = {} as ResponseVm;
  public appValForm: FormGroup;
  public appVal: FormGroup;
  display: any;
  public status: boolean = false;
  public statusCode: boolean = false;
  public responseMessage: string="";
  public responseMessageCode: string="";
  public verificationCode: number=0;
  public verficationMobileNumberErrorMessage: string="";
  public profile: Customer = {} as  Customer;
  public verficationCodeErrorMessage: string="";
  public noNotCorrect: boolean=false;
  public enabled: boolean = true;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  //public loggedUserDetails;
  public statusMessage: number=0;
  public incompletedProfile = false;
  public eventSubscription: Subscription;
  public settingList: IApplicationSetting[] = [];
  public isActiveTopup: boolean = false;
  public btnMarketPlace: boolean = false;
  skillDetails: ISkill = {} as ISkill;
  //Input
  @Input() skill: ISkill = {} as ISkill;;
  @ViewChild('varifyMobileNumberModel', { static: true }) varifyMobileNumberModel: ModalDirective;
  @ViewChild('varifyAcountModel', { static: true }) varifyAcountModel: ModalDirective;
  @ViewChild('verifyAccountMessageModal', { static: false }) verifyAccountMessageModal: ModalDirective;


  constructor(private common: CommonService, private event: Events, public toastr: ToastrService, private formBuilder: FormBuilder) {
    this.eventSubscription = this.event.profile_Completed.subscribe(res => {
      let profileCompleted = localStorage.getItem("profileCompleted");
      if (profileCompleted != null)
        this.incompletedProfile = profileCompleted == "false" ? false : true;
      localStorage.removeItem("profileCompleted");
    });
    this.eventSubscription = this.event.account_verfication.subscribe(res => {    
      let accountVerfication = localStorage.getItem("accountVerfication");
      if (accountVerfication != null)
        this.isVerified = accountVerfication == "true" ? true : false;
      localStorage.removeItem("accountVerfication");
    });
    this.appVal = {} as FormGroup;
    this.appValForm = {} as FormGroup;
    this.varifyMobileNumberModel = {} as ModalDirective;
    this.varifyAcountModel = {} as ModalDirective;
    this.verifyAccountMessageModal = {} as ModalDirective;
  }
  ngOnChanges(changes: SimpleChanges) {
    this.skillDetails = changes['skill'].currentValue;
  }
  ngOnInit() {

    this.getSettingList();
    this.token = localStorage.getItem("auth_token");
    var decodedtoken = this.token!=null ? this.jwtHelperService.decodeToken(this.token):"";
    this.userRole = decodedtoken.Role;
    this.userId = decodedtoken.UserId;

    if (this.token != null && this.token != '') {
      this.loginCheck = true;
    }
    else {
      this.loginCheck = false;
    }
    //this.noNotCorrect = false;
    this.appValForm = this.formBuilder.group(
      {
        verificationMobileNumber: ['', [Validators.required, Validators.pattern("^[0-9]{4}[0-9]{7}$")]],
      }
    );

    this.appVal = this.formBuilder.group(
      {
        verificationCode: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(6)]],
      }
    );
    
    this.GetVerifyStatus();
    this.getSupplierBusinessDetails(this.userRole);
  }
  ngOnDestroy() {
    if (this.eventSubscription) {
      this.eventSubscription.unsubscribe();
    }
  }
  getSupplierBusinessDetails(userRole: string) {
    if (userRole == "Supplier") {
      this.common.get(this.common.apiRoutes.Supplier.GetBusinessProfile).subscribe(result => {
        let res = <any>result ;
        if (res != null) {
          if (res.companyName == null || res.companyName == "") {
            this.incompletedProfile = true;
          }
          else {
            this.incompletedProfile = false;
          }
        }
        else {
          this.incompletedProfile = true;
        }
      })
    }
  }

  public onClickedOutside(e: Event) {
    console.log(e.srcElement);
  }
  public GetVerifyStatus() {
    if (this.token != null) {
      this.common.get(this.common.apiRoutes.IdentityServer.GetPhoneNumberVerificationStatus + "?userId=" + this.token).subscribe(result => {
        this.isVerified = <boolean>result;

      });
    }
  }

  get f() { return this.appValForm.controls; }

  get g() { return this.appVal.controls; }

  timer(minute: number) {
    
    this.IsRunning = true;
    let seconds: number = minute * 60;
    let textSec: any = "0";
    let statSec: number = 60;

    const prefix = minute < 10 ? "0" : "";

    const timer = setInterval(() => {
      seconds--;
      if (statSec != 0) statSec--;
      else statSec = 59;

      if (statSec < 10) {
        textSec = "0" + statSec;
      } else textSec = statSec;

      this.display = `Your OTP will be Expired in: ${prefix}${Math.floor(seconds / 60)}:${textSec}`;

      if (seconds == 0) {
        this.IsRunning = false;
        this.display = "";
        console.log("finished");
        clearInterval(timer);
      }
    }, 1000);
  }

  public VarifyMobileNumber() {

    this.verficationMobileNumberErrorMessage = RegistrationErrorMessages.verificationPhoneNumberErrorMessage;
    this.submitted = true;
    if (this.appValForm.invalid) {
      return this.appValForm.markAllAsTouched();
    }
    this.common.GetData(this.common.apiRoutes.UserManagement.GetUserDetailsByUserRole + `?userRole=${this.userRole}&userId=${this.userId}`, true).then(result => {
      
      if (status = httpStatus.Ok) {
        this.profile = result ;
        if (this.profile.mobileNumber == this.appValForm.value.verificationMobileNumber) {
          this.noNotCorrect = false;
          this.phoneNumber = this.appValForm.value.verificationMobileNumber;
          if (this.phoneNumber !=null  && this.noNotCorrect == false) {
            this.SentOtpCode();
            
            if (!this.IsRunning) {
              this.timer(1);
            }
          }
        }
        else {
          this.noNotCorrect = true;
          setTimeout(() => {
            this.noNotCorrect = false;
          }, 2000);
        }
      }
    });
  }

  public showhide() {
    this.isVerified = !this.isVerified
  }

  public SentOtpCode() {

    this.basicRegistrationVm.phoneNumber = this.phoneNumber.toString();
    this.statusMessage = this.phoneNumber;
    //this.statusMessage = this.basicRegistrationVm.email != "" ? this.basicRegistrationVm.email : this.basicRegistrationVm.phoneNumber;
    this.common.PostData(this.common.apiRoutes.Common.getOtp, this.basicRegistrationVm, true).then(result => {
      if (status = httpStatus.Ok) {
        this.varifyMobileNumberModel.hide();
        this.varifyAcountModel.show();
        this.varifyAcountModel.config.backdrop = 'static';
        this.varifyAcountModel.config.keyboard = false;
      }
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
    this.common.GetData(this.common.apiRoutes.Common.VerifyOtp + "?code=" + this.code + "&phoneNumber=" + this.phoneNumber + "&userId=" + this.token, false).then(result => {
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
    this.appVal.reset();
    this.SentOtpCode();
    if (!this.IsRunning) {
      this.timer(1);
    }
  }


  showHideNav() {

    this.show = !this.show;
  }
  //public onSubmit() {
  //  this.submitted = true;
  //}

  public ShowPopup() {

    if (this.userRole != 'Customer') {
      let leftMenuWrapper = document.getElementById("nd-ap");
      if (leftMenuWrapper !== null) {
        let screen_width = document.documentElement.clientWidth;
        if (screen_width <= 767) {
          leftMenuWrapper.style.zIndex = 'unset';
        }
      }
      this.statusCode = false;
      this.varifyMobileNumberModel.show();
    }
    else
      this.common.NavigateToRoute(this.common.apiUrls.User.PersonalProfile);
  }

  public CloseModal() {
    let leftMenuWrapper = document.getElementById("nd-ap");
    if (leftMenuWrapper !== null) {
      let screen_width = document.documentElement.clientWidth;
      if (screen_width <= 767) {
        leftMenuWrapper.style.zIndex = '2';
      }
    }
    this.appValForm.reset();
    this.varifyMobileNumberModel.hide();
  }

  public hideModal() {
    this.varifyAcountModel.hide();
    let leftMenuWrapper = document.getElementById("nd-ap");
    if (leftMenuWrapper !== null) {
      let screen_width = document.documentElement.clientWidth;
      if (screen_width <= 767) {
        leftMenuWrapper.style.zIndex = '2';
      }
    }

  }

  public getSettingList() {
    
    this.common.get(this.common.apiRoutes.UserManagement.GetSettingList).subscribe(res => {
      this.settingList = <IApplicationSetting[]>res ;
      
      if (this.settingList.length > 0) {
        this.settingList.forEach(x => {
          if (x.settingName == "Topup" && x.isActive) {
            this.isActiveTopup = true;
          }
        })
        this.settingList.forEach(y => {
          if (y.settingName == "MarketPlace"  && y.isActive) {
            this.btnMarketPlace  = true;
          }
        })
      }
    })
  }


  numberOnly(event: KeyboardEvent): boolean {
    //this.noNotCorrect = true;
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }

  public checkVarifyAccount(url: string) {
    if (this.token != null) {
      this.common.GetData(this.common.apiRoutes.IdentityServer.GetPhoneNumberVerificationStatus + "?userId=" + this.token, true).then(result => {
        if (result  == true) {
          this.common.NavigateToRoute(url);
        }
        else {
          this.verifyAccountMessageModal.show();
        }

      });
    }
  }

  public verifyAccount() {
    this.verifyAccountMessageModal.hide();
    if (this.userRole == "Customer") {
      this.common.NavigateToRoute(this.common.apiUrls.User.PersonalProfile);
    }
    else {
      this.varifyMobileNumberModel.show();
      // this.common.NavigateToRoute(this.common.apiUrls.Tradesman.Profile);
    }
  }

  public closeVerifyAccountMessageModal() {
    this.verifyAccountMessageModal.hide();
  }


}
