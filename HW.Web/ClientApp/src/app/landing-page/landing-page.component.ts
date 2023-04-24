import { Component, OnInit } from '@angular/core';
import { CommonService } from '../shared/HttpClient/_http';
import { isNull } from 'util';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent implements OnInit {
  public loginCheck: boolean = false;
  public subCategory: any;
  public Hidden = false;
  public selectedItems = [];
  constructor(private common: CommonService, private router: Router) { }


  keyPress(event: any) {
    debugger;
    if (event.length > 0) {
      this.common.get(this.common.apiRoutes.Home.GetAllSubcategory + "?search=" + event).subscribe(data => {
        var result = data.json();
        if (!isNull(result)) {
          this.subCategory = data.json();
          this.Hidden = true;
        } else this.Hidden = false;

      });
    }
  }
  ngOnInit() {
    this.common.IsUserLogIn();
  }


  public PostJob() {
    debugger
    if (this.common.loginCheck) {
      this.common.NavigateToRoute(this.common.apiUrls.User.Quotations.getQuotes1);
    }
    else {
      this.common.NavigateToRoute("/login");
    }
  }
}
