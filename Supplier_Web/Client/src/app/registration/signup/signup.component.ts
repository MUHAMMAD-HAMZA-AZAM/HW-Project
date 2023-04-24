import { HttpStatusCode } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonService } from '../../Shared/HttpClient/HttpClient';
import { ToastrService } from 'ngx-toastr';
import { FacebookLoginProvider, GoogleLoginProvider, SocialAuthService, SocialUser } from 'angularx-social-login';
import { ActivatedRoute, Params } from '@angular/router';
import { strict } from 'assert';
import { ICityList, IPageSeoVM, IPersonalDetailsVM, IUserObj, IUserRegistrationConfirm, LoginVM } from '../../Shared/Enums/Interface';
import { IResponseVM } from '../../Shared/Enums/enum';
import { metaTagsService } from '../../Shared/HttpClient/seo-dynamic.service';
import { JwtHelperService } from '@auth0/angular-jwt';



@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  public appValForm: FormGroup;
  public verifyValForm: FormGroup;
  public hasPinentered: boolean = true;
  public role: string = "Customer";
  public submitted = false;
  public otpsubmitted = false;
  public profile: IPersonalDetailsVM;
  public verifyVM: any = "";
  public code: number = 0;
  public phoneNumber: number = 0;
  public existedAccount: boolean = false;
  public inValidOTP: boolean = false;
  public obj: IUserObj;
  public errorMsg: string = "";
  public loginVm: LoginVM = {} as LoginVM;
  private user: SocialUser;
  private loggedIn: boolean = false;
  public disableInput: boolean = false;
  private firstName: string = "";
  private lastName: string = "";
  private emailAddress: string = "";
  private token: string = "";
  private facebookId: string = "";
  private googleId: string = "";
  public customerId: number = 0;
  public IsPhoneNumberTimerRunning: boolean = false;
  public ptimer: any = "";
  public phoneNumberdisplay: any = "";
  public userAvailabilityMessage: string = "";
  public response: IResponseVM;
  public userRegistrationConfirm: IUserRegistrationConfirm;
  public cityList: ICityList[] = [];
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(public _httpService: CommonService, public formBuilder: FormBuilder, public _modalService: NgbModal
    , private toaster: ToastrService, private _metaService: metaTagsService,private socialAuthService: SocialAuthService, private route: ActivatedRoute) {
    this.route.queryParams.subscribe((params: Params) => {
      this.firstName = params['firstName'];
      this.lastName = params['lastName'];
      this.emailAddress = params['emailAddress'];
      this.token = params['token'];
      this.facebookId = params['facebookId'];
      this.googleId = params['googleuserId'];
    });
    this.profile = {} as IPersonalDetailsVM;
    this.appValForm = {} as FormGroup;
    this.verifyValForm = {} as FormGroup;
    this.obj = {} as IUserObj;
    this.user = {} as SocialUser;
    this.userRegistrationConfirm = {} as IUserRegistrationConfirm;
  }
   
  ngOnInit(): void {
    this.appValForm = this.formBuilder.group({
      firstName: ['', [Validators.required, Validators.pattern("[a-zA-Z][a-zA-Z ]+[a-zA-Z ]$")]],
      lastName: ['', [Validators.required, Validators.pattern("[a-zA-Z][a-zA-Z ]+[a-zA-Z ]$")]],
      phoneNumber: ['', [Validators.required]],
      email: ['', Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")],
      cityId: [null, [Validators.required]],
      tab1: ['', [Validators.required, Validators.maxLength(1)]],
      tab2: ['', [Validators.required, Validators.maxLength(1)]],
      tab3: ['', [Validators.required, Validators.maxLength(1)]],
      tab4: ['', [Validators.required, Validators.maxLength(1)]],
      tab5: ['', [Validators.required, Validators.maxLength(1)]],
    });
    this.verifyValForm = this.formBuilder.group({
      otp: ['', [Validators.required]]
    });

    // for google authentication
    if (this.googleId) {
      this.appValForm.controls.firstName.setValue(this.firstName);
      this.appValForm.controls.lastName.setValue(this.lastName);
      this.appValForm.controls.email.setValue(this.emailAddress);
      this.appValForm.controls.tab1.setValue("g");
      this.appValForm.controls.tab2.setValue("o");
      this.appValForm.controls.tab3.setValue("1");
      this.appValForm.controls.tab4.setValue("2");
      this.appValForm.controls.tab5.setValue("3");
      this.appValForm.controls.phoneNumber.clearValidators();
      this.appValForm.controls.phoneNumber.updateValueAndValidity();
      this.appValForm.controls.cityId.clearValidators();
      this.appValForm.controls.cityId.updateValueAndValidity();
      this.googleId = this.googleId;
      this.disableInput = true;
      this.Register("");
    }
    else if (this.facebookId) {
      this.appValForm.controls.firstName.setValue(this.firstName);
      this.appValForm.controls.lastName.setValue(this.lastName);
      this.appValForm.controls.email.setValue(this.emailAddress);
      this.appValForm.controls.tab1.setValue("f");
      this.appValForm.controls.tab2.setValue("b");
      this.appValForm.controls.tab3.setValue("1");
      this.appValForm.controls.tab4.setValue("2");
      this.appValForm.controls.tab5.setValue("3");
      this.appValForm.controls.phoneNumber.clearValidators();
      this.appValForm.controls.phoneNumber.updateValueAndValidity();
      this.appValForm.controls.cityId.clearValidators();
      this.appValForm.controls.cityId.updateValueAndValidity();
      this.facebookId = this.facebookId;
      this.disableInput = true;
      this.Register("");
    }
    this.bindMetaTags();
    this.getAllCities();
  }

  public getAllCities() {
    this._httpService.get(this._httpService.apiUrls.Supplier.City.getCityList).subscribe(result => {
      this.cityList = <any>result;
      console.log(this.cityList);
    });
  }

  Register(verifyAccount: any) {
    debugger;
    this.submitted = true;
    if (this.appValForm.valid) {
      let formValue = this.appValForm.value;
      formValue.firstName = formValue.firstName.trim();
      formValue.lastName = formValue.lastName.trim();
      this.obj = {
        email: formValue.email,
        phoneNumber: formValue.phoneNumber,
        firstName: formValue.firstName,
        lastName: formValue.lastName,
        city:formValue.cityId,
        role: this.role,
        password: "P@ss" + formValue.tab1 + formValue.tab2 + formValue.tab3 + formValue.tab4 + formValue.tab5,
        termsAndConditions: true,
        userName: formValue.email,
        emailOrPhoneNumber: formValue.email,
        facebookUserId: this.facebookId,
        googleUserId: this.googleId,
        facebookClientId: null,
        googleClientId: null
      }
      this._httpService.PostData(this._httpService.apiUrls.Customer.Registration.CheckEmailandPhoneNumberAvailability, this.obj, true)?.then(res => {
        this.response = res;
        console.log(this.response);
        if (this.response.status == HttpStatusCode.Ok) {

          this._httpService.PostData(this._httpService.apiUrls.Customer.Registration.VerifyOtpAndRegisterUser, this.obj, true)?.then(result => {
            this.response = result;
            this.userRegistrationConfirm = this.response.resultData;
            console.log(this.userRegistrationConfirm);
            if (this.response.status == HttpStatusCode.Ok) {
              this.toaster.success("Account Registered Successfully", "Success");
              this.phoneNumber = formValue.phoneNumber;
              if (this.phoneNumber) {

                this._httpService.GetData(this._httpService.apiUrls.Customer.Registration.GetPersonalDetailsByCustomerId + "?customerId=" + this.userRegistrationConfirm.customerId, true).then(result => {

                  this.profile = result;
                  if (this.profile.mobileNumber) {
                    let ngbModalOptions: NgbModalOptions = {
                      backdrop: 'static',
                      keyboard: false
                    };

                    this._modalService.open(verifyAccount, ngbModalOptions);
                    this.SentOtpCode();
                    if (!this.IsPhoneNumberTimerRunning) {
                      this.phoneNumbertimer(1);
                    }
                  }
                });
              }
            }
          });
        }
        else {
          this.userAvailabilityMessage = this.response.message;
          this.existedAccount = true;
          setTimeout(() => {
            this.existedAccount = false;
          }, 3000);
        }

      });

    }
  }
  public CancelClicked() {
    localStorage.removeItem('web_auth_token');
    this.verifyValForm.reset();
    this._modalService.dismissAll();
  }
  phoneNumbertimer(minute: any) {
    
    this.IsPhoneNumberTimerRunning = true;
    let seconds: number = minute * 60;
    let textSec: any = "0";
    let statSec: number = 60;

    const prefix = minute < 10 ? "0" : "";

    

    if (minute == "0") {
      clearInterval(this.ptimer);
      this.IsPhoneNumberTimerRunning = false;
      this.phoneNumberdisplay = null;
    }
    else {
      this.ptimer = setInterval(() => {
        seconds--;
        if (statSec != 0) statSec--;
        else statSec = 59;

        if (statSec < 10) {
          textSec = "0" + statSec;
        } else textSec = statSec;

        this.phoneNumberdisplay = `OTP will be Expired in: ${prefix}${Math.floor(seconds / 60)}:${textSec}`;

        if (seconds == 0) {
          this.IsPhoneNumberTimerRunning = false;
          this.phoneNumberdisplay = null;
          console.log("finished");
          clearInterval(this.ptimer);
        }
      }, 1000);
    }
  }
  get f() {
    return this.appValForm.controls;
  }
  get g() {
    return this.verifyValForm.controls;
  }
  public SentOtpCode() {
    this.verifyVM = {
      id: this.profile.userId,
      phoneNumber: this.profile.mobileNumber

    }
    this._httpService.PostData(this._httpService.apiUrls.Customer.Registration.GetOtp, this.verifyVM, true).then(result => {
      let response = result;
      if (response.status == HttpStatusCode.Ok) {
      }

    });
  }
  public VerifyOTP() {
    this.otpsubmitted = true;
    if (this.verifyValForm.invalid) {
      return;
    }
    this.code = this.verifyValForm.value.otp;
    this._httpService.GetData(this._httpService.apiUrls.Customer.Verification.VerifyOtpWithoutToken + "?code=" + this.code + "&phoneNumber=" + this.phoneNumber + "&email=" + "" + "&userId=" + this.profile.userId, true).then(result => {
      let response = result;
      if (response.status == HttpStatusCode.Ok) {
        this.inValidOTP = false;
        this.obj.facebookClientId = this.facebookId;
        this.obj.googleClientId = this.googleId;
        this.obj.emailOrPhoneNumber = this.obj?.email ? this.obj.email : this.obj.phoneNumber;
        this._httpService.PostData(this._httpService.apiUrls.Customer.Login.Login, this.obj, true)?.then(result => {
          let response = result;
          if (response.status == HttpStatusCode.Ok) {
            localStorage.setItem("web_auth_token", "Bearer " + response.resultData);
            let role = this._httpService.decodedToken().Role;
            let userId = this._httpService.decodedToken().UserId;

            this._httpService.PostData(this._httpService.apiUrls.PackagesAndPayments.AddSubAccountWithoutToken + "?userId=" + userId + "&role=" + role, true)?.then(result => {
              let subAccountResponse = result;
            });
            this._modalService.dismissAll();
            this._httpService.NavigateToRoute(this._httpService.apiRoutes.Home.Index);
          }
          else {

          }
        });
      }
      else {
        if (response.status == HttpStatusCode.Forbidden)
          this.errorMsg = "Your account has been temporarily blocked. Please try again later."
        else
          this.errorMsg = "Invalid OTP"

        this.inValidOTP = true;
      }
    });


  }
  ResendOTP() {
    this.verifyValForm.reset();
    this.inValidOTP = false;
    this.otpsubmitted = false;
    if (!this.IsPhoneNumberTimerRunning) {
      this.phoneNumbertimer(1);
    }
    this.SentOtpCode();
  }
  public movetoNext(privious: string, nextFieldID: string, obj: Event) {
    if ((<HTMLInputElement>obj.target).value == "") {
      document.getElementById(privious)?.focus();
    }
    else {
      document.getElementById(nextFieldID)?.focus();
    }
    if ((<HTMLInputElement>obj.target).value != "" && privious == 'fourth') {
      this.hasPinentered = true;
    }
    else if ((<HTMLInputElement>obj.target).value == "") {
      this.hasPinentered = false;
    }
  }
  // for google authentication
  public async signUpWithGoogle() {
    debugger;
    if (this._httpService.IsUserLogIn()) {
      this.toaster.error("Already Login");
    }
    else {
      await this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
      this.socialAuthService.authState.subscribe((user) => {
        this.user = user;
        this.loggedIn = (user != null);
        if (this.loggedIn) {
          let obj = {
            facebookId: this.user.id,
            role: this.role,
            email: this.user.email
          };
          debugger;
          this._httpService.PostData(this._httpService.apiUrls.Identity.GetUserByFacebookId, obj, true).then(result => {
            debugger;
            let response = result;
            if (response.status == HttpStatusCode.Forbidden) {
              this.loginVm.emailOrPhoneNumber = this.user.email;
              this.loginVm.password = "P@ssgo123";
              this.loginVm.googleClientId = this.user.id;
              this.loginVm.facebookClientId = "";
              if (this.loginVm.emailOrPhoneNumber == null || this.loginVm.emailOrPhoneNumber == "") {
                this.loginVm.emailOrPhoneNumber = this.user.id;
              }
              this._httpService.PostData(this._httpService.apiUrls.Customer.Registration.login, this.loginVm, true).then(result => {
                this.response = result;
                if (this.response.status == HttpStatusCode.Ok) {
                  localStorage.setItem("web_auth_token", "Bearer " + response.resultData);
                  this.customerId = this._httpService.decodedToken().Id;
                  let userId = this._httpService.decodedToken().UserId;
                  if (!localStorage.getItem("UserId")) {
                    localStorage.setItem("UserId", userId)
                  }
                  if (localStorage.getItem("UserId") != userId) {
                    localStorage.removeItem("ca_items");
                    this._httpService.subject$.next(true);
                  }
                  localStorage.setItem("UserId", userId)

                  this._httpService.GetData(this._httpService.apiUrls.Customer.Registration.GetPersonalDetailsByCustomerId + "?customerId=" + this.customerId, true).then(result => {

                    this.profile = result;
                    if (this.profile.isNumberConfirmed == true) {

                      this._httpService.GetData(this._httpService.apiUrls.PackagesAndPayments.getSubAccountRecordWithoutToken + "?id=" + this.customerId)?.then(result => {
                        let response = result;
                        if (response.status == HttpStatusCode.Ok) {
                          let role: string = this._httpService.decodedToken().Role;
                          let userId: string = this._httpService.decodedToken().UserId;
                          this._httpService.PostData(this._httpService.apiUrls.PackagesAndPayments.AddSubAccountWithoutToken + "?userId=" + userId + "&role=" + role, true)?.then(result => {
                            let subAccountResponse = result;
                          });
                        }
                        debugger;
                        this._httpService.NavigateToRoute(this._httpService.apiRoutes.Home.Index);
                      });
                    }
                    else {
                      let ngbModalOptions: NgbModalOptions = {
                        backdrop: 'static',
                        keyboard: false
                      };
                      //this._modalService.open(verifyAccount, ngbModalOptions);
                      //this.SentOtpCode();
                      //if (!this.IsPhoneNumberTimerRunning) {
                      //  this.phoneNumbertimer(1);
                      //}
                    }
                  });
                }
              });
            }
            else
            {
              if (this.user.provider == "GOOGLE") {
                this.appValForm.controls.firstName.setValue(this.user.firstName);
                this.appValForm.controls.lastName.setValue(this.user.lastName);
                this.appValForm.controls.email.setValue(this.user.email);
                this.appValForm.controls.tab1.setValue("g");
                this.appValForm.controls.tab2.setValue("o");
                this.appValForm.controls.tab3.setValue("1");
                this.appValForm.controls.tab4.setValue("2");
                this.appValForm.controls.tab5.setValue("3");
                this.appValForm.controls.phoneNumber.clearValidators();
                this.appValForm.controls.phoneNumber.updateValueAndValidity();
                this.appValForm.controls.cityId.clearValidators();
                this.appValForm.controls.cityId.updateValueAndValidity();
                this.googleId = this.user.id;
                this.disableInput = true;
                this.Register("");
              }
            }
          });

        }
      });
    }
  }
  //for facebook authentication
  public signUpWithFacebook(): void {
    if (this._httpService.IsUserLogIn()) {
      this.toaster.error("Already Login");
    }
    else {
      this.socialAuthService.signIn(FacebookLoginProvider.PROVIDER_ID);
      this.socialAuthService.authState.subscribe((user) => {
        this.user = user;
        this.loggedIn = (user != null);
        if (this.loggedIn) {
          let obj = {
            facebookId: this.user.id,
            role: this.role,
            email: this.user.email
          };
          this._httpService.PostData(this._httpService.apiUrls.Identity.GetUserByFacebookId, obj, true).then(result => {
            let response = result;
            if (this.response.status == HttpStatusCode.Forbidden) {
              this.loginVm.emailOrPhoneNumber = this.user.email;
              this.loginVm.password = "P@ssfb123";
              this.loginVm.facebookClientId = this.user.id;
              this.loginVm.googleClientId = "";
              if (this.loginVm.emailOrPhoneNumber == null || this.loginVm.emailOrPhoneNumber == "") {
                this.loginVm.emailOrPhoneNumber = this.user.id;
              }
              this._httpService.PostData(this._httpService.apiUrls.Customer.Registration.login, this.loginVm, true).then(result => {
                this.response = result;
                if (response.status == HttpStatusCode.Ok) {
                  localStorage.setItem("web_auth_token", "Bearer " + response.resultData);
                  this.customerId = this._httpService.decodedToken().Id;
                  let userId = this._httpService.decodedToken().UserId;
                  if (!localStorage.getItem("UserId")) {
                    localStorage.setItem("UserId", userId)
                  }
                  if (localStorage.getItem("UserId") != userId) {
                    localStorage.removeItem("ca_items");
                    this._httpService.subject$.next(true);
                  }
                  localStorage.setItem("UserId", userId)

                  this._httpService.GetData(this._httpService.apiUrls.Customer.Registration.GetPersonalDetailsByCustomerId + "?customerId=" + this.customerId, true).then(result => {

                    this.profile = result;
                    if (this.profile.isNumberConfirmed == true) {

                      this._httpService.GetData(this._httpService.apiUrls.PackagesAndPayments.getSubAccountRecordWithoutToken + "?id=" + this.customerId)?.then(result => {
                        let response = result;
                        if (response.status == HttpStatusCode.Ok) {
                          let role: string = this._httpService.decodedToken().Role;
                          let userId: string = this._httpService.decodedToken().UserId;
                          this._httpService.PostData(this._httpService.apiUrls.PackagesAndPayments.AddSubAccountWithoutToken + "?userId=" + userId + "&role=" + role, true)?.then(result => {
                            let subAccountResponse = result;
                          });
                        }
                        this._httpService.NavigateToRoute(this._httpService.apiRoutes.Home.Index);
                      });
                    }
                    else {
                      let ngbModalOptions: NgbModalOptions = {
                        backdrop: 'static',
                        keyboard: false
                      };
                      //this._modalService.open(verifyAccount, ngbModalOptions);
                      //this.SentOtpCode();
                      //if (!this.IsPhoneNumberTimerRunning) {
                      //  this.phoneNumbertimer(1);
                      //}
                    }
                  });
                }
              });
            }
            else {
              if (this.user.provider == "FACEBOOK") {
                this.appValForm.controls.firstName.setValue(this.user.firstName);
                this.appValForm.controls.lastName.setValue(this.user.lastName);
                this.appValForm.controls.email.setValue(this.user.email);
                this.appValForm.controls.tab1.setValue("f");
                this.appValForm.controls.tab2.setValue("b");
                this.appValForm.controls.tab3.setValue("1");
                this.appValForm.controls.tab4.setValue("2");
                this.appValForm.controls.tab5.setValue("3");
                this.appValForm.controls.phoneNumber.clearValidators();
                this.appValForm.controls.phoneNumber.updateValueAndValidity();
                this.appValForm.controls.cityId.clearValidators();
                this.appValForm.controls.cityId.updateValueAndValidity();
                this.facebookId = this.user.id;
                this.disableInput = true;
                this.Register("");
              }
            }
          });
        }
      });
    }
  }

  public bindMetaTags() {
    this._httpService.get(this._httpService.apiUrls.CMS.GetSeoPageById + "?pageId=25").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description, res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }
}
