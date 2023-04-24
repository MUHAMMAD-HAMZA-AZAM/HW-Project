import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { ToastrService } from 'ngx-toastr';
import { ImageCroppedEvent } from 'ngx-image-cropper';
import { CommonService } from '../../Shared/HttpClient/_http';
import { JwtHelperService } from '@auth0/angular-jwt/src/jwthelper.service';
import { NgxSpinnerService } from "ngx-spinner";
import { Router } from '@angular/router';

@Component({
  selector: 'app-faqs',
  templateUrl: './faqs.component.html',
  styleUrls: ['./faqs.component.css']
})
export class FAQsComponent implements OnInit {
  public FaqId = 5;
  public updateDataFaq = {};
  public faqList = [];
  faqForm: FormGroup;
  config: AngularEditorConfig = {
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
  jwtHelperService: JwtHelperService = new JwtHelperService();
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    
  constructor(public router: Router, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService, ) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("FAQs"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.getFaqList();
    this.faqForm = this.fb.group({
      FaqId :[0],
      Header: ["", [Validators.required]],
      FAQsText: [""],
      UserType: [null],
      OrderByColumn: [""],
    })
  }
  handleSubmit() {
    this.Loader.show();
    let faqObj;
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    if (this.faqForm.value.FaqId > 0) {
      faqObj = this.updateDataFaq;
      faqObj.ModifiedBy = decodedtoken.UserId;
      faqObj.Header = this.faqForm.value.Header;
      faqObj.UserType = this.faqForm.value.UserType;
      faqObj.OrderByColumn = this.faqForm.value.OrderByColumn;
      faqObj.FAQsText = this.faqForm.value.FAQsText;
      console.log(faqObj);
    }
    else {
      faqObj = {
        Header: this.faqForm.value.Header,
        FAQsText: this.faqForm.value.FAQsText,
        UserType: Number(this.faqForm.value.UserType),
        OrderByColumn: Number(this.faqForm.value.OrderByColumn),
        CreatedBy: decodedtoken.UserId,
        ModifiedBy: "",
        IsActive: true,
      }
    }
    if (this.faqForm.valid) {
      this.service.PostData(this.service.apiRoutes.UserManagement.InsertAndUpDateFAQs, faqObj, true).then(result => {
        var res = result.json();
        console.log(res);
        if (res.message == "Inserted") {
          this.toastr.success("Data Saved", "Success");
          this.getFaqList();
          this.faqForm.reset();
        }
        else if (res.message == "Updated") {
          this.toastr.success("Data updated", "Success");
          this.getFaqList();
          this.faqForm.reset();
        }
        else if (res.message == "AlreadyExists") {
          this.toastr.error("Already Exsit", "Success");
        }
        this.Loader.hide();
      })
    }
  }
  updateFaq(faq) {
    let obj = {
      FaqId: faq.faqId,
      Header: faq.header,
      FAQsText: faq.faQsText,
      UserType:faq.userType,
      OrderByColumn: faq.orderByColumn,
      CreatedBy: faq.createdBy,
      CreatedOn: faq.createdOn,
      ModifiedOn: faq.modifiedOn,
      IsActive: faq.isActive,
    }
    this.updateDataFaq = obj;
    this.faqForm.patchValue((obj));
  }
  deleteFaq(faq) {
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    let obj = {
      FaqId: faq.faqId,
      Header: faq.header,
      FAQsText: faq.faQsText,
      UserType: faq.userType,
      OrderByColumn: faq.orderByColumn,
      CreatedBy: faq.createdBy,
      CreatedOn: faq.createdOn,
      ModifiedOn: faq.modifiedOn,
      ModifiedBy: decodedtoken.UserId,
      IsActive: !faq.isActive,

    }
    console.log(obj);
    this.service.PostData(this.service.apiRoutes.UserManagement.DeleteFaq, obj, true).then(result => {
      var res = result.json();
      console.log(res);
      if (res.message == "Updated") {
        this.toastr.error("Status changed successfully", "Warning");
        this.getFaqList();
      }
    })
  }
  resetForm() {
    this.faqForm.reset();
  }
  getFaqList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.UserManagement.GetFAQsList).subscribe(result => {
      var response = result.json();
      this.faqList = response;
      console.log(response);
      this.Loader.hide();
    })
  }
  get f() {
    return this.faqForm.controls;
  }

}
