import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../Shared/HttpClient/_http';
import { NgxSpinnerService } from "ngx-spinner";
import { DatePipe } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-voucherbookallocation',
  templateUrl: './voucherbookallocation.component.html',
  styleUrls: ['./voucherbookallocation.component.css']
})
export class VoucherBookAllocationComponent implements OnInit {
  public appValueForm: FormGroup;
  public filterappValueForm: FormGroup;
  public voucherBookList = [];
  public voucherBookList1 = [];
  public voucherBookAllocationList = [];
  public updateFromData = {};
  public modelText: string = "";
  public promotiontypeon = '';
  public submited: boolean = false;
  public disabledfields;


  jwtHelperService: JwtHelperService = new JwtHelperService();

  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    
  constructor(public router: Router,public _modalService: NgbModal, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Voucher Book Allocation"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.promFormData();
    this.getVoucherBookList();
    this.getVoucherBookAllocationList();
  }
  public promFormData() {
    //this.filterappValueForm = this.fb.group({
    //  fpromotionIdTypeId: [null],
    //  fpromotionCode: [''],
    //  fpromotionName: [''],
    //  fpromoStartDate: [''],
    //  fpromotionEndDate: [''],
    //  entity: ['1'],
    //})
    this.appValueForm = this.fb.group({
      voucherBookAllocationId: [0],
      assigneeFirstName: ['', [Validators.required]],
      assigneeLastName: ['', [Validators.required]],
      contactNo: ['', [Validators.required]],
      voucherBookId: [0, [Validators.required]],
      isInternalPerson: [0, [Validators.required]],
      employDesignation: [''],
      externalPersonOccupation: [''],
      externalDesignation: [''],
      company: ['', [Validators.required]],
      nopagesAssigned: [0, [Validators.required]],
      noOfLeavesAssigned: [0, [Validators.required]],
      active: [0, [Validators.required]]
    })
  }
  //filterPromotions() {
  //  console.warn(this.filterappValueForm.value);
  //}

  getVoucherBookAllocationList() {
    
    //var data = this.filterappValueForm.value;
    //let obj = {
    //  promotionName: data.fpromotionName,
    //  promotionCode: data.fpromotionCode,
    //  PromoStartDate: data.fpromoStartDate,
    //  PromotionEndDate: data.fpromotionEndDate,
    //  PromotionIdTypeId: data.fpromotionIdTypeId,
    //  entityStatus: data.entity
    //};
    this.Loader.show()
    this.service.get(this.service.apiRoutes.PackagesAndPayments.getVouchrBookAllocation).subscribe(result => {
      
      this.voucherBookAllocationList = result.json();
      console.log(this.voucherBookAllocationList);
      console.log(result);
      this.Loader.hide();
    })
  }
  saveAndUpdate() {
    ;
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    let formData;
    if (this.appValueForm.invalid) {
      this.submited = true;
      return;
    }
    //add
    if (this.appValueForm.value.voucherBookAllocationId <= 0) {
      formData = this.appValueForm.value;
      if (this.appValueForm.controls.isInternalPerson.value == "2") {
        formData.isInternalPerson = 0;
      }
      formData.createdBy = decodedtoken.UserId;
      formData.voucherBookAllocationId = 0;
      formData.active = parseInt(formData.active);
      formData.isInternalPerson = parseInt(formData.isInternalPerson);
      console.log(formData);
      this.Loader.show();
      this.service.post(this.service.apiRoutes.PackagesAndPayments.AddUpdateVoucherBookAllocation, formData).subscribe(result => {
        var res = result.json();
        if (res.status == 200) {
          this.appValueForm.reset()
          this.toastr.success(res.message, "Success");
          this.getVoucherBookAllocationList();
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
      formData = this.appValueForm.value;
      if (this.appValueForm.controls.isInternalPerson.value == "2") {
        formData.isInternalPerson = 0;
      }
      formData.modifiedBy = decodedtoken.UserId;
      formData.active = parseInt(formData.active);
      formData.isInternalPerson = parseInt(formData.isInternalPerson);
      console.log(formData);
      this.Loader.show();
      this.service.post(this.service.apiRoutes.PackagesAndPayments.AddUpdateVoucherBookAllocation, formData).subscribe(result => {
        var res = result.json();
        if (res.status == 200) {
          this.appValueForm.reset()
          this.toastr.success(res.message, "Success");
          this.getVoucherBookAllocationList();
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
  updateVoucherBookAllocation(voucherBookAllocation, content) {
    
    voucherBookAllocation.active == true ? voucherBookAllocation.active = "1" : voucherBookAllocation.active = "0";
    voucherBookAllocation.isInternalPerson == true ? voucherBookAllocation.isInternalPerson = "1" : voucherBookAllocation.isInternalPerson = "2";
    this.appValueForm.patchValue(voucherBookAllocation);
    this.updateFromData = voucherBookAllocation;
    console.log(this.updateFromData);
    this.modelText = "Update Voucher Book Allocation"
    this._modalService.open(content)
  }
  deleteVoucherBooKAllocation(voucherBookAllocationId) {
    
    this.Loader.show();
    this.service.get(this.service.apiRoutes.PackagesAndPayments.deleteVoucherBookAllocation + "?voucherBookAllocationId=" + voucherBookAllocationId).subscribe(result => {
      var res = result.json();
      if (res.status == 200) {
        this.appValueForm.reset()
        this.toastr.success(res.message, "Success");
        this.getVoucherBookAllocationList();
      }
      else {
        this.toastr.error(res.message, "Error");
      }
      this.Loader.hide();
    })
  }
  showModal(content) {
    this.modelText = "Add New Voucher Book Allocation"
    this.appValueForm.reset();
    this._modalService.open(content)
  }

  resetForm(): void {
    this.appValueForm.reset();
  }
  resetFForm() {
    
    this.filterappValueForm.reset();
  }
  get f() {
    return this.appValueForm.controls;
  }
  public getVoucherBookList() {
    
    this.service.get(this.service.apiRoutes.PackagesAndPayments.GetVoucherBookList).subscribe(result => {
      
      this.voucherBookList = result.json();
      
      console.log(this.voucherBookList);
    })
  }

}
