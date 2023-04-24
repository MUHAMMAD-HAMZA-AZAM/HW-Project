import { Component, OnInit, NgZone, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { CommonService } from '../../../shared/HttpClient/_http';
import { MapsAPILoader, MouseEvent } from '@agm/core';
import { httpStatus, BidStatus } from '../../../shared/Enums/enums';
import { CompletedJobDetailsVM } from '../../../models/tradesmanModels/tradesmanModels';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-reviewe',
  templateUrl: './reviewe.component.html',
  styleUrls: ['./reviewe.component.css']
})
export class RevieweComponent implements OnInit {
  public jobDetailId: number=0;
  public jobdetail: CompletedJobDetailsVM = {} as CompletedJobDetailsVM;
  public latitude: number=0;
  public longitude: number=0;
  public googleMap: any;
  public zoom: number = 15;
  @ViewChild('CallBackModel', { static: true }) CallBackModel: ModalDirective;
  @ViewChild('FeedbackModel', { static: true }) FeedbackModel: ModalDirective;

  constructor(private route: ActivatedRoute, private common: CommonService, public mapsAPILoader: MapsAPILoader,
    public ngZone: NgZone) {
    this.CallBackModel = {} as ModalDirective;
    this.FeedbackModel = {} as ModalDirective;
  }

  ngOnInit() {
    this.route.queryParams.subscribe((param: Params) => {
      this.jobDetailId = param['jobDetailId'];
      if (this.jobDetailId > 0)
        this.PopulateData();
    })
  }

  public PopulateData() {
    this.common.GetData(this.common.apiRoutes.Tradesman.GetCompletedJob + "?jobDetailId=" + this.jobDetailId, true).then(data => {
      
      this.jobdetail = data ;
      if (this.jobdetail.latLong != null) {
        var splits = this.jobdetail.latLong.split(",")
        if (splits.length) {
          this.latitude = parseFloat(splits[0]);
          this.longitude = parseFloat(splits[1]);
          this.getAddress(this.latitude, this.longitude);
          const map = document.getElementById('map');
          if (map!=null) {
            this.googleMap = new google.maps.Map(map, {
              center: { lat: this.latitude, lng: this.longitude },
              zoom: 16,
            });
          }

        }
      }
    },
      error => {
        console.log(error);
      });
  }
  public geoCoder = new google.maps.Geocoder();
  public address: string="";
  public getAddress(latitude: number, longitude: number) {
    this.geoCoder.geocode({ 'location': { lat: latitude, lng: longitude } }, (results, status) => {
      if (status === 'OK') {
        if (results[0]) {
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
  public CallBackPopup(QuoteID: number, customerId: number) {
    this.common.GetData(this.common.apiRoutes.Tradesman.NotificationCallRequest + "?jobQuotationId=" + QuoteID + "&customerId" + customerId, true).then(data => {
      var result = data;
      if (result)
        this.CallBackModel.show();
    },
      error => {
        console.log(error);
      });

  }

  public CloseCallBack() {
    this.CallBackModel.hide();
  }

  public FeedbackPopup() {
    this.common.GetData(this.common.apiRoutes.Tradesman.FeedbackRequest + "?jobDetailId=" + this.jobDetailId, true).then(data => {
      var result = data;
      console.log(result);
      if (result)
        this.FeedbackModel.show();
    },
      error => {
        console.log(error);
      });

  }

  public CloseFeedback() {
    this.FeedbackModel.hide();
  }

  addMarker(addressLine: string) {
    var marker = new google.maps.Marker({
      position: { lat: this.latitude, lng: this.longitude },
      map: this.googleMap,
      title: addressLine
    });

    var infoWindow = new google.maps.InfoWindow({ content: addressLine });

    google.maps.event.addListener(marker, 'click', function () {
      infoWindow.open(this.googleMap, marker);
    });

    infoWindow.open(this.googleMap, marker);
  }
}
