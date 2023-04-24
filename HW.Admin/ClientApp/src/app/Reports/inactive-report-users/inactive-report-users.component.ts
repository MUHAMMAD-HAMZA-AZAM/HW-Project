import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { DynamicScriptLoaderService } from '../../Shared/CommonServices/dynamicScriptLoaderService';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from "ngx-spinner";
import { CommonService } from '../../Shared/HttpClient/_http';
import html2canvas from 'html2canvas';
import { ExportToCsv } from 'export-to-csv';
import { ExcelFileService } from '../../Shared/CommonServices/excel-file.service';
import { DatePipe } from '@angular/common';
import { GetAllaJobsCount } from '../../Shared/Models/JobModel/JobModel';
import { ToastrService } from 'ngx-toastr';

import { jsPDF } from 'jspdf';
import 'jspdf-autotable';
import { UserOptions } from 'jspdf-autotable';
//declare const jsPDF: any;
interface jsPDFWithPlugin extends jsPDF {
  autoTable: (options: UserOptions) => jsPDF;
}

@Component({
  selector: 'app-inactive-report-users',
  templateUrl: './inactive-report-users.component.html',
  styleUrls: ['./inactive-report-users.component.css']
})
export class InactiveReportUsersComponent implements OnInit {
  public selectedDuration: string = '';
  public selectedUser: string = null;
  public InActiveUserList = [];
  public showTable = false;
  public completeList: number;
  public showNullMessage = false;
  public dataNotFound = false;
  public startDate: Date;
  public endDate: Date;
  public fromDate: Date;
  public toDate: Date;
  public fromDate1: Date;
  public toDate1: Date;
  public pipe;
  public startDateErrorMessage: string;
  public endDateErrorMessage: string;
  public excelFileList = [];
  public inactiveUserModel: any = {};
  public showTradeMan = false;
  public showSupplier = false;
  public showCustomer = false;


  public supplierdropdownSettings;
  public dropdownListForCity = {};
  public selectedCity = [];
  public cityList = [];
  public selectedCities = [];
  public selectedColumn = [];

  //pagination
  public data: GetAllaJobsCount = new GetAllaJobsCount();
  // public pageing: PageingCount = new PageingCount();
  public pageing1 = [];
  public pageNumber: number = 1;
  public totalPages: number;
  public selectedPage: number;
  public recoardNoFrom = 0;
  public recoardNoTo = 50;
  public pageSize: number = 30;
  public pagercoards;
  public noOfRecoardShowOnPage;
  public totalRecoards = 101;
  public noOfPages;
  public noOfPageshtml: string;
  public dataOrderBy = "DESC";
  public totalJobs: number;
  public searchTerm: string;
  public status: boolean = true;
  public firstPageactive: boolean;
  public lastPageactive: boolean;
  public previousPageactive: boolean;
  public nextPageactive: boolean;
  @ViewChild("f", { static: true }) f: ElementRef
  @ViewChild('dataTable', { static: true }) dataTable: ElementRef;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  constructor(public toastr: ToastrService , public excelService: ExcelFileService, public service: CommonService, public Loader: NgxSpinnerService, private router: Router, private formBuilder: FormBuilder, public dynamicScripts: DynamicScriptLoaderService) {
    this.pagercoards = 50;
    this.data.pageSize = 50;
  }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Inactive Users"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.startDate = null;
    this.endDate = null;
    this.inactiveUserModel = {
      selectedUser: null,

    }
    this.supplierdropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'value',
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
    this.selectedCity = [];
    this.getAllCities();
  }
  resetForm() {
    this.endDate = null;
    this.startDate = null;
    this.inactiveUserModel.selectedCities = []
    this.selectedCity = [];
    this.inactiveUserModel.selectedUser = null;
  }

  exportCSV() {
    const options = {
      fieldSeparator: ',',
      quoteStrings: '"',
      decimalSeparator: '.',
      showLabels: true,
      showTitle: true,
      title: 'InactiveuserReportCSV',
      useTextFile: false,
      useBom: true,
      useKeysAsHeaders: true,
    };
    const csvExporter = new ExportToCsv(options);
    csvExporter.generateCsv(this.excelFileList);
  }

  DownloadXlsx() {
    this.excelService.exportAsExcelFile(this.excelFileList, "InactiveuserReport")
  }
  selectChangeHandlerForDuration(event: any) {
    
    this.selectedDuration = event.target.value;
  }
  selectChangeHandlerForUser(event: any) {
    
    this.selectedUser = event.target.value;
  }
  checkUserType() {
    if (this.selectedUser == "Tradesman") {
      this.showTradeMan = true;
      this.showCustomer = false
      this.showSupplier = false;
    } else if (this.selectedUser == "Supplier") {
      this.showTradeMan = false;
      this.showCustomer = false
      this.showSupplier = true;
    } else {
      this.showTradeMan = false;
      this.showCustomer = true
      this.showSupplier = false;
    }
  }
  InActiveUserReport() {
    this.checkUserType();
    let cityIds = [];
    this.selectedCity.forEach(function (item) {
      cityIds.push(item.id);
    });
    this.pipe = new DatePipe('en-US');
    this.fromDate1 = this.pipe.transform(this.startDate, 'MM/dd/yyyy');
    this.toDate1 = this.pipe.transform(this.endDate, 'MM/dd/yyyy');
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Users.getAllInActiveFromToReport + "?pageNumber=" + this.pageNumber + "&pageSize="
      + this.data.pageSize + "&dataOrderBy=" + this.dataOrderBy + "&fromDate=" + this.fromDate1 + "&toDate=" + this.toDate1
      + "&city=" + cityIds + "&selectedUser=" + this.selectedUser).subscribe(result => {
        this.InActiveUserList = result.json();
        console.log(this.InActiveUserList);
        
        if (this.InActiveUserList != null) {
          this.excelFileList = this.InActiveUserList.map((data) => {
            return {
              firstName: data.firstName,
              lastName: data.lastName,
              cnic: data.cnic,
              mobileNumber: data.mobileNumber,
              createdOn: new Date(data.createdOn).toLocaleDateString(),
              lastActive: new Date(data.lastActive).toLocaleDateString(),
              city: data.city,
              businessAddress: data.businessAddress,
            }
          })

        this.noOfPages = this.InActiveUserList[0].noOfRecoards / this.data.pageSize
        this.noOfPages = Math.floor(this.noOfPages);
        if (this.InActiveUserList[0].noOfRecoards > this.noOfPages) {
          this.noOfPages = this.noOfPages + 1;
        }
        this.totalRecoards = this.InActiveUserList[0].noOfRecoards;
        this.pageing1 = [];
        for (var x = 1; x <= this.noOfPages; x++) {
          this.pageing1.push(x);
        }
        this.recoardNoFrom = (this.data.pageSize * this.pageNumber) - this.data.pageSize + 1;
        this.recoardNoTo = (this.data.pageSize * this.pageNumber);
          if (this.recoardNoTo > this.InActiveUserList[0].noOfRecoards) {
            this.recoardNoTo = this.InActiveUserList[0].noOfRecoards;
          }
        //this.JobDetailsFunction(this.InActiveUserList[0].customerId, 'activeJob');
        setTimeout(() => { this.Loader.hide() }, 1000);
        this.dataNotFound = true;
      }
      else {
        this.dataNotFound = false;
        this.toastr.error("No record found !", "Search");
        this.recoardNoFrom = 0;
        this.recoardNoTo = 0;
        this.noOfPages = 0;
        this.totalRecoards = 0;
        this.InActiveUserList = [];
        this.Loader.hide();
      }    
    });

    }

  //paginatioon
  SelectedPageData(page) {
    this.pageNumber = page;
    this.status = true;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = false;
    this.InActiveUserReport();
  }

  NumberOfPages() {
    this.totalPages = Math.ceil(this.totalJobs / this.data.pageSize);
  }

  changePageSize() {
    this.pageNumber = 1;
    this.NumberOfPages();
    this.InActiveUserReport();
  }

  lastPage() {
    this.status = false;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = true;
    this.pageNumber = this.totalPages;
    this.InActiveUserReport();

  }

  FirstPage() {
    this.status = false;
    this.firstPageactive = true;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = false;
    this.pageNumber = 1;
    this.InActiveUserReport();
  }

  nextPage() {
    this.status = false;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = true;
    this.lastPageactive = false;
    if (this.totalPages > this.pageNumber) {
      this.pageNumber++;
    }
    this.InActiveUserReport();
  }

  previousPage() {
    this.status = false;
    this.firstPageactive = false;
    this.previousPageactive = true;
    this.nextPageactive = false;
    this.lastPageactive = false;
    if (this.pageNumber > 1) {
      this.pageNumber--;
    }
    this.InActiveUserReport();
  }

  clickchange() {
    this.InActiveUserReport();
  }
  PageSizeChange() {

    this.pageNumber = 1;
    this.InActiveUserReport();
  }
  PriviousClick() {
    if (this.pageNumber > 1) {
      this.pageNumber = parseInt(this.pageNumber.toString()) - 1;
      this.InActiveUserReport();
    }

  }
  NextClick() {
    
    if (this.pageNumber < this.noOfPages) {
      this.pageNumber = parseInt(this.pageNumber.toString()) + 1;
      this.InActiveUserReport();
    }

  }
  LoadNewestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "DESC";
    this.InActiveUserReport();

  }
  LoadOldestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "ASC";
    this.InActiveUserReport();

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
    //    pdf.save('InActiveUsersReport.pdf'); // Generated PDF  
    //  });
    //})
    //this.Loader.hide();
  }
  //  City Drop Setting

  public getAllCities() {
    this.service.get(this.service.apiRoutes.Common.GetCityList).subscribe(result => {
      this.cityList = result.json();
      console.log(this.cityList)
    })
  }

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


 }

