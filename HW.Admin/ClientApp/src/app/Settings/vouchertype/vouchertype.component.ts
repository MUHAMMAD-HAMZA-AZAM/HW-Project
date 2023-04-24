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
  selector: 'app-vouchertype',
  templateUrl: './vouchertype.component.html',
  styleUrls: ['./vouchertype.component.css']
})
export class VoucherTypeComponent implements OnInit {
  public appValueForm: FormGroup;
  public voucherCategoryList = [];
  public voucherTypeList = [];
  public updateFromData = {};
  public modelText: string = "";
  public promotiontypeon = '';
  public submited: boolean = false;
  public disabledfields;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    
  constructor(public router: Router,public _modalService: NgbModal, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Voucher Type"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');    this.promFormData();
    this.getVoucherCategoryList();
    this.getVoucherTypeList();
  }
  public promFormData() {
    this.appValueForm = this.fb.group({
      voucherTypeId: [0],
      voucherTypeCode: ['', [Validators.required]],
      voucherTypeName: ['', [Validators.required]],
      voucherCategoryId: ['', [Validators.required]],
      active: [0, [Validators.required]],
    })
  }
 
  get f() {
    return this.appValueForm.controls;
  }
  getVoucherTypeList() {
    
    this.Loader.show()
    this.service.get(this.service.apiRoutes.PackagesAndPayments.getVoucherTypeList).subscribe(result => {
      
      this.voucherTypeList = result.json();
      this.Loader.hide();
    })
  }
  saveAndUpdate() {
    
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    let formData;
    if (this.appValueForm.invalid) {
      this.submited = true;
      return;
    }
    //add
    if (this.appValueForm.value.voucherTypeId <= 0) {
      formData = this.appValueForm.value;
      formData.createdBy = decodedtoken.UserId;
      formData.voucherTypeId = 0;
      formData.active = parseInt(formData.active);
      console.log(formData);
      this.Loader.show();
      this.service.post(this.service.apiRoutes.PackagesAndPayments.AddUpdateVoucherType, formData).subscribe(result => {
        
        var res = result.json();
        if (res.status == 200) {
          this.appValueForm.reset()
          this.toastr.success(res.message, "Success");
          this.getVoucherTypeList();
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
      formData.modifiedBy = decodedtoken.UserId;
      formData.active = parseInt(formData.active);
      console.log(formData);
      this.Loader.show();
      this.service.post(this.service.apiRoutes.PackagesAndPayments.AddUpdateVoucherType, formData).subscribe(result => {
        var res = result.json();
        if (res.status == 200) {
          this.appValueForm.reset()
          this.toastr.success(res.message, "Success");
          this.getVoucherTypeList();
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
  updateVoucherType(item, content) {
    
    item.active == true ? item.active = "1" : item.active = "0";
    this.appValueForm.patchValue(item);
    this.updateFromData = item;
    console.log(this.updateFromData);
    this.modelText = "Update Promotion"
    this._modalService.open(content)
  }
  deleteVouchertype(voucherTypeId) {
    
    this.Loader.show();
    this.service.get(this.service.apiRoutes.PackagesAndPayments.deleteVoucherType + "?voucherTypeId=" + voucherTypeId).subscribe(result => {
      var res = result.json();
      if (res.status == 200) {
        this.appValueForm.reset()
        this.toastr.success(res.message, "Success");
        this.getVoucherTypeList();
      }
      else {
        this.toastr.error(res.message, "Error");
      }
      this.Loader.hide();
    })
  }
  showModal(content) {
    this.modelText = "Add New Voucher Type"
    this.appValueForm.reset();
    this._modalService.open(content)
  }
  resetForm(): void {
    this.appValueForm.reset();
  }
  public getVoucherCategoryList() {
    this.service.get(this.service.apiRoutes.PackagesAndPayments.GetvoucherCategoryList).subscribe(result => {
      this.voucherCategoryList = result.json();
    })
  }

}
