import { Component, OnInit, ViewChild, ElementRef, Inject } from '@angular/core';
import { CommonService } from '../../shared/HttpClient/_http';
import { SortedType } from '../../shared/Enums/enums';
import { ResponseVm, AdsParameterVM } from '../../models/commonModels/commonModels';
import { ActivatedRoute, Params } from '@angular/router';
import { isNullOrUndefined } from 'util';
import { FormGroup, FormBuilder } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgxSpinnerService } from 'ngx-spinner';
import { ICategoryDropDownSettings, IDropDownListCat, IDropDownListForProduct, IGetMarkeetPlaceProducts, IIdValue, IImage, IProductCategory } from '../../shared/Enums/Interface';
//import { debug } from 'console';
//import { type } from 'os';


@Component({
  selector: 'app-market-place-index',
  templateUrl: './market-place-index.component.html',
  styleUrls: ['./market-place-index.component.css']
})
export class MarketPlaceIndexComponent implements OnInit {
  public pageWrapper: string="";
  public supplierCatList: IGetMarkeetPlaceProducts[] = [];
  public noRecordFound: boolean = false;
  //public supplierCatList2 = [];
  public intArray: any[] = [];
  //public supplierCatImageList = [];
  public image: IImage;
  public pageNumber: number = 1;
  public pageSize: number = 21;
  public NumberOfPages: number[] = [];
  public TotalPages: number[] = [];
  public totalProudcts: number=0;
  public firstPage: any;
  public lastPage: any;
  public pageInc = 10;
  public hidePagination = false;
  public noDataFound = false;
  dropdownList: IDropDownListCat[] = [];
  //selectedItems = [];
  public searchItem: number=0;
  public subCategory: IIdValue[] = [];
  selectedPriceItems: IDropDownListCat;
  CategorydropdownSettings: ICategoryDropDownSettings;
  FilterdropdownSettings: ICategoryDropDownSettings;
  //public categoryId = [];
  //public result: any;
  public selectedItem: string="";
  public showmessage: boolean = false;
  public supplierAd: any;
  public norecordfound: boolean=false;
  //public Categories: [];
  public adsParameterVM: AdsParameterVM = {} as  AdsParameterVM;
  public imageObject_market: Array<object>;
  //public supplierInfo = [];
  //public supplierProfileSlider = [];
  public productId: number=0;
  public productObj: IIdValue;
  public productSubCatId: string|null="";
  keyword: string = 'value';
  public productList: IProductCategory[] = [];
  public selectedProducts: IIdValue[] = [];
  //public selectedColumn = [];
  public dropdownListForProduct: IDropDownListForProduct;
  public filterForm: FormGroup;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(public fb: FormBuilder, public common: CommonService, private route: ActivatedRoute, public Loader: NgxSpinnerService) {
    this.route.queryParamMap.subscribe(queryParams => {
      this.productObj = { id: Number(queryParams.get('id')), value: queryParams.get('value') };
    });
    this.route.queryParamMap.subscribe(queryParams => {
      this.productSubCatId = queryParams.get("subCatId");
    });
    this.image = {} as IImage;
    this.selectedPriceItems = {} as IDropDownListCat;
    this.CategorydropdownSettings = {} as  ICategoryDropDownSettings;
    this.FilterdropdownSettings = {} as ICategoryDropDownSettings;
    this.imageObject_market = {} as Array<object>;
    this.productObj = {} as IIdValue;
    this.dropdownListForProduct = {} as IDropDownListForProduct;
    this.filterForm = {} as FormGroup;
  }

  ngOnInit() {
    this.filterForm = this.fb.group({
      products: [0],
      sortBy: ['']
    })
    this.GetSupplierCategoryList();
    this.dropdownListForProduct = {
      singleSelection: false,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: true,
      itemsShowLimit: 1,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      closeDropDownOnSelection: true,
      enableCheckAll: true
    };

    this.common.IsUserLogIn();
    this.IsUserLogIn();
    this.dropdownList = [
      { Value: 1, Name: 'Lowest Price' },
      { Value: 2, Name: 'Highest Price' },
      //{ Value: 3, Name: 'Saved' },
    ];
    this.CategorydropdownSettings = {
      singleSelection: true,
      idField: 'id',
      textField: 'value',
      itemsShowLimit: 1,
      enableCheckAll: false,
      closeDropDownOnSelection: true
    };
    this.FilterdropdownSettings = {
      singleSelection: true,
      idField: 'Value',
      textField: 'Name',
      closeDropDownOnSelection: true
    };
    //if (isNullOrUndefined(this.productId)) {
    //  this.intArray = [];
    //}
    //else {
    //  this.intArray.push(parseInt(this.productId));
    //}
    //this.selectedItem = null;
    //if (isNullOrUndefined(this.productSubCatId)) {
    //  this.searchItem = 0;
    //}
    //else {
    //  this.searchItem = parseInt(this.productSubCatId);
    //}
    //this.getSubCategoryList();
    //this.getSupplierCatList(this.selectedItem, this.intArray, this.searchItem);
  }
  getSubCategoryList() {   
    this.common.GetData(this.common.apiRoutes.Supplier.AllSubCategory, false).then(result => {
      this.subCategory = result ;
    });
  }
  filterMarketPlaceAds() {  
    this.Loader.show();
    var token = localStorage.getItem("auth_token");
    var decodedtoken =token!=null ? this.jwtHelperService.decodeToken(token):"";
    this.pageNumber = 1; 
    let formData = this.filterForm.value.products;
    let catIds = []; 
    for (var i = 0; i < formData.length; i++) {
      catIds.push(formData[i].id)
    }
    let obj = {
      catIds: catIds.toString(),
      sortBy: this.filterForm.value.sortBy,
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      userId: decodedtoken == null ? 0 : decodedtoken.Id,
    }
    this.common.PostData(this.common.apiRoutes.Supplier.GetMarketPlaceAds, obj, true).then(result => {
      let res: IGetMarkeetPlaceProducts[] = result ;
      if (res.length > 0) {
        this.supplierCatList = res;
        this.totalProudcts = this.supplierCatList[0].totalProducts;
        this.noRecordFound = false;
        this.Loader.hide(); 
      }
      else {
        this.Loader.hide(); 
        this.noRecordFound = true;
        this.supplierCatList = [];
      }
    });

    }
    getPostByPage(page: number) {
    var token = localStorage.getItem("auth_token");
    var decodedtoken = token!=null ? this.jwtHelperService.decodeToken(token):"";
    let formData = this.filterForm.value.products;
    let catIds = [];
    for (var i = 0; i < formData.length; i++) {
      catIds.push(formData[i].id)
    }
    let obj = {
      catIds: catIds.toString(),
      sortBy: this.filterForm.value.sortBy,
      pageNumber: page,
      pageSize: this.pageSize,
      userId: decodedtoken == null ? 0 : decodedtoken.Id,
    }
    this.common.PostData(this.common.apiRoutes.Supplier.GetMarketPlaceAds, obj, false).then(result => {
      this.supplierCatList = [];
      this.supplierCatList = result ;
      this.totalProudcts = this.supplierCatList[0].totalProducts;
    });
  }
  GetSupplierCategoryList() {
    this.common.GetData(this.common.apiRoutes.Supplier.GetAllProductCategory, false).then(result => {
      this.productList = result ;
      if (this.productObj != null && this.productObj.id > 0) {
        this.selectedProducts.push(this.productObj);
        this.filterForm.controls['products'].setValue(this.selectedProducts);
      }
      this.filterMarketPlaceAds();
    });
  }

  public getSupplierCatList(ids: string, selectedCategory: number[], searchItem: number) {
    
    this.adsParameterVM.pageNumber = this.pageNumber;
    this.adsParameterVM.pageSize = this.pageSize;
    this.adsParameterVM.productCategoryIds = selectedCategory;
    this.adsParameterVM.sortBy = ids;
    this.adsParameterVM.subCategoryId = searchItem;
    
    this.common.PostData(this.common.apiRoutes.Supplier.GetMarkeetPlaceProducts, this.adsParameterVM, false).then(result => {
      this.supplierCatList = result ;
      console.log(this.supplierCatList);
      this.totalProudcts = this.supplierCatList[0].totalProducts;
      if (this.supplierCatList != null) {
        this.supplierCatList.forEach(value => {
          this.common.GetData(this.common.apiRoutes.Supplier.GetMarkeetPlaceProductsImages + "?adImageId=" + value.adImageId, false).then(result => {
            this.image = result ;
            value.thumbImageContent = this.image.thumbImageContent;
          });
        });
      }
    })
  }
  //getPostByPage(page) {
  //  console.log(page);
  //  this.adsParameterVM.pageNumber = page;
  //  this.adsParameterVM.pageSize = this.pageSize;
  //  this.common.PostData(this.common.apiRoutes.Supplier.GetMarkeetPlaceProducts, this.adsParameterVM, false).then(result => {
  //    this.supplierCatList = result ;
  //    this.totalProudcts = this.supplierCatList[0].totalProducts;
  //    if (this.supplierCatList != null) {
  //      this.supplierCatList.forEach(value => {
  //        this.common.GetData(this.common.apiRoutes.Supplier.GetMarkeetPlaceProductsImages + "?adImageId=" + value.adImageId, false).then(result => {
  //          this.image = result ;
  //          value.thumbImageContent = this.image.thumbImageContent;

  //        });
  //      });
  //    }
  //  });
  //}
  public goToPage(e: any) {
    var evt = e.target.outerText;
    const pn = parseInt(evt);
    this.pageNumber = pn;
    this.getSupplierCatList(this.selectedItem, this.intArray, this.searchItem);
  }
  public nextPage(e: any) {
    

    if (this.pageNumber >= 5) {
      var lastItem = this.NumberOfPages[this.NumberOfPages.length - 1];
      var findIndex = this.NumberOfPages.indexOf(this.lastPage);
      if (findIndex <= -1) {
        this.NumberOfPages.splice(0, 1);
        this.NumberOfPages.push(lastItem + 1);
      }
    }
    var evt = e.target.outerText;
    const pn = parseInt(evt);
    this.pageNumber = this.pageNumber + 1;
    console.log(this.NumberOfPages);
    this.getSupplierCatList(this.selectedItem, this.intArray, this.searchItem);
  }
  public prevPage(e: any) {
    
    if (this.pageNumber > 5) {
      var fv = this.NumberOfPages[0];
      this.NumberOfPages.unshift(fv - 1);
      this.NumberOfPages.splice(-1, 1);
      console.log(this.NumberOfPages);
    }
    var evt = e.target.outerText;
    const pn = parseInt(evt);
    this.pageNumber = this.pageNumber - 1;
    this.getSupplierCatList(this.selectedItem, this.intArray, this.searchItem);
  }
  public trackItem(index: number, item: number) {
    return item;
  }
  public isActivePage(item: number): boolean {
    if (item == this.pageNumber) {
      return true;
    }
    else {
      return false;
    }
  }
  public IsUserLogIn() {
    if (this.common.loginCheck) {
      this.pageWrapper = 'page-wrapper';
    }
    else {
      this.pageWrapper = '';
    }
  }
  public AdDetail(AdId: number) {
    
    if (this.common.IsUserLogIn()) {
      this.common.NavigateToRoute(this.common.apiUrls.User.AdDetail + AdId);
    }
    else {
      this.common.NavigateToRoute(this.common.apiUrls.User.AdDetail + AdId);
    }
  }

  public Sorting(selectedPriceItems:  IDropDownListCat) {
    
    this.selectedItem = selectedPriceItems.Name;
    
    if (selectedPriceItems.Value == SortedType.lowest) {
      this.pageNumber = 1;
      this.getSupplierCatList(selectedPriceItems.Name, this.intArray, this.searchItem);
    }
    else if (selectedPriceItems.Value == SortedType.Highest) {
      this.pageNumber = 1;
      this.getSupplierCatList(selectedPriceItems.Name, this.intArray, this.searchItem);
    }
  }

  public SortingDeSelect(item: number) {
    
    this.getSupplierCatList(this.selectedItem = "", this.intArray, this.searchItem);
  }

  onItemSelect(item: any) {
    
    this.pageNumber = 1;
    this.adsParameterVM.productCategoryIds = item.id;
    this.intArray.push(this.adsParameterVM.productCategoryIds);
    this.getSupplierCatList(this.selectedItem, this.intArray, this.searchItem);
  }

  onItemDeSelect(item: number) {
    this.getSupplierCatList(this.selectedItem, this.intArray = [], this.searchItem);
  }



  selectEvent(item: any) {

    
    this.pageNumber = 1;
    this.searchItem = item.id;
    this.getSupplierCatList(this.selectedItem, this.intArray, this.searchItem);
  }

  unselectEvent(item: Event) {
    
    this.getSupplierCatList(this.selectedItem, this.intArray, this.searchItem = 0);
  }

  onChangeSearch(val: string) {
    // fetch remote data from here
    // And reassign the 'data' which is binded to 'data' property.
  }

  onFocused(e: Event) {

  }

  setImage() {  

  }
}
