import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  public count = 1;
  public liveLeads: object[] = [];
  public liveLeadsConcate: object[] = [];

  constructor(private common: CommonService) {

  }

  ngOnInit() {
    this.populateData(this.count);
  }

  public populateData(count: number) {

    this.common.GetData(this.common.apiRoutes.Tradesman.GetJobLeadsWebByTradesmanId + "?pageNumber=" + count + "&pageSize=" + 10, true).then(data => {
      
      this.liveLeads = data ;
      if (this.liveLeads != null)
      this.liveLeadsConcate = [...this.liveLeadsConcate, ...this.liveLeads];
    });
  }

  onScroll() {
    
    this.populateData(++this.count);
  }
  public JobDetail(adId: number) {
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Tradesman.LiveLeadsDeatils, { queryParams: { jobDetailId: adId } });
  }
}
