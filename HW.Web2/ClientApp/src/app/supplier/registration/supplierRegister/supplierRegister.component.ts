import { Component, OnInit, ElementRef, ViewChild, NgZone } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonService } from '../../../shared/HttpClient/_http';
import { RegistrationErrorMessagesForSupplier, httpStatus, CommonErrors } from '../../../shared/Enums/enums';

import { IdValueVm, ResponseVm } from '../../../models/commonModels/commonModels';
import { BusinessDetailUpdate } from '../../../models/supplierModels/supplierModels';
import { MapsAPILoader } from '@agm/core';
import { Events } from '../../../common/events';
import {IIdValue, ISubCategorySettings } from '../../../shared/Enums/Interface';

@Component({
  selector: 'app-supplier-register',
  templateUrl: './supplierRegister.component.html',
  styleUrls: ['./supplierRegister.component.css']
})
export class SupplierRegisterComponent implements OnInit {
  public appValForm: FormGroup;
  public submitted = false;
  public _http: CommonService;
  public supplierBusinessDetail: BusinessDetailUpdate = new BusinessDetailUpdate();
  //public dropdownListCategory: IdValueVm[] = [];
  public SubCategories: IdValueVm[] = [];
  //public selectedItems = [];
  public selectedItemsSubCategory = new Array;
  public cityList: IIdValue[]= [];
  public distanceList: IIdValue[] = [];
  public dropdownListForCity: ISubCategorySettings;
  public dropdownListCategory: IIdValue[] = [];
  public dropdownListCategorySettings: ISubCategorySettings;
  public SubCategorySettings: ISubCategorySettings;
  public ShowFilter: boolean = true;
  public tradeNameErrorMessage: string="";
  public primaryTradeErrorMessage: string="";
  public subCategoryErrorMessage: string="";
  public bussinessErrorMessage: string="";
  public listOfSubCategories: number[] = [];
  public latitude: number=0;// = 30.3753;
  public longitude: number=0;//= 69.3451;
  public zoom: number = 15;
  public address: string = "";
  public response: ResponseVm = {} as ResponseVm;
  public geoCoder = new google.maps.Geocoder();
  @ViewChild('search', { static: true })
  public searchElementRef: ElementRef;

  constructor(
    private formBuilder: FormBuilder,
    private common: CommonService,
    public mapsAPILoader: MapsAPILoader,
    public ngZone: NgZone,
    private event: Events,
  ) {
    this._http = common;
    this.appValForm = {} as FormGroup;
    this.dropdownListForCity = {} as ISubCategorySettings;
    this.dropdownListCategorySettings = {} as ISubCategorySettings;
    this.SubCategorySettings = {} as ISubCategorySettings;
    this.searchElementRef = {} as ElementRef;
  }

  ngOnInit() {
    this.startLocation();
    this.appValForm = this.formBuilder.group(
      {
        SupplierId: [0],
        CompanyName: ['', Validators.required],
        CompanyRegistrationNo: [''],
        PrimaryTradeId: [0],
        PrimaryTrade: ['', [Validators.required]],
        ProductIds: [0, [Validators.required]],
        DeliveryRadius: ['', [Validators.required]],
        CityId: ['', [Validators.required]],
        BusinessAddress: ['', [Validators.required]],
        EmailAddress: [''],
      }
    );
    this.dropdownListCategorySettings = {
      singleSelection: true,
      idField: 'id',
      textField: 'value',
      enableCheckAll: false,
      allowSearchFilter: this.ShowFilter,
      closeDropDownOnSelection: true,
    };
    this.dropdownListForCity = {
      singleSelection: true,
      idField: 'id',
      textField: 'value',
      enableCheckAll: false,
    };
    this.SubCategorySettings = {
      //singleSelection: false,
      //idField: 'id',
      //textField: 'value',
      //itemsShowLimit: 3,
      //enableCheckAll: false,
      singleSelection: false,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: this.ShowFilter,
      itemsShowLimit: 2,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };
    this.getAllCategories();
    // this.SelectSubCategroy(1);
    this.getAllCities();
    this.getDistances();
  }

  public getAllCategories() {
    this.common.GetData(this.common.apiRoutes.Supplier.GetAllProductCategory, true).then(result => {
      this.dropdownListCategory = result ;
    });

  }

  public getAllCities() {
    this.common.GetData(this.common.apiRoutes.Common.getCityList, true).then(result => {
      this.cityList = result ;
    })
  }

  public getDistances() {
    this.common.GetData(this.common.apiRoutes.Supplier.GetDistances, true).then(result => {
      this.distanceList = result ;
    })
  }

  public SelectSubCategroy(productCategoryId: number) {
    if (productCategoryId > 0) {
      this.common.GetData(this.common.apiRoutes.Supplier.GetAllProductSubCatagories + "?productId=" + productCategoryId, true).then(result => {
        this.SubCategories = result ;
      })
    }
  }

  get f() { return this.appValForm.controls; }

  public onCategorySelect(selectedPriceItems: IIdValue) {
    this.SelectSubCategroy(selectedPriceItems.id);
  }

  public resetSelection(selectArgument: Event) {
    this.selectedItemsSubCategory = [];

  }

  public SendVerificationLink() {
    this.submitted = true;
    if (this.appValForm.invalid) {
      this.tradeNameErrorMessage = RegistrationErrorMessagesForSupplier.TradeNameErrorMessage;
      this.primaryTradeErrorMessage = RegistrationErrorMessagesForSupplier.primaryTradeErrorMessage;
      this.subCategoryErrorMessage = RegistrationErrorMessagesForSupplier.subCategoryErrorMessage;
      this.bussinessErrorMessage = RegistrationErrorMessagesForSupplier.subCategoryErrorMessage;
      return;
    }
    var test = this.appValForm.value;
    this.supplierBusinessDetail = this.appValForm.value;
    this.supplierBusinessDetail.PrimaryTradeId = this.appValForm.controls.PrimaryTrade.value[0].id;
    this.supplierBusinessDetail.PrimaryTrade = "";
    this.supplierBusinessDetail.LocationCoordinates = this.latitude + "," + this.longitude;
    this.supplierBusinessDetail.BusinessAddress = this.searchElementRef.nativeElement.value;
    var data = test.ProductIds;
    this.listOfSubCategories = [];
    if (data.length > 0) {
      for (var i = 0; i < data.length; i++) {
        this.listOfSubCategories.push(test.ProductIds[i].id);
      }
    }
    this.supplierBusinessDetail.ProductIds = this.listOfSubCategories;
    this.common.PostData(this.common.apiRoutes.Supplier.AddSupplierBusinessDetails, this.supplierBusinessDetail, true).then(result => {
      this.response = result ;
      if (this.response.status == httpStatus.Ok) {
        
        localStorage.setItem('profileCompleted', 'false');
        this.event.profile_Completed.emit();
        this.common.NavigateToRoute(this.common.apiUrls.Supplier.Home);
        // location.reload();
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });

  }


  public setCurrentLocation(latitude: number, longitude: number) {
    if (latitude != undefined && longitude != undefined) {
      this.latitude = latitude;
      this.longitude = longitude;
      this.getAddress(latitude, longitude);
    }
    else if ('geolocation' in navigator) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.latitude = position.coords.latitude;
        this.longitude = position.coords.longitude;
        this.getAddress(this.latitude, this.longitude);
      });
    }
  }

  public markerDragEnd($event: MouseEvent) {
    console.log($event);
    this.getAddress(this.latitude, this.longitude);
  }

  public getAddress(latitude: number, longitude: number) {
    this.geoCoder.geocode({ 'location': { lat: latitude, lng: longitude } }, (results, status) => {
      if (status === 'OK') {
        if (results[0]) {
          this.address = results[0].formatted_address;
          console.log(this.address);
        } else {
          window.alert('No results found');
        }
      } else {
        window.alert('Geocoder failed due to: ' + status);
      }

    });
  }

  public startLocation() {
    this.mapsAPILoader.load().then(() => {
      this.setCurrentLocation(this.latitude, this.longitude);
      let autocomplete = new google.maps.places.Autocomplete(this.searchElementRef.nativeElement, {
        types: ["address"]
      });
      autocomplete.addListener("place_changed", () => {
        this.ngZone.run(() => {
          //get the place result
          let place: google.maps.places.PlaceResult = autocomplete.getPlace();
          //verify result
          if (place.geometry === undefined || place.geometry === null) {
            return;
          }
          //set latitude, longitude and zoom
          this.latitude = place.geometry.location.lat();
          this.longitude = place.geometry.location.lng();
        });
      });
    });
  }

  public addMarker(lat: number, lng: number) {
    this.latitude = lat;
    this.longitude = lng;
    this.setCurrentLocation(this.latitude, this.longitude);
  }

  public SearchLocation(location: string) {

    this.startLocation();
  }
}
