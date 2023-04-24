import { Component, OnInit, ElementRef, NgZone, ViewChild } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { CommonService } from '../../../shared/HttpClient/_http';
import { httpStatus, CommonErrors } from '../../../shared/Enums/enums';
import { FinishedJobDetails } from '../../../models/userModels/userModels';
import { MapsAPILoader, MouseEvent } from '@agm/core';
import { TradesmanRatingsVM } from '../../../models/tradesmanModels/tradesmanModels';
import { StarRatingComponent } from 'ng-starrating';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ModalDirective } from 'ngx-bootstrap/modal';


@Component({
  selector: 'app-get-finished-job-detail',
  templateUrl: './detail.component.html',
})
export class FinishedJobDetailComponent implements OnInit {
  public jobDetailId: number=0;
  public submittedForm = false;
  public appValForm: FormGroup;
  latitude: number=0;
  longitude: number=0;
  zoom: number = 15;
  address: string="";
  public geoCoder = new google.maps.Geocoder();
  @ViewChild('search', { static: true })
  public searchElementRef: ElementRef;
  public jobDetail: FinishedJobDetails = {} as  FinishedJobDetails;
  public tradesmanRatingsVM: TradesmanRatingsVM = {} as TradesmanRatingsVM;
  @ViewChild('RateTradesman', { static: true }) RateTradesman: ModalDirective;

  constructor(
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    public common: CommonService,
    public mapsAPILoader: MapsAPILoader,
    public ngZone: NgZone
  ) {
    this.appValForm = {} as FormGroup;
    this.searchElementRef = {} as ElementRef;
    this.RateTradesman = {} as ModalDirective;
  }

  ngOnInit() {
    
    this.route.queryParams.subscribe((params: Params) => {
      this.jobDetailId = params['jobDetailId'];
      if (this.jobDetailId > 0)
        this.PopulateData();
    });
    this.appValForm = this.formBuilder.group({
      comments: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(250)]],
      overallRating: ['', Validators.required],
     
    });

  }
  public RateCommunication($event: { oldValue: number, newValue: number, starRating: StarRatingComponent }) {
    this.tradesmanRatingsVM.communicationRating = $event.newValue;

  }

  public RateServices($event: { oldValue: number, newValue: number, starRating: StarRatingComponent }) {
    this.tradesmanRatingsVM.qualityRating = $event.newValue;
  }
  get f() {
    return this.appValForm.controls;
  }
  public Save() {
    
    this.submittedForm = true;
    if (this.appValForm.invalid) {
      return;
    }
    
    var data = this.appValForm.value;
    this.tradesmanRatingsVM.jobDetailId = this.jobDetail.jobDetailId;
    this.tradesmanRatingsVM.tradesmanId = this.jobDetail.tradesmanId;
    this.tradesmanRatingsVM.overallRating = data.overallRating
    this.tradesmanRatingsVM.comments = data.comments
    this.common.PostData(this.common.apiRoutes.Users.PostTradesmanFeedback, this.tradesmanRatingsVM, true).then(result => {
      if (status = httpStatus.Ok) {
        //this.common.Notification.show("Feedback Posted successfully!");
        this.common.NavigateToRoute(this.common.apiUrls.User.GetFinishedJobs);
        //window.location.reload();
      }
    }, error => {

      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  
  }

  onMouseOver(infoWindow:any, $event: MouseEvent) {
    infoWindow.open();
  }

  public PopulateData() {
    
    var urls = this.common.apiRoutes.Jobs.GetFinishedJobDetail + "?jobDetailId=" + this.jobDetailId;
    
    this.common.GetData(urls, true).then(result => {
      
      if (status = httpStatus.Ok) {
        this.jobDetail = result ;
        console.log(this.jobDetail);
        if (this.jobDetail.overallRating == 0) {
          this.openModel();
        }
        this.jobDetail.tradesmanProfileImage = 'data:image/png;base64,' + this.jobDetail.tradesmanProfileImage;
        if (this.jobDetail.tradesmanProfileImage == 'data:image/png;base64,null') {
          this.jobDetail.tradesmanProfileImage = "";
        }
        console.log(this.jobDetail.jobAddress);
        var splits = this.jobDetail.latLng?.split(",")
        if (splits?.length) {
          console.log(this.jobDetail.latLng);
          this.latitude = parseFloat(splits[0]);
          this.longitude = parseFloat(splits[1]);

          this.getAddress(this.latitude, this.longitude);

        }
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }

  public ViewProfile(tradesmanId: number) {
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.TradesmanProfile, { queryParams: { tradesmanId: tradesmanId } });

  }

  //Test Maps

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
    var googleMap:any;
    const element = document.getElementById('map');
    if (element != null) {
      googleMap= new google.maps.Map(element, {
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
      infoWindow.open(googleMap, marker);
    });

    infoWindow.open(googleMap, marker);
  }

  public openModel() {
    this.RateTradesman.show();
  }
  public closeModel() {
    this.RateTradesman.hide();
  }
}

