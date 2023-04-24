import { Component, OnInit } from '@angular/core';
import { Params, Route, Router, ActivatedRoute } from '@angular/router';
import { JobDetails } from 'src/app/Shared/Models/JobModel/JobModel';
import { CommonService } from 'src/app/Shared/HttpClient/_http';
import { NgxSpinnerService } from "ngx-spinner";
import { DatePipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-active-jobs-details',
  templateUrl: './active-jobs-details.component.html',
  styleUrls: ['./active-jobs-details.component.css']
})
export class ActiveJobsDetailsComponent implements OnInit {
  public CustomerId: number;
  public comment: string;
  public overallRating: number;
  public activity: string;
  public jobDetails: JobDetails;
  public pageNumber: number = 1;
  public pageSize: number = 30;
  public pipe;
  public section = 1;
  public peopleByCountry = [];
  public peopleByCountry1 = [];
  public lastdate: Date;
  public nodata: string;

  constructor(
    public toastrService: ToastrService,
    public httpService: CommonService,
    private route: ActivatedRoute,
    public Loader: NgxSpinnerService,
  )
  {
    
  }
  public title: string;
  ngOnInit() {
    this.route.queryParams.subscribe((params: Params) => {
      
      this.CustomerId = params['id'];
      this.comment = params['comment'];
      this.overallRating = params['overallRat'];
      this.activity = params['activity'];
      this.section = 1;
      this.GetJobDetails(this.CustomerId);
    });
   
  }
  GetJobDetails(CustomerId: number) {
    this.Loader.show();
    this.httpService.get(this.httpService.apiRoutes.Jobs.GetJobDetailsList + "?pageNumber=" + this.pageNumber + "&pageSize=" + this.pageSize + "&customerId=" + this.CustomerId).subscribe(result => {
      
      this.jobDetails = result.json();
    

      if (this.jobDetails.notificationDTO == null) {
        this.nodata = "Data Not Exist";
      }
      else if (this.jobDetails.notificationDTO.length < 1) {
        this.nodata = "Data Not Exist";
      }
      else {
        this.nodata = "";
      }
      if (this.jobDetails.jobActivity!=null) {
        if (this.jobDetails.jobActivity.length > 0) {
          this.pipe = new DatePipe('en-US');
          for (let i = 0; i < this.jobDetails.jobActivity.length; i++) {
            if (this.lastdate != this.pipe.transform(this.jobDetails.jobActivity[i].createdDate, 'MM/dd/yyyy')) {
              this.lastdate = this.pipe.transform(this.jobDetails.jobActivity[i].createdDate, 'MM/dd/yyyy'),

                this.peopleByCountry.push(
                  {

                    'createdDate': this.jobDetails.jobActivity[i].createdDate,
                    'activitydetails': [
                      {
                        "text": this.jobDetails.jobActivity[i].status,

                        "value": this.jobDetails.jobActivity[i].activiyType,
                        "palceddate": this.jobDetails.jobActivity[i].createdDate
                      },
                    ]
                  },
                );
            }
            else {
              this.peopleByCountry1 = [];
              this.peopleByCountry1.push(
                {

                  'createdDate': this.jobDetails.jobActivity[i].createdDate,
                  'activitydetails': [
                    {
                      "text": this.jobDetails.jobActivity[i].status,

                      "value": this.jobDetails.jobActivity[i].activiyType,
                      "palceddate": this.jobDetails.jobActivity[i].createdDate,
                    },
                  ]
                },
              );
              this.peopleByCountry[this.peopleByCountry.length - 1].activitydetails.push(this.peopleByCountry1[0].activitydetails[0]);

            }
          }
        }
      }
      console.log(this.peopleByCountry1);
      console.log(this.peopleByCountry);

      setTimeout(() => { this.Loader.hide() }, 1000)


    },
      error => {
        this.Loader.hide();
        alert(error)
      }
    );
  }
  customerDetail(customerId: number, role: string) {
    
    this.httpService.NavigateToRouteWithQueryString(this.httpService.apiRoutes.Users.UserProfile, { queryParams: { userId: customerId, userRole: role } });
  }

  bindnofication() {
    alert('test');

  }

}
