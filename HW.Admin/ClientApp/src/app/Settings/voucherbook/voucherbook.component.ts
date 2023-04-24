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
  selector: 'app-voucherbook',
  templateUrl: './voucherbook.component.html',
  styleUrls: ['./voucherbook.component.css']
})
export class VoucherBookComponent implements OnInit {
  public appValueForm: FormGroup;
  public filterappValueForm: FormGroup;
  public VoucherTypeList = [];
  public VoucherTypeList1 = [];
  public VoucherBookList = [];
  public updateFromData = {};
  public modelText: string = "";
  public promotiontypeon = '';
  public submited: boolean = false;
  public disabledfields;
  public voucherBook: boolean = true;
  public voucherType: boolean = true;
  public noOfPages: boolean = true;
  public noOfLeaves: boolean = true;
  public amount: boolean = true;
  public amountDiscount: boolean = true;
  public amountPercentage: boolean = true;
  public voucherBookAssign: boolean = true;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    

  constructor(
    public router: Router,public _modalService: NgbModal, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Voucher Book"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.promVoucherBookFormData();
   this.getVoucherType();
    this.getVoucherBookList();
  }
  public promVoucherBookFormData() {
    this.appValueForm = this.fb.group({
      voucherBookId: [0],
      voucherBookName: ['', [Validators.required]],
      voucherTypeId : [0, [Validators.required]],
      noOfLeaves: [0, [Validators.required]],
      noOfPages: [0, [Validators.required]],
      validFrom: ['', [Validators.required]],
      validTo: ['', [Validators.required]],
      bookLevelAmountDiscount: [0],
      bookLevelPersentageDiscount: [0],
      isAssigned: [0],
      active: [0, [Validators.required]],
    })
  }
  //filterPromotions() {
  //  //console.warn(this.filterappValueForm.value);
  //}

  getVoucherBookList() {
    
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
    this.service.get(this.service.apiRoutes.PackagesAndPayments.GetVoucherBookList).subscribe(result => {
      this.VoucherBookList = result.json();
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
    if (this.appValueForm.value.voucherBookId <= 0) {
      formData = this.appValueForm.value;
      formData.createdBy = decodedtoken.UserId;
      formData.voucherBookId = 0;
      formData.active = parseInt(formData.active);
      this.Loader.show();
      this.service.post(this.service.apiRoutes.PackagesAndPayments.AddUpdateVoucherBook, formData).subscribe(result => {
        
        var res = result.json();
        if (res.status == 200) {
          this.appValueForm.reset()
          this.toastr.success(res.message, "Success");
          this.getVoucherBookList();
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
      ;
      formData = this.appValueForm.value;
      formData.modifiedBy = decodedtoken.UserId;
      formData.active = parseInt(formData.active);
      this.Loader.show();
      this.service.post(this.service.apiRoutes.PackagesAndPayments.AddUpdateVoucherBook, formData).subscribe(result => {
        var res = result.json();
        if (res.status == 200) {
          this.appValueForm.reset()
          this.toastr.success(res.message, "Success");
          this.getVoucherBookList();
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
  updateVoucherBook(voucherBook, content) {
    this.voucherBook = false;
    this.voucherType = false;
    this.noOfPages = false;
    this.noOfLeaves = false;
    this.amountDiscount = false;
    this.amountPercentage = false;
    this.voucherBookAssign = false;

    var datePipe = new DatePipe("en-US");
    let voucherValidFrom = datePipe.transform(voucherBook.validFrom, 'yyyy-MM-dd');
    let voucherValidTo = datePipe.transform(voucherBook.validTo, 'yyyy-MM-dd');
    voucherBook.active == true ? voucherBook.active = "1" : voucherBook.active = "0";
    voucherBook.validFrom = voucherValidFrom;
    voucherBook.validTo = voucherValidTo;
    this.appValueForm.patchValue(voucherBook);
    this.updateFromData = voucherBook;
    console.log(this.updateFromData);
    this.modelText = "Update Voucher Book"
    this._modalService.open(content)
  }
  deleteVoucherBook(voucherBookId) {
    
    this.Loader.show();
    this.service.get(this.service.apiRoutes.PackagesAndPayments.deleteVoucherBook + "?voucherBookId=" + voucherBookId).subscribe(result => {
      var res = result.json();
      if (res.status == 200) {
        this.appValueForm.reset()
        this.toastr.success(res.message, "Success");
        this.getVoucherBookList();
      }
      else {
        this.toastr.error(res.message, "Error");
      }
      this.Loader.hide();
    })
  }
  showModal(content) {
    this.modelText = "Add New Voucher Book"
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
  public getVoucherType() {
    
    this.service.get(this.service.apiRoutes.PackagesAndPayments.getVoucherTypeList).subscribe(result => {
      
      this.VoucherTypeList = result.json();
      
      console.log(this.VoucherTypeList);
    })
  }

}
