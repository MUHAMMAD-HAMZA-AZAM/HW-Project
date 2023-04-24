import { HttpStatusCode } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { UploadFileService } from '../../Shared/HttpClient/upload-file.service';
import { CommonService } from '../../Shared/HttpClient/_http';
import { delay } from 'rxjs/operators';
import { IProductCategoryList,  IProductSubCategory, iQuerypram, ISelectedCategoryList, ISubcategoryGroupList } from '../../Shared/Enums/Interface';
@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  jwtHelperService: JwtHelperService = new JwtHelperService();
  filterForm: FormGroup
  productList: any = [];
  products: any = [];
  categoryId: string | null = null;
  subCategoryId: string | null = null;
  categoryGroupId: string | null = null;
  //groupArr = [];
  public pageNumber:number = 1;
  public pageSize :number= 18;
  public noOfRecords:number = 0;
  public obj: iQuerypram;
  productsList: IProductCategoryList[] = [];
  productId:number= 0;
  productName:string | null = null;
  price:number= 0;
  toPrice:number= 0;
  subProductsList: IProductSubCategory[] = [];
  subCategoryGroupList: ISubcategoryGroupList[] = [];
  isSubCategory: boolean = false;
  isSubCategoryGroup: boolean = false;
  disableFlag: boolean = true;
  selectedIndex: number = 0;
  selectedSubIndex: number = 0;
  selectedSubGroupIndex: number = 0;
  selectedCategory: ISelectedCategoryList = { category: '', categoryId: null, subCategory: '', subCategoryId: null, categoryGroup: '', subCategoryGroupId: null };
  public skeltonArr: any;
  public imageLoader: boolean = false;
  constructor(public _toastr: ToastrService, public fb: FormBuilder, public service: CommonService, public _fileService: UploadFileService, public Loader: NgxSpinnerService, private route: ActivatedRoute) {
    this.filterForm = {} as FormGroup;
    this.obj = {} as iQuerypram;
    this.skeltonArr = {
      productLength: Array<number>(18)
    }
  }

  ngOnInit(): void {
    var data=localStorage.getItem('auth_token');
    var decodedToken = data!=null ? this.jwtHelperService.decodeToken(data):"";
    this.route.queryParams.subscribe((param: Params) => {
      this.obj = {
        categoryId: param['categoryId'],
        subCategoryId: param['subCategoryId'],
        categoryGroupId: param['categoryGroupId'], 
        supplierId: decodedToken.Id,
        pageNumber: this.pageNumber,
        pageSize: this.pageSize,
        id: this.productId,
        name: this.productName,
        price: this.price,
        toPrice: this.toPrice
      };

      this.getProductList();
      this.getSupplierProductList(this.obj);
      this.SearchProductForm();
    });
  }

  SearchProductForm() {
    this.filterForm = this.fb.group({
      productId: [null],
      productName: [null],
      price: [null],
      toPrice: [null],
      categoryId: [null],
      subCategoryId: [null],
      subCategoryGroupId: [null],
    })
  }

  getPostByPage(page:number) {
    this.obj.pageNumber = page;
    this.obj.pageSize = this.pageSize;
    this.getSupplierProductList(this.obj);
  }

  getSupplierProductList(obj: iQuerypram) {
    this.service.post(this.service.apiUrls.Supplier.Product.GetSupplierProductList, obj).pipe(delay(2000)).subscribe(response => {
      this.productList = (<any>response).resultData;
      if ((<any>response).status == HttpStatusCode.Ok) {
        this.noOfRecords = this.productList[0].noOfRecords;
        this.imageLoader = true;
      }
      else {
        this.noOfRecords = 0;
        this.imageLoader = true;
      }
    })
        
      
  }

  getProductList() {
    //this.Loader.show();
    this.service.get(this.service.apiUrls.Supplier.Product.GetActiveProducts).subscribe(res => {
      this.productsList = <any>res.resultData;
     // this.Loader.hide();
    });
  }
  getSubProductList(productId: Event  ) {
   // this.Loader.show();
    
    //this.selectedCategory.categoryId = productId.target.value;
    this.selectedCategory.categoryId = (<HTMLInputElement>productId.target).value;
    this.selectedCategory.category = (<HTMLInputElement>productId.target).textContent
    this.service.get(this.service.apiUrls.Supplier.Product.GetProductSubCategoryById + `?productCatgoryId=${this.selectedCategory.categoryId}`).subscribe(res => {
      this.subProductsList = <any>res;
      this.isSubCategoryGroup = false;
      if (this.subProductsList.length > 0) {
        this.isSubCategory = true;
        this.disableFlag = true;
      }
      else {
        this.isSubCategory = false;
        this.disableFlag = false;
      }
     // this.Loader.hide();
    });
  }
  getProductCategoryGroupList(subCategoryId: Event) {
    
    //this.Loader.show();
    this.selectedCategory.subCategoryId = (<HTMLInputElement>subCategoryId.target).value;
    this.selectedCategory.subCategory = ' / ' + (<HTMLInputElement>subCategoryId.target).textContent;

    this.service.get(this.service.apiUrls.Supplier.Product.GetProductCategoryGroupListById + `?subCategoryId=${this.selectedCategory.subCategoryId}`).subscribe(res => {
      this.subCategoryGroupList = (<any>res).resultData;
      if (this.subCategoryGroupList.length > 0) {
        this.isSubCategoryGroup = true;
        this.disableFlag = true;
      }
      else {
        this.isSubCategoryGroup = false;
        this.disableFlag = false;
      }
    //  this.Loader.hide();
    });
  }

  getSubCategoryGroup(subCategoryGroupId: Event) {
    this.selectedCategory.subCategoryGroupId = (<HTMLInputElement>subCategoryGroupId.target).value;
    this.selectedCategory.categoryGroup = ' / ' + (<HTMLInputElement>subCategoryGroupId.target).textContent;
  }
  setSubGroupIndex(index: number) {
    this.selectedSubGroupIndex = index;
  }
  setIndex(index: number) {
    this.selectedCategory.subCategoryId = '0';
    this.selectedCategory.subCategoryGroupId = '0';
    this.selectedIndex = index;
    this.selectedSubIndex = -1;
  }
  setSubIndex(index: number) {
    this.selectedCategory.subCategoryGroupId = '0';
    this.selectedSubIndex = index;
    this.selectedSubGroupIndex = -1;
  }
  filter() {
    this.filterForm.patchValue({
      categoryId: this.selectedCategory.categoryId,
      subCategoryId: this.selectedCategory.subCategoryId,
      subCategoryGroupId: this.selectedCategory.subCategoryGroupId,
    })

    this.filterForm.value.productId=0;
    this.obj.categoryId = this.filterForm.value?.categoryId;
    this.obj.subCategoryId = this.filterForm.value?.subCategoryId;
    this.obj.categoryGroupId = this.filterForm.value?.subCategoryGroupId;
    this.obj.id = this.filterForm.value?.productId;
    this.obj.name = this.filterForm.value.productName;
    this.obj.price = this.filterForm.value.price;
    this.obj.toPrice = this.filterForm.value.toPrice;
    this.getSupplierProductList(this.obj);
  }
  reset() {
    this.obj.categoryId = 0;
    this.obj.subCategoryId = 0;
    this.obj.categoryGroupId = 0;
    this.obj.name=null;
    this.obj.price = 0;
    this.obj.toPrice = 0;
    this.filterForm.reset();
    this.getSupplierProductList(this.obj);
  }
}
