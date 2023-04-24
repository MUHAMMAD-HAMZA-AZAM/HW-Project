import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { CommonService } from '../../Shared/HttpClient/_http';
import { NgxSpinnerService } from "ngx-spinner";
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReportDatesValidation } from '../../Shared/Enums/enums';
import { ReportDateVm } from '../../Shared/Models/HomeModel/HomeModel';
//import { debug } from 'util';
import { CustomerReportVM } from '../../Shared/Models/UserModel/SpTradesmanVM';
import html2canvas from 'html2canvas';
import { DynamicScriptLoaderService } from '../../Shared/CommonServices/dynamicScriptLoaderService';
import { DatePipe } from '@angular/common';
import { PostedAdsDailyComponent } from '../posted-ads-daily/posted-ads-daily.component';
import { JobQuotations } from '../../Shared/Models/JobModel/JobModel';
import { ClassGetter } from '@angular/compiler/src/output/output_ast';
import { ExcelFileService } from '../../Shared/CommonServices/excel-file.service';
import { ExportToCsv } from 'export-to-csv';
import { ToastrService } from 'ngx-toastr';
import { jsPDF } from 'jspdf';
import 'jspdf-autotable';
import { UserOptions } from 'jspdf-autotable';
//declare const jsPDF: any;
interface jsPDFWithPlugin extends jsPDF {
  autoTable: (options: UserOptions) => jsPDF;
}

@Component({
  selector: 'app-manual-reports-posted-jobs',
  templateUrl: './manual-reports-posted-jobs.component.html',
  styleUrls: ['./manual-reports-posted-jobs.component.css']
})
export class ManualReportsPostedJobsComponent implements OnInit {

  public startDate: Date;
  public endDate: Date;
  public startDate1: Date;
  public endDate1: Date;

  public fromDate: Date;
  public toDate: Date;
  public fromDate1: Date;
  public toDate1: Date;
  public startDateErrorMessage: string;
  public endDateErrorMessage: string;
  public cityid: "";
  public lastActive = false;
  public location = "";
  public submittedApplicationForm = false;
  public showTable = false;
  public showNullMessage = false;
  //public JobsList: JobQuotations[] = [];
  public JobsList = [];
  public excelFileList = [];
  public statusList: [];
  public customerdropdownSettings;
  public statusdropdownSettings;
  public completeList: number;
  public completeStatusList: number;
  public selectedCustomers = [];
  public selectedStatus = [];
  public cityList = [];
  public dropdownListForCity = {};
  public dropdownListForColumn = {};
  public selectedCity = [];
  public selectedColumn = [];
  public columnList = [];
  public customerName = "";
  public tradesmanName = "";
  public customerSearch = 1;
  public tradesmanSearch = 4;
  public jobStatus = [];
  public selectedCities = [];
  public pipe;
  public jobId = "";
  public jobDetId = "";
  public customerAddressList = [];
  public searchFromAddress = [];
  public searchedAddress = [];
  public isAddress = false;
  public locationSearchKeyMatch = false;
  public reportFilter = false;
  public allowview;
  public userType = 3;

  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    
  @ViewChild('checkParent', { static: true }) checkParent: ElementRef;
  @ViewChild('restTradesmanChecked', { static: true }) restTradesmanChecked: ElementRef;
  @ViewChild('dataTable', { static: true }) dataTable: ElementRef;
  constructor(
    public toastrService: ToastrService,
    public service: CommonService,
    public Loader: NgxSpinnerService,
    private router: Router,
    private formBuilder: FormBuilder,
    public dynamicScripts: DynamicScriptLoaderService,
    public excelService: ExcelFileService

  ) {

  }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Posted Jobs"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.populateCustomerAddress();
    this.customerdropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };
    this.statusdropdownSettings = {
      singleSelection: false,
      idField: 'statusId',
      textField: 'name',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };
    this.dropdownListForCity = {
      singleSelection: false,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };
    this.dropdownListForColumn = {
      singleSelection: false,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };
    this.selectedCustomers = [];
    this.selectedStatus = [];
    this.selectedCity = [];
    this.selectedColumn = [];
    this.CustomerCustomeReportS();
    this.getAllCities();


    this.columnList = [{ Id: 1, value: "CNIC" },
    { Id: 2, value: "Last Modified" },
    { Id: 3, value: "City" },
    { Id: 4, value: "Town" },
    { Id: 5, value: "Last Modified" }
    ];
    console.log(this.columnList);
  }
  resetForm() {
    
    this.location = "";
    this.endDate = null;
    this.startDate = null;
    this.tradesmanName = "";
    this.customerName = "";
    this.jobStatus = [];
    this.selectedCities = []
    this.selectedCity = [];
    this.selectedStatus = [];
    this.jobId = "";
    this.jobDetId = "";
    this.fromDate = null;
    this.toDate = null;
    const parent = this.checkParent.nativeElement
    parent.querySelector("#start").checked = true;
    parent.querySelector("#start1").checked = true;
    parent.querySelector("#all").checked = true;
    this.userType = 3;
  }
  funReportFilter() {
    this.reportFilter = !this.reportFilter;
  }

  exportCSV() {
    const options = {
      fieldSeparator: ',',
      quoteStrings: '"',
      decimalSeparator: '.',
      showLabels: true,
      showTitle: true,
      title: 'PostedJobsCSV',
      useTextFile: false,
      useBom: true,
      useKeysAsHeaders: true,
    };
    const csvExporter = new ExportToCsv(options);
    csvExporter.generateCsv(this.excelFileList);
  }
  DownloadXlsx() {
    this.excelService.exportAsExcelFile(this.excelFileList, "PostedJobsReport")
  }
  populateCustomerAddress() {
    this.service.get(this.service.apiRoutes.Jobs.GetPostedJobsAddressList).subscribe(result => {
      console.log(result.json());
      this.customerAddressList = result.json();
      
      this.customerAddressList.forEach(value => {
        let add = { "customerAddress": value.jobAddress }
        this.searchFromAddress.push(add);
      })
    })
  }
  serachAddress(input) {
    if (input.length > 0) {
      this.searchedAddress = [];
      if (this.searchFromAddress.length > 0) {
        this.searchFromAddress.forEach(value => {
          if (value.customerAddress.toLowerCase().includes(input.toLowerCase())) {
            let add = { "customerAddress": value.customerAddress };
            this.searchedAddress.push(add);
          }
          if (this.searchedAddress.length > 0) {
            this.locationSearchKeyMatch = true;
          }
          else {
            this.locationSearchKeyMatch = false;
          }
        })
        this.isAddress = true;
      }
      else {
        this.isAddress = true;
        this.locationSearchKeyMatch = false;

      }
    } else {
      this.searchedAddress = [];
      this.isAddress = false;
    }
  }

  setAddressValue(e) {
    this.location = e.target.text;
    this.isAddress = false;
  }
  getRadioValueut(e) {
    this.userType = e.target.value;
  }
  getRadioValue(e) {
    this.customerSearch = e.target.value;
  }
  getTrademanRadioValue(e) {
    this.tradesmanSearch = e.target.value;
  }



  loadStatusdropdown() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Jobs.GetJobStatusForDropdown).subscribe(result => {
      
      this.statusList = result.json();
      console.log(this.statusList);
      this.completeStatusList = this.statusList.length;

      this.Loader.hide();
    },
      error => {
        console.log(error);
        this.Loader.hide();
      });

  }
  CustomerCustomeReportS() {
    
    //this.submittedApplicationForm = true;
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Users.getAllCustonmersDropdown).subscribe(result => {
      
      this.JobsList = result.json();

      console.log(this.JobsList);
      this.completeList = this.JobsList.length;
      if (this.JobsList.length > 0) {

        this.loadStatusdropdown();
      }
      else {

        this.loadStatusdropdown();
      }
      this.Loader.hide();
    },
      error => {
        console.log(error);
        this.Loader.hide();
        this.loadStatusdropdown();
      });

  }
  DownloadPdf() {

    const doc = new jsPDF('l', 'px', 'a4') as jsPDFWithPlugin;
    const pdfTable = this.dataTable.nativeElement;
    doc.autoTable({
      styles: { fontSize: 8 },
      html: pdfTable
    });
    doc.save('table.pdf')
    //
    //this.Loader.show();
    //this.dynamicScripts.load("pdf").then(data => {
    //  var htmltable = document.getElementById('dataTable');
    //  var shtmltable = document.getElementById('summary');
    //  html2canvas(htmltable).then(canvas => {
    //    // Few necessary setting options  
    //    var imgWidth = 208;
    //    var pageHeight = 295;
    //    var imgHeight = canvas.height * imgWidth / canvas.width;
    //    var heightLeft = imgHeight;

    //    const contentDataURL = canvas.toDataURL('image/png')
    //    let pdf = new jsPDF('p', 'mm'); // A4 size page of PDF  
    //    var position = 0;
    //    pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight)
    //    pdf.save('PostedJobsManualReport.pdf'); // Generated PDF  
    //    this.Loader.hide();
    //  });

    //})

  }
  public jobList = [];
  public jobcount = 0;
  public skipDate: boolean = false;

  ////////////
  public loadJobs() {
    var customerpra = "";
    var tradesmanpra = "";
    
    this.jobList = [];
    this.jobcount = 0;
    let cids = [];
    let sids = [];
    let ctids = [];
    //this.selectedCustomers.forEach(function (item) {
    //  cids.push(item.id);
    //});
    this.selectedStatus.forEach(function (item) {
      sids.push(item.statusId);
    });
    this.selectedCity.forEach(function (item) {

      ctids.push(item.id);
    });
    console.log(this.selectedCustomers);
    console.log(this.selectedStatus);
    console.log(this.selectedCity);
    console.log(this.location);
    console.log(this.lastActive);

    if (this.customerName != "" && this.customerName != undefined) {
      if (this.customerSearch == 1) {
        customerpra = this.customerName + "%";
      }
      else if (this.customerSearch == 2) {
        customerpra = "%" + this.customerName + "%";
      }
      else if (this.customerSearch == 3) {
        customerpra = "%" + this.customerName;
      }
    }
    
    if (this.tradesmanName != "" && this.tradesmanName != undefined) {
      if (this.tradesmanSearch == 4) {
        tradesmanpra = this.tradesmanName + "%";
      }
      else if (this.tradesmanSearch == 5) {
        tradesmanpra = "%" + this.tradesmanName + "%";
      }
      else if (this.tradesmanSearch == 6) {
        tradesmanpra = "%" + this.tradesmanName;
      }
    }


    this.pipe = new DatePipe('en-US');
    this.startDate1 = this.pipe.transform(this.startDate, 'MM/dd/yyyy');
    this.endDate1 = this.pipe.transform(this.endDate, 'MM/dd/yyyy');
    this.fromDate1 = this.pipe.transform(this.fromDate, 'MM/dd/yyyy');
    this.toDate1 = this.pipe.transform(this.toDate, 'MM/dd/yyyy');
    console.log(this.startDate);
    let query = ""
    //if (this.skipDate) {
    //  query = this.service.apiRoutes.Jobs.GetJobsForReport + "?customer=" + cids + "&status=" + sids;
    //}
    //else {
    //  query = this.service.apiRoutes.Jobs.GetJobsForReport + "?startDate=" + this.startDate + "&endDate=" + this.endDate + "&customer=" + cids + "&status=" + sids + "&city=" + ctids + "&lastActive=" + this.lastActive + "&location=" + this.location;
    //}
    query = this.service.apiRoutes.Jobs.getJobsForDynamicReport + "?postedstartDate=" + this.startDate1 + "&postedendDate=" + this.endDate1 + "&endfromDate=" + this.fromDate1 + " &endtoDate=" + this.toDate1 + "&customer=" + customerpra + "&tradesman=" + tradesmanpra + "&status=" + sids + "&city=" + ctids + "&lastActive=" + this.lastActive + "&location=" + this.location + "&jobId=" + this.jobId + "&jobDetId=" + this.jobDetId + "&userType=" + this.userType;

    console.log(query);
    this.Loader.show();
    this.service.get(query).subscribe(result => {
      
      this.jobList = [];
      this.JobsList = result.json();
      console.log(this.JobsList);
      if (this.JobsList) {
        this.excelFileList = this.JobsList.map((job) => {
          return {
            jobQuotationId: job.jobQuotationId,
            firstName: job.firstName,
            lastName: job.lastName,
            workTitle: job.workTitle,
            workDescription: job.workDescription,
            customerMobileNo: job.customerMobileNo,
            PostedJobDate: this.service.formatDate(job.createdOn, "YYYY-MM-DD"),
            city: job.city,
            Town: job.area,
            jobAddress: job.jobAddress,
            skillName: job.skillName,
            subSkillName: job.subSkillName,
            workBudget: job.workBudget,
            tradesmanName: job.tradesmanName,
            desiredBids: job.desiredBids,
            statusName: job.statusName,
          }
        })
        this.reportFilter = true;
        this.showTable = true;
        this.showNullMessage = false;
        this.jobcount = this.JobsList.length;

      }
      else {
        this.toastrService.error("No record found !", "Search");
        this.showNullMessage = false;
        this.showTable = false;

      }
      this.Loader.hide();
    },
      error => {
        console.log(error);
        this.Loader.hide();

      });
  }
  onCustomerSelectAll(items: any) {
    console.log(items);
    this.selectedCustomers = items;


  }
  onCustomerDeSelectALL(items: any) {
    this.selectedCustomers = [];
    console.log(items);
  }
  onCustomerSelect(item: any) {
    this.selectedCustomers.push(item);
    //console.log(this.selectedCategories);
  }
  onCustomerDeSelect(item: any) {

    this.selectedCustomers = this.selectedCustomers.filter(
      function (value, index, arr) {
        //console.log(value);
        return value.id != item.id;
      }

    );

  }
  onStatusSelectAll(items: any) {
    console.log(items);
    this.selectedStatus = items;


  }
  onStatusDeSelectALL(items: any) {
    this.selectedStatus = [];
    console.log(items);
  }
  onStatusSelect(items: any) {
    this.selectedStatus.push(items);
    //console.log(this.selectedCategories);
  }
  onStatusDeSelect(items: any) {

    this.selectedStatus = this.selectedStatus.filter(
      function (value, index, arr) {
        //console.log(value);
        return value.id != items.id;
      }

    );

  }
  //  City Drop Setting
  onCitySelectAll(item: any) {
    console.log(item);
    this.selectedCity = item;
  }
  onCityDeSelectALL(item: any) {
    this.selectedCity = [];
    console.log(item);
  }
  onCitySelect(item: any) {
    this.selectedCity.push(item);
    //console.log(this.selectedCategories);
  }
  onCityDeSelect(item: any) {

    this.selectedCity = this.selectedCity.filter(
      function (value, index, arr) {
        return value.id != item.id;
      });
  }

  onColumnSelectAll(item: any) {
    console.log(item);
    this.selectedColumn = item;
  }
  onColumnDeSelectALL(item: any) {
    this.selectedColumn = [];
    console.log(item);
  }
  onColumnSelect(item: any) {
    this.selectedColumn.push(item);
    //console.log(this.selectedCategories);
  }
  onColumnDeSelect(item: any) {

    this.selectedColumn = this.selectedColumn.filter(
      function (value, index, arr) {
        return value.id != item.id;
      });
  }

  public getAllCities() {
    this.service.get(this.service.apiRoutes.Common.GetCityList).subscribe(result => {
      this.cityList = result.json();

    })
  }
}

