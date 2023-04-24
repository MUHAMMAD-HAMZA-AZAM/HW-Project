import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../Shared/HttpClient/_http';
import { JwtHelperService } from '@auth0/angular-jwt/src/jwthelper.service';
import { NgxSpinnerService } from "ngx-spinner";
import { Router } from '@angular/router';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DatePipe, DOCUMENT } from '@angular/common';
import { IdValueVm } from '../../Shared/Models/UserModel/SpSupplierVM';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { CampaignTypes, StatusCode } from '../../Shared/Enums/enums';



@Component({
  selector: 'app-active-promotion',
  templateUrl: './active-promotion.component.html',
  styleUrls: ['./active-promotion.component.css']
})
export class ActivePromotionComponent implements OnInit {
  jwtHelperService: JwtHelperService = new JwtHelperService();
  public base64Image: string;
  public fileSelected: boolean = false;
  public base64Mobile: string;
  public fileSelectedMobile: boolean = false;
  redirectURL: string;
  public formAction = "add";
  activePromotionForm: FormGroup;
  public activePromotionList: any = [];
  public categoryList: any = [];
  public listOfCampaignTypes: any = [];
  public promoDetails = { name: null, skillName: null, image: null, description: null };
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  public userRolesList: IdValueVm[] = [{ id: 0, value: "Business" }, { id: 1, value: "Tradesman" }, { id: 2, value: "Organization" }, { id: 3, value: "Customer" }, { id: 4, value: "Supplier" }];
  public pipe;
  public date: Date;
  public day: string;
  public month: string;
  public year: string;
  public maxdate1: string;
  public dropdownSetting;
  public subSkillList;
  public subSkillDropDownList;
  public selectedItems;
  public config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '15rem',
    minHeight: '5rem',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Arial',
    toolbarHiddenButtons: [
      ['textColor'],
      ['backgroundColor'],
    ],
    customClasses: [
      {
        name: "quote",
        class: "quote",
      },
      {
        name: 'redText',
        class: 'redText'
      },
      {
        name: "titleText",
        class: "titleText",
        tag: "h1",
      },
    ]
  };
  @ViewChild("inputFile") inputFile: ElementRef;
  @ViewChild("inputFileMobile") inputFileMobile: ElementRef;

  constructor(@Inject(DOCUMENT) public document: Document,public _modalService: NgbModal, public router: Router, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService) {


  }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Active Promotions"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');

    let url = this.document.URL;
    if (url.includes("admin")) {
      this.redirectURL = 'https://www.hoomwork.com/';
    }
    else if (url.includes("admin2")) {
      this.redirectURL = 'https://test2.hoomwork.com/';
    }
    else {
      this.redirectURL = 'http://localhost:4200/';
    }
    this.activePromotionForm = this.fb.group({
      promotionId: [0],
      name: ['', [Validators.required]],
      description: ['', [Validators.required]],
      isAcitve: [false],
      isMain: [false],
      skillId: [null, [Validators.required]],
      subSkillIds: [''],
      campaignTypeId: [null, [Validators.required]],
      userRoleId: [null, [Validators.required]],
      promotionStartDate: ['', [Validators.required]],
      promotionEndDate: ['', [Validators.required]],
    })
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
    this.getPromotionList();
    this.getCategoryList();
    this.getCampaignTypesList();
    this.dropdownSetting = {
      singleSelection: false,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: true,
      itemsShowLimit: 2,
    };
  }
  fileChangeEvent(event: any): void {
    var fReader = new FileReader();
    var file = event.target.files[0];
    if (file.type != "image/png" && file.type != "image/jpg" && file.type != "image/jpeg") {
      this.inputFile.nativeElement = null;
      this.toastr.error("Upload Only PNG ,JPEG or JPG format", "Image Type");
      return;
    }
    else {
      fReader.readAsDataURL(file);
      fReader.onload = (event: any) => {
        this.base64Image = event.target.result;
        this.fileSelected = true;
      };
    }
  }
  fileChangeEventMobile(event: any): void {
    var fReaders = new FileReader();
    var files = event.target.files[0];
    if (files.type != "image/png" && files.type != "image/jpg" && files.type != "image/jpeg") {
      this.inputFileMobile.nativeElement = null;
      this.toastr.error("Upload Only PNG ,JPEG or JPG format", "Image Type");
      return;
    }
    else {
      fReaders.readAsDataURL(files);
      fReaders.onload = (event: any) => {
        this.base64Mobile = event.target.result;
        this.fileSelectedMobile = true;
      };
    }
  }

  //----------------------- Show Campaign Type List
  getCampaignTypesList() {
    this.Loader.show()
    this.service.get(this.service.apiRoutes.UserManagement.GetCompaignsTypeList).subscribe(result => {
      let response = result.json();
      if (response.status == StatusCode.OK) {
        this.listOfCampaignTypes = response.resultData;
      }
      this.Loader.hide();
    });
  }

  campaignType(event) {
    var selectedCampaignTypeId = event.target.value;
    if (selectedCampaignTypeId == CampaignTypes.Advertisement) {
      this.activePromotionForm.controls.promotionStartDate.clearValidators();
      this.activePromotionForm.controls.promotionStartDate.updateValueAndValidity();
      this.activePromotionForm.controls.promotionEndDate.clearValidators();
      this.activePromotionForm.controls.promotionEndDate.updateValueAndValidity();
    }
    else {
      this.activePromotionForm.controls.promotionStartDate.setValue(null);
      this.activePromotionForm.controls.promotionEndDate.setValue(null);
      this.activePromotionForm.controls.promotionStartDate.setValidators([Validators.required]);
      this.activePromotionForm.controls.promotionEndDate.setValidators([Validators.required]);
      this.activePromotionForm.updateValueAndValidity();
      this.activePromotionForm.markAllAsTouched();
      return;
    }
  }

  // Show Promotions List
  getPromotionList() {
    debugger;
    this.Loader.show();
    this.service.get(this.service.apiRoutes.UserManagement.GetPromotionList).subscribe(result => {
      let response = result.json();
      this.activePromotionList = response;
      debugger;
      console.log(this.activePromotionList);
      this.service.get(this.service.apiRoutes.TrdesMan.GetSubSkillsBySkillId).subscribe(subskillList => {
        this.subSkillList = subskillList.json();
        debugger;
        response.forEach(x => {
          if (x.subSkillIds) {
            let data = [];
            let subSkillIds = x.subSkillIds.split(",");
            for (let i = 0; i < subSkillIds.length; i++) {
              data.push(this.subSkillList.filter(item => item.id == subSkillIds[i])[0])
            }
            let subSkillNames = [];
            data.forEach(y => {
              subSkillNames.push({ id: y.id, value: y.value })
            })
            x.subSkillIds = subSkillNames;
          }
        })
      });
      this.Loader.hide();
    })
  }
  getCategoryList() {
    this.Loader.show()
    this.service.get(this.service.apiRoutes.TrdesMan.GetTradesManSkills).subscribe(result => {
      this.categoryList = result.json();
      this.Loader.hide();
    });
  }
  handleSkillChange(e) {
    this.service.get(this.service.apiRoutes.TrdesMan.GetSubSkillsBySkillId + `?skillId=${e.target.value}`).subscribe(skills => {
      this.activePromotionForm.controls['subSkillIds'].setValue('');
      this.subSkillDropDownList = skills.json();
    })
  }
  // Add New Promotion
  handleSubmit() {
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    let formValue = this.activePromotionForm.value;
    console.log(formValue);
    let skillIds = [];
    if (formValue.subSkillIds) {
      formValue.subSkillIds.forEach(x => {
        skillIds.push(x.id);
      });
    }
    formValue.subSkillIds = skillIds.toString();
    if (this.formAction == "add") {
      formValue.promotionId = 0,
      formValue.base64Image = this.base64Image;
      formValue.Base64Mobile = this.base64Mobile;

      formValue.action = this.formAction;
      formValue.createdBy = decodedtoken.UserId;
      this.fileSelected = false;
      this.fileSelectedMobile = false;

    }
    else {
      if (this.fileSelected) {
        formValue.base64Image = this.base64Image;
      }
      if(this.fileSelectedMobile){
        formValue.base64Mobile = this.base64Mobile;
      }
      formValue.action = this.formAction;
      formValue.modifiedBy = decodedtoken.UserId;
      console.log(formValue);
    }

    console.log(formValue);
    this.service.PostData(this.service.apiRoutes.UserManagement.InsertAndUpDateActivePromotion, formValue, true).then(result => {
      let response = result.json();
      if (response.status == 200) {
        this.formAction = "add"
        this.toastr.success(response.message, "Success");
        this.subSkillDropDownList = '';
        this.getPromotionList();
        this.resetForm();
      }
    })
  }

  // Update Promotion
  updatePromotion(obj) {
    this.formAction = "update"
    var datePipe = new DatePipe("en-US");
    let promotionStartDate = datePipe.transform(obj.promotionStartDate, 'yyyy-MM-dd');
    let promotionEndDate = datePipe.transform(obj.promotionEndDate, 'yyyy-MM-dd');
    obj.promotionStartDate = promotionStartDate;
    obj.promotionEndDate = promotionEndDate;
    this.activePromotionForm.patchValue(obj);
    //this.campaignType(obj.campaignTypeId);
    console.log(this.activePromotionForm.value);
    //this.activePromotionForm.controls.subSkillIds.setValue(obj.subSkillName)
    this.base64Image = 'data:image/png;base64,' + obj.image;
    this.base64Mobile = 'data:image/png;base64,' + obj.imageMobile;
    console.log(this.base64Image);
  }
  // View Promotion Details
  viewPromotionDetails(promoId, content) {
    this.activePromotionList.filter(x => x.promotionId == promoId).map(y => {
      this.promoDetails = {
        name: y.name,
        description: y.description,
        skillName: y.skillName,
        image: y.image,
      }
    });
    this._modalService.open(content, { size: 'lg', scrollable: true, centered: true });
  }

  // Delete Promotion 
  deletePromotion(promotionId) {
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    let obj = {
      action: "delete",
      promotionId,
      modifiedBy: decodedtoken.UserId
    }
    this.service.PostData(this.service.apiRoutes.UserManagement.InsertAndUpDateActivePromotion, obj, true).then(result => {
      let response = result.json();
      if (response.status == 200) {
        this.toastr.success(response.message, "Success");
        this.getPromotionList();
      }
    })

  }
  resetForm() {
    this.activePromotionForm.reset();
    this.activePromotionForm.controls['promotionId'].setValue(0);
    this.subSkillDropDownList = '';
    this.inputFile.nativeElement.value = null;
    this.base64Image = "";
    this.inputFileMobile.nativeElement.value = null;
    this.base64Mobile = "";
  }
  get f() {
    return this.activePromotionForm.controls;
  }
}
