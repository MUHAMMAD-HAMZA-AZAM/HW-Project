import { Component, OnInit, ViewChild, ElementRef, NgZone } from '@angular/core';
import { Customer, TrademanProfileVm } from '../../../models/userModels/userModels';
import { CommonService } from '../../../shared/HttpClient/_http';
import { httpStatus } from '../../../shared/Enums/enums';
import { Params, ActivatedRoute } from '@angular/router';
import { MapsAPILoader, MouseEvent } from '@agm/core';


@Component({
  selector: 'app-tradesman-profile',
  templateUrl: './tradesmanProfile.component.html',

})
export class TradesmanProfileComponent implements OnInit {
  public tradesmanId: number;
  latitude: number;
  longitude: number;
  zoom: number = 15;
  address: string;
  public geoCoder = new google.maps.Geocoder();
  @ViewChild('search', { static: true })
  public searchElementRef: ElementRef;

  public profile: TrademanProfileVm = new TrademanProfileVm();
  constructor(
    public common: CommonService,
    private route: ActivatedRoute,
    public mapsAPILoader: MapsAPILoader,
    public ngZone: NgZone
  ) { }

  ngOnInit() {
    this.route.queryParams.subscribe((params: Params) => {
      this.tradesmanId = params['tradesmanId'];
      if (this.tradesmanId > 0)

        this.PopulateData();
    });
  }

  public PopulateData() {
    debugger;
    var url = this.common.apiRoutes.Tradesman.GetTradesmanProfile + "?tradesmanId=" + this.tradesmanId + "&isActive=" + true;
    this.common.GetData(url, true).then(result => {
      debugger;
      if (status = httpStatus.Ok) {
        this.profile = result.json();
        if (this.profile.tradesmanProfileImg != null) {
          this.profile.tradesmanProfileImg = 'data:image/png;base64,' + this.profile.tradesmanProfileImg;
        }
        if (this.profile.gpsCoordinates != null) {
          var splits = this.profile.gpsCoordinates.split(",")
          this.latitude = parseFloat(splits[0]);
          this.longitude = parseFloat(splits[1]);
        }
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    });
  }

  GetAddress(latitude, longitude) {
    debugger;
    this.geoCoder.geocode({ 'location': { lat: latitude, lng: longitude } }, (results, status) => {
      if (status === 'OK') {
        if (results[0]) {
          this.zoom = 12;
          this.address = results[0].formatted_address;
        } else {
          window.alert('No results found');
        }
      } else {
        window.alert('Geocoder failed due to: ' + status);
      }

    });
  }

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

  markerDragEnd($event: MouseEvent) {
    console.log($event);
    this.latitude = $event.coords.lat;
    this.longitude = $event.coords.lng;
    this.getAddress(this.latitude, this.longitude);
  }

  getAddress(latitude, longitude) {
    debugger;
    this.geoCoder.geocode({ 'location': { lat: latitude, lng: longitude } }, (results, status) => {
      if (status === 'OK') {
        if (results[0]) {
          this.zoom = 12;
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

  addMarker(lat, lng) {
    debugger;
    this.latitude = lat;
    this.longitude = lng;
    this.setCurrentLocation(this.latitude, this.longitude);
  }

}

