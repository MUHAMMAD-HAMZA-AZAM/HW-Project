import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { TrademanProfileVm, ReceivedBidVM } from '../../../models/userModels/userModels';
import { CommonService } from '../../../shared/HttpClient/_http';
import { ActivatedRoute, Params } from '@angular/router';
import { httpStatus, BidStatus } from '../../../shared/Enums/enums';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-received-bid-detail',
  templateUrl: './receivedBidDetail.component.html',
  styleUrls: ['./receivedBidDetail.component.css']
})
export class ReceivedBidDetailComponent implements OnInit {
  public bidId: number;
  public bidDetail: ReceivedBidVM = new ReceivedBidVM();
  @ViewChild('ConfirmQuotes', { static: true }) ConfirmQuotes: ModalDirective;

  constructor(
    public common: CommonService,
    private route: ActivatedRoute,
  ) { }

  ngOnInit() {
    this.route.queryParams.subscribe((params: Params) => {
      this.bidId = params['bidId'];
      if (this.bidId > 0)

        this.PopulateData();
    });
  }

  public PopulateData() {
    debugger;
    this.common.GetData(this.common.apiRoutes.Jobs.GetBidDetails + "?bidId=" + this.bidId, true).then(result => {
      debugger;
      if (status = httpStatus.Ok) {
        this.bidDetail = result.json();
        if (this.bidDetail.tradesmanProfileImage != null) {
          this.bidDetail.tradesmanProfileImage = 'data:image/png;base64,' + this.bidDetail.tradesmanProfileImage;
        }
        if (this.bidDetail.bidAudioMessage != null) {
          this.bidDetail.bidAudioMessage = 'data:audio/mp3;base64,' + this.bidDetail.bidAudioMessage;
        }

      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    });
  }

  public AcceptBid() {
    debugger;
    this.common.GetData(this.common.apiRoutes.Jobs.AddJobDetails + "?bidId=" + this.bidId + "&isPaymentDirect=" + true + "&statusId=" + BidStatus.Accepted, true).then(result => {
      debugger;
      if (status = httpStatus.Ok) {
        this.CloseModel();
        var data = result.json();
        this.common.Notification.success(data.message);
        this.common.NavigateToRoute(this.common.apiUrls.User.InProgressJobList)
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    });
  }

  public DeclineBid() {
    debugger;
    this.common.GetData(this.common.apiRoutes.Jobs.UpdateBidStatus + "?bidId=" + this.bidId + "&statusId=" + BidStatus.Accepted, true).then(result => {
      debugger;
      if (status = httpStatus.Ok) {
        var data = result.json();
        this.common.NavigateToRoute(this.common.apiUrls.User.GetPostedJobs)
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    });
  }

  public ViewProfile(tradesmanId) {
    debugger
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.TradesmanProfile, { queryParams: { tradesmanId: tradesmanId } });

  }

  public UserAgrement() {
    this.common.NavigateToRoute(this.common.apiUrls.User.Home.UserAgreement)
  }



  public QuoteConfirmation() {
    this.ConfirmQuotes.show();
  }
  public CloseModel() {
    this.ConfirmQuotes.hide();
  }
}
