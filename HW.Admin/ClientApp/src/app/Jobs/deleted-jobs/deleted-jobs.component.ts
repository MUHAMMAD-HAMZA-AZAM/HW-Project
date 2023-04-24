import { Component, ElementRef, Host, Input, OnInit, Pipe, ViewChild, ViewChildren, QueryList } from '@angular/core';
import { CommonService } from 'src/app/Shared/HttpClient/_http';
import { GetactiveJobList, GetAllaJobsCount, GetDeletedJobList, jobDetails } from 'src/app/Shared/Models/JobModel/JobModel';
import { NgxSpinnerService } from "ngx-spinner";
import { SortList } from 'src/app/Shared/Sorting/sortList';
import { ActivatedRoute, Router } from '@angular/router';
import { JobDetails } from 'src/app/Shared/Models/JobModel/JobModel';
import { DatePipe } from '@angular/common';
import { FormBuilder, FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import { ok } from 'assert';
import { ModalDirective } from 'ngx-bootstrap/modal/modal.directive';
import { ToastrService } from 'ngx-toastr';
import { DynamicScriptLoaderService } from '../../Shared/CommonServices/dynamicScriptLoaderService';
import { ExcelFileService } from '../../Shared/CommonServices/excel-file.service';
import { ExportToCsv } from 'export-to-csv';
import html2canvas from 'html2canvas';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import autoTable from 'jspdf-autotable'
import { jsPDF } from 'jspdf';
import 'jspdf-autotable';
import { UserOptions } from 'jspdf-autotable';
import { httpStatus, BidStatus } from '../../Shared/Enums/enums';
import { ResponseVm } from '../../Shared/Models/HomeModel/HomeModel';
import { JwtHelperService } from '@auth0/angular-jwt';
//declare const jsPDF: any;
interface jsPDFWithPlugin extends jsPDF {
  autoTable: (options: UserOptions) => jsPDF;
}

@Component({
  selector: 'app-deleted-jobs',
  templateUrl: './deleted-jobs.component.html',
  styleUrls: ['./deleted-jobs.component.css']
})
export class DeletedJobsComponent implements OnInit {



  @ViewChildren("checkboxes") checkboxes: QueryList<ElementRef>;
  @ViewChild('pdfTable', { static: true }) pdfTable: ElementRef;
  @ViewChild('pdfTable1') pdfTable1: ElementRef;
  @ViewChild('restChecked', { static: true }) restChecked: ElementRef;
  @ViewChild('restTradesManChecked', { static: true }) restTradesManChecked: ElementRef;
  @ViewChild('restUserType', { static: true }) restUserType: ElementRef;
  @ViewChild('dataTable', { static: true }) dataTable: ElementRef;

  public getDeletedJobList: GetDeletedJobList[] = [];
  public cloneList: GetDeletedJobList[] = [];
  public selecteddata: GetDeletedJobList[] = [];
  public selecteddatasingle = [];
  public jobdetail: jobDetails[] = []
  public data: GetAllaJobsCount = new GetAllaJobsCount();
  // public pageing: PageingCount = new PageingCount();
  public pdfrequest: boolean = true;
  public pageing1 = [];
  public actionPageName: string = "7";
  public pageNumber: number = 1;
  public totalJobs: number;
  public totalPages: number;
  public selectedPage: number;
  public searchTerm: string;
  public status: boolean = true;
  public firstPageactive: boolean;
  public lastPageactive: boolean;
  public previousPageactive: boolean;
  public nextPageactive: boolean;
  public recoardNoFrom = 0;
  public recoardNoTo = 50;
  public pagercoards;
  public noOfRecoardShowOnPage;
  public totalRecoards = 101;
  public noOfPages;
  public noOfPageshtml: string;
  public dataOrderBy = "DESC";
  public quotationId;
  public deleterights;
  public allowview;
  public CustomerId: number;
  public comment: string;
  public overallRating: number;
  public activity: string;
  public jobDetails: JobDetails;
  public pageSize: number = 30;
  public pipe;
  public section = 1;
  public peopleByCountry = [];
  public peopleByCountry1 = [];
  public lastdate: Date;
  public nodata: string;
  public customerSearch: number = 1;
  public town: string = '';
  public searchBy: 1;
  public searchBytrd: 1;

  public startDate: Date;
  public endDate: Date;
  public startDate1: Date;
  public endDate1: Date;
  public location = "";
  public customerName = "";
  public jobId = "";
  public cityList = [];
  public selectedCities = [];
  public selectedCity = [];
  public selectedColumn = [];
  public dropdownListForCity = {};
  public dropdownListForColumn = {};

  public isAddress = false;
  public searchedAddress = [];
  public completeStatusList: number;
  public statusList: [];
  public startDateErrorMessage: string;
  public endDateErrorMessage: string;
  public submittedApplicationForm = false;
  public selectedStatus = [];
  public statusdropdownSettings = {};
  public jobStatus = [];
  public jobdetailid = "";
  public dataNotFound: boolean = false;
  public tradesmanName: string;
  public fromDate: Date;
  public toDate: Date;
  public fromDate1: Date;
  public toDate1: Date;
  public usertype = 3;
  public excelFileList = [];
  public activitytab = "1";
  public website = [];
  public showreporttable = 0;
  public responseVm: ResponseVm;
  public searchBtnClicked: boolean = false;
  public customerId;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  public jobQuotationId;
  public allowEdit;
  public allowDelete;
  public AssignJobAllowEdit;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  @ViewChild('postJobModalConfirm', { static: true }) postJobModal: ModalDirective;
  @ViewChild('JobsDetailsModal', { static: true }) JobDetailsModal: ModalDirective;
  @ViewChild('jobDetailsContent', { static: true }) jobDetailsContent: ModalDirective;
  constructor(public dynamicScripts: DynamicScriptLoaderService,
    public _modalService: NgbModal,
    private formBuilder: FormBuilder,
    public excelService: ExcelFileService, public toastr: ToastrService, public httpService: CommonService, public Loader: NgxSpinnerService, public sortList: SortList, public _host: ElementRef, private router: Router) {
    this.pagercoards = 50;
    this.populateData();
    this.GetDeletedJobList();
  }
  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Deleted Jobs"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    debugger;
    this.loadStatusdropdown();
    this.searchBytrd = 1;
    this.usertype = 3;
    this.searchBy = 1;
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

    this.selectedStatus = [];
    this.activitytab = "1";
    this.pdfrequest = true;
    this.getAllCities();
    this.Loader.show();
  }
  setAddressValue(e) {
    this.location = e.target.text;
    this.isAddress = false;
  }
  getRadioValue(e) {
    this.searchBy = e.target.value;
  }
  getRadioValuetrd(e) {
    this.searchBytrd = e.target.value;
  }
  exportCSV() {
    const options = {
      fieldSeparator: ',',
      quoteStrings: '"',
      decimalSeparator: '.',
      showLabels: true,
      showTitle: true,
      title: 'DeletedJobsReport',
      useTextFile: false,
      useBom: true,
      useKeysAsHeaders: true,
    };
    const csvExporter = new ExportToCsv(options);
    if (this.selecteddatasingle.length == 0)
      csvExporter.generateCsv(this.excelFileList);
    else
      csvExporter.generateCsv(this.selecteddatasingle);
  }
  DownloadXlsx() {
    if (this.selecteddatasingle.length == 0)
      this.excelService.exportAsExcelFile(this.excelFileList, "DeletedJobsReport")
    else
      this.excelService.exportAsExcelFile(this.selecteddatasingle, "DeletedJobsReport")
  }
  DownloadPdf() {  
    setTimeout(() => { this.downloadpdf1() }, 1000)
  }

  downloadpdf1() {
    
    const doc = new jsPDF('l', 'px', 'a4') as jsPDFWithPlugin;
    const pdfTable = this.dataTable.nativeElement;
    doc.autoTable({
      styles: { fontSize: 8 },
      html: pdfTable
    });
    doc.save('table.pdf')

  }

  onCheckboxChange(e, obj) {
    // customerid, customername, mobile, town, jqid, jdid, jobtitle, tradesmanname, skillname, subskillname, wb, status, createdon//
    if (e.target.checked) {

      this.selecteddatasingle.push({//obj

        'CustomerId': obj.jobId,
        'CustomerName': obj.customerName,
        'Mobile': obj.mobileNumber,
        'Town': obj.town,
        'JobQuotationId': obj.customerId,
        'JobDetailId': obj.jobDetailId,
        'JobTitle': obj.jobTitle,
        'TradesmanName': obj.tradesmanName,
        'Skills': obj.skillName,
        'SubSkills': obj.subSkillName,
        'WorkBudget': obj.workBudget,
        'Status': obj.statusId,
        'CreatedOn': obj.createdOn


      });
     
    }
    else {
      this.selecteddatasingle = this.selecteddatasingle.filter(item => item.JobQuotationId != e.target.value);
    }
  }
  resetForm() {
    this.location = "";
    this.endDate = null;
    this.startDate = null;
    this.customerName = "";
    this.selectedCities = []
    this.selectedCity = [];
    this.jobId = "";
    this.jobStatus = [];
    this.selectedStatus = [];
    this.jobdetailid = "";
    this.fromDate = null;
    this.toDate = null;
    this.tradesmanName = "";
    this.selecteddatasingle = [];
    this.usertype = 3;

    //this.uncheckAll();
    this.restChecked.nativeElement.checked = true;
    this.restTradesManChecked.nativeElement.checked = true;
    this.restUserType.nativeElement.checked = true;

  }
  GetDeletedJobList() {
    let cityIds = [];
    let sids = [];
    this.selectedCity.forEach(function (item) {
      cityIds.push(item.id);
    });
    var customerpra = "";
    if (this.customerName != "" && this.customerName != undefined) {
      if (this.searchBy == 1) {
        customerpra = this.customerName + "%";
      }
      else if (this.searchBy == 2) {
        customerpra = "%" + this.customerName + "%";
      }
      else if (this.searchBy == 3) {
        customerpra = "%" + this.customerName;
      }
    }

    this.selectedStatus.forEach(function (item) {
      sids.push(item.statusId);
    });
    if (sids.length == 0)
      sids = [1, 2, 3, 4, 5,6,7];
    this.pipe = new DatePipe('en-US');
    this.startDate1 = this.pipe.transform(this.startDate, 'MM/dd/yyyy');
    this.endDate1 = this.pipe.transform(this.endDate, 'MM/dd/yyyy');
    this.fromDate1 = this.pipe.transform(this.fromDate, 'MM/dd/yyyy');
    this.toDate1 = this.pipe.transform(this.toDate, 'MM/dd/yyyy');
    this.Loader.show();
    this.httpService.get(this.httpService.apiRoutes.DeletedJobList.GetDeletedJobList + "?pageNumber=" + this.pageNumber + "&pageSize=" + this.data.pageSize + "&dataOrderBy=" + this.dataOrderBy
      + "&customerName=" + customerpra + "&jobId=" + this.jobId + "&startDate=" + this.startDate1
      + "&endDate=" + this.endDate1 + "&city=" + cityIds + "&status=" + BidStatus.Deleted + "&jobdetailid=" + this.jobdetailid + "&fromDate=" + this.fromDate1 + "&toDate=" + this.toDate 
      + "&usertype=" + this.usertype + "&location=" + this.location + "&town=" + this.town ).subscribe(result => {
        this.getDeletedJobList = null;
        if (result.json() != null) {
          this.getDeletedJobList = result.json();
          if (this.selecteddatasingle.length > 0) {

            for (var x = 0; x < this.getDeletedJobList.length; x++) {
              this.getDeletedJobList[x].isselectedforexport = false;
              var xx = this.selecteddatasingle.find(z => z.JobQuotationId == this.getDeletedJobList[x].customerId)
              if (xx != null && xx != undefined && xx != "")
                this.getDeletedJobList[x].isselectedforexport = true;
            }
          }
          else {
            for (var x = 0; x < this.getDeletedJobList.length; x++) {
              this.getDeletedJobList[x].isselectedforexport = false;
            }
          }

          this.cloneList = this.getDeletedJobList;

          console.log(this.cloneList);
          this.excelFileList = this.getDeletedJobList;
          this.noOfPages = this.getDeletedJobList[0].noOfRecoards / this.data.pageSize

          this.noOfPages = Math.floor(this.noOfPages);
          if (this.getDeletedJobList[0].noOfRecoards > this.noOfPages) {
            this.noOfPages = this.noOfPages + 1;
          }
          this.totalRecoards = this.getDeletedJobList[0].noOfRecoards;
          this.pageing1 = [];
          for (var x = 1; x <= this.noOfPages; x++) {
            this.pageing1.push(
              x
            );
          }
          this.recoardNoFrom = (this.data.pageSize * this.pageNumber) - this.data.pageSize + 1;
          this.recoardNoTo = (this.data.pageSize * this.pageNumber);
          if (this.recoardNoTo > this.getDeletedJobList[0].noOfRecoards)
            this.recoardNoTo = this.getDeletedJobList[0].noOfRecoards;
          //this.JobDetailsFunction(this.getActiveJobList[0].customerId, 'pending');
          this.dataNotFound = false;
        }
        else {

          this.recoardNoFrom = 0;
          this.recoardNoTo = 0;
          this.noOfPages = 0;
          this.totalRecoards = 0;
          this.getDeletedJobList = [];
          this.cloneList = [];
          this.toastr.warning(" No Job Available for Delete !!!");
          this.dataNotFound = false;
        }
        this.Loader.hide();
      },
        error => {
          alert(error);
          //this.Loader.hide();
        }
      );
  }

  populateData() {
    this.Loader.show();
    this.data.pageSize = this.pagercoards;
    this.httpService.get(this.httpService.apiRoutes.Jobs.GetAllJobsCount).subscribe(result => {
   
      this.data = result.json();
      this.data.pageSize = this.pagercoards;
      this.totalJobs = this.data.activeJobs;
      this.NumberOfPages();
    },
      error => {
        this.Loader.hide();
        alert(error)
      }
    );
  }
  getRadioValueut(e) {
    this.usertype = e.target.value;
  }
  GetTotalPageList() {
    var array = new Array();
    for (var i = 1; i <= this.totalPages; i++) {
      array.push(i);
    }
    return array;
  }
  SelectedPageData(page) {
    this.pageNumber = page;
    this.status = true;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = false;
    this.GetDeletedJobList();
  }
  NumberOfPages() {
    this.totalPages = Math.ceil(this.totalJobs / this.data.pageSize);
  }
  changePageSize() {
    this.pageNumber = 1;
    this.NumberOfPages();
    this.GetDeletedJobList();
  }
  lastPage() {
    this.status = false;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = true;
    this.pageNumber = this.totalPages;
    this.GetDeletedJobList();

  }
  FirstPage() {
    this.status = false;
    this.firstPageactive = true;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = false;
    this.pageNumber = 1;
    this.GetDeletedJobList();
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
    this.GetDeletedJobList();
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
    this.GetDeletedJobList();
  }
  Filter(value: any) {

    if (!value) {
      this.cloneList = this.getDeletedJobList;
    }
    else {
      this.cloneList = this.getDeletedJobList.filter(item => item.customerId.toString().includes(value) || item.mobileNumber.includes(value));
    }
  }
  sorting(columnName: string, orderType: string) {
    
    this.cloneList = this.sortList.transform(this.cloneList, columnName, orderType);


    let sortElement;
    let oppositElement;

    if (orderType == "asc") {

      sortElement = this._host.nativeElement.querySelectorAll('.up');
      oppositElement = this._host.nativeElement.querySelectorAll('.down');
    }
    else {

      sortElement = this._host.nativeElement.querySelectorAll('.down');
      oppositElement = this._host.nativeElement.querySelectorAll('.up');
    }

    for (let i = 0; i < sortElement.length; i++) {
      let el = sortElement[i];

      oppositElement[i].classList.remove('enable')

      let div = el.closest('#' + columnName);
      if (div !== null) {
        //console.log(div.querySelector('#h0'));
        (el).classList.add('enable');
      }
      else {
        (el).classList.remove('enable');
      }
    }

  }

  clickchange() {
    this.GetDeletedJobList();
  }
  PageSizeChange() {

    this.pageNumber = 1;
    this.GetDeletedJobList();
  }
  PriviousClick() {
    if (this.pageNumber > 1) {
      this.pageNumber = parseInt(this.pageNumber.toString()) - 1;
      this.GetDeletedJobList();
    }

  }
  NextClick() {

    if (this.pageNumber < this.noOfPages) {
      this.pageNumber = parseInt(this.pageNumber.toString()) + 1;
      this.GetDeletedJobList();
    }

  }
  LoadNewestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "DESC";
    this.GetDeletedJobList();

  }
  LoadOldestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "ASC";
    this.GetDeletedJobList();

  }
  hidemodal1() {
    this._modalService.dismissAll();
  }
  hidemodal() {
    this.peopleByCountry = [];
    this.activitytab = "1";
    this._modalService.dismissAll();
  }
  currenttab2() {

    this.activitytab = "2";
  }
  currenttab3() {

    this.activitytab = "3";
  }
  currenttab4() {
    this.activitytab = "4";
  }
  currenttab1() {
    this.activitytab = "1";
  }

  DeleteJobByJobQuotationId(jobQoutationId, deleteJob) {

    this.quotationId = jobQoutationId;
    this._modalService.open(deleteJob);
  }
  deletejobconfirm() {

    if ((this.totalRecoards % this.data.pageSize) == 1 && this.pageNumber == this.noOfPages) {
      if (this.pageNumber > 1)
        this.pageNumber = parseInt(this.pageNumber.toString()) - 1;
    }

    this.httpService.get(this.httpService.apiRoutes.Jobs.deleteJobWithJobQuotationId + "?jobQuotationId=" + this.quotationId + "&actionPageName=" + this.actionPageName).subscribe(result => {
      this.Loader.show();
      if (result.status = 200) {
        this.GetDeletedJobList();
        this.populateData();
        this.quotationId = "";
        this._modalService.dismissAll();
        this.toastr.error("Job Deleted successfully", "Delete");
        this.Loader.hide();
      }
    });
  }

 onStatusSelectAll(items: any) {
    this.selectedStatus = items;
  }
  onStatusDeSelectALL(items: any) {
    this.selectedStatus = [];
  }
  onStatusSelect(items: any) {
    this.selectedStatus.push(items);
    
  }
  onStatusDeSelect(items: any) {

    this.selectedStatus = this.selectedStatus.filter(
      function (value, index, arr) {
        return value.statusId != items.statusId;
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
  }
  onColumnDeSelect(item: any) {

    this.selectedColumn = this.selectedColumn.filter(
      function (value, index, arr) {
        return value.id != item.id;
      });
  }
  public getAllCities() {
    this.httpService.get(this.httpService.apiRoutes.Common.GetCityList).subscribe(result => {
      this.cityList = result.json();
    })
  }
  loadStatusdropdown() {
    debugger;
    this.httpService.get(this.httpService.apiRoutes.Jobs.GetJobStatusForDropdown).subscribe(result => {
      this.statusList = result.json();
      console.log(this.statusList);
      this.completeStatusList = this.statusList.length;
    },
      error => {
        console.log(error);
        this.Loader.hide();
      });

  }


  numberOnly(event): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }

}
