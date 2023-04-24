import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from "ngx-spinner";
import { CommonService } from '../../Shared/HttpClient/_http';
import { CustomerReportVM } from '../../Shared/Models/UserModel/SpTradesmanVM';
import html2canvas from 'html2canvas';
import { DynamicScriptLoaderService } from '../../Shared/CommonServices/dynamicScriptLoaderService';
import { Bids } from '../../Shared/Models/JobModel/JobModel';
import { Router, NavigationEnd, RouterEvent, ActivatedRoute } from '@angular/router';
import { DatePipe } from '@angular/common';
declare const jsPDF: any

@Component({
  selector: 'app-active-bids-daily',
  templateUrl: './active-bids-daily.component.html',
  styleUrls: ['./active-bids-daily.component.css']
})
export class ActiveBidsDailyComponent implements OnInit {
  public bidsList: Bids[] = [];
  public completeList: number;
  public fromDate = new Date()
  public toDate = new Date();
  public startDate = "";
  public endDate = "";
  public DataNotFound = "";
  public pageMode = "";
  public pipe;
  public reportTitle = "";
  constructor(public service: CommonService, public Loader: NgxSpinnerService, private route: Router, private router: ActivatedRoute, public dynamicScripts: DynamicScriptLoaderService) {
    var _path = this.router.snapshot.routeConfig.path;
    this.pageMode = _path.split(",")[1];
    this.populateActiveBidsReport();
  }

  ngOnInit() {
  }
  populateActiveBidsReport() {

    if (this.pageMode == "1") {
      this.toDate = new Date();
      this.fromDate = new Date(new Date().setDate(new Date().getDate() - 1));
      this.reportTitle = "Bids Daily Report";
    }
    else if (this.pageMode == "2") {
      this.toDate = new Date();
      this.fromDate = new Date(new Date().setDate(new Date().getDate() - 7));
      this.reportTitle = "Bids Weekly Report";
    }
    else if (this.pageMode == "3") {
      this.toDate = new Date();
      this.fromDate = new Date(new Date().setDate(new Date().getDate() - 14));
      this.reportTitle = "Bids Two Weeks Report";
    }
    else if (this.pageMode == "4") {
      this.toDate = new Date();
      this.fromDate = new Date(new Date().setDate(new Date().getDate() - 30));
      this.reportTitle = "Bids Monthly Report";
    }
    this.pipe = new DatePipe('en-US');
    this.startDate = this.pipe.transform(this.fromDate, 'MM/dd/yyyy');
    this.endDate = this.pipe.transform(this.toDate, 'MM/dd/yyyy');

    this.Loader.show();
    this.service.get(this.service.apiRoutes.Users.getActiveBidsLastDay + "?startDate=" + this.startDate + "&endDate=" + this.endDate).subscribe(result => {
      
      if (result.json() != null) {
        this.bidsList = result.json();
        console.log(this.bidsList)
        this.completeList = this.bidsList.length;
         this.DataNotFound = "";
      }
      else {
        this.DataNotFound = "No Record Found!"
      }
      this.Loader.hide();
    },
      error => {
        console.log(error);
        this.Loader.hide();
      });
  }
  DownloadPdf() {
    
    this.Loader.show();
    this.dynamicScripts.load("pdf").then(data => {
      var htmltable = document.getElementById('dataTable');
      html2canvas(htmltable).then(canvas => {
        // Few necessary setting options  
        var imgWidth = 208;
        var pageHeight = 295;
        var imgHeight = canvas.height * imgWidth / canvas.width;
        var heightLeft = imgHeight;

        const contentDataURL = canvas.toDataURL('image/png')
        let pdf = new jsPDF('p', 'mm'); // A4 size page of PDF  
        var position = 0;
        pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight)
        pdf.save('CustomerCompleteReport.pdf'); // Generated PDF  
      });
    })
    this.Loader.hide();
  }
}
