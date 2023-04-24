import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '../../Shared/HttpClient/_http';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";
import { JwtHelperService } from '@auth0/angular-jwt';
import { CouponTypeClass, PackagesFiltrClass } from '../../Shared/Models/PackagesAndPayments/PackagesModel';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-coupon-type',
  templateUrl: './coupon-type.component.html',
  styleUrls: ['./coupon-type.component.css']
})
export class CouponTypeComponent implements OnInit {
  public pkgForm: FormGroup;

  public responcelist = [];
  public packagesList = [];
  public tempList = [];
  public pageNumber: number = 1;
  public pageSize: number = 50;
  public dataOrderBy = "DESC";
  public noOfPages;
  public pacclass: CouponTypeClass = new CouponTypeClass();
  public pacclassdata: CouponTypeClass = new CouponTypeClass();
  public filterdata: PackagesFiltrClass = new PackagesFiltrClass();
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
  public submited: boolean = false;

  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(public fb: FormBuilder, public toastr: ToastrService, public _modalService: NgbModal, public service: CommonService, public Loader: NgxSpinnerService, ) {

  }

  ngOnInit() {
    this.getPromotionTypeList();
    this.pkgForm = this.fb.group({
      CouponTypeId: [0],
      CouponTypeName: ['', [Validators.required]],
      CouponTypeCode: ['', [Validators.required]],
      entityStatus: [0, [Validators.required]],
    })
    this.submited = false;
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
    this.pacclass = new CouponTypeClass();

  }

  save() {
    
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
  
    var data1 = this.pkgForm.value;
    if (this.pacclass.Status != 'Delete') {
      if (this.pkgForm.invalid) {
        this.submited = true;
        return;
      }
      this.pacclass.CouponTypeCode = data1.CouponTypeCode;
      this.pacclass.CreatedBy = decodedtoken.UserId;
      this.pacclass.UpdatedBy = decodedtoken.UserId;
      this.pacclass.CouponTypeName = data1.CouponTypeName;
      this.pacclass.entityStatus = data1.entityStatus;
      this.pacclass.CouponTypeId = data1.CouponTypeId;

      if (this.pacclass.CouponTypeId == 0 || this.pacclass.CouponTypeId < 1) {
        this.pacclass.Status = "Saved";
        this.pacclass.IsActive = true;

      }
    }
    else {
      this.pacclass.CreatedBy = decodedtoken.UserId;
      this.pacclass.UpdatedBy = decodedtoken.UserId;
    }
    if (this.pacclass.CouponTypeId == null)
      this.pacclass.CouponTypeId = 0;
    

    this.service.post(this.service.apiRoutes.PackagesAndPayments.AddNewCouponTypes, this.pacclass).subscribe(result => {
      var res = result.json();

      if (res.message == 'AlreadyExists') {
        this.toastr.error("Coupon Name Already Exist", "Error");
      }
      else if (res.message == 'CodeAlreadyExists') {
        this.toastr.error("Coupon Code Already Exist", "Error");
      }
      else if (res.message == 'Saved') {
        this.toastr.success("Data added Successfully", "Success");

        this.getPromotionTypeList();
        this.resetform();
        this._modalService.dismissAll();
        this.submited = false;
      }
      else if (res.message == 'UpDated') {
        if (this.pacclass.Status == "Update") {
          this.toastr.success("Data Updated Successfully", "Success");
          this.resetform();
          this._modalService.dismissAll();
          this.submited = false;
        }
        else {
          this.toastr.success("Change status Successfully", "Success");
          this._modalService.dismissAll();
          this.resetform();
          this.submited = false;
        }

        this.getPromotionTypeList();
      }
    })
  }
  public showModal(content) {

    this._modalService.open(content);
    this.resetform();
  }
  public getPromotionTypeList() {

    this.service.get(this.service.apiRoutes.PackagesAndPayments.getAllCoupontype).subscribe(result => {
      


      this.packagesList = result.json();


    })
  }
  updatePackages(packagedata, content) {
    
    this.pacclass.CouponTypeId = packagedata.couponTypeId
    this.pacclass.CouponTypeCode = packagedata.couponTypeCode
    this.pacclass.CouponTypeName = packagedata.couponTypeName
    this.pacclass.IsActive = packagedata.isActive
    this.pacclass.entityStatus = packagedata.entityStatus
    this.pacclass.Status = "Update";
    this.pkgForm.patchValue(this.pacclass);
    this._modalService.open(content);
  }

  deletepackage(packagedata, deleteContent) {
    
    this.pacclass.CouponTypeId = packagedata.couponTypeId
    this.pacclass.CouponTypeCode = packagedata.couponTypeCode
    this.pacclass.CouponTypeName = packagedata.couponTypeName
    this.pacclass.entityStatus = packagedata.entityStatus

    if (packagedata.isActive == true)
      this.pacclass.IsActive = false;
    else
      this.pacclass.IsActive = true;
    this.pacclass.Status = "Delete";
    this._modalService.open(deleteContent);
  }

  confirmDeletePackage() {

    this.save();

  }
  clickchange() {
    this.getPromotionTypeList();
  }
  PageSizeChange() {

    this.pageNumber = 1;
    this.getPromotionTypeList();
  }
  PriviousClick() {
    if (this.pageNumber > 1) {
      this.pageNumber = parseInt(this.pageNumber.toString()) - 1;
      this.getPromotionTypeList();
    }

  }
  NextClick() {
    if (this.pageNumber < this.noOfPages) {
      this.pageNumber = parseInt(this.pageNumber.toString()) + 1;
      this.getPromotionTypeList();
    }

  }
  LoadNewestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "DESC";
    this.getPromotionTypeList();

  }
  LoadOldestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "ASC";
    this.getPromotionTypeList();

  }
  NumberOfPages() {
    this.totalPages = Math.ceil(this.totalJobs / this.pageSize);
  }
  changePageSize() {
    this.pageNumber = 1;
    this.NumberOfPages();
    this.getPromotionTypeList();
  }
}
