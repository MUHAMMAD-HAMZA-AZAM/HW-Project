import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';
import { httpStatus, BidStatus } from '../../../shared/Enums/enums';
import { FinishedJobList } from '../../../models/userModels/userModels';

@Component({
  selector: 'app-finished-job',
  templateUrl: './GetFinishedJobs.component.html',
})
export class GetFinishedJobsComponent implements OnInit {
  public BidStatus = BidStatus;
  public jobList: FinishedJobList[] = [];

  constructor(
    public common: CommonService,
  ) { }
  ngOnInit() {
    debugger
    this.PopulateData();
  }
  public PopulateData() {
    debugger
    var urls = this.common.apiRoutes.Jobs.GetFinishedJobs + "?statusId=" + BidStatus.Completed;
    this.common.GetData(urls, true).then(result => {
      debugger;
      if (status = httpStatus.Ok) {
        this.jobList = result.json();
        for (var i = 0; i < this.jobList.length; i++) {
          if (this.jobList[i].jobImage != null) {
            var imgURl = 'data:image/png;base64,' + this.jobList[i].jobImage;
            this.jobList[i].jobImage = imgURl.toString();
          }
        }
      }
      else {
        this.common.Notification.error("Some Thing Went Wrong");
      }
    });
  }

  public JobDetail(jobDetailId) {
    debugger
    this.common.NavigateToRouteWithQueryString('User/Job/GetFinishedJobDetail', { queryParams: { jobDetailId: jobDetailId } });
    //this.common.NavigateToRouteWithQueryString(this.common.apiRoutes.Jobs.GetFinishedJobDetail);

  }


}
