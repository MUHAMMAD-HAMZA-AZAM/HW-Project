import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '../../Shared/HttpClient/_http';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";
import { JwtHelperService } from '@auth0/angular-jwt';
import { PromotionTypeClass, PackagesFiltrClass, PackageTypeClass } from '../../Shared/Models/PackagesAndPayments/PackagesModel';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-packagetype',
  templateUrl: './packagetype.component.html',
  styleUrls: ['./packagetype.component.css']
})
export class PackagetypeComponent implements OnInit {
  public pkgForm: FormGroup;

  public responcelist = [];
  public packagesList = [];
  public tempList = [];
  public pageNumber: number = 1;
  public pageSize: number = 50;
  public dataOrderBy = "DESC";
  public noOfPages;
  public pacclass: PackageTypeClass = new PackageTypeClass();
  public pacclassdata: PackageTypeClass = new PackageTypeClass();
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
  public sprights = 'false';
  public submited: boolean=false;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(public fb: FormBuilder, public toastr: ToastrService, public _modalService: NgbModal, public service: CommonService, public Loader: NgxSpinnerService, ) {

  }

  ngOnInit() {
    this.getPromotionTypeList();
    this.pkgForm = this.fb.group({
      PackageTypeId: [0],
      PackageTypeName: ['', [Validators.required]],
      PackageTypeCode: ['', [Validators.required]],
      entityStatus: [0, [Validators.required]],
      userRoleId: [0, [Validators.required]],
      IsActive: [false,],
      
    })
    this.sprights = localStorage.getItem("SpectialRights");
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
    this.pacclass = new PackageTypeClass();

  }

  save() {
    
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    
    var data1 = this.pkgForm.value;
    if (this.pacclass.sta != 'Delete') {
      if (!this.pkgForm.valid) {
        this.submited = true;
        return;
      }
      this.pacclass.PackageTypeCode = data1.PackageTypeCode;
      this.pacclass.CreatedBy = decodedtoken.UserId;
      this.pacclass.ModifiedBy = decodedtoken.UserId;
    this.pacclass.PackageTypeName = data1.PackageTypeName;
      this.pacclass.entityStatus = data1.entityStatus;
      this.pacclass.userRoleId = data1.userRoleId;
      
      this.pacclass.IsActive = true;
      this.pacclass.PackageTypeId = data1.PackageTypeId;

      if (this.pacclass.PackageTypeId == 0 || this.pacclass.PackageTypeId < 1) {
        this.pacclass.IsActive = data1.IsActive;
      }
    }
    else {
      this.pacclass.CreatedBy = decodedtoken.UserId;
      this.pacclass.ModifiedBy = decodedtoken.UserId;

    }
    if (this.pacclass.PackageTypeId == null)
      this.pacclass.PackageTypeId = 0;
    

    this.service.post(this.service.apiRoutes.PackagesAndPayments.AddUpdatePackageType, this.pacclass).subscribe(result => {
      var res = result.json();

      if (res.message == 'AlreadyExists') {
        this.toastr.error("Promotion Name Already Exist", "Error");
      }
      else if (res.message == 'CodeAlreadyExists') {
        this.toastr.error("Promotion Code Already Exist", "Error");
      }
      else if (res.message == 'Saved') {
        this.toastr.success("Data added Successfully", "Success");

        this.getPromotionTypeList();
        this.resetform();
        this._modalService.dismissAll();
        this.submited = false;
      }
      else if (res.message == 'UpDated' ) {
        // this.productName = "";
        if (this.pacclass.sta == "Update") {
          this.toastr.success("Data Updated Successfully", "Success");
          this.resetform();
          this._modalService.dismissAll();
          this.submited = false;
        }
        if (this.pacclass.sta == 'Delete') {
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
    this.pkgForm.controls.IsActive.setValue(true);
  }
  public getPromotionTypeList() {
    this.service.get(this.service.apiRoutes.PackagesAndPayments.GetAllPackagetype).subscribe(result => {
      
      this.packagesList = result.json();
    })
  }
  updatePackages(packagedata, content) {
    
    this.pacclass.PackageTypeId = packagedata.packageTypeId
    this.pacclass.PackageTypeCode = packagedata.packageTypeCode
    this.pacclass.PackageTypeName = packagedata.packageTypeName
    this.pacclass.userRoleId = packagedata.userRoleId
    
    this.pacclass.IsActive = packagedata.isActive
    this.pacclass.sta = "Update";
    this.pacclass.entityStatus = packagedata.entityStatus;
    this.pkgForm.patchValue(this.pacclass);
    this._modalService.open(content);
  }

  deletepackage(packagedata, deleteContent) {
    
    this.pacclass.PackageTypeId = packagedata.packageTypeId
    this.pacclass.PackageTypeCode = packagedata.packageTypeCode
    this.pacclass.PackageTypeName = packagedata.packageTypeName
    this.pacclass.entityStatus = packagedata.entityStatus
    this.pacclass.userRoleId = packagedata.userRoleId
    if (packagedata.isActive == true)
      this.pacclass.IsActive = false;
    else
      this.pacclass.IsActive = true;
    this.pacclass.sta = "Delete";
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
