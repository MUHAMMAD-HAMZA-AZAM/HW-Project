import { EventEmitter, Injectable, Output, Directive } from '@angular/core';

@Directive()
@Injectable()
//@Injectable({
//  providedIn: 'root'
//})
export class EventsService {
  @Output() RoleChanges = new EventEmitter();
}
