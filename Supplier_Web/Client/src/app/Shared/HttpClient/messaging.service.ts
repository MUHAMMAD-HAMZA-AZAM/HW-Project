import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { AngularFireMessaging } from '@angular/fire/messaging';
import { BehaviorSubject } from 'rxjs'
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonService } from './HttpClient';
import { isPlatformBrowser } from '@angular/common';
@Injectable()
export class MessagingService {
  currentMessage = new BehaviorSubject<any>(null);
  TOKEN = 'AAAARb-zILs:APA91bH6ssc7RlCIwVjFf3S-iXw_XPDpemDaXdLFx60Gb6IJGKTibS7yZWyh3fo91crq73ef5FGlhubLoYcKcdEzWs9_HkYFPZdgOfnvZWxWTkvciF4WxTRuYUNXE1E71pXOs6dl_8qz'
  constructor(@Inject(PLATFORM_ID) private platformId: Object, private angularFireMessaging: AngularFireMessaging, private _http: HttpClient, private common: CommonService) {
    if (isPlatformBrowser(this.platformId)) {
      this.angularFireMessaging.messaging.subscribe(
        (_messaging) => {
          _messaging.onMessage = _messaging.onMessage.bind(_messaging);
          _messaging.onTokenRefresh = _messaging.onTokenRefresh.bind(_messaging);
        }
      )
    }
  }
  requestPermission(firebaseClientId: string) {
    if (isPlatformBrowser(this.platformId)) {
      this.angularFireMessaging.requestToken.subscribe(
        (token) => {
          console.log(token);
          if (token && token != firebaseClientId) {
            this.updateUserFirebaseToken(token);
          }
        },
        (err) => {
          console.error('Unable to get permission to notify.', err);
        }
      );
    }
  }
  updateUserFirebaseToken(token: string) {
    this.common.get(this.common.apiUrls.IdentityServer.UpdateUserFirebaseToken + `?firebaseToken=${token}`).subscribe(x => { })
  }

  receiveMessage() {
    if (isPlatformBrowser(this.platformId)) {
      this.angularFireMessaging.messages.subscribe((payload: any) => {
        console.log("web new message received. ", payload);
        this.currentMessage.next(payload);
        this.showCustomPopup(payload);
      })
    }
  }
  showCustomPopup(payload: any) {
    let notify_data = payload['data'];
    let title = notify_data['title'];
    let data = payload['data'];
    let options = {
      body: notify_data['body'],
      icon: '../../assets/images/logo.png',
      badge: '../../assets/images/logo.png',
      image: '../../assets/images/logo.png'
    }
    console.log(window.location.origin + this.common.apiRoutes.User.Orders);
    let notify: Notification = new Notification(title, options);
    notify.onclick = event => {
      event.preventDefault();
      window.location.href = window.location.origin + this.common.apiRoutes.User.Orders;
    }
  }
  sendMessage(title: string, body: string, firebaseId: string,) {
    const fcm = {
      "data": {
        "title": title,
        "body": body,
        "tag": "notification-1",
        "sound": "default",
        //  "targetActivity": target
      },
      "to": firebaseId
    };
    this.createNotification(fcm)
  }
  createNotification(data: any) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'key=' + this.TOKEN
    });
      debugger;
    return this._http.post('https://fcm.googleapis.com/fcm/send', data, { headers: headers }).subscribe(res => {
      console.log('Res from APi', res)
    })

  }
}
