import { Injectable } from '@angular/core';
import { AngularFireMessaging } from '@angular/fire/messaging';
import { BehaviorSubject } from 'rxjs'
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonService } from './_http';
import { IPayLoad } from '../Enums/Interface';
@Injectable()
export class MessagingService {
  currentMessage = new BehaviorSubject<any>(null);
  orderActivity = new BehaviorSubject(false);
  TOKEN = 'AAAARb-zILs:APA91bH6ssc7RlCIwVjFf3S-iXw_XPDpemDaXdLFx60Gb6IJGKTibS7yZWyh3fo91crq73ef5FGlhubLoYcKcdEzWs9_HkYFPZdgOfnvZWxWTkvciF4WxTRuYUNXE1E71pXOs6dl_8qz'
  //TOKEN = 'AAAAxvnf-5I:APA91bHWkkAa_ppj5D28EHmTF-f6J1MKuMOvaRWT5UH9T5uhd6tbDeS7gn1wi0QXaotlIh9VI4DFsGyP0K2BvCNqkbxMZbDs1_RbM0RRetzNZtG_AgQwKxRQ7kxfkMcvZuz6Cd2JSWzd'
  constructor(private angularFireMessaging: AngularFireMessaging, private _http: HttpClient, private common: CommonService) {
    this.angularFireMessaging.messaging.subscribe(
      (_messaging) => {
        _messaging.onMessage = _messaging.onMessage.bind(_messaging);
        _messaging.onTokenRefresh = _messaging.onTokenRefresh.bind(_messaging);
      }
    )
  }
  requestPermission(firebaseClientId: string) {
    this.angularFireMessaging.requestToken.subscribe(token => { 
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
  updateUserFirebaseToken(token:string) {
    this.common.get(this.common.apiUrls.Supplier.IdentityServer.UpdateUserFirebaseToken + `?firebaseToken=${token}`).subscribe(x => { })
  }
  receiveMessage() {
    this.angularFireMessaging.messages.subscribe(
      (payload: any) => {
        if (payload) {
          console.log("new message received. ", payload);
          this.currentMessage.next(payload);
          this.showCustomPopup(payload);
        }
      })
  }
  showCustomPopup(payload: IPayLoad) {
    //let notify_data = payload['data'];
    //let title = notify_data['title'];
    let notify_data = payload['notification'];
    let title = notify_data['title'];
    let options = {
      body: notify_data['body'],
      icon: '../../assets/images/logo.png',
      badge: '../../assets/images/logo.png',
      image: '../../assets/images/logo.png'
    }
    let notify: Notification = new Notification(title, options);
    notify.onclick = event => {
      event.preventDefault();
      window.location.href = window.location.origin + this.common.apiRoutes.Order.OrderList

    }
  }
  sendMessage(title: string, body: string, firebaseId: string,) {
    const fcm = {
      "data": {
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
