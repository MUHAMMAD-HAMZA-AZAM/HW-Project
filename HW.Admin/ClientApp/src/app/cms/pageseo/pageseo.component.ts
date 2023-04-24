import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { httpStatus } from '../../Shared/Enums/enums';
import { CommonService } from '../../Shared/HttpClient/_http';
import { ResponseVm } from '../../Shared/Models/HomeModel/HomeModel';

@Component({
  selector: 'app-pageseo',
  templateUrl: './pageseo.component.html',
  styleUrls: ['./pageseo.component.css']
})
export class PageseoComponent implements OnInit {
  public pageNameList = [];
  public pagesList = [];
  jwtHelperService: JwtHelperService = new JwtHelperService();
  public pageSeoForm: FormGroup;
  public formAction = "add";
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  public response = new ResponseVm;
  public sitePagesList = [];

  constructor(public router: Router, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) {
    this.pageNameList = ["Skill", "about", "blog", "tradesman", "supplier", "customer"];
    this.pageNameList.sort();
  }


  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Add Page SEO"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
     this.getPagesList();
    this.addNewSeoPageForm();
  }
  getSitePagesList(projectId: number) {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.CMS.GetSitePagesListByPageId + "?ProjectId=" + projectId).subscribe(result => {
      this.response = result.json();
      if (this.response.status == httpStatus.Ok) {
        this.sitePagesList = this.response.resultData;
        this.Loader.hide();
      }
    })
  }
  getPagesList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.CMS.GetPagesList).subscribe(response => {
      this.pagesList = response.json();
      this.Loader.hide();
    })
  }
  updatePage(formValue) {
   this.sitePagesList = [];
    console.log(formValue);
    this.formAction = "edit";
    this.getSitePagesList(formValue.projectId)
    this.pageSeoForm.patchValue(formValue);
  }
  public handleSubmit() {
    if (this.pageSeoForm.valid) {
      var decodedToken = this.jwtHelperService.decodeToken(localStorage.getItem('auth_token'));
      let formValues = this.pageSeoForm.value;
      if (this.formAction == "add") {
        formValues.formAction = "add";
        formValues.createdBy = decodedToken.UserId;
        console.log(formValues);
      }
      else if (this.formAction == "edit") {
        formValues.formAction = "edit";
        formValues.modifiedBy = decodedToken.UserId;
        console.log(formValues);
      }
      this.service.post(this.service.apiRoutes.CMS.CreateUpdatePageSeo, formValues).subscribe(response => {
        let res = (response.json());
        if (res.status == 200) {
          if (res.resultData) {
            this.toastr.error(res.message, "Opps");
          }
          else {
            this.toastr.success(res.message, "Success");
           this.getPagesList();
            this.resetForm();
          }
        }
        else {
          this.toastr.error("Something went wrong!", "Error");
        }
      })
    }
  }
  public addNewSeoPageForm() {
    this.pageSeoForm = this.fb.group({
      pageId: [0],
      sitePageId: [null, [Validators.required]],
     projectId: [null, [Validators.required]],
      pageTitle: ['', [Validators.required, Validators.maxLength(200)]],
      description: ['', [Validators.required]],
      keywords: ['', [Validators.required, Validators.maxLength(1000)]],
      ogDescription: ['', [Validators.required]],
      ogTitle: ['', [Validators.required, Validators.maxLength(1000)]],
      canonical: [''],
    })
  }
  resetForm() {
    this.pageSeoForm.reset();
    this.pageSeoForm.controls['pageId'].setValue(0);
  }
  get f() {
    return this.pageSeoForm.controls;
  }

}
