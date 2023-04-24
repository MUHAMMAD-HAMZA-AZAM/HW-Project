import { Component, OnInit } from '@angular/core';
import { Notifications } from '../../../models/userModels/userModels';
import { CommonService } from '../../../shared/HttpClient/_http';
import { httpStatus, CommonErrors } from '../../../shared/Enums/enums';

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
    
    this.common.GetData(this.common.apiRoutes.Notifications.GetNotifications +"?pageNumber="+1, true).then(result => {
      
      if (status = httpStatus.Ok) {
        this.notifications = result ;
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }


  public NotificationDetail(senderId: string, targetActivity: string) {
    
    if (targetActivity == 'MyAds') {
      //this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.GetFinishedJobDetail, { queryParams: { jobDetailId: senderEntityId } });
      this.common.NavigateToRoute(this.common.apiUrls.Supplier.ManageAd);
    }
  }
}
