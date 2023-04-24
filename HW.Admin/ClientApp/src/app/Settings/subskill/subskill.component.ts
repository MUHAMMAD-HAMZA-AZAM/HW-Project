import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt/src/jwthelper.service';
import { ToastrService } from 'ngx-toastr';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CommonService } from '../../Shared/HttpClient/_http';
import { NgxSpinnerService } from "ngx-spinner";
import { ImageCroppedEvent } from 'ngx-image-cropper';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { NgForm } from '@angular/forms';
import { debounce } from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({
  selector: 'app-subskill',
  templateUrl: './subskill.component.html',
  styleUrls: ['./subskill.component.css']
})
export class SubskillComponent implements OnInit {
  jwtHelperService: JwtHelperService = new JwtHelperService();

  public subSkillList: [] = [];
  public subSkillName: string = "";
  public skillId: number;
  public isSkillId: number;
  public subSkillNameExist: string = "";
  public PriceReview: string = "";
  public Name: string = "";
  public subSkillData: any;
  public confirmDelete: boolean = false;
  public subSkillDeleteId: any;
  public subSkillModelText = "Add New Sub Skill";

  public skillList: [] = [];
  SelectedSkillsList = [];
  public selectedSkills = [];
  public skillsdropdownSettings = {};
  public dropdownListForColumn = {};

  public toggle = true;
  //public items = [];
  public skillTitleName = "";
  public subSkillMinimumAmount = "";
  public skillDescription = "";
  public fileChanged = false;
  public subSkillImage : any;
  public imgPath = "";
  public seoDescription = "";
  public visitCharges = "";

   
  ////// Image //////
  imageChangedEvent: any = '';
  croppedImage: any = '';
  editorConfig: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '5rem',
    minHeight: '10rem',
    maxHeight: 'auto',
    width: 'auto',
    minWidth: '0',
    translate: 'yes',
    enableToolbar: true,
    showToolbar: true,
    placeholder: 'Enter text here...',
    defaultParagraphSeparator: '',
    defaultFontName: '',
    defaultFontSize: '',
    fonts: [
      { class: 'arial', name: 'Arial' },
      { class: 'times-new-roman', name: 'Times New Roman' },
      { class: 'calibri', name: 'Calibri' },
      { class: 'comic-sans-ms', name: 'Comic Sans MS' }
    ],
    customClasses: [
      {
        name: 'quote',
        class: 'quote',
      },
      {
        name: 'redText',
        class: 'redText'
      },
      {
        name: 'titleText',
        class: 'titleText',
        tag: 'h1',
      },
    ],
    uploadUrl: 'v1/image',
    uploadWithCredentials: false,
    sanitize: true,
    toolbarPosition: 'top',
    //toolbarHiddenButtons: [
    //  ['bold', 'italic'],
    //  ['fontSize']
    //]
  };
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    
  constructor(public router: Router,public toastrService: ToastrService, public _modalService: NgbModal, public service: CommonService, public Loader: NgxSpinnerService,) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Sub Skills"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.skillsdropdownSettings = {
      singleSelection: true,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };
    this.getSkillList();
    this.getSubSkillList();
  }
  ////// Image //////
  fileChangeEvent(event: any): void {
    this.fileChanged = true;
    this.imageChangedEvent = event;
    this.imgPath = event.target.files[0].name;
    console.log(this.subSkillImage);
  }
  imageCropped(event: ImageCroppedEvent) {
    this.croppedImage = event.base64;
  }
  imageLoaded() {
  }
  cropperReady() {
  }
  loadImageFailed() {
  }
  handleToggle() {
    this.toggle = !this.toggle;
    this.subSkillData = null;
  }
  onSelect(event) {

  }
  save(f: NgForm) {
    debugger;
    this.skillId = Number(this.SelectedSkillsList[0].id);
  
  var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
  this.Loader.show();
    let cropImage;
    //if (this.croppedImage != null && this.fileChanged) {
     cropImage = this.croppedImage;
    //}
    //else {
    //}
      /////update subskill
    if (this.subSkillData != null) {
      
      if (this.fileChanged) {
        cropImage = this.croppedImage;
      }
      else {
        cropImage = this.subSkillData.subSkillImage;
        this.imgPath = "";
      }
      let obj = {
        SubSkillId: this.subSkillData.subSkillId,
        SubSkillName: this.subSkillName,
        SkillId: this.skillId,
        ModifiedBy: decodedtoken.UserId,
        Base64Image: cropImage,
        SubSkillTitle: this.skillTitleName,
        MetaTags: this.seoDescription,
        Description: this.skillDescription,
        ImagePath: this.imgPath,
        Slug: this.service.convertToSlug(this.subSkillName),
        SubSkillPrice: this.subSkillMinimumAmount,
        VisitCharges: this.visitCharges,
        PriceReview: this.PriceReview,
      }
      console.log(obj);
      this.service.PostData(this.service.apiRoutes.TrdesMan.AddOrUpdateSubSkill, obj, true).then(result => {
        let response = result.json();
        
        console.log(response)
        if (response.status == 200) {
          f.reset();
          this.toggle = !this.toggle;
          this.subSkillName = "";
          this.toastrService.success("Data updated Successfully", "Success");
          //this._modalService.dismissAll() ;
          this.getSubSkillList();
          this.subSkillData = null;
          this.SelectedSkillsList = [];
          this.selectedSkills = [];
        }
        else if (response.status == 400) {
          this.toastrService.error(response.message , "Error");
        }
      })
    }
    ///// Add new sub skill
    else {
      this.fileChanged = false;
      let obj = {
        SubSkillName: this.subSkillName,
        SkillId: this.skillId,
        CreatedBy: decodedtoken.UserId,
        IsActive: false,
        Base64Image: cropImage,
        SubSkillTitle: this.skillTitleName,
        MetaTags: this.seoDescription,
        Description: this.skillDescription,
        ImagePath: this.imgPath,
        Slug: this.service.convertToSlug(this.subSkillName),
        SubSkillPrice: this.subSkillMinimumAmount,
        VisitCharges: this.visitCharges,
        PriceReview: this.PriceReview,
      }
      console.log(obj);
      this.service.PostData(this.service.apiRoutes.TrdesMan.AddOrUpdateSubSkill, obj, true).then(result => {
        
        let response = result.json();
        console.log(response);
        if (response.status == 200) {
          f.resetForm();
          this.toggle = !this.toggle;
          this.croppedImage = null;
          this.toastrService.success("Data added Successfully", "Success");
          this.getSubSkillList();
        }
        else if (response.status == 400) {
          this.toastrService.error(response.message, "Error");
        }
      })
    }
  this.Loader.hide();
  }
  updateSkill(skill, content) {
    this.toggle = !this.toggle; 
    this.subSkillModelText = "Update Sub Skill"
    if (skill != null && skill != "") {
      this.subSkillName = skill.subSkillName;
      this.selectedSkills = [];
      this.selectedSkills = [{ id: skill.skillId, value: skill.skillName }]
      this.SelectedSkillsList = [];
      this.SelectedSkillsList = this.selectedSkills;
      this.skillId = skill.skillId;
      this.isSkillId = skill.skillId;
      this.subSkillData = skill;
      this.skillTitleName = skill.subSkillTitle;
      this.seoDescription = skill.metaTags;
      this.skillDescription = skill.description;
      this.subSkillMinimumAmount = skill.subSkillPrice;
      this.visitCharges = skill.visitCharges;
      this.PriceReview = skill.priceReview;
      //this._modalService.open(content);
    }
  }
  showModal(content) {
    this.subSkillModelText = "Add New Sub Skill";
    this.subSkillName = "";
    this.selectedSkills = [];
    this._modalService.open(content)
  }
  deleteSkill(subSkillId, deleteContent) {
    this.subSkillDeleteId = Number(subSkillId);
    this._modalService.open(deleteContent);
  }
  confirmDeleteSkill() {
    if (this.subSkillDeleteId != null && this.subSkillDeleteId != "") {
      this.service.get(this.service.apiRoutes.TrdesMan.DeleteSkill + "?subSkillId=" + this.subSkillDeleteId).subscribe(result => {
        if (result.status == 200) {
          this.toastrService.error("Sub Skill status changed successfully!", "Change");
          this._modalService.dismissAll();
          this.getSubSkillList();
        }
        else {
          this.toastrService.warning("Something went wrong please try again", "Error");
          this._modalService.dismissAll();
        }
      })
    }
  }
  public getSubSkillList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.TrdesMan.GetSubSkillList).subscribe(result => {
      this.subSkillList = result.json();
      console.log(this.subSkillList);
      this.Loader.hide();
    })
  }
  public getSkillList() {
    this.service.get(this.service.apiRoutes.TrdesMan.GetTradesManSkills).subscribe(result => {
      this.skillList = result.json();
      console.log(this.skillList);
    })
  }

  // Tradesmna drop down setting

  onItemSelectAll(items: any) {
    console.log(items);
    this.SelectedSkillsList = items;
  }
  OnItemDeSelectALL(items: any) {
    this.SelectedSkillsList = [];
    console.log(items);
  }
  onItemSelect(item: any) {
    this.SelectedSkillsList = [];
    this.SelectedSkillsList.push(item);
    console.log(this.SelectedSkillsList);
  }
  OnItemDeSelect(item: any) {
    this.SelectedSkillsList = this.SelectedSkillsList.filter(
      function (value, index, arr) {
        //console.log(value);
        return value.id != item.id;
      }
    );
  }


}
