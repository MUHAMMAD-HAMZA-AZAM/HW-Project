import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from '../../shared/HttpClient/_http';
import { DomSanitizer } from '@angular/platform-browser';
import { SortedType, keyValue } from '../../../app/shared/Enums/enums';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { FilterDropDown } from '../../models/userModels/userModels';
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';

@Component({
  selector: 'app-product-category-home',
  templateUrl: './product-category-home.component.html',
  styleUrls: ['./product-category-home.component.css']
})
export class ProductCategoryHomeComponent implements OnInit {
  dropdownList = [];
  selectedItems = [];
  selectedPriceItems = [];
  CategorydropdownSettings = {};
  FilterdropdownSettings = {};
  public categoryId: string[] = [];
  public pageWrapper: string;

  public result: any;
  public supplierAd: any;
  public Categories: [];
  public imgURl: "";
  searchText;
  sort;
  savedPopulate;

  //public TalentPoolConfig = {};
  //public TalentPoolValue: keyValue<string, number>[] = [];

  constructor(private route: ActivatedRoute, public common: CommonService, private sanitizer: DomSanitizer, public Loader: Ng4LoadingSpinnerService) {

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
      itemsShowLimit: 3,
      enableCheckAll: false
    };
    this.FilterdropdownSettings = {
      singleSelection: true,
      idField: 'Value',
      textField: 'Name',
    };
  }

  PopulateData() {
    const id = +this.route.snapshot.paramMap.get('id');
    this.Loader.show();
    this.common.get(this.common.apiRoutes.Users.MarketPlace.ProductCategory + "?productCategoryId=" + id).subscribe(data => {
      debugger;
      this.result = data.json();
      this.supplierAd = this.result.supplierAd;
      this.Categories = this.result.categoryList;
      for (var i = 0; i < this.supplierAd.length; i++) {
        if (this.supplierAd[i].supplierAdImage != null) {
          var imgURl = 'data:image/png;base64,' + this.supplierAd[i].supplierAdImage;
          this.supplierAd[i].supplierAdImage = imgURl.toString();
        }

      }
      this.savedPopulate = this.supplierAd;
      this.Loader.hide();
    })
  }

  transform(base64Image) {
    return this.sanitizer.bypassSecurityTrustResourceUrl(base64Image);
  }

  onItemSelect(item) {
    debugger;

    var result = item.categoryId;
    this.categoryId.push(result);
    this.CategorySelectArticles(this.categoryId);
  }
  onItemDeSelect(item) {
    debugger;

    var result = item.categoryId;
    var index = this.categoryId.indexOf(result);
    this.categoryId.splice(index, 1);
    if (this.categoryId.length > 0)
      this.CategorySelectArticles(this.categoryId);
    else
      this.PopulateData();
  }

  CategorySelectArticles(categoryId) {
    this.common.post(this.common.apiRoutes.Supplier.GetAdBySubCategoryIdsWeb, categoryId).subscribe(data => {
      this.result = data.json();
      debugger;
      this.supplierAd = this.result;
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

  Sorting(selectedPriceItems) {
    debugger;
    if (selectedPriceItems.Value == SortedType.lowest)
      this.supplierAd = this.supplierAd.sort((a, b) => 0 - (a.price > b.price ? -1 : 1));
    else
      if (selectedPriceItems.Value == SortedType.Highest)
        this.supplierAd = this.supplierAd.sort((a, b) => 0 - (a.price > b.price ? 1 : -1));
      else
        this.PopulateData();//this section is pending for saved data type
  }

  public IsUserLogIn() {
    debugger
    if (this.common.loginCheck) {
      this.pageWrapper = 'page-wrapper';
    }
    else {
      this.pageWrapper = '';
    }
  }
}
