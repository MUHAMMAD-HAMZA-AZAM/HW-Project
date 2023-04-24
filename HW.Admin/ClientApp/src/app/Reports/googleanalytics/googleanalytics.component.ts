import { Component, OnInit } from '@angular/core';
declare var gapi;

@Component({
  selector: 'app-googleanalytics',
  templateUrl: './googleanalytics.component.html',
  styleUrls: ['./googleanalytics.component.css']
})
export class GoogleanalyticsComponent implements OnInit {
  public VIEW_ID: string = '190027671';
  constructor() { }

  ngOnInit() {
    this.VIEW_ID = '190027671';
    this.queryReports();
  }

  // Replace with your view ID.
  

  // Query the API and print the results to the page.
  public queryReports(){
    
  gapi.client.request({
    path: '/v4/reports:batchGet',
    root: 'https://analyticsreporting.googleapis.com/',
    method: 'POST',
    body: {
      reportRequests: [
        {
          viewId: this.VIEW_ID,
          dateRanges: [
            {
              startDate: '7daysAgo',
              endDate: 'today'
            }
          ],
          metrics: [
            {
              expression: 'ga:sessions'
            }
          ]
        }
      ]
    }
  }).then(this.displayResults, console.error.bind(console));
}

  public displayResults(response) {
    
  var formattedJson = JSON.stringify(response.result, null, 2);
  document.getElementById('query-output').nodeValue = formattedJson;
}


  
   
}
