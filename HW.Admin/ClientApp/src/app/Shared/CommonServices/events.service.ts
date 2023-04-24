import { Output, EventEmitter, Injectable, Directive } from "@angular/core";

@Directive()
@Injectable({
  providedIn: 'root'
})
export class EventsService {

  @Output() pic_Changed = new EventEmitter();
  constructor() { }
}

//import { Output, EventEmitter, Injectable } from "@angular/core";
//@Injectable()
//export class Events {
  

//}
