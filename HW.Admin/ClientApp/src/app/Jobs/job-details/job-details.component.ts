import { Component, OnInit } from '@angular/core';
import { Params, Route, Router, ActivatedRoute } from '@angular/router';
import { JobDetails } from 'src/app/Shared/Models/JobModel/JobModel';
import { CommonService } from 'src/app/Shared/HttpClient/_http';
import { NgxSpinnerService } from "ngx-spinner";

@Component({
  selector: 'app-job-details',
  templateUrl: './job-details.component.html',
  styleUrls: ['./job-details.component.css']
})
export class JobDetailsComponent implements OnInit {
  public CustomerId: number;
  public comment: string;
  public overallRating: number;
  public activity: string;
  public jobDetails: JobDetails;
  public pageNumber: number = 1;
  public pageSize: number = 30;
  constructor(public httpService: CommonService, private route: ActivatedRoute, public Loader: NgxSpinnerService) { }
  public title: string;
  ngOnInit() {
      this.route.queryParams.subscribe((params: Params) => {
        
        this.CustomerId = params['id'];
        this.comment = params['comment'];
        this.overallRating = params['overallRat'];
        this.activity = params['activity'];
        this.GetJobDetails(this.CustomerId);
      });
  }
  GetJobDetails(CustomerId: number) {

    this.Loader.show();
    this.httpService.get(this.httpService.apiRoutes.Jobs.GetJobDetailsList + "?pageNumber=" + this.pageNumber + "&pageSize=" + this.pageSize + "&customerId=" + this.CustomerId).subscribe(result => {
      this.jobDetails = result.json();
      this.Loader.hide();

      },
        error => {
          alert(error)
        }
      );
    }
  customerDetail(customerId: number, role: string) {
    
    this.httpService.NavigateToRouteWithQueryString(this.httpService.apiRoutes.Users.UserProfile, { queryParams: { userId: customerId, userRole: role } });
  }

}
