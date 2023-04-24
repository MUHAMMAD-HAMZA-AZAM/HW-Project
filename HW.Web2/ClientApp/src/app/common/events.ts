import { Output, EventEmitter, Injectable } from "@angular/core";
@Injectable()
export class Events {
  @Output() pic_Changed = new EventEmitter();
  @Output() profile_Completed = new EventEmitter();
  @Output() account_verfication = new EventEmitter();
  @Output() update_Trademan_Credits = new EventEmitter();
  @Output() start_Job = new EventEmitter();
  @Output() skills_obj = new EventEmitter();

}
