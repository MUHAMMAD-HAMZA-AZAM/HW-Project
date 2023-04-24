import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';
import { ActivatedRoute, Params } from '@angular/router';
import { jobDetails } from '../../../models/tradesmanModels/tradesmanModels';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { httpStatus, BidStatus } from '../../../shared/Enums/enums';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class LeadsDetailComponent implements OnInit {
  public jobQuatationid: number = 0;
  public jobdetail: jobDetails = {} as jobDetails;
  @ViewChild('ImageModel', { static: true }) ImageModel: ModalDirective;
  @ViewChild('CallBackModel', { static: true }) CallBackModel: ModalDirective;
  @ViewChild('declinieBidMessageModal', { static: true }) declinieBidMessageModal: ModalDirective;
  public jwtHelperService: JwtHelperService = new JwtHelperService()
  public imageContent: any;
  public hasNoImage: boolean = false;
  public slideIndex: number = 1;
  public PageName = '';
  public loggedTradesmanId: any;
  public isDecline: boolean = false;
  audio: any;

  constructor(public common: CommonService, private route: ActivatedRoute) {
    this.ImageModel = {} as ModalDirective;
    this.CallBackModel = {} as ModalDirective;
    this.declinieBidMessageModal = {} as ModalDirective;
  }

  ngOnInit() {
    var token = localStorage.getItem("auth_token");
    var decodedToken = token!=null ? this.jwtHelperService.decodeToken(token):"";
    this.loggedTradesmanId = decodedToken.Id;
    this.route.queryParams.subscribe((param: Params) => {
      this.jobQuatationid = param['jobDetailId'];
      this.PageName = param['PageName'];
      if (this.jobQuatationid == 0)
        this.jobQuatationid = 0;
      else {
        //this.PopulateData();
        this.getBidStatusForTradesman(this.jobQuatationid);
      }
    });
  }

  public getBidStatusForTradesman(JQID: number) {
    this.common.GetData(this.common.apiRoutes.Tradesman.GetBidStatusForTradesmanId + "?jobQuotationId=" + JQID + "&tradesmanId=" + this.loggedTradesmanId + "&statusId=" + BidStatus.Declined, true).then(result => {
      
      let declinedStatus = result;
      if (declinedStatus) {
        this.isDecline = true;
        this.declinieBidMessageModal.show();
        setTimeout(() => {
          this.isDecline = false;
          this.declinieBidMessageModal.hide();
          this.common.NavigateToRoute(this.common.apiUrls.Tradesman.LiveLeads);
        }, 5000);

      }
      else {
        this.PopulateData();
      }

    });
  }
  public hideBlockModal() {
    this.declinieBidMessageModal.hide();

  }
  public PopulateData() {
    this.common.GetData(this.common.apiRoutes.Tradesman.GetCustomerDetailsByIdWeb + "?id=" + this.jobQuatationid, true).then(data => {
      this.jobdetail = data ;
      console.log(this.jobdetail);
      this.audio = this.jobdetail.audioMessage;
      if (this.audio != null) {
        this.jobdetail.audioMessage = 'data:audio/mp3;base64,' + this.audio.audioContent;
      }

      if (this.jobdetail.jobImages == null || this.jobdetail.jobImages.length == 0) { this.hasNoImage = true; }

    },
      error => {
        console.log(error);
      });
  }
  public CallBackPopup(QuoteID: number, customerId:number) {
    this.common.GetData(this.common.apiRoutes.Tradesman.NotificationCallRequest + "?jobQuotationId=" + QuoteID + "&customerId" + customerId, true).then(data => {
      var result = data;
      if (result.status == httpStatus.Ok)
        this.CallBackModel.show();
    },
      error => {
        console.log(error);
      });
  }
  public Close() {
    this.ImageModel.hide();
  }
  public CloseCallBack() {
    this.CallBackModel.hide();
  }
  public ImagePopUp(image: any) {
    this.imageContent = image;
    this.ImageModel.show()
  }
  public PostBid(jobQuotationId: number) {
    this.jobdetail.jobQuotationId = jobQuotationId;
    this.jobdetail.jobImages = [];
    this.jobdetail.video = [];
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Tradesman.MakeBids, { queryParams: { jobQuotes: JSON.stringify(this.jobdetail) } });
  }
  public EditBid(bId: number) {
    ;
    this.jobdetail.bidId = bId;
    this.jobdetail.jobImages = [];
    this.jobdetail.video = [];
    console.log(this.jobdetail);
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Tradesman.MakeBids, { queryParams: { jobQuotes: JSON.stringify(this.jobdetail) } });
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
  public hideBidDeclineModal() {
    this.declinieBidMessageModal.hide();
  }
}
