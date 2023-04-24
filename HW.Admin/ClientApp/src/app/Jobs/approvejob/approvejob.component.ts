import { Component, ElementRef, Host, Input, OnInit, Pipe, ViewChild, ViewChildren, QueryList } from '@angular/core';
import { CommonService } from 'src/app/Shared/HttpClient/_http';
import { GetactiveJobList, GetAllaJobsCount, GetCustomerJobsCount, jobDetails } from 'src/app/Shared/Models/JobModel/JobModel';
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
import { httpStatus } from '../../Shared/Enums/enums';
import { CSJobStatus } from '../../Shared/Enums/enums';
import { ResponseVm } from '../../Shared/Models/HomeModel/HomeModel';
import { JwtHelperService } from '@auth0/angular-jwt';
//declare const jsPDF: any;
interface jsPDFWithPlugin extends jsPDF {
  autoTable: (options: UserOptions) => jsPDF;
}

@Component({
  selector: 'app-approvejob',
  templateUrl: './approvejob.component.html',
  styleUrls: ['./approvejob.component.css']
})
export class ApprovejobComponent implements OnInit {


  @ViewChildren("checkboxes") checkboxes: QueryList<ElementRef>;
  @ViewChild('pdfTable') pdfTable: ElementRef;
  @ViewChild('pdfTable1') pdfTable1: ElementRef;
  @ViewChild('restChecked', { static: true }) restChecked: ElementRef;
  @ViewChild('restTradesManChecked', { static: true }) restTradesManChecked: ElementRef;
  @ViewChild('restUserType', { static: true }) restUserType: ElementRef;
  @ViewChild('dataTable') dataTable: ElementRef;
  public csJobSatusEnum = CSJobStatus;
  public getActiveJobList: GetactiveJobList[] = [];
  public cloneList: GetactiveJobList[] = [];
  public selecteddata: GetactiveJobList[] = [];
  public selecteddatasingle = [];
  public jobdetail: jobDetails[] = []
  public data: GetAllaJobsCount = new GetAllaJobsCount();
  public customerjobsdata: GetCustomerJobsCount = new GetCustomerJobsCount();
  // public pageing: PageingCount = new PageingCount();
  public pageing1 = [];
  public customerjobspageing = [];
  public customerdata = [];

  public pageNumber: number = 1
  public customerjobspageNumber: number = 1;
  public totalJobs: number;
  public totalPages: number;
  public customerjobstotalPages: number;
  public pdfrequest: boolean = true;
  public selectedPage: number;
  public searchTerm: string;
  public status: boolean = true;
  public customerjobstatus: boolean = true;
  public firstPageactive: boolean;
  public customerjobsfirstPageactive: boolean;
  public lastPageactive: boolean;
  public customerjobslastPageactive: boolean;
  public previousPageactive: boolean;
  public customerjobspreviousPageactive: boolean;
  public nextPageactive: boolean;
  public customerjobsnextPageactive: boolean;
  public recoardNoFrom = 0;
  public recoardNoTo = 50;
  public customerrecoardNoFrom = 0;
  public customerrecoardNoTo = 50;
  public pagercoards;
  public customerpagercoards;
  public noOfRecoardShowOnPage;
  public totalRecoards = 101;
  public customertotalRecoards = 101;
  public noOfPagesCustomerJobs;
  public noOfPages;
  public noOfPageshtml: string;
  public dataOrderBy = "DESC";
  public customerjobsdataOrderBy = "DESC";
  public quotationId;
  public deleterights;
  public allowview;
  public CustomerId: number;
  public comment: string;
  public overallRating: number;
  public activity: string;
  public jobDetails: JobDetails;
  public pageSize: number = 30;
  public studentPageSize = 5;
  public pipe;
  public section = 1;
  public peopleByCountry = [];
  public peopleByCountry1 = [];
  public lastdate: Date;
  public nodata: string;
  public customerSearch: number = 1;
  public  town: string;
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
  public statusdropdownSettings;
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
  public customerJobsList = [];
  public activitytab = "1";
  public website = [];
  public showreporttable = 0;
  public appValForm: FormGroup;
  public assignJobForm: FormGroup;
  public businessProfile: any;
  public loaderSala: boolean = true;
  public hasNoImage: boolean = false;
  public CustomerJobsShow: boolean = false;
  public approveId: string = '';
  public responseVm: ResponseVm;
  public serachTradesId;
  public serachTradesmanName = "";
  public serachTradesmanMobileNo = "";
  public tradesmanNameList = [];
  public searchBtnClicked: boolean = false;
  public customerId;
  public customerIdforpagination;
  public isOrganization: boolean = false;
  public assignJobObj;
  public decodedtoken;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  public isbidCount: boolean = false;
  public bidCount;
  public isBidCount: boolean = false;
  public jobQuotationId;
  public csjqJobStatusId;
  public area: string = '';
  public isCSJobStatus: boolean = false;
  public actionPageName: string = "1";
  public csJobStatusList = [];
  public allowEdit;
  public allowDelete;
  public AssignJobAllowEdit;
  public AssignJobAllowView;
  @ViewChild('postJobModalConfirm', { static: true }) postJobModal: ModalDirective;
  @ViewChild('JobsDetailsModal', { static: true }) JobDetailsModal: ModalDirective;
  @ViewChild('jobDetailsContent', { static: true }) jobDetailsContent: ModalDirective;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  constructor(public dynamicScripts: DynamicScriptLoaderService,
    public _modalService: NgbModal,
    public common: CommonService,
    private formBuilder: FormBuilder,
    public excelService: ExcelFileService, public toastr: ToastrService, public httpService:
      CommonService, public Loader: NgxSpinnerService, public sortList: SortList, public _host: ElementRef, private router: Router) {
    this.pagercoards = 50;
    this.customerpagercoards = 10;
    this.populateData();
    debugger;
    this.GetActiveJobList();
    this.getCSJobStatusList();
  }
  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Authorize Job"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.assignJobForm = this.formBuilder.group({
      amount: ['', [Validators.required]],
      comments: [''],
    });
    this.appValForm = this.formBuilder.group({
      workBudget: ['', [Validators.required]],
      visitCharges: [''],
      serviceCharges: [''],
      otherCharges: [''],
      csJobStatus: [0],
      quantity: [1],
      estimatedCommission: [''],
      remarksDescription: [''],
      changeStatus: ['']
    });
    this.loadStatusdropdown();
    this.searchBytrd = 1;
    this.usertype = 3;
    this.searchBy = 1;
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
    this.selectedStatus = [];
    this.activitytab = "1";
    this.pdfrequest = true;
    this.getAllCities();
    this.Loader.show();
  }
  get f() {
    return this.appValForm.controls;
  }
  calculateTotalServiceCharges(serviceCharges?: any, quantity?: any) {
   
    let totalCharges;
    let estimatedCommision;
    if (quantity) {
      let serviceCharges = this.appValForm.value.serviceCharges;
      totalCharges = (quantity * serviceCharges);
      estimatedCommision = (totalCharges * 25) / 100;
    }
    else {
      let qty = this.appValForm.value.quantity;
      totalCharges = (qty * serviceCharges);
      estimatedCommision = (totalCharges * 25) / 100;
    }
    this.appValForm.controls['otherCharges'].setValue(totalCharges);
    this.appValForm.controls['estimatedCommission'].setValue(estimatedCommision);
  }
  showCustomerModel() {

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
      title: 'AuthorizejobsReport',
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
      this.excelService.exportAsExcelFile(this.excelFileList, "AuthorizejobsReport")
    else
      this.excelService.exportAsExcelFile(this.selecteddatasingle, "AuthorizejobsReport")
  }
  DownloadPdf() {
    
    this.pdfrequest = false;
    setTimeout(() => { this.downloadpdf1() }, 1000)
    setTimeout(() => { this.pdfrequest = true; }, 3000)
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

  uncheckAll() {
    //
    //this.checkboxes.forEach((element) => {
    //  element.nativeElement.checked = false;
    //});
    // this.callNotPicked.nativeElement.checked = false;
    // document.getElementById("callNotPicked").innerHTML.
    this.appValForm.controls['csJobStatus'].setValue(0);
    this.appValForm.controls['remarksDescription'].setValue('');
    this.csjqJobStatusId = 0;
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
      //  this.selecteddata.splice(0)
    } else {
      //var xxxx = this.selecteddatasingle.findIndex(item => item.JobQuotationId !== e.target.value);
      this.selecteddatasingle = this.selecteddatasingle.filter(item => item.JobQuotationId != e.target.value);
      //  this.selecteddata.splice(0)
      // this.selecteddata.splice(0)
    }

    //this.selecteddata = this.selecteddatasingle; 
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
  public trackById = (index, item) => item.customerId;
  GetActiveJobList() {
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

    var tradesmanpra = "";
    if (this.tradesmanName != "" && this.tradesmanName != undefined) {
      if (this.searchBytrd == 1) {
        tradesmanpra = this.tradesmanName + "%";
      }
      else if (this.searchBytrd == 2) {
        tradesmanpra = "%" + this.tradesmanName + "%";
      }
      else if (this.searchBytrd == 3) {
        tradesmanpra = "%" + this.tradesmanName;
      }
    }
    ;
    this.selectedStatus.forEach(function (item) {
   
      sids.push(item.statusId);
    });
    if (sids.length == 0)
      sids = [1, 2, 3, 4, 5];
    this.pipe = new DatePipe('en-US');
    this.startDate1 = this.pipe.transform(this.startDate, 'MM/dd/yyyy');
    this.endDate1 = this.pipe.transform(this.endDate, 'MM/dd/yyyy');
    this.fromDate1 = this.pipe.transform(this.fromDate, 'MM/dd/yyyy');
    this.toDate1 = this.pipe.transform(this.toDate, 'MM/dd/yyyy');
    this.Loader.show();
    this.httpService.get(this.httpService.apiRoutes.SpGetActiveJobList.SpGetPendingJobList + "?pageNumber=" + this.pageNumber + "&pageSize=" + this.data.pageSize + "&dataOrderBy=" + this.dataOrderBy
      + "&customerName=" + customerpra + "&jobId=" + this.jobId + "&startDate=" + this.startDate1
      + "&endDate=" + this.endDate1 + "&city=" + cityIds + "&status=" + sids + "&jobdetailid=" + this.jobdetailid + "&fromDate=" + this.fromDate1 + "&toDate=" + this.toDate + "&tradesmanName=" + tradesmanpra
      + "&usertype=" + this.usertype + "&location=" + this.location + "&athorize=" + false + "&town=" + this.town).subscribe(result => {
        this.getActiveJobList = null;
        debugger;
        if (result.json() != null) {
        
          this.getActiveJobList = result.json();
          console.log(this.getActiveJobList)
          if (this.selecteddatasingle.length > 0) {

            for (var x = 0; x < this.getActiveJobList.length; x++) {
              this.getActiveJobList[x].isselectedforexport = false;
              var xx = this.selecteddatasingle.find(z => z.JobQuotationId == this.getActiveJobList[x].customerId)
              if (xx != null && xx != undefined && xx != "")
                this.getActiveJobList[x].isselectedforexport = true;
            }
          }
          else {
            for (var x = 0; x < this.getActiveJobList.length; x++) {
              this.getActiveJobList[x].isselectedforexport = false;
            }
          }
          //totalrecords
          this.cloneList = this.getActiveJobList;

          //this.excelFileList = this.getActiveJobList;
          for (let item in this.getActiveJobList) {
            let obj = {
              'CustomerId': this.getActiveJobList[item].jobId,
              'CustomerName': this.getActiveJobList[item].customerName,
              'Mobile': this.getActiveJobList[item].mobileNumber,
              'City': this.getActiveJobList[item].city,
              'Town': this.getActiveJobList[item].town,
              'Address': this.getActiveJobList[item].streetAddress,
              'JobQuotationId': this.getActiveJobList[item].customerId,
              'JobTitle': this.getActiveJobList[item].jobTitle,
              'WorkBudget': this.getActiveJobList[item].workBudget,
              'WorkStartDate': this.getActiveJobList[item].createdOn
            }
            this.excelFileList.push(obj);
          }
          this.noOfPages = this.getActiveJobList[0].noOfRecoards / this.data.pageSize

          this.noOfPages = Math.floor(this.noOfPages);
          if (this.getActiveJobList[0].noOfRecoards > this.noOfPages) {
            this.noOfPages = this.noOfPages + 1;
          }
          this.totalRecoards = this.getActiveJobList[0].noOfRecoards;
          this.pageing1 = [];
          for (var x = 1; x <= this.noOfPages; x++) {
            this.pageing1.push(x);
          }
          this.recoardNoFrom = (this.data.pageSize * this.pageNumber) - this.data.pageSize + 1;
          this.recoardNoTo = (this.data.pageSize * this.pageNumber);
          if (this.recoardNoTo > this.getActiveJobList[0].noOfRecoards)
            this.recoardNoTo = this.getActiveJobList[0].noOfRecoards;
          //this.JobDetailsFunction(this.getActiveJobList[0].customerId, 'pending');
          this.dataNotFound = false;
        }
        else {

          this.recoardNoFrom = 0;
          this.recoardNoTo = 0;
          this.noOfPages = 0;
          this.totalRecoards = 0;
          this.getActiveJobList = [];
          this.cloneList = [];
          this.toastr.warning(" No Job Available for Authorize !!!");
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

  GetCustomerJobs(jobId, contentCustomerJobs) {
    
    this.Loader.show();
    this.customerIdforpagination = jobId;
    this.customerjobsdata.pageSize = this.customerpagercoards;
    this._modalService.open(contentCustomerJobs, { size: 'xl' })
    this.Loader.hide();
    this.GetCustomerJobsList();
  }

  GetCustomerJobsList() {
    this.Loader.show();

    let obj = {
      pageNumber: this.customerjobspageNumber,
      pageSize: this.customerjobsdata.pageSize,
      dataOrderBy: this.dataOrderBy,
      customerId: this.customerIdforpagination
    };

    this.httpService.post(this.httpService.apiRoutes.Jobs.SpGetJobsByCustomerId, obj).subscribe(result => {
      
      //  this.customerJobsList = null;
      this.customerJobsList = result.json();
      this.Loader.hide();
      if (this.customerJobsList.length > 0) {
        this.CustomerJobsShow = true;
        this.noOfPagesCustomerJobs = this.customerJobsList[0].totalRecords / this.customerjobsdata.pageSize
        this.noOfPagesCustomerJobs = Math.floor(this.noOfPagesCustomerJobs);
        if (this.customerJobsList[0].totalRecords > this.noOfPagesCustomerJobs) {
          this.noOfPagesCustomerJobs = this.noOfPagesCustomerJobs + 1;
        }
        this.customertotalRecoards = this.customerJobsList[0].totalRecords;
        this.customerjobspageing = [];
        for (var x = 1; x <= this.noOfPagesCustomerJobs; x++) {
          this.customerjobspageing.push(
            x
          );
        }
        this.customerrecoardNoFrom = (this.customerjobsdata.pageSize * this.customerjobspageNumber) - this.customerjobsdata.pageSize + 1;
        this.customerrecoardNoTo = (this.customerjobsdata.pageSize * this.customerjobspageNumber);
        if (this.customerrecoardNoTo > this.customerJobsList[0].totalRecords)
          this.customerrecoardNoTo = this.customerJobsList[0].totalRecords;
        //this.JobDetailsFunction(this.getActiveJobList[0].customerId, 'pending');

      }
      else {
        this.CustomerJobsShow = false;
        this.customerrecoardNoFrom = 0;
        this.customerrecoardNoTo = 0;
        this.noOfPagesCustomerJobs = 0;
        this.customertotalRecoards = 0;
        this.customerJobsList = [];
      }

    },
      error => {
        console.log(error);
        alert(error);
        this.Loader.hide();
      });
  }

  showModel(content, tradsamanId) {
    this.Loader.show();
   
    this.httpService.get(this.httpService.apiRoutes.Users.getBussinessProfile + "?userId=" + tradsamanId + "&role=" + 'Tradesman').subscribe(result => {
      this.businessProfile = null;
      this.businessProfile = result.json();
      if (this.businessProfile.skills != null) {
        this.businessProfile.skills = this.businessProfile.skills.split("&amp;").join(" ");
      }
      this._modalService.open(content);
      this.Loader.hide();
    })
  }
  populateData() {
    this.Loader.show();
    this.data.pageSize = this.pagercoards;
    this.httpService.get(this.httpService.apiRoutes.Jobs.GetAllJobsCount).subscribe(result => {
      this.data = result.json();
      this.data.pageSize = this.pagercoards;
      this.totalJobs = this.data.activeJobs;
      this.NumberOfPages();
      this.Loader.hide();
    },
      error => {
        this.Loader.hide();
        alert(error)
      }
    );
  }
  getCSJobStatusList() {

    this.httpService.get(this.httpService.apiRoutes.Jobs.GetCsJobStatusDropdown).subscribe(x => {
     
      this.csJobStatusList = x.json();
    });
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
  JobDetailsFunction(customerId, jobDetailsContent) {
    this.jobDetails = null;
    this.activitytab = "1";
    this.Loader.show();
    this.httpService.get(this.httpService.apiRoutes.Jobs.GetJobDetailsList + "?pageNumber=" + this.pageNumber + "&pageSize=" + this.pageSize + "&customerId=" + customerId).subscribe(result => {
      this.lastdate = null;
      this.jobDetails = result.json();
      if (this.jobDetails.notificationDTO == null) {
        this.nodata = "Data Not Exist";
      }
      else if (this.jobDetails.notificationDTO.length < 1) {
        this.nodata = "Data Not Exist";
      }
      else {
        this.nodata = "";
      }
      if (this.jobDetails.jobActivity != null) {
        if (this.jobDetails.jobActivity.length > 0) {
          this.pipe = new DatePipe('en-US');
          this.peopleByCountry = [];
          for (let i = 0; i < this.jobDetails.jobActivity.length; i++) {
            if (this.lastdate != this.pipe.transform(this.jobDetails.jobActivity[i].createdDate, 'MM/dd/yyyy')) {
              this.lastdate = this.pipe.transform(this.jobDetails.jobActivity[i].createdDate, 'MM/dd/yyyy'),
                this.peopleByCountry.push(
                  {
                    'createdDate': this.jobDetails.jobActivity[i].createdDate,
                    'activitydetails': [
                      {
                        "text": this.jobDetails.jobActivity[i].status,

                        "value": this.jobDetails.jobActivity[i].activiyType,
                        "palceddate": this.jobDetails.jobActivity[i].createdDate
                      },
                    ]
                  },
                );
            }
            else {
              this.peopleByCountry1 = [];
              this.peopleByCountry1.push(
                {

                  'createdDate': this.jobDetails.jobActivity[i].createdDate,
                  'activitydetails': [
                    {
                      "text": this.jobDetails.jobActivity[i].status,

                      "value": this.jobDetails.jobActivity[i].activiyType,
                      "palceddate": this.jobDetails.jobActivity[i].createdDate,
                    },
                  ]
                },
              );
              this.peopleByCountry[this.peopleByCountry.length - 1].activitydetails.push(this.peopleByCountry1[0].activitydetails[0]);

            }
          }
        }
      }
      this._modalService.open(jobDetailsContent, { windowClass: 'my-class' });
      this.Loader.hide();
    },
      error => {
        this.Loader.hide();
        alert(error)
      }
    );
  }
  SelectedPageData(page) {
    this.pageNumber = page;
    this.status = true;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = false;
    this.GetActiveJobList();
  }

  SelectedCustomerJobsPageData(page) {
    this.customerjobspageNumber = page;
    this.customerjobstatus = true;
    this.customerjobsfirstPageactive = false;
    this.customerjobspreviousPageactive = false;
    this.customerjobsnextPageactive = false;
    this.customerjobslastPageactive = false;
    this.GetCustomerJobsList();
  }
  NumberOfPages() {
    this.totalPages = Math.ceil(this.totalJobs / this.data.pageSize);
  }
  CustomerJobsNumberOfPages() {
    this.customerjobstotalPages = Math.ceil(this.customerjobstotalPages / this.customerjobsdata.pageSize);
  }
  changePageSize() {
    this.pageNumber = 1;
    this.NumberOfPages();
    this.GetActiveJobList();
  }
  customerjobschangePageSize() {
    this.customerjobspageNumber = 1;
    this.CustomerJobsNumberOfPages();
    this.GetCustomerJobsList();
  }
  lastPage() {
    this.status = false;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = true;
    this.pageNumber = this.totalPages;
    this.GetActiveJobList();

  }
  customerjobslastPage() {
    this.customerjobstatus = false;
    this.customerjobsfirstPageactive = false;
    this.customerjobspreviousPageactive = false;
    this.customerjobsnextPageactive = false;
    this.customerjobslastPageactive = true;
    this.customerjobspageNumber = this.totalPages;
    this.GetCustomerJobsList();

  }
  FirstPage() {
    this.status = false;
    this.firstPageactive = true;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = false;
    this.pageNumber = 1;
    this.GetActiveJobList();
  }
  CustomerJobsFirstPage() {
    this.customerjobstatus = false;
    this.customerjobsfirstPageactive = true;
    this.customerjobspreviousPageactive = false;
    this.customerjobsnextPageactive = false;
    this.customerjobslastPageactive = false;
    this.customerjobspageNumber = this.totalPages;
    this.customerjobspageNumber = 1;
    this.GetCustomerJobsList();
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
    this.GetActiveJobList();
  }
  customerjobsnextPage() {
    this.status = false;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = true;
    this.lastPageactive = false;
    if (this.customerjobstotalPages > this.customerjobspageNumber) {
      this.customerjobspageNumber++;
    }
    this.GetCustomerJobsList();
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
    this.GetActiveJobList();
  }
  customerjobspreviousPage() {
    this.customerjobstatus = false;
    this.customerjobsfirstPageactive = false;
    this.customerjobspreviousPageactive = true;
    this.customerjobsnextPageactive = false;
    this.customerjobslastPageactive = false;
    if (this.customerjobspageNumber > 1) {
      this.customerjobspageNumber--;
    }
    this.GetCustomerJobsList();
  }
  Filter(value: any) {

    if (!value) {
      this.cloneList = this.getActiveJobList;
    }
    else {
      this.cloneList = this.getActiveJobList.filter(item => item.customerId.toString().includes(value) || item.mobileNumber.includes(value));
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
  GetReCoards() {
  }
  clickchange() {
    this.GetActiveJobList();
  }
  customerjobsclickchange() {
    this.GetCustomerJobsList();
  }
  PageSizeChange() {

    this.pageNumber = 1;
    this.GetActiveJobList();
  }
  customerjobsPageSizeChange() {

    this.customerjobspageNumber = 1;
    this.GetCustomerJobsList();
  }
  PriviousClick() {
    if (this.pageNumber > 1) {
      this.pageNumber = parseInt(this.pageNumber.toString()) - 1;
      this.GetActiveJobList();
    }

  }
  NextClick() {

    if (this.pageNumber < this.noOfPages) {
      this.pageNumber = parseInt(this.pageNumber.toString()) + 1;
      this.GetActiveJobList();
    }

  }
  customerPriviousClick() {
  
    if (this.customerjobspageNumber > 1 ) {
      this.customerjobspageNumber = parseInt(this.customerjobspageNumber.toString()) - 1;
      this.GetCustomerJobsList();
    }
  }
  customerNextClick() {
    if (this.customerjobspageNumber < this.noOfPagesCustomerJobs) {
      debugger
      this.customerjobspageNumber = parseInt(this.customerjobspageNumber.toString()) + 1;
      this.GetCustomerJobsList();
    }
  }
  LoadNewestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "DESC";
    this.GetActiveJobList();

  }
  LoadOldestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "ASC";
    this.GetActiveJobList();

  }
  CustomerJobsLoadNewestRecoards() {
    this.customerjobspageNumber = 1;
    this.customerjobsdataOrderBy = "DESC";
    this.GetCustomerJobsList();

  }
  CustomerJobsLoadOldestRecoards() {
    this.customerjobspageNumber = 1;
    this.customerjobsdataOrderBy = "ASC";
    this.GetCustomerJobsList();

  }
  hidemodal1() {
    this._modalService.dismissAll();
  }
  hidemodal() {

    this.postJobModal.hide();
    this.JobDetailsModal.hide();
    this.jobDetailsContent.hide();
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
  GetJobQoutation(jobQoutationId) {

    this.httpService.NavigateToRouteWithQueryString(this.httpService.apiRoutes.Jobs.getqoutationId, { queryParams: { id: jobQoutationId } });
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
      let res = result.json();
      if (res.status == httpStatus.Ok) {
        this.GetActiveJobList();
        this.quotationId = "";
        this._modalService.dismissAll();
        //this.postJobModal.hide();
        this.toastr.error(res.message, "Delete");
        this.Loader.hide();
      }
    });
  }

  // Customer Model //
  GetBussinessProfile(CustomerId, jobQuotationId, contentCustomer) {
    this.Loader.show();
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    this.customerId = CustomerId;
    this.jobQuotationId = jobQuotationId;
    this.activitytab = "1";
    let modelRef = this._modalService.open(contentCustomer, { windowClass: 'my-class', backdrop: 'static', keyboard: false });
    modelRef.result.then(() => {
      this.submittedApplicationForm = false;
      this.GetActiveJobList();
      this.appValForm.setValue({
        csJobStatus: 0,
        quantity: 1,
        remarksDescription: '',
        changeStatus: ''
      })
    });
    this.Loader.show();
    this.httpService.get(this.httpService.apiRoutes.Users.getBussinessProfile + "?userId=" + CustomerId + "&role=" + 'Customer').subscribe(result => {
      this.businessProfile = null;
      this.businessProfile = result.json();
      if (this.businessProfile != null) {
        this.httpService.get(this.httpService.apiRoutes.Jobs.GetJobDetailsList + "?pageNumber=" + this.pageNumber + "&pageSize=" + this.pageSize + "&customerId=" + jobQuotationId).subscribe(result => {
          this.jobDetails = result.json();
          this.csjqJobStatusId = this.jobDetails.csjqJobStatusId;
                         

          if (this.csjqJobStatusId) {
            this.appValForm.controls.csJobStatus.setValue(this.csjqJobStatusId);
          }
          else {
            this.uncheckAll();
          }

          let obj = {
            workBudget: this.jobDetails.workBudget,
            visitCharges: this.jobDetails.visitCharges,
            serviceCharges: this.jobDetails.serviceCharges,
          }
          this.appValForm.patchValue(obj);
          this.calculateTotalServiceCharges(this.jobDetails.serviceCharges);
          this.Loader.hide();
        });
      }

      if (this.businessProfile.tradeName != null) {
        this.businessProfile.tradeName = this.businessProfile.tradeName.split("&amp;").join(" ");
      }
    },
      error => {
        console.log(error);
        alert(error);
        this.Loader.hide();
      });
  }

  // Save Budget
  saveBudget(id) {
    if (id) {
      this.httpService.get(this.httpService.apiRoutes.Bids.GetBidCountByJobId + "?jobQuotationId=" + id).subscribe(count => {
        var bidsCount = count.json();
        if (bidsCount.resultData) {
          this.isBidCount = true;
          setTimeout(() => {
            this.isBidCount = false;
          }, 3000)
        }
        else {
          if (this.jobDetails.workBudget) {
            this.appValForm.controls.workBudget.clearValidators();
            this.appValForm.controls.workBudget.updateValueAndValidity();
          }
          this.submittedApplicationForm = true;
          if (this.appValForm.value.changeStatus == 1) {
            this.appValForm.controls['remarksDescription'].clearValidators();
            this.appValForm.controls['remarksDescription'].updateValueAndValidity();
          }
          else {
            if (this.appValForm.value.csJobStatus > 0) {
              this.appValForm.controls['remarksDescription'].setValidators([Validators.required]);
              this.appValForm.controls['remarksDescription'].updateValueAndValidity();
            }
            else {
              this.appValForm.controls['remarksDescription'].clearValidators();
              this.appValForm.controls['remarksDescription'].updateValueAndValidity();
            }
          }
          if (this.appValForm.invalid) {
            return;
          }
          else {
            let data = {
              workBudget: this.appValForm.value.workBudget! ? this.appValForm.value.workBudget : this.jobDetails.workBudget,
              visitCharges: this.appValForm.value.visitCharges! ? this.appValForm.value.visitCharges : this.jobDetails.visitCharges,
              serviceCharges: this.appValForm.value.serviceCharges! ? this.appValForm.value.serviceCharges : this.jobDetails.serviceCharges,
              otherCharges: this.appValForm.value.otherCharges! ? this.appValForm.value.otherCharges : this.jobDetails.otherCharges,
              estimatedCommission: this.appValForm.value.estimatedCommission! ? this.appValForm.value.estimatedCommission : this.jobDetails.estimatedCommission,
              quantity: this.appValForm.value.quantity! ? this.appValForm.value.quantity : this.jobDetails.quantity,
              jobQuotationId: id,
              jobStatus: this.appValForm.value.csJobStatus! ? this.appValForm.value.csJobStatus : this.csjqJobStatusId,
              remarksDescription: this.appValForm.value.remarksDescription,
              createdBy: this.decodedtoken.UserId,
              changeStatus: this.appValForm.value.changeStatus
            };
            
            this.httpService.PostData(this.httpService.apiRoutes.Jobs.UpdateJobBudget, data, true).then(res => {
              this.responseVm = res.json();
              if (this.responseVm.status == httpStatus.Ok) {
                this.toastr.success("Successfully Updated", "Saved");
                if (this.appValForm.value.changeStatus == 1) {
                  this.cloneList = this.cloneList.filter(x => x.customerId != parseInt(id))
                }
              }
            });
          }
        }
      });
    }
  }


  // modal tradesman
  GetBussinessProfileTradesMan(CustomerId, contentTradesman) {
    
    this._modalService.open(contentTradesman, { windowClass: 'my-class' })
    this.httpService.get(this.httpService.apiRoutes.Users.getBussinessProfile + "?userId=" + CustomerId + "&role=" + 'Tradesman').subscribe(result => {
      this.businessProfile = null;
      this.businessProfile = result.json();
      if (this.businessProfile.skills != null) {
        this.businessProfile.skills = this.businessProfile.skills.split("&amp;").join(" ");
      }
    },
      error => {
        console.log(error);
        alert(error);
        this.Loader.hide();
      });
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
    this.httpService.get(this.httpService.apiRoutes.Common.GetCityList).subscribe(result => {
      this.cityList = result.json();
    })
  }
  loadStatusdropdown() {
    this.httpService.get(this.httpService.apiRoutes.Jobs.GetJobStatusForDropdown).subscribe(result => {
      this.statusList = result.json();
      this.completeStatusList = this.statusList.length;
    },
      error => {
        console.log(error);
        this.Loader.hide();
      });

  }

  public PopulateDataImage(id) {

    this.activitytab = "2";
    this.hasNoImage = true;
    this.Loader.show();
    this.httpService.get(this.httpService.apiRoutes.Customers.GetCustomerDetailsByIdWeb + "?quotationId=" + id).subscribe(data => {
      var result = data.json();
      this.jobdetail = data.json();
      this.Loader.hide();
      if (this.jobdetail.length == 0) {
        this.hasNoImage = true;
      }
      else {
        this.hasNoImage = false;
      }
    },
      error => {
        console.log(error);
      });
  }

  ApproveJob(id, postJobModalConfirm) {
    ;
    this._modalService.open(postJobModalConfirm);
    this.approveId = id;
  }

  ConfirmJob() {
    
    this.Loader.show();
    this.httpService.get(this.httpService.apiRoutes.Jobs.ApproveJob + "?JobQuotationId=" + this.approveId).subscribe(data => {
      let res = data.json();
      if (res.status == httpStatus.Ok) {
        this._modalService.dismissAll();
        this.cloneList = this.cloneList.filter(x => x.customerId != parseInt(this.approveId))
        this.toastr.success("Job Authorize successfully", "Authorize");
        this.Loader.hide();
      }
    })
  }
  bidNow(jobQuotationId) {
    this.activitytab = "3";
  }
  searchTrdesmanByName() {

    this.httpService.get(this.httpService.apiRoutes.TrdesMan.GetTradesmanByName + "?tradesmanName=" + this.serachTradesmanName + "&tradesmanId=" + this.serachTradesId + "&tradesmanPhoneNo=" + this.serachTradesmanMobileNo + "&jobQuotationId=" + this.jobQuotationId).subscribe(res => {
      this.tradesmanNameList = res.json();

      this.searchBtnClicked = true;
    })
  }
  assignJobToTradesman(trdId, jobQuoteId, modalContent) {

    this.assignJobObj = {
      tradesmanId: trdId,
      jobQuotationId: jobQuoteId,
    }
    this.httpService.get(this.httpService.apiRoutes.Bids.GetBidCountByJobId + "?tradesmanId=" + trdId + "&jobQuotationId=" + jobQuoteId).subscribe(res => {

      this.bidCount = res.json();

      if (this.bidCount.resultData) {
        this.isbidCount = true;
        setTimeout(() => {
          this.isbidCount = false;
        },
          5000)
      }
      else if (this.csjqJobStatusId == 1 || this.csjqJobStatusId == 4 || this.csjqJobStatusId == 5) {
        this.isCSJobStatus = true;
        setTimeout(() => {
          this.isCSJobStatus = false;
        },
          5000)
      }
      else {
        console.log(trdId, jobQuoteId);
        this._modalService.open(modalContent, { size: 'lg', centered: true })
      }
    });

  }

  assignJob() {
    let formVal = this.assignJobForm.value;
    this.assignJobObj.customerId = this.customerId;
    this.assignJobObj.amount = formVal.amount;
    this.assignJobObj.comments = formVal.comments;
    this.assignJobObj.createdBy = this.decodedtoken.UserId;

    this.httpService.PostData(this.httpService.apiRoutes.Jobs.AssignJobToTradesman, this.assignJobObj, true).then(res => {

      this.responseVm = res.json();

      if (this.responseVm.status == httpStatus.Ok) {
        this.toastr.success(this.responseVm.status, "Job Assigned")
        this.assignJobForm.reset();
        this._modalService.dismissAll();
      }
      else {
        this.toastr.error(this.responseVm.status, "Error")
      }
    })

    console.log(this.assignJobObj);
  }
  get g() {
    return this.assignJobForm.controls;
  }
  numberOnly(event): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }

  //public cancelModel() {
  //  this.appValForm.reset();
  // // this.GetActiveJobList();
  //  this._modalService.dismissAll();
  //}

}
