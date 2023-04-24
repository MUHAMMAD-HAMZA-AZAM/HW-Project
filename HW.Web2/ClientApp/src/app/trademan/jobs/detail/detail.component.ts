import { Component, OnInit, ViewChild, NgZone } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { CommonService } from '../../../shared/HttpClient/_http';
import { httpStatus, BidStatus } from '../../../shared/Enums/enums';
import { jobDetails, MarkAsFinished } from '../../../models/tradesmanModels/tradesmanModels';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { MouseEvent, MapsAPILoader, InfoWindowManager } from '@agm/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ResponseVm } from '../../../models/commonModels/commonModels';
import { Events } from '../../../common/events';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class JobDetailComponent implements OnInit {
  public jobId: number=0;
  public jobdetail: jobDetails = {} as jobDetails;
  public markAsFinished: MarkAsFinished = {} as MarkAsFinished;
  public response: ResponseVm = {} as ResponseVm;
  public imageContent: ArrayBuffer;
  public latitude: number=0;
  public longitude: number=0;
  public zoom: number = 18;
  public hasNoImage: boolean = false;
  public slideIndex: number = 1;
  public tradesmanId: number=0;
  public jwtHelperService: JwtHelperService = new JwtHelperService();;
  @ViewChild('ImageModel', { static: true }) ImageModel: ModalDirective;
  @ViewChild('CallBackModel', { static: true }) CallBackModel: ModalDirective;
  @ViewChild('MarkFinishModel', { static: true }) MarkFinishModel: ModalDirective;
  public audio: any="";
  public show: boolean = false;


  constructor(public event:Events, private loader: NgxSpinnerService, private route: ActivatedRoute, public common: CommonService, public mapsAPILoader: MapsAPILoader,
    public ngZone: NgZone) {
    this.imageContent = {} as ArrayBuffer;
    this.ImageModel = {} as ModalDirective;
    this.CallBackModel = {} as ModalDirective;
    this.MarkFinishModel = {} as ModalDirective;
  }

  ngOnInit() {
    this.localStorageUserData();
    this.route.queryParams.subscribe((param: Params) => {
      this.jobId = param['jobDetailId'];
      if (this.jobId == 0)
        this.jobId = 0;
      else
        this.PopulateData(this.jobId);
    })
  }
  public localStorageUserData() {
    var token = localStorage.getItem("auth_token");
    var decodedtoken = token != null ? this.jwtHelperService.decodeToken(token):"";
    this.tradesmanId = decodedtoken.Id;
  }
  public geoCoder = new google.maps.Geocoder();
  public address: string = "";
  public getAddress(latitude: number, longitude: number) {
    this.geoCoder.geocode({ 'location': { lat: latitude, lng: longitude } }, (results, status) => {
      if (status === 'OK') {
        if (results[0]) {
          this.address = results[0].formatted_address;
          this.show = true;
          this.addMarker(this.address);
        } else {
          window.alert('No results found');
        }
      } else {
        window.alert('Geocoder failed due to: ' + status);
      }

    });
  }

  public PopulateData(jobId: number) {
    this.common.GetData(this.common.apiRoutes.Tradesman.GetJobDetailsByIdWeb + "?jobDetailId=" + jobId, true).then(data => {
      //var result = data ;
      this.jobdetail = data ;
      this.audio = this.jobdetail.audioMessage;
      if (this.audio != null) {
        this.jobdetail.audioMessage = 'data:audio/mp3;base64,' + this.audio.audioContent;
      }
      if (this.jobdetail.latLong != null) {
        var splits = this.jobdetail.latLong.split(",")
        if (splits.length) {
          this.latitude = parseFloat(splits[0]);
          this.longitude = parseFloat(splits[1]);
          this.getAddress(this.latitude, this.longitude);
        }
      }
    },
      error => {
        console.log(error);
      });
  }


  public CompleteJob() {
    
    this.loader.show();
    let obj = {
      JobDetailsId :this.jobdetail.jobDetailId,
      action:"finish"
    }
    this.common.post(this.common.apiRoutes.Jobs.StartOrFinishJob, obj).subscribe(response => {
      
      let res = <any>response ;
      if (res.status == httpStatus.Ok) {
        this.MarkFinishModel.hide();
        this.common.get(this.common.apiRoutes.Notifications.NotifyJobFinishFromTradesman + `?JobQuotationId=${this.jobdetail.jobQuotationId}`).subscribe(response => {
          this.PopulateData(this.jobId);
        })
        this.loader.hide;
      }
    })
    //this.markAsFinished.BidId = this.jobdetail.bidId;
    //this.markAsFinished.CustomerId = this.jobdetail.customerId;
    //this.markAsFinished.EndDate = new Date();
    //this.markAsFinished.isPaymentDirect = true;
    //this.markAsFinished.JobDetailId = this.jobdetail.jobDetailId;
    //this.markAsFinished.JobQuotationId = this.jobdetail.jobQuotationId;
    //this.markAsFinished.StartDate = this.jobdetail.expectedJobStartDate;
    //this.markAsFinished.StatusId = BidStatus.Completed;
    //this.markAsFinished.TradesmanId = this.jobdetail.tradesmanBid;

    //this.common.get(this.common.apiRoutes.Jobs.UpdateBidStatus + `?BidId=${this.markAsFinished.BidId}&statusId=${BidStatus.Completed}`).subscribe(result => {
    //  this.response = result ;
    //  if (this.response.status == httpStatus.Ok) {
    //    this.common.PostData(this.common.apiRoutes.Tradesman.MarkAsFinishedJob, this.markAsFinished).then(data => {
    //      var result = data ;
    //      if (data.status == httpStatus.Ok) {
    //        //this.MarkFinishModel.hide();
    //        this.loader.hide();
    //        this.common.Notification.show("Job completed successfully!");
    //        this.common.NavigateToRoute(this.common.apiUrls.Tradesman.MyJobs);
    //        localStorage.setItem("cFlag", "1");
    //      }
    //    },
    //      error => {
    //        console.log(error);
    //      });
    //  }
    //}, error => {
    //  console.log(error);
    //});
    //  this.event.update_Trademan_Credits.emit(this.tradesmanId);
  }

  public ImagePopUp(image: ArrayBuffer) {
    this.imageContent = image;
    this.ImageModel.show()

  }
  public CallBackPopup(QuoteID: number, customerId: number) {
    
    this.common.GetData(this.common.apiRoutes.Tradesman.NotificationCallRequest + "?jobQuotationId=" + QuoteID + "&customerId" + customerId, true).then(data => {
      var result = data;
      if (result.status == httpStatus.Ok)
        this.CallBackModel.show();
    },
      error => {
        console.log(error);
      });

  }
  public OpenMarkFinish() {
    this.MarkFinishModel.show()
      //this.CompleteJob();
  }

  public Close() {
    this.ImageModel.hide();
  }
  public CloseCallBack() {
    this.CallBackModel.hide();
  }

  public CloseMarkFinish() {
    this.MarkFinishModel.hide();
  }

  public markerClicked(infowindow: Event) {
    this.show = true;
  }
  public hideModal() {
    this.MarkFinishModel.hide();
  }

  addMarker(addressLine: string) {
    var googleMap;
    const map = document.getElementById('map');
    if (map) {
       googleMap = new google.maps.Map(map, {
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

  public openModal() {
    const element = document.getElementById("myModalLightbox");
    if (element != null) {
      element.style.display = "block";
    }
  }

  public closeModal() {
    const element = document.getElementById("myModalLightbox");
    if (element != null) {
      element.style.display = "none";
    }
  }


  public plusSlides(n: number) {
    this.showSlides(this.slideIndex += n);
  }

  public currentSlide(n: number) {
    this.showSlides(this.slideIndex = n);
  }

  public showSlides(n: number) {
    var slides = document.getElementsByClassName("lightboxImg");

    if (n > slides.length) { this.slideIndex = 1 }
    if (n < 1) { this.slideIndex = slides.length }

    let img: any = slides[this.slideIndex - 1];
    this.imageContent = img.src;
  }

}
