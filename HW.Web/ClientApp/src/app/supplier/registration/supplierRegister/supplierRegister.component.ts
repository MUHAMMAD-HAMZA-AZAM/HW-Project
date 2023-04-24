import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonService } from '../../../shared/HttpClient/_http';
import { RegistrationErrorMessagesForSupplier, httpStatus } from '../../../shared/Enums/enums';

import { IdValueVm } from '../../../models/commonModels/commonModels';
import { BusinessDetailUpdate } from '../../../models/supplierModels/supplierModels';

@Component({
  selector: 'app-supplier-register',
  templateUrl: './supplierRegister.component.html',
  styleUrls: ['./supplierRegister.component.css']
})
export class SupplierRegisterComponent implements OnInit {
  public appValForm: FormGroup;
  submitted = false;
  public _http: CommonService;
  public supplierBusinessDetail: BusinessDetailUpdate = new BusinessDetailUpdate();
  //public dropdownListCategory: IdValueVm[] = [];
  public SubCategories: IdValueVm[] = [];
  selectedItems = [];
  selectedItemsSubCategory = [];

  public cityList= [];
  public distanceList= [];
  dropdownListForCity = {};

  dropdownListCategory = [];
  dropdownListCategorySettings = {};

  
  SubCategorySettings = {};


  public tradeNameErrorMessage: string;
  public primaryTradeErrorMessage: string;
  public subCategoryErrorMessage: string;
  public bussinessErrorMessage: string;
  public listOfSubCategories: number[];

  constructor(private formBuilder: FormBuilder, private http: CommonService) {
    this._http = http;
  }
  ngOnInit() {
    this.appValForm = this.formBuilder.group(
      {
        SupplierId:[0],
        CompanyName: ['', Validators.required],
        CompanyRegistrationNo: [],
        PrimaryTradeId: [0],
        PrimaryTrade: ['', [Validators.required]],
        ProductIds: [0, Validators.required],
        DeliveryRadius: [0],
        CityId: [0],
        BusinessAddress: ['', Validators.required],
        //LocationCoordinates: ['', Validators.required],
        EmailAddress: [],
      }
    );


    this.dropdownListCategorySettings = {
      singleSelection: true,
      idField: 'id',
      textField: 'value',
      enableCheckAll: false
    };
    this.dropdownListForCity = {
      singleSelection: true,
      idField: 'id',
      textField: 'value',
      enableCheckAll: false
    };
    this.SubCategorySettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'value',
      itemsShowLimit: 3,
      enableCheckAll: false
    };
    this.getAllCategories();
   // this.SelectSubCategroy(1);
    this.getAllCities();
    this.getDistances();
  }

  getAllCategories() {
    this.http.get(this.http.apiRoutes.Supplier.GetAllProductCategory).subscribe(result => {
      debugger;
      this.dropdownListCategory = result.json();
    });

  }
  getAllCities() {
    this.http.get(this.http.apiRoutes.Common.getCityList).subscribe(result => {
      debugger;
      this.cityList = result.json();
    })
  }
  getDistances() {
    this.http.get(this.http.apiRoutes.Supplier.GetDistances).subscribe(result => {
      debugger;
      this.distanceList = result.json();
    })
  }

  SelectSubCategroy(productCategoryId) {
    debugger;
    if (productCategoryId > 0) {
      this.http.get(this.http.apiRoutes.Supplier.GetAllProductSubCatagories + "?productId=" + productCategoryId).subscribe(result => {
        debugger;
        this.SubCategories = result.json();
      })
    }
  }

  get f() { return this.appValForm.controls; }

  onCategorySelect(selectedPriceItems) {
    debugger;
    this.SelectSubCategroy(selectedPriceItems.id);
  }

  resetSelection() {
    this.selectedItemsSubCategory = [];

  }
  
  SendVerificationLink() {
    this.submitted = true;
    if (this.appValForm.invalid) {
      this.tradeNameErrorMessage = RegistrationErrorMessagesForSupplier.TradeNameErrorMessage;
      this.primaryTradeErrorMessage = RegistrationErrorMessagesForSupplier.primaryTradeErrorMessage;
      this.subCategoryErrorMessage = RegistrationErrorMessagesForSupplier.subCategoryErrorMessage;
      this.bussinessErrorMessage = RegistrationErrorMessagesForSupplier.subCategoryErrorMessage;
      return;
    }
    debugger;
    var test = this.appValForm.value;
    this.supplierBusinessDetail = this.appValForm.value;
    this.supplierBusinessDetail.PrimaryTradeId = this.appValForm.controls.PrimaryTrade.value[0].id;
    this.supplierBusinessDetail.PrimaryTrade = "";
    //this.supplierBusinessDetail.CityId = 1;
    //this.supplierBusinessDetail.DeliveryRadius = 2;
    var data = test.ProductIds;
    this.listOfSubCategories = [];
    if (data.length > 0) {
      for (var i = 0; i <data.length; i++) {
        this.listOfSubCategories.push(test.ProductIds[i].id);
      }
    }

    //if (data.length) {
    //  this.supplierBusinessDetail.productIds = [];
    //  for (var i in data) {
    //    var pd= data[i].id;
    //    this.supplierBusinessDetail.productIds.push(pd);
    //  }
    //}
    this.supplierBusinessDetail.ProductIds = this.listOfSubCategories;
    this.http.PostData(this.http.apiRoutes.Supplier.AddSupplierBusinessDetails, this.supplierBusinessDetail, true).then(result => {
      debugger
      if (status = httpStatus.Ok) {
        this.http.NavigateToRoute(this.http.apiUrls.Supplier.Home);
      }
      else {
        this.http.Notification.error("Some thing went wrong");
      }
    });

  }
}
