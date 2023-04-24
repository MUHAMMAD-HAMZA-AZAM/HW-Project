import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '../../../shared/HttpClient/_http';
import { BasicRegistration, IdValueVm, LoginVM, ResponseVm, CheckEmailandPhoneNumberAvailability } from '../../../models/commonModels/commonModels';
import { RegistrationErrorMessages, httpStatus, loginsecurity, LoginValidation } from '../../../shared/Enums/enums';
import { ActivatedRoute, Params } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { SocialAuthService, SocialUser } from "angularx-social-login";
import { FacebookLoginProvider, GoogleLoginProvider } from "angularx-social-login";
import { ToastrService } from 'ngx-toastr';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { metaTagsService } from '../../../shared/CommonServices/seo-dynamictags.service';
import { IPageSeoVM } from '../../../shared/Enums/Interface';

@Component({
  selector: 'app-registration-basic',
  templateUrl: './registration-basic.component.html',
  styleUrls: ['./registration-basic.component.css']
})
export class RegistrationBasicComponent implements OnInit {
  public appValForm: FormGroup;
  public check: { T: boolean, O: boolean, U: boolean, S: boolean } = { T: false, O: false, U: false, S: false };
  public roleType: string|null="";
  public response: ResponseVm = {} as ResponseVm;
  public subAccountResponse: ResponseVm = {} as ResponseVm;
  public submitted = false;
  public basicRegistrationVm: BasicRegistration = {} as BasicRegistration;
  public checkEmailEvailabilityVm: CheckEmailandPhoneNumberAvailability = {} as CheckEmailandPhoneNumberAvailability;
  public idValueVm: IdValueVm[] = [];
  public firstNameErrorMessage: string="";
  public lastNameErrorMessage: string="";
  public dataOfBirthErrorMessage: string="";
  public passwordErrorMessage: string="";
  public genderErrorMessage: string="";
  public cityErrorMessage: string="";
  public roleErrorMessage: string="";
  public emailErrorMessage: string="";
  public termsAndConditionErrorMessage: string="";
  public userAvailabilty: boolean = false;
  public userAvailabiltyErrorMessage: string="";
  public RoleTypeErrorMessage: string = "";
  public roleId: string="";
  //public pipe;
  public type = "Password";
  private user: SocialUser;
  private loggedIn: boolean = false;
  private firstName: string="";
  private lastName: string="";
  private emailAddress: string="";
  private token: string="";
  private facebookId: string="";
  private googleId: string="";
  public disableInput: boolean = false;
  public startDate: Date = new Date;
  public registrationMaxdate: string = "";
  public loginVm: LoginVM = {} as LoginVM;
  public loginstatus: boolean = false;
  public unauthorizeduser: boolean = false;
  public isUserBlocked: boolean = false;
  public invalidcredentials: string = "";
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  @ViewChild("phoneNumber", { static: true }) phoneNumber: ElementRef;
  @ViewChild("switchBtn", { static: true }) switchBtn: ElementRef;
  @ViewChild("datePicker", { static: true }) datePicker: ElementRef;
  @ViewChild('blockAccountMessageModal', { static: false }) blockAccountMessageModal: ModalDirective;
  constructor(private formBuilder: FormBuilder, private common: CommonService, private route: ActivatedRoute,
    private authService: SocialAuthService, private toaster: ToastrService, private _metaService: metaTagsService) {
    this.blockAccountMessageModal = {} as ModalDirective;
    this.common.get(this.common.apiRoutes.Common.getCityList).subscribe(result => {
      this.user = {} as SocialUser;
      this.idValueVm = <IdValueVm[]>result ;
      
    })
    this.phoneNumber = {} as ElementRef;
    this.switchBtn = {} as ElementRef;
    this.datePicker = {} as ElementRef;
    this.user = {} as SocialUser;
    this.appValForm = {} as FormGroup;
    this.roleType = localStorage.getItem("Role");
    this.route.queryParams.subscribe((params: Params) => {
      this.firstName = params['firstName'];
      this.lastName = params['lastName'];
      this.emailAddress = params['emailAddress'];
      this.token = params['token'];
      this.facebookId = params['facebookId'];
      this.googleId = params['googleuserId'];
    });
  }
  ngOnInit() {
    switch (this.roleType) {
      case '1':
        this.check.T = true;
        this.check.O = false;
        this.check.S = false;
        this.check.U = false;
        break;
      //case '2':
      //  this.check.O = true;
      //  this.check.T = false;
      //  this.check.S = false;
      //  this.check.U = false;
      //  break;
      case '3':
        this.check.U = true;
        this.check.O = false;
        this.check.T = false;
        this.check.S = false;
        break;
      case '4':
        this.check.S = true;
        this.check.U = false;
        this.check.O = false;
        this.check.T = false;
      default:
    }
    this.startDate = new Date();
    this.startDate.setFullYear(this.startDate.getFullYear() - 10);
    var a = this.startDate;
    this.registrationMaxdate = this.common.formatDate(a, "YYYY-MM-DD");
    this.bindMetaTags();
    this.appValForm = this.formBuilder.group(
      {
        firstName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(25)]],
        lastName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(25)]],
        dateOfBirth: [null, Validators.required],
        emailAddress: ['', Validators.pattern("[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$")],
        cnic: ['', [Validators.required, Validators.minLength(13), Validators.maxLength(13), Validators.pattern(/^[0-9]*$/)]],
        gender: ['', Validators.required],
        phoneNumber: ['', [Validators.required, Validators.minLength(11), Validators.maxLength(11), Validators.pattern(/^[0-9]*$/)]],
        city: ['', Validators.required],
        role: [''],
        termsAndConditions: [true, Validators.requiredTrue],
        tab1: ['', Validators.required],
        tab2: ['', Validators.required],
        tab3: ['', Validators.required],
        tab4: ['', Validators.required],
        tab5: ['', Validators.required],
      }
    );

    if (this.facebookId) {
      
      this.appValForm.controls.firstName.setValue(this.firstName);
      this.appValForm.controls.lastName.setValue(this.lastName);
      this.appValForm.controls.emailAddress.setValue(this.emailAddress);
      this.appValForm.controls.tab1.setValue("f");
      this.appValForm.controls.tab2.setValue("b");
      this.appValForm.controls.tab3.setValue("1");
      this.appValForm.controls.tab4.setValue("2");
      this.appValForm.controls.tab5.setValue("3");
      this.appValForm.controls.phoneNumber.clearValidators();
      this.appValForm.controls.phoneNumber.updateValueAndValidity();
      this.appValForm.controls.termsAndConditions.clearValidators();
      this.appValForm.controls.termsAndConditions.updateValueAndValidity();
      //this.phoneNumber.nativeElement.style.display = 'none';
      this.disableInput = true;
      this.VerifyAndSendOtp();
    }
    else if (this.googleId) {
      this.appValForm.controls.firstName.setValue(this.firstName);
      this.appValForm.controls.lastName.setValue(this.lastName);
      this.appValForm.controls.emailAddress.setValue(this.emailAddress);
      this.appValForm.controls.tab1.setValue("g");
      this.appValForm.controls.tab2.setValue("o");
      this.appValForm.controls.tab3.setValue("1");
      this.appValForm.controls.tab4.setValue("2");
      this.appValForm.controls.tab5.setValue("3");
      this.appValForm.controls.phoneNumber.clearValidators();
      this.appValForm.controls.phoneNumber.updateValueAndValidity();
      this.appValForm.controls.termsAndConditions.clearValidators();
      this.appValForm.controls.termsAndConditions.updateValueAndValidity();
      //this.phoneNumber.nativeElement.style.display = 'none';
      this.disableInput = true;
      this.VerifyAndSendOtp();
    }
   
  }
  get f() { return this.appValForm.controls; }
  public signInWithGoogle(): void {
    if (this.common.IsUserLogIn()) {
      this.toaster.error("Already Login");
    }
    else {
    this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);
    this.authService.authState.subscribe((user) => {
      this.user = user;
      this.loggedIn = (user != null);
      if (this.loggedIn) {
        switch (this.roleType) {
          case '1':
            this.loginVm.role = "Tradesman"
            break;
          case '2':
            this.loginVm.role = "Organization"
            break;
          case '3':
            this.loginVm.role = "Customer"
            break;
          case '4':
            this.loginVm.role = "Supplier"
          default:
        }

        let obj = {
          facebookId: this.user.id,
          role: this.roleType,
          email: ''
        };

        this.common.PostData(this.common.apiRoutes.IdentityServer.GetUserByFacebookId, obj, true).then(res => {
          this.response = {} as ResponseVm;
          this.response = res;
          if (this.response.status == httpStatus.Restricted) {
            this.loginVm.emailOrPhoneNumber = this.user.email;
            this.loginVm.password = "P@ssgo123";
            this.loginVm.facebookClientId = "";
            this.loginVm.googleClientId = this.user.id;
            if (this.loginVm.emailOrPhoneNumber == null || this.loginVm.emailOrPhoneNumber == "") {
              this.loginVm.emailOrPhoneNumber = this.user.id;
            }
            this.common.PostData(this.common.apiRoutes.Login.Login, this.loginVm, true).then(result => {
              this.response = result;
              if (this.response != null) {
                if (this.response.status == httpStatus.Ok) {
                  var decodedtoken = this.jwtHelperService.decodeToken(this.response.resultData);
                  this.common.GetData(this.common.apiRoutes.Login.GetUserBlockStatus + "?userId=" + decodedtoken.UserId).then(result => {
                    let blockStatus = result;
                    if (!blockStatus) {
                      if (decodedtoken.Role == loginsecurity.CRole || decodedtoken.Role == loginsecurity.ORole || decodedtoken.Role == loginsecurity.TRole || decodedtoken.Role == loginsecurity.SRole) {
                        this.loginstatus = true;
                        localStorage.removeItem("Role");
                        localStorage.removeItem("Show");
                        localStorage.setItem("auth_token", "Bearer " + this.response.resultData);
                        if (decodedtoken.Role == loginsecurity.CRole)
                          this.common.NavigateToRoute(this.common.apiUrls.User.UserDefault);
                        else if (decodedtoken.Role == loginsecurity.SRole)
                          this.common.NavigateToRoute(this.common.apiUrls.Supplier.Home);
                        else if (decodedtoken.Role == loginsecurity.TRole || decodedtoken.Role == loginsecurity.ORole)
                          this.common.NavigateToRoute(this.common.apiUrls.Tradesman.LiveLeads);
                      }
                      else {
                        this.unauthorizeduser = true;
                      }
                    }
                    else {
                      this.blockAccountMessageModal.show();
                      setTimeout(() => {
                        this.blockAccountMessageModal.hide();
                      }, 5000);
                    }

                  }, error => {
                    console.log(error);
                  });
                }
                else {
                  this.unauthorizeduser = true;
                  this.invalidcredentials = LoginValidation.InvalidCredentials;
                }
              }
              else {
                this.common.Notification.warning("Go To Registration");
              }
            });
          }
          else {
            if (this.user.provider == "GOOGLE") {
              this.appValForm.controls.firstName.setValue(this.user.firstName);
              this.appValForm.controls.lastName.setValue(this.user.lastName);
              this.appValForm.controls.emailAddress.setValue(this.user.email);
              this.appValForm.controls.tab1.setValue("g");
              this.appValForm.controls.tab2.setValue("o");
              this.appValForm.controls.tab3.setValue("1");
              this.appValForm.controls.tab4.setValue("2");
              this.appValForm.controls.tab5.setValue("3");
              this.googleId = this.user.id;
              this.appValForm.controls.phoneNumber.clearValidators();
              this.appValForm.controls.phoneNumber.updateValueAndValidity();
              this.appValForm.controls.termsAndConditions.clearValidators();
              this.appValForm.controls.termsAndConditions.updateValueAndValidity();
              this.appValForm.controls.city.clearValidators();
              this.appValForm.controls.city.updateValueAndValidity();
              //this.phoneNumber.nativeElement.style.display = 'none';
              this.disableInput = true;
              this.VerifyAndSendOtp();

            }
          }
        });
      }
    });
    }

  }
  public signInWithFB(): void {
    if (this.common.IsUserLogIn()) {
      this.toaster.error("Already Login");
    }
    else {
    this.authService.signIn(FacebookLoginProvider.PROVIDER_ID);
    this.authService.authState.subscribe((user) => {
      this.user = user;
      this.loggedIn = (user != null);
      if (this.loggedIn) {

        switch (this.roleType) {
          case '1':
            this.loginVm.role = "Tradesman"
            break;
          case '2':
            this.loginVm.role = "Organization"
            break;
          case '3':
            this.loginVm.role = "Customer"
            break;
          case '4':
            this.loginVm.role = "Supplier"
          default:
        }

        let obj = {
          facebookId: this.user.id,
          role: this.roleType,
          email: this.user.email
        };

        this.common.PostData(this.common.apiRoutes.IdentityServer.GetUserByFacebookId, obj, true).then(res => {
          this.response = {} as ResponseVm;
          this.response = res;
          if (this.response.status == httpStatus.Restricted) {
            this.loginVm.emailOrPhoneNumber = this.user.email;
            this.loginVm.password = "P@ssfb123";
            this.loginVm.facebookClientId = this.user.id;
            this.loginVm.googleClientId = "";
            if (this.loginVm.emailOrPhoneNumber == null || this.loginVm.emailOrPhoneNumber == "") {
              this.loginVm.emailOrPhoneNumber = this.user.id;
            }
            this.common.PostData(this.common.apiRoutes.Login.Login, this.loginVm, true).then(result => {
              this.response = result;
              if (this.response != null) {
                if (this.response.status == httpStatus.Ok) {
                  var decodedtoken = this.jwtHelperService.decodeToken(this.response.resultData);
                  this.common.GetData(this.common.apiRoutes.Login.GetUserBlockStatus + "?userId=" + decodedtoken.UserId).then(result => {
                    let blockStatus = result;
                    if (!blockStatus) {
                      if (decodedtoken.Role == loginsecurity.CRole || decodedtoken.Role == loginsecurity.ORole || decodedtoken.Role == loginsecurity.TRole || decodedtoken.Role == loginsecurity.SRole) {
                        this.loginstatus = true;
                        localStorage.removeItem("Role");
                        localStorage.removeItem("Show");
                        localStorage.setItem("auth_token", "Bearer " + this.response.resultData);
                        if (decodedtoken.Role == loginsecurity.CRole)
                          this.common.NavigateToRoute(this.common.apiUrls.User.UserDefault);
                        else if (decodedtoken.Role == loginsecurity.SRole)
                          this.common.NavigateToRoute(this.common.apiUrls.Supplier.Home);
                        else if (decodedtoken.Role == loginsecurity.TRole || decodedtoken.Role == loginsecurity.ORole)
                          this.common.NavigateToRoute(this.common.apiUrls.Tradesman.LiveLeads);
                      }
                      else {
                        this.unauthorizeduser = true;
                      }
                    }
                    else {
                      this.blockAccountMessageModal.show();
                      setTimeout(() => {
                        this.blockAccountMessageModal.hide();
                      }, 5000);
                    }

                  }, error => {
                    console.log(error);
                  });
                }
                else {
                  this.unauthorizeduser = true;
                  this.invalidcredentials = LoginValidation.InvalidCredentials;
                }
              }
              else {
                this.common.Notification.warning("Go To Registration");
              }
            });
          }
          else {
            if (this.user.provider == "FACEBOOK") {
              this.appValForm.controls.firstName.setValue(this.user.firstName);
              this.appValForm.controls.lastName.setValue(this.user.lastName);
              this.appValForm.controls.emailAddress.setValue(this.user.email);
              this.appValForm.controls.tab1.setValue("f");
              this.appValForm.controls.tab2.setValue("b");
              this.appValForm.controls.tab3.setValue("1");
              this.appValForm.controls.tab4.setValue("2");
              this.appValForm.controls.tab5.setValue("3");
              this.facebookId = this.user.id;
              this.appValForm.controls.phoneNumber.clearValidators();
              this.appValForm.controls.phoneNumber.updateValueAndValidity();
              this.appValForm.controls.termsAndConditions.clearValidators();
              this.appValForm.controls.termsAndConditions.updateValueAndValidity();
              this.appValForm.controls.city.clearValidators();
              this.appValForm.controls.city.updateValueAndValidity();
              //this.phoneNumber.nativeElement.style.display = 'none';
              this.disableInput = true;
              this.VerifyAndSendOtp();
            }

          }
        });
      }
    });
    }
  }
  VerifyAndSendOtp() {
    
    this.firstNameErrorMessage = RegistrationErrorMessages.firstNameErrorMessage;
    this.lastNameErrorMessage = RegistrationErrorMessages.lastNameErrorMessage;
    this.dataOfBirthErrorMessage = RegistrationErrorMessages.dataOfBirth;
    this.genderErrorMessage = RegistrationErrorMessages.genderErrorMessage;
    this.cityErrorMessage = RegistrationErrorMessages.cityErrorMessage;
    this.roleErrorMessage = RegistrationErrorMessages.roleErrorMessage;
    this.emailErrorMessage = RegistrationErrorMessages.emailErrorMessage;
    this.passwordErrorMessage = RegistrationErrorMessages.passwordErrorMessage;
    this.termsAndConditionErrorMessage = RegistrationErrorMessages.termsAndConditionErrorMessage;
    this.submitted = true;

    if (this.roleType == "1" || this.roleType == "4" || this.roleType == "2") {
      this.appValForm.controls.role.setValidators([Validators.required]);
      this.appValForm.controls.role.updateValueAndValidity();
      //this.appValForm.controls.emailAddress.clearValidators();
      //this.appValForm.controls.emailAddress.updateValueAndValidity();
      this.appValForm.controls.cnic.clearValidators();
      this.appValForm.controls.cnic.updateValueAndValidity();
    }
    if (this.roleType == "3") {

      this.appValForm.controls.dateOfBirth.clearValidators();
      this.appValForm.controls.dateOfBirth.updateValueAndValidity();

      this.appValForm.controls.cnic.clearValidators();
      this.appValForm.controls.cnic.updateValueAndValidity();

      this.appValForm.controls.gender.clearValidators();
      this.appValForm.controls.gender.updateValueAndValidity();

      //this.appValForm.controls.emailAddress.clearValidators();
      //this.appValForm.controls.emailAddress.updateValueAndValidity();

      this.appValForm.controls.city.clearValidators();
      this.appValForm.controls.city.updateValueAndValidity();
      //this.appValForm.controls.phoneNumber.clearValidators();
      //this.appValForm.controls.phoneNumber.updateValueAndValidity();

    }
    if (this.appValForm.value.role == null || this.appValForm.value.role == "")
     // this.appValForm.value.role = this.roleType;
      this.appValForm.controls.role.setValue ( this.roleType);
    
    this.basicRegistrationVm = this.appValForm.value;
    this.basicRegistrationVm.password = "P@ss"+this.appValForm.value.tab1+this.appValForm.value.tab2+this.appValForm.value.tab3+this.appValForm.value.tab4+this.appValForm.value.tab5;

    if (this.facebookId) {
      this.basicRegistrationVm.facebookUserId = this.facebookId;
      this.basicRegistrationVm.googleUserId = "";
    }
    else if (this.googleId) {
      this.basicRegistrationVm.googleUserId = this.googleId;
      this.basicRegistrationVm.facebookUserId = "";
    }
    else {
      this.basicRegistrationVm.googleUserId = "";
            this.basicRegistrationVm.facebookUserId = "";
    }
    
    if (this.appValForm.valid) {
      
      
      switch (this.basicRegistrationVm.role) {
        case '1':
          localStorage.setItem("TorSrole", this.basicRegistrationVm.role);
          this.basicRegistrationVm.role = loginsecurity.TRole;
          break;
        case '2':
          localStorage.setItem("TorSrole", this.basicRegistrationVm.role);
          this.basicRegistrationVm.role = loginsecurity.ORole;
          break;
        case '3':
          this.basicRegistrationVm.role = loginsecurity.CRole;
          break;
        case '4':
          this.basicRegistrationVm.role = loginsecurity.SRole;
        default:
      }
      this.checkEmailEvailabilityVm.email = this.basicRegistrationVm.emailAddress;
      this.checkEmailEvailabilityVm.phoneNumber = this.basicRegistrationVm.phoneNumber;
      this.checkEmailEvailabilityVm.role = this.basicRegistrationVm.role;
      this.checkEmailEvailabilityVm.googleUserId = this.basicRegistrationVm.googleUserId;
      this.checkEmailEvailabilityVm.facebookUserId = this.basicRegistrationVm.facebookUserId;
      this.checkEmailEvailabilityVm.password = this.basicRegistrationVm.password;
      this.basicRegistrationVm.facebookClientId = this.basicRegistrationVm.facebookUserId;
      this.basicRegistrationVm.googleClientId = this.basicRegistrationVm.googleUserId;
      this.basicRegistrationVm.city = this.basicRegistrationVm.city;

      
      this.common.PostData(this.common.apiRoutes.registration.CheckEmailandPhoneNumberAvailability, this.checkEmailEvailabilityVm, true).then(result => {
        
        this.response = result ;
        
        if (this.response.status == httpStatus.Ok) {
          
          this.common.PostData(this.common.apiRoutes.registration.verifyOtpAndRegisterUser, this.basicRegistrationVm, true).then(result => {
            
            this.response = result ;
            
            if (this.response.status == httpStatus.Ok) {
              this.basicRegistrationVm.emailOrPhoneNumber = this.basicRegistrationVm.emailAddress != "" ? this.basicRegistrationVm.emailAddress : this.basicRegistrationVm.phoneNumber.toString();
              if ((this.basicRegistrationVm.emailAddress == "" || this.basicRegistrationVm.emailAddress == undefined || this.basicRegistrationVm.emailAddress == null) && (this.basicRegistrationVm.phoneNumber == "" || this.basicRegistrationVm.phoneNumber == undefined || this.basicRegistrationVm.phoneNumber == null)) {
                this.basicRegistrationVm.emailOrPhoneNumber = this.facebookId;
              }
              this.common.PostData(this.common.apiRoutes.Login.Login, this.basicRegistrationVm, true).then(result => {
                
                this.response = result ;

                localStorage.setItem("auth_token", "Bearer " + this.response.resultData);
                
                this.common.PostData(this.common.apiRoutes.PackagesAndPayments.AddSubAccount, this.response.resultData, true).then(result => {

                  this.subAccountResponse = result ;
                });
                
                if (this.response.status == httpStatus.Ok) {
                  var decodeToken = this.jwtHelperService.decodeToken(this.response.resultData);
                  if (decodeToken.Role == loginsecurity.CRole) {
                
                    this.common.NavigateToRoute(this.common.apiUrls.User.UserDefault);
                  }
                  if (decodeToken.Role == loginsecurity.ORole) {
                
                    this.common.NavigateToRoute(this.common.apiUrls.Tradesman.BussinessRegistration);
                  }
                  if (decodeToken.Role == loginsecurity.TRole) {
                
                    this.common.NavigateToRoute(this.common.apiUrls.Tradesman.BussinessRegistration);

                  }
                  if (decodeToken.Role == loginsecurity.SRole) {
                    this.common.NavigateToRoute(this.common.apiUrls.Supplier.BussinesDetail);
                  }
                }
              })
            }

          })

        }
        else {
          this.userAvailabilty = true;
          this.userAvailabiltyErrorMessage = this.response.message;
        }
      })
    }
  }
  public Showhide() {
    if (this.type == 'text') {
      this.type = "Password";
    }
    else {
      this.type = "text";
    }
  }
  public movetoNext(privious: string, nextFieldID: string, obj: Event) {
    if ((<HTMLInputElement>obj.target).value == "") {
      document.getElementById(privious)?.focus();
    }
    else
    document.getElementById(nextFieldID)?.focus();
  }
  numberOnly(event: KeyboardEvent): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }
  charOnly(event: KeyboardEvent): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if ((charCode > 47 && charCode < 58)) {
      return false;
    }
    return true;
  }
  public hideBlockModal() {
    this.blockAccountMessageModal.hide();
  }
  public bindMetaTags() {
    this.common.get(this.common.apiRoutes.CMS.GetSeoPageById + "?pageId=1").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description,res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }

}
