import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '../../Shared/HttpClient/_http';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";
import { JwtHelperService } from '@auth0/angular-jwt';
import { PackagesAndPromotion, PackagesFiltrClass, PackageDDClass, PromotionDDClass, PackagesClass, PromotionClass } from '../../Shared/Models/PackagesAndPayments/PackagesModel';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-promotion-on-packages',
  templateUrl: './promotion-on-packages.component.html',
  styleUrls: ['./promotion-on-packages.component.css']
})
export class PromotionOnPackagesComponent implements OnInit {

  public pkgForm: FormGroup;

  public responcelist = [];
  public packagesList = [];
  public promotionList = [];
  public tempList = [];
  public pageNumber: number = 1;
  public pageSize: number = 50;
  public dataOrderBy = "DESC";
  public noOfPages;

  public promotionddl: PromotionClass []
  public packagesfilter : PackagesFiltrClass = new PackagesFiltrClass();
  public pacclass: PackagesAndPromotion = new PackagesAndPromotion();
  public pacclassdata: PackagesAndPromotion = new PackagesAndPromotion();
  public filterdata: PackagesAndPromotion = new PackagesAndPromotion();
  public packagesdata: PackagesClass = new PackagesClass();
  public packageddl:  PackagesClass[] 
  public packagesset:  PackagesClass[] 
  public promotionset: PromotionClass[];
  public data = [];
  public totalRecoards = 101;
  public pageing1 = [];
  public recoardNoFrom = 0;
  public recoardNoTo = 50;
  public totalJobs: number;
  public totalPages: number;
  public selectedPage: number;
  public PackageIdFilter: number;
  public PromotionIdFilter: number;
  public UserRoleIdfilter: number;
  public Entity: string;
  public submited: boolean = false;

  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(public fb: FormBuilder, public toastr: ToastrService, public _modalService: NgbModal, public service: CommonService, public Loader: NgxSpinnerService, ) {

  }

  ngOnInit() {
   
    this.pkgForm = this.fb.group({
      promotionOnPackagesId: [0],
      packageId: [0, [Validators.required]],
      promotionId: [0, [Validators.required]],
      userRoleId: ['', [Validators.required]],
      originalSalePrice: ['', [Validators.required]],
      discountPercentPrice: ['', [Validators.required]],
      validityDays: ['', [Validators.required]],
      priceAfterDiscount: ['', [Validators.required]],
      discountDays: ['', [Validators.required]],
      discountedValidityDays: ['', [Validators.required]],
      totalApplicableJobs: ['', [Validators.required]],
      discountJobsApplied: ['', [Validators.required]],
      discountedTotalApplicableJobs: ['', [Validators.required]],
      totalCategories: ['', [Validators.required]],
      discountCategories: ['', [Validators.required]],
      discountedTotalCategories: ['', [Validators.required]],
      entityStatus: [0, [Validators.required]],
      isActive: [false],
      createdBy: "TEST",
      updatedBy: "Test:",

    })
    this.Entity = '1';
    this.BindPackages();
    this.bindPromotionsList();
    this.dataOrderBy = "DESC";
    this.PackageIdFilter = 0;
    this.PromotionIdFilter = 0;
    this.UserRoleIdfilter = 0;
    this.pageSize = 50;
    this.pageNumber = 1;
    this.getPromotionOnPackagesList();
    
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
    this.pacclass = new PackagesAndPromotion();
    this.promotionset = []
    this.packagesset = [] 

  }

  save() {
    
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    var data1 = this.pkgForm.value;
    if (this.pacclass.Status != 'Delete') {
      if (this.pkgForm.invalid) {

        this.submited = true;
        return;
      }

      this.pacclass.packageId = data1.packageId;
      this.pacclass.promotionId = data1.promotionId;
      this.pacclass.CreatedBy = decodedtoken.UserId;
      this.pacclass.UpdatedBy = decodedtoken.UserId;
      this.pacclass.originalSalePrice = data1.originalSalePrice;
      this.pacclass.userRoleId = data1.userRoleId;
      this.pacclass.discountPercentPrice = data1.discountPercentPrice;
      this.pacclass.priceAfterDiscount = data1.priceAfterDiscount;
      this.pacclass.validityDays = data1.validityDays;
      this.pacclass.discountDays = data1.discountDays;
      this.pacclass.discountedValidityDays = data1.discountedValidityDays;
      this.pacclass.totalApplicableJobs = data1.totalApplicableJobs;

      this.pacclass.discountJobsApplied = data1.discountJobsApplied;
      this.pacclass.discountedTotalApplicableJobs = data1.discountedTotalApplicableJobs;
      this.pacclass.totalCategories = data1.totalCategories;
      this.pacclass.discountCategories = data1.discountCategories;
      this.pacclass.discountedTotalCategories = data1.discountedTotalCategories;
      this.pacclass.entityStatus = data1.entityStatus;    
      
      this.pacclass.promotionOnPackagesId = data1.promotionOnPackagesId;

      if (this.pacclass.promotionOnPackagesId == 0 || this.pacclass.promotionOnPackagesId < 1) {
        this.pacclass.Status = "Saved";
        this.pacclass.isActive = true;

      }
    }
    else {
      this.pacclass.CreatedBy = decodedtoken.UserId;
      this.pacclass.UpdatedBy = decodedtoken.UserId;
    }
    if (this.pacclass.promotionOnPackagesId == null)
      this.pacclass.promotionOnPackagesId = 0;
    

    this.service.post(this.service.apiRoutes.PackagesAndPayments.AddPromotionOnPackages, this.pacclass).subscribe(result => {
      var res = result.json();

      if (res.message == 'AlreadyExists') {
        this.toastr.error("Package& Name Already Exist", "Error");
      }
      else if (res.message == 'CodeAlreadyExists') {
        this.toastr.error("Package Code Already Exist", "Error");
      }
      else if (res.message == 'Saved') {
        this.submited = false;
        this.toastr.success("Data added Successfully", "Success");

        this.getPromotionOnPackagesList();
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

        this.getPromotionOnPackagesList();
      }
    })
  }
  public showModal(content) {

    this._modalService.open(content, { size: 'lg' });
    this.resetform();
  }
  public getPromotionOnPackagesList() {
    


    this.filterdata.packageId = this.PackageIdFilter;
    this.filterdata.promotionId = this.PromotionIdFilter
    this.filterdata.userRoleId = this.UserRoleIdfilter
    this.filterdata.PageNumber = this.pageNumber
    this.filterdata.PageSize = this.pageSize
    this.filterdata.OrderByColumn = this.dataOrderBy;
    this.filterdata.entityStatus = this.Entity;
    
    this.service.post(this.service.apiRoutes.PackagesAndPayments.GetAllPromotionOnPackages, this.filterdata).subscribe(result => {
      

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
        this.Loader.hide();
      }
      else {
        this.recoardNoFrom = 0;
        this.recoardNoTo = 0;
        this.noOfPages = 0;
        this.totalRecoards = 0;
        this.packagesList = [];
        this.toastr.error("No record found !", "Search")
        this.Loader.hide();
      }
      this.Loader.hide();

    })
  }
  updatePackages(packagedata, content) {
    
    this.pacclass.packageId = packagedata.packageId;
    this.pacclass.promotionId = packagedata.promotionId;
    this.pacclass.CreatedBy = packagedata.userId;
    this.pacclass.UpdatedBy = packagedata.userId;
    this.pacclass.originalSalePrice = packagedata.originalSalePrice;
    this.pacclass.userRoleId = packagedata.userRoleId;
    this.pacclass.discountPercentPrice = packagedata.discountPercentPrice;
    this.pacclass.priceAfterDiscount = packagedata.priceAfterDiscount;
    this.pacclass.validityDays = packagedata.validityDays;
    this.pacclass.discountDays = packagedata.discountDays;
    this.pacclass.discountedValidityDays = packagedata.discountedValidityDays;
    this.pacclass.totalApplicableJobs = packagedata.totalApplicableJobs;

    this.pacclass.discountJobsApplied = packagedata.discountJobsApplied;
    this.pacclass.discountedTotalApplicableJobs = packagedata.discountedTotalApplicableJobs;
    this.pacclass.totalCategories = packagedata.totalCategories;
    this.pacclass.discountCategories = packagedata.discountCategories;
    this.pacclass.discountedTotalCategories = packagedata.discountedTotalCategories;
    this.pacclass.entityStatus = packagedata.entityStatus;
    this.pacclass.Status = "Update";
    this.pacclass.promotionOnPackagesId = packagedata.promotionOnPackagesId;
    this.pacclass.isActive = packagedata.isActive
    this.pkgForm.patchValue(this.pacclass);
    this._modalService.open(content, { size: 'lg' });

  }

  deletepackage(packagedata, deleteContent) {
    
    this.pacclass.packageId = packagedata.packageId;
    this.pacclass.promotionId = packagedata.promotionId;
    this.pacclass.CreatedBy = packagedata.userId;
    this.pacclass.UpdatedBy = packagedata.userId;
    this.pacclass.originalSalePrice = packagedata.originalSalePrice;
    this.pacclass.userRoleId = packagedata.userRoleId;
    this.pacclass.discountPercentPrice = packagedata.discountPercentPrice;
    this.pacclass.priceAfterDiscount = packagedata.priceAfterDiscount;
    this.pacclass.validityDays = packagedata.validityDays;
    this.pacclass.discountDays = packagedata.discountDays;
    this.pacclass.discountedValidityDays = packagedata.discountedValidityDays;
    this.pacclass.totalApplicableJobs = packagedata.totalApplicableJobs;

    this.pacclass.discountJobsApplied = packagedata.discountJobsApplied;
    this.pacclass.discountedTotalApplicableJobs = packagedata.discountedTotalApplicableJobs;
    this.pacclass.totalCategories = packagedata.totalCategories;
    this.pacclass.discountCategories = packagedata.discountCategories;
    this.pacclass.discountedTotalCategories = packagedata.discountedTotalCategories;
    this.pacclass.entityStatus = packagedata.entityStatus;
   
    this.pacclass.promotionOnPackagesId = packagedata.promotionOnPackagesId;
    this.pacclass.isActive = packagedata.isActive
    if (packagedata.isActive == true)
      this.pacclass.isActive = false;
    else
      this.pacclass.isActive = true;

    this.pacclass.Status = "Delete";
    this._modalService.open(deleteContent);
  }

  confirmDeletePackage() {
    this.save();
  }
  clickchange() {
    this.getPromotionOnPackagesList();
  }
  PageSizeChange() {

    this.pageNumber = 1;
    this.getPromotionOnPackagesList();
  }
  PriviousClick() {
    if (this.pageNumber > 1) {
      this.pageNumber = parseInt(this.pageNumber.toString()) - 1;
      this.getPromotionOnPackagesList();
    }

  }
  NextClick() {
    if (this.pageNumber < this.noOfPages) {
      this.pageNumber = parseInt(this.pageNumber.toString()) + 1;
      this.getPromotionOnPackagesList();
    }

  }
  LoadNewestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "DESC";
    this.getPromotionOnPackagesList();

  }
  LoadOldestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "ASC";
    this.getPromotionOnPackagesList();

  }
  NumberOfPages() {
    this.totalPages = Math.ceil(this.totalJobs / this.pageSize);
  }
  changePageSize() {
    this.pageNumber = 1;
    this.NumberOfPages();
    this.getPromotionOnPackagesList();
  }
  public BindPackages() {
    
    this.packagesfilter.PackageName = "";
    this.packagesfilter.PackageCode = "";
    this.packagesfilter.UserRoleId = 0;
    this.packagesfilter.PageNumber = 1;
    this.packagesfilter.PageSize = 500;
    this.packagesfilter.OrderByColumn = 'ASC';

    
    this.service.post(this.service.apiRoutes.PackagesAndPayments.GetPackages, this.packagesfilter).subscribe(result => {
      
      this.packageddl = result.json();
      this.packageddl = this.packageddl.filter(obj => obj.isActive == true)
      
    });
  }
  bindPromotionsList() {
    
    let obj = {};
    this.Loader.show()
    this.service.post(this.service.apiRoutes.PackagesAndPayments.GetPromotionList, obj).subscribe(result => {
      this.promotionddl = result.json();
      this.promotionddl = this.promotionddl.filter(obj => obj.isActive == true)


    });
  }
  public bindpackagesdata(obj1) {
    // alert('eee');
    
    var selectedid = obj1.target.value;
     this.packagesset = this.packageddl.filter(obj => obj.packageId == parseInt(selectedid))

    this.pkgForm.controls.originalSalePrice.setValue(this.packagesset[0].salePrice);
    this.pkgForm.controls.discountPercentPrice.setValue(0);
    this.pkgForm.controls.priceAfterDiscount.setValue(this.packagesset[0].salePrice);

    this.pkgForm.controls.validityDays.setValue(this.packagesset[0].validityDays);
    this.pkgForm.controls.discountDays.setValue(0);
    this.pkgForm.controls.discountedValidityDays.setValue(this.packagesset[0].validityDays);

    this.pkgForm.controls.totalApplicableJobs.setValue(this.packagesset[0].totalApplicableJobs);
    this.pkgForm.controls.discountJobsApplied.setValue(0);
    this.pkgForm.controls.discountedTotalApplicableJobs.setValue(this.packagesset[0].totalApplicableJobs);

    this.pkgForm.controls.totalCategories.setValue(this.packagesset[0].totalCategories);
    this.pkgForm.controls.discountCategories.setValue(0);
    this.pkgForm.controls.discountedTotalCategories.setValue(this.packagesset[0].totalCategories);
    this.pkgForm.controls.userRoleId.setValue(this.packagesset[0].userRoleId);
  }


  public setpromotion(obj) {
    
    var selectedid = obj.target.value;

     this.promotionset = this.promotionddl.filter(obj => obj.promotionId == parseInt(selectedid))

    if (this.promotionset[0].discountPercentPrice > 0) {
      this.pkgForm.controls.discountPercentPrice.setValue(this.promotionset[0].discountPercentPrice);
      this.pkgForm.controls.priceAfterDiscount.setValue(this.packagesset[0].salePrice -((this.packagesset[0].salePrice / 100) * this.promotionset[0].discountPercentPrice));


    }
    else {
      this.pkgForm.controls.discountPercentPrice.setValue(0);
      this.pkgForm.controls.priceAfterDiscount.setValue(this.packagesset[0].salePrice);
    }

    if (this.promotionset[0].discountDays > 0) {
      this.pkgForm.controls.discountDays.setValue(this.promotionset[0].discountDays);
      this.pkgForm.controls.discountedValidityDays.setValue(this.packagesset[0].validityDays + this.promotionset[0].discountDays);


    }
    else {
      this.pkgForm.controls.discountDays.setValue(0);
      this.pkgForm.controls.discountedValidityDays.setValue(this.packagesset[0].validityDays);
    }

    if (this.promotionset[0].discountJobsApplied > 0) {
      this.pkgForm.controls.discountJobsApplied.setValue(this.promotionset[0].discountJobsApplied);
      this.pkgForm.controls.discountedTotalApplicableJobs.setValue(this.packagesset[0].totalApplicableJobs + this.promotionset[0].discountJobsApplied);


    }
    else {
      this.pkgForm.controls.discountJobsApplied.setValue(0);
      this.pkgForm.controls.discountedTotalApplicableJobs.setValue(this.packagesset[0].totalApplicableJobs);
    }

    if (this.promotionset[0].discountCategories > 0) {
      this.pkgForm.controls.discountCategories.setValue(this.promotionset[0].discountCategories);
      this.pkgForm.controls.discountedTotalCategories.setValue(this.packagesset[0].totalCategories + this.promotionset[0].discountCategories);


    }
    else {
      this.pkgForm.controls.discountCategories.setValue(0);
      this.pkgForm.controls.discountedTotalCategories.setValue(this.packagesset[0].totalCategories);
    }
  }
}
