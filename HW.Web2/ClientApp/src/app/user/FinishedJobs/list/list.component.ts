import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';
import { httpStatus, BidStatus, CommonErrors } from '../../../shared/Enums/enums';
import { FinishedJobList } from '../../../models/userModels/userModels';
import { IFinishedJob } from '../../../shared/Enums/Interface';

@Component({
  selector: 'app-finished-job',
  templateUrl: './list.component.html',
})
export class FinishedJobListComponent implements OnInit {
  public BidStatus = BidStatus;
  public jobList: IFinishedJob[] = [];
  public notFound: boolean = false;

  constructor(
    public common: CommonService,
  ) { }

  ngOnInit() {
    this.PopulateData();
  }

  public PopulateData() {
    
    this.common.GetData(this.common.apiRoutes.Jobs.GetFinishedJobs + "?statusId=" + BidStatus.Completed, true).then(result => {
      
      if (status = httpStatus.Ok) {
        this.jobList = result;
        if (this.jobList.length > 0) {
          for (var i = 0; i < this.jobList.length; i++) {
            if (this.jobList[i].jobImage != null) {
              var imgURl = 'data:image/png;base64,' + this.jobList[i].jobImage;
              this.jobList[i].jobImage = imgURl.toString();

            }
          }
          this.notFound = false;
        }
        else {
          this.notFound = true;
        }
      }
     
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }

  public JobDetail(jobDetailId: number, e: Event) {
    e.preventDefault();
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.GetFinishedJobDetail, { queryParams: { jobDetailId: jobDetailId } });
  }

}
