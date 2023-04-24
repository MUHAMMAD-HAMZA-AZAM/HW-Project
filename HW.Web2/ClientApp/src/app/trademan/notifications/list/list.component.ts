import { Component, OnInit } from '@angular/core';
import { Notifications } from '../../../models/userModels/userModels';
import { CommonService } from '../../../shared/HttpClient/_http';
import { httpStatus } from '../../../shared/Enums/enums';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class NotificationListComponent implements OnInit {

  public notifications: Notifications[] = [];
  public notFound: boolean = false;
  constructor(
    public common: CommonService,
  ) { }

  ngOnInit() {
    this.PopulateData();
  }

  public PopulateData() {
    
    this.common.GetData(this.common.apiRoutes.Notifications.GetNotifications, true).then(result => {
      
      if (status = httpStatus.Ok) {
        this.notifications = result;
        if (this.notifications.length > 0) {
          this.notFound = false;
          this.notFound = false;
        }
        else {
          this.notFound = true;
        }
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    },
      error => {
        console.log(error);
      });
  }


  public NotificationDetail(senderId: number, targetActivity: string) {
    //if (targetActivity == 'JobDetailActive') {
    //  this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Tradesman.ViewDetialJobs, { queryParams: { jobDetailId: senderId } });
    //}
    //else if (targetActivity == 'JobDetail_DeclinedBid') {
    //  this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Tradesman.ViewDetialJobs, { queryParams: { jobDetailId: senderId } });
    //}
    //else if (targetActivity == 'JobDetail_CompletedJob') {

    //}
    
  }

}
