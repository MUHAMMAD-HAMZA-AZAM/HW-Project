import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '../../Shared/HttpClient/_http';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";
import { JwtHelperService } from '@auth0/angular-jwt/src/jwthelper.service';
import { DatePipe } from '@angular/common';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';

@Component({
  selector: 'app-promotions',
  templateUrl: './promotions.component.html',
  styleUrls: ['./promotions.component.css']
})
export class PromotionsComponent implements OnInit {
  public promoForm: FormGroup;
  public filterPromoForm : FormGroup;
  public promoTypeList = [];
  public promoTypeList1 = [];
  public promotionList = [];
  public categoryList = [];
  public updateFromData = {};
  public modelText: string = "";
  public promotiontypeon = '';
  public submited: boolean = false;
  public disabledfields;
  public isCategory: boolean = false;
  public isPromotionCode: boolean = false;
 
  jwtHelperService: JwtHelperService = new JwtHelperService();
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    

  constructor(public router: Router,public _modalService: NgbModal, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Promotions"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.promFormData();
    this.getPromoTypes();
    this.getPromotionsList();
    this.getTradesmanCategory();
  }
  public promFormData() {
    this.filterPromoForm = this.fb.group({
      fpromotionIdTypeId: [null],
      fpromotionCode: [''],
      fpromotionName: [''],
      fpromoStartDate: [''],
      fpromotionEndDate: [''],
      entity: ['1'],
    });
    this.promoForm = this.fb.group({
      promotionId: [0],
      promotionIdTypeId: [0, [Validators.required]],
      promotionCode: ['', [Validators.required]],
      promotionName: ['', [Validators.required]],
      promoStartDate: ['', [Validators.required]],
      promotionEndDate: ['', [Validators.required]],
      categoryId: [0, [Validators.required]],
      //discountDays: ['0'],
      //discountCategories: ['0'],
      entityStatus: [0, [Validators.required]],
      //discountJobsApplied: ['0'],
      //discountPercentPrice :['0'],
      isActive: [false],
      amount: [0, [Validators.required]],
      PermotionsForAll: ['0'],
      PermotionsForOld: ['0'],
      PermotionsForNew: ['0']
    });


  }
  filterPromotions() {
    console.warn(this.filterPromoForm.value);
  }
  getPromotionsList() {
    var data = this.filterPromoForm.value;
    let obj = {
      promotionName: data.fpromotionName,
      promotionCode: data.fpromotionCode,
      PromoStartDate: data.fpromoStartDate,
      PromotionEndDate: data.fpromotionEndDate,
      PromotionIdTypeId: data.fpromotionIdTypeId,
      entityStatus:data.entity
    };
    this.Loader.show()
    this.service.post(this.service.apiRoutes.PackagesAndPayments.GetPromotionList, obj).subscribe(result => {
      ;
      this.promotionList = result.json();
      console.log(this.promotionList);
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
    ;
    if (this.promoForm.value.promotionId <= 0) {
      formData = this.promoForm.value;
      formData.createdBy = decodedtoken.UserId;
      formData.promotionId = 0;
      formData.isActive = true;
      console.log(formData);
      this.Loader.show();
      ;
      this.service.post(this.service.apiRoutes.PackagesAndPayments.AddOrUpdatePromotions, formData).subscribe(result => {
        var res = result.json();
        ;
        if (res.status == 200) {
          this.promoForm.reset()
          this.toastr.success(res.message, "Success");
          this.getPromotionsList();
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
      formData = this.promoForm.value;
      ;
      
      formData.updatedBy = decodedtoken.UserId;
      console.log(formData);
      this.Loader.show();
      ;
      this.service.post(this.service.apiRoutes.PackagesAndPayments.AddOrUpdatePromotions, formData).subscribe(result => {
        var res = result.json();
        ;
        if (res.status == 200) {
          this.promoForm.reset()
          this.toastr.success(res.message, "Success");
          this.getPromotionsList();
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

    if (promo.promotionIdTypeId == 2) {
      this.isCategory = true;
      this.isPromotionCode = true;
      this.promoForm.controls.promotionCode.clearValidators();
      this.promoForm.controls.promotionCode.updateValueAndValidity();
    }
    else {
      this.isCategory = false;
      this.isPromotionCode = false;
      this.promoForm.controls.categoryId.clearValidators();
      this.promoForm.controls.categoryId.updateValueAndValidity();
    }
    ;
    var datePipe = new DatePipe("en-US");
    let promoStartdate = datePipe.transform(promo.promoStartDate, 'yyyy-MM-dd');
    let promoEndDate = datePipe.transform(promo.promotionEndDate, 'yyyy-MM-dd');
    promo.promoStartDate = promoStartdate;
    promo.promotionEndDate = promoEndDate;
    this.promoForm.patchValue(promo);
    this.updateFromData = promo;
    console.log(this.updateFromData);
    this.modelText = "Update Promotion"
    this._modalService.open(content)
    
  }
  deletePromo(promoId) {
    
    this.Loader.show();
    this.service.post(this.service.apiRoutes.PackagesAndPayments.DeletePromotion, promoId).subscribe(result => {
      var res = result.json();
      if (res.status == 200) {
        this.promoForm.reset()
        this.toastr.success(res.message, "Success");
        this.getPromotionsList();
      }
      else {
        this.toastr.error(res.message, "Error");
      }
      this.Loader.hide();
    })
  }
  showModal(content) {
    this.modelText = "Add New Promotion"
    this.promoForm.reset();
    this._modalService.open(content)
  }

  resetForm() : void {
    this.promoForm.reset();
  }
  resetFForm() {   
    this.filterPromoForm.reset();
  }
  get f() {
    return this.promoForm.controls;
  }
  public getPromoTypes() {
    
    this.service.get(this.service.apiRoutes.PackagesAndPayments.GetPromoTypesList).subscribe(result => {
      
      this.promoTypeList = result.json();
      
      console.log(this.promoTypeList);
    })
  }
  public getTradesmanCategory() {
    this.service.get(this.service.apiRoutes.TrdesMan.GetTradesManSkills).subscribe(result => {
      this.categoryList = result.json();
    })
  }
  public makeid(length) {
    var result = '';
    var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    var charactersLength = characters.length;
    for (var i = 0; i < length; i++) {
      result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    return result;
  }

  promotionType(obj) {
    ;
    var id = obj.target.value;
    if (id == "2") {
      this.isCategory = true;
      this.isPromotionCode = true;
      this.promoForm.controls.promotionCode.clearValidators();
      this.promoForm.controls.promotionCode.updateValueAndValidity();
    }
    else {
      this.isCategory = false;
      this.isPromotionCode = false;
      this.promoForm.controls.categoryId.clearValidators();
      this.promoForm.controls.categoryId.updateValueAndValidity();
    }

  }
 
}
