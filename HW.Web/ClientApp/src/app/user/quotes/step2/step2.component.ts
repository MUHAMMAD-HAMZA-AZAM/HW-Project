import { Component, OnInit, ViewChild, Input, ElementRef, NgZone } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IdValueVm, ResponseVm } from '../../../models/commonModels/commonModels';
import { MapsAPILoader } from '@agm/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Observable } from 'rxjs';
import { GetQuotes } from '../../../models/userModels/userModels';
import { JobQuotationErrors, BidStatus, httpStatus } from '../../../shared/Enums/enums';
import { ModalDirective } from 'ngx-bootstrap';

@Component({
  selector: 'step2',
  templateUrl: './step2.component.html',
  styleUrls: ['./step2.component.css']
})
export class Step2 implements OnInit {
  public cityList: IdValueVm[] = [];
  time = { hour: 13, minute: 30 };
  meridian = true;
  public latitude: number;// = 30.3753;
  public longitude: number;// = 69.3451;
  public zoom: number = 15;
  address: string;
  public Id: number;
  public geoCoder = new google.maps.Geocoder();
  @ViewChild('search', { static: true })
  public searchElementRef: ElementRef;
  public getQuotationsVM: GetQuotes = new GetQuotes();
  public jobQuotationForm2: FormGroup;
  public errorsList: any;
  public setDate: any;
  public setTime: any;
  public submitted: boolean = false;
  public locationError: boolean = false;
  public showModal: boolean = false;
  public response: ResponseVm = new ResponseVm();
  @ViewChild('postJobModalConfirm', { static: true }) postJobModal: ModalDirective;

  constructor(
    private route: ActivatedRoute,
    private service: CommonService,
    private formBuilder: FormBuilder,
    public mapsAPILoader: MapsAPILoader,
    public ngZone: NgZone
  ) { }

  ngOnInit() {
    
    this.route.queryParams.subscribe((params: Params) => {
      this.Id = params['id'];
    });
    this.service.get(this.service.apiRoutes.Common.getCityList).subscribe(result => {
      this.cityList = result.json();
    })
    this.startLocation();
    
    this.Validators();

  }

  Validators() {
    this.jobQuotationForm2 = this.formBuilder.group({
      cityId: ['', Validators.required],
      town: ['', Validators.required],
      area: ['', Validators.required],
      numberOfBids: ['', Validators.required],
      startedDate: ['', Validators.required],
      startTime: ['', Validators.required],
      budget: ['', Validators.required],
    })
  }

  get f() { return this.jobQuotationForm2.controls; }

  SelectCity(cityId) {
    var index = 0;
    while (this.cityList[index].id != cityId) {
      index++;
    }
   
  }

  public setCurrentLocation(latitude, longitude) {
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

  markerDragEnd($event: MouseEvent) {
    console.log($event);
    this.getAddress(this.latitude, this.longitude);
  }

  getAddress(latitude, longitude) {
    this.geoCoder.geocode({ 'location': { lat: latitude, lng: longitude } }, (results, status) => {
      if (status === 'OK') {
        if (results[0]) {
          this.address  = results[0].formatted_address;
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

  addMarker(lat, lng) {
    this.latitude = lat;
    this.longitude = lng;
    this.setCurrentLocation(this.latitude, this.longitude);
  }

  SearchLocation(location) {
    
    this.startLocation();
  }

  //get latitude and longitude against user input location plesae dont delete. 
  //preventDefault(address):Observable<any> {
  //  
  //  console.log('Getting address: ', address);
  //  let geocoder = new google.maps.Geocoder();
  //  return Observable.create(observer => {
  //    geocoder.geocode({
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
  toggleMeridian() {
    this.meridian = !this.meridian;
  }

  toggleSpinner() {

  }
  PostQuotations() {
    this.errorsList = {
      cityIdError: JobQuotationErrors.cityError,
      townError: JobQuotationErrors.townError,
      areaError: JobQuotationErrors.areaError,
      numberOfBidsError: JobQuotationErrors.numberOfBidsError,
      startedDateError: JobQuotationErrors.startDateError,
      startedTimeError: JobQuotationErrors.startTimeError,
      budgetError: JobQuotationErrors.budgetError,
      streetAddressError: JobQuotationErrors.streetAddressError,
    }
    this.submitted = true;
    this.getQuotationsVM = this.jobQuotationForm2.value;
    this.getQuotationsVM.statusId = BidStatus.Active;
    if (this.address == null) {
      this.locationError = true;
      return;
    }
    this.getQuotationsVM.addressLine = this.address;
    if (this.jobQuotationForm2.valid) {
      this.postJobModal.show();
    }
  }
  PostJob() {
    
    this.setDate = this.getQuotationsVM.startedDate;
    this.setTime = this.getQuotationsVM.startTime;
    this.getQuotationsVM.streetAddress = this.getQuotationsVM.area;
    var strdate = this.setDate.formatted + " " + this.setTime.hour + ":" + this.setTime.minute + ":" + this.setTime.second;
    this.getQuotationsVM.jobstartDateTime = new Date(strdate);
    this.getQuotationsVM.jobQuotationId = this.Id;
    this.getQuotationsVM.locationCoordinates = this.latitude + "," + this.longitude;
    this.service.post(this.service.apiRoutes.Customers.JobQuotationsWeb, this.getQuotationsVM).subscribe(result => {
      this.response = result.json();
      if (this.response.status == httpStatus.Ok) {
        this.service.NavigateToRoute(this.service.apiUrls.User.GetPostedJobs);
      }
    });
    this.postJobModal.hide();
  }

  hideModal() {
    this.postJobModal.hide();
  }


  


}
