import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { CommonService } from 'src/app/Shared/HttpClient/_http';
import { GetReciveBids } from 'src/app/Shared/Models/JobModel/JobModel';

@Component({
  selector: 'app-recive-bids',
  templateUrl: './recive-bids.component.html',
  styleUrls: ['./recive-bids.component.css']
})
export class ReciveBidsComponent implements OnInit {
  public jobQoutationId: number;
  public reciveBids: GetReciveBids[] = [];


  constructor(private route: ActivatedRoute, public services: CommonService) { }

  ngOnInit() {
    this.route.queryParams.subscribe((params: Params) => {
      
      this.jobQoutationId = params['id'];
      this.GetReciveBids();
    });
  }
  GetReciveBids() {
    
    this.services.get(this.services.apiRoutes.Jobs.GetReciceBids + "?jobQoutationId=" + this.jobQoutationId).subscribe(result => {
      this.reciveBids = result.json();
    });
  }
  GetJobQoutationId(jobQoutation) {
    
    this.services.NavigateToRouteWithQueryString(this.services.apiRoutes.Jobs.getJobDetails, { queryParams: { id: jobQoutation } });
  }
}
