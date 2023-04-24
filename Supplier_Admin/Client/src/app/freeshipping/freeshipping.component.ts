import { DatePipe } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { StatusCode } from '../Shared/Enums/common';
import { IFreeShippingList, IProductCategoryGroupList, IProductCategoryList, IProductSubCategory, ISelectedCategoryList } from '../Shared/Enums/Interface';
import { CommonService } from '../Shared/HttpClient/_http';

@Component({
  selector: 'app-freeshipping',
  templateUrl: './freeshipping.component.html',
  styleUrls: ['./freeshipping.component.css']
})
export class FreeshippingComponent implements OnInit {

  public isCategorySelected: boolean = true;
  public isCategoryGroupSelected: boolean = true;
  categoryList: IProductCategoryList[] = [];
  subCategoryList: IProductSubCategory[] = [];
  subCategoryGroupList: IProductCategoryGroupList[] = [];
  public freeShippingList: IFreeShippingList[] = [];
  public maxdate: string = "";
  public productId: number = 0;
  public appValForm: FormGroup;
  public userId: any = "";
  public freeShippingId: Number=0;
  selectedCategory: ISelectedCategoryList = { category: '', categoryId: null, subCategory: '', subCategoryId: null, categoryGroup: '', subCategoryGroupId: null };
  public filterFreeShipping: FormGroup;
  public submited: boolean = false;
  public pipe: DatePipe;
  public date: Date = new Date;
  public day: string = "";
  public month: string = "";
  public year: string = "";
  constructor(public formBuilder: FormBuilder, public service: CommonService, public _modalService: NgbModal, public toastr: ToastrService, public Loader: NgxSpinnerService) {
    this.pipe = {} as DatePipe;
    this.appValForm = {} as FormGroup;
    this.filterFreeShipping = {} as FormGroup;
  }


  ngOnInit(): void {
    this.appValForm = this.formBuilder.group({
      id:[0],
      categoryId: [0],
      subCategoryId: [0],
      categoryGroupId: [0],
      StartDate: [],
      EndDate: [],
      status: [null],
    });
    this.filterFreeShipping = this.formBuilder.group({
      categoryId: [null],
      subCategoryId: [null],
      categoryGroupId: [null],
      startDate: [null],
      endDate: [null],
      status: [1],
    });
    var decodedtoken = this.service.decodedToken();
    
    this.userId = decodedtoken.Id;

    this.getFreeShippingList();
    this.getCategoryList();
  }
  get f() {
    return this.appValForm.controls;
  }
  public getFreeShippingList() {
    var data = this.filterFreeShipping.value;
    
    let obj = {
      supplierId: this.userId,
      categoryId: parseInt(data.categoryId),
      subCategoryId: parseInt(data.subCategoryId),
      categoryGroupId: parseInt(data.categoryGroupId),
      startDate: data.startDate,
      endDate: data.endDate,
      active: parseInt(data.status)
    };
    
    this.service.PostData(this.service.apiUrls.Supplier.Product.GetFreeShippingList, JSON.stringify(obj)).then(result => {
      
      this.freeShippingList = result.resultData;
      console.log(this.freeShippingList);
      this.service.Loader.hide();
    });
  }
  resetSearchForm() {
    this.filterFreeShipping.reset();
  }
  getCategoryList() {
    this.Loader.show();
    this.service.get(this.service.apiUrls.Supplier.Product.GetActiveProducts).subscribe(res => {
      this.categoryList = <any>res.resultData;
      this.Loader.hide();
    });
  }

  onChangeCategory(categoryId: number) {
    this.Loader.show();
    this.service.GetData(this.service.apiUrls.Supplier.Product.GetProductSubCategoryById + "?productCatgoryId=" + categoryId).then(res => {
      this.subCategoryList = <any>res;
      //this.isSubCategoryGroup = false;
      //if (this.subCategoryList.length > 0) {
      //  this.isSubCategory = true;
      //  this.disableFlag = true;
      //}
      //else {
      //  this.isSubCategory = false;
      //  this.disableFlag = false;
      //}
      this.Loader.hide();
    });
  }

  onChangeSubCategory(subCategoryId: number) {
    this.Loader.show();
    this.service.get(this.service.apiUrls.Supplier.Product.GetProductCategoryGroupListById + "?subCategoryId=" + subCategoryId).subscribe(res => {
      this.subCategoryGroupList = (<any>res).resultData;
      //if (this.subCategoryGroupList.length > 0) {
      //  this.isSubCategoryGroup = true;
      //  this.disableFlag = true;
      //}
      //else {
      //  this.isSubCategoryGroup = false;
      //  this.disableFlag = false;
      //}
      this.Loader.hide();
    });
  }

  save() {
    
    this.submited = true;
    var data = this.appValForm.value;
    if (this.appValForm.valid) {
      let formData = {
        id: this.freeShippingId,
        supplierId: parseInt(this.userId),
        categoryId: parseInt(data.categoryId),
        subCategoryId: parseInt(data.subCategoryId),
        categoryGroupId: parseInt(data.categoryGroupId),
        starDate: data.StartDate,
        endDate: data.EndDate,
        active: parseInt(data.status)
      }
      console.log(formData);
      this.service.PostData(this.service.apiUrls.Supplier.Product.AddUpdateFreeShipping, JSON.stringify(formData)).then(x => {
        
        var data = x;
        if (data == "Data Saved") {
          this.toastr.success(data);
          this._modalService.dismissAll();
          this.getFreeShippingList();
        }
      });
    }
   
  }
  deletePromotion(deleteContent: TemplateRef<any>, id: number) {
    
    this.freeShippingId = id;
    this._modalService.open(deleteContent);
  }
  confirmDelete() {
    
    this.service.GetData(this.service.apiUrls.Supplier.Product.DeleteFreeShipping + "?freeShippingId=" + this.freeShippingId).then(data => {
      var result = data;
      if (result.status == StatusCode.OK) {
        this.toastr.success("Deleted Successfully", "Success");
        this._modalService.dismissAll();
        this.getFreeShippingList();
      }
    });
  }
  
  showModal(content: TemplateRef<any>) {

    this._modalService.open(content);
    this.resetForm();
  }
  GetPromotionById(content: TemplateRef<any>, data) {
    this.freeShippingId = data.freeShippingId;
    var datePipe = new DatePipe("en-US");
    data.starDate = datePipe.transform(data.starDate, 'yyyy-MM-dd');
    data.endDate = datePipe.transform(data.endDate, 'yyyy-MM-dd');
    this.appValForm.controls.StartDate.setValue(data.starDate);
    this.appValForm.controls.EndDate.setValue(data.endDate);
    this.onChangeCategory(data.categoryId);
    this.onChangeSubCategory(data.subCategoryId);
    this._modalService.open(content);
    this.appValForm.patchValue(data);
    this.appValForm.controls.status.setValue((data.status? 1 : 0));
  }
  resetForm() {
    this.appValForm.reset();
  //  this.appValForm.value.promotionTypeId.value = null;
  }
}
