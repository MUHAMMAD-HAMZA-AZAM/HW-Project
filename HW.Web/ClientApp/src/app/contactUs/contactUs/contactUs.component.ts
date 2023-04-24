import { Component, OnInit, ViewChild, NgZone, ElementRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '../../shared/HttpClient/_http';
import { ActivatedRoute } from '@angular/router';
import { httpStatus } from '../../shared/Enums/enums';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { MapsAPILoader, MouseEvent } from '@agm/core';
@Component({
  selector: 'app-contact-us',
  templateUrl: './contactUs.component.html',
  styleUrls: ['./contactUs.component.css']
})
export class ContactUsComponent implements OnInit {
  public submittedForm = false;
  public appValForm: FormGroup;
  public latitude = 31.4728;
  public longitude = 74.3278;
  public zoom: number = 15;
  public address: string;
  public geoCoder = new google.maps.Geocoder();
  @ViewChild('search', { static: true })
  public searchElementRef: ElementRef;
  @ViewChild('ContactSms', { static: true }) ContactSms: ModalDirective;

  constructor(
    public common: CommonService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    public mapsAPILoader: MapsAPILoader,
    public ngZone: NgZone
  ) { }

  ngOnInit() {
    this.appValForm = this.formBuilder.group({
      name: ['', Validators.required],
      phone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      subject: ['', Validators.required],
      message: ['', Validators.required],
    });
    this.startLocation();
  }
  get f() {
    return this.appValForm.controls;
  }
  public Save() {
    debugger
    this.submittedForm = true;
    if (this.appValForm.invalid) {
      return;
    }
    var data = this.appValForm.value;
    this.common.PostData(this.common.apiRoutes.Communication.SendUsAMessage, data, true).then(result => {
      debugger;
      if (status = httpStatus.Ok) {
        this.ContactSms.show();
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    });
  }

  public Close() {
    this.ContactSms.hide();
    this.appValForm.reset();
    this.appValForm = this.formBuilder.group({
      name: ['', Validators.required],
      phone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      subject: ['', Validators.required],
      message: ['', Validators.required],
    });
    this.submittedForm = false;
  }
  //public GetAddress(latitude, longitude) {
  //  debugger;
  //  this.geoCoder.geocode({ 'location': { lat: latitude, lng: longitude } }, (results, status) => {
  //    if (status === 'OK') {
  //      if (results[0]) {
  //        this.zoom = 12;
  //        this.address = results[0].formatted_address;
  //      } else {
  //        this.common.Notification.warning('No results found');
  //      }
  //    } else {
  //      this.common.Notification.error('Geocoder failed due to: ' + status);
  //    }

  //  });
  //}
  public setCurrentLocation(latitude, longitude) {
    debugger
    if (latitude != undefined && longitude != undefined) {
      this.latitude = latitude;
      this.longitude = longitude;
      debugger
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
    this.latitude = $event.coords.lat;
    this.longitude = $event.coords.lng;
    this.getAddress(this.latitude, this.longitude);
  }

  public getAddress(latitude, longitude) {
    debugger;
    this.geoCoder.geocode({ 'location': { lat: latitude, lng: longitude } }, (results, status) => {
      if (status === 'OK') {
        if (results[0]) {
          this.zoom = 12;
          this.address = results[0].formatted_address;
        } else {
          this.common.Notification.warning('No results found');
        }
      } else {
        this.common.Notification.error('Geocoder failed due to: ' + status);
      }

    });
  }

  public startLocation() {
    debugger
    this.mapsAPILoader.load().then(() => {
      this.setCurrentLocation(this.latitude, this.longitude);
      //this.geoCoder = new google.maps.Geocoder;
      //console.log(this.searchElementRef.nativeElement);
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

  public addMarker(lat, lng) {
    debugger;
    this.latitude = lat;
    this.longitude = lng;
    this.setCurrentLocation(this.latitude, this.longitude);
  }
}
