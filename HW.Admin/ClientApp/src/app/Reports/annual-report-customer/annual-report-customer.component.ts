import { Component, OnInit } from '@angular/core';
import { CustomerReportVM } from '../../Shared/Models/UserModel/SpTradesmanVM';
import { CommonService } from '../../Shared/HttpClient/_http';
import { NgxSpinnerService } from "ngx-spinner";
import html2canvas from 'html2canvas';
import { DynamicScriptLoaderService } from '../../Shared/CommonServices/dynamicScriptLoaderService';
declare const jsPDF: any

@Component({
  selector: 'app-annual-report-customer',
  templateUrl: './annual-report-customer.component.html',
  styleUrls: ['./annual-report-customer.component.css']
})
export class AnnualReportCustomerComponent implements OnInit {
  public CustomerList: CustomerReportVM[] = [];

  public completeList: number;
  constructor(public service: CommonService, public Loader: NgxSpinnerService, public dynamicScripts: DynamicScriptLoaderService) {
    this.populateCustomerReport();
  }

  ngOnInit() {
  }
  populateCustomerReport() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Users.getAllCustomerYearlyReport).subscribe(result => {
      
      this.CustomerList = result.json();
      this.completeList = this.CustomerList.length;
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
        pdf.save('CustomerAnnualReport.pdf'); // Generated PDF  
      });
    })
    this.Loader.hide();
  }
}
