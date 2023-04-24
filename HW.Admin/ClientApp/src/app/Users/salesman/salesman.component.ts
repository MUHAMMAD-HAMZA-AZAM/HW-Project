import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '../../Shared/HttpClient/_http';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";
import { JwtHelperService } from '@auth0/angular-jwt';
import { CouponTypeClass, PackagesFiltrClass, SalesmanVM } from '../../Shared/Models/PackagesAndPayments/PackagesModel';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';

@Component({
  selector: 'app-salesman',
  templateUrl: './salesman.component.html',
  styleUrls: ['./salesman.component.css']
})
export class SalesmanComponent implements OnInit {
  public pkgForm: FormGroup;

  public responcelist = [];
  public salesmanList = [];
  public tempList = [];
  public pageNumber: number = 1;
  public pageSize: number = 50;
  public dataOrderBy = "DESC";
  public noOfPages;
  public pacclass: SalesmanVM = new SalesmanVM();
  public pacclassdata: SalesmanVM = new SalesmanVM();
  //public filterdata: PackagesFiltrClass = new PackagesFiltrClass();
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
  constructor(public router: Router , public fb: FormBuilder, public toastr: ToastrService, public _modalService: NgbModal, public service: CommonService, public Loader: NgxSpinnerService, ) {

  }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Salesman"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');

    this.getPromotionTypeList();
    this.pkgForm = this.fb.group({
      salesmanId: [0],
      name: ['', [Validators.required]],
      mobileNumber: [''],
      address: [''],
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
    this.pacclass = new SalesmanVM();

  }

  save() {
    
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));

    var data1 = this.pkgForm.value;
    if (this.pacclass.Status != 'Delete') {
      if (this.pkgForm.invalid) {
        this.submited = true;
        return;
      }
      this.pacclass.mobileNumber = data1.mobileNumber;
      this.pacclass.createdBy = decodedtoken.UserId;
      this.pacclass.mobifiedBy = decodedtoken.UserId;
      this.pacclass.address = data1.address;
      this.pacclass.name = data1.name;
      this.pacclass.salesmanId = data1.salesmanId;
      if (this.pacclass.salesmanId == 0 || this.pacclass.salesmanId < 1) {
        this.pacclass.Status = "Saved";
        this.pacclass.isActive = true;
      }
    }
    else {
      this.pacclass.createdBy = decodedtoken.UserId;
      this.pacclass.mobifiedBy = decodedtoken.UserId;
    }
    if (this.pacclass.salesmanId == null)
      this.pacclass.salesmanId = 0;
    this.service.post(this.service.apiRoutes.UserManagement.addsalesman, this.pacclass).subscribe(result => {
      var res = result.json();
      if (res.message == 'Saved') {
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
    this.service.get(this.service.apiRoutes.UserManagement.getAllSalesman).subscribe(result => {   
      this.salesmanList = result.json();
    })
  }
  updatePackages(packagedata, content) {
    
    // this.modalHeadText = "Upade City"
    //this.cityDeleteId = String(cityId);
    this.pacclass.salesmanId = packagedata.salesmanId
    this.pacclass.name = packagedata.name
    this.pacclass.mobileNumber = packagedata.mobileNumber
    this.pacclass.isActive = packagedata.isActive
    this.pacclass.address = packagedata.address
    this.pacclass.Status = "Update";
    //this.pacclassdata. = packagedata.packageId
    this.pkgForm.patchValue(this.pacclass);
    this._modalService.open(content);
  }

  deletepackage(packagedata, deleteContent) {
    
    this.pacclass.salesmanId = packagedata.salesmanId
    this.pacclass.name = packagedata.name
    this.pacclass.address = packagedata.address
    this.pacclass.mobileNumber = packagedata.mobileNumber

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

}
