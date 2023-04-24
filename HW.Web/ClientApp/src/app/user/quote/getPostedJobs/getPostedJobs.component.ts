import { Component, OnInit } from '@angular/core';
import { httpStatus } from '../../../shared/Enums/enums';
import { CommonService } from '../../../shared/HttpClient/_http';
import { UserPaymentInformation, GetPostedJobs } from '../../../models/userModels/userModels';

@Component({
  selector: 'app-get-posted-jobs',
  templateUrl: './getPostedJobs.component.html',
})
export class GetPostedJobsComponent implements OnInit {

  public postedJob: GetPostedJobs[] = [];
  constructor(
    public common: CommonService,
  ) { }

  ngOnInit() {
    this.PopulateData();
  }

  public PopulateData() {
    debugger
    this.common.GetData(this.common.apiRoutes.Jobs.GetPostedJobsByCustomerId, true).then(result => {
      debugger;
      if (status = httpStatus.Ok) {
        this.postedJob = result.json();
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    });
  }
  public JobDetail(jobQuotationId) {
    //alert("In Progress");
    //this.common.NavigateToRouteWithQueryString('User/Quote/GetPostedJobDetail', { queryParams: { jobQuotationId: jobQuotationId } });
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.GetPostedJobDetail, { queryParams: { jobQuotationId: jobQuotationId } });

  }
  public ReceivedBids(quotationId) {
    debugger
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.ReceivedBids, { queryParams: { quotationId: quotationId } });

  }
}
