import { Component, OnInit } from '@angular/core';
import { httpStatus, CommonErrors } from '../../../shared/Enums/enums';
import { CommonService } from '../../../shared/HttpClient/_http';
import { Notifications } from '../../../models/userModels/userModels';

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
    this.common.GetData(this.common.apiRoutes.Notifications.GetNotifications, true).then(result => {
      if (status = httpStatus.Ok) {
        var data = result ;
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }

}
