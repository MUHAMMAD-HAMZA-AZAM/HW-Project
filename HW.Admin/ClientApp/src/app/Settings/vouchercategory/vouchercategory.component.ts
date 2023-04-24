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
  selector: 'app-vouchercategory',
  templateUrl: './vouchercategory.component.html',
  styleUrls: ['./vouchercategory.component.css']
})
export class VoucherCategoryComponent implements OnInit {
  public voucherForm: FormGroup;
  public voucherCategoryList = [];
  public modelText: string = "";
  public submited: boolean = false;
  public disabledfields;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    

  constructor(public router: Router,public _modalService: NgbModal, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Voucher Category"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.voucherCategorForm();
    this.getVoucherCategoryList();
  }
  public voucherCategorForm() {
    this.voucherForm = this.fb.group({
      voucherCategoryId: [0],
      voucherCategoryCode: ['', [Validators.required]],
      voucherCategoryName: ['', [Validators.required]],
      active: [0, [Validators.required]],
      createdBy: [''],
    })
  }
  get f() {
    return this.voucherForm.controls;
  }
  getVoucherCategoryList() {
    
    this.Loader.show()
    this.service.get(this.service.apiRoutes.PackagesAndPayments.GetvoucherCategoryList).subscribe(result => {
      
      this.voucherCategoryList = result.json();
      console.log(this.voucherCategoryList);
      this.Loader.hide();
    })
  }
  saveAndUpdate() {
    
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    
    let formData;
    if (this.voucherForm.invalid) {
      this.submited = true;
      return;
    }
    if (this.voucherForm.value.voucherCategoryId <= 0) {
      
      formData = this.voucherForm.value;
      formData.createdBy = decodedtoken.UserId;
      formData.voucherCategoryId = 0;
      formData.active = parseInt(formData.active);
      console.log(formData);
      this.Loader.show();
      this.service.post(this.service.apiRoutes.PackagesAndPayments.AddUpdateVoucherCategory, formData).subscribe(result => {
        var res = result.json();
        if (res.status == 200) {
          this.voucherForm.reset()
          this.toastr.success(res.message, "Success");
          this.getVoucherCategoryList();
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
      formData = this.voucherForm.value;
      formData.modifiedBy = decodedtoken.UserId;
      formData.active = parseInt(formData.active);
      console.log(formData);
      this.Loader.show();
      this.service.post(this.service.apiRoutes.PackagesAndPayments.AddUpdateVoucherCategory, formData).subscribe(result => {
        var res = result.json();
        if (res.status == 200) {
          this.voucherForm.reset()
          this.toastr.success(res.message, "Success");
          this.getVoucherCategoryList();
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
  updateVoucherCategory(category, content) {
    
    category.active == true ? category.active = "1" : category.active = "0";
    this.voucherForm.patchValue(category);
    this.modelText = "Update Voucher Category"
    this._modalService.open(content)
  }
  deleteVoucherCategory(voucherCategoryId) {
    
    this.Loader.show();
    this.service.get(this.service.apiRoutes.PackagesAndPayments.deleteVoucherCategory + "?voucherCategoryId=" + voucherCategoryId).subscribe(result => {
      var res = result.json();
      if (res.status == 200) {
        this.voucherForm.reset()
        this.toastr.success(res.message, "Success");
        this.getVoucherCategoryList();
      }
      else {
        this.toastr.error(res.message, "Error");
      }
      this.Loader.hide();
    })
  }
  showModal(content) {
    this.modelText = "Add New Voucher Category"
    this.voucherForm.reset();
    this._modalService.open(content)
  }
  resetForm(): void {
    this.voucherForm.reset();
  }

}
