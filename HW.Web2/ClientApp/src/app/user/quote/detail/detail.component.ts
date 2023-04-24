import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';
import { httpStatus, CommonErrors, BidStatus } from '../../../shared/Enums/enums';
import { ActivatedRoute, Params } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { PostedJobDetailVM, PostedJobsVM, ImageVM, GetQuotes } from '../../../models/userModels/userModels';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { NgxImageCompressService } from 'ngx-image-compress';
import { ToastrService } from 'ngx-toastr';
import { DatePipe, formatDate } from '@angular/common';
import { IdValueVm } from '../../../models/commonModels/commonModels';
import { IIdValue, IImage, IJobQuotationDetail, ISubSkill, ITownList, ITownListSearch } from '../../../shared/Enums/Interface';
@Component({
  selector: 'app-get-posted-job-detail',
  templateUrl: './detail.component.html',

})
export class QuoteDetailComponent implements OnInit {
  public jobQuotationId: number=0;
  public submittedForm = false;
  public meridian = true;
  public appValForm: FormGroup;
  public CitiesList: IIdValue[] = [];
  //public SubCategoriesList: any = [];
  public listOfSkills: IdValueVm[] = [];
  public listOfSubSkills: IdValueVm[] = [];
  public selectedSkill: string="";
  public selectedSubSkill: string="";
  public showSubSkillList: boolean = false;
  public ImagesList: any[] = [];
  public jobStartTime: string="";
  public imageContent: string="";
  public readOnly: boolean = true;
  public imgResultBeforeCompress: string="";
  public imgResultAfterCompress: string="";
  public townList: ITownList[] = [];
  public searchtownList: ITownListSearch[] = [];
  public cancelBtn: boolean = true;
  public townInvalidInput: boolean = false;
  public initValue: number = 0;
  townkeyword: string = 'value';
  //public postJobDetail: PostedJobDetailVM = new PostedJobDetailVM();
  public postJobDetail: GetQuotes = {} as  GetQuotes;
  cityName: any;
  @ViewChild('CancelJob', { static: true }) CancelJob: ModalDirective;
  @ViewChild('ImageModel', { static: true }) ImageModel: ModalDirective;

  public imageSubmitted: boolean = false;
  public listOfImages: ImageVM[] = [];
  public listofFiles: any[] = [];
  public imageVm: ImageVM = {} as  ImageVM;
  public sizeOfOriginalImage: number=0;
  public sizeOFCompressedImage: number=0;
  public fileLength: number | undefined = 0;
  public file: Blob | undefined;
  public localUrl: any;
  public localCompressedURl: any;
  public selectedTime:string="";
  public DataList: ImageVM[] = [];
  public time = { hour: 0, minute: 0 };
  public mainImage: any;
  public hasNoImage: boolean = false;
  public slideIndex: number = 1;
  public maxdate1: string="";
  public pipe: DatePipe;
  public date: Date = new Date;
  public day: string="";
  public month: string="";
  public year: string="";
  public bidId: any;
  public skillid: number=0;
  public skillautoset = 0;
  public bidCount: number = 0;
  public townChanged: boolean = false;
  public subSkillPrice:number=0;
  public visitCharges:number=0;
  public skillMarketRate: any;
  public customerBudget: any;
  public disableSelectedCategory: boolean = false;
  public invalidCustomerBudget: boolean = false;
  constructor(private toast: ToastrService,
    public common: CommonService,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private imageCompress: NgxImageCompressService,
  ) {
    this.appValForm = {} as FormGroup;
    this.CancelJob = {} as ModalDirective;
    this.ImageModel = {} as ModalDirective;
    this.pipe = {} as DatePipe;
  }
  ngOnInit() {
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
    this.appValForm = this.formBuilder.group({
      address: ['', [Validators.required]],
      area: ['', [Validators.required]],
      budget: ['', [Validators.required]],
      categoryId: ['', Validators.required],
      cityId: ['', Validators.required],
      jobDescription: ['', [Validators.required]],
      startTime: [''],
      startingDateTime: ['', [Validators.required]],
      subSkillId: [''],
      title: ['', [Validators.required]],
      quotesQuantity: [0],
      visitCharges: [0],
    });
    this.route.queryParams.subscribe((params: Params) => {
      
      this.jobQuotationId = params['jobQuotationId'];
      this.bidId = params['bidId'];
      console.log(this.bidId);
      this.bidCount = params['bidCount'];
      if (this.jobQuotationId > 0)
        this.GetSkills();
      this.PopulateData();
      if (this.bidCount > 0 || this.bidId > 0) {
        this.cancelBtn = false;
        this.disableSelectedCategory = true;
      }
    });
    
  }
  getSubSkillDetails(subSkillId: number | undefined) {

    this.common.get(this.common.apiRoutes.Tradesman.GetSubSkillDetails + "?subSkillId=" + subSkillId).subscribe(result => {

      let respone: ISubSkill = <ISubSkill>result ;
      if (respone) {
        this.subSkillPrice = respone.subSkillPrice;
        this.appValForm.controls['subSkillId'].setValue(respone.subSkillId);
        this.appValForm.controls['budget'].setValue(this.subSkillPrice);
        this.visitCharges = respone.visitCharges != null ? respone.visitCharges : 0;
        this.appValForm.controls['visitCharges'].setValue(this.visitCharges);
        var data = this.appValForm.value;
        console.log(data);
      }
    })
  }
  handleBudgetAmount(amount:number) {

    if (amount < this.subSkillPrice) {
      this.appValForm.controls['budget'].setErrors({ invalidBudget: "Your budget can't be less than tentative market rate" })
    }

  }

  public selectedCity(cId:number){
    let cityId = cId;
    this.getTownList(cityId);
  }
  public getTownList(cId:number) {
    this.searchtownList = [];
    let cityId = cId.toString();
    this.common.GetData(this.common.apiRoutes.UserManagement.GetTownsByCityId + "?cityId=" + cityId, false).then(res => {
      this.townList = res;
      this.townList.forEach((x) => {
        this.searchtownList.push({ value: x.name, id: x.townId });
        
      })
      console.log(this.searchtownList);
    });

  }
  public selecttownEvent(item: Event) {
    this.townChanged = true;
    console.log(item);
  }
  public unselecttownEvent(item: Event) {
  }
  public onChangetownSearch(val: string) {
    // fetch remote data from here
    // And reassign the 'data' which is binded to 'data' property.
  }
  public GetSkills() {
    this.common.get(this.common.apiRoutes.Jobs.GetTradesmanSkillsByParentId + "?parentId=0").subscribe(result => {
      this.listOfSkills = <IdValueVm[]>result;

    },
      error => {
        console.log(error);
      })
  }

  public bindSubSkills(skillId: number) {

    ++this.initValue;
    //this.listOfSubSkills = [];
    this.common.get(this.common.apiRoutes.Jobs.GetTradesmanSkillsByParentId + "?parentId=" + skillId).subscribe(result => {
      this.listOfSubSkills = <IdValueVm[]>result;
      if(this.listOfSubSkills.length <= 0){
        this.showSubSkillList = false;
      }
      else{
        if (this.initValue > 1)
        this.getSubSkillDetails(this.listOfSubSkills[0].id);
        this.showSubSkillList = true;
      }
   
    });
    }
    public cityValue(event: Event) {
        let selectElementText = event != undefined && event.target != null ? (<HTMLSelectElement>event.target)['options']
        [(<HTMLSelectElement>event.target)['options'].selectedIndex].text:"";
    this.cityName = selectElementText;
    //this.preventDefault(this.cityName)
  }

  get f() {
    return this.appValForm.controls;
  }

  public Save() {
    this.DataList = [];
    this.submittedForm = true;
    if (this.appValForm.invalid) {
      return;
    }
    if (this.listOfImages.length > 0) {
      for (var i in this.listOfImages) {
        let as = <ImageVM>{};
        as.id = this.jobQuotationId;
        as.imageBase64 = this.listOfImages[i].imageBase64;
        this.DataList.push(as);
      }
    }
    else {
      this.postJobDetail.imageVMs = [];
    }
    var data = this.appValForm.value;
    console.log(data);

    this.postJobDetail.imageVMs = this.DataList;
    if (this.postJobDetail.imageVMs.length > 0) {
      this.postJobDetail.imageVMs[0].IsMain = true;
    }
    this.postJobDetail.jobQuotationId = this.jobQuotationId;
    this.postJobDetail.workTitle = data.title;
    this.postJobDetail.categoryId = data.categoryId;
    this.postJobDetail.jobDescription = data.jobDescription;
    this.postJobDetail.subCategoryId = data.subSkillId;
    this.postJobDetail.visitCharges = data.visitCharges;
    this.postJobDetail.numberOfBids = data.quotesQuantity;
    this.postJobDetail.cityId = data.cityId;
    this.customerBudget = this.appValForm.value.budget;

    if (this.townChanged) {
      this.postJobDetail.town = data.area.value;
      this.postJobDetail.area = data.area.value;
      let town = this.appValForm.value.area;
      this.postJobDetail.townId = town.id;
      console.log(this.postJobDetail.townId);
    }
    else {
      this.postJobDetail.town = data.area;
      this.postJobDetail.area = data.area;
      let town = this.appValForm.value.area;
      this.postJobDetail.townId = town.id;
    }
    this.postJobDetail.streetAddress = data.address;
    this.postJobDetail.budget = data.budget;
    this.postJobDetail.jobstartDateTime = data.startingDateTime;
    this.postJobDetail.jobStartTime = data.startTime.hour + ":" + data.startTime.minute;;
    let filterTown = this.searchtownList.filter(x => x.value == this.postJobDetail.area);
    console.log(filterTown);
    if (filterTown.length <= 0) {
      this.townInvalidInput = true;
      this.appValForm.controls['area'].setErrors({ incorrect: true, inValidTown: 'Invalid town' });
      setTimeout(()=>{
        this.townInvalidInput = false;
      },3000);
      return;
    }

    this.customerBudget = this.appValForm.value.budget;
    if (this.customerBudget < this.subSkillPrice) {
      this.appValForm.controls['budget'].setErrors({ invalidBudget: "Your budget can't be less than tentative market rate" })
      return;
    }
   
    this.common.PostData(this.common.apiRoutes.Customers.JobQuotationsWeb, this.postJobDetail, true).then(result => {
      if (status == httpStatus.Ok) {
        //this.common.Notification.show("Saved Successfully!");
        this.common.NavigateToRoute(this.common.apiUrls.User.GetPostedJobs)
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }
  public PopulateData() {

    this.common.GetData(this.common.apiRoutes.Jobs.GetPostedJobDetailByJobQuotationId + "?jobQuotationId=" + this.jobQuotationId, true).then(result => {

      var data: IJobQuotationDetail = result ;
      console.log(data);
      this.bindSubSkills(data.categoryId);
      let timeArr = data.jobStartingTime.split(":");
      this.time = { hour: Number(timeArr[0]), minute: Number(timeArr[1]) }
      data.startTime = this.time;
      setTimeout(() => {
        this.appValForm.patchValue(data);
        this.subSkillPrice = this.appValForm.value.budget;
        this.appValForm.controls.startingDateTime.setValue(formatDate(data.startingDateTime, 'yyyy-MM-dd', 'en'));
      }, 1000)
      this.CitiesList = data.citiesList;
      this.selectedCity(data.cityId);
      this.ImagesList = data.images;
      for (var i = 0; i < this.ImagesList.length; i++) {
        if (this.ImagesList[i].imageContent != null) {
          var imgContent = 'data:image/png;base64,' + this.ImagesList[i].imageContent;
          this.ImagesList[i].imageBase64 = imgContent.toString();
        }
      }
      this.listOfImages = this.ImagesList;
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }

  public DeleteJob() {
    this.common.Notification.show("Job Cancelled");
    this.common.GetData(this.common.apiRoutes.Users.CancelJob + "?jobQuotationId=" + this.jobQuotationId + "&statusId=" + BidStatus.Deleted, true).then(result => {
      if (status = httpStatus.Ok) {
        //this.common.Notification.success("Job Cancelled!")
        this.common.NavigateToRoute(this.common.apiUrls.User.GetPostedJobs);
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }

  public OnSelectFile(event: Event) {
    this.postJobDetail.imageVMs = [];
    this.fileLength = (<HTMLInputElement>event.target).files?.length
    if (this.fileLength) {
      var imageCount = (this.fileLength) + (this.listOfImages.length);
      if (this.fileLength > 4 || this.listOfImages.length > 4 || imageCount > 4) {
        this.imageSubmitted = true;
        this.common.Notification.warning("Limit Exceeded! You can select 4 more image(s).")
        return;
      }
      else {
        for (var i = 0; i < this.fileLength; i++) {
          var reader = new FileReader();
          var file = (<HTMLInputElement>event.target).files?.[i];
          this.imageVm.filePath = file != undefined ? file['name']:"";
          reader.onload = (event: any) => {
            this.listofFiles.push(event.target.result);
            this.imageVm.localUrl = event.target.result;
            this.compressFile(this.imageVm.localUrl, this.imageVm.filePath, this.imageVm, i);
          }
          this.file = (<HTMLInputElement>event.target).files?.[i]
          this.file != undefined ? reader.readAsDataURL(this.file):"";
        }
      }
    }
  }

  public dataURItoBlob(dataURI:string) {
    const byteString = window.atob(dataURI);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8Array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) {
      int8Array[i] = byteString.charCodeAt(i);
    }
    const blob = new Blob([int8Array], { type: 'image/png' });
    return blob;
  }

  public compressFile(image: string | undefined, fileName: string, imageVms: any, index: number) {
    var orientation = -1;
    this.sizeOfOriginalImage = this.imageCompress.byteCount(image) / (1024 * 1024);
    this.imageCompress.compressFile(image, orientation, 50, 50).then(
      result => {
        this.imgResultAfterCompress = result;
        this.localCompressedURl = result;
        this.sizeOFCompressedImage = this.imageCompress.byteCount(result) / (1024 * 1024)
        this.imageVm = {} as  ImageVM;
        const imageBlob = this.dataURItoBlob(this.imgResultAfterCompress.split(',')[1]);
        this.imageVm.filePath = fileName;
        this.imageVm.imageBase64 = result;
        this.imageVm.localUrl = "";
        this.listOfImages.push(this.imageVm);
      })
  }


  public ConfirmModel() {
    this.CancelJob.show();
  }

  public CloseModel() {
    this.CancelJob.hide();
  }

  public ImagePopUp(image:string) {
    this.imageContent = image;
    this.ImageModel.show()

  }

  public Close() {
    this.ImageModel.hide();
  }

  public EditJob() {

    this.readOnly = false;
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


  public plusSlides(n:number) {
    this.showSlides(this.slideIndex += n);
  }

  public currentSlide(n:number) {

    this.showSlides(this.slideIndex = n);
  }

  public showSlides(n:number) {
    var slides = document.getElementsByClassName("lightboxImg");

    if (n > slides.length) { this.slideIndex = 1 }
    if (n < 1) { this.slideIndex = slides.length }

    let img: any = slides[this.slideIndex - 1];
    this.mainImage = img.src;
  }

  public CancelImage(index:number) {

    this.listOfImages.splice(index - 1, 1);
  }

  charOnly(event:any): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if ((charCode > 47 && charCode < 58) || ((charCode > 32 && charCode <= 47)) || ((charCode >= 58 && charCode <= 64)) || ((charCode >= 91 && charCode <= 96)) || ((charCode >= 123 && charCode <= 126))) {
      return false;
    }
    return true;
  }

  AllowNonZeroIntegers(e:any): boolean {

    var val = e.keyCode;
    // var target = event.target ? event.target : event.srcElement;
    if (val == 48 && e.target.value == "" || val == 101 || val == 45 || val == 46 || ((val >= 65 && val <= 90)) || ((val >= 97 && val <= 122))) {
      return false;
    }
    else if ((val >= 48 && val < 58) || ((val > 96 && val < 106)) || val == 46 || val == 8 || val == 127 || val == 189 || val == 109 || val == 9) {
      return true;
    }
    else {
      return false;
    }
  }


  numberOnly(event:any): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }


}
