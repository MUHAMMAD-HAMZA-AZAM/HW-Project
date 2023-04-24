import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '../../Shared/HttpClient/_http';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";
import { JwtHelperService } from '@auth0/angular-jwt';
import { PromotionTypeClass, PackagesFiltrClass } from '../../Shared/Models/PackagesAndPayments/PackagesModel';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';

@Component({
  selector: 'app-promotion-type',
  templateUrl: './promotion-type.component.html',
  styleUrls: ['./promotion-type.component.css']
})
export class PromotionTypeComponent implements OnInit {
  public pkgForm: FormGroup;

  public responcelist = [];
  public packagesList = [];
  public tempList = [];
  public pageNumber: number = 1;
  public pageSize: number = 50;
  public dataOrderBy = "DESC";
  public noOfPages;
  public pacclass: PromotionTypeClass = new PromotionTypeClass();
  public pacclassdata: PromotionTypeClass = new PromotionTypeClass();
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
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    

  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(public router: Router,public fb: FormBuilder, public toastr: ToastrService, public _modalService: NgbModal, public service: CommonService, public Loader: NgxSpinnerService, ) {

  }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Promotions Type"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.getPromotionTypeList();
    this.pkgForm = this.fb.group({
      PromotionTypeId: [0],
      PromotionTypeName: ['', [Validators.required]],
      PromotionTypeCode: ['', [Validators.required]],
      promotionOn: [0, [Validators.required]],
      entityStatus: [0, [Validators.required]],
    })

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
    this.pacclass = new PromotionTypeClass();

  }

  save() {
    
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    var data1 = this.pkgForm.value;
    if (this.pacclass.Status != 'Delete') {
      if (!this.pkgForm.valid) {
        this.submited = true;
        return;
      }
      this.pacclass.PromotionTypeCode = data1.PromotionTypeCode;
      this.pacclass.CreatedBy = decodedtoken.UserId;
      this.pacclass.UpdatedBy = decodedtoken.UserId;
      this.pacclass.entityStatus = data1.entityStatus;
      this.pacclass.PromotionTypeName = data1.PromotionTypeName;
      this.pacclass.promotionOn = data1.promotionOn;

      this.pacclass.PromotionTypeId = data1.PromotionTypeId;

      if (this.pacclass.PromotionTypeId == 0 || this.pacclass.PromotionTypeId < 1) {
        this.pacclass.Status = "Saved";
        this.pacclass.IsActive = true;

      }
    }
    else {
      this.pacclass.CreatedBy = decodedtoken.UserId;
      this.pacclass.UpdatedBy = decodedtoken.UserId;
    }
    if (this.pacclass.PromotionTypeId == null)
      this.pacclass.PromotionTypeId = 0;
    

    this.service.post(this.service.apiRoutes.PackagesAndPayments.AddNewPromotionTypes, this.pacclass).subscribe(result => {
      var res = result.json();

      if (res.message == 'AlreadyExists') {
        this.toastr.error("Promotion Name Already Exist", "Error");
      }
      else if (res.message == 'CodeAlreadyExists') {
        this.toastr.error("Promotion Code Already Exist", "Error");
      }
      else if (res.message == 'Saved') {
        this.toastr.success("Data added Successfully", "Success");
        this.submited = false;
        this.getPromotionTypeList();
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
    
    this.service.get(this.service.apiRoutes.PackagesAndPayments.GetAllPromotiontype).subscribe(result => {
      

     
        this.packagesList = result.json();
       

    })
  }
  updatePackages(packagedata, content) {
    
    this.pacclass.PromotionTypeId = packagedata.promotionTypeId
    this.pacclass.PromotionTypeCode = packagedata.promotionTypeCode
    this.pacclass.PromotionTypeName = packagedata.promotionTypeName  
    this.pacclass.promotionOn = packagedata.promotionOn  
    this.pacclass.IsActive = packagedata.isActive
    this.pacclass.entityStatus = packagedata.entityStatus
    this.pacclass.Status = "Update";
    this.pkgForm.patchValue(this.pacclass);
    this._modalService.open(content);
  }

  deletepackage(packagedata, deleteContent) {
    
    this.pacclass.PromotionTypeId = packagedata.promotionTypeId
    this.pacclass.PromotionTypeCode = packagedata.promotionTypeCode
    this.pacclass.PromotionTypeName = packagedata.promotionTypeName
    this.pacclass.promotionOn = packagedata.promotionOn
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
