import { HttpStatusCode } from '@angular/common/http';
import { ConditionalExpr } from '@angular/compiler';
import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { int } from 'aws-sdk/clients/datapipeline';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { ResponseVm, StatusCode } from '../../Shared/Enums/common';
import { BankList, IAreaList, ICity, ICityList, ICountryList, ILocationList, IResponse, IStateList, ISupplierDetails } from '../../Shared/Enums/Interface';
import { UploadFileService } from '../../Shared/HttpClient/upload-file.service';
import { CommonService } from '../../Shared/HttpClient/_http';
import { NgbDate, NgbCalendar } from '@ng-bootstrap/ng-bootstrap';
import { DatePipe } from '@angular/common';
import { ImageCroppedEvent, LoadedImage } from 'ngx-image-cropper';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-profile-management',
  templateUrl: './profile-management.component.html',
  styleUrls: ['./profile-management.component.css']
})
export class ProfileManagementComponent implements OnInit, OnDestroy {

  public sellerAccountForm: FormGroup;
  public businessInformationForm: FormGroup;
  public bankAccountForm: FormGroup;
  public wareHouseAddressForm: FormGroup;
  public returnAddressForm: FormGroup;
  public logoForm: FormGroup;
  public sociallinkForm: FormGroup;
  public minShopImgHeight: number = 320;
  public minShopImgWidth: number = 1300;
  public isShopImageValid: boolean = false;
  public isParrentTab: boolean = false;
  public response: IResponse;
  public suppId: number = 0;
  public userId: string = "";
  public profileData: ISupplierDetails;
  public countryList: ICountryList[] = [];
  public cityList: ICityList[] = [];
  public stateList: IStateList[] = [];
  public areaList: IAreaList[] = [];
  public locationList: ILocationList[] = [];
  public listOfBanks: BankList[] = [];
  public GetCityListByReleventState: ICity[] = [];
  public tabId: string = "";
  public file: any | null = "";
  public shopCoverImageProp: string = "";
  public frontImage: string = "";
  public activeTab: string = "";
  public backImage: string = "";
  public checkImage: string = "";
  public logoImage: string = "";
  public returnAddress: boolean = false;
  public isSupplierWeb: string = "SupplierWebApp";
  public shopUrlCrtl: Subscription;
  public responseData: IResponse;
  public hoveredDate: NgbDate | null = null;
  public fromDate: NgbDate;
  public toDate: NgbDate | null = null;
  public imageChangedEvent: any = '';
  public imageLogoChangedEvent: any = '';
  public croppedImage: any = '';
  public croppedLogoImage: any = '';
  public updatedAreaId: Number = null;
  public selectAreaTown: boolean = false;
  businessArea = 'areaName';
  @ViewChild('cropModal', { static: true }) cropImageModel: ModalDirective;
  @ViewChild('cropLogoModal', { static: true }) cropLogoModal: ModalDirective;
  @ViewChild("inputFile") inputFile: ElementRef;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(public common: CommonService, private formBuilder: FormBuilder, private toaster: ToastrService, private route: ActivatedRoute, private _fileService: UploadFileService, calendar: NgbCalendar) {
    this.sellerAccountForm = {} as FormGroup;
    this.businessInformationForm = {} as FormGroup;
    this.bankAccountForm = {} as FormGroup;
    this.wareHouseAddressForm = {} as FormGroup;
    this.returnAddressForm = {} as FormGroup;
    this.logoForm = {} as FormGroup;
    this.sociallinkForm = {} as FormGroup;
    this.profileData = {} as ISupplierDetails;
    this.fromDate = calendar.getToday();
    this.toDate = calendar.getNext(calendar.getToday(), 'd', 1);
    this.cropImageModel = {} as ModalDirective;
   
  }
  ngOnInit(): void {
     
    this.route.queryParams.subscribe((param: Params) => {
      this.tabId = param['tabId'];
  
    });
    this.common.subjectNavigateToProfile$.subscribe((isrouteChanged: any) => {
      if (isrouteChanged) {
        this.changeTab();
      }
    }, error => {
      console.log(error);
    });
    this.common.subjectNavigateToLogo$.subscribe((isrouteChanged: any) => {
      if (isrouteChanged) {
        this.changeTabsaller();
      }
    }, error => {
      console.log(error);
    });
    var decodedtoken = this.common.decodedToken();
    this.suppId = decodedtoken.Id;
    this.userId = decodedtoken.UserId;

    this.getCountryList();
    //this.getStatesList();
    //this.getCityList();
    //this.getAreaList();
    //this.getLocationList();
    this.getBanksList();

    //this.checkingCitycategory(0);
    // Seller Account Form
    this.sellerAccountForm = this.formBuilder.group({
      supplierId: [0],
      publicId: [''],
      firstAndLastName: ['', [Validators.required]],
      emailAddress: [null, [Validators.required, Validators.pattern("^[A-Za-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]],
      mobileNumber: ['', [Validators.required]],
      shopName: ['', [Validators.required]],
      shopUrl: ['', [Validators.required, Validators.pattern(/^[A-Za-z0-9]*$/)]],
      holidayMode: [null],
      holidayModePeriod: [''],
      isCompleted: [false]
    });

    // Business Information Form

    this.businessInformationForm = this.formBuilder.group({
      supplierId: [0],
      companyName: ['', [Validators.required]],
      businessAddress: [null, [Validators.required]],
      ntnnumber: ['', [Validators.required]],
      countryId: [null, [Validators.required]],
      inChargePerson: ['', [Validators.required]],
      registrationNumber: ['', [Validators.required]],
      businessDescription: ['', [Validators.required]],
      shopCoverImage: ['', [Validators.required]],
      idFrontImage: ['', Validators.required],
      cnic: ['', [Validators.required]],
      stateId: [null, [Validators.required]],
      areaId: [null, [Validators.required]],
      location: [null],
      cityId: [null, [Validators.required]],
      idBackSideImage: ['', Validators.required],
      inchargePersonMobileNo: ['', [Validators.required]],
      inchargePersonEmail: ['', [Validators.required]],
      isCompleted: [false]
    });

    // Bank Account Form
    this.bankAccountForm = this.formBuilder.group({
      id: [0],
      supplierId: [0],
      accountTitle: ['', [Validators.required]],
      accountNumber: [null, [Validators.required]],
      bankId: [null, [Validators.required]],
      branchCode: ['', [Validators.required]],
      iban: ['', [Validators.required]],
      bankchequeImage: ['', Validators.required],
      ChequeImageName: [null],
      isCompleted: [false]
    });

    // WareHouseAddress Form
    this.wareHouseAddressForm = this.formBuilder.group({
      id: [0],
      supplierId: [0],
      firstAndLastName: ['', [Validators.required]],
      address: [null, [Validators.required]],
      mobileNumber: ['', [Validators.required]],
      email: ['', [Validators.required]],
      countryId: [null, [Validators.required]],
      stateId: [null, [Validators.required]],
      cityId: [null, [Validators.required]],
      areaId: [null, [Validators.required]],
      location: [null],
      isCompleted: [false]
    });

    // ReturnAddress Form
    this.returnAddressForm = this.formBuilder.group({
      id: [0],
      supplierId: [0],
      isWhareHouseAddress: [true, [Validators.required]],
      name: ['', [Validators.required]],
      address: [null, [Validators.required]],
      mobileNumber: ['', [Validators.required]],
      email: ['', [Validators.required]],
      countryId: [null, [Validators.required]],
      stateId: [null, [Validators.required]],
      cityId: [null, [Validators.required]],
      areaId: [null, [Validators.required]],
      location: [null],
      isCompleted: [false]
    });
    this.logoForm = this.formBuilder.group({
      profileImageId: [0],
      supplierId: [0],
      companyLogo: ['', Validators.required],
    });

    this.sociallinkForm = this.formBuilder.group({
      id: [0],
      supplierId: [0],
      youtubeUrl: ['', Validators.pattern(
        '^(https?:\/\/)?((w{3}\.|m\.)?)youtube.com\/.*'
      )],
      facebookUrl: ['', Validators.pattern(
        '^(https?:\/\/)?((w{3}\.|m\.)?)facebook.com\/.*'  //
      )],
      instagramUrl: ['', Validators.pattern(
        '^(https?:\/\/)?((w{3}\.|m\.)?)instagram.com\/.*'
      )],
      twitterUrl: ['', Validators.pattern(
        '^(https?:\/\/)?((w{3}\.|m\.)?)twitter.com\/.*'
      )],
      linkedInUrl: ['', Validators.pattern(
        '^(https?:\/\/)?((w{3}\.|uk\.|m\.)?)linkedin.com\/.*'
      )],
    });
    this.sellerAccountForm.controls.holidayMode.setValue('1');
    if (this.tabId == 'seller-account' || this.tabId == 'business-information') {
      this.isParrentTab = false;
      this.PopulateData();

    }
    if (this.tabId == 'wherehouse-address') {
      this.isParrentTab = false;
      this.populateWareHouseAddress();

    }
    if (this.tabId == 'return-address') {
      this.isParrentTab = false;
      this.populateReturnAddress();
    }
    if (this.tabId == 'seller-logo') {
      this.isParrentTab = true;
    }
    this.populateBankAccountData();
    this.populateSocialLinks();
    this.PopulateLogo();
    this.shopUrlCrtl = this.sellerAccountForm.controls['shopUrl'].valueChanges.pipe(debounceTime(1000), distinctUntilChanged()).subscribe(url => {
      if (this.profileData.shopUrl !== url) {
        this.common.get(this.common.apiUrls.Supplier.Profile.GetSupplierShopUrl + "?shopUrl=" + url).subscribe(res => {
          this.response = res;
          if (this.response.resultData) {
            this.sellerAccountForm.controls['shopUrl'].setErrors({ shopUrlExist: true })
          } else {
            this.sellerAccountForm.controls['shopUrl'].setErrors(null)
          }
        }, error => {
          console.log(error);
        });
      }
    })
  }
  ngOnDestroy() {
    this.shopUrlCrtl.unsubscribe();
  }
  public PopulateLogo() {
    let supplierId = this.suppId;
    this.common.GetData(this.common.apiUrls.Supplier.Profile.GetLogoData + "?supplierId=" + this.suppId, true).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        if (this.response.resultData?.profileImage) {
          this.logoImage = this.response.resultData.profileImage;
          this.logoForm.controls['companyLogo'].clearValidators();
          this.logoForm.controls['companyLogo'].updateValueAndValidity();
        }
        this.logoForm.patchValue(this.response.resultData);

      }

    }, error => {
      console.log(error);
    });
  }

  //------------------------ Show Country List
  public getCountryList() {
    this.common.GetData
      (this.common.apiUrls.Supplier.Profile.GetCountryList).then(res => {
        if (res.status == StatusCode.OK) {
          this.countryList = res.resultData;
        }
      }, error => {
        console.log(error);
      });
  }

  ////------------------------ Show States List
  //public getStatesList() {
  //  let countryId = 0;
  //  this.common.GetData
  //    (this.common.apiUrls.Supplier.Profile.GetSateList + "?countryId=" + countryId).then(res => {
  //      if (res.status == StatusCode.OK) {
  //        this.stateList = res.resultData;
  //      }
  //    }, error => {
  //      console.log(error);
  //    });
  //}
  //------------------------ Show City List
  //public getCityList() {

  //  this.common.get(this.common.apiUrls.Supplier.City.getCityList).subscribe(result => {
  //    this.cityList = <any>result;
  //    console.log(this.cityList);
  //  });
  //}
  ////------------------------ Show Area List
  //public getAreaList() {
  //  let stateId = 0;
  //  this.common.GetData
  //    (this.common.apiUrls.Supplier.Profile.GetAreaList + "?stateId=" + stateId).then(res => {
  //      if (res.status == StatusCode.OK) {
  //        this.areaList = res.resultData;
  //      }
  //    }, error => {
  //      console.log(error);
  //    });
  //}

  ////------------------------ Show Location List
  //public getLocationList() {
  //  let areaId = 0;
  //  this.common.GetData
  //    (this.common.apiUrls.Supplier.Profile.GetLocationList + "?areaId=" + areaId).then(res => {
  //      if (res.status == StatusCode.OK) {
  //        this.locationList = res.resultData;
  //      }
  //    }, error => {
  //      console.log(error);
  //    });
  //}

  //------------------------ Show States List By Country Id
  public checkingStateCategory(countryId: number) {
    this.resetCascadingCountryDropdown()
    this.common.GetData
      (this.common.apiUrls.Supplier.Profile.GetSateList + "?countryId=" + countryId).then(res => {
        if (res.status == StatusCode.OK) {
          this.stateList = res.resultData;
        }
      }, error => {
        console.log(error);
      });
  }

  //------------------------ Show Area List By City Id
  public checkingAreaCategory(cityId: number) {
    this.resetCascadingCityDropdown();
    this.common.GetData
      (this.common.apiUrls.Supplier.Profile.GetAreaList + "?cityId=" + cityId).then(res => {
        if (res.status == StatusCode.OK) {
          this.areaList = res.resultData;
          if (this.updatedAreaId) {
            let filterArea = this.areaList.find(x => x.areaId == this.updatedAreaId)
            if (filterArea) {
              this.businessInformationForm.controls['areaId'].setValue(filterArea);
              this.returnAddressForm.controls['areaId'].setValue(filterArea);
              this.wareHouseAddressForm.controls['areaId'].setValue(filterArea);

            }
            this.updatedAreaId = null;
          }
        }
      }, error => {
        console.log(error);
      });
  }
  ////------------------------ show city list by state id
  public checkingCitycategory(stateId: number) {
    console.log(stateId)
    this.resetCascadingStateDropdown();
    console.log(stateId)
    this.common.GetData
      (this.common.apiUrls.Supplier.Profile.GetCityListByReleventState + "?stateId=" + stateId).then(res => {
        if (res.status == StatusCode.OK) {
          this.GetCityListByReleventState = res.resultData;
        }
      }, error => {
        console.log(error);
      });
  }

  ////------------------------ Show Location List By Area Id
  //public checkingLocationCategory(areaId: number) {
  //  this.common.GetData
  //    (this.common.apiUrls.Supplier.Profile.GetLocationList + "?areaId=" + areaId).then(res => {
  //      if (res.status == StatusCode.OK) {
  //        this.locationList = res.resultData;
  //      }
  //    }, error => {
  //      console.log(error);
  //    });
  //}



  //------------------------ Show Supplier Details Data

  public PopulateData() {
    this.common.GetData(this.common.apiUrls.Supplier.Profile.GetSupplierDetails + "?supplierId=" + this.suppId, true).then(data => {
      this.response = data;
      if (this.response.status == StatusCode.OK) {
        this.profileData = this.response.resultData;
        console.log(this.profileData);
        this.sellerAccountForm.patchValue(this.profileData);
        if (this.profileData.firstName || this.profileData.lastName)
          this.sellerAccountForm.controls.firstAndLastName.setValue(this.profileData.firstName + ' ' + this.profileData.lastName);
        if (this.profileData.supplierRole != 2) {
          this.businessInformationForm.controls['ntnnumber'].clearValidators();
          this.businessInformationForm.controls['ntnnumber'].updateValueAndValidity();
        }
        let holidayModeValue = this.profileData.holidayMode == true ? '1' : '0';
        this.sellerAccountForm.controls.holidayMode.setValue(holidayModeValue);
        if (this.profileData.shopCoverImage) {
          this.shopCoverImageProp = this.profileData.shopCoverImage;
          this.businessInformationForm.controls['shopCoverImage'].clearValidators();
          this.businessInformationForm.controls['shopCoverImage'].updateValueAndValidity();
        }
        if (this.profileData.idfrontImage) {
          this.frontImage = this.profileData.idfrontImage;
          this.businessInformationForm.controls['idFrontImage'].clearValidators();
          this.businessInformationForm.controls['idFrontImage'].updateValueAndValidity();
        }
        if (this.profileData.cityId == 0) {
          this.profileData.cityId = null;
        }
        if (this.profileData.idbackImage) {

          this.backImage = this.profileData.idbackImage;
          this.businessInformationForm.controls['idBackSideImage'].clearValidators();
          this.businessInformationForm.controls['idBackSideImage'].updateValueAndValidity();
        }
        this.businessInformationForm.controls['businessDescription'].setValue(this.profileData.businessDescription);
        let stateId = Number(this.profileData.state);
        if (stateId == 0) {
          stateId = null;
        }
        console.log(this.profileData);
        this.updatedAreaId = this.profileData.areaId;
        this.checkingStateCategory(this.profileData.countryId);
        this.checkingCitycategory(stateId);
        this.checkingAreaCategory(this.profileData.cityId)
        this.businessInformationForm.controls['stateId'].setValue(this.profileData.state);
        this.businessInformationForm.patchValue(this.profileData);
        this.common.Loader.hide();


      }
    }, error => {
      console.log(error);
    });
  }

  //------------------------ SaveAndUpdate SellerAccount In Profile 
  get f1() {
    return this.sellerAccountForm.controls;
  }
  public saveAndUpdateSellerAccount() {

    if (this.sellerAccountForm.invalid) {
      this.sellerAccountForm.markAllAsTouched();
      return;
    }
    let formData = this.sellerAccountForm.value;

    formData.firstName = formData.firstAndLastName.split(' ').slice(0, -1).join(' ');
    formData.LastName = formData.firstAndLastName.split(' ').slice(-1).join(' ');
    formData.holidayMode = formData.holidayMode == 1 ? formData.holidayMode = true : formData.holidayMode = false;
    formData.supplierId = this.suppId;
    formData.createdBy = this.userId;
    var sDate = new Date(this.fromDate.year, this.fromDate.month - 1, this.fromDate.day);
    var tDate = new Date(this.toDate.year, this.toDate.month - 1, this.toDate.day);
    const datepipe: DatePipe = new DatePipe('en-US')
    let startDate = datepipe.transform(sDate, 'yyyy-MM-dd');
    let toDate = datepipe.transform(tDate, 'yyyy-MM-dd');
    formData.holidayStart = startDate;
    formData.hoilidayEnd = toDate;
    console.log(formData);
    this.common.PostData(this.common.apiUrls.Supplier.Profile.AddAndUpdateSellerAccount, JSON.stringify(formData), true).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        this.toaster.success(this.response.message);
      }

    }, error => {
      this.common.Loader.show();
      console.log(error);
    });

  }


  //------------------------ SaveAndUpdate BusinessInformation In Profile 

  get f2() {
    return this.businessInformationForm.controls;
  }

  // AddAndUpdate BusinessInformation Address
  public SaveAndUpdateBusinessInformation() {

    if (this.businessInformationForm.invalid) {
      this.businessInformationForm.markAllAsTouched();
      return;
    }
    let formData = this.businessInformationForm.value;
    if (typeof formData.areaId === 'object') {
      formData.areaId = formData['areaId'].areaId;
    }
    else {
      this.businessInformationForm.controls['areaId'].setErrors({ areaInvalidInput: true });
      return;


    }

    console.log(formData)
    //Save BusinessInforamtion Data
    formData.supplierId = this.suppId;
    formData.createdBy = this.userId;
    formData.idFrontImage = this.frontImage;
    formData.idBackSideImage = this.backImage;
    formData.shopCoverImage = this.croppedImage;
    this.common.PostData(this.common.apiUrls.Supplier.Profile.AddAndUpdateBusinessAccount, JSON.stringify(formData), true).then(res => {

      this.response = res;
      if (this.response.status == StatusCode.OK) {
        this.toaster.success(this.response.message);
      }

    }, error => {
      this.common.Loader.show();
      console.log(error);
    });
  }

  //------------------------ SaveAndUpdateBankAccount In Profile 

  //Show Banks list 
  public getBanksList() {
    this.common.GetData
      (this.common.apiUrls.Supplier.Profile.GetBanksList).then(res => {
        if (res.status == StatusCode.OK) {
          this.listOfBanks = res.resultData;
        }
      }, error => {
        console.log(error);
      });
  }

  get f3() {
    return this.bankAccountForm.controls;
  }

  //------------------------ AddAndUpdate BankAccount Address
  public saveAndUpdateBankAccountData() {

    if (this.bankAccountForm.invalid) {
      this.bankAccountForm.markAllAsTouched();
      return;
    }
    let formData = this.bankAccountForm.value;

    if (formData.id <= 0) {
      //Save BankAccount Data
      formData.id = 0;
      formData.supplierId = this.suppId;
      formData.createdBy = this.userId;
      formData.ChequeImageName = this.checkImage;
      this.common.PostData(this.common.apiUrls.Supplier.Profile.SaveAndUpdateBankAccountData, JSON.stringify(formData), true).then(res => {
        this.response = res;
        if (this.response.status == StatusCode.OK) {
          this.bankAccountForm.controls['id'].setValue(this.response.resultData[0].scopeIdentity);
          this.toaster.success(this.response.message);
        }

      }, error => {
        this.common.Loader.show();
        console.log(error);
      });
    }
    else {
      //Update BankAccount Data
      formData.supplierId = this.suppId;
      formData.modifiedBy = this.userId;
      formData.ChequeImageName = this.checkImage;
      this.common.PostData(this.common.apiUrls.Supplier.Profile.SaveAndUpdateBankAccountData, JSON.stringify(formData), true).then(res => {
        this.response = res;
        if (this.response.status == StatusCode.OK) {
          this.toaster.success(this.response.message);
          this.PopulateData();
        }

      }, error => {
        this.common.Loader.show();
        console.log(error);
      });

    }

  }

  //------------------------ Show BankAccount Data
  public populateBankAccountData() {

    let supplierId = this.suppId;
    this.common.GetData(this.common.apiUrls.Supplier.Profile.GetBankAccountData + "?supplierId=" + this.suppId, true).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        if (this.response.resultData?.chequeImage) {

          this.checkImage = this.response.resultData?.chequeImage;
          this.bankAccountForm.controls['bankchequeImage'].clearValidators();
          this.bankAccountForm.controls['bankchequeImage'].updateValueAndValidity();
        }

        this.bankAccountForm.patchValue(this.response.resultData);
        //this.toaster.success(this.response.message);
      }

    }, error => {
      this.common.Loader.show();
      console.log(error);
    });
  }

  //------------------------ AddAndUpdateWhareHouseAddress In Profile 

  get f4() {
    return this.wareHouseAddressForm.controls;
  }

  //------------------------ AddAndUpdate WareHouse Address
  public addAndUpdateWhareHouseAddress() {
    if (this.wareHouseAddressForm.invalid) {
      this.wareHouseAddressForm.markAllAsTouched();
      return;
    }
    let formData = this.wareHouseAddressForm.value;
    if (typeof formData.areaId === 'object') {
      formData.areaId = formData['areaId'].areaId;
    }
    else {
      this.wareHouseAddressForm.controls['areaId'].setErrors({ areaInvalidInput: true });
      return;


    }
    console.log(formData)
    if (formData.id <= 0) {
      //Save WareHouseAddress Data
      formData.id = 0;
      formData.supplierId = this.suppId;
      formData.createdBy = this.userId;
      formData.supplierId = this.suppId;
      this.common.PostData(this.common.apiUrls.Supplier.Profile.SaveAndUpdateWhareHouseAddress, JSON.stringify(formData), true).then(res => {

        this.response = res;
        if (this.response.status == StatusCode.OK) {
          this.wareHouseAddressForm.controls['id'].setValue(this.response.resultData[0].scopeIdentity);
          this.toaster.success(this.response.message);
        }

      }, error => {
        this.common.Loader.show();
        console.log(error);
      });
    }
    else {
      //Update WareHouseAddress Data
      formData.supplierId = this.suppId;
      formData.modifiedBy = this.userId;
      this.common.PostData(this.common.apiUrls.Supplier.Profile.SaveAndUpdateWhareHouseAddress, JSON.stringify(formData), true).then(res => {

        this.response = res;
        if (this.response.status == StatusCode.OK) {
          //this.wareHouseAddressForm.patchValue(this.response.resultData[0]);
          this.toaster.success(this.response.message);
        }

      }, error => {
        this.common.Loader.show();
        console.log(error);
      });


    }
  }

  //------------------------ Show WareHouseAddress Data
  public populateWareHouseAddress() {
    this.common.GetData(this.common.apiUrls.Supplier.Profile.GetWareHouseAddress + "?supplierId=" + this.suppId, true).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        this.updatedAreaId = this.response.resultData?.areaId;
        this.checkingStateCategory(this.response.resultData?.countryId);
        this.checkingCitycategory(this.response.resultData?.stateId);
        this.checkingAreaCategory(this.response.resultData?.cityId)
        this.wareHouseAddressForm.patchValue(this.response.resultData);
        this.wareHouseAddressForm.controls.firstAndLastName.setValue(this.response.resultData?.name);

      }
      if (this.response.status == StatusCode.Restricted) {
        this.updatedAreaId = null;
        this.resetCascadingCityDropdown();
      }

    }, error => {
      this.common.Loader.show();
      console.log(error);
    });
  }
  //------------------------ AddAndUpdateReturnAddress In Profile 

  get f5() {
    return this.returnAddressForm.controls;
  }
  get f6() {
    return this.logoForm.controls;
  }
  get fsocial() {
    return this.sociallinkForm.controls;
  }

  // AddAndUpdate Return Address
  public addAndUpdateReturnAddress() {
     

    if (this.returnAddressForm.invalid) {
      this.returnAddressForm.markAllAsTouched();
      return;
    }
    let formData = this.returnAddressForm.value;
    if (typeof formData.areaId === 'object') {
      formData.areaId = formData['areaId'].areaId;
    }
    else {
      this.returnAddressForm.controls['areaId'].setErrors({ areaInvalidInput: true });
      return;
     

    }

    console.log(formData)
    if (formData.id <= 0) {
      //Save ReturnAddress Data
      formData.id = 0;
      formData.supplierId = this.suppId;
      formData.copyFromWhareHouseAddress = parseInt(formData.copyFromWhareHouseAddress);
      formData.createdBy = this.userId;
      this.common.PostData(this.common.apiUrls.Supplier.Profile.SaveAndUpdateReturnAddress, JSON.stringify(formData), true).then(res => {

        this.response = res;
        if (this.response.status == StatusCode.OK) {

          this.returnAddressForm.controls['id'].setValue(this.response.resultData[0].scopeIdentity);
          this.toaster.success(this.response.message);
        }

      }, error => {
        this.common.Loader.show();
        console.log(error);
      });
    }
    else {
      //Update ReturnAddress Data
      formData.supplierId = this.suppId;
      formData.modifiedBy = this.userId;
      this.common.PostData(this.common.apiUrls.Supplier.Profile.SaveAndUpdateReturnAddress, JSON.stringify(formData), true).then(res => {

        this.response = res;
        if (this.response.status == StatusCode.OK) {
          this.toaster.success(this.response.message);
        }

      }, error => {
        this.common.Loader.show();
        console.log(error);
      });
    }
  }
  public addAndUpdateLogo() {

    if (this.logoForm.invalid) {
      this.logoForm.markAllAsTouched();
      return;
    }
    let formDataLogo = this.logoForm.value;
    if (formDataLogo.profileImageId <= 0) {
      //Save ReturnAddress Data
      formDataLogo.profileImageId = 0;
    }
    formDataLogo.supplierId = this.suppId;
    formDataLogo.profileImage = this.croppedLogoImage;
    formDataLogo.createdBy = this.userId;
    this.common.PostData(this.common.apiUrls.Supplier.Profile.AddAndUpdateLogo, JSON.stringify(formDataLogo), true).then(res => {

      this.response = res;
      if (this.response.status == StatusCode.OK) {
        this.toaster.success("Logo Updated", "Successfully !!");
        this.common.subject$.next(true);
        this.cropLogoModal.hide();
        this.PopulateLogo();
      }

    }, error => {
      this.common.Loader.show();
      console.log(error);
    });

  }

  //------------------------ Show ReturnAddress Data
  public populateReturnAddress() {


    let supplierId = this.suppId;
    this.common.GetData(this.common.apiUrls.Supplier.Profile.GetReturnAddress + "?supplierId=" + this.suppId, true).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        this.updatedAreaId = this.response.resultData?.areaId;
        this.checkingStateCategory(this.response.resultData.countryId);
        this.checkingCitycategory(this.response.resultData?.stateId);
        this.checkingAreaCategory(this.response.resultData?.cityId);
        this.returnAddressForm.patchValue(this.response.resultData);
        this.returnAddress = true;
      }
      if (this.response.status == StatusCode.Restricted) {
        this.updatedAreaId = null;
       this.returnAddress = false;
        this.resetCascadingCityDropdown();

      }

    }, error => {
      this.common.Loader.show();
      console.log(error);
    });
  }

  // ------------------------ Show Social Links -------------------
  public populateSocialLinks() {
    let supplierId = this.suppId;
    let isSupplierWeb = this.suppId;
    console.log(this.isSupplierWeb);
    this.common.GetData(this.common.apiUrls.Supplier.Profile.GetSocialLinks + `?supplierId=${this.suppId}&isSupplierWeb=${this.isSupplierWeb}`).then(res => {
      this.response = res;

      if (this.response.status == StatusCode.OK) {
        this.sociallinkForm.patchValue(this.response.resultData);
        this.returnAddress = true;
      }

    }, error => {
      this.common.Loader.show();
      console.log(error);
    });
  }


  //public fileChangeShopCoverImage(event: any) {
  //  this._fileService.getBase64(event.target.files[0]).then(x => {
  //    let img = String(x);
  //    this.shopCoverImageProp = img.split(',')[1];
  //  });
  //}

  fileChangeEvent(event: any): void {
    this.imageChangedEvent = event;
    var file = event.target.files[0];
    const reader = new FileReader();
    if (file.type != "image/png" && file.type != "image/jpg" && file.type != "image/jpeg") {
      this.inputFile.nativeElement = null;
      this.toaster.error("You can't be able to upload file except PNG ,JPEG or JPG format", "Image Type");
      return;
    }
    else {
      let that = this;
      reader.readAsDataURL(file);
      reader.onload = (e: any) => {
        const image = new Image();
        image.src = e.target.result;
        image.onload = function (event) {
          if (event.currentTarget['height'] < that.minShopImgHeight && event.currentTarget['width'] < that.minShopImgWidth) {
            that.toaster.error(`Please upload image with minimum these dimensions Width ${that.minShopImgWidth}px  and Height ${that.minShopImgHeight}px`, "Error", { timeOut: 5000 });
            return false;
          }
          else {
            that.cropImageModel.show();
            return true;
          }
        }
      }
    }
  }
  imageCropped(event: ImageCroppedEvent) {

    this.croppedImage = event.base64;
  }



  fileChangeFrontImage(event: Event) {

    //this._fileService.getBase64(event.target.files[0]).then(x => {
    //  let img = String(x);
    //  this.frontImage = img.split(',')[1];

    //});
    //this.file=(;
    this._fileService.getBase64((<HTMLInputElement>event.target).files?.[0]).then(x => {
      let img = String(x);
      this.frontImage = img.split(',')[1];

    });
  }

  fileChangeBackImage(event?: Event) {
    //this._fileService.getBase64(event.target.files[0]).then(y => {
    //  let img = String(y);
    //  this.backImage = img.split(',')[1];
    //});
    this._fileService.getBase64((<HTMLInputElement>event?.target).files?.[0]).then(y => {
      let img = String(y);
      this.backImage = img.split(',')[1];
    });
  }
  fileChangeEventCheck(event: Event) {
    //this._fileService.getBase64(event.target.files[0]).then(x => {
    //  let img = String(x);
    //  this.checkImage = img.split(',')[1];
    //});
    this._fileService.getBase64((<HTMLInputElement>event.target).files?.[0]).then(x => {
      let img = String(x);
      this.checkImage = img.split(',')[1];
    });
  }
  fileChangeEventLogo(event: Event) {
    this.imageLogoChangedEvent = event;
    this.cropLogoModal.show();

    //this._fileService.getBase64((<HTMLInputElement>event.target).files?.[0]).then(x => {
    //  let img = String(x);
    //  this.logoImage = img.split(',')[1];
    //});
  }
  logoImageCropped(event: ImageCroppedEvent) {
    this.croppedLogoImage = event.base64;

  }
  clickRadioBtn(btnvalue: number) {
 
    console.log(btnvalue);
    let idValue = this.returnAddressForm.controls.id.value;
    if (btnvalue == 0) {
      this.returnAddressForm.controls.isWhareHouseAddress.setValue(btnvalue);
      if (idValue == 0) {
        this.returnAddressForm.reset();
      }
    }
    else {
      this.returnAddressForm.controls.name.setValue(this.wareHouseAddressForm.controls.firstAndLastName.value);
      this.returnAddress = true;
let countryId=this.wareHouseAddressForm.value.countryId;
let stateId=this.wareHouseAddressForm.value.stateId;
let cityId=this.wareHouseAddressForm.value.stateId;

        this.checkingStateCategory(countryId);
        this.checkingCitycategory(stateId);
        this.checkingAreaCategory(cityId);
   this.returnAddressForm.patchValue(this.wareHouseAddressForm.value);
      this.returnAddressForm.controls.id.setValue('0');
    }
  }
  public addAndUpdateSocialLinks() {
    if (this.sociallinkForm.invalid) {
      this.sociallinkForm.markAllAsTouched();
      return;
    }
    let formData = this.sociallinkForm.value;
    if (formData.id <= 0) {
      //Save Social Links Data
      formData.id = 0;
      formData.supplierId = this.suppId;
      formData.createdBy = this.userId;
      this.common.PostData(this.common.apiUrls.Supplier.Profile.SaveAndUpdateSocialLinks, JSON.stringify(formData), true).then(res => {

        this.response = res;

        if (this.response.status == StatusCode.OK) {
          this.sociallinkForm.controls['id'].setValue(this.response.resultData[0].id);
          this.toaster.success(this.response.message);
        }

      }, error => {
        this.common.Loader.show();
        console.log(error);
      });
    }
    else {
      //Update ReturnAddress Data
      formData.supplierId = this.suppId;
      formData.modifiedBy = this.userId;
      this.common.PostData(this.common.apiUrls.Supplier.Profile.SaveAndUpdateSocialLinks, JSON.stringify(formData), true).then(res => {

        this.response = res;

        if (this.response.status == StatusCode.OK) {
          this.toaster.success(this.response.message);
        }

      }, error => {
        this.common.Loader.show();
        console.log(error);
      });
    }
  }

  onDateSelection(date: NgbDate) {
    if (!this.fromDate && !this.toDate) {
      this.fromDate = date;
    } else if (this.fromDate && !this.toDate && date.after(this.fromDate)) {
      this.toDate = date;
    } else {
      this.toDate = null;
      this.fromDate = date;
    }
  }
  isHovered(date: NgbDate) {
    return this.fromDate && !this.toDate && this.hoveredDate && date.after(this.fromDate) && date.before(this.hoveredDate);
  }

  isInside(date: NgbDate) {
    return this.toDate && date.after(this.fromDate) && date.before(this.toDate);
  }

  isRange(date: NgbDate) {
    return date.equals(this.fromDate) || (this.toDate && date.equals(this.toDate)) || this.isInside(date) || this.isHovered(date);
  }

  public hideModal() {
    this.cropImageModel.hide();
  }
  public hideLogoModal() {
    this.cropLogoModal.hide();
  }
  public populateBussnessSallerData() {
    this.PopulateData()

  }
  public populateWherehouseAddressData() {
    this.populateWareHouseAddress();
  }
  public populateReturnAddressData() {
    this.populateReturnAddress();
  }
  public resetCascadingCountryDropdown() {
    this.stateList = []
    this.GetCityListByReleventState = [];
    this.areaList = [];
    this.businessInformationForm.controls['stateId'].setValue(null);
    this.returnAddressForm.controls['stateId'].setValue(null);
    this.wareHouseAddressForm.controls['stateId'].setValue(null);

    this.businessInformationForm.controls['cityId'].setValue(null);
    this.returnAddressForm.controls['cityId'].setValue(null);
    this.wareHouseAddressForm.controls['cityId'].setValue(null);

    this.businessInformationForm.controls['areaId'].setValue(null);
    this.returnAddressForm.controls['areaId'].setValue(null);
    this.wareHouseAddressForm.controls['areaId'].setValue(null);
  }
  public resetCascadingStateDropdown() {
    this.GetCityListByReleventState = [];
    this.areaList = [];
    this.businessInformationForm.controls['cityId'].setValue(null);
    this.returnAddressForm.controls['cityId'].setValue(null);
    this.wareHouseAddressForm.controls['cityId'].setValue(null);

    this.businessInformationForm.controls['areaId'].setValue(null);
    this.returnAddressForm.controls['areaId'].setValue(null);
    this.wareHouseAddressForm.controls['areaId'].setValue(null);

  }
  public resetCascadingCityDropdown() {
    this.areaList = [];
    this.businessInformationForm.controls['areaId'].setValue(null);
    this.returnAddressForm.controls['areaId'].setValue(null);
    this.wareHouseAddressForm.controls['areaId'].setValue(null);
  }

  public selectTownEvent(item: Event) {

  }

  public unselectTownEvent(item: Event) {
  }

  public onChangetownSearch(event: Event) {
    // fetch remote data from here
    // And reassign the 'data' which is binded to 'data' property.
  }
  changeTab() {
     
    this.isParrentTab = false;
    this.tabId ="seller-account"
    this.PopulateData();
  }
  changeTabsaller() {
     
    this.isParrentTab = true;
    this.route.queryParams.subscribe((param: Params) => {
      this.tabId = param['tabId'];
    });
  }
}
