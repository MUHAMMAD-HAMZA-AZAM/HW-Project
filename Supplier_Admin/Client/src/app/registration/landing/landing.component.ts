import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent implements OnInit {

  constructor(public _httpService: CommonService) { }

  ngOnInit(): void {
  }

  public sellerType(sTypeId: number) {

    this._httpService.NavigateToRouteWithQueryString(this._httpService.apiRoutes.Registration.Signup, {
      queryParams: {
        "sellerType": sTypeId
      }
    });
  }

}
