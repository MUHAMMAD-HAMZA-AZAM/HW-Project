import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '../../Shared/HttpClient/_http';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";
import { JwtHelperService } from '@auth0/angular-jwt';
import { PackagesAndPromotion, PackagesFiltrClass, PackageDDClass, PromotionOnPackagesDDClass, PackagesClass, UserDetailData, SetProduct } from '../../Shared/Models/PackagesAndPayments/PackagesModel';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { httpStatus } from '../../Shared/Enums/enums';
import { SortList } from '../../Shared/Sorting/sortList';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {


  public pkgForm: FormGroup;
  public pkgfilters: FormGroup;
  public responcelist = [];
  public packagesList = [];
  public promotionList = [];
  public tempList = [];
  public pageNumber: number = 1;
  public pageSize: number = 50;
  public dataOrderBy = "DESC";
  public noOfPages;
  public Productlist: any;
  public Productlist1: SetProduct = new SetProduct();
  public Productlist2:  SetProduct[];

  public promotionddl: PromotionOnPackagesDDClass = new PromotionOnPackagesDDClass();
  public packagesfilter: PackagesFiltrClass = new PackagesFiltrClass();
  public pacclass: PackagesAndPromotion = new PackagesAndPromotion();
  public pacclassdata: PackagesAndPromotion = new PackagesAndPromotion();
  public filterdata: PackagesAndPromotion = new PackagesAndPromotion();
  public packageddl: PackagesClass[]
  public packagesset: PackagesClass[]
  public promotiononpackages: PackagesAndPromotion[];
  public promotiononpackagesfilter: PackagesAndPromotion[];
  public promotiononpackagessingle: PackagesAndPromotion[];
  public userdata: UserDetailData[];
  public userdatafiltred: UserDetailData[];
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
  public AspUserfilter: number;
  public TradesmanSkills = [''];
  public TradesmanSkillsSelected = [''];
  public skillsdropdownSettings = {};
  public skillList;
  public SelectedSkillsList = [];
  public supplierdropdownSettings = {};
  public byDefaultSelectCategoriesInDropdown: number;
  public selectedUserRoleId: number;
  public categoryList = [];
  public selectedSupplierType = [];
  public selectedCategories = [];

  public submited: boolean = false;


  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(public fb: FormBuilder, public toastr: ToastrService, public _modalService: NgbModal, public service: CommonService, public Loader: NgxSpinnerService, public sortBy: SortList) {

  }

  ngOnInit() {

    this.skillsdropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true,
      limitSelection: this.byDefaultSelectCategoriesInDropdown
    };

    this.supplierdropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true,
      limitSelection: this.byDefaultSelectCategoriesInDropdown
    };

    this.pkgForm = this.fb.group({
      orderId: [0],
      promotionOnPackagesId: [0, [Validators.required]],
      packageId: [0, [Validators.required]],
      userRoleId: [0, [Validators.required]],
      userId: [0, [Validators.required]],
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
      orderTotal: ['', [Validators.required]],
      isActive: [false],
      createdBy: "TEST",
      updatedBy: "Test:",
      tradesmanSkills: [0],
      selectedSkills: [0],
      selectedSupplierType:[0]
    })
    this.pkgfilters = this.fb.group({
      PackageIdFilter: [0],
      PromotionOnPackagesIdFilter: [0],
      UserRoleIdfilter: [0],
      AspUserfilter: [0],
    });
    this.submited = false;
    this.BindPackages();
    this.bindPromotionsList();
    this.dataOrderBy = "DESC";
    this.PackageIdFilter = 0;
    this.PromotionIdFilter = 0;
    this.UserRoleIdfilter = 0;
    this.SelectedSkillsList = [];
    this.pageSize = 50;
    this.pageNumber = 1;
    this.getOrderList();
    this.bindusernames();
    this.populateSkills();
    this.populateCategories();
  }
  saveAndUpdate() {
    alert(this.pkgForm.value.status);
  }
  resetForm() {
    this.pkgfilters.reset();
    this.pacclass = new PackagesAndPromotion();
    this.promotiononpackagesfilter = this.promotiononpackages;
  }
  get f() {
    return this.pkgForm.controls;

  }


  resetform() {
    
    this.SelectedSkillsList = [];
    this.selectedCategories = [];
    this.pkgForm.reset();
    this.promotiononpackagesfilter = this.promotiononpackages;
    this.pacclass = new PackagesAndPromotion();

  }

  save() {
    
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    var data1 = this.pkgForm.value;
    if (this.pacclass.Status != 'Delete') {
      if (this.pkgForm.invalid) {
        this.submited = true;
        return;

      }
      let ids = [];
     
      this.SelectedSkillsList.forEach(function (item) {
        ids.push(item.id);
      })

      this.pacclass.packageId = data1.packageId;
      this.pacclass.selectedSkills = ids.toString();
      this.pacclass.promotionOnPackagesId = data1.promotionOnPackagesId;
      this.pacclass.CreatedBy = decodedtoken.UserId;
      this.pacclass.UpdatedBy = decodedtoken.UserId;
      this.pacclass.originalSalePrice = data1.originalSalePrice;
      this.pacclass.userRoleId = data1.userRoleId;
      this.pacclass.aspnetUserId = data1.userId;
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
      this.pacclass.orderTotal = data1.orderTotal;
      this.pacclass.entityStatus = data1.entityStatus;

      this.pacclass.orderId = data1.orderId;

      if (this.pacclass.orderId == 0 || this.pacclass.orderId < 1) {
        this.pacclass.Status = "Saved";
        this.pacclass.isActive = true;

      }
    }
    else {
      this.pacclass.CreatedBy = decodedtoken.UserId;
      this.pacclass.UpdatedBy = decodedtoken.UserId;
    }
    if (this.pacclass.orderId == null)
      this.pacclass.orderId = 0;
    

    this.service.post(this.service.apiRoutes.PackagesAndPayments.AddOrder, this.pacclass).subscribe(result => {
      var res = result.json();

      if (res.message == 'AlreadyExists') {
        this.toastr.error("Package& Name Already Exist", "Error");
      }
      else if (res.message == 'CodeAlreadyExists') {
        this.toastr.error("Package Code Already Exist", "Error");
      }
      else if (res.message == 'Saved') {
        this.toastr.success("Data added Successfully", "Success");

        this.getOrderList();
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

        this.getOrderList();
      }
    })
  }
  public showModal(content) {
    this.SelectedSkillsList = [];
    this._modalService.open(content, { size: 'lg' });
    this.resetform();
  }
  public getOrderList() {
    
    this.filterdata = new PackagesAndPromotion();
    var data1 = this.pkgfilters.value;

    this.filterdata.packageId = data1.PackageIdFilter;
    this.filterdata.promotionOnPackagesId = data1.PromotionOnPackagesIdFilter;
    this.filterdata.userRoleId = data1.UserRoleIdfilter;
    this.filterdata.aspnetUserId = data1.AspUserfilter;
    this.filterdata.PageNumber = this.pageNumber
    this.filterdata.PageSize = this.pageSize
    this.filterdata.OrderByColumn = this.dataOrderBy;

    
    this.service.post(this.service.apiRoutes.PackagesAndPayments.GetOrder, this.filterdata).subscribe(result => {
      

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
    this.pacclass.promotionOnPackagesId = packagedata.promotionOnPackagesId;
    this.pacclass.CreatedBy = packagedata.userId;
    this.pacclass.UpdatedBy = packagedata.userId;
    this.pacclass.originalSalePrice = packagedata.originalSalePrice;
    this.pacclass.userRoleId = packagedata.userRoleId;
    this.pacclass.aspnetUserId = packagedata.aspnetUserId;
    this.pacclass.userId = packagedata.aspnetUserId;
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
    this.pacclass.orderTotal = packagedata.orderTotal;
    this.pacclass.entityStatus = packagedata.entityStatus;
    this.pacclass.Status = "Update";
    this.pacclass.orderId = packagedata.orderId;
    this.pacclass.isActive = packagedata.isActive
    this.pkgForm.patchValue(this.pacclass);

    let skillsid;
    let skillname;

    if (packagedata.skillsIds != null) {
       skillsid = (packagedata.skillsIds).split(',');
    }
    if (packagedata.selectedSkills != null) {
      skillname = (packagedata.selectedSkills).split(',');
     
    }
    
      this._modalService.open(content);
    
  }

  deletepackage(packagedata, deleteContent) {
    
    this.pacclass.packageId = packagedata.packageId;
    this.pacclass.promotionOnPackagesId = packagedata.promotionOnPackagesId;
    this.pacclass.CreatedBy = packagedata.userId;
    this.pacclass.UpdatedBy = packagedata.userId;
    this.pacclass.originalSalePrice = packagedata.originalSalePrice;
    this.pacclass.userRoleId = packagedata.userRoleId
    this.pacclass.aspnetUserId = packagedata.aspnetUserId;
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
    this.pacclass.selectedSkills = packagedata.skillsIds;
    
    this.pacclass.orderId = packagedata.orderId;
    this.pacclass.isActive = packagedata.isActive
    if (packagedata.isActive == true)
      this.pacclass.isActive = false;
    else
      this.pacclass.isActive = true;

    let ids = [];

    this.SelectedSkillsList.forEach(function (item) {
      ids.push(item.id);
    })
    this.pacclass.Status = "Delete";
    this._modalService.open(deleteContent);
  }

  confirmDeletePackage() {
    this.save();
  }
  clickchange() {
    this.getOrderList();
  }
  PageSizeChange() {

    this.pageNumber = 1;
    this.getOrderList();
  }
  PriviousClick() {
    if (this.pageNumber > 1) {
      this.pageNumber = parseInt(this.pageNumber.toString()) - 1;
      this.getOrderList();
    }

  }
  NextClick() {
    if (this.pageNumber < this.noOfPages) {
      this.pageNumber = parseInt(this.pageNumber.toString()) + 1;
      this.getOrderList();
    }

  }
  LoadNewestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "DESC";
    this.getOrderList();

  }
  LoadOldestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "ASC";
    this.getOrderList();

  }
  NumberOfPages() {
    this.totalPages = Math.ceil(this.totalJobs / this.pageSize);
  }
  changePageSize() {
    this.pageNumber = 1;
    this.NumberOfPages();
    this.getOrderList();
  }
  onItemSelectAll(items: any) {
    console.log(items);
    this.SelectedSkillsList = items;
  
  }
  OnItemDeSelectALL(items: any) {
    this.SelectedSkillsList = [];
    console.log(items);
  }
  onItemSelect(item: any) {
    this.SelectedSkillsList.push(item);
    console.log(this.SelectedSkillsList);
  }
  OnItemDeSelect(item: any) {

    this.SelectedSkillsList = this.SelectedSkillsList.filter(
      function (value, index, arr) {
        return value.id != item.id;
      }

    );

    console.log(this.SelectedSkillsList);
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
      this.dataOrderBy = "ASC";
    });
  }
  public bindPromotionsList() {
    


    this.filterdata.packageId = 0;
    this.filterdata.promotionId = 0;
    this.filterdata.userRoleId = 0;
    this.filterdata.entityStatus = '1';
    this.filterdata.PageNumber = 1;
    this.filterdata.PageSize = 500;
    this.filterdata.OrderByColumn = "DESC";
    
    
    this.service.post(this.service.apiRoutes.PackagesAndPayments.GetAllPromotionOnPackages, this.filterdata).subscribe(result => {
      


      this.promotiononpackages = result.json();
      this.promotiononpackagesfilter = this.promotiononpackages;
      
    });
  }
  public bindusernames() {
    
    this.service.get(this.service.apiRoutes.Customers.GetCustomer ).subscribe(result => {
        
      this.userdata = result.json();
      if (this.userdata.filter(x => x.roleId == 1)) {
        this.userdatafiltred = this.userdata.filter(x => x.roleId == 1);
      }
      else if (this.userdata.filter(x => x.roleId == 2)) {
        this.userdatafiltred = this.userdata.filter(x => x.roleId == 2);
      }
      else if (this.userdata.filter(x => x.roleId == 3)) {
        this.userdatafiltred = this.userdata.filter(x => x.roleId == 3);
      }
      else if (this.userdata.filter(x => x.roleId == 4)) {
        this.userdatafiltred = this.userdata.filter(x => x.roleId == 4);
      }
      else {
        this.userdatafiltred = this.userdata;
      }

      this.userdatafiltred = this.sortBy.transform(this.userdatafiltred, "firstName", 'asc');
    });
   

  }

  public bindpackagesdata(obj1) {
    
    
    var selectedid = obj1.target.value;
    this.packagesset = this.packageddl.filter(obj => obj.packageId == parseInt(selectedid))
    
    this.pkgForm.controls.originalSalePrice.setValue(this.packagesset[0].salePrice);
    this.pkgForm.controls.discountPercentPrice.setValue(0);
    this.pkgForm.controls.priceAfterDiscount.setValue(this.packagesset[0].salePrice);

    this.pkgForm.controls.orderTotal.setValue(this.packagesset[0].salePrice);

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

    this.promotiononpackagesfilter = this.promotiononpackages.filter(obj => obj.packageId == parseInt(selectedid))

    this.pkgForm.controls.promotionOnPackagesId.setValue(0);

    this.pkgForm.controls.selectedSkills.setValue(0);

    this.pkgForm.controls.selectedSupplierType.setValue(0);

    this.userdatafiltred = this.userdata.filter(x => x.roleId == this.packagesset[0].userRoleId);

    this.userdatafiltred = this.sortBy.transform(this.userdatafiltred, "firstName", 'asc');

    this.selectedUserRoleId = this.packagesset[0].userRoleId;


    
    if (this.packagesset[0].userRoleId == 1 || this.packagesset[0].userRoleId == 2 || this.packagesset[0].userRoleId== 4) {
      
    }
  }

  public bindpromotiononPkg(obj1) {
    
    var selectedid = obj1.target.value;
    this.promotiononpackagessingle = this.promotiononpackages.filter(obj => obj.promotionOnPackagesId == parseInt(selectedid))

    this.pkgForm.controls.originalSalePrice.setValue(this.promotiononpackagessingle[0].originalSalePrice);
    this.pkgForm.controls.discountPercentPrice.setValue(this.promotiononpackagessingle[0].discountPercentPrice);
    this.pkgForm.controls.priceAfterDiscount.setValue(this.promotiononpackagessingle[0].priceAfterDiscount);

    this.pkgForm.controls.orderTotal.setValue(this.promotiononpackagessingle[0].priceAfterDiscount);

    this.pkgForm.controls.validityDays.setValue(this.promotiononpackagessingle[0].validityDays);
    this.pkgForm.controls.discountDays.setValue(this.promotiononpackagessingle[0].discountDays);
    this.pkgForm.controls.discountedValidityDays.setValue(this.promotiononpackagessingle[0].discountedValidityDays);

    this.pkgForm.controls.totalApplicableJobs.setValue(this.promotiononpackagessingle[0].totalApplicableJobs);
    this.pkgForm.controls.discountJobsApplied.setValue(this.promotiononpackagessingle[0].discountJobsApplied);
    this.pkgForm.controls.discountedTotalApplicableJobs.setValue(this.promotiononpackagessingle[0].discountedTotalApplicableJobs);

    this.pkgForm.controls.totalCategories.setValue(this.promotiononpackagessingle[0].totalCategories);
    this.pkgForm.controls.discountCategories.setValue(this.promotiononpackagessingle[0].discountCategories);
    this.pkgForm.controls.discountedTotalCategories.setValue(this.promotiononpackagessingle[0].discountedTotalCategories);
    this.pkgForm.controls.userRoleId.setValue(this.promotiononpackagessingle[0].userRoleId);
    
  }
  public populateSkills() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.TrdesMan.GetTradesManSkills).subscribe(result => {
      this.skillList = result.json();
      console.log(result.json());
      this.Loader.hide();
    },
      error => {
        console.log(error);
        this.Loader.hide();
      });
  }

  populateCategories() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Supplier.GetCategories).subscribe(result => {
      
      this.categoryList = result.json();
      console.log(result.json());
      this.Loader.hide();
    },
      error => {
        console.log(error);
        this.Loader.hide();
      });


  }
  onItemSelectAll1(items: any) {
    console.log(items);
    this.selectedCategories = items;


  }
  OnItemDeSelectALL1(items: any) {
    this.selectedCategories = [];
    console.log(items);
  }
  onItemSelect1(item: any) {
    this.selectedCategories.push(item);
    console.log(this.selectedCategories);
  }
  OnItemDeSelect1(item: any) {

    this.selectedCategories = this.selectedCategories.filter(
      function (value, index, arr) {
        return value.id != item.id;
      }

    );
    console.log(this.selectedCategories);
  }

  
  change() {
    
    this.skillsdropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true,
      limitSelection: this.byDefaultSelectCategoriesInDropdown
    };
    this.supplierdropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true,
      limitSelection: this.byDefaultSelectCategoriesInDropdown
    };
  }
}
