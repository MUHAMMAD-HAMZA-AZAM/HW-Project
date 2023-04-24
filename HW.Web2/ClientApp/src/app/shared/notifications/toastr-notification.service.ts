import { Injectable } from '@angular/core';  
import { Router, NavigationStart } from '@angular/router';  
import { Observable, Subject } from 'rxjs';  
import { NotificationType, Notification } from '../../models/notification/toastr-notification.model';
@Injectable()  
export class NotificationService {  
    public subject = new Subject<Notification>();  
    public keepAfterRouteChange = true;  
  
  constructor(public router: Router) {  
        // clear alert messages on route change unless 'keepAfterRouteChange' flag is true  
        router.events.subscribe(event => {  
            if (event instanceof NavigationStart) {  
                if (this.keepAfterRouteChange) {  
                    // only keep for a single route change  
                    this.keepAfterRouteChange = false;  
                } else {  
                    // clear alert messages  
                    this.clear();  
                }  
            }  
        });  
    }  
  
    getAlert(): Observable<any> {  
        return this.subject.asObservable();  
  }
  success(message: string, keepAfterRouteChange = false, saveMsg = false) {
      this.show(message, NotificationType.Success, keepAfterRouteChange, saveMsg);  
    }  
  
  error(message: string, keepAfterRouteChange = false, saveMsg = false) {  
    this.show(message, NotificationType.Error, keepAfterRouteChange, saveMsg);  
    }  
  
  info(message: string, keepAfterRouteChange = false, saveMsg = false) {  
    this.show(message, NotificationType.Info, keepAfterRouteChange, saveMsg);  
    }  
  
  warning(message: string, keepAfterRouteChange = false, saveMsg = false) {  
    this.show(message, NotificationType.Warning, keepAfterRouteChange, saveMsg);  
    }  

  show(message: string, type: NotificationType = NotificationType.Success, keepAfterRouteChange = false, saveMsg = false) {  
    this.keepAfterRouteChange = keepAfterRouteChange;
    this.subject.next(<Notification>{ type: type, message: message, saveMsg: saveMsg  });  
    }  
  
    clear() {  
        this.subject.next();  
    }  
}  
