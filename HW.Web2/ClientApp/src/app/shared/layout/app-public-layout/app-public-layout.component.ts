import { Component, ElementRef, Inject, OnInit, PLATFORM_ID, Renderer2, TemplateRef, ViewChild, ViewChildren, ViewEncapsulation } from '@angular/core';
import { CommonService } from '../../HttpClient/_http';
import { Router } from '@angular/router';
import { Customer, ImageVM } from '../../../models/userModels/userModels';
import { JwtHelperService } from '@auth0/angular-jwt';
import { httpStatus, loginsecurity, JobQuotationErrors, LoginValidation, BidStatus, RegistrationErrorMessages } from '../../Enums/enums';
import { NgbModal, NgbModalConfig, NgbPopoverConfig, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { GetQuotes } from '../../../models/userModels/userModels';
import { NgxImageCompressService } from 'ngx-image-compress';
import { LoginVM, ResponseVm, RequestCallVm, IfindTradesmanSearch, IdValueVm, BasicRegistration, CheckEmailandPhoneNumberAvailability } from '../../../models/commonModels/commonModels';
import { SocialAuthService, FacebookLoginProvider, GoogleLoginProvider, SocialUser } from 'angularx-social-login';
import { DatePipe, isPlatformBrowser, PlatformLocation } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { IActivePromotionVM, IErrorMessage, IFileResult, IIdValue, IPersonalDetails, ISkill, ISkillIdValue, ITownListVM } from '../../Interface/tradesman';
import { IApplicationSetting } from '../../Enums/Interface';
declare var $: any;
import {
  TransferState,
  makeStateKey
} from '@angular/platform-browser';
import { Events } from '../../../common/events';
const promoListData = makeStateKey('promoListData');

@Component({
  selector: 'app-app-public-layout',
  templateUrl: './app-public-layout.component.html',
  styleUrls: ['./app-public-layout.component.css'],
  encapsulation: ViewEncapsulation.None,
})


export class AppPublicLayoutComponent implements OnInit {
  
  modalReference: NgbModalRef;
  public response: ResponseVm = {} as ResponseVm;
  public subAccountResponse: ResponseVm = {} as ResponseVm;
  public basicRegistrationVm: BasicRegistration = {} as BasicRegistration;
  public checkEmailEvailabilityVm: CheckEmailandPhoneNumberAvailability = {} as CheckEmailandPhoneNumberAvailability;
  public userAvailabilty: boolean = false;
  public userAvailabiltyErrorMessage: string | undefined = "";
  public RoleTypeErrorMessage: string = "";
  public verficationCodeErrorMessage: string = "";
  public verifySubmitt: boolean = false;
  public statusCode: boolean = false;
  public code: number = 0;
  public loggedUsertoken: string | null = "";
  public selectedTown: string = "";
  public phoneNumber: string | null="";
  public IsRunning: boolean = false;
  display: string = "";
  public verificationCode: number |null = null ;
  public email: string = "";
  public noNotCorrect: boolean = false;
  public statusMessage: string | undefined = "";
  public roleId: number = 0;
  public type = "Password";
  public loginCheck: boolean = false;
  //public subCategory: any;
  public Hidden = false;
  //public selectedItems = [];
  public skillList: ISkill[] = [];
  public idValueVm: IdValueVm[] = [];
  public loginVm: LoginVM = {} as LoginVM;
  public role: string | null = "";
  public hasPin: boolean = false;
  public responseMessage: string | undefined = "";
  public status: boolean = false;
  public responseVm: ResponseVm = {} as ResponseVm;
  public requestCallVm: RequestCallVm = {} as RequestCallVm;
  public profile: Customer = {} as Customer;
  public getQuotes: GetQuotes = {} as GetQuotes;
  public listOfImages: ImageVM[] = [];
  public DataList: ImageVM[] = [];
  public BlogUrl: string = "";
  public userImage: string = "";
  public userName: string = "";
  public profileImagecheck = false;
  public showImages = false;
  public appValForm: FormGroup;
  public requestForm: FormGroup;
  public appVal: FormGroup;
  public searchTradesmanForm: FormGroup;
  public registrationValForm: FormGroup;
  public isJobDone: boolean = false;
  public cancelJob = false;
  public showServiceTab = false;
  public showScheduleTab = false;
  public categorySelect = false;
  //public priceList = [];
  public listofFiles: any[] = [];
  public sizeOfOriginalImage: number = 0;
  public sizeOFCompressedImage: number = 0;
  public imgResultBeforeCompress: string = "";
  public imgResultAfterCompress: string = "";
  public localCompressedURl: string = "";
  public login: boolean = false;
  public imageVm: ImageVM = {} as ImageVM;
  public loggedUserDetails: IPersonalDetails;
  public requestStatus: boolean = false;
  public requestCallMessage: string | undefined = "";
  public submitted: boolean = false;
  public registerSubmitt: boolean = false;
  public accepted: boolean = false;
  public showcategory: boolean = true;
  public showSubcategory: boolean = false;
  //public skill: any = {};
  public subSkill: IIdValue[] = [];
  public cityList: IIdValue[] = [];
  public townList: ITownListVM[] = [];
  public town: string = "";
  public categoryId: number = 0;
  public subCategoryId: number = 0;
  public skillId: number = 0;
  public flag: boolean = false;
  public searcedSkills: IIdValue[] = [];
  public activeskillList: ISkill[] = [];
  public searchSkillList: IIdValue[] = [];
  public searchtownList: IIdValue[] = [];
  public selectedSkill: IIdValue;
  public jobStatusDone: boolean = false;
  public myDate: Date = new Date;
  public pipe: DatePipe = new DatePipe("");
  public date: Date = new Date;
  public day: string = "";
  public month: string = "";
  public year: string = "";
  public imageError: string = JobQuotationErrors.imageError;
  public ss: string = "";
  public st: string = "";
  public errorsList: IErrorMessage;
  public maxdate1: string = "";
  private user: SocialUser;
  private loggedIn: boolean = false;
  public roleType: string | null = "";
  public loginstatus: boolean = false;
  public unauthorizeduser: boolean = false;
  public emailNotFound: boolean = false;
  public emailorPhoneNotRegister: boolean = false;
  public checkEmail: boolean = true;
  public invalidcredentials: string = "";
  public pinCodeEmpty: boolean = false;
  public showRegistration: boolean = false;
  public gpslocationCoordinates: string = "";
  public showContact: boolean = true;
  public latitude: number = 31.5204;
  public longitude: number = 74.3587;
  public defaultAddress: string = "Iqra Street, Block Q Model Town, Lahore";
  public trademanSearch: IfindTradesmanSearch;
  public activePromotionList: IActivePromotionVM[] = [];
  public promoDetails = {};
  public cusStyle = { marginTop: "0px" }
  public routeUrl: string = "";
  public token: string | null = "";
  public showBookNowBtn: boolean = false;
  public time = { hour: 0, minute: 0 };
  public meridian = true;
  public subSkillPrice: number = 0;
  public visitCharges: number = 0;
  public defaultCityId: number = 64;
  public settingList: IApplicationSetting[] = [];
  public btnMarketPlace: boolean = false;
  public closeModal: boolean = true;
  public pageNumber: number = 1;
  public commonSkills: ISkill = {} as ISkill;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  public check: {
    T: { role: number, Show: boolean },
    O: { role: number, Show: boolean },
    U: { role: number, Show: boolean },
    S: { role: number, Show: boolean }
  } = {
      T: { role: 1, Show: true },
      O: { role: 2, Show: true },
      U: { role: 3, Show: true },
      S: { role: 4, Show: true }
    };
  @ViewChild("bannerText", { static: true }) bannerText: ElementRef;
  @ViewChild("pillshome", { static: true }) pillsHome: ElementRef;
  @ViewChild("content", { static: true }) content: ElementRef;
  @ViewChild("datePicker", { static: true }) datePicker: ElementRef;
  @ViewChild("promoContent", { static: true }) promoContent: ElementRef;
  @ViewChild("mobileViewBtn", { static: true }) mobileViewBtn: ElementRef;
  @ViewChild('verifyAccountMessageModal', { static: false }) verifyAccountMessageModal: ModalDirective;
  keyword = 'value';
  townkeyword = 'value';
  isBrowser: boolean = false;
  constructor(private authService: SocialAuthService, public renderer: Renderer2, public common: CommonService,
    private router: Router, public modalService: NgbModal, config: NgbModalConfig, private toaster: ToastrService,
    private formBuilder: FormBuilder, private imageCompress: NgxImageCompressService,
    config2: NgbPopoverConfig, public Loader: NgxSpinnerService,
    @Inject(PLATFORM_ID) private platformId: Object,
    private state: TransferState,
    private event: Events
  ) {
    this.appValForm = {} as FormGroup;
    this.requestForm = {} as FormGroup;
    this.appVal = {} as FormGroup;
    this.searchTradesmanForm = {} as FormGroup;
    this.registrationValForm = {} as FormGroup;
    this.modalReference = {} as NgbModalRef;
    this.loggedUserDetails = {} as IPersonalDetails;
    this.modalService.dismissAll();
    this.trademanSearch = {} as IfindTradesmanSearch;
    this.selectedSkill = {} as IIdValue;
    this.errorsList = {} as IErrorMessage;
    this.user = {} as SocialUser;
    this.bannerText = {} as ElementRef;
    this.pillsHome = {} as ElementRef;
    this.content = {} as ElementRef;
    this.datePicker = {} as ElementRef;
    this.promoContent = {} as ElementRef;
    this.mobileViewBtn = {} as ElementRef;
    this.verifyAccountMessageModal = {} as ModalDirective;
    this.common.get(this.common.apiRoutes.Common.getCityList).subscribe(result => {
      this.idValueVm = <IdValueVm[]>result;
    });
    if (isPlatformBrowser(this.platformId)) {
      this.isBrowser = isPlatformBrowser(platformId);
      window.scroll(0, 0);
    }
    // Book Now Job Form
    this.appValForm = this.formBuilder.group({
      emailOrPhoneNumber: ['',],
      categoryName: [''],
      selectedCategoryName: [''],
      selectedSubCategoryName: [''],
      subCategoryName: [''],
      workTitle: ['', [Validators.required]],
      jobDescription: ['', [Validators.required]],
      jobstartDateTime: ['', [Validators.required]],
      jobStartTime: [this.time],
      city: [null],
      town: [null],
      address: [''],
      budget: ['', [Validators.required]],
      streetAddress: [''],
      tab1: [''],
      tab2: [''],
      tab3: [''],
      tab4: [''],
      tab5: [''],
      name: [''],
      email: ['', [Validators.pattern("^[A-Za-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]],
      phoneNumber: [''],
      relationship: [null]
    });

    // Urgent User Registration
    this.registrationValForm = this.formBuilder.group(
      {
        firstName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(25)]],
        lastName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(25)]],
        dateOfBirth: ['1900-01-01',],
        emailAddress: ['', [Validators.minLength(8), Validators.maxLength(40), Validators.pattern("^[A-Za-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]],
        cnic: ['', [Validators.minLength(13), Validators.maxLength(13), Validators.pattern(/^[0-9]*$/)]],
        gender: ['1',],
        phoneNumber: ['', [Validators.required, Validators.minLength(11), Validators.maxLength(11), Validators.pattern(/^[0-9]*$/)]],
        city: [null, Validators.required],
        role: ['Customer'],
        termsAndConditions: [true, Validators.requiredTrue],
        tab1: ['', Validators.required],
        tab2: ['', Validators.required],
        tab3: ['', Validators.required],
        tab4: ['', Validators.required],
        tab5: ['', Validators.required],
      }
    );


    this.requestForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.maxLength(18), Validators.minLength(3)]],
      phoneNumber: ['', [Validators.required, Validators.maxLength(11), Validators.minLength(11), Validators.pattern(/^[0-9]*$/)]]
    });

    this.searchTradesmanForm = this.formBuilder.group({
      selectedSkill: ['', [Validators.required]],
      selectedTown: ['', [Validators.required]],
    });
    this.appVal = this.formBuilder.group(
      {
        verificationCode: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(6)]],
      }
    );
  }

  ngOnInit() {
    this.routeUrl = this.router.url;
    //this.populateSkills();
    this.mainSlider();
  //  this.populateTradesmanSkills();
    this.IsUserLogIn();
    this.getLocalStorageData();
    this.applicationSetting();
    this.event.skills_obj.subscribe(x => {
      this.commonSkills.name = x.name;
      this.commonSkills.slug = x.slug;
      this.commonSkills.skillId = x.skillId;
    });
    // this.getTradesmanSkills();
  //  this.getTownList(this.defaultCityId);

    let currentDate = new Date();
    this.time = { hour: currentDate.getHours(), minute: currentDate.getMinutes() }



    if (this.common.IsUserLogIn()) {
      this.role = localStorage.getItem("Role");
    }
    else {
      this.showBookNowBtn = true;
    }
    var token = localStorage.getItem("auth_token");
    if (token != null && token != '') {
      var decodedtoken = token != null ? this.jwtHelperService.decodeToken(token.replace("Bearer", "")) : "";
      this.role = decodedtoken.Role;
    }
    this.pipe = new DatePipe('en-US');
    this.date = new Date();
    this.month = (this.date.getMonth() + 1).toString();
    if (this.date.getMonth() == 12) {
      this.month = '01';
    }

    this.day = this.date.getDate().toString();
    this.year = this.date.getFullYear().toString();

    if (this.month.length == 1) {
      this.month = '0' + this.month;
    }
    if (this.day.length == 1) {
      this.day = '0' + this.day;
    }

    this.maxdate1 = this.year + "-" + this.month + "-" + this.day;
    //this.getPromotionList();
  }

  get f() { return this.requestForm.controls; }
  get g() { return this.appValForm.controls; }
  get f1() { return this.searchTradesmanForm.controls; }
  get j() { return this.registrationValForm.controls; }
  get k() { return this.appVal.controls; }

  getSubSkillDetails(subSkillId: number) {
    this.common.get(this.common.apiRoutes.Tradesman.GetSubSkillDetails + "?subSkillId=" + subSkillId).subscribe(result => {
      let respone = <any>result;
      if (respone) {
        this.subSkillPrice = respone.subSkillPrice;
        this.appValForm.controls['budget'].setValue(this.subSkillPrice);
        this.visitCharges = respone.visitCharges != null ? respone.visitCharges : 0;
      }
    })
  }
  public hideMvBtns() {
    console.log(this.mobileViewBtn.nativeElement);
    this.mobileViewBtn.nativeElement.classList.add('display-none');
    console.log(this.mobileViewBtn.nativeElement.classList);
  }
  navigateWithSkillId(skillId: number) {
    this.common.NavigateToRouteWithQueryString('User/JobQuotes/step1', { queryParams: { skillId: skillId } })
    this.modalService.dismissAll();
  }
  promotions() {
    this.common.NavigateToRoute("/promotions/promotionlist");
    this.modalService.dismissAll();
  }
  getPromotionList() {

    // 4) CHECK TO SEE IF DATA EXISTS
    const store : any = this.state.get(promoListData, null) as any;
    if (store) {
      //return store;
      this.activePromotionList = <IActivePromotionVM[]>store;
      if (this.activePromotionList) {
        this.viewPromotionDetails(this.promoContent);
      }
    }
    else {

      this.Loader.show();

      this.common.get(this.common.apiRoutes.UserManagement.GetPromotionList).subscribe(result => {

        let response = result;
        this.activePromotionList = <IActivePromotionVM[]>response;
        this.state.set(promoListData, response);
        if (this.activePromotionList) {
          this.viewPromotionDetails(this.promoContent);
        }
        this.Loader.hide();
      })

    }
  }
  viewPromotionDetails(promoContent: ElementRef<any>) {
    this.activePromotionList.filter(x => x.isAcitve && x.isMain).sort((a, b) => {
      if (a.promotionId < b.promotionId) return -1
      return a.promotionId > b.promotionId ? 1 : 0
    }).map(y => {
      this.promoDetails = {
        name: y.name,
        description: y.description,
        skillName: y.skillName,
        skillId: y.skillId,
        image: y.image,
      }
    });
    if (this.isBrowser)
      this.modalService.open(promoContent, { size: 'lg', scrollable: true, centered: true });
  }
  public getLocalStorageData() {
    var token = localStorage.getItem("auth_token");
    var decodedtoken = token != null ? this.jwtHelperService.decodeToken(token) : "";
    if (decodedtoken) {
      this.getLoggedUserDetails(decodedtoken.Role, decodedtoken.UserId);
    }
  }
  getLoggedUserDetails(userRole: string, userId: string) {
    this.Loader.show();
    this.common.get(this.common.apiRoutes.UserManagement.GetUserDetailsByUserRole + `?userRole=${userRole}&userId=${userId}`).subscribe(result => {
      this.loggedUserDetails = <IPersonalDetails>result;
      if (this.loggedUserDetails.role == loginsecurity.CRole) {
        this.showBookNowBtn = true;
      }
      this.Loader.hide();
    });
  }
  public logout() {
    this.loggedUserDetails = {} as IPersonalDetails;
    localStorage.clear();
    this.authService.signOut();
    this.loginCheck = false;
    this.common.NavigateToRoute('');
    //window.location.href = '';
  }
  dismissModal(param: Event) {
    this.showServiceTab = false;
    this.showScheduleTab = false;
  }
  handleCancelClick() {
    this.cancelJob = true;
  }
  cancelPopup() {

    this.modalService.dismissAll();
    this.cancelJob = false;
    this.categorySelect = false;
    window.location.href = '';
  }
  GoBack() {
    this.cancelJob = false;
  }

  public PostAd() {

    if (this.common.loginCheck) {
      this.common.NavigateToRoute(this.common.apiUrls.Supplier.EditAd);
    }
    else {
      localStorage.setItem("Role", '4');
      localStorage.setItem("Show", 'true');
      this.common.NavigateToRoute(this.common.apiUrls.Login.customer);
    }
  }
  public PostJob() {
    if (this.common.loginCheck) {
      this.common.NavigateToRoute(this.common.apiUrls.User.Quotations.getQuotes1);
    }
    else {
      localStorage.setItem("Role", '3');
      localStorage.setItem("Show", 'true');
      window.location.href = '/login/Customer';
    }
  }
  public RegisterEntityCheck(role: string, show: string, clickPage: string) {
    localStorage.setItem("Role", role);
    localStorage.setItem("Show", show);
    if (clickPage == "login") {
      if (role == "3") {
        //window.location.href = '/login/Customer';
        this.common.NavigateToRoute('/login/Customer');

      }
      else if (role == "1" || role == "2") {
        //window.location.href = '/login/Tradesman';
        this.common.NavigateToRoute('/login/Tradesman');
      }
    }
  }
  public DashBoard() {
    var token = localStorage.getItem("auth_token");
    var decodedtoken = token != null ? this.jwtHelperService.decodeToken(token.replace("Bearer", "")) : "";
    if (decodedtoken.Role == loginsecurity.CRole) {
      this.common.NavigateToRoute(this.common.apiUrls.User.UserDefault)
    }
    else if (decodedtoken.Role == loginsecurity.SRole) {
      this.common.NavigateToRoute(this.common.apiUrls.Supplier.Home)
    }
    else if (decodedtoken.Role == loginsecurity.TRole || decodedtoken.Role == loginsecurity.ORole) {
      this.common.NavigateToRoute(this.common.apiUrls.Tradesman.LiveLeads)
    }
  }
  public IsUserLogIn() {
    var token = localStorage.getItem("auth_token");
    if (token != null && token != '') {
      this.loginCheck = true;
    }
    else {
      this.loginCheck = false;
    }
  }
  // Book Now Section
  showModalPopup(content: Event) {
    this.modalService.open(content, {
      windowClass: 'lp-custom-modal-css',
      backdrop: 'static',
      keyboard: false,
    });
    this.getTradesmanSkills();
  }
  getTradesmanSkills() {
    this.common.GetData(this.common.apiRoutes.Tradesman.GetSkillList, false).then(result => {
      this.skillList = result;
      console.log(this.skillList);

    });
  }
  selectSkill(skill: ISkillIdValue) {
    if (skill.skillId) {
      this.categoryId = skill.skillId;
      this.common.get(this.common.apiRoutes.Jobs.GetTradesmanSkillsByParentId + "?parentId=" + this.categoryId).subscribe(result => {
        this.subSkill = <IIdValue[]>result;
        this.getQuotes.skillName = skill.name;
        if (this.subSkill.length == 0) {
          this.appValForm.controls.categoryName.setValue(this.getQuotes.skillName);
          this.categorySelect = true;
        }
        else {
          this.showcategory = false;
          this.showSubcategory = true;
        }
      });
    }
  }

  selectSubSkill(subSkill: IIdValue) {
    if (subSkill) {
      this.getQuotes.subCategoryName = subSkill.value;
      this.getQuotes.subCategoryId = subSkill.id;
      this.subCategoryId = subSkill.id;
      this.appValForm.controls.categoryName.setValue(this.getQuotes.skillName);
      this.appValForm.controls.subCategoryName.setValue(this.getQuotes.subCategoryName);
      this.categorySelect = true;
      this.showSubcategory = false;
    }
  }


  handleNextClick() {

    document.getElementById("collapseOne")?.classList.remove("show");
    var collapsTwo = document.getElementById("collapseTwo");
    collapsTwo?.classList.add("show");
    collapsTwo?.classList.add("text-secondary");
    this.showServiceTab = true;
    if (this.showServiceTab) {
      var elementRightSide = document.getElementById("lpFormRightSide");
      elementRightSide?.classList.remove("col-xl-12");
      elementRightSide?.classList.add("col-xl-10");
    }
    this.appValForm.controls.selectedCategoryName.setValue(this.getQuotes.skillName);
    this.appValForm.controls.selectedSubCategoryName.setValue(this.getQuotes.subCategoryName);

  }


  submitt() {

    this.errorsList = {
      jobTitleError: JobQuotationErrors.jobTitle,
      jobDiscriptionError: JobQuotationErrors.jobDescription,
      cityIdError: JobQuotationErrors.cityError,
      townError: JobQuotationErrors.townError,
      areaError: JobQuotationErrors.areaError,
      startedDateError: JobQuotationErrors.startDateError,
      budgetError: JobQuotationErrors.budgetError,
      budgetPatternError: JobQuotationErrors.budgetPatternError,
    };
    this.submitted = true;
    if (this.appValForm.invalid) {
      return;
    }
    else {
      this.getQuotes = this.appValForm.value;
      this.getQuotes.jobStartTime = this.appValForm.value.jobStartTime.hour + ":" + this.appValForm.value.jobStartTime.minute;
      console.log(this.getQuotes);
      document.getElementById("collapseTwo")?.classList.remove("show");
      document.getElementById("collapseThree")?.classList.add("show");
      this.showScheduleTab = true;
      if (this.common.IsUserLogIn()) {
        this.login = true;
        this.getPersonalDetails();
      }
      else {
        this.login = false;

      }
    }
  }

  loginCustomer() {

    this.loginVm = this.appValForm.value;
    this.loginVm.role = "Customer";
    if (this.checkEmail) {
      if (this.loginVm.emailOrPhoneNumber != "") {
        this.common.GetData(this.common.apiRoutes.IdentityServer.GetUserPinStatus + "?role=" + 3 + "&emailOrPhone=" + this.loginVm.emailOrPhoneNumber, true).then(result => {
          this.responseVm = result;
          if (this.responseVm.status == httpStatus.Ok) {
            if (this.responseVm.resultData == true) {
              this.hasPin = true;
              this.checkEmail = false;
            }
            else {
              this.appValForm.controls.tab1.setValue('');
              this.common.NavigateToRouteWithQueryString(this.common.pagesUrl.CommonRegistrationPages.forgotPassword);
            }
          }

          else {
            this.emailorPhoneNotRegister = true;
          }
        });
      }
      else {
        this.emailNotFound = true;
        setTimeout(() => {
          this.emailNotFound = false;
        }, 2000);

      }
    }
    else {
      if (this.appValForm.value.tab1 !== "" && this.appValForm.value.tab2 !== "" && this.appValForm.value.tab3 !== "" && this.appValForm.value.tab4 !== "" && this.appValForm.value.tab5 !== "") {
        this.loginVm.password = "P@ss" + this.appValForm.value.tab1 + this.appValForm.value.tab2 + this.appValForm.value.tab3 + this.appValForm.value.tab4 + this.appValForm.value.tab5;
        this.loginVm.facebookClientId = "";
        this.loginVm.googleClientId = "";

        this.common.PostData(this.common.apiRoutes.Login.Login, this.loginVm, true).then(result => {
          this.responseVm = result;
          if (this.responseVm != null) {
            if (this.responseVm.status == httpStatus.Ok) {
              var decodedtoken = this.jwtHelperService.decodeToken(this.responseVm.resultData);
              if (decodedtoken.Role == loginsecurity.CRole || decodedtoken.Role == loginsecurity.ORole || decodedtoken.Role == loginsecurity.TRole || decodedtoken.Role == loginsecurity.SRole) {
                // this.loginstatus = true;
                localStorage.removeItem("Role");
                localStorage.removeItem("Show");
                localStorage.setItem("auth_token", "Bearer " + this.responseVm.resultData);
                if (decodedtoken.Role == loginsecurity.CRole) {
                  this.login = true;
                  this.getPersonalDetails();
                }
              }
              else {
                this.unauthorizeduser = true;
              }
            }
            else {
              this.unauthorizeduser = true;
              setTimeout(() => {
                this.unauthorizeduser = false;
              }, 2000);
              this.invalidcredentials = LoginValidation.InvalidPin;
            }
          }
          else {
            this.emailorPhoneNotRegister = true;
            this.common.Notification.warning("Go To Registration");
          }
        });
      }

      else {
        this.pinCodeEmpty = true;
        setTimeout(() => {
          this.pinCodeEmpty = false;
        }, 2000);
        // Pin Code Validations
      }
    }

  }

  //--------------------- User Registration for  Urgent Job Post ----------------------------
  public openRegistration() {
    this.showRegistration = true;
    this.emailorPhoneNotRegister = true;

  }
  public closeVerifyAccountMessageModal() {
    this.verifyAccountMessageModal.hide();
  }

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

  public VerifyAndSendOtp(registerMobileNumber: string) {

    this.registerSubmitt = true;
    if (this.registrationValForm.invalid) {
      this.pinCodeEmpty = true;
      return this.registrationValForm.markAllAsTouched();
    }
    this.basicRegistrationVm = this.registrationValForm.value;
    this.basicRegistrationVm.password = "P@ss" + this.registrationValForm.value.tab1 + this.registrationValForm.value.tab2 + this.registrationValForm.value.tab3 + this.registrationValForm.value.tab4 + this.registrationValForm.value.tab5;
    console.log(this.basicRegistrationVm);
    this.checkEmailEvailabilityVm.email = this.basicRegistrationVm.emailAddress;
    this.checkEmailEvailabilityVm.phoneNumber = this.basicRegistrationVm.phoneNumber;
    this.checkEmailEvailabilityVm.role = this.basicRegistrationVm.role;
    this.checkEmailEvailabilityVm.password = this.basicRegistrationVm.password;
    this.common.PostData(this.common.apiRoutes.registration.CheckEmailandPhoneNumberAvailability, this.checkEmailEvailabilityVm, true).then(result => {

      this.response = result;
      if (this.response.status == httpStatus.Ok) {

        this.common.PostData(this.common.apiRoutes.registration.verifyOtpAndRegisterUser, this.basicRegistrationVm, true).then(result => {

          this.response = result;
          if (this.response.status == httpStatus.Ok) {
            this.basicRegistrationVm.emailOrPhoneNumber = this.basicRegistrationVm.emailAddress != "" ? this.basicRegistrationVm.emailAddress : this.basicRegistrationVm.phoneNumber.toString();

            this.common.PostData(this.common.apiRoutes.Login.Login, this.basicRegistrationVm, true).then(result => {

              this.response = result;

              localStorage.setItem("auth_token", "Bearer " + this.response.resultData);

              this.common.PostData(this.common.apiRoutes.PackagesAndPayments.AddSubAccount, this.response.resultData, true).then(result => {

                this.subAccountResponse = result;
              });
              this.modalReference = this.modalService.open(registerMobileNumber);
              this.VerifyPhoneNumber();
              if (this.response.status == httpStatus.Ok) {
                var decodeToken = this.jwtHelperService.decodeToken(this.response.resultData);
                if (decodeToken.Role == loginsecurity.CRole) {
                  this.emailorPhoneNotRegister = false;
                  document.getElementById("collapseTwo")?.classList.remove("show");
                  document.getElementById("collapseThree")?.classList.add("show");
                  this.showScheduleTab = true;
                  if (this.common.IsUserLogIn()) {
                    this.login = true;
                    this.getPersonalDetails();
                  }
                  else {
                    this.login = false;
                  }
                }

              }
            })
          }

        })

      }
      else {
        this.userAvailabilty = true;
        this.userAvailabiltyErrorMessage = this.response.message;
        setTimeout(() => {
          this.userAvailabilty = false;
        }, 5000);
      }
    })
  }

  // verification Phone Number Module
  VerifyPhoneNumber() {

    this.phoneNumber = this.basicRegistrationVm.phoneNumber;
    if (this.phoneNumber) {
      this.common.GetData(this.common.apiRoutes.Users.CustomerProfile, true).then(result => {
        if (status = httpStatus.Ok) {
          this.profile = result ;
          if (this.profile.mobileNumber == this.phoneNumber) {
            if (this.phoneNumber != null) {
              this.email = "";
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
  }
  public SentOtpCode() {

    this.basicRegistrationVm.email = this.email;
    if (this.phoneNumber) {
      this.basicRegistrationVm.phoneNumber = this.phoneNumber;
      this.statusMessage = this.basicRegistrationVm.email != "" ? this.basicRegistrationVm.email : this.basicRegistrationVm.phoneNumber.toString();
      this.common.PostData(this.common.apiRoutes.Common.getOtp, this.basicRegistrationVm, true).then(result => {
        this.response = result;
        this.status = true;
        this.responseMessage = this.response.message;

      });
    }
  }

  public VarifyAccount() {
    this.token = localStorage.getItem("auth_token");
    this.verficationCodeErrorMessage = RegistrationErrorMessages.verificationErrorMessage;
    this.verifySubmitt = true;
    if (this.appVal.invalid) {
      return;
    }
    this.code = this.appVal.value.verificationCode;
    this.common.GetData(this.common.apiRoutes.Common.VerifyOtp + "?code=" + this.code + "&phoneNumber=" + this.phoneNumber + "&email=" + this.email + "&userId=" + this.token, true).then(result => {
      if (status = httpStatus.Ok) {
        this.response = result;
        if (this.response.status == 400) {
          this.statusCode = true;
          this.verifySubmitt = false;
          setTimeout(() => {
            this.statusCode = false;
          },3000);
        }
        else {
          this.statusCode = false;
          localStorage.setItem("accountVerfication", 'true');
          this.modalReference.close();
          this.appVal.reset();
          // this.modalService.dismissAll(registerMobileNumber);


        }
      }
    });
  }

  public hideModal(registerMobileNumber: TemplateRef<any>) {
    this.modalService.dismissAll(registerMobileNumber);
  }


  public ResendCode() {

    this.verificationCode = 0;
    this.SentOtpCode();
    if(this.IsRunning)
    if (!this.IsRunning) {
      this.timer(1);
    }
  }

  //--------------------- User Registration for  Urgent Job ----------------------------
  getPersonalDetails() {
    this.common.GetData(this.common.apiRoutes.Common.getCityList, true).then(res => {
      this.cityList = res;
    });

    this.common.GetData(this.common.apiRoutes.UserManagement.GetTownsByCityId + "?cityId=" + "64", true).then(res => {
      this.townList = res;
    });

    this.common.GetData(this.common.apiRoutes.Customers.GetPersonalDetails, true).then(result => {
      var personalDetail = result;
      if (personalDetail) {
        this.appValForm.controls.name.setValue(personalDetail.firstName + ' ' + personalDetail.lastName);
        this.appValForm.controls.email.setValue(personalDetail.email);
        this.appValForm.controls.phoneNumber.setValue(personalDetail.mobileNumber);
        let cityId = this.cityList.filter(c => c.id == personalDetail.cityId);
        if (cityId[0].id > 0) {
          this.appValForm.controls.city.setValue(cityId[0].id);
          let selectedCityId = (cityId[0].id).toString();
          this.common.GetData(this.common.apiRoutes.UserManagement.GetTownsByCityId + "?cityId=" + selectedCityId, true).then(res => {
            this.townList = res;
          });
        }
        else {
          this.appValForm.controls.city.setValue(this.defaultCityId);
          let selectedCityId = this.defaultCityId.toString;
          this.common.GetData(this.common.apiRoutes.UserManagement.GetTownsByCityId + "?cityId=" + selectedCityId, true).then(res => {
            this.townList = res;
          });
        }
        //console.log(cityId[0].id);
        //this.appValForm.controls.city.setValue(cityId[0].id);

      }
    });

  }
  public done(registerMobileNumber: TemplateRef<any>) {
    debugger;
    this.errorsList = {
      userNameError: JobQuotationErrors.userName,
      emailerror: JobQuotationErrors.email,
      cityIdError: JobQuotationErrors.cityError,
      townError: JobQuotationErrors.townError,
      areaError: JobQuotationErrors.areaError,
      phoneNumberError: JobQuotationErrors.mobileNumber,
      propertyRelationShipError: JobQuotationErrors.relationShip,
    };

    this.appValForm.controls.name.setValidators([Validators.required]);
    this.appValForm.controls.name.updateValueAndValidity();
    this.appValForm.controls.email.setValidators([Validators.required]);
    this.appValForm.controls.email.updateValueAndValidity();
    this.appValForm.controls.city.setValidators([Validators.required]);
    this.appValForm.controls.city.updateValueAndValidity();
    this.appValForm.controls.town.setValidators([Validators.required]);
    this.appValForm.controls.town.updateValueAndValidity();
    this.appValForm.controls.address.setValidators([Validators.required]);
    this.appValForm.controls.address.updateValueAndValidity();
    this.appValForm.controls.phoneNumber.setValidators([Validators.required]);
    this.appValForm.controls.phoneNumber.updateValueAndValidity();
    this.appValForm.controls.relationship.setValidators([Validators.required]);
    this.appValForm.controls.relationship.updateValueAndValidity();


    this.submitted = true;
    this.DataList = [];
    this.loggedUsertoken = localStorage.getItem("auth_token");
    if (this.appValForm.invalid) {
      return;
    }
    if (this.loggedUsertoken != null && this.loggedUsertoken != '') {
      this.isJobDone = true;
      this.common.GetData(this.common.apiRoutes.IdentityServer.GetPhoneNumberVerificationStatus + "?userId=" + this.loggedUsertoken, true).then(result => {
        if (result == true) {

          if (this.listOfImages.length > 0) {
            for (var i in this.listOfImages) {
              let as = <ImageVM>{};
              as.filePath = "img-" + new Date();
              as.imageBase64 = this.listOfImages[i].imageBase64;
              as.ImageContent = "";
              this.DataList.push(as);
            }
          }
          else {
            this.getQuotes.imageVMs = [];
          }
          this.getQuotes = this.appValForm.value;
          let searchTownObj = this.appValForm.value;
          this.selectedTown = searchTownObj.town.value;
          this.getQuotes.town = searchTownObj.town.id;
          this.getQuotes.categoryId = this.categoryId.toString();
          this.getQuotes.subCategoryId = this.subCategoryId;
          this.getQuotes.locationCoordinates = this.latitude + "," + this.longitude;
          this.getQuotes.imageVMs = this.DataList;
          this.getQuotes.jobStartTime = this.appValForm.value.jobStartTime.hour + ":" + this.appValForm.value.jobStartTime.minute;
          let jqObj = {
            workTitle: this.getQuotes.workTitle,
            jobDescription: this.getQuotes.jobDescription,
            budget: this.getQuotes.budget,
            town: this.selectedTown,
            addressLine: this.getQuotes.address,
            //startTime: this.getQuotes.startTime,
            locationCoordinates: this.getQuotes.locationCoordinates,
            area: this.getQuotes.area,
            streetAddress: this.getQuotes.address,
            categoryId: this.getQuotes.categoryId,
            subCategoryId: this.getQuotes.subCategoryId,
            startedDate: this.getQuotes.startedDate,
            cityId: this.getQuotes.city,
            jobstartDateTime: this.getQuotes.jobstartDateTime,
            WorkStartTime: this.getQuotes.jobStartTime,
            numberOfBids: 3,
            createdBy: this.getQuotes.createdBy,
            imageVMs: this.getQuotes.imageVMs,
            skillName: this.getQuotes.skillName,
            cityName: this.getQuotes.cityName,
            selectiveTradesman: this.getQuotes.selectiveTradesman,
            statusId: BidStatus.Urgent,
            relationship: this.getQuotes.relationship
          };

          this.Loader.show();
          this.common.PostData(this.common.apiRoutes.Customers.JobQuotation, jqObj, true).then(result => {
            this.responseVm = result;
            if (this.responseVm.status = httpStatus.Ok) {
              this.jobStatusDone = true;
              this.appValForm.reset();
              this.categorySelect = false;
              this.Loader.hide();
            }
          },
            error => {
              console.log('Error');
            });
        }
        else {
          this.modalReference = this.modalService.open(registerMobileNumber);
          this.getQuotes = this.appValForm.value;
          this.email = this.getQuotes.email;
          this.phoneNumber = this.getQuotes.phoneNumber;
          this.basicRegistrationVm.email = this.email;
          this.basicRegistrationVm.phoneNumber = this.phoneNumber
          this.appVal.reset();

          if (this.phoneNumber != null) {
            this.email = "";
            this.SentOtpCode();
            if (!this.IsRunning) {
              this.timer(1);
            }
          }
          // this.ResendCode();
          //this.VarifyAccount();
          //this.verifyAccountMessageModal.show();
        }

      });
      
    }


  }

  public onFileChange(event: Event) {
    this.getQuotes.imageVMs = [];
    let fileLength = (<HTMLInputElement>event?.target)?.files?.length;
    if (fileLength) {
      var imageCount = (fileLength) + (this.listOfImages.length);
      if (fileLength > 4 || this.listOfImages.length > 4 || imageCount > 4) {
        // this.imageSubmitted = true;
        this.common.Notification.error(this.imageError);
        return;
      }
      else {
        for (var i = 0; i < fileLength; i++) {
          var reader = new FileReader();
          reader.onload = (event: ProgressEvent<FileReader>) => {
            this.listofFiles.push(event.target?.result);
            this.imageVm.localUrl = event.target?.result != undefined ? event.target?.result?.toString() : "";
            this.imageVm.localUrl ? this.compressFile(this.imageVm.localUrl, this.imageVm.filePath, this.imageVm, i) : "";
          }
          let file = (<HTMLInputElement>event.target).files?.[i];
          file ? reader.readAsDataURL(file) : "";
        }
      }
    }
  }
  public compressFile(image: string, fileName: string, imageVms: ImageVM, index: number) {
    var orientation = -1;
    this.sizeOfOriginalImage = this.imageCompress.byteCount(image) / (1024 * 1024);
    this.imageCompress.compressFile(image, orientation, 50, 50).then(
      result => {
        this.imgResultAfterCompress = result;
        this.localCompressedURl = result;
        this.sizeOFCompressedImage = this.imageCompress.byteCount(result) / (1024 * 1024)
        this.imageVm = {} as ImageVM;
        this.imageVm.filePath = imageVms.filePath;
        const imageBlob = this.dataURItoBlob(this.imgResultAfterCompress.split(',')[1]);
        this.imageVm.imageBase64 = result;
        this.listOfImages.push(this.imageVm);
      })
  }
  public dataURItoBlob(dataURI: string) {
    const byteString = window.atob(dataURI);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8Array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) {
      int8Array[i] = byteString.charCodeAt(i);
    }
    const blob = new Blob([int8Array], { type: 'image/png' });
    return blob;
  }

  CancelImage(index: number) {
    this.listOfImages.splice(index - 1, 1);
  }

  Urgently() {
    this.appValForm.controls.jobstartDateTime.setValue((new Date()).toISOString().substring(0, 10));
  }

  // End Book Now Section

  // Request Call Back Function
  public sendRequest(modalContent: TemplateRef<any>) {

    //this.submitted = true;
    if (this.requestForm.valid) {
      this.requestCallVm = this.requestForm.value;
      this.requestCallVm.isActive = true;
      this.common.PostData(this.common.apiRoutes.Communication.PostRequestCallBack, this.requestCallVm, true).then(res => {
        this.responseVm = res;
        if (this.responseVm.status == httpStatus.Ok) {
          this.requestCallMessage = this.responseVm.message;
          this.popoverHidden();
          this.modalService.open(modalContent);

        }
      });
    }
  }

  public openPrivacyModal(pmodalContent: TemplateRef<any>) {
    this.modalService.open(pmodalContent, { scrollable: true, size: 'lg' });
  }

  numberOnly(event: KeyboardEvent): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }
  popoverHidden() {
    //this.submitted = false;
    this.requestForm.reset();
  }
  // End Request Call Back Fuction


  //scrollTop(event: Event) {
  //  window.scroll(0, 0);
  //}

  public movetoNext(privious: string, nextFieldID: string, obj: Event) {
    if ((<HTMLInputElement>obj.target).value == "") {
      document.getElementById(privious)?.focus();
    }
    else
      document.getElementById(nextFieldID)?.focus();

    if ((<HTMLInputElement>obj.target).value != "" && privious == 'fourth') {
    }
    else if ((<HTMLInputElement>obj.target).value == "") {
    }
  }

  public search() {
    this.accepted = true;
    if (this.searchTradesmanForm.valid) {
      let searchFormObj = this.searchTradesmanForm.value;
      this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Tradesman.Searchtradesmanbyskill,
        { queryParams: { 'skill': searchFormObj.selectedSkill.id, 'town': searchFormObj.selectedTown.value } });
    }

  }
  //application setting
  public applicationSetting() {

    this.common.get(this.common.apiRoutes.UserManagement.GetSettingList).subscribe(result => {
      this.settingList = <IApplicationSetting[]>result;
      if (this.settingList.length > 0) {
        this.settingList.forEach(x => {
          if (x.settingName == "MarketPlace" && x.isActive) {
            this.btnMarketPlace = true;
          }
        });
      }
    });

  }
  public searchClient(skill: IIdValue, eve: Event) {
    this.searcedSkills = [];
    this.skillList.forEach(value => {
      if (value.value.toLowerCase().includes(skill.value.toLowerCase())) {
        let skilltoAdd = { "id": value.id, "value": value.value };
        this.searcedSkills.push(skilltoAdd);
      }
    });
    if (this.searcedSkills != null && this.searcedSkills.length > 0) {
      this.flag = true;
    }
    else {
      this.flag = false;
    }
  }

  public populateTradesmanSkills() {

    this.common.get(this.common.apiRoutes.Tradesman.GetSkillList + "?skillId=" + this.skillId).subscribe(result => {
      this.activeskillList = <ISkill[]>result;
      this.activeskillList.forEach((x) => {
        this.searchSkillList.push({ value: x.name, id: x.skillId });
      });
    });
  }

  //autocomplete

  selectEvent(item: Event) {

  }

  unselectEvent(item: Event) {
  }

  onChangeSearch(val: string) {
    // fetch remote data from here
    // And reassign the 'data' which is binded to 'data' property.
  }
  public selectCity(cityId: number) {
    this.getTownList(cityId);
  }

  public getTownList(cId: number) {
    this.searchtownList = [];
    let cityId = cId.toString();
    this.common.GetData(this.common.apiRoutes.UserManagement.GetTownsByCityId + "?cityId=" + cityId, false).then(res => {
      this.townList = res;
      console.log(this.townList);
      this.townList.forEach((x) => {
        this.searchtownList.push({ value: x.name, id: x.townId });
      })
    });
  }

  selecttownEvent(item: Event) {

  }

  unselecttownEvent(item: Event) {
  }

  onChangetownSearch(val: string) {
    // fetch remote data from here
    // And reassign the 'data' which is binded to 'data' property.
  }

  charOnly(event: KeyboardEvent): boolean {

    const charCode = (event.which) ? event.which : event.keyCode;
    if ((charCode > 47 && charCode < 58) || ((charCode > 32 && charCode <= 47)) || ((charCode >= 58 && charCode <= 64)) || ((charCode >= 91 && charCode <= 96)) || ((charCode >= 123 && charCode <= 126))) {
      return false;
    }

    return true;
  }

  accordionCollapse(event: KeyboardEvent) {
    if (event.which == 13) {
      event.preventDefault();
    }
  }
  public AllowNonZeroIntegers(event: KeyboardEvent): boolean {

    var val = event.keyCode;
    // var target = event.target ? event.target : event.srcElement;
    if (val == 48 && (<HTMLInputElement>event.target).value == "" || val == 101 || val == 45 || val == 46 || ((val >= 65 && val <= 90)) || ((val >= 97 && val <= 122))) {
      return false;
    }
    else if ((val >= 48 && val < 58) || ((val > 96 && val < 106)) || val == 8 || val == 127 || val == 189 || val == 109 || val == 9) {
      return true;
    }
    else {
      return false;
    }
  }

  // Social Registration
  public signInWithFB(): void {
    if (this.common.IsUserLogIn()) {
      this.toaster.error("Already Login");
    }
    else {
      this.authService.signIn(FacebookLoginProvider.PROVIDER_ID);
      this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);

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
    if (this.appValForm.value.role != "") {
      this.roleType = this.appValForm.value.role;
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
    this.common.PostData(this.common.apiRoutes.IdentityServer.GetUserByFacebookId, obj, true).then(result => {
      if (status = httpStatus.Ok) {
        this.responseVm = result;
        if (this.responseVm.status == httpStatus.Restricted) {
          this.loginVm.emailOrPhoneNumber = this.user.email;
          this.loginVm.password = "P@ssfb123";
          this.loginVm.facebookClientId = this.user.id;
          this.loginVm.googleClientId = "";
          this.common.PostData(this.common.apiRoutes.Login.Login, this.loginVm, true).then(result => {
            this.responseVm = result;
            if (this.responseVm != null) {
              if (this.responseVm.status == httpStatus.Ok) {
                var decodedtoken = this.jwtHelperService.decodeToken(this.responseVm.resultData);
                if (decodedtoken.Role == loginsecurity.CRole || decodedtoken.Role == loginsecurity.ORole || decodedtoken.Role == loginsecurity.TRole || decodedtoken.Role == loginsecurity.SRole) {
                  this.loginstatus = true;
                  localStorage.removeItem("Role");
                  localStorage.removeItem("Show");
                  localStorage.setItem("auth_token", "Bearer " + this.responseVm.resultData);
                  if (decodedtoken.Role == loginsecurity.CRole)
                    this.login = true;
                  this.getPersonalDetails();
                }
                else {
                  this.unauthorizeduser = true;
                }
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
      }
    });
  }

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
          if (this.user.provider == "GOOGLE") {
            this.Googlelogin();
          }
        }
      });
    }
  }
  public Googlelogin() {
    this.roleType = localStorage.getItem("Role");
    if (this.appValForm.value.role != "") {
      this.roleType = this.appValForm.value.role;
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


  public mainSlider() {
    if (this.isBrowser) {
      $("#nlp-sw").owlCarousel({
        //slideSpeed: 3000,
        ////paginationSpeed: 4000,
        //singleItem: true,
        //autoPlay: 8000,
        ////autoplaySpeed: 8000,
        //loop: true,

        loop: true,
        slideSpeed: 5000,
        autoplay: false,
        autoplayHoverPause: true,
        autoplayTimeout: 8000,
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        items: 1,
      });
    }
  }

}



