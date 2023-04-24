import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../shared/HttpClient/_http';
import { NavigationExtras, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { IdValueVm } from '../../models/commonModels/commonModels';
import { JwtHelperService } from '@auth0/angular-jwt';
import { error } from '@angular/compiler/src/util';
import { BidStatus } from '../../shared/Enums/enums';
import { IWebLiveLeads } from '../../shared/Interface/tradesman';
import { IPageSeoVM } from '../../shared/Enums/Interface';
import { metaTagsService } from '../../shared/CommonServices/seo-dynamictags.service';

declare let OverlappingMarkerSpiderfier: any;
@Component({
  selector: 'app-liveleads',
  templateUrl: './liveleads.component.html',
  styleUrls: ['./liveleads.component.css']
})

export class LiveleadsComponent implements OnInit {
  //public LeadsList; 
  public activeWebLiveLeads: IWebLiveLeads[] = [];
  public inProgressWebLiveLeads: IWebLiveLeads[] = [];
  public completedWebLiveLeads: IWebLiveLeads[] = [];
  public activePageNumber: number = 1;
  public inprogressPageNumber: number = 1;
  public completedPageNumber: number = 1;
  public pageSize = 21;
  public count = 1;
  public totalActiveLeads: number = 0;
  public totalInProgressLeads: number = 0;
  public totalCompletedLeads: number = 0;
  public page: number = 0;
  public latitude: number = 0;// = 30.3753;
  public longitude: number = 0;// = 69.3451;
  public zoom: number = 15;
  public address: string = "";
  public Id: number = 0;
  //public geoCoder = new google.maps.Geocoder();
  //public google: any;
  //public googleMap: any;
  public addInfoWindow: any;
  public active: number = BidStatus.Active;
  public inProgress: number = BidStatus.Accepted;
  public completed: number = BidStatus.Completed;
  public statusId: number = BidStatus.Active;
  public activeWebLiveLeadsBit: boolean = false;
  public inprogressWebLiveLeadsBit: boolean = false;
  public completedWebLiveLeadsBit: boolean = false;
  public jwtHelperService: JwtHelperService = new JwtHelperService();

  constructor(public service: CommonService, private router: Router, public Loader: NgxSpinnerService, private _metaService: metaTagsService) {

  }

  ngOnInit() {
    this.activeLeads();
    this.bindMetaTags();
  }

  //LoadMap() {

  //  this.service.get(this.service.apiRoutes.Jobs.WebLiveLeadsLatLong).subscribe(result => {

  //    var bounds = new google.maps.LatLngBounds();
  //    var markers = <any>result ;
  //    var googleMap: any;
  //    var cityLatLong = { lat: parseFloat('31.5204'), lng: parseFloat('74.3587') };
  //    const map = document.getElementById('map');
  //    if (map != null) {
  //      googleMap = new google.maps.Map(map, {
  //        center: cityLatLong,
  //        zoom: 12,
  //        mapTypeId: google.maps.MapTypeId.ROADMAP,
  //        zoomControl: true
  //      });

  //    }
  //    var oms = new OverlappingMarkerSpiderfier(googleMap,
  //      { markersWontMove: true, markersWontHide: true, keepSpiderfied: true, circleSpiralSwitchover: 40 });


  //    for (var latlong of markers) {
  //      var latLong = new Array();

  //      if (latlong.value != "") {
  //        latLong = latlong.value.split(',');
  //      }

  //      var myLatLng = { lat: parseFloat(latLong[0]), lng: parseFloat(latLong[1]) };

  //      var icon = {
  //        url: "../../../../assets/img/hoomwork_location_pin.png", // url
  //        scaledSize: new google.maps.Size(40, 40), // scaled size
  //        origin: new google.maps.Point(0, 0), // origin
  //        anchor: new google.maps.Point(0, 0) // anchor
  //      };

  //      let marker = new google.maps.Marker({
  //        position: myLatLng,
  //        map: googleMap,
  //        title: 'test',
  //        icon: icon,
  //      });

  //      marker.set("id", latlong.id);

  //      oms.addMarker(marker);
  //    }

  //    var infoWindow = new google.maps.InfoWindow();
  //    let srv = this.service;

  //    oms.addListener('click', function (marker: any, event: Event) {

  //      var loadingHtml = "";

  //      loadingHtml += "<div class='card'  style='width:300px; height:180px;'>";
  //      loadingHtml += "<div class='card-body' style='display: flex;align-items: center;justify-content: center;'>";
  //      loadingHtml += "<div class='loader'></div>";
  //      loadingHtml += "</div>";
  //      loadingHtml += "</div>";

  //      infoWindow.setContent(loadingHtml);
  //      let id = marker.get("id");
  //      let url = srv.apiRoutes.Jobs.WebLiveLeads + "?jobQuotationId=" + id;

  //      srv.get(url).subscribe(result => {
  //        var res = <any>result ;
  //        if (res.length > 0) {

  //          let html = "";
  //          var job = res[0];

  //          let date = new Date(job.createdOn);
  //          let formatted_date = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate()

  //          var parts = job.workBudget.toString().split(".");
  //          parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");

  //          html += "<div class='card'  style='width:300px'>";
  //          html += "<div class='card-body'>";
  //          html += "<div class='row'>";
  //          html += "<div class='col-lg-4'>";
  //          html += "<div>";
  //          if (job.jobImage != null) {

  //            html += "<img style='height:70px;width:100%;object-fit: cover;' src='data:image/jpg;base64," + job.jobImage + "'/>";
  //          }
  //          else {
  //            html += "<img style='height:70px;width:100%;object-fit: cover;' src='../../../../assets/img/no_thumbnail.jpg'/>";
  //          }
  //          html += "</div>";
  //          html += "</div>";
  //          html += "<div class='col-lg-8'>";
  //          html += "<div class='row'>";
  //          html += "<div class='col-lg-7'>";
  //          html += "<div class='row'>";
  //          html += "<h5 class='username'><small>" + job.workTitle + "</small></h5>";
  //          html += "</div>";
  //          html += "</div>";//col-lg-5
  //          html += "<div class='col-lg-8'><small>Rs. " + parts.join(".") + "</small></div>";
  //          html += "</div>";
  //          html += "<div class='row'>";
  //          html += "<p class='card-text userlocation'><i class='fas fa-map-marker-alt fa-fw'></i><small>" + " " + "" + job.area + ", " + job.cityName + "</small></p>";
  //          html += "<p class='card-text'><i class='far fa-calendar-alt fa-fw'></i><small> " + formatted_date + "</small></p>";
  //          html += "</div></div></div>";
  //          //row

  //          html += "</div>";
  //          //col - lg - 9
  //          //html += "<div class='card-footer'><button type='button' class='btn btn-sm btn-outline-success' data-toggle='modal' data-target='#downloadapp'>Bid Now</button></div>";
  //          html += "</div></div>";


  //          infoWindow.setContent(html);
  //        }
  //        else {
  //          var loadingHtml = "";

  //          loadingHtml += "<div class='card'  style='width:300px; height:180px;'>";
  //          loadingHtml += "<div class='card-body' style='display: flex;align-items: center;justify-content: center;'>";
  //          loadingHtml += "<h5>Data not found.</h5>";
  //          loadingHtml += "</div>";
  //          loadingHtml += "</div>";


  //          infoWindow.setContent(loadingHtml);
  //        }
  //      });

  //      infoWindow.open(googleMap, marker);
  //    });

  //    oms.addListener('spiderfy', function (markers: any) {
  //      infoWindow.close();
  //    });

  //  });
  //}

  // ------------------------ To Show Active Live Leads -------------------------------// 
  public activeLeads() {
    this.activePageNumber = 1;
    this.statusId = BidStatus.Active;
    this.service.GetData(this.service.apiRoutes.Jobs.WebLiveLeadsPanel + `?statusId=${this.statusId}&pageNumber=${this.activePageNumber}&pageSize=${this.pageSize}`, true).then(result => {
      this.activeWebLiveLeads = [];
      this.activeWebLiveLeads = result ;
      if (this.activeWebLiveLeads.length ==0) {
        this.activeWebLiveLeadsBit = true;
      }
      this.totalActiveLeads = this.activeWebLiveLeads[0].totalJobs;
      this.Loader.hide();

    }, error => {
      console.log(error);
    });
  }


  public getActiveJobsByPage(page: number) {
    ;
    this.activePageNumber = page;
    this.statusId = BidStatus.Active;
    this.service.GetData(this.service.apiRoutes.Jobs.WebLiveLeadsPanel + `?statusId=${this.statusId}&pageNumber=${this.activePageNumber}&pageSize=${this.pageSize}`, true).then(result => {
      this.activeWebLiveLeads = [];
      this.activeWebLiveLeads = result ;
      if (this.activeWebLiveLeads.length==0) {
        this.activeWebLiveLeadsBit = true;
      }
      this.totalActiveLeads = this.activeWebLiveLeads[0].totalJobs;
      this.activePageNumber = page;
      this.Loader.hide();

    }, error => {
      console.log(error);
    });
  }

  // ------------------------ To Show Inprogress Live Leads -------------------------------// 
  public inProgressLeads() {
    this.inprogressPageNumber = 1;
    this.statusId = BidStatus.Accepted;
    this.service.GetData(this.service.apiRoutes.Jobs.WebLiveLeadsPanel + `?statusId=${this.statusId}&pageNumber=${this.inprogressPageNumber}&pageSize=${this.pageSize}`, true).then(result => {

      this.inProgressWebLiveLeads = [];
      this.inProgressWebLiveLeads = result ;
      
      if (this.inProgressWebLiveLeads.length == 0) {
        this.inprogressWebLiveLeadsBit = true;
      }
      this.totalInProgressLeads = this.inProgressWebLiveLeads[0].totalJobs;
      this.Loader.hide();

    }, error => {
      console.log(error);
    });
  }

  public getInProgressJobsByPage(page: number) {

    this.inprogressPageNumber = page;
    this.statusId = BidStatus.Accepted;
    this.service.GetData(this.service.apiRoutes.Jobs.WebLiveLeadsPanel + `?statusId=${this.statusId}&pageNumber=${this.inprogressPageNumber}&pageSize=${this.pageSize}`, true).then(result => {

      this.inProgressWebLiveLeads = [];
      this.inProgressWebLiveLeads = result ;
      
      if (this.inProgressWebLiveLeads.length==0) {
        this.inprogressWebLiveLeadsBit = true;
      }
      this.totalInProgressLeads = this.inProgressWebLiveLeads[0].totalJobs;
      this.inprogressPageNumber = page;
      this.Loader.hide();

    }, error => {
      console.log(error);
    });
  }

  // ------------------------ To Show Complete Live Leads -------------------------------// 
  public completedLeads() {
    this.completedPageNumber = 1;
    this.statusId = BidStatus.Completed;
    this.service.GetData(this.service.apiRoutes.Jobs.WebLiveLeadsPanel + `?statusId=${this.statusId}&pageNumber=${this.completedPageNumber}&pageSize=${this.pageSize}`, true).then(result => {

      this.completedWebLiveLeads = [];
      this.completedWebLiveLeads = result ;
      if (this.completedWebLiveLeads.length ==0) {
        this.completedWebLiveLeadsBit = true;
      }
      this.totalCompletedLeads = this.completedWebLiveLeads[0].totalJobs;
      this.Loader.hide();

    }, error => {
      console.log(error);
    });
  }

  public getCompletedJobsByPage(page: number) {

    this.completedPageNumber = page;
    this.statusId = BidStatus.Completed;
    this.service.GetData(this.service.apiRoutes.Jobs.WebLiveLeadsPanel + `?statusId=${this.statusId}&pageNumber=${this.completedPageNumber}&pageSize=${this.pageSize}`, true).then(result => {

      this.completedWebLiveLeads = [];
      this.completedWebLiveLeads = result ;
      if (this.completedWebLiveLeads.length==0) {
        this.completedWebLiveLeadsBit = true;
      }
      this.totalCompletedLeads = this.completedWebLiveLeads[0].totalJobs;
      this.completedPageNumber = page;
      this.Loader.hide();;

    }, error => {
      console.log(error);
    });
  }

  public login(jqId: any, Liveleads: any) {

    var token = localStorage.getItem("auth_token");
    var decodedtoken = token != null ? this.jwtHelperService.decodeToken(token) : "";

    if (this.service.loginCheck && decodedtoken.Role == "Tradesman" || this.service.loginCheck && decodedtoken.Role == "Organization") {
      this.service.NavigateToRouteWithQueryString(this.service.apiUrls.Tradesman.LiveLeadsDeatils, { queryParams: { jobDetailId: jqId, PageName: Liveleads } });
    }
    else {
      localStorage.setItem("Role", '1');
      localStorage.setItem("Show", 'true');
      this.service.NavigateToRoute(this.service.apiUrls.Login.tradesman);
    }
  }
  public bindMetaTags() {
    this.service.get(this.service.apiRoutes.CMS.GetSeoPageById + "?pageId=15").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description,res.keywords, res.ogTitle, res.ogDescription, res.canonical);    });
  }

}
