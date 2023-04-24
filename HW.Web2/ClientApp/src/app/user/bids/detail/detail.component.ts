import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ReceivedBidVM } from '../../../models/userModels/userModels';
import { CommonService } from '../../../shared/HttpClient/_http';
import { ActivatedRoute, Params } from '@angular/router';
import { httpStatus, BidStatus, CommonErrors } from '../../../shared/Enums/enums';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { IReceivedBid, IResponse } from '../../../shared/Enums/Interface';

@Component({
  selector: 'app-bid-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class BidDetailComponent implements OnInit {

  public bidId: number=0;
  public confirmbId: number=0;
  public declinebId: number = 0;
  public getBId: string="";
  public bidDetail: IReceivedBid;
  @ViewChild('ConfirmQuotes', { static: true }) ConfirmQuotes: ModalDirective;

  constructor(
    public common: CommonService,
    private route: ActivatedRoute,) {
    this.bidDetail = {} as IReceivedBid;
    this.ConfirmQuotes = {} as ModalDirective;
  }

  ngOnInit() {
    let bidIds = localStorage.getItem("getBidId");
    this.getBId = bidIds != null ? JSON.parse(bidIds):"";
    localStorage.removeItem("getBidId");
    this.route.queryParams.subscribe((params: Params) => {
      this.bidId = params['bidId'];
      this.confirmbId = params['confirmbId'];
      this.declinebId = params['declinebId'];
      if ((this.getBId != null && this.confirmbId > 0)) {
        this.bidId = this.confirmbId;
        this.confirmbId = 0;
      }
      if (this.bidId > 0) {
        this.PopulateData();
      }

      if (this.confirmbId > 0 ) {
        this.bidId = this.confirmbId;
        this.QuoteConfirmation();
      }

      if (this.declinebId > 0) {
        this.bidId = this.declinebId;
        this.DeclineBid();
      }
    });

  

  }

  public PopulateData() {
    this.common.GetData(this.common.apiRoutes.Jobs.GetBidDetails + "?bidId=" + this.bidId, true).then(result => {
      
      if (status = httpStatus.Ok) {
        this.bidDetail = result ;
        console.log(this.bidDetail);
        if (this.bidDetail.tradesmanProfileImage != null) {
          this.bidDetail.tradesmanProfileImage = 'data:image/png;base64,' + this.bidDetail.tradesmanProfileImage;
        }
        if (this.bidDetail.bidAudioMessage != null) {
          this.bidDetail.bidAudioMessage = 'data:audio/mp3;base64,' + this.bidDetail.bidAudioMessage;
        }

      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }

  public AcceptBid() {

    this.common.GetData(this.common.apiRoutes.Jobs.AddJobDetails + "?bidId=" + this.bidId + "&isPaymentDirect=" + true + "&statusId=" + BidStatus.Accepted, true).then(result => {
      if (status = httpStatus.Ok) {
        this.CloseModel();
        var data: IResponse = result ;
        this.common.Notification.success(data.message);
        this.common.NavigateToRoute(this.common.apiUrls.User.InProgressJobList)
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }

  public DeclineBid() {
    ;
    this.common.GetData(this.common.apiRoutes.Jobs.UpdateBidStatus + "?bidId=" + this.bidId + "&statusId=" + BidStatus.Declined, true).then(result => {
      if (status = httpStatus.Ok) {
        var data: boolean = result ;
        this.common.NavigateToRoute(this.common.apiUrls.User.GetPostedJobs)
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }

  public ViewProfile(tradesmanId: number) {
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.TradesmanProfile, { queryParams: { tradesmanId: tradesmanId } });

  }

  public UserAgrement() {
    this.common.NavigateToRoute(this.common.apiUrls.User.Home.UserAgreement)
  }

  public QuoteConfirmation() {
    
    // this.ConfirmQuotes.show();
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.Payments, { queryParams: { bidId: this.bidId } });
  }

  public CloseModel() {
    this.ConfirmQuotes.hide();
  }

}
