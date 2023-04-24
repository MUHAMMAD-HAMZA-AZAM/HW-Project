import { Component, OnInit, OnChanges, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { CommonService } from '../../shared/HttpClient/_http';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LoginVM, ResponseVm } from '../../models/commonModels/commonModels';
import { LoginValidation, loginsecurity, httpStatus } from '../../shared/Enums/enums';
import { NgxSpinnerService } from 'ngx-spinner';
import { SocialAuthService, SocialUser } from "angularx-social-login";
import { FacebookLoginProvider, GoogleLoginProvider } from "angularx-social-login";
import { ToastrService } from 'ngx-toastr';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { CookieService } from 'ngx-cookie-service';
import { strict } from 'assert';
import { setFullYear } from 'ngx-bootstrap/chronos/utils/date-setters';
import { IPageSeoVM } from '../../shared/Enums/Interface';
import { metaTagsService } from '../../shared/CommonServices/seo-dynamictags.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnChanges {
  public emailErrorMessage: string = "";
  public passwordErrorMessage: string = "";
  public invalidcredentials: string = "";
  public unauthorizeduser: boolean = false;
  public submittedApplicationForm = false;
  public appValForm: FormGroup;
  public roleId: string | null = "";
  public check: { T: boolean, O: boolean, U: boolean, S: boolean } = { T: false, O: false, U: false, S: false };
  public roleType: string | null = "";
  public token: string = "";
  public emailorPassword: string = "";
  public password: string = "";
  public loginstatus: boolean = false;
  public loginVm: LoginVM = {} as LoginVM;
  public responseVm: ResponseVm = {} as ResponseVm;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  public type = "Password";
  public show = false;
  public isConnected = true;
  public noInternetConnection: boolean = false;
  public roleErrorMessage: string = "";
  private user: SocialUser;
  private loggedIn: boolean = false;
  public hasPin: boolean = false;
  public hasPinentered: boolean = true;
  public required: boolean = false;
  public checkEmailValid: boolean = true;
  public isUserBlocked: boolean = false;
  public subAccountResponse: ResponseVm = {} as ResponseVm;
  @ViewChild('blockAccountMessageModal', { static: false }) blockAccountMessageModal: ModalDirective;
  constructor(public router: Router, private route: ActivatedRoute, public common: CommonService, public Loader: NgxSpinnerService, private toaster: ToastrService,
    private formBuilder: FormBuilder, private authService: SocialAuthService, private cookieService: CookieService, private _metaService: metaTagsService) {
    this.user = {} as SocialUser;
    this.appValForm = {} as FormGroup;
    this.blockAccountMessageModal = {} as ModalDirective;
  }
  ngOnInit() {
    this.RoleCheck();
    this.updateLastLogin();
    this.PopulateForm();
    this.bindMetaTags();
  }
  public PopulateForm() {
    this.Loader.show();
    if (this.cookieService.get('customerusername') != "" && this.roleType == "3") {
      this.appValForm = this.formBuilder.group({
        emailOrPhoneNumber: [this.cookieService.get('customerusername')],
        rememberMe: [true],
        roleCrtl: [this.cookieService.get('customerrole')],
        tab1: [this.cookieService.get('customertab1')],
        tab2: [this.cookieService.get('customertab2')],
        tab3: [this.cookieService.get('customertab3')],
        tab4: [this.cookieService.get('customertab4')],
        tab5: [this.cookieService.get('customertab5')],
      });
      this.hasPin = true;
    }
    else if (this.cookieService.get('tradesmanusername') != "" && this.roleType == "1" || this.roleType == "2") {
      this.appValForm = this.formBuilder.group({
        emailOrPhoneNumber: [this.cookieService.get('tradesmanusername')],
        rememberMe: [true],
        roleCrtl: [this.cookieService.get('tradesmanrole')],
        tab1: [this.cookieService.get('tradesmantab1')],
        tab2: [this.cookieService.get('tradesmantab2')],
        tab3: [this.cookieService.get('tradesmantab3')],
        tab4: [this.cookieService.get('tradesmantab4')],
        tab5: [this.cookieService.get('tradesmantab5')],
      });
      this.hasPin = true;
    }
    else {
      this.appValForm = this.formBuilder.group({
        emailOrPhoneNumber: ['', Validators.required],
        rememberMe: [false],
        roleCrtl: [null],
        tab1: ['', [Validators.required]],
        tab2: ['', [Validators.required]],
        tab3: ['', [Validators.required]],
        tab4: ['', [Validators.required]],
        tab5: ['', [Validators.required]],
      });
    }
    this.Loader.hide();
  }
  public updateLastLogin() {

  }
  ngOnChanges() {
    this.RoleCheck();
  }
  public RoleCheck() {
    this.roleType = localStorage.getItem("Role");
    this.roleId = this.route.snapshot.paramMap.get('id');
    switch (this.roleType) {
      case '1':
        this.check.T = true;
        this.check.O = false;
        this.check.S = false;
        this.check.U = false;
        break;
      case '2':
        this.check.O = true;
        this.check.T = false;
        this.check.S = false;
        this.check.U = false;
        break;
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
  }
  get f() {
    return this.appValForm.controls;
  }
  public Login() {
    this.emailErrorMessage = LoginValidation.emailOrPhoneNumber;
    this.passwordErrorMessage = LoginValidation.password;
    this.roleErrorMessage = LoginValidation.roleErrorMessage;
    this.submittedApplicationForm = true;
    this.loginVm = this.appValForm.value;

    if (this.appValForm.value.roleCrtl != "") {
      this.roleType = this.appValForm.value.roleCrtl;
    }

    if (this.roleType == null) {
      this.roleType = localStorage.getItem("Role");
    }

    var expireDate = new Date();
    expireDate.setFullYear(expireDate.getFullYear() + 1);

    if (this.loginVm.rememberMe && this.roleType != null && this.appValForm.value.tab1 != ""
      && this.appValForm.value.tab2 != "" && this.appValForm.value.tab3 != "" && this.appValForm.value.tab4 != ""
      && this.appValForm.value.tab5 != "") {
      if (this.roleType == "3") {
        this.cookieService.set('customerusername', this.loginVm.emailOrPhoneNumber, { expires: expireDate });
        this.cookieService.set('customertab1', this.appValForm.value.tab1, { expires: expireDate });
        this.cookieService.set('customertab2', this.appValForm.value.tab2, { expires: expireDate });
        this.cookieService.set('customertab3', this.appValForm.value.tab3, { expires: expireDate });
        this.cookieService.set('customertab4', this.appValForm.value.tab4, { expires: expireDate });
        this.cookieService.set('customertab5', this.appValForm.value.tab5, { expires: expireDate });
        this.cookieService.set('customerrole', this.roleType != null ? this.roleType : "", { expires: expireDate });
      }
      else {
        this.cookieService.set('tradesmanusername', this.loginVm.emailOrPhoneNumber, { expires: expireDate });
        this.cookieService.set('tradesmantab1', this.appValForm.value.tab1, { expires: expireDate });
        this.cookieService.set('tradesmantab2', this.appValForm.value.tab2, { expires: expireDate });
        this.cookieService.set('tradesmantab3', this.appValForm.value.tab3, { expires: expireDate });
        this.cookieService.set('tradesmantab4', this.appValForm.value.tab4, { expires: expireDate });
        this.cookieService.set('tradesmantab5', this.appValForm.value.tab5, { expires: expireDate });
        this.cookieService.set('tradesmanrole', this.roleType != null ? this.roleType : "", { expires: expireDate });
      }
    }

    if (this.loginVm.rememberMe == false && this.roleType == "3") {
      this.cookieService.delete('customerusername');
      this.cookieService.delete('customertab1');
      this.cookieService.delete('customertab2');
      this.cookieService.delete('customertab3');
      this.cookieService.delete('customertab4');
      this.cookieService.delete('customertab5');
      this.cookieService.delete('customerrole');
    }
    else if (this.loginVm.rememberMe == false) {
      this.cookieService.delete('tradesmanusername');
      this.cookieService.delete('tradesmantab1');
      this.cookieService.delete('tradesmantab2');
      this.cookieService.delete('tradesmantab3');
      this.cookieService.delete('tradesmantab4');
      this.cookieService.delete('tradesmantab5');
      this.cookieService.delete('tradesmanrole');
    }

    if (this.roleType == "1") {
      this.appValForm.controls.roleCrtl.setValidators([Validators.required]);
      this.appValForm.controls.roleCrtl.updateValueAndValidity();
    }
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

    if (this.appValForm.value.tab1 !== "" && this.appValForm.value.tab2 !== "" && this.appValForm.value.tab3 !== "" && this.appValForm.value.tab4 !== "" && this.appValForm.value.tab5 !== "") {
      this.loginVm.password = "P@ss" + this.appValForm.value.tab1 + this.appValForm.value.tab2 + this.appValForm.value.tab3 + this.appValForm.value.tab4 + this.appValForm.value.tab5;
      this.loginVm.facebookClientId = "";
      this.loginVm.googleClientId = "";
      if (this.appValForm.valid) {

        this.common.PostData(this.common.apiRoutes.Login.Login, this.loginVm, true).then(result => {

          this.responseVm = result;
          if (this.responseVm != null) {

            if (this.responseVm.status == httpStatus.Ok) {

              var decodedtoken = this.jwtHelperService.decodeToken(this.responseVm.resultData);
              console.log(decodedtoken);
              this.common.GetData(this.common.apiRoutes.Login.GetUserBlockStatus + "?userId=" + decodedtoken.UserId).then(result => {
                let blockStatus = result;
                if (!blockStatus) {
                  if (decodedtoken.Role == loginsecurity.CRole || decodedtoken.Role == loginsecurity.ORole || decodedtoken.Role == loginsecurity.TRole || decodedtoken.Role == loginsecurity.SRole) {
                    this.loginstatus = true;
                    localStorage.removeItem("Role");
                    localStorage.removeItem("Show");
                    localStorage.setItem("auth_token", "Bearer " + this.responseVm.resultData);
                    if (decodedtoken.Role == loginsecurity.CRole) {
                      this.common.get(this.common.apiRoutes.PackagesAndPayments.getSubAccountRecord).subscribe(result => {
                        let response = <ResponseVm>result;

                        if (response.status == httpStatus.Ok) {
                          this.common.PostData(this.common.apiRoutes.PackagesAndPayments.AddSubAccount, true).then(result => {

                            let responce = result;
                          });
                        }
                      });
                      this.common.NavigateToRoute(this.common.apiUrls.User.UserDefault);

                    }
                    else if (decodedtoken.Role == loginsecurity.SRole) {
                      this.common.get(this.common.apiRoutes.PackagesAndPayments.getSubAccountRecord).subscribe(result => {
                        let response = <ResponseVm>result;

                        if (response.status == httpStatus.Ok) {
                          this.common.PostData(this.common.apiRoutes.PackagesAndPayments.AddSubAccount, true).then(result => {

                            let responce = result;
                          });
                        }
                      });
                      this.common.NavigateToRoute(this.common.apiUrls.Supplier.Home);

                    }
                    else if (decodedtoken.Role == loginsecurity.TRole || decodedtoken.Role == loginsecurity.ORole) {
                      this.common.get(this.common.apiRoutes.PackagesAndPayments.getSubAccountRecord).subscribe(result => {
                        let response = <ResponseVm>result;

                        if (response.status == httpStatus.Ok) {
                          this.common.PostData(this.common.apiRoutes.PackagesAndPayments.AddSubAccount, true).then(result => {

                            let responce = result;
                          });
                        }
                      });
                      this.common.NavigateToRoute(this.common.apiUrls.Tradesman.LiveLeads);

                    }
                  }
                  else {
                    this.loginVm.role = "";
                    this.unauthorizeduser = true;
                    this.invalidcredentials = LoginValidation.InvalidCredentials;
                  }
                }
                else {
                  this.isUserBlocked = true;
                  this.blockAccountMessageModal.show();
                  setTimeout(() => {
                    this.isUserBlocked = false;
                    this.blockAccountMessageModal.hide();
                  }, 5000);
                }
              });
            }
            else {

              if (!this.unauthorizeduser) { //// User Email is valid 
                this.hasPin = true;
                this.hasPinentered = false;
              }
            }
          }
          else {
            this.common.Notification.warning("Go To Registration");
          }
        });
      }
    }
    else {
      if (this.loginVm.emailOrPhoneNumber != "") {
        // Make Request to check User Block Status

        if (this.hasPin == true) {
          this.hasPinentered = false;
        }
        this.common.GetData(this.common.apiRoutes.IdentityServer.GetUserPinStatus + "?role=" + this.roleType + "&emailOrPhone=" + this.loginVm.emailOrPhoneNumber, true).then(result => {
          this.responseVm = result;
          this.common.get(this.common.apiRoutes.Login.GetUserBlockStatus + "?userId=" + this.responseVm.message).subscribe(blockStatus => {

            let bs = blockStatus;

            if (!bs) {
              if (this.responseVm.status == httpStatus.Ok) {
                if (this.responseVm.resultData == true) {
                  this.hasPin = true;
                  this.unauthorizeduser = false;
                }
                else {
                  this.appValForm.controls.tab1.setValue('');
                  this.common.NavigateToRouteWithQueryString(this.common.pagesUrl.CommonRegistrationPages.forgotPassword);
                }
              }
              else {
                this.unauthorizeduser = true;
                this.invalidcredentials = LoginValidation.InvalidCredentials;
              }
            }
            else {
              this.isUserBlocked = true;
              this.blockAccountMessageModal.show();
              setTimeout(() => {
                this.isUserBlocked = false;
                this.blockAccountMessageModal.hide();
              }, 5000);
            }

          });
        });
      }
    }

  }
  public hideBlockModal() {
    this.blockAccountMessageModal.hide();
  }
  public signInWithGoogle(): void {
    debugger;
    if (this.common.IsUserLogIn()) {
      this.toaster.error("Already Login");
    }
    else {
      debugger;
      this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);
      this.authService.authState.subscribe((user) => {
        debugger;
        this.user = user;
        this.loggedIn = (user != null);
        if (this.loggedIn) {
          if (this.user.provider == "GOOGLE") {
            this.Googlelogin();
          }
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
      // this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);
      this.authService.authState.subscribe((user) => {
        this.user = user;

        this.loggedIn = (user != null);
        if (this.loggedIn) {
          if (this.user.provider == "FACEBOOK") {
            this.FBLogin();
          }
        }
      });
    }
  }
  public FBLogin() {

    this.roleType = localStorage.getItem("Role");
    if (this.appValForm.value.roleCrtl != "") {
      this.roleType = this.appValForm.value.roleCrtl;
    }

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
      this.responseVm = {} as ResponseVm;
      this.responseVm = res;
      if (this.responseVm.status == httpStatus.Restricted) {
        this.loginVm.emailOrPhoneNumber = this.user.email;
        this.loginVm.password = "P@ssfb123";
        this.loginVm.facebookClientId = this.user.id;
        this.loginVm.googleClientId = "";
        if (this.loginVm.emailOrPhoneNumber == null || this.loginVm.emailOrPhoneNumber == "") {
          this.loginVm.emailOrPhoneNumber = this.user.id;
        }
        this.common.PostData(this.common.apiRoutes.Login.Login, this.loginVm, true).then(result => {

          this.responseVm = result;
          if (this.responseVm != null) {
            if (this.responseVm.status == httpStatus.Ok) {
              var decodedtoken = this.jwtHelperService.decodeToken(this.responseVm.resultData);
              this.common.GetData(this.common.apiRoutes.Login.GetUserBlockStatus + "?userId=" + decodedtoken.UserId).then(result => {
                let blockStatus = result;
                if (!blockStatus) {
                  if (decodedtoken.Role == loginsecurity.CRole || decodedtoken.Role == loginsecurity.ORole || decodedtoken.Role == loginsecurity.TRole || decodedtoken.Role == loginsecurity.SRole) {
                    this.loginstatus = true;
                    localStorage.removeItem("Role");
                    localStorage.removeItem("Show");
                    localStorage.setItem("auth_token", "Bearer " + this.responseVm.resultData);
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
                  this.isUserBlocked = true;
                  this.blockAccountMessageModal.show();
                  setTimeout(() => {
                    this.isUserBlocked = false;
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
        this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Common.Header.userSignUp, {
          queryParams: {
            "firstName": this.user.firstName, "lastName": this.user.lastName, "emailAddress": this.user.email, "token": this.user.authToken, "facebookId": this.user.id
          }
        });
      }
    });
  }
  public Googlelogin() {
    debugger;
    this.roleType = localStorage.getItem("Role");
    if (this.appValForm.value.roleCrtl != "") {
      this.roleType = this.appValForm.value.roleCrtl;
    }
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
    this.common.PostData(this.common.apiRoutes.IdentityServer.GetUserByFacebookId, obj, true).then(result => {
      if (status = httpStatus.Ok) {
        this.responseVm = result;

        if (this.responseVm.status == httpStatus.Restricted) {
          this.loginVm.emailOrPhoneNumber = this.user.email;
          this.loginVm.password = "P@ssgo123";
          this.loginVm.facebookClientId = "";
          this.loginVm.googleClientId = this.user.id;
          this.common.PostData(this.common.apiRoutes.Login.Login, this.loginVm, true).then(result => {

            this.responseVm = result;
            if (this.responseVm != null) {
              if (this.responseVm.status == httpStatus.Ok) {

                var decodedtoken = this.jwtHelperService.decodeToken(this.responseVm.resultData);
                this.common.GetData(this.common.apiRoutes.Login.GetUserBlockStatus + "?userId=" + decodedtoken.UserId).then(result => {
                  let blockStatus = result;
                  if (!blockStatus) {
                    if (decodedtoken.Role == loginsecurity.CRole || decodedtoken.Role == loginsecurity.ORole || decodedtoken.Role == loginsecurity.TRole || decodedtoken.Role == loginsecurity.SRole) {
                      this.loginstatus = true;
                      localStorage.removeItem("Role");
                      localStorage.removeItem("Show");
                      localStorage.setItem("auth_token", "Bearer " + this.responseVm.resultData);
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
                    this.isUserBlocked = true;
                    this.blockAccountMessageModal.show();
                    setTimeout(() => {
                      this.isUserBlocked = false;
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
          this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Common.Header.userSignUp, {
            queryParams: {
              "firstName": this.user.firstName, "lastName": this.user.lastName, "emailAddress": this.user.email, "token": this.user.authToken, "googleuserId": this.user.id
            }
          });
        }
      }
    });
  }
  public ForgotPassword() {
    this.common.NavigateToRouteWithQueryString(this.common.pagesUrl.CommonRegistrationPages.forgotPassword);
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

    if ((<HTMLInputElement>obj.target).value != "" && privious == 'fourth') {
      this.hasPinentered = true;
    }
    else if ((<HTMLInputElement>obj.target).value == "") {
      this.hasPinentered = false;
    }
  }
  numberOnly(event: KeyboardEvent): boolean {
    this.hasPinentered = true;
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }
  CheckValidation(event: Event) {

    this.unauthorizeduser = false;
  }
  public bindMetaTags() {
    this.common.get(this.common.apiRoutes.CMS.GetSeoPageById + "?pageId=2").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description,res.keywords, res.ogTitle, res.ogDescription, res.canonical);    });
  }

}

