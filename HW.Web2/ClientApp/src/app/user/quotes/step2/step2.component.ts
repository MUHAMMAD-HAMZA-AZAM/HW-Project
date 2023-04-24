import { Component, OnInit, ViewChild, Input, ElementRef, NgZone, ViewEncapsulation } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IdValueVm, ResponseVm, BasicRegistration } from '../../../models/commonModels/commonModels';
import { MapsAPILoader } from '@agm/core';
import { ActivatedRoute, Params } from '@angular/router';
import { GetQuotes, GetQuotesVM, SmsVM, Images } from '../../../models/userModels/userModels';
import { JobQuotationErrors, BidStatus, httpStatus, CommonErrors } from '../../../shared/Enums/enums';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { DatePipe } from '@angular/common';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { SocialAuthService } from 'angularx-social-login';
import { IErrorList, IPersonalDetails, ITownList, ITownListSearch } from '../../../shared/Enums/Interface';
import { MessagingService } from '../../../shared/CommonServices/messaging.service';
import { ThrowStmt } from '@angular/compiler';

@Component({
  selector: 'step2',
  templateUrl: './step2.component.html',
  styleUrls: ['./step2.component.css'],
})

export class Step2 implements OnInit {
  public cityList: IdValueVm[] = [];
  public latitude: number = 31.5204;
  public longitude: number = 74.3587;
  public jobAddressLatitude: number = 0;
  public jobAddressLongitude: number = 0;
  public defaultAddress: string = "Iqra Street, Block Q Model Town, Lahore";
  public zoom: number = 15;
  public address: string = "";
  public notSelectedFromMap: boolean = false;
  public Id: number = 1;
  public geoCoder = new google.maps.Geocoder();
  @ViewChild('search', { static: true })
  public searchElementRef: ElementRef;
  public getQuotationsVM: GetQuotes = {} as GetQuotes;
  public quotationVM: GetQuotesVM = {} as GetQuotesVM;
  public smsVM: SmsVM = {} as SmsVM;
  public appValForm: FormGroup;
  public errorsList: IErrorList;
  public setDate: any;
  public setTime: any;
  public submitted: boolean = false;
  public isEmail: boolean = true;
  public locationError: boolean = false;
  public showModal: boolean = false;
  public isVerified: boolean = false;
  public btnQuote: boolean = false;
  public townInvalidInput: boolean = false;
  public token: string | null = "";
  public phoneNumber: any;
  public basicRegistrationVm: BasicRegistration = {} as BasicRegistration;
  public code: number = 0;
  public currentDate: Date = new Date;
  public pipe: any;
  public date: Date = new Date;
  public selecteddate: Date = new Date;
  public isdate = false;
  public maxdate1: string = "";
  public day: string = "";
  public month: string = "";
  public year: string = "";
  public loggedUserDetails: IPersonalDetails;
  public userId: string = "";
  public loginCheck: boolean = false;
  public townChanged: boolean = false;
  public townList: ITownList[] = [];
  public searchtownList: ITownListSearch[] = [];
  townkeyword = 'value';
  public isUserBlocked: boolean = false;
  public response: ResponseVm = {} as ResponseVm;
  @ViewChild('postJobModalConfirm', { static: true }) postJobModal: ModalDirective;
  @ViewChild('verifyAccountMessageModal', { static: false }) verifyAccountMessageModal: ModalDirective;
  @ViewChild('blockAccountMessageModal', { static: false }) blockAccountMessageModal: ModalDirective;
  cityName: string = 'Lahore';
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(
    //private datepipe: DatePipe,
    private route: ActivatedRoute,
    public common: CommonService,
    private formBuilder: FormBuilder,
    public mapsAPILoader: MapsAPILoader,
    public toastr: ToastrService,
    private authService: SocialAuthService,
    public ngZone: NgZone,
    public Loader: NgxSpinnerService,
    private _messagingService: MessagingService
  ) {
    this.currentDate = new Date();
    this.searchElementRef = {} as ElementRef;
    this.appValForm = {} as FormGroup;
    this.errorsList = {} as IErrorList;
    this.postJobModal = {} as ModalDirective;
    this.verifyAccountMessageModal = {} as ModalDirective;
    this.blockAccountMessageModal = {} as ModalDirective;
    this.loggedUserDetails = {} as IPersonalDetails;
    this.currentDate.setDate(this.currentDate.getDate() - 4);
  }

  ngOnInit() {

    this.token = localStorage.getItem("auth_token");
    var decodedtoken = this.token != null ? this.jwtHelperService.decodeToken(this.token) : "";
    this.userId = decodedtoken.UserId
    let step1Data = localStorage.getItem("step1Data");
    this.quotationVM = step1Data != null ? JSON.parse(step1Data) : "";
    this.route.queryParams.subscribe((params: Params) => {
      this.Id = params['id'];
    });
    this.GetCities();
    this.startLocation();
    this.Validators();


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

    this.maxdate1 = this.year + "-" + this.month + "-" + this.day;
    this.errorsList = {
      cityIdError: JobQuotationErrors.cityError,
      townError: JobQuotationErrors.townError,
      areaError: JobQuotationErrors.areaError,
      numberOfBidsError: JobQuotationErrors.numberOfBidsError,
      startedDateError: JobQuotationErrors.startDateError,
      startedTimeError: JobQuotationErrors.startTimeError,
      budgetError: JobQuotationErrors.budgetError,
      streetAddressError: JobQuotationErrors.streetAddressError,
      budgetPatternError: JobQuotationErrors.budgetPatternError,
    }
    //this.getTownList();

    this.checkUserStatus();

  }

  public checkUserStatus() {

    this.common.GetData(this.common.apiRoutes.Login.GetUserBlockStatus + "?userId=" + this.userId).then(result => {
      this.isUserBlocked = result 
      if (!this.isUserBlocked) {
        this.getLocalStorageData();
      }
      else {
        this.isUserBlocked = true;
        this.blockAccountMessageModal.show();
        setTimeout(() => {
          this.isUserBlocked = false;
          this.blockAccountMessageModal.hide();
          this.logout();
        }, 5000);
      }
    }, error => {

      console.log(error);
    });

  }

  public logout() {
    this.loggedUserDetails = {} as IPersonalDetails;
    localStorage.clear();
    this.authService.signOut();
    this.loginCheck = false;
    window.location.href = '';
  }

  public hideBlockModal() {
    this.blockAccountMessageModal.hide();
    this.logout();
  }

  public getTownList(cId:number) {
    this.searchtownList = [];
    let cityId = cId.toString();
    this.common.GetData(this.common.apiRoutes.UserManagement.GetTownsByCityId + "?cityId=" + cityId, false).then(res => {
      this.townList = res;
      console.log(this.townList);
      this.townList.forEach((x) => {
        this.searchtownList.push({ value: x.name, id: x.townId });
      })
    });

  }

  public selectTownEvent(item: Event) {
    this.townChanged = true;
  }

  public unselectTownEvent(item: Event) {
  }

  public onChangetownSearch(event: Event) {
    // fetch remote data from here
    // And reassign the 'data' which is binded to 'data' property.
  }

  public JobDetail() {

    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.QuoteStep1, { queryParams: { id: "1" } });
  }

  focusOutFunction() {
    this.pipe = new DatePipe('en-US');
    this.date = this.pipe.transform(this.currentDate, 'MM/dd/yyyy');
    this.selecteddate = this.pipe.transform(this.appValForm.controls.startedDate.value.jsdate, 'MM/dd/yyyy');
    if (this.date > this.selecteddate) {
      this.appValForm.controls.startedDate.setValue("");
      this.isdate = true;
    }
    else {
      this.isdate = false;
    }
  }

  public Validators() {

    this.appValForm = this.formBuilder.group({
      firstName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      //emailAddress: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(40), Validators.pattern("^[A-Za-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]],
      emailAddress: ['', [Validators.pattern("^[A-Za-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^[0-9]*$/)]],
      cityId: [null, [Validators.required]],
      town: ['', [Validators.required]],
      area: ['', [Validators.required]],
      relationship: [null, [Validators.required]],
      streetAddress: ['', [Validators.required]],
    })
  }
  public getLocalStorageData() {
    let token = localStorage.getItem("auth_token");
    var decodedtoken = token != null ? this.jwtHelperService.decodeToken(token) : "";

    if (decodedtoken) {
      this.getLoggedUserDetails(decodedtoken.Role, decodedtoken.UserId);
    }
  }

  getLoggedUserDetails(userRole: string, userId: string) {

    this.common.get(this.common.apiRoutes.UserManagement.GetUserDetailsByUserRole + `?userRole=${userRole}&userId=${userId}`).subscribe(result => {
      this.loggedUserDetails = <IPersonalDetails>result;
      //this.SelectCity(this.loggedUserDetails.cityId);
      this.appValForm.controls.firstName.setValue(this.loggedUserDetails.firstName + ' ' + this.loggedUserDetails.lastName);
      this.appValForm.controls.emailAddress.setValue(this.loggedUserDetails?.email ? this.loggedUserDetails?.email : '');
      this.isEmail = this.loggedUserDetails?.email ? true : false ;
      this.appValForm.controls.phoneNumber.setValue(this.loggedUserDetails.mobileNumber);
      this.appValForm.controls.cityId.setValue(null);
      
    });
  }

  public GetCities() {
    this.common.GetData(this.common.apiRoutes.Common.getCityList, true).then(result => {
      this.cityList = result ;
    }, error => {
      console.log(error);
    })
  }

  public SelectCity(cityId: number) {
    var index = 0;
    while (this.cityList[index].id != cityId) {
      index++;
    }

  }
  public selectedCity(cityId: number) {
  
    console.log(cityId);
    this.getTownList(cityId);
    //let selectElementText = event != null && event.target != null ? (<HTMLSelectElement>event.target)['options'][(<HTMLSelectElement>event.target)['options'].selectedIndex].text : "";
    //this.cityName = selectElementText;
    //this.preventDefault(this.cityName)
  }

  get f() { return this.appValForm.controls; }

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
          console.log(place);
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
    this.latitude = lat;
    this.longitude = lng;
    this.setCurrentLocation(this.latitude, this.longitude);
  }

  public SearchLocation(location: any) {

    this.startLocation();
  }

  public Save() {
    if (this.appValForm.invalid) {
      
      return this.appValForm.markAllAsTouched();
    }
    else {
      let data = this.appValForm.value;
      console.log(data);
      this.btnQuote = true;
      if (this.token != null) {
        this.common.GetData(this.common.apiRoutes.IdentityServer.GetPhoneNumberVerificationStatus + "?userId=" + this.token, true).then(result => {
          if (result  == true) {
            this.PostJob();
          }
          else {
            this.verifyAccountMessageModal.show();
            this.btnQuote = false;
          }

        });
      }
    }
  }

  public closeVerifyAccountMessageModal() {
    this.verifyAccountMessageModal.hide();
    this.btnQuote = false;
  }


  public PostJob() {

    this.getQuotationsVM = this.appValForm.value;
    this.getQuotationsVM.statusId = BidStatus.Active;
    this.getQuotationsVM.addressLine = this.address;
    this.setDate = this.getQuotationsVM.startedDate;
    this.getQuotationsVM.jobQuotationId = this.Id ? this.Id: 0;
    this.getQuotationsVM.locationCoordinates = this.latitude + "," + this.longitude;
    this.getQuotationsVM.imageVMs = this.quotationVM.imageVMs;
    this.getQuotationsVM.jobDescription = this.quotationVM.jobDescription;
    this.getQuotationsVM.jobstartDateTime = this.quotationVM.startedDate;
    this.getQuotationsVM.jobStartTime = this.quotationVM.jobStartTime;
    this.getQuotationsVM.categoryId = this.quotationVM.categoryId;
    this.getQuotationsVM.subCategoryId = this.quotationVM.subCategoryId;
    this.getQuotationsVM.workTitle = this.quotationVM.workTitle;
    this.getQuotationsVM.budget = this.quotationVM.budget;
    this.getQuotationsVM.serviceCharges = this.quotationVM.serviceCharges;
    this.getQuotationsVM.visitCharges = this.quotationVM.visitCharges;
    this.getQuotationsVM.numberOfBids = 3;
    let townValue = this.appValForm.value.town;
    this.getQuotationsVM.townId = townValue.id;
    this.getQuotationsVM.town = this.townChanged ? townValue.value : "";
    let filterTown = this.searchtownList.filter(x => x.value == this.getQuotationsVM.town);
    if (filterTown.length <= 0) {
      this.btnQuote = false;
      this.townInvalidInput = true;
      this.appValForm.controls['town'].setErrors({ incorrect: true, inValidTown: 'Invalid town' })
      return;
    }

    if (this.latitude == this.jobAddressLatitude && this.longitude == this.jobAddressLongitude) {
      this.getQuotationsVM.streetAddress = this.defaultAddress;
    }
    else {
      this.getQuotationsVM.streetAddress = this.getQuotationsVM.area;
    }
    this.Loader.show();
    this.common.PostData(this.common.apiRoutes.Customers.JobQuotationsWeb, this.getQuotationsVM).then(result => {
      this.response = result;
      if (this.response.status == httpStatus.Ok) {
        //res.resultData.forEach((item: any) => {
        //  this._messagingService.sendMessage("New Job Posted", `${this.loggedUserDetails?.firstName} ${this.loggedUserDetails?.lastName} posted a new Job ${this.getQuotationsVM?.workTitle}`, item.firebaseId);
        //})
        localStorage.removeItem("step1Data");
        this.Loader.hide();
        this.common.NavigateToRoute(this.common.apiUrls.User.GetPostedJobs);
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
    //this.postJobModal.hide();
    //this.common.get(this.common.apiRoutes.Tradesman.GetTradesmanFirebaseIdListBySkillAndCity +
    //  `?categoryId=${this.getQuotationsVM.categoryId}&city=${this.cityName}`).subscribe((firbaseIdList) => {
    //    let res: ResponseVm = firbaseIdList;
    //    debugger;
    //    if (res.resultData) {
    //      this.getQuotationsVM.fireBaseIds = res.resultData;
    //    }
    //  })
  }

  public hideModal() {
    this.postJobModal.hide();
  }

  public verifyAccount() {
    this.common.NavigateToRoute(this.common.apiUrls.User.PersonalProfile);
  }

  //get latitude and longitude against user input location plesae dont delete. 
  //public preventDefault(address): Observable<any> {
  //  console.log('Getting address: ', address);
  //  let geocoder = new google.maps.Geocoder();
  //  return Observable.create(observer => {
  //    geocoder.geocode({
  //      'address': address
  //      'address': address
  //    }, (results, status) => {
  //      if (status == google.maps.GeocoderStatus.OK) {
  //        observer.next(results[0].geometry.location);
  //        observer.complete();
  //      } else {
  //        console.log('Error: ', results, ' & Status: ', status);
  //        observer.error();
  //      }
  //    });
  //  });
  //}

  //numberOnly(event): boolean {
  //  const charCode = (event.which) ? event.which : event.keyCode;
  //  if (charCode > 31 && (charCode < 48 || charCode > 57)) {
  //    return false;
  //  }
  //  return true;
  //}
  numberOnly(event: KeyboardEvent): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if ((charCode >= 96 && charCode <= 105) || (charCode >= 48 && charCode <= 57) || (charCode == 8))
      return true;
    else
      return false;
  }

  charOnly(event: KeyboardEvent): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if ((charCode > 47 && charCode < 58) || ((charCode > 32 && charCode <= 47)) || ((charCode >= 58 && charCode <= 64)) || ((charCode >= 91 && charCode <= 96)) || ((charCode >= 123 && charCode <= 126))) {
      return false;
    }
    return true;
  }

  AllowNonZeroIntegers(event: KeyboardEvent): boolean {

    var val = event.keyCode;
    // var target = event.target ? event.target : event.srcElement;
    if (val == 48 && (<HTMLInputElement>event.target).value == "" || val == 101 || val == 45 || val == 46 || ((val >= 65 && val <= 90)) || ((val >= 97 && val <= 122))) {
      return false;
    }
    else if ((val >= 48 && val < 58) || ((val > 96 && val < 106)) || val == 8 || val == 127 || val == 189 || val == 109 || val == 9) {
      return true;
    }
    else {
      return false;
    }
  }

}
