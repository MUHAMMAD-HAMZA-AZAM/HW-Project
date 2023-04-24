import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from "ngx-spinner";
import { CommonService } from '../../Shared/HttpClient/_http';


@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.css']
})
export class NotificationsComponent implements OnInit {
  public notificationList = [];
  public notificationCount: number;
  public pageNumber = 1;
  public pageSize = 20;
  constructor(public service: CommonService, public Loader: NgxSpinnerService) { }

  ngOnInit() {
    this.getNotification(this.pageNumber);
  }

  getNotification(pagenumber: number) {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Notifications.GetAdminNotifications + "?pageSize=" + this.pageSize + "&pageNumber=" + this.pageNumber).subscribe(result => {
      var res = result.json();
      this.Loader.hide();
      if (res != null) {
        this.notificationList = res;
      }

    })
  }

  onScroll() {
    debugger
    this.getNotification(++this.pageNumber);
  }
}
