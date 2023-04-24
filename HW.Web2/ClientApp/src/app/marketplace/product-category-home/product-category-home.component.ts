import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { CommonService } from '../../shared/HttpClient/_http';
import { SortedType, keyValue } from '../../shared/Enums/enums';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { FilterDropDown } from '../../models/userModels/userModels';
import { NgxSpinnerService } from 'ngx-spinner';
import { ICategoryList, IDropDownListCat, IProductCategory_Home, ISupplierAd } from '../../shared/Enums/Interface';

@Component({
  selector: 'app-product-category-home',
  templateUrl: './product-category-home.component.html',
  styleUrls: ['./product-category-home.component.css']
})
export class ProductCategoryHomeComponent implements OnInit {
  dropdownList: IDropDownListCat[] = [];
  //selectedItems = [];
  selectedPriceItems: IDropDownListCat[] = [];
  CategorydropdownSettings = {};
  FilterdropdownSettings = {};
  public categoryId: number[] = [];
  public pageWrapper: string="";
  public result: IProductCategory_Home;
  public resultAd: ISupplierAd[]=[];
  public supplierAd: ISupplierAd[] = [];
  public adListEmpty: boolean = false;
  public Categories: ICategoryList[] = [];
  public imgURl: string= "";
  //searchText;
  //sort;
  savedPopulate: ISupplierAd[]=[];
  public productId: number=0;
  //public TalentPoolConfig = {};
  //public TalentPoolValue: keyValue<string, number>[] = [];

  constructor(private route: ActivatedRoute, public common: CommonService, public Loader: NgxSpinnerService,
  ) {
    this.result = {} as IProductCategory_Home;
    this.route.queryParams.subscribe((params: Params) => {
      this.productId = params['id'];

    });
  }

  ngOnInit() {
    this.common.IsUserLogIn();
    this.IsUserLogIn();
    this.PopulateData();
    this.dropdownList = [
      { Value: 1, Name: 'Lowest Price' },
      { Value: 2, Name: 'Highest Price' },
      { Value: 3, Name: 'Saved' },
    ];
    this.CategorydropdownSettings = {
      singleSelection: false,
      idField: 'categoryId',
      textField: 'categoryName',
      itemsShowLimit: 1,
      enableCheckAll: false
    };
    this.FilterdropdownSettings = {
      singleSelection: true,
      idField: 'Value',
      textField: 'Name',
    }; 
  }

  PopulateData() {
    
    this.Loader.show();
    this.common.get(this.common.apiRoutes.Users.MarketPlace.ProductCategory + "?productCategoryId=" + this.productId).subscribe(data => {
      this.result = <IProductCategory_Home>data ;
      this.supplierAd = this.result.supplierAd;
      console.log(this.supplierAd);
      this.Categories = this.result.categoryList;
      if (this.supplierAd.length == 0) {
        this.adListEmpty = true;
      }
      this.savedPopulate = this.supplierAd;
      this.Loader.hide();
    })
  }

  onItemSelect(item: ICategoryList) {
    var result: number = item.categoryId;
    this.categoryId.push(result);
    this.CategorySelectArticles(this.categoryId);
  }
  onItemDeSelect(item: ICategoryList) {
    var result: number = item.categoryId;
    var index: number = this.categoryId.indexOf(result);
    this.categoryId.splice(index, 1);
    if (this.categoryId.length > 0)
      this.CategorySelectArticles(this.categoryId);
    else
      this.PopulateData();
  }

  CategorySelectArticles(categoryId: number[]) {
    
    this.common.post(this.common.apiRoutes.Supplier.GetAdBySubCategoryIdsWeb, categoryId).subscribe(data => {
      this.resultAd = <ISupplierAd[]>data;
      this.supplierAd = this.resultAd;
      //this.Categories = this.result.categoryList;
      for (var i = 0; i < this.supplierAd.length; i++) {
        if (this.supplierAd[i].supplierAdImage != null) {
          var imgURl = 'data:image/png;base64,' + this.supplierAd[i].supplierAdImage;
          this.supplierAd[i].supplierAdImage = imgURl.toString();
        }

      }
      this.savedPopulate = this.supplierAd;
    })

  }

  Sorting(selectedPriceItems: IDropDownListCat) {
    if (selectedPriceItems.Value == SortedType.lowest)
      this.supplierAd = this.supplierAd.sort((a, b) => 0 - (a.price > b.price ? -1 : 1));
    else
      if (selectedPriceItems.Value == SortedType.Highest)
        this.supplierAd = this.supplierAd.sort((a, b) => 0 - (a.price > b.price ? 1 : -1));
      else
        this.PopulateData();//this section is pending for saved data type
  }

  public AdDetail(AdId: number) {
    if (this.common.IsUserLogIn()) {
      this.common.NavigateToRoute(this.common.apiUrls.User.AdDetail + AdId);
    }
    else {
      localStorage.setItem("Role", '3');
      localStorage.setItem("Show", 'true');
      this.common.NavigateToRoute(this.common.apiUrls.Login.customer);
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
}
