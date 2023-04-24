import { DatePipe } from '@angular/common';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { BasicRegistration, CheckEmailandPhoneNumberAvailability, ResponseVm } from '../../models/commonModels/commonModels';
import { Customer, GetQuotes } from '../../models/userModels/userModels';
import { metaTagsService } from '../../shared/CommonServices/seo-dynamictags.service';
import { BidStatus, CommonErrors, httpStatus, loginsecurity, LoginValidation, RegistrationErrorMessages } from '../../shared/Enums/enums';
import { IIdValue, ISkill, ISubSkill } from '../../shared/Enums/Interface';
import { CommonService } from '../../shared/HttpClient/_http';
import { ISkillIdValue } from '../../shared/Interface/tradesman';

@Component({
  selector: 'app-cost-guide',
  templateUrl: './cost-guide.component.html',
  styleUrls: ['./cost-guide.component.css']
})
export class CostGuideComponent implements OnInit {

  public skillId: number = 0;
  public skillDetail: ISkill;
  public subSkillsList: ISubSkill[] = [];
  public minPrice: any;
  public maxPrice: any;
  public signupForm: FormGroup;
  public loginForm: FormGroup;
  public jobForm: FormGroup;
  public otpForm: FormGroup;
  public verifyForm: FormGroup;
  public appValForm: FormGroup;
  public checkEmailEvailabilityVm: CheckEmailandPhoneNumberAvailability = {} as CheckEmailandPhoneNumberAvailability;
  public basicRegistrationVm: BasicRegistration = {} as BasicRegistration;
  public getQuotationsVM: GetQuotes = {} as GetQuotes;
  public response: ResponseVm = {} as ResponseVm;
  public subAccountResponse: ResponseVm = {} as ResponseVm;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  public userAvailabilty: boolean = false;
  public userAvailabiltyErrorMessage: string = "";
  public isLogin: boolean = false;
  public isJobPost: boolean = false;
  public latitude: number = 31.5204;
  public longitude: number = 74.3587;
  public userRole: string;
  public token: string | null = "";
  public userId: string;
  public profile: Customer = {} as Customer;
  public noNotCorrect: boolean = false;
  public phoneNumber: Number = {} as Number;
  public responseMessage: string = {} as string;
  public status: boolean = false;
  public code: Number = {} as Number;
  public statusCode: boolean = false;
  public skillList: ISkill[] = [];
  public skillLists = [];
  public cancelJob: boolean = false;
  public showcategory: boolean = false;
  public showSubcategory: boolean = false;
  public categoryId: number = 0;
  public subCategoryId: number = 0;
  public subSkill: IIdValue[] = [];
  public skillsVM: ISkillIdValue = {} as ISkillIdValue;
  public subSkillPrice: number = 0;
  public categorySelect: boolean = false;
  public getQuotes: GetQuotes = {} as GetQuotes;
  public loginCheck: boolean = false;
  public isRegister: boolean = false;
  public isVerify: boolean = false;
  public hasPinentered: boolean = false;
  public emailErrorMessage = {} as string;
  public passwordErrorMessage = {} as string;
  public submittedApplicationForm: boolean = false;
  public submittedForm: boolean = false;
  public submitt: boolean = false;
  public skillName: string | null;
  public navigateFrom;
  public isVerified: boolean = false;
  public verficationCodeErrorMessage = {} as string;
  @ViewChild('loginModal', { static: false }) loginModal: ModalDirective;
  @ViewChild('signUpModal', { static: false }) signUpModal: ModalDirective;
  @ViewChild('otpModal', { static: false }) otpModal: ModalDirective;
  @ViewChild('otpVerifyModal', { static: false }) otpVerifyModal: ModalDirective;
  @ViewChild('successMessageModal', { static: false }) successMessageModal: ElementRef;
  constructor(private _toastr: ToastrService, public service: CommonService, private router: Router, private route: ActivatedRoute, private _metaService: metaTagsService, private formBuilder: FormBuilder, private _modalService: NgbModal) {
    this.skillId = Number(this.route.snapshot.paramMap.get('id'));
    this.skillName =this.route.snapshot.paramMap.get('name');
    this.navigateFrom =this.route.snapshot.paramMap.get('nf');
    
    this.userRole = {} as string;
    this.token = {} as string;
    this.userId = {} as string;
    this.skillDetail = {} as ISkill;
    this.signupForm = {} as FormGroup;
    this.loginForm = {} as FormGroup;
    this.jobForm = {} as FormGroup;
    this.otpForm = {} as FormGroup;
    this.verifyForm = {} as FormGroup;
    this.appValForm = {} as FormGroup;
    this.loginModal = {} as ModalDirective;
    this.signUpModal = {} as ModalDirective;
    this.otpModal = {} as ModalDirective;
    this.otpVerifyModal = {} as ModalDirective;
    this.successMessageModal = {} as ElementRef;
  }
  ngOnInit() {
    this.token = localStorage.getItem("auth_token");
    var decodedtoken = (this.token) ? this.jwtHelperService.decodeToken(this.token) : "";
    
    this.phoneNumber = decodedtoken.sub;
    this.userRole = decodedtoken.Role;
    this.userId = decodedtoken.UserId;
    this.basicRegistrationVm.phoneNumber = this.phoneNumber.toString();
    this.basicRegistrationVm.role = this.userRole;

    if (this.skillId > 0 && this.navigateFrom == "fs") {
      this.showcategory = true;
      this.showSubcategory = true;
      this.skillsVM.skillId = this.skillId;
      this.skillsVM.name = this.skillName ? this.skillName : "";
      this.selectSkill(this.skillsVM);
    }
    else if (this.skillId > 0 && this.navigateFrom == "lk") {
      this.showcategory = false;
      this.showSubcategory = false;
    }

    this.signupForm = this.formBuilder.group({
      name: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required]],
      tab1: ['', [Validators.required]],
      tab2: ['', [Validators.required]],
      tab3: ['', [Validators.required]],
      tab4: ['', [Validators.required]],
      tab5: ['', [Validators.required]],
    });
    this.loginForm = this.formBuilder.group({
      phoneNumber: ['', [Validators.required]],
      tab1: ['', [Validators.required]],
      tab2: ['', [Validators.required]],
      tab3: ['', [Validators.required]],
      tab4: ['', [Validators.required]],
      tab5: ['', [Validators.required]],
    });
    this.otpForm = this.formBuilder.group({
      phoneNumber: ['']
    });
    this.verifyForm = this.formBuilder.group({
      verificationCode: ['']
    });
    this.appValForm = this.formBuilder.group({
      categoryName: [''],
      subCategoryName: [''],
      budget: [''],
    });


    this.getTradesmanSkills();
    this.getSkillDetails();
    this.getSubSkillBySkill();
  }
  get f() {
    return this.loginForm.controls;
  }
  get g() {
    return this.signupForm.controls;
  }
  get h() {
    return this.verifyForm.controls;
  }
  public getSkillDetails() {
    this.service.get(this.service.apiRoutes.Tradesman.GetSkillTagsById + "?skillId=" + this.skillId).subscribe(response => {
      this.skillDetail = <ISkill>response;
      //this.bindMetaTags(this.skillDetail.metaTags, this.skillDetail.name)
      this._metaService.updateTags(this.skillDetail.seoPageTitle, this.skillDetail.skillTitle ? this.skillDetail.skillTitle : this.skillDetail.seoPageTitle, this.skillDetail.metaTags, this.skillDetail.skillTitle ? this.skillDetail.skillTitle : this.skillDetail.seoPageTitle, this.skillDetail.ogTitle, this.skillDetail.ogDescription)
    })
  }
  getSubSkillBySkill() {
    this.service.get(this.service.apiRoutes.Tradesman.GetSubSkillTagsBySkillId + "?SkillId=" + this.skillId).subscribe(res => {
      this.subSkillsList = <ISubSkill[]>res;
      this.minPrice = Math.min(...this.subSkillsList.map(item => item.subSkillPrice));
      this.maxPrice = Math.max(...this.subSkillsList.map(item => item.subSkillPrice));
    })
  }
  public postJob(obj: ISubSkill) {
    if (this.service.loginCheck) {
      this.service.NavigateToRouteWithQueryString(this.service.apiUrls.User.Quotations.getQuotes1, {
        queryParams: {
          skillIdForJob: obj.skillId,
          subSkillIdForJob: obj.subSkillId,

        }
      }
      );
    }
    else {
      localStorage.setItem("Role", '3');
      localStorage.setItem("Show", 'true');
      this.service.NavigateToRoute(this.service.apiUrls.Login.customer);
    }
  }


  getTradesmanSkills() {
    this.service.GetData(this.service.apiRoutes.Tradesman.GetSkillList, false).then(result => {
      this.skillList = result;
    });
  }
  handleGoback() {
    this.showcategory = false;
    this.showSubcategory = false;
  }
  selectSkill(skill: ISkillIdValue) {
    if (skill.skillId) {
      this.categoryId = skill.skillId;
      this.service.get(this.service.apiRoutes.Tradesman.GetSubSkillTagsBySkillId + "?skillId=" + this.categoryId).subscribe(result => {
        this.skillLists = result;
        this.showcategory = true;
        this.showSubcategory = true;
        this.getQuotes.skillName = skill.name;
        this.skillName = skill.name;
        this.skillId = skill.skillId;
        this.getSkillDetails();
      });
    }
  }

  selectSubSkill(subSkill: any) {
    console.log(subSkill)
    debugger;
    if (subSkill) {
      this.getQuotes.subCategoryName = subSkill.name;
      this.getQuotes.subCategoryId = subSkill.subSkillId;
      this.getQuotes.priceReview = subSkill.priceReview;
      this.subCategoryId = subSkill.subSkillId;
      this.getQuotes.budget = subSkill.subSkillPrice;
      this.appValForm.controls.categoryName.setValue(this.getQuotes.skillName);
      this.appValForm.controls.subCategoryName.setValue(this.getQuotes.subCategoryName);
      this.appValForm.controls.budget.setValue(subSkill.subSkillPrice);
      this.categorySelect = true;
      this.showSubcategory = false;
    }
  }

  handleNextClick() {
    this.token = localStorage.getItem("auth_token");
    if (this.token != null && this.token != '') {
      this.service.get(this.service.apiRoutes.IdentityServer.GetPhoneNumberVerificationStatus + "?userId=" + this.token).subscribe(result => {
        this.isVerified = <boolean>result;
        if (this.isVerified == true) {
            let dateTime: Date = new Date()
            var data = this.appValForm.value;
            this.getQuotationsVM.categoryId = this.categoryId.toString();
            this.getQuotationsVM.workTitle = this.getQuotes.skillName;
            this.getQuotationsVM.subCategoryId = this.getQuotes.subCategoryId;
            this.getQuotationsVM.budget = this.getQuotes.budget;
            this.getQuotationsVM.numberOfBids = 3;
            this.getQuotationsVM.statusId = BidStatus.Active;
            this.getQuotationsVM.jobstartDateTime = dateTime;
            let hour = dateTime.getHours();
            let minute = dateTime.getMinutes();
            let second = dateTime.getSeconds();
            let time = hour + ":" + minute + ":" + second;
            this.getQuotationsVM.jobStartTime = time;
            this.getQuotationsVM.cityId = 64;
            this.getQuotationsVM.town = "Lahore";
            this.getQuotationsVM.streetAddress = "Lahore";
          this.getQuotationsVM.locationCoordinates = this.latitude + "," + this.longitude;
          this.service.PostData(this.service.apiRoutes.Customers.JobQuotationsWeb, this.getQuotationsVM, true).then(result => {
              this.response = result;
              if (this.response.status == httpStatus.Ok) {
                this.isJobPost = true;
                //this._toastr.success("Success", "Job Posted Successfully")
                this._modalService.dismissAll();
                this._modalService.open(this.successMessageModal, { centered: true});
              }
            }, error => {
              console.log(error);
              this.service.Notification.error(CommonErrors.commonErrorMessage);
            });
        }
        else {
          this.SentOtpCode();
        }

      });
    }
    else {
      // this.loginCheck = true;
      this._modalService.open(this.loginModal, { centered: true });
    }
   

    
  }
  registerNow() {
    this._modalService.dismissAll();
    this._modalService.open(this.signUpModal, { centered: true });
  }
  login() {
    this.submittedApplicationForm = true;
    this.emailErrorMessage = LoginValidation.emailOrPhoneNumber;
    this.passwordErrorMessage = LoginValidation.password;
    if (this.loginForm.valid) {
      let data = this.loginForm.value;
      this.basicRegistrationVm.emailOrPhoneNumber = data.phoneNumber;
      this.basicRegistrationVm.phoneNumber = data.phoneNumber;
      this.basicRegistrationVm.role = "Customer";
      this.basicRegistrationVm.password = "P@ss" + this.loginForm.value.tab1 + this.loginForm.value.tab2 + this.loginForm.value.tab3 + this.loginForm.value.tab4 + this.loginForm.value.tab5;

      this.service.PostData(this.service.apiRoutes.Login.Login, this.basicRegistrationVm, true).then(result => {
        this.response = result;
        if (this.response.status == httpStatus.Ok) {
          localStorage.setItem("auth_token", "Bearer " + this.response.resultData);
          this.token = localStorage.getItem("auth_token");
          this.handleNextClick();
        }
        else if (this.response) {
          this._toastr.error("Failed", "Invalid Input");
        }
      })
    }
  }
  submit() {
    this.submittedForm = true;
    this.emailErrorMessage = LoginValidation.emailOrPhoneNumber;
    this.passwordErrorMessage = LoginValidation.password;
    if (this.signupForm.valid) {
      let data = this.signupForm.value;
      this.checkEmailEvailabilityVm.phoneNumber = data.phoneNumber;
      this.checkEmailEvailabilityVm.role = "Customer";
      this.checkEmailEvailabilityVm.password = "P@ss" + this.signupForm.value.tab1 + this.signupForm.value.tab2 + this.signupForm.value.tab3 + this.signupForm.value.tab4 + this.signupForm.value.tab5;
      this.basicRegistrationVm.firstName = data.name;
      this.basicRegistrationVm.phoneNumber = data.phoneNumber;
      this.basicRegistrationVm.emailOrPhoneNumber = data.phoneNumber;
      this.basicRegistrationVm.role = this.checkEmailEvailabilityVm.role;
      this.basicRegistrationVm.password = this.checkEmailEvailabilityVm.password;
      this.service.PostData(this.service.apiRoutes.registration.CheckEmailandPhoneNumberAvailability, this.checkEmailEvailabilityVm, true).then(result => {
        this.response = result;
        if (this.response.status == httpStatus.Ok) {
          this.service.PostData(this.service.apiRoutes.registration.verifyOtpAndRegisterUser, this.basicRegistrationVm, true).then(result => {
            this.response = result;
            
            if (this.response.status == httpStatus.Ok) {
              this.userId = this.response.resultData.userId;
             // this.service.PostData(this.service.apiRoutes.Login.Login, this.basicRegistrationVm, true).then(result => {
              //  this.response = result;
             //    localStorage.setItem("auth_token", "Bearer " + this.response.resultData);
             //   this.service.PostData(this.service.apiRoutes.PackagesAndPayments.AddSubAccount, this.response.resultData, true).then(result => {
             //     this.subAccountResponse = result;
            //    });
             //   if (this.response.status == httpStatus.Ok) {
            //      var decodeToken = this.jwtHelperService.decodeToken(this.response.resultData);
             //     this.userId = decodeToken.UserId;
             //     this.userRole = decodeToken.Role;
             //     this.isLogin = true;
              this._modalService.dismissAll();
                  this.SentOtpCode();
             //   }
            //  })
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
  public SentOtpCode() {

    // this.statusMessage = this.phoneNumber;
    //this.statusMessage = this.basicRegistrationVm.email != "" ? this.basicRegistrationVm.email : this.basicRegistrationVm.phoneNumber;
    
    this.service.PostData(this.service.apiRoutes.Common.getOtp, this.basicRegistrationVm, true).then(result => {
      if (status = httpStatus.Ok) {
        this.responseMessage = this.response.message;
        this._modalService.dismissAll();
        this._modalService.open(this.otpVerifyModal, { centered: true });
      }
      this.response = result;
      this.responseMessage = this.response.message;
    });
  }
  public VarifyAccount() {
    this.verficationCodeErrorMessage = RegistrationErrorMessages.verificationErrorMessage;
    this.submitt = true;
    if (this.verifyForm.invalid) {
      return;
    }
    this.code = this.verifyForm.value.verificationCode;
    
    this.service.GetData(this.service.apiRoutes.Common.VerifyOtpWithoutToken + "?code=" + this.code + "&phoneNumber=" + this.basicRegistrationVm.phoneNumber + "&userId=" + this.userId, false).then(result => {

      
      if (status = httpStatus.Ok) {
        this.response = result;
        if (this.response.status == 400) {
          this.statusCode = true;
        }
        else {
          if (!this.token) {

              this.service.PostData(this.service.apiRoutes.Login.Login, this.basicRegistrationVm, true).then(result => {
                this.response = result;
                if (this.response.status == httpStatus.Ok) {
                  localStorage.setItem("auth_token", "Bearer " + this.response.resultData);
                  this.service.PostData(this.service.apiRoutes.PackagesAndPayments.AddSubAccount, this.response.resultData, true).then(result => {
                    this.subAccountResponse = result;
                  });
                  if (this.response.status == httpStatus.Ok) {
                    var decodeToken = this.jwtHelperService.decodeToken(this.response.resultData);
                    this.userId = decodeToken.UserId;
                    this.userRole = decodeToken.Role;

                    this.statusCode = false;
                    this._modalService.dismissAll();
                    this.handleNextClick();
                }
                else if (this.response) {
                  this._toastr.error("Failed", "Invalid Input");
                }

                
                  //this.isLogin = true;
                //  this.SentOtpCode();
                }
              })
          }
          else {
          this._modalService.dismissAll();
          this.handleNextClick();
          }



         
        }
      }
    });
  }
  ResendCode() {
    this.SentOtpCode();
  }
  cancelCategory() {
    this.showcategory = false;
    this.categorySelect = false;
  }
  cancelSubCategory() {
    this.showSubcategory = true;
    this.categorySelect = false;
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
  navigateToDashboard() {
    this.service.NavigateToRoute(this.service.apiUrls.User.UserDefault);
    this._modalService.dismissAll();
  }
}

