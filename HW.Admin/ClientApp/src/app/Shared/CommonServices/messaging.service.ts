import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';
import { AngularFireMessaging } from '@angular/fire/messaging';
import { BehaviorSubject } from 'rxjs';
import { IPayLoad } from '../Enums/Interface';
import { CommonService } from '../HttpClient/_http';

@Injectable(
  { providedIn: 'root' }
)
export class MessagingService {

  currentMessage = new BehaviorSubject<any>(null);
  orderActivity = new BehaviorSubject(false);
  TOKEN = 'AAAARb-zILs:APA91bH6ssc7RlCIwVjFf3S-iXw_XPDpemDaXdLFx60Gb6IJGKTibS7yZWyh3fo91crq73ef5FGlhubLoYcKcdEzWs9_HkYFPZdgOfnvZWxWTkvciF4WxTRuYUNXE1E71pXOs6dl_8qz'
  constructor(private injector: Injector, private angularFireMessaging: AngularFireMessaging, private _http: HttpClient, private common: CommonService) {
    this.angularFireMessaging.messaging.subscribe(
      (_messaging) => {
        _messaging.onMessage = _messaging.onMessage.bind(_messaging);
        _messaging.onTokenRefresh = _messaging.onTokenRefresh.bind(_messaging);
      }
    )
  }
  requestPermission(firebaseClientId: string) {
    this.angularFireMessaging.requestToken.subscribe(token => {
      debugger;
      console.log(token);
      if (firebaseClientId != token && token) {
        this.updateUserFirebaseToken(token);
      }
    },
      (err) => {
        console.error('Unable to get permission to notify.', err);
      }
    );
  }
  updateUserFirebaseToken(token: string) {
    //const common = this.injector.get(CommonService);
    //this.common.get(this.common.apiRoutes.Login.UpdateUserFirebaseToken + `?firebaseToken=${token}`).subscribe(x => { })
  }
  receiveMessage() {
    this.angularFireMessaging.messages.subscribe(
      (payload: any) => {
        debugger;
        if (payload) {
          console.log("new message received. ", payload);
          this.currentMessage.next(payload);
          this.showCustomPopup(payload);
        }
      })
  }
  showCustomPopup(payload: IPayLoad) {
    console.log(payload);
    debugger;
    //let notify_data = payload['data'];
    let notify_data = payload['notification'];
    let title = notify_data['title'];
    let options = {
      body: notify_data['body'],
      icon: '../../../assets/img/login-logo.png',
      badge: '../../../assets/img/login-logo.png',
      image: '../../../assets/img/login-logo.png'
    }
    let notify: Notification = new Notification(title, options);
    //notify.onclick = event => {
    //  event.preventDefault();
    //  window.location.href = window.location.origin + this.common.apiRoutes.Order.OrderList

    //}
  }
  sendMessage(title: string, body: string, firebaseId: string,) {
    const fcm = {
      "notification": {
        "title": title,
        "body": body,
        "sound": "default",
        "tag": "notification-1",
      },
      "to": firebaseId
    };
    this.createNotification(fcm)
  }
  createNotification(data: IPayLoad) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'key=' + this.TOKEN
    });
    return this._http.post('https://fcm.googleapis.com/fcm/send', data, { headers: headers }).subscribe(res => {
      console.log('Res from APi', res)
    })

  }
}
