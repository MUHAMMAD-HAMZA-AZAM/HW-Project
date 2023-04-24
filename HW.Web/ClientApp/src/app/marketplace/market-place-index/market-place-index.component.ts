import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../shared/HttpClient/_http';

@Component({
  selector: 'app-market-place-index',
  templateUrl: './market-place-index.component.html',
  styleUrls: ['./market-place-index.component.css']
})
export class MarketPlaceIndexComponent implements OnInit {
  public pageWrapper: string;
  constructor(public common: CommonService) { }

  ngOnInit() {
    this.common.IsUserLogIn();
    this.IsUserLogIn();

  }

  public IsUserLogIn() {
    debugger
    if (this.common.loginCheck) {
      this.pageWrapper = 'page-wrapper';
    }
    else {
      this.pageWrapper = '';
    }
  }

}
