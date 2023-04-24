import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CommonService } from '../../Shared/HttpClient/_http';
import { JwtHelperService } from '@auth0/angular-jwt/src/jwthelper.service';
import { NgxSpinnerService } from "ngx-spinner";
import { ImageCroppedEvent } from 'ngx-image-cropper';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
@Component({
  selector: 'app-skill',
  templateUrl: './skill.component.html',
  styleUrls: ['./skill.component.css']
})
export class SkillComponent implements OnInit {
  jwtHelperService: JwtHelperService = new JwtHelperService();

  public skillList = [];
  public responseList = [];
  public skillListImage: any;
  public statesList: [] = [];
  public skillName: string = "";
  public orderBy: string = "";
  public skillNameExist: string = "";
  public orderByExist: string = "";
  public Name: string = "";
  public skillData: any;
  public confirmDelete: boolean = false;
  public skillDeleteId: any;
  public skillModelText = "Add New Skill";
  public shiftFrom;
  public shiftTo;
  public skillId = 0;
  public skillIcon: string = "";
  public seoPageTitle: string = "";
  public ogTitle: string = "";
  public ogDescription: string = "";

  //public items = [];
  public seoDescription = "";
  public skillDescription: any;
  public toggle = true;
  public skillTitleName = "";
  public imgPath = "";

  ////// Image //////
  imageChangedEvent: any = '';
  croppedImage: any = null;
  public fileChanged = false;
    public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    
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
  @ViewChild("inputFile") inputFile: ElementRef;
  constructor(
    public router: Router,public toastrService: ToastrService, public _modalService: NgbModal, public service: CommonService, public Loader: NgxSpinnerService,) {
  }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Skills"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    //this.getSkillList();
    //this.service.convertToSlug("12 3334 kafhsdksahdf !@#$%^&*()+}{|><?, jhk");
    this.populateSkills();
  }
  handleToggle() {
    this.toggle = !this.toggle;
    this.skillModelText = "Add New Skill";
    this.croppedImage = null;
    this.imgPath = null;
    this.inputFile.nativeElement.value = null;
  

  }
  public populateSkills() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.TrdesMan.GetSkillListAdmin).subscribe(result => {
      this.skillList = result.json();
      
      console.log(this.skillList);
      this.Loader.hide();
    },
      error => {
        console.log(error);
      });
  }

  ////// Image //////
  fileChangeEvent(event: any): void {
    var fReader = new FileReader();
    var isThumb = event.target.files[0];
    console.log(isThumb);
    this.imgPath = event.target.files[0].name;
    fReader.readAsDataURL(isThumb);
    fReader.onload = (event: any) => {
      this.croppedImage = event.target.result;
      this.fileChanged = true;
      console.log((this.croppedImage))
    };

  }
  public resetForm(f: NgForm) {
    f.resetForm();
    this.inputFile.nativeElement.value = null;
    this.croppedImage = null;
  }
  public onSelect(event) {

  }
  //save() {
  //  console.log(this.items);
  //  console.log(this.skillTitleName);
  //  console.log(this.skillDescription);
  //}
  save(f: NgForm) {
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    let orderBy = Number(this.orderBy);
    const ob = Number(this.orderBy)
    let orderObj = {
      OrderBy: ob,
    }
    if (this.skillData == null || this.skillData == "") {
      this.fileChanged = false;
      const ob = Number(this.orderBy)
      let obj = {
        CreatedBy: decodedtoken.UserId,
        SkillName: this.skillName,
        SkillTitle: this.skillTitleName,
        //MetaTags: this.items.toString(),
        MetaTags: this.seoDescription,
        Description: this.skillDescription,
        IsActive: false,
        OrderByColumn: ob,
        Base64Image: this.croppedImage,
        ImagePath: this.imgPath,
        Slug: this.service.convertToSlug(this.skillName),
        SkillIconPath: this.skillIcon,
        SeoPageTitle: this.seoPageTitle,
        OgTitle: this.ogTitle,
        OgDescription: this.ogDescription

      }
      console.log(obj);
      //add new skill ////
      this.Loader.show();
      this.service.post(this.service.apiRoutes.TrdesMan.AddNewSkill, obj).subscribe(result => {
        let response = result.json();
        if (response.status == 200) {
          this.croppedImage = null;
          f.reset();
          this.inputFile.nativeElement.value = null;
          this.toastrService.success("Data added Successfully", "Success");
          this.toggle = !this.toggle;
          this.populateSkills();
        }
        else if (response.status == 400) {
          this.toastrService.error(response.message, "Error");
        }
        this.Loader.hide();
      })
    }
    else {
      ///Update ///////
      if (!this.fileChanged) {
        this.imgPath = "";
      }
      
      let shiftFrom = Number(this.shiftFrom);
      let shiftTo = Number(this.orderBy);
      let cropImage;
      if (this.croppedImage != null && this.fileChanged) {
        this.fileChanged = !this.fileChanged;
        cropImage = this.croppedImage;
      }
      else {
        cropImage = this.skillData.skillImage;
        console.log(cropImage);
      }
      let obj = {
        ModifiedBy: decodedtoken.UserId,
        Name: this.skillName,
        SkillTitle: this.skillTitleName,
        MetaTags: this.seoDescription,
        Description: this.skillDescription,
        SkillId: this.skillData.skillId,
        OrderByColumn: ob,
        ShiftFrom: shiftFrom,
        ShiftTo: shiftTo,
        Base64Image: cropImage,
        ImagePath: this.imgPath,
        Slug: this.service.convertToSlug(this.skillTitleName),
        skillIconPath: this.skillIcon,
        SkillIconPath: this.skillIcon,
        SeoPageTitle: this.seoPageTitle,
        OgTitle: this.ogTitle,
        OgDescription: this.ogDescription

      }
      console.log(obj);
      this.Loader.show();
      this.service.post(this.service.apiRoutes.TrdesMan.UpdateSkill, obj).subscribe(result => {
        
        var res = result.json();
        if (res.status == 200) {
          this.skillName = "";
          this.toastrService.success("Data updated Successfully", "Success");
          f.reset();
          this.inputFile.nativeElement.value = null;
          this.toggle = !this.toggle;
          this.populateSkills();
          this.croppedImage = null;
        }
        else if (res.status == 400) {
          this.toastrService.error("Skill name already exsit", "Error");
        }
        this.Loader.hide();
      })
    }
  }
  showModal(content) {
    //this.skillModelText = "Add New Skill"
    this.skillName = "";
    this._modalService.open(content)
  }
  public getSkillList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.TrdesMan.GetSkillList).subscribe(result => {
      
      this.skillList = result.json();
      console.log(this.skillList);
      this.Loader.hide();
    })
  }
  updateSkill(skill) {
    this.toggle = !this.toggle;
    this.skillModelText = "Update Skill"
    if (skill != null && skill != "") {
      this.skillName = skill.skillName,
        this.orderBy = skill.orderByColumn;
      this.shiftFrom = skill.orderByColumn;
      this.skillData = skill;
      this.skillDescription = skill.description;
      this.skillTitleName = skill.skillTitle;
      this.seoDescription = skill.metaTags;
      this.seoPageTitle = skill.seoPageTitle;
      this.ogTitle = skill.ogTitle;
      this.ogDescription = skill.ogDescription;
      if (skill.skillImage) {
        this.croppedImage = this.service.transform(skill.skillImage);
        this.imgPath = skill.imagePath;
      }
      else {
        this.croppedImage = null;
      }
      this.skillIcon = skill.skillIconPath;
      //console.log(this.items);
      //this._modalService.open(content);
    }
  }
  deleteSkill(skillId, deleteContent) {
    this.skillDeleteId = Number(skillId);
    this._modalService.open(deleteContent);
    
  }
  confirmDeleteSkill() {
    if (this.skillDeleteId != null && this.skillDeleteId != "") {
      this.service.get(this.service.apiRoutes.TrdesMan.DeleteSkill + "?skillId=" + this.skillDeleteId).subscribe(result => {
        
        if (result.status == 200) {
          this.toastrService.success("Skill status changed successfully!", "Change");
          this._modalService.dismissAll();
          // this.getSkillList();
          this.populateSkills();

        }
        else {
          this.toastrService.warning("Something went wrong please try again", "Error");
          this._modalService.dismissAll();
        }
      })
    }
  }

}
