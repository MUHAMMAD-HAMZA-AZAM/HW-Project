import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../Shared/HttpClient/_http';
import { NgxSpinnerService } from "ngx-spinner";
import { SupplierReportVM } from '../../Shared/Models/UserModel/SpTradesmanVM';
import { DynamicScriptLoaderService } from '../../Shared/CommonServices/dynamicScriptLoaderService';
import { Router, NavigationEnd, RouterEvent, ActivatedRoute } from '@angular/router';
import { DatePipe } from '@angular/common';
import html2canvas from 'html2canvas';
declare const jsPDF: any
@Component({
  selector: 'app-supplier-report',
  templateUrl: './supplier-report.component.html',
  styleUrls: ['./supplier-report.component.css']
})
export class SupplierReportComponent implements OnInit {
  public SupplierList: SupplierReportVM[] = [];
  public completeList: number;
  public fromDate = new Date()
  public toDate = new Date()
  public startDate = "";
  public endDate = "";
  public DataNotFound = "";
  public pageMode = "";
  public pipe;
  public reportTitle = "";
  constructor(public service: CommonService, public Loader: NgxSpinnerService, private route: Router, private router: ActivatedRoute, public dynamicScripts: DynamicScriptLoaderService) {
    var _path = this.router.snapshot.routeConfig.path;
    this.pageMode = _path.split(",")[1];
    this.populateSupplierReport();
  }

  ngOnInit() {
  }
  populateSupplierReport() {

    if (this.pageMode == "1" ) {
      this.toDate = new Date();
      this.fromDate = new Date(new Date().setDate(new Date().getDate() - 1));
      this.reportTitle = "Supplier Daily Report";
    }
    else if (this.pageMode == "2") {
      this.toDate = new Date();
      this.fromDate = new Date(new Date().setDate(new Date().getDate() - 7));
      this.reportTitle = "Supplier Weekly Report";
    }
    else if (this.pageMode == "3") {
      this.toDate = new Date();
      this.fromDate = new Date(new Date().setDate(new Date().getDate() - 14));
      this.reportTitle = "Supplier Two Weeks Report";
    }
    else if (this.pageMode == "4") {
      this.toDate = new Date();
      this.fromDate = new Date(new Date().setDate(new Date().getDate() - 30));
      this.reportTitle = "Supplier Monthly Report";
    }
    this.pipe = new DatePipe('en-US');
    this.startDate = this.pipe.transform(this.fromDate, 'MM/dd/yyyy');
    this.endDate = this.pipe.transform(this.toDate, 'MM/dd/yyyy');


    this.Loader.show();
    this.service.get(this.service.apiRoutes.Users.getAllSupplierLast24Hours + "?startDate=" + this.startDate + "&endDate=" + this.endDate).subscribe(result => {
      if (result.json() != null) {
        
        this.SupplierList = result.json();
        this.completeList = this.SupplierList.length;
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
        pdf.save('SuppliersCompleteReport.pdf'); // Generated PDF  
      });
    })
    this.Loader.hide();
  }

}
