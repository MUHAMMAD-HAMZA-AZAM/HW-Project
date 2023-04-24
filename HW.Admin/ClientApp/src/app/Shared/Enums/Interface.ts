import { DecimalPipe } from "@angular/common";
export interface IPayLoad {
  notification: INotification;
  to: string;
}
export interface INotification {
  title: string;
  body: string;
  sound: string;
  tag: string;
}
