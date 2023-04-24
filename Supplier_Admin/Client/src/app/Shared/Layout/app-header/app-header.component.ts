import { HttpStatusCode } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AngularFireMessaging } from '@angular/fire/messaging';
import { StatusCode } from '../../Enums/common';
import { IDisplayNotification, INotificationLogging, IPostNotificationVM } from '../../Enums/Interface';
import { MessagingService } from '../../HttpClient/messaging.service';
import { CommonService } from '../../HttpClient/_http';

@Component({
  selector: 'app-app-header',
  templateUrl: './app-header.component.html',
  styleUrls: ['./app-header.component.css']
})
export class AppHeaderComponent implements OnInit {
  public response: any="";
  public suppId: string="";
  public logoImage: string="";
  public fullName: string="";
  public userId: any;
  public notificationList: IDisplayNotification[] = [];
  public notificaioncount: number = 0;
  public pageSize: number = 10;
  public pageNumber: number = 1;
  elementIndex : number = 0;
  message: any;
  dataNotFound: boolean = false;
  unreadNotificationCount: number = 0;
  public walletAmount: number=0;
  constructor(private angularFireMessaging: AngularFireMessaging, public common: CommonService, private _messagingService: MessagingService) {
  }

  ngOnInit(): void {
    //this._messagingService.receiveMessage();
    this.angularFireMessaging.messages.subscribe((payload: any) => {
      if (payload) {
        console.log("new message received. ", payload);
        this._messagingService.currentMessage.next(payload);
        this._messagingService.showCustomPopup(payload);
        this.getNotificationList();
      }
    })
    //this._messagingService.currentMessage.subscribe(x => {
    //})
    var decodedtoken = this.common.decodedToken();
    this.suppId = decodedtoken.Id;
    this.userId = decodedtoken.UserId;
    this.GetSupplierWallet();
    this.PopulateProfile();
    this.getNotificationList();
    this.common.subject$.subscribe((isLogoChanged: any) => {
      if (isLogoChanged) {
        this.PopulateProfile();
      }
    }, error => {
      console.log(error);
    });
  }
  GetSupplierWallet() {
    this.common.get(this.common.apiUrls.Supplier.Profile.GetSupplierWallet + "?refSupplierId=" + this.suppId).subscribe(res => {
        this.response = res;
        if (this.response.status == StatusCode.OK) {
          if (this.response.resultData) {
            this.walletAmount = this.response.resultData;
          }
        }
      }, error => {
        console.log(error);
      });
    }
  logOut() {
    let username = localStorage.getItem('supplierUserName');
    let password = localStorage.getItem('supplierPassword');
    let role = localStorage.getItem('supplierPassword');
    let rememberMe = localStorage.getItem('rememberMe');
    localStorage.clear();
    if(rememberMe) {
      localStorage.setItem('supplierUserName', username);
      localStorage.setItem('supplierPassword', password);
      localStorage.setItem('supplierRole', role);
    }
    window.location.href = '';
  }
  markNotificationAsRead(notificationId: number, targetActivity: string, index: number) {
    this.common.GetData(this.common.apiUrls.Supplier.Notifications.MarkNotificationAsRead + `?notificationId=${notificationId}`, false).then(x => {
      if (x.status == HttpStatusCode.Ok) {
        this.elementIndex = index;
        this.getNotificationList();
        this.navigateToRoute(targetActivity);
      }
    })
  }
  identify(index: number, item: IPostNotificationVM) {
    return item.notificationId
  }
  public PopulateProfile() {
    this.common.GetData(this.common.apiUrls.Supplier.Profile.GetProfile + "?supplierId=" + this.suppId, true).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        if (this.response.resultData[0].profileImage) {
          this.logoImage = this.response.resultData[0].profileImage;
        }
        this.fullName = this.response.resultData[0].fullName
        this._messagingService.requestPermission(this.response.resultData[0].firebaseClientId)
      }

    }, error => {
      console.log(error);
    });
  }
  ShowCard(tabName:string) {
    this.common.NavigateToRouteWithQueryString(this.common.apiRoutes.ProfileManagement.ProfileManagement, {
      queryParams: {
        "tabId": tabName
      }
    });
  }
  getNotificationList() {
    this.common.get(this.common.apiUrls.Supplier.Notifications.GetNotificationsByUserId + "?pageSize=" + this.pageSize + "&pageNumber=" + this.pageNumber + "&userId=" + this.userId).subscribe(result => {
      let res: IPostNotificationVM[] = result;
      if (res) {
        this.notificationList = [];
        this.unreadNotificationCount = res.filter(x => !x.isRead).length;
        res.forEach(x => {
          if (x.body) {
            let body = JSON.parse(x.body);
            let obj = {
              createdOn: x.createdOn,
              title: body.notification.title,
              content: body.notification.body,
              isRead: x.isRead,
              notificationId: x.notificationId,
              targetActivity: body.data.targetActivity
            }
            this.notificationList.push(obj);
          }
        })
      }
      else
        this.dataNotFound = true;
    })
  }
  navigateToRoute(targetActivity: string) {
    if (targetActivity == "NewOrderPlace" || targetActivity == "NewOrderPlaced") {
      this.common.NavigateToRoute(this.common.apiRoutes.Order.OrderList);
    }
  }
}
