import { Component, OnInit } from '@angular/core';
import { SupplierReportVM } from '../../Shared/Models/UserModel/SpTradesmanVM';
import { CommonService } from '../../Shared/HttpClient/_http';
import { NgxSpinnerService } from "ngx-spinner";
import html2canvas from 'html2canvas';
import { DynamicScriptLoaderService } from '../../Shared/CommonServices/dynamicScriptLoaderService';
declare const jsPDF: any

@Component({
  selector: 'app-annual-report-supplier',
  templateUrl: './annual-report-supplier.component.html',
  styleUrls: ['./annual-report-supplier.component.css']
})
export class AnnualReportSupplierComponent implements OnInit {
  public SupplierList: SupplierReportVM[] = [];
  public completeList: number;
  constructor(public service: CommonService, public Loader: NgxSpinnerService, public dynamicScripts: DynamicScriptLoaderService) {
    this.populateSupplierReport();
  }

  ngOnInit() {
  }
  populateSupplierReport() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Users.getAllSupplierYearlyReport).subscribe(result => {
      
      this.SupplierList = result.json();
      this.completeList = this.SupplierList.length;
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
        pdf.save('SupplierAnnualReport.pdf'); // Generated PDF  
      });
    })
    this.Loader.hide();
  }
}
