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
  selector: 'app-voucherbookleaves',
  templateUrl: './voucherbookleaves.component.html',
  styleUrls: ['./voucherbookleaves.component.css']
})
export class VoucherbookleavesComponent implements OnInit {
  public promoForm: FormGroup;
  public filterPromoForm: FormGroup;
  public promoTypeList = [];
  public promoTypeList1 = [];
  public voucherBookLeavesList = [];
  public updateFromData = {};
  public modelText: string = "";
  public promotiontypeon = '';
  public submited: boolean = false;
  public disabledfields;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    


  jwtHelperService: JwtHelperService = new JwtHelperService();

  constructor(public router: Router,public _modalService: NgbModal, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Voucher Book Leaves"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.promFormData();
  //  this.getVoucherBookList();
    this.getVoucherBookLeavesList();
  }
  public promFormData() {
    this.filterPromoForm = this.fb.group({
      fpromotionIdTypeId: [null],
      fpromotionCode: [''],
      fpromotionName: [''],
      fpromoStartDate: [''],
      fpromotionEndDate: [''],
      entity: ['1'],
    })
    this.promoForm = this.fb.group({
      referralId: [0],
      userTypeId: [0, [Validators.required]],
      referralStartDate: ['', [Validators.required]],
      referralEndDate: ['', [Validators.required]],
      referralLimit: [''],
      entityStatus: [0, [Validators.required]],
      amount: [0],

    })
  }
  filterPromotions() {
    console.warn(this.filterPromoForm.value);
  }

  getVoucherBookLeavesList() {
    
    var data = this.filterPromoForm.value;
    let obj = {
      //promotionName: data.fpromotionName,
      //promotionCode: data.fpromotionCode,
      //PromoStartDate: data.fpromoStartDate,
      //PromotionEndDate: data.fpromotionEndDate,
      //PromotionIdTypeId: data.fpromotionIdTypeId,
      //entityStatus: data.entity
    };
    this.Loader.show()
    this.service.post(this.service.apiRoutes.PackagesAndPayments.getVoucherBookLeavesList, obj).subscribe(result => {
      this.voucherBookLeavesList = result.json();
      ;
      console.log(this.voucherBookLeavesList);
      console.log(result);
      this.Loader.hide();
    })
  }
  saveAndUpdate() {
    
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    let formData;
    if (this.promoForm.invalid) {
      this.submited = true;
      return;
    }
    //add
    if (this.promoForm.value.promotionId <= 0) {
      formData = this.promoForm.value;
      formData.createdBy = decodedtoken.UserId;
      formData.promotionId = 0;
      formData.isActive = true;
      console.log(formData);
      this.Loader.show();
      this.service.post(this.service.apiRoutes.PackagesAndPayments.AddOrUpdatePromotions, formData).subscribe(result => {
        var res = result.json();
        if (res.status == 200) {
          this.promoForm.reset()
          this.toastr.success(res.message, "Success");
          this.getVoucherBookLeavesList();
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
      formData = this.promoForm.value;
      formData.updatedBy = decodedtoken.UserId;
      console.log(formData);
      this.Loader.show();
      this.service.post(this.service.apiRoutes.PackagesAndPayments.AddOrUpdatePromotions, formData).subscribe(result => {
        var res = result.json();
        if (res.status == 200) {
          this.promoForm.reset()
          this.toastr.success(res.message, "Success");
          this.getVoucherBookLeavesList();
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
  updatePromo(promo, content) {
    
    var datePipe = new DatePipe("en-US");
    let promoStartdate = datePipe.transform(promo.promoStartDate, 'yyyy-MM-dd');
    let promoEndDate = datePipe.transform(promo.promotionEndDate, 'yyyy-MM-dd');
    promo.promoStartDate = promoStartdate;
    promo.promotionEndDate = promoEndDate;
    this.promoForm.patchValue(promo);
    this.updateFromData = promo;
    console.log(this.updateFromData);
    this.modelText = "Update Voucher Book Leaves"
    this._modalService.open(content)
  }
  deletePromo(promoId) {
    
    this.Loader.show();
    this.service.post(this.service.apiRoutes.PackagesAndPayments.DeletePromotion, promoId).subscribe(result => {
      var res = result.json();
      if (res.status == 200) {
        this.promoForm.reset()
        this.toastr.success(res.message, "Success");
        this.getVoucherBookLeavesList();
      }
      else {
        this.toastr.error(res.message, "Error");
      }
      this.Loader.hide();
    })
  }
  showModal(content) {
    this.modelText = "Add New Voucher Book Leaves"
    this.promoForm.reset();
    this._modalService.open(content)
  }

  resetForm(): void {
    this.promoForm.reset();
  }
  resetFForm() {
    
    this.filterPromoForm.reset();
  }
  get f() {
    return this.promoForm.controls;
  }
  public getVoucherBookList() {
    
    this.service.get(this.service.apiRoutes.PackagesAndPayments.GetVoucherBookList).subscribe(result => {
      
      this.promoTypeList = result.json();
      
      console.log(this.promoTypeList);
    })
  }

}
