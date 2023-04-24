import { Component, OnInit } from '@angular/core';
import { httpStatus, BidStatus } from '../../../shared/Enums/enums';
import { CommonService } from '../../../shared/HttpClient/_http';
import { InProgressJobList } from '../../../models/userModels/userModels';

@Component({
  selector: 'app-inprogress',
  templateUrl: './inProgressJobList.component.html',
})
export class InprogressJobListComponent implements OnInit {
  public inProgressJobList: InProgressJobList[] = [];

  constructor(
    public common: CommonService,
  ) { }
  ngOnInit() {
    debugger
    this.PopulateData();
  }
  public PopulateData() {
    debugger
    var urls = this.common.apiRoutes.Jobs.GetAlljobDetails + "?jobQuotationId=" + BidStatus.Active;
    this.common.GetData(urls, true).then(result => {
      debugger;
      if (status = httpStatus.Ok) {
        this.inProgressJobList = result.json();
      }
      else {
        alert("Some Thing Went Wrong");
      }
    });
  }

  public JobDetail(jobQuotationId) {
    debugger
    this.common.NavigateToRouteWithQueryString('User/InProgressJob/InProgreesJobDetails', { queryParams: { jobQuotationId: jobQuotationId } });

  }

}
