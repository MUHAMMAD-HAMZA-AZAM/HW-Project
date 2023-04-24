import { Component, OnInit } from '@angular/core';
import { TrademanProfileVm, ReceivedBids } from '../../../models/userModels/userModels';
import { CommonService } from '../../../shared/HttpClient/_http';
import { ActivatedRoute, Params } from '@angular/router';
import { httpStatus } from '../../../shared/Enums/enums';

@Component({
  selector: 'app-received-bids',
  templateUrl: './receivedBids.component.html',
  styleUrls: ['./receivedBids.component.css']
})
export class ReceivedBidsComponent implements OnInit {

  public quotationId: number;
  public jobTitle: string;
  public jobImages: string;
  public Selected = '';
  public receivedBids: ReceivedBids[] = [];
  constructor(
    public common: CommonService,
    private route: ActivatedRoute,
  ) { }

  ngOnInit() {
    this.route.queryParams.subscribe((params: Params) => {
      this.quotationId = params['quotationId'];
      if (this.quotationId > 0)
        this.PopulateData();
    });
  }

  public PopulateData() {
    debugger;
    var url = this.common.apiRoutes.Jobs.GetQuotationBids + "?quotationId=" + this.quotationId;
    this.common.GetData(url, true).then(result => {
      debugger;
      if (status = httpStatus.Ok) {
        this.receivedBids = result.json();
        this.jobTitle = this.receivedBids[0].jobQuotationTitle;
        if (this.receivedBids[0].bidImage != null) {
          this.jobImages = 'data:image/png;base64,' + this.receivedBids[0].bidImage;
        }
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    });
  }
  public BidSorted($event) {
    debugger
    var sort = $event.target.value
    var url = this.common.apiRoutes.Jobs.GetQuotationBids + "?quotationId=" + this.quotationId + "&sortId=" + sort;
    this.common.GetData(url, true).then(result => {
      debugger;
      if (status = httpStatus.Ok) {
        this.receivedBids = result.json();
        this.jobTitle = this.receivedBids[0].jobQuotationTitle;
        if (this.receivedBids[0].bidImage != null) {
          this.jobImages = 'data:image/png;base64,' + this.receivedBids[0].bidImage;
        }
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    });
  }

  public BidDetail(bidId) {
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.ReceivedBidDetail, { queryParams: { bidId: bidId } });
  }
  public IsSelected(isSelected, BidId, index) {
    if (isSelected == true) {
      this.receivedBids[index].isSelected = false;
      isSelected = false
    }
    else {
      this.receivedBids[index].isSelected = true;
      isSelected = true
    }
    var url = this.common.apiRoutes.Jobs.UpdateSelectedBid + "?BidId=" + BidId + "&IsSelected=" + isSelected;
    this.common.GetData(url, false).then(result => {
      debugger;
      if (status = httpStatus.Ok) {
        var data = result.json();
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    });
  }


}

