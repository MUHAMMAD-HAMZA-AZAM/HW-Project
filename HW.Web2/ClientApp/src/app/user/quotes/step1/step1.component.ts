import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';
import { IdValueVm, ResponseVm } from '../../../models/commonModels/commonModels';
import { GetQuotes, ImageVM, Images } from '../../../models/userModels/userModels';
import { FormGroup, FormBuilder, Validators, PatternValidator } from '@angular/forms';
import { JobQuotationErrors, BidStatus, httpStatus, CommonErrors } from '../../../shared/Enums/enums';
import { NgxImageCompressService } from 'ngx-image-compress';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Params, ActivatedRoute } from '@angular/router';
import { DatePipe } from '@angular/common';
import { IFacebookLeads, IIdValue, IPageSeoVM, IPersonalDetails, ISubSkill } from '../../../shared/Enums/Interface';
import { JwtHelperService } from '@auth0/angular-jwt';
import { timeStamp } from 'console';
import { connectableObservableDescriptor } from 'rxjs/internal/observable/ConnectableObservable';
import { metaTagsService } from '../../../shared/CommonServices/seo-dynamictags.service';
//declare const showMobileNav: any;
@Component({
  selector: 'step1',
  templateUrl: './step1.component.html',
  styleUrls: ['./step1.component.css']
})
export class Step1 implements OnInit {
  public listOfSkills: IIdValue[] = [];
  public listOfSubSkills: IIdValue[] = [];
 
  public showSubCategoryList: boolean = false;
  public showCategoryList: boolean = false;
  public submitted: boolean = false;
  public imageSubmitted: boolean = false;
  public selectedCategory: string="";
  public fileLength: number | undefined = 0;
  public blob: Blob | undefined;
  public selectedSubCategory: string="";
  public getQuotationsVM: GetQuotes = {} as  GetQuotes;
  public imageVm: ImageVM = {} as  ImageVM;
  public listOfImages: ImageVM[] = [];
  public appValForm: FormGroup;
  public categoryError: string="";
  public subCategoryError: string="";
  public jobTitleError: string="";
  public jobDescriptionError: string="";
  public startedDateError: string="";
  public budgetError: string="";
  public budgetPatternError: string="";
  public imageError: string = JobQuotationErrors.imageError;
  public listofFiles: any[] = [];
  public response: ResponseVm = {} as ResponseVm;
  public imageContent: [] = [];
  public DataList: ImageVM[] = [];
  public subSkillPrice: number=0;
  public visitCharges: number=0;
  public maxdate1: string="";
  public imageVM: Images;
  public predictions: number[]=[];
  public imageDataEvent: any;
  public localUrl: any;
  public localCompressedURl: any;
  public isFlip: boolean = false;
  public sizeOfOriginalImage: number=0;
  public sizeOFCompressedImage: number=0;
  public imgResultBeforeCompress: string="";
  public imgResultAfterCompress: string="";
  public Id: number = 0; ImagesList: any;
  public mainImage: any;
  public hasNoImage: boolean = false;
  public slideIndex: number = 1;
  public showNav = false;
  public skillid: any = null ;
  public skillautoset = 0;
  public title: string = "";
  public pipe: DatePipe;
  public date: Date = new Date;
  public day: string="";
  public month: string="";
  public year: string="";
  public skillId: any;
  public selectedSkill: any;
  public time = { hour: 0, minute: 0 };
  public meridian = true;
  public token: any;
  public userVerify:boolean = false;
  public userId: string='';
  public userRole:string='';
  public loggedUserDetails: IPersonalDetails;
  public facebookLeadUser:IFacebookLeads;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  @ViewChild('ImageModal', { static: true }) imageModal: ModalDirective;
  @ViewChild('verifyAccountMessageModal', { static: false }) verifyAccountMessageModal: ModalDirective;
  constructor(
    public common: CommonService,
    private formBuilder: FormBuilder,
    private _metaService: metaTagsService,
    private imageCompress: NgxImageCompressService,
    private route: ActivatedRoute) {
    this.appValForm = {} as FormGroup;
    this.imageVM = {} as Images;
    this.pipe = {} as DatePipe;
    this.imageModal = {} as ModalDirective;
    this.verifyAccountMessageModal = {} as ModalDirective;
    this.loggedUserDetails = {} as IPersonalDetails;
    this.facebookLeadUser= {} as IFacebookLeads;
  }

  ngOnInit() {
    this.isUserLogin();
    this.GetSkills();
    this.bindMetaTags();
    this.token = localStorage.getItem("auth_token");
    var decodedtoken = this.token != null ? this.jwtHelperService.decodeToken(this.token) : "";
    this.userId = decodedtoken.UserId;
    this.userRole = decodedtoken.Role;
  
    //  localStorage.removeItem("step1Data");
    
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
    let currentDate = new Date();
    this.time = { hour: currentDate.getHours(), minute:currentDate.getMinutes() }
    this.appValForm = this.formBuilder.group({
      categoryId: [null, Validators.required],
      subCategoryId: [null, Validators.required],
      workTitle: ['', [Validators.required]],
      jobDescription: ['', Validators.required],
      startedDate: ['', Validators.required],
      startTime: [this.time, Validators.required],
      budget: ['', Validators.required],
      
    });

    this.route.queryParams.subscribe((params: Params) => {
      
      this.userVerify = params['status'];
    });
    this.getLoggedUserDetails(this.userRole,this.userId);
    this.appValForm.controls.categoryId.setValue(this.skillid);

    this.route.queryParams.subscribe((params: Params) => {
      this.Id = params['id'];
      this.skillid = params['skill'];
      if (this.Id > 0) {
        this.PopulateData();
      }
    });
    this.categoryError = JobQuotationErrors.categoryId;
    this.subCategoryError = JobQuotationErrors.subcatgoryId;
    this.jobTitleError = JobQuotationErrors.jobTitle;
    this.jobDescriptionError = JobQuotationErrors.jobDescription;
    this.startedDateError = JobQuotationErrors.startDateError;
    this.budgetError = JobQuotationErrors.budgetError;
    this.budgetPatternError = JobQuotationErrors.budgetPatternError;
    this.route.queryParams.subscribe((params: Params) => {
      
      this.skillId = params['skillId'];
      if (this.skillId == undefined) {
        this.skillId = 0;
      }
      
      if (params['skillIdForJob'] > 0) {
        this.selectSkill(params['skillIdForJob'] );
        this.appValForm.controls.subCategoryId.setValue(params['subSkillIdForJob']);
        this.getSubSkillDetails(params['subSkillIdForJob'])
      }
      if (this.skillId > 0) {
        this.showSubCategoryList = true;
       // this.CheckingSubcategory(this.skillId);
        this.selectSkill(this.skillId);
  
      }
    });


  }

  public getLoggedUserDetails(userRole: string, userId: string) {

    this.common.get(this.common.apiRoutes.UserManagement.GetUserDetailsByUserRole + `?userRole=${userRole}&userId=${userId}`).subscribe(result => {
      this.loggedUserDetails = <IPersonalDetails>result;
      if(this.loggedUserDetails?.mobileNumber){
        this.getUserFromFacebookLeads(this.loggedUserDetails.mobileNumber);
      }
    });
  }

  public getUserFromFacebookLeads(phoneNumber:string){
    let obj = {
      phoneNumber:phoneNumber
    };
    this.common.PostData(this.common.apiRoutes.Jobs.getUserFromFacebookLeads,JSON.stringify(obj)).then(res =>{
      let response = res;
      if(response.status == httpStatus.Ok){
        if(response?.resultData){
          this.facebookLeadUser = response.resultData[0];
          this.selectSkill(this.facebookLeadUser.skillId.toString());
         // this.appValForm.controls['subCategoryId'].setValue(this.facebookLeadUser.subSkillId);
          // this.SelectSubCategory(this.facebookLeadUser.subSkillId);
          //  this.appValForm.controls['startedDate'].setValue(this.facebookLeadUser.startedDate);
        }
      }
    },error =>{
      console.log(error);
    });
  }

    //------------------- show Verify Message Modal
  ngAfterViewInit(){
    if(!this.userVerify){
      this.verifyAccountMessageModal.hide();
    }
    else{
      this.verifyAccountMessageModal.show();
    }
  }
  isUserLogin() {
    if (!this.common.IsUserLogIn) {
      this.common.NavigateToRoute(this.common.apiUrls.Login.customer);
    }
    else {

      localStorage.setItem("Role", '3');
      localStorage.setItem("Show", 'true');
      
    }
  }
  public selectSkill(skillId: string) {
    
    this.getQuotationsVM.categoryId = skillId;
    this.appValForm.controls.categoryId.setValue(skillId);
   
    this.common.get(this.common.apiRoutes.Jobs.GetTradesmanSkillsByParentId + "?parentId=" + skillId).subscribe(result => {
      let res: IIdValue[] = <IIdValue[]>result ;
    //  console.log(res);
      if (res.length > 0) {
        this.listOfSubSkills = res;
        this.showSubCategoryList = true;
      }
      else {
        this.showSubCategoryList = false;
      }
    },
      error => {
        console.log(error);
        this.common.Notification.error(CommonErrors.commonErrorMessage);
      })
  }
  showHideNav() {
    alert();
  }
  get f() { return this.appValForm.controls; }
  public Save() {
    
    this.DataList = [];
    this.submitted = true;
    let subCategoryId = this.appValForm.value.subCategoryId;
    if(!subCategoryId){
      this.appValForm.controls['subCategoryId'].setValue(0);
    }
    if (this.appValForm.invalid) {

      this.appValForm.markAllAsTouched();
    }
    else {
     
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
        this.getQuotationsVM.imageVMs = [];
      }
      if (this.token != null) {
        this.common.GetData(this.common.apiRoutes.IdentityServer.GetPhoneNumberVerificationStatus + "?userId=" + this.token, true).then(result => {
          if (result  == true) {
            this.getQuotationsVM = this.appValForm.value;
            this.getQuotationsVM.imageVMs = this.DataList;
            this.getQuotationsVM.statusId = BidStatus.Pending;
            this.getQuotationsVM.jobQuotationId = this.Id;
            this.getQuotationsVM.visitCharges = this.visitCharges;
            this.getQuotationsVM.serviceCharges = this.subSkillPrice;
            this.getQuotationsVM.jobStartTime = this.appValForm.value.startTime.hour + ":" + this.appValForm.value.startTime.minute;
            // this.getQuotationsVM.jobstartDateTime = new Date();
            if (this.getQuotationsVM.imageVMs.length > 0) {
              this.getQuotationsVM.imageVMs[0].IsMain = true;
            }
            this.showSubCategoryList = false;

            localStorage.setItem("step1Data", JSON.stringify(this.getQuotationsVM));
            console.log(this.response.resultData);
            this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.Quotations.getQuotes2, { queryParams: { 'id': this.response.resultData } });
          }
          else {
            this.verifyAccountMessageModal.show();
          }

        });
      }
    }
    
  }

  public verifyAccount() {
    this.common.NavigateToRoute(this.common.apiUrls.User.PersonalProfile);
  }

  public closeVerifyAccountMessageModal() {
    this.verifyAccountMessageModal.hide();
  }

  public PopulateData() {
   
    let step1Data = localStorage.getItem("step1Data");
    this.getQuotationsVM = step1Data != null ? JSON.parse(step1Data):"";
    this.appValForm.patchValue(this.getQuotationsVM);
    console.log(this.getQuotationsVM);
    //if (this.getQuotationsVM.subCategoryId == 0) {
    //  this.appValForm.controls.subCategoryId.setValue(null);
    //}
    
    this.getQuotationsVM?.imageVMs?.forEach(res => {
      this.listOfImages.push(res);
    });
    let subCategoryId = this.getQuotationsVM?.subCategoryId != undefined ? this.getQuotationsVM?.subCategoryId : 0;
    let CategoryId = this.getQuotationsVM?.categoryId != undefined ? this.getQuotationsVM?.categoryId : 0;
    if (subCategoryId > 0) {
      this.showSubCategoryList = true;
      this.CheckingSubcategory(CategoryId.toString());
      this.appValForm.controls.subCategoryId.setValue(this.getQuotationsVM.subCategoryId);
    }
  }

  public GetSkills() {
    
    this.common.get(this.common.apiRoutes.Jobs.GetTradesmanSkillsByParentId + "?parentId=0").subscribe(result => {
      this.listOfSkills = <IIdValue[]>result;
      this.skillautoset = this.skillid;
     // this.selectSkill(this.skillId);
    },
      error => {
        console.log(error);
      })
  }

  public CheckingSubcategory(categoryId: string) {
    
    this.getQuotationsVM.categoryId = categoryId;
    this.common.get(this.common.apiRoutes.Jobs.GetTradesmanSkillsByParentId + "?parentId=" + categoryId).subscribe(result => {
      let res = <any>result ;
      if (res.length > 0) {
        this.showSubCategoryList = true;
        this.listOfSubSkills = res;
        this.appValForm.controls.subCategoryId.setValue(null);
      }
      else {
        this.showSubCategoryList = false;
        this.appValForm.controls.subCategoryId.setValue(0);
      }

    },
      error => {
        console.log(error);
        this.common.Notification.error(CommonErrors.commonErrorMessage);
      })
  }
  getSubSkillDetails(subSkillId: number) {
    this.common.get(this.common.apiRoutes.Tradesman.GetSubSkillDetails + "?subSkillId=" + subSkillId).subscribe(result => {
      let respone: ISubSkill = <ISubSkill>result ;
      if (respone) {
        this.subSkillPrice = respone.subSkillPrice;
        this.appValForm.controls['budget'].setValue(this.subSkillPrice);
        this.visitCharges = respone.visitCharges != null ? respone.visitCharges : 0 ;
      }
    })
  }
  public SelectSubCategory(subcategoryId: number) {
    this.showCategoryList = true;
    this.getQuotationsVM.subCategoryId = subcategoryId;
    this.getSubSkillDetails(subcategoryId);
    var index = 0

    while (this.listOfSubSkills[index].id != subcategoryId) {
      index++;
    }
    this.selectedSubCategory = "& " + this.listOfSubSkills[index].value;
  }
  handleBudgetAmount(amount: number) {
    if (amount < this.subSkillPrice) {
      this.appValForm.controls['budget'].setErrors({ invalidBudget:"Your budget can't be less than tentative market rate"})
    }
  }
  public OnSelectFile(event: Event) {
    if (event.target) {
      this.fileLength = (<HTMLInputElement>event?.target)?.files?.length
    }
    // this.listOfImages = [];
    if (this.fileLength) {
      this.getQuotationsVM.imageVMs = [];
      var imageCount = (this.fileLength) + (this.listOfImages.length);
      if (this.fileLength > 4 || this.listOfImages.length > 4 || imageCount > 4) {
        this.imageSubmitted = true;
        this.common.Notification.error(this.imageError);
        return;
      }
      else {
        for (var i = 0; i < this.fileLength; i++) {
          this.blob = (<HTMLInputElement>event?.target)?.files?.[i]
          var reader = new FileReader();
          reader.onload = (event: any) => {
            this.listofFiles.push(event.target.result);
            this.imageVm.localUrl = event.target.result;
            this.compressFile(this.imageVm.localUrl, this.imageVm.filePath, this.imageVm, i);
          }
          this.blob != undefined ? reader.readAsDataURL(this.blob):"";
        }
      }
    }
   
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

  public compressFile(image: any, fileName: string, imageVms: ImageVM, index:number) {
    var orientation = -1;
    this.sizeOfOriginalImage = this.imageCompress.byteCount(image) / (1024 * 1024);
    this.imageCompress.compressFile(image, orientation, 50, 50).then(
      result => {
        this.imgResultAfterCompress = result;
        this.localCompressedURl = result;
        this.sizeOFCompressedImage = this.imageCompress.byteCount(result) / (1024 * 1024)
        this.imageVm = {} as  ImageVM;
        this.imageVm.filePath = imageVms;
        const imageBlob = this.dataURItoBlob(this.imgResultAfterCompress.split(',')[1]);
        this.imageVm.imageBase64 = result;
        this.listOfImages.push(this.imageVm);
      })
  }

  public Close() {
    this.imageModal.hide();
  }


  public openModal() {
    const element = document.getElementById("myModalLightbox");
    if (element != null) {
      element.style.display = "block";
    }
  }

  public closeModal() {
    const element = document.getElementById("myModalLightbox");
    if (element != null) {
      element.style.display = "none";
    }
  }


  public plusSlides(n: number) {
    this.showSlides(this.slideIndex += n);
  }

  public currentSlide(n: number) {
    this.showSlides(this.slideIndex = n);
  }

  public showSlides(n: number) {
    var slides = document.getElementsByClassName("lightboxImg");

    if (n > slides.length) { this.slideIndex = 1 }
    if (n < 1) { this.slideIndex = slides.length }

    let img: any = slides[this.slideIndex - 1];
    this.mainImage = img.src;
  }
  CancelImage(index: number) {
    this.listOfImages.splice(index - 1, 1);
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


  public bindMetaTags() {
    this.common.get(this.common.apiRoutes.CMS.GetSeoPageById + "?pageId=33").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description, res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }
}
