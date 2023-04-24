import { Component, OnInit } from '@angular/core';

import { NotificationService } from "../toastr-notification.service";
import { DataPassService } from '../../CommonServices/app.datapass.service';
import { NotificationType, Notification } from '../../../models/notification/toastr-notification.model';

@Component({
  selector: 'toastr-notification',
  templateUrl: './toastr-notification.component.html',
  styleUrls: ['./toastr-notification.component.css']
})

export class ToastrNotificationComponent {
  notifications: Notification[] = [];
  public overlay = false;

  constructor(public _notificationService: NotificationService, public datapass: DataPassService) { }

  ngOnInit() {
    this._notificationService.getAlert().subscribe((alert: Notification) => {
      
      
      this.notifications = [];
      if (!alert) {
        if (this.datapass.notification != null) {
          alert = this.datapass.notification;
          this.datapass.notification = {} as Notification;
          this.notifications.push(alert);
        } else {
          this.notifications = [];
          return;
        }
      } else {
        this.overlay = true;
        this.notifications.push(alert);
        if (alert.saveMsg) {
          this.datapass.notification = alert;
          this.overlay = true;
        }
      }

      var delayTime = 2000;
      if (alert.type == NotificationType.Success) {
        delayTime = 2000000;
      }

      setTimeout(() => {
        if (this.notifications)
        this.notifications = this.notifications.filter(x => x !== alert);
        this.overlay = false;
      }, delayTime);
    });
  }

  removeMsg() {
    
    this.notifications = [];
    this.overlay = false;
  }

  removeNotification(notification: Notification) {
    this.notifications = this.notifications.filter(x => x !== notification);
    this.overlay = false;
  }

  /**Set css class for Alert -- Called from alert component**/
  cssClass(notification: Notification) {
    if (!notification) {
      return;
    }
    switch (notification.type) {
      case NotificationType.Success:
        return 'toast-success';
      case NotificationType.Error:
        return 'toast-error';
      case NotificationType.Info:
        return 'toast-info';
      case NotificationType.Warning:
        return 'toast-warning';
    }
  }
}  
