import { Component, OnInit, ViewChild, ElementRef, NgZone } from '@angular/core';
import { Customer, TrademanProfileVm } from '../../../models/userModels/userModels';
import { CommonService } from '../../../shared/HttpClient/_http';
import { httpStatus, CommonErrors } from '../../../shared/Enums/enums';
import { Params, ActivatedRoute } from '@angular/router';
import { MapsAPILoader, MouseEvent } from '@agm/core';

@Component({
  selector: 'app-tradesman-profile',
  templateUrl: './tradesmanProfile.component.html',

})
export class TradesmanProfileComponent implements OnInit {
  public tradesmanId: number=0;
  //public skillslist: [];
  public latitude: number=0;
  public longitude: number=0;
  public zoom: number = 15;
  public address: string="";
  public geoCoder = new google.maps.Geocoder();
  @ViewChild('search', { static: true })
  public searchElementRef: ElementRef;
  public profile: TrademanProfileVm = {} as  TrademanProfileVm;

  constructor(
    public common: CommonService,
    private route: ActivatedRoute,
    public mapsAPILoader: MapsAPILoader,
    public ngZone: NgZone
  ) {
    this.searchElementRef = {} as ElementRef;
  }

  ngOnInit() {
    this.route.queryParams.subscribe((params: Params) => {
      this.tradesmanId = params['tradesmanId'];
      if (this.tradesmanId > 0)

        this.PopulateData();
    });
  }

  public PopulateData() {
    var url = this.common.apiRoutes.Tradesman.GetTradesmanProfile + "?tradesmanId=" + this.tradesmanId + "&isActive=" + true;
    this.common.GetData(url, true).then(result => {
      if (status = httpStatus.Ok) {
        
        this.profile = result ;
        console.log(this.profile);
        if (this.profile.tradesmanProfileImg != null) {
          this.profile.tradesmanProfileImg = 'data:image/png;base64,' + this.profile.tradesmanProfileImg;
        }
        if (this.profile.gpsCoordinates != null) {
          var splits = this.profile.gpsCoordinates.split(",")
          if (splits.length) {
            this.latitude = parseFloat(splits[0]);
            this.longitude = parseFloat(splits[1]);
            this.getAddress(this.latitude, this.longitude);
          }
        }
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }


  public getAddress(latitude: number, longitude: number) {
    this.geoCoder.geocode({ 'location': { lat: latitude, lng: longitude } }, (results, status) => {
      if (status === 'OK') {
        if (results[0]) {
          this.zoom = 12;
          this.address = results[0].formatted_address;

          this.addMarker(this.address);
        } else {
          window.alert('No results found');
        }
      } else {
        window.alert('Geocoder failed due to: ' + status);
      }

    });
  }


    public addMarker(addressLine: string) {
    var googleMap;
    const element = document.getElementById('map');
    if (element!= null) {
      googleMap = new google.maps.Map(element, {
        center: { lat: this.latitude, lng: this.longitude },
        zoom: 16,
      });
    }

    var marker = new google.maps.Marker({
      position: { lat: this.latitude, lng: this.longitude },
      map: googleMap,
      title: addressLine
    });

    var infoWindow = new google.maps.InfoWindow({ content: addressLine });

    google.maps.event.addListener(marker, 'click', function () {
      infoWindow.open(this.googleMap, marker);
    });

    infoWindow.open(googleMap, marker);
  }

}

