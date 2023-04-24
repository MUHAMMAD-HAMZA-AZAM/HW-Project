import { Component, OnInit } from '@angular/core';
import { Notifications } from '../../../models/userModels/userModels';
import { CommonService } from '../../../shared/HttpClient/_http';
import { httpStatus } from '../../../shared/Enums/enums';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  public notifications: Notifications[] = [];
  constructor(
    public common: CommonService,
  ) { }

  ngOnInit() {
    this.PopulateData();
  }

  public PopulateData() {
    debugger
    this.common.GetData(this.common.apiRoutes.Notifications.GetNotifications +"?pageNumber="+1, true).then(result => {
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
    debugger
    if (targetActivity == 'MyAds') {
      //this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.GetFinishedJobDetail, { queryParams: { jobDetailId: senderEntityId } });
      this.common.NavigateToRoute(this.common.apiUrls.Supplier.ManageAd);
    }
  }
}
