import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { CommonService } from 'src/app/Shared/HttpClient/_http';
import { ReciveBidDetails } from 'src/app/Shared/Models/JobModel/JobModel';

@Component({
  selector: 'app-recive-bids-detail',
  templateUrl: './recive-bids-detail.component.html',
  styleUrls: ['./recive-bids-detail.component.css']
})
export class ReciveBidsDetailComponent implements OnInit {
  public jobQoutationId: number;
  public reciveBidDetails: ReciveBidDetails;
  constructor(private route: ActivatedRoute, public services: CommonService) { }

  ngOnInit() {

    this.route.queryParams.subscribe((params: Params) => {
      
      this.jobQoutationId = params['id'];
      this.GetReciveBidsDetails();
    });


  }
  GetReciveBidsDetails() {
    
    this.services.get(this.services.apiRoutes.Jobs.GetReciceBidsDetails + "?jobQoutationId=" + this.jobQoutationId).subscribe(result => {
      this.reciveBidDetails = result.json();
      console.log(this.reciveBidDetails);
    });
  }

}
