import {OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { MessagingService } from './Shared/HttpClient/messaging.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor() { }
  ngOnInit() {
  }
  public scrollTop(event : any) {
    window.scroll(0, 0);
  }
}
