import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-leftmenu',
  templateUrl: './app-leftmenu.component.html',
  styleUrls: ['./app-leftmenu.component.css']
})
export class AppLeftmenuComponent implements OnInit {
  public loginCheck: boolean = false;
  constructor() { }

  ngOnInit() {
    var token = localStorage.getItem("auth_token");
    if (token != null && token != '') {
      this.loginCheck = true;
    }
    else {
      this.loginCheck = false;
    }
  }
}
