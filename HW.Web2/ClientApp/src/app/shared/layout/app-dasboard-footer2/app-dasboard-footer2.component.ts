import { Component, OnInit, AfterViewInit } from '@angular/core';
import { CommonService } from '../../HttpClient/_http';
@Component({
  selector: 'appcommonfooter2',
  templateUrl: './app-dasboard-footer2.component.html',
  styleUrls: ['./app-dasboard-footer2.component.css']
})
export class AppDasboardFooter2Component implements OnInit {
  constructor(public common: CommonService) { }

  ngOnInit() {
  }
 
  public PostJob() {
    
    if (this.common.loginCheck) {
      this.common.NavigateToRoute(this.common.apiUrls.User.Quotations.getQuotes1);
    }
    else {
      localStorage.setItem("Role", '3');
      localStorage.setItem("Show", 'true');
      this.common.NavigateToRoute(this.common.apiUrls.Login.customer);
    }
  }
  public PostAd() {
    
    if (this.common.loginCheck) {
      this.common.NavigateToRoute(this.common.apiUrls.Supplier.EditAd);
    }
    else {
      localStorage.setItem("Role", '4');
      localStorage.setItem("Show", 'true');
      this.common.NavigateToRoute(this.common.apiUrls.Login.customer);
    }
  }
  

}
