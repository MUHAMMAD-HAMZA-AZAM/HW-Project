import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-UserApplayout',
  templateUrl: './userApplayout.component.html',
})
export class UserApplayoutComponent implements OnInit {
  public loginCheck: boolean = false;
  constructor(

  ) { }

  ngOnInit() {
    this.IsUserLogIn();
  }

  public IsUserLogIn() {
    var token = localStorage.getItem("auth_token");
    if (token != null && token != '') {
      this.loginCheck = true;
    }
    else {
      this.loginCheck = false;
    }
  }
}
