import { DatePipe } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { StatusCode } from '../../Shared/Enums/common';
import { IProductCategory, IProductSubCategory, Ipromotion, IResponse, ISupplierPromotions } from '../../Shared/Enums/Interface';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-promotions',
  templateUrl: './promotions.component.html',
  styleUrls: ['./promotions.component.css']
})
export class PromotionsComponent implements OnInit {

  public promotionList :ISupplierPromotions []=[];
  public promotionTypeList :IProductCategory []=[];
  public categoryList :IProductCategory []=[];
  public subCategoryList : IProductSubCategory[]=[];
  public categoryGroupList :IProductCategory []=[];

  public appValForm: FormGroup;
  public filterPromoForm: FormGroup; 
  public submited: boolean = false;
  public isCategorySelected = false;
  public isCategoryGroupSelected = false;
  public promotion: Ipromotion;
  public promotionId: number = 0;
  public amountMaxLength: number = 5;
  public pipe: DatePipe;
  public date: Date = new Date;
  public day: string="";
  public month: string="";
  public year: string="";
  public maxdate: string="";
  public userId: any="";
  response: IResponse;
  constructor(public formBuilder: FormBuilder, public service: CommonService, public _modalService: NgbModal, public toastr: ToastrService) {
    this.pipe = {} as DatePipe;
    this.promotion = {} as Ipromotion;
    this.appValForm = {} as FormGroup;
    this.filterPromoForm = {} as FormGroup;
  }


  ngOnInit(): void {
    this.getMaxDate();
    this.appValForm = this.formBuilder.group({
      promotionId: [0],
      promotionTypeId: [0, [Validators.required]],
      categoryId: [0, [Validators.required]],
      subCategoryId: [0],
      categoryGroupId: [0],
      promotionName: ['', [Validators.required]],
      promotionStartDate: ['', [Validators.required]],
      promotionEndDate: ['', [Validators.required]],
      entityStatus: [null, [Validators.required]],
      amountType: [null, [Validators.required]],
      isActive: [false],
      amount: [0, [Validators.required]],
    });
    this.filterPromoForm = this.formBuilder.group({
      categoryId: [null],
      fpromotionName: [''],
      entity: [null],
    });
    var decodedtoken = this.service.decodedToken();
    this.userId = decodedtoken.Id;
    
    this.getPromotionList();
    this.GetCategories();
    this.GetPromotionTypes();
  }
  get f() {
    return this.appValForm.controls;
  }
  public getPromotionList() {
    var data = this.filterPromoForm.value;
    let obj = {
      promotionName: data.fpromotionName,
      categoryId: data.categoryId,
      PromotionIdTypeId: data.fpromotionIdTypeId,
      entityStatus: data.entity,
      supplierId: this.userId
    };
    this.service.PostData(this.service.apiUrls.Supplier.PackagesAndPayments.GetAllPromotionsForSupplier, obj).then(result => {
      this.promotionList = result.resultData;
      console.log(this.promotionList);
      this.service.Loader.hide();
    });
  }
  resetSearchForm() {
    this.filterPromoForm.reset();
  }
  GetPromotionById(content: TemplateRef<any>, promotionId:number) {

    this.service.Loader.show();
    this.service.GetData(this.service.apiUrls.Supplier.PackagesAndPayments.GetPromotionByIdForSupplier + promotionId).then(data => {
      if (data.status == StatusCode.OK) {
        this.promotion = data.resultData;

        var datePipe = new DatePipe("en-US");
        this.promotion.promotionStartDate = datePipe.transform(this.promotion.promoStartDate, 'yyyy-MM-dd');
        this.promotion.promotionEndDate = datePipe.transform(this.promotion.promotionEndDate, 'yyyy-MM-dd');
        this.appValForm.patchValue(this.promotion);
        if (this.categoryList.length == 0)
          this.GetCategories();
        this.onChangeCategory(this.promotion.categoryId);
        this.onChangeSubCategory(this.promotion.subCategoryId)

        this._modalService.open(content);
        this.service.Loader.hide();
      }
    });
  }
  save() {

    this.submited = true;
    this.promotion = this.appValForm.value;
    if (this.appValForm.invalid) {
      return;
    }
    if (this.promotion.promotionId == null)
      this.promotion.promotionId = 0;
    if (this.promotion.promotionId == 0) {
      this.promotion.createdBy = this.userId;
    }
    else {
      this.promotion.updatedBy = this.userId;
    }

    this.promotion.isActive = true;
    this.service.PostData(this.service.apiUrls.Supplier.PackagesAndPayments.AddPromotionForSupplier, JSON.stringify(this.promotion)).then(data => {
      var result = data;
      if (result.status == StatusCode.OK) {
        this.toastr.success("Saved Successfully", "Success");
        this._modalService.dismissAll();
        this.getPromotionList();
      }
    });
  }
  deletePromotion(deleteContent: TemplateRef<any>, promotionId:number) {
    this.promotionId = promotionId;
    this._modalService.open(deleteContent);
  }
  confirmDelete() {

    this.service.GetData(this.service.apiUrls.Supplier.PackagesAndPayments.DeletePromotionForSupplier + this.promotionId).then(data => {
      var result = data;
      if (result.status == StatusCode.OK) {
        this.toastr.success("Deleted Successfully", "Success");
        this._modalService.dismissAll();
        this.getPromotionList();
      }
    });
  }
  GetCategories() {

    this.service.Loader.show();
    this.service.GetData(this.service.apiUrls.Supplier.Product.GetProductCategories).then(data => {
      if (data.status == StatusCode.OK) {
        this.categoryList = data.resultData;
        //this.appValForm.patchValue(this.categoryList);
        this.service.Loader.hide();
      }
      this.service.Loader.hide();
    });
  }
  onChangeCategory(categoryId:number) {
    this.service.Loader.show();
    this.isCategorySelected = true;
    this.service.GetData(this.service.apiUrls.Supplier.Product.GetProductSubCategoryById + `?productCatgoryId=${categoryId}`).then(data => {
      this.subCategoryList = data;

      let that = this;
      setTimeout(function () {
        that.appValForm.get('subCategoryId')?.setValue(that.promotion.subCategoryId, {
          onlySelf: true
        })
      }, 1000);

      this.service.Loader.hide();
    });
  }
  onChangeSubCategory(subCategoryId:number) {
    this.service.Loader.show();
    this.isCategoryGroupSelected = true;
    this.service.GetData(this.service.apiUrls.Supplier.Product.GetCategoryGroupsById + `?subCategoryId=${subCategoryId}`).then(data => {
      if (data.status == StatusCode.OK) {
        this.categoryGroupList = data.resultData;
        let that = this;
        setTimeout(function () {
          that.appValForm.get('categoryGroupId')?.setValue(that.promotion.categoryGroupId, {
            onlySelf: true
          })
        }, 1000);

      }
      this.service.Loader.hide();
    });
  }
  onChangePromotion(e: number) {
    this.GetCategories();
  }
  onChangeAmountType(e: number) {
    if (e == 2) {
      this.appValForm.get('amount')?.setValue('');
      this.amountMaxLength = 2
    }
    else
      this.amountMaxLength = 5
  }
  GetPromotionTypes() {
    this.service.Loader.show();
    this.service.GetData(this.service.apiUrls.Supplier.PackagesAndPayments.GetPromotionTypesListForSupplier + "?supplierId=" + this.userId).then(data => {
      if (data.status == StatusCode.OK) {
        
        this.promotionTypeList = data.resultData;
       // this.promotionTypeList = this.promotionTypeList.filter(x=> x.);
        this.service.Loader.hide();
      }
    });
  }
  showModal(content: TemplateRef<any>) {
    
    this._modalService.open(content);
    this.resetForm();
  }
  resetForm() {
    this.appValForm.reset();
    this.appValForm.value.promotionTypeId.value = null;
    this.appValForm.value.categoryId.value = null;
  }
  //---------------------- Get Maximum Promotion Date
  public getMaxDate() {
    this.pipe = new DatePipe('en-US');
    this.date = new Date();
    this.month = (this.date.getMonth() + 1).toString();
    if (this.date.getMonth() == 12) {
      this.month = '01';
    }
    this.day = this.date.getDate().toString();
    this.year = this.date.getFullYear().toString();
    if (this.month.length == 1) {
      this.month = '0' + this.month;
    }
    if (this.day.length == 1) {
      this.day = '0' + this.day;
    }
    this.maxdate = this.year + "-" + this.month + "-" + this.day;
  }
}
