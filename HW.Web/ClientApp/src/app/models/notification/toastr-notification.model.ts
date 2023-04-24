export class Notification {  
    type: NotificationType;  
    message: string;
  saveMsg: boolean;
}  
export enum NotificationType {  
    Success,  
    Error,  
    Info,  
    Warning  
} 
