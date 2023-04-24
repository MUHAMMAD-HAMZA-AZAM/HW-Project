import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { ToastrService } from 'ngx-toastr';
import { JwtHelperService } from '@auth0/angular-jwt/src/jwthelper.service';
import { CommonService } from '../../Shared/HttpClient/_http';
import { NgxSpinnerService } from "ngx-spinner";
import { Router } from '@angular/router';


@Component({
  selector: 'app-agreements',
  templateUrl: './agreements.component.html',
  styleUrls: ['./agreements.component.css']
})
export class AgreementsComponent implements OnInit {
  faqForm: FormGroup;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  public agreementList = [];
  public updateDataFaq = {};
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
    public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    

  constructor(public router:Router ,public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService, ) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Agreements"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.getFaqList();
    this.faqForm = this.fb.group({
      AgreementId: [0],
      Header: ["", [Validators.required]],
      AgreementsText: [""],
      UserType: [null],
      OrderByColumn: [""],
    })
  }
  handleSubmit() {
    this.Loader.show();
    let faqObj;
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    if (this.faqForm.value.AgreementId > 0) {
      faqObj = this.updateDataFaq;
      faqObj.ModifiedBy = decodedtoken.UserId;
      faqObj.Header = this.faqForm.value.Header;
      faqObj.UserType = this.faqForm.value.UserType;
      faqObj.OrderByColumn = this.faqForm.value.OrderByColumn;
      faqObj.AgreementsText = this.faqForm.value.AgreementsText;
      console.log(faqObj);
    }
    else {
      faqObj = {
        Header: this.faqForm.value.Header,
        AgreementsText: this.faqForm.value.AgreementsText,
        UserType: Number(this.faqForm.value.UserType),
        OrderByColumn: Number(this.faqForm.value.OrderByColumn),
        CreatedBy: decodedtoken.UserId,
        ModifiedBy: "",
        IsActive: true,
      }
      console.log(faqObj);
    }
    if (this.faqForm.valid) {
      this.service.PostData(this.service.apiRoutes.UserManagement.InsertAndUpDateAgreement, faqObj, true).then(result => {
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
      AgreementId: faq.agreementId,
      Header: faq.header,
      AgreementsText: faq.agreementsText,
      UserType: faq.userType,
      OrderByColumn: faq.orderByColumn,
      CreatedBy: faq.createdBy,
      CreatedOn: faq.createdOn,
      ModifiedOn: faq.modifiedOn,
      IsActive: faq.isActive,
    }
    this.updateDataFaq = obj;
    this.faqForm.patchValue((obj));
  }
  deleteAgreement(faq) {
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    let obj = {
      AgreementId: faq.agreementId,
      Header: faq.header,
      AgreementsText: faq.agreementsText,
      UserType: faq.userType,
      OrderByColumn: faq.orderByColumn,
      CreatedBy: faq.createdBy,
      CreatedOn: faq.createdOn,
      ModifiedOn: faq.modifiedOn,
      ModifiedBy: decodedtoken.UserId,
      IsActive: !faq.isActive,
    }
    this.service.PostData(this.service.apiRoutes.UserManagement.DeleteAgreement, obj, true).then(result => {
      var res = result.json();
      console.log(res);
      if (res.message == "Updated") {
        this.toastr.error("Status changed successfully", "Warning");
        this.getFaqList();
      }
    })
  }
  resetAgreementForm() {
    this.faqForm.reset();
  }
  getFaqList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.UserManagement.GetAgreementsList).subscribe(result => {
      var response = result.json();
      this.agreementList = response;
      console.log(response);
      this.Loader.hide();
    })
  }
  get f() {
    return this.faqForm.controls;
  }

}
