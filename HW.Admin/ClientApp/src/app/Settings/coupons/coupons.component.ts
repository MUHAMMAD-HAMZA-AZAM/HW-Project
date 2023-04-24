import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '../../Shared/HttpClient/_http';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";
import { JwtHelperService } from '@auth0/angular-jwt';
import { CouponClass, CouponTypeDDClass, PackagesFiltrClass } from '../../Shared/Models/PackagesAndPayments/PackagesModel';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-coupons',
  templateUrl: './coupons.component.html',
  styleUrls: ['./coupons.component.css']
})
export class CouponsComponent implements OnInit {

  public pkgForm: FormGroup;

  public packagesList = [];
  public pageNumber: number = 1;
  public pageSize: number = 50;
  public dataOrderBy = "DESC";
  public noOfPages;
  public pacclass: CouponClass = new CouponClass();
  public pacclassdata: CouponClass = new CouponClass();
  public filterdata: CouponClass = new CouponClass();
  public CoouponTypedd: CouponTypeDDClass = new CouponTypeDDClass();
  
  public data = [];
  public totalRecoards = 101;
  public pageing1 = [];
  public recoardNoFrom = 0;
  public recoardNoTo = 50;
  public totalJobs: number;
  public totalPages: number;
  public selectedPage: number;
  public PackageNamefilter: string;
  public PackageCodefilter: string;
  public UserRoleIdfilter: number;
  public CouponNamefilter: string;
  public CouponCodefilter: string;
  public CouponTypeIdfilter: number;
  public submited: boolean = false;
  public Entity: string;

  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(public fb: FormBuilder, public toastr: ToastrService, public _modalService: NgbModal, public service: CommonService, public Loader: NgxSpinnerService, ) {

  }

  ngOnInit() {
    this.getCouponList();
    this.GetCouponTypes();
    this.pkgForm = this.fb.group({
      CouponId: [0],
      CouponeName: ['', [Validators.required]],
      CouponCode: ['', [Validators.required]],
      DiscountAmount: ['', [Validators.required]],
      DiscountDays: ['', [Validators.required]],
      DiscountCategories: ['', [Validators.required]],
      DiscountJobsApplied: ['', [Validators.required]],
      CouponTypeId: [0, [Validators.required]],
      entityStatus: [0, [Validators.required]],
      IsActive: [false],
      CreatedBy: "TEST",
      UpdatedBy: "Test:",

    })
    this.Entity = '1';
    this.submited = false;
    this.pageNumber = 1;
    this.pageSize = 50;

    this.dataOrderBy = "DESC";

  }
  saveAndUpdate() {
    alert(this.pkgForm.value.status);
  }
  resetForm() {
    this.pkgForm.reset();
  }
  get f() {
    return this.pkgForm.controls;
  }


  resetform() {
    this.pkgForm.reset();
    this.pacclass = new CouponClass();

  }

  save() {
    
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    var data1 = this.pkgForm.value;
    if (this.pacclass.Status != 'Delete') {
      if (this.pkgForm.invalid) {
        this.submited = true;
        return;
      }
      this.pacclass.CouponeName = data1.CouponeName;
      this.pacclass.CreatedBy = decodedtoken.UserId;
      this.pacclass.UpdatedBy = decodedtoken.UserId;
      this.pacclass.entityStatus = data1.entityStatus;
      this.pacclass.CouponCode = data1.CouponCode;
      this.pacclass.DiscountAmount = data1.DiscountAmount;
      this.pacclass.DiscountDays = data1.DiscountDays;
      this.pacclass.DiscountCategories = data1.DiscountCategories;
      this.pacclass.DiscountJobsApplied = data1.DiscountJobsApplied;
      this.pacclass.CouponTypeId = data1.CouponTypeId;

      this.pacclass.CouponId = data1.CouponId;

      if (this.pacclass.CouponId == 0 || this.pacclass.CouponId < 1) {
        this.pacclass.Status = "Saved";
        this.pacclass.IsActive = true;

      }
    }
    else {
      this.pacclass.CreatedBy = decodedtoken.UserId;
      this.pacclass.UpdatedBy = decodedtoken.UserId;
    }
    if (this.pacclass.CouponId == null)
      this.pacclass.CouponId = 0;
    

    this.service.post(this.service.apiRoutes.PackagesAndPayments.AddNewCoupons, this.pacclass).subscribe(result => {
      var res = result.json();

      if (res.message == 'AlreadyExists') {
        this.toastr.error("Coupon Name Already Exist", "Error");
      }
      else if (res.message == 'CodeAlreadyExists') {
        this.toastr.error("Coupon Code Already Exist", "Error");
      }
      else if (res.message == 'Saved') {
        this.toastr.success("Data added Successfully", "Success");
        this.submited = false;
        this.getCouponList();
        this.resetform();
        this._modalService.dismissAll();
      }
      else if (res.message == 'UpDated') {
        if (this.pacclass.Status == "Update") {
          this.toastr.success("Data Updated Successfully", "Success");
          this.resetform();
          this._modalService.dismissAll();
          this.submited = false;
        }
        else {
          this.toastr.success("Delete Successfully", "Success");
          this._modalService.dismissAll();
          this.resetform();
          this.submited = false;
        }

        this.getCouponList();
      }
    })
  } 
  public showModal(content) {

    this._modalService.open(content);
    this.resetform();
  }
  public getCouponList() {
    
    let obj;

    this.filterdata.CouponeName = this.CouponNamefilter;
    this.filterdata.CouponCode = this.CouponCodefilter;
    this.filterdata.CouponTypeId = this.CouponTypeIdfilter;
    this.filterdata.entityStatus = this.Entity;
    this.filterdata.PageNumber = this.pageNumber;
    this.filterdata.PageSize = this.pageSize;
    this.filterdata.OrderByColumn = this.dataOrderBy;

    
    this.service.post(this.service.apiRoutes.PackagesAndPayments.GetAllCoupons, this.filterdata).subscribe(result => {
      

      if (result.json() != null) {
        this.packagesList = result.json();
        this.totalRecoards = this.packagesList[0].noOfRecoards;
        this.noOfPages = this.packagesList[0].noOfRecoards / this.pageSize;
        this.noOfPages = Math.floor(this.noOfPages);
        if (this.packagesList[0].noOfRecoards > this.noOfPages) {
          this.noOfPages = this.noOfPages + 1;
        }
        this.pageing1 = [];
        for (var x = 1; x <= this.noOfPages; x++) {
          this.pageing1.push(
            x
          );
        }
        this.recoardNoFrom = (this.pageSize * this.pageNumber) - this.pageSize + 1;
        this.recoardNoTo = (this.pageSize * this.pageNumber);
        if (this.recoardNoTo > this.packagesList[0].noOfRecoards)
          this.recoardNoTo = this.packagesList[0].noOfRecoards;
      }
      else {
        this.recoardNoFrom = 0;
        this.recoardNoTo = 0;
        this.noOfPages = 0;
        this.totalRecoards = 0;
        this.packagesList = [];
        this.toastr.error("No record found !", "Search")
      }
     
    })
  }
  updatePackages(packagedata, content) {
    
    this.pacclass.CouponId = packagedata.couponId
    this.pacclass.CouponCode = packagedata.couponCode
    this.pacclass.CouponeName = packagedata.couponeName
    this.pacclass.DiscountAmount = packagedata.discountAmount
    this.pacclass.DiscountDays = packagedata.discountDays
    this.pacclass.DiscountCategories = packagedata.discountCategories
    this.pacclass.DiscountJobsApplied = packagedata.discountJobsApplied
    this.pacclass.IsActive = packagedata.isActive
    this.pacclass.CouponTypeId = packagedata.couponTypeId
    this.pacclass.entityStatus = packagedata.entityStatus
  
    this.pacclass.Status = "Update";
    this.pkgForm.patchValue(this.pacclass);
    this._modalService.open(content);
  }

  deletepackage(packagedata, deleteContent) {
    
    this.pacclass.CouponId = packagedata.couponId
    this.pacclass.CouponCode = packagedata.couponCode
    this.pacclass.CouponeName = packagedata.couponeName
    this.pacclass.DiscountAmount = packagedata.discountAmount
    this.pacclass.DiscountDays = packagedata.discountDays
    this.pacclass.DiscountCategories = packagedata.discountCategories
    this.pacclass.DiscountJobsApplied = packagedata.discountJobsApplied
    this.pacclass.entityStatus = packagedata.entityStatus
    if (packagedata.isActive == true)
      this.pacclass.IsActive = false;
    else
      this.pacclass.IsActive = true;

    this.pacclass.CouponTypeId = packagedata.couponTypeId
  
    this.pacclass.Status = "Delete";
    this._modalService.open(deleteContent);
  }

  confirmDeletePackage() {

    this.save();

  }
  clickchange() {
    this.getCouponList();
  }
  PageSizeChange() {

    this.pageNumber = 1;
    this.getCouponList();
  }
  PriviousClick() {
    if (this.pageNumber > 1) {
      this.pageNumber = parseInt(this.pageNumber.toString()) - 1;
      this.getCouponList();
    }

  }
  NextClick() {
    if (this.pageNumber < this.noOfPages) {
      this.pageNumber = parseInt(this.pageNumber.toString()) + 1;
      this.getCouponList();
    }

  }
  LoadNewestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "DESC";
    this.getCouponList();

  }
  LoadOldestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "ASC";
    this.getCouponList();

  }
  NumberOfPages() {
    this.totalPages = Math.ceil(this.totalJobs / this.pageSize);
  }
  changePageSize() {
    this.pageNumber = 1;
    this.NumberOfPages();
    this.getCouponList();
  }
  public GetCouponTypes() {

    this.service.get(this.service.apiRoutes.PackagesAndPayments.getAllCoupontype).subscribe(result => {
      

      this.CoouponTypedd = result.json();
    });
  }
}
