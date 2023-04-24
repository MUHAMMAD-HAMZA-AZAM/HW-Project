import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from "ngx-spinner";
import { INotificationLogging } from '../../Shared/Enums/Interface';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.css']
})
export class NotificationsComponent implements OnInit {

  public notificationList: any =[];
  public notificationCount: number = 0;
  public userId: number = 0;
  public pageNumber = 1;
  public pageSize = 50;
  constructor(public service: CommonService, public Loader: NgxSpinnerService) { }

  ngOnInit() {
    var decodedtoken = this.service.decodedToken();
    this.userId = decodedtoken.UserId;
    this.getNotification(this.pageNumber);
  }

  getNotification(pagenumber: number) {
    this.service.GetData(this.service.apiUrls.Supplier.Notifications.GetNotificationsByUserId + "?pageSize=" + this.pageSize + "&pageNumber=" + this.pageNumber + "&userId=" + this.userId, true).then(result => {
      var res = result;
      if (res) {
        res.forEach(x => {
          if (x.body) {
            let body = JSON.parse(x.body);
            let obj = {
              createdOn: x.createdOn,
              title: body.notification.title,
              content: body.notification.body,

            }
            this.notificationList.push(obj);
          }
        })

        //this.notificationList = res;
      }
    })
  }

  onScroll() {
    debugger
    this.getNotification(++this.pageNumber);
  }
}
