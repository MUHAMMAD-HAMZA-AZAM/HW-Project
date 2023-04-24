import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';
import { Notifications } from '../../../models/userModels/userModels';
import { httpStatus } from '../../../shared/Enums/enums';


@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
})
export class NotificationsComponent implements OnInit {

  public notifications: Notifications[] = [];
  constructor(
    public common: CommonService,
  ) { }

  ngOnInit() {
    this.PopulateData();
  }

  public PopulateData() {
    debugger
    this.common.GetData(this.common.apiRoutes.Notifications.GetNotifications, true).then(result => {
      debugger;
      if (status = httpStatus.Ok) {
        this.notifications = result.json();
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    });
  }


  public NotificationDetail(senderId, targetActivity) {
    var splits = senderId.split(",")
    if (splits.length) {
      var senderEntityId = parseFloat(splits[0]);
    }
    if (targetActivity == 'RateTradesman') {
      this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.GetFinishedJobDetail, { queryParams: { jobDetailId: senderEntityId } });
    }
    else if (targetActivity == 'TradesmanProfile') {
      this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.TradesmanProfile, { queryParams: { tradesmanId: senderEntityId } });
    }
    else if (targetActivity == 'PostedJobs') {
      this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.GetFinishedJobDetail, { queryParams: { jobDetailId: senderEntityId } });
    }
    else if (targetActivity == 'InProgressJobDetail') {
      this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.InProgreesJobDetails, { queryParams: { jobQuotationId: senderEntityId } });
    }
    else if (targetActivity == 'BidDetails') {
      this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.ReceivedBidDetail, { queryParams: { bidId: senderEntityId } });
    }
  }

}
