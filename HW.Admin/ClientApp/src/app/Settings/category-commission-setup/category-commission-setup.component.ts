import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../Shared/HttpClient/_http';
import { NgxSpinnerService } from "ngx-spinner";
import { DatePipe } from '@angular/common';
import { Router } from '@angular/router';
import { httpStatus } from '../../Shared/Enums/enums';

@Component({
  selector: 'app-category-commission-setup',
  templateUrl: './category-commission-setup.component.html',
  styleUrls: ['./category-commission-setup.component.css']
})
export class CategoryCommissionSetupComponent implements OnInit {
  public appValForm: FormGroup;
  public CategoryCommissionSetupList = [];
  public CategoryList = [];
  public modelText: string = "";
  public submited: boolean = false;
  public disabledfields;
  public pipe;
  public date: Date;
  public day: string;
  public month: string;
  public year: string;
  public maxdate1: string;
  public disableSelectedCategory: boolean = false;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };

  constructor(public router: Router, public _modalService: NgbModal, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Category Commission Setup"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
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
    this.voucherCategorForm();
    this.getCategoryCommissionSetupList();
    this.getCategoryList();
  }
  public voucherCategorForm() {
    this.appValForm = this.fb.group({
      categoryCommissionSetupId: [0],
      categoryId: ['', [Validators.required]],
      commisionAmount: [0],
      commissionPercentage: [0, [Validators.required]],
      entityStatus: ['', [Validators.required]],
      commissionStartDate: ['', [Validators.required]],
      commissionEndDate: ['', [Validators.required]],
    });
  }
  get f() {
    return this.appValForm.controls;
  }
  getCategoryCommissionSetupList() {
    
    this.Loader.show()
    this.service.get(this.service.apiRoutes.PackagesAndPayments.getCategoryCommissionSetupList).subscribe(result => {
     
      this.CategoryCommissionSetupList = result.json();
      console.log(this.CategoryCommissionSetupList);
      this.Loader.hide();
    })
  }
  getCategoryList() {
    this.Loader.show()
    this.service.get(this.service.apiRoutes.TrdesMan.GetTradesManSkills).subscribe(result => {
      
      this.CategoryList = result.json();
      console.log(this.CategoryList);
      this.Loader.hide();
    });
  }

  saveAndUpdate() {
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    let formData;
    if (this.appValForm.invalid) {
      this.submited = true;
      return;
    }
    if (this.appValForm.value.categoryCommissionSetupId <= 0) {
      formData = this.appValForm.value;
      if (this.CategoryCommissionSetupList) {
        for (var i = 0; i < this.CategoryCommissionSetupList.length; i++) {
          if (formData.categoryId == this.CategoryCommissionSetupList[i].categoryId) {
            this.toastr.error("Commission Aleready Implemented On Selected Category ", "Alert !!");
            return;
          }
        }
      }
      formData.createdBy = decodedtoken.UserId;
      formData.categoryCommissionSetupId = 0;
      formData.commisionAmount = formData.commisionAmount != null ? parseInt(formData.commisionAmount) : 0;
      formData.commissionPercentage = formData.commissionPercentage != null ? parseInt(formData.commissionPercentage) : 0;
      this.Loader.show();
      this.service.post(this.service.apiRoutes.PackagesAndPayments.AddUpdateCategoryCommissionSetup, formData).subscribe(result => {
        var res = result.json();
        if (res.status == httpStatus.Ok) {
          this.appValForm.reset()
          this.toastr.success(res.message, "Success");
          this.getCategoryCommissionSetupList();
          this._modalService.dismissAll()
          this.submited = false;
        }
        else {
          this.toastr.error(res.message, "Error");
        }
        this.Loader.hide();
      })
    }
    // update
    else {
      formData = this.appValForm.value;
      formData.modifiedBy = decodedtoken.UserId;
      formData.commisionAmount = formData.commisionAmount != null ? parseInt(formData.commisionAmount) : 0;
      formData.commissionPercentage = formData.commissionPercentage != null ? parseInt(formData.commissionPercentage) : 0;
      console.log(formData);
      this.Loader.show();
      this.service.post(this.service.apiRoutes.PackagesAndPayments.AddUpdateCategoryCommissionSetup, formData).subscribe(result => {
        var res = result.json();

        if (res.status == httpStatus.Ok) {
          this.appValForm.reset()
          this.toastr.success(res.message, "Success");
          this.getCategoryCommissionSetupList();
          this._modalService.dismissAll()
          this.submited = false;
        }
        else {
          this.toastr.error(res.message, "Error");
        }
        this.Loader.hide();
      })

    }
  }
  updateCategoryCommissionSetup(categoryCommission, content) {
    //categoryCommission.active == true ? categoryCommission.active = "1" : categoryCommission.active = "0";
    var datePipe = new DatePipe("en-US");
    let commissionStartDate = datePipe.transform(categoryCommission.commissionStartDate, 'yyyy-MM-dd');
    let commissionEndDate = datePipe.transform(categoryCommission.commissionEndDate, 'yyyy-MM-dd');
    categoryCommission.commissionStartDate = commissionStartDate;
    categoryCommission.commissionEndDate = commissionEndDate;
    this.appValForm.patchValue(categoryCommission);
    this.disableSelectedCategory = true;
    this.modelText = "Update Category Commission Setup";
    this._modalService.open(content)
  }
  deleteCategoryCommissionSetup(categoryCommissionId) {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.PackagesAndPayments.deleteCategoryCommissionSetup + "?categoryCommissionId=" + categoryCommissionId).subscribe(result => {
      var res = result.json();
      if (res.status == httpStatus.Ok) {
        this.appValForm.reset()
        this.toastr.success(res.message, "Success");
        this.getCategoryCommissionSetupList();
      }
      else {
        this.toastr.error(res.message, "Error");
      }
      this.Loader.hide();
    })
  }

  showModal(content) {
    this.modelText = "Add New Category Commission Setup"
    this.disableSelectedCategory = false;
    this.appValForm.reset();
    this._modalService.open(content)
  }
  resetForm(): void {
    this.appValForm.reset();
  }

}
