import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/retry';
import { Notification } from '../../models/notification/toastr-notification.model';
@Injectable()
export class DataPassService {

  public notification: Notification = new Notification();


  constructor(
  ) {
  }

}
