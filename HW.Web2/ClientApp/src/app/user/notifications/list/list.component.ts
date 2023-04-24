import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';
import { Notifications } from '../../../models/userModels/userModels';
import { httpStatus, CommonErrors } from '../../../shared/Enums/enums';
import { INotifications } from '../../../shared/Enums/Interface';


@Component({
  selector: 'app-notifications',
  templateUrl: './list.component.html',
})
export class NotificationsListComponent implements OnInit {

  public notifications: INotifications[] = [];
  public notFound: boolean = false;

  constructor(
    public common: CommonService,
  ) {
}

  ngOnInit() {
    this.PopulateData();
  }

  public PopulateData() {
    this.common.GetData(this.common.apiRoutes.Notifications.GetNotifications, true).then(result => {
      if (status = httpStatus.Ok) {
        this.notifications = result;
        if (this.notifications.length > 0) {
          this.notFound = false;
        }
        else {
          this.notFound = true;
        }
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }


  public NotificationDetail(senderId: string, targetActivity: string) {
    var splits = senderId.split(",")
    var senderEntityId;
    if (splits.length) {
      senderEntityId= parseFloat(splits[0]);
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
