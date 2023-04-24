
import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../shared/HttpClient/_http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { httpStatus, CommonErrors } from '../../shared/Enums/enums';
import { TrademanProfileVm } from '../../models/userModels/userModels';

@Component({
  selector: 'app-tradesman-profile',
  templateUrl: './tradesman-profile.component.html',
  styleUrls: ['./tradesman-profile.component.css']
})
export class TradesmanProfileComponent implements OnInit {

  public tradesmanId: number=0;
  public profile: TrademanProfileVm = {} as  TrademanProfileVm;
  constructor(private spinner: NgxSpinnerService, public service: CommonService, private route: Router, private router: ActivatedRoute, public Loader: NgxSpinnerService) { }

  ngOnInit() {
    this.router.queryParams.subscribe((params: Params) => {
      this.tradesmanId = params['tradesmanId'];
      if (this.tradesmanId > 0)

        this.populateTradesmanProfile();
    });
  }

  public populateTradesmanProfile() {
    
    var url = this.service.apiRoutes.Tradesman.GetTradesmanProfile + "?tradesmanId=" + this.tradesmanId + "&isActive=" + true;
    this.service.GetData(url, true).then(result => {
      if (status = httpStatus.Ok) {
        
        this.profile = result ;
        if (this.profile.tradesmanProfileImg != null) {
          this.profile.tradesmanProfileImg = 'data:image/png;base64,' + this.profile.tradesmanProfileImg;
        }
      }
    }, error => {
      console.log(error);
      this.service.Notification.error(CommonErrors.commonErrorMessage);
    });
  }

}
