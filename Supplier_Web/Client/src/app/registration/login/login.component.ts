import { HttpStatusCode } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { CommonService } from '../../Shared/HttpClient/HttpClient';
import { FacebookLoginProvider, GoogleLoginProvider, SocialAuthService, SocialUser } from 'angularx-social-login';
import { ActivatedRoute, Params } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ILogIn, IPageSeoVM, IPersonalDetailsVM } from '../../Shared/Enums/Interface';
import { timeStamp } from 'console';
import { metaTagsService } from '../../Shared/HttpClient/seo-dynamic.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public appValForm: FormGroup;
  public role: string = "Customer";
  public emailErrorMessage: string="";
  public hasPinentered: boolean = true;
  public submittedApplicationForm: boolean = false;
  public profile: IPersonalDetailsVM;
  public customerId: number=0;
  public verifyValForm: FormGroup;
  public otpsubmitted = false;
  public verifyVM: any;
  public inValidOTP: boolean = false;
  public code: number=0;
  public obj: ILogIn;
  public errorMsg: string="";

  private user: SocialUser;
  private loggedIn: boolean=false;
  private firstName: string="";
  private lastName: string="";
  private emailAddress: string="";
  private token: string="";
  private facebookId: string="";
  private googleId: string="";
  public returnUrl: null;

  public IsPhoneNumberTimerRunning: boolean = false;
  public ptimer: any;
  public phoneNumberdisplay: any;

  constructor(private formBuilder: FormBuilder, public common: CommonService, public _modalService: NgbModal, private _metaService: metaTagsService,
    public socialAuthService: SocialAuthService, private route: ActivatedRoute, private toaster: ToastrService) {
    this.route.queryParams.subscribe((params: Params) => {
      this.firstName = params['firstName'];
      this.lastName = params['lastName'];
      this.emailAddress = params['emailAddress'];
      this.token = params['token'];
      this.facebookId = params['facebookId'];
      this.googleId = params['googleuserId'];
      this.returnUrl = params['returnUrl'];
    });
this.obj={} as ILogIn;
this.user={} as SocialUser;
this.appValForm={} as FormGroup;
this.verifyValForm={} as FormGroup;
this.profile={} as IPersonalDetailsVM;
  }

  ngOnInit(): void {
    this.appValForm = this.formBuilder.group({
      emailOrPhoneNumber: ['', Validators.required],
      tab1: ['', Validators.required],
      tab2: ['', Validators.required],
      tab3: ['', Validators.required],
      tab4: ['', Validators.required],
      tab5: ['', Validators.required]
    });
    this.verifyValForm = this.formBuilder.group({
      otp: ['', [Validators.required]]
    });
    this.common.subject$.next(false);
    if (this.googleId) {
      this.obj = {
        emailOrPhoneNumber: this.emailAddress,
        password: "P@ssgo123",
        role: this.role,
        facebookClientId: "",
        googleClientId: this.googleId
      }
      var verifyAccount;
      this.LoginUser(verifyAccount, this.obj);
    }
    else if (this.facebookId) {
      this.obj = {
        emailOrPhoneNumber: this.emailAddress,
        password: "P@ssfb123",
        role: this.role,
        facebookClientId: this.facebookId,
        googleClientId: ""
      }
      var verifyAccount;
      console.log(this.obj);
      this.LoginUser(verifyAccount, this.obj);
    }
    this.bindMetaTags();
  }

  get f() { return this.appValForm.controls }
  public Login(verifyAccount:any) {
    this.submittedApplicationForm = true;
    if (this.appValForm.valid) {

      let data = this.appValForm.value;
      this.obj = {
        emailOrPhoneNumber: data.emailOrPhoneNumber,
        password: "P@ss" + data.tab1 + data.tab2 + data.tab3 + data.tab4 + data.tab5,
        role: this.role,
        facebookClientId: "",
        googleClientId: ""
      }
      this.LoginUser(verifyAccount, this.obj);


    }
  }
  LoginUser(verifyAccount: any, obj: any) {
    this.common.PostData(this.common.apiUrls.Customer.Registration.login, obj, true).then(result => {
      let response = result;
      if (response != null) {

        if (response.status == HttpStatusCode.Ok) {
          localStorage.setItem("web_auth_token", "Bearer " + response.resultData);
          this.customerId = this.common.decodedToken().Id;
          let userId = this.common.decodedToken().UserId;
          if (!localStorage.getItem("UserId")){
            localStorage.setItem("UserId",userId)
          }
          if (localStorage.getItem("UserId")!=userId){
            localStorage.removeItem("ca_items");
            this.common.subject$.next(true);
          }    
          localStorage.setItem("UserId",userId)

          this.common.GetData(this.common.apiUrls.Customer.Registration.GetPersonalDetailsByCustomerId + "?customerId=" + this.customerId, true).then(result => {

            this.profile = result;
            if (this.profile.isNumberConfirmed == true) {
              
              this.common.GetData(this.common.apiUrls.PackagesAndPayments.getSubAccountRecordWithoutToken + "?id=" + this.customerId)?.then(result => {
                let response = result;
                if (response.status == HttpStatusCode.Ok) {
                  let role:string = this.common.decodedToken().Role;
                  let userId: string = this.common.decodedToken().UserId;
                  this.common.PostData(this.common.apiUrls.PackagesAndPayments.AddSubAccountWithoutToken + "?userId=" + userId + "&role=" + role, true)?.then(result => {
                    let subAccountResponse = result;
                  });
                }
                this.common.NavigateToRoute(this.common.apiRoutes.Home.Index);
              });
              
            }
            else {
              let ngbModalOptions: NgbModalOptions = {
                backdrop: 'static',
                keyboard: false
              };
              console.log(ngbModalOptions);
              this._modalService.open(verifyAccount, ngbModalOptions);
              this.SentOtpCode();
              if (!this.IsPhoneNumberTimerRunning)
              {
                this.phoneNumbertimer(1);
              }

            }
          });

        }
        else {
          this.emailErrorMessage = 'Invalid Credentials';
          setTimeout(() => {
            this.emailErrorMessage = '';
          }, 3000);
        }
      }
    });

  }
  phoneNumbertimer(minute:any) {
    
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
  get g() {
    return this.verifyValForm.controls;
  }
  public SentOtpCode() {
    this.verifyVM = {
      id: this.profile.userId,
      phoneNumber: this.profile.mobileNumber
    }
    this.common.PostData(this.common.apiUrls.Customer.Registration.GetOtp, this.verifyVM, true).then(result => {
      let response = result;
    });
  }
  public closeModal() {
    this._modalService.dismissAll();
    localStorage.removeItem('web_auth_token');
    this.common.NavigateToRoute(this.common.apiRoutes.Login.login);
    //this.verifyValForm = {} as FormGroup;
    this.verifyValForm.reset();
  }
  public VerifyOTP() {
    this.otpsubmitted = true;
    if (this.verifyValForm.invalid) {
      return;
    }
    this.code = this.verifyValForm.value.otp;
    console.log(this.verifyValForm.value.otp);
    this.common.GetData(this.common.apiUrls.Customer.Verification.VerifyOtpWithoutToken + "?code=" + this.code + "&phoneNumber=" + this.profile.mobileNumber + "&email=" + "" + "&userId=" + this.profile.userId, true).then(result => {
      let response = result;
      
      if (response.status == HttpStatusCode.Ok) {
        this.inValidOTP = false;
        this.common.PostData(this.common.apiUrls.Customer.Login.Login, this.obj, true)?.then(result => {
          let response = result;
          if (response.status = HttpStatusCode.Ok) {
            localStorage.setItem("web_auth_token", "Bearer " + response.resultData);

            this.common.GetData(this.common.apiUrls.PackagesAndPayments.getSubAccountRecordWithoutToken + "?id=" + this.customerId)?.then(result => {
              let response = result;
              
              if (response.status == HttpStatusCode.Ok) {
                let role:string = this.common.decodedToken().Role;
                let userId:string = this.common.decodedToken().UserId;

                this.common.PostData(this.common.apiUrls.PackagesAndPayments.AddSubAccountWithoutToken + "?userId=" + userId + "&role=" + role, true)?.then(result => {
                  let subAccountResponse = result;
                });
              }
              
            });
            
            this._modalService.dismissAll();
            this.common.NavigateToRoute(this.common.apiRoutes.Home.Index);
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

  public movetoNext(privious:string, nextFieldID:string, obj:Event) {
    if ((<HTMLInputElement>obj.target).value == "") {
      document.getElementById(privious)?.focus();
    }
    else
      document.getElementById(nextFieldID)?.focus();

    if ((<HTMLInputElement>obj.target).value != "" && privious == 'fourth') {
      this.hasPinentered = true;
    }
    else if ((<HTMLInputElement>obj.target).value == "") {
      this.hasPinentered = false;
    }
  }
  public async signInWithGoogle(verifyAccount:any) {
    debugger;
    if (this.common.IsUserLogIn()) {
      this.toaster.error("Already Login");
    }
    else {
      debugger;
      await this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
      this.socialAuthService.authState.subscribe((user) => {
        debugger;
        this.user = user;
        this.loggedIn = (user != null);
        if (this.loggedIn) {
          if (this.user.provider == "GOOGLE") {
            let obj = {
              facebookId: this.user.id,
              role: this.role
            };
            this.common.PostData(this.common.apiUrls.Identity.GetUserByGoogleId, obj, true).then(result => {

              let response = result;
              if (response.status == HttpStatusCode.Ok) {

                this.common.NavigateToRouteWithQueryString(this.common.apiRoutes.Registration.Signup, {
                  queryParams: {
                    "firstName": this.user.firstName, "lastName": this.user.lastName, "emailAddress": this.user.email, "token": this.user.authToken, "googleuserId": this.user.id
                  }
                });


              }
              else {

                this.obj = {
                  emailOrPhoneNumber: this.user.email,
                  password: "P@ssgo123",
                  role: this.role,
                  facebookClientId: "",
                  googleClientId: this.user.id,
                }
                this.LoginUser(verifyAccount, this.obj);
              }
            });
          }
        }
      });
    }
  }
  public signInWithFacebook(verifyAccount:any): void {

    if (this.common.IsUserLogIn()) {
      this.toaster.error("Already Login");
    }
    else {
      this.socialAuthService.signIn(FacebookLoginProvider.PROVIDER_ID);
      this.socialAuthService.authState.subscribe((user) => {
        this.user = user;
        this.loggedIn = (user != null);
        if (this.loggedIn) {
          if (this.user.provider == "FACEBOOK") {
            let obj = {
              facebookId: this.user.id,
              role: this.role,
              email: this.user.email
            };
            this.common.PostData(this.common.apiUrls.Identity.GetUserByFacebookId, obj, true).then(result => {

              let response = result;

              if (response.status == HttpStatusCode.Ok) {

                this.common.NavigateToRouteWithQueryString(this.common.apiRoutes.Registration.Signup, {
                  queryParams: {
                    "firstName": this.user.firstName, "lastName": this.user.lastName, "emailAddress": this.user.email, "token": this.user.authToken, "facebookId": this.user.id
                  }
                });


              }
              else {
                this.obj = {
                  emailOrPhoneNumber: this.user.email,
                  password: "P@ssfb123",
                  role: this.role,
                  facebookClientId: this.user.id,
                  googleClientId: "",
                }

                this.LoginUser(verifyAccount, this.obj);
              }
            });
          }
        }
      });
    }
  }



  public bindMetaTags() {
    this.common.get(this.common.apiUrls.CMS.GetSeoPageById + "?pageId=24").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description, res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }
}
