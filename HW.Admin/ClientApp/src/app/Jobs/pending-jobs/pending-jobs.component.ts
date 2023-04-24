import { Component, ElementRef, Host, Input, OnInit, Pipe, ViewChild, ViewChildren, QueryList } from '@angular/core';
import { CommonService } from 'src/app/Shared/HttpClient/_http';
import { GetactiveJobList, GetAllaJobsCount, GetCustomerJobsCount } from 'src/app/Shared/Models/JobModel/JobModel';
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
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

import autoTable from 'jspdf-autotable'
import { jsPDF } from 'jspdf';
import 'jspdf-autotable';
import { UserOptions } from 'jspdf-autotable';
import { ResponseVm } from '../../Shared/Models/HomeModel/HomeModel';
import { CSJobStatus, httpStatus } from '../../Shared/Enums/enums';
import { JwtHelperService } from '@auth0/angular-jwt';
import * as FileSaver from 'file-saver';
import * as XLSX from 'xlsx';
const EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
const EXCEL_EXTENSION = '.xlsx';
interface jsPDFWithPlugin extends jsPDF {
  autoTable: (options: UserOptions) => jsPDF;
}

@Component({
  selector: 'app-pending-jobs',
  templateUrl: './pending-jobs.component.html',
  styleUrls: ['./pending-jobs.component.css']
})
export class PendingJobsComponent implements OnInit {
  @ViewChildren("checkboxes") checkboxes: QueryList<ElementRef>;
  @ViewChild('pdfTable') pdfTable: ElementRef;
  @ViewChild('pdfTable1') pdfTable1: ElementRef;
  @ViewChild('restChecked', { static: true }) restChecked: ElementRef;
  @ViewChild('restTradesManChecked', { static: true }) restTradesManChecked: ElementRef;
  @ViewChild('restUserType', { static: true }) restUserType: ElementRef;
  @ViewChild('dataTable', { static: true }) dataTable: ElementRef;
  public csJobSatusEnum = CSJobStatus;
  public jwtHelperService: JwtHelperService = new JwtHelperService();

  public getActiveJobList: GetactiveJobList[] = [];
  public cloneList: GetactiveJobList[] = [];
  public selecteddata: GetactiveJobList[] = [];
  public selecteddatasingle = [];
  public data: GetAllaJobsCount = new GetAllaJobsCount();
  public pageing1 = [];
  public pageNumber: number = 1;
  public totalJobs: number;
  public totalPages: number;
  public status: boolean = true;
  public firstPageactive: boolean;
  public lastPageactive: boolean;
  public previousPageactive: boolean;
  public nextPageactive: boolean;
  public recoardNoFrom = 0;
  public recoardNoTo = 50;
  public pagercoards;
  public totalRecoards = 101;
  public noOfPages;
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
  public jobStatusName: string;
  public csjqJobStatusId: any;
  public customerSearch: number = 1;
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
  public csJobStatusDropdown = [];
  public townDropdown = [];
  public selectedStatuses = [];
  public selectedTowns = [];
  public selectedTown = [];
  public selectedCsStatus = [];
  public csStatusdropdownSettings;
  public towndropdownSettings;
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
  public customerjobstotalPages: number;
  public jobdetailid = "";
  public dataNotFound: boolean;
  public tradesmanName: string;
  public fromDate: Date;
  public toDate: Date;
  public customerjobspageNumber: number = 1;
  public fromDate1: Date;
  public toDate1: Date;
  public customerIdforpagination;
  public usertype = 3;
  public town: string="";
  public excelFileList = [];
  public customertotalRecoards = 101;
  public activitytab = "1";
  public customerjobspreviousPageactive: boolean;
  public customerrecoardNoFrom = 0;
  public customerrecoardNoTo = 50;
  public website = [];
  public customerpagercoards;
  public noOfPagesCustomerJobs;
  public customerJobsList = [];
  public customerjobstatus: boolean = true;
  public showreporttable = 0;
  public customerjobsfirstPageactive: boolean;
  public customerjobspageing = [];
  public appValForm: FormGroup;
  public responseVm: ResponseVm;
  public businessProfile: any;
  public CustomerJobsShow: boolean = false;
  public customerjobslastPageactive: boolean;
  public customerjobsnextPageactive: boolean;
  public loaderSala: boolean = true;
  public pdfrequest: boolean = true;
  public changeStatusRef: NgbModalRef;
  public jobQuotationId;
  public bidId;
  public jobStatusByName;
  public decodedtoken;
  public actionPageName: string = "JobList";
  @ViewChild('postJobModalConfirm', { static: true }) postJobModal: ModalDirective;
  @ViewChild('JobsDetailsModal', { static: true }) JobDetailsModal: ModalDirective;
  @ViewChild('jobDetailsContent', { static: true }) jobDetailsContent: ModalDirective;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  public customerjobsdata: GetCustomerJobsCount = new GetCustomerJobsCount();

  constructor(public dynamicScripts: DynamicScriptLoaderService,
    public _modalService: NgbModal,
    public excelService: ExcelFileService, private formBuilder: FormBuilder, public toastr: ToastrService, public httpService: CommonService, public Loader: NgxSpinnerService, public sortList: SortList, public _host: ElementRef, private router: Router) {
    this.pagercoards = 50;
    this.customerpagercoards = 10;
    this.populateData();
    this.GetActiveJobList();
  }
  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Job List"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.loadStatusdropdown();
    this.searchBytrd = 1;
    this.usertype = 3;
    this.searchBy = 1;
    this.pdfrequest = true;
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
    this.csStatusdropdownSettings = {
      singleSelection: false,
      idField: 'statusId',
      textField: 'name',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };
    this.towndropdownSettings = {
      singleSelection: false,
      idField: 'townId',
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
    this.getAllCities();
    this.getCsJobStatus();
    this.gettownList();
    this.appValForm = this.formBuilder.group({
      jobQuotationId: [0],
      workBudget: [''],
      otherCharges: [''],
      serviceCharges: [''],
      materialCharges: [''],
      additionalCharges: [''],
      totalJobValue: [''],
      estimatedCommission: [''],
      quantity: [1],
      chargesDescription: [''],
      csJobStatus: [0],
      remarksDescription: [''],
      changeStatus: [''],
      agreedBudget:[''],
    });
  }

  public changeJobStatus(jqId, bidsId, content) {
    this.jobQuotationId = jqId;
    this.bidId = bidsId;
    this.changeStatusRef = this._modalService.open(content);
  }
  public changeStatusConfirmation() {
                   
    this.httpService.get(this.httpService.apiRoutes.Jobs.ChangeJobStatus + `?jqId=${this.jobQuotationId}&bidId=${this.bidId}`).subscribe(response => {
      let res = response.json();
      if (res.status = httpStatus.Ok) {
        this.toastr.success("Status changed successfully!", "Success");
        this._modalService.dismissAll();
        this.GetActiveJobList();
      }
    })
  }
  updateWithExtraCharges() {
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
      if (this.appValForm.value.jobQuotationId > 0) {
        let formData = this.appValForm.value;
        formData.createdBy = this.decodedtoken.UserId,
          this.httpService.PostData(this.httpService.apiRoutes.Jobs.UpdateJobExtraCharges, formData, true).then(result => {
            let res = result.json();
            if (res.status == 200) {
              this.toastr.success(res.message, "Success")
            }
            else {
              this.toastr.error(res.message, "Error")
            }
          })
      }
      else {
        this.toastr.error("Something went wrong!", "Error")
      }
    }
  }
  public calculateCommission(change?: any, value?: any) {
    let jobValue = 0;
    if (change == "mc") {
      jobValue = Number(this.jobDetails.workBudget) + Number(this.appValForm.value.otherCharges ? this.appValForm.value.otherCharges : 0)
        + Number(this.appValForm.value.additionalCharges ? this.appValForm.value.additionalCharges : 0)
        + Number(this.appValForm.value.materialCharges ? this.appValForm.value.materialCharges : 0)
      this.appValForm.controls['totalJobValue'].setValue(jobValue);
    }
    else {
      jobValue = 0
      let commissionValue = (Number(value) + Number(this.appValForm.value.otherCharges ? this.appValForm.value.otherCharges : 0)) * 25 / 100;
      this.appValForm.controls['estimatedCommission'].setValue(commissionValue);
      jobValue = Number(this.jobDetails.workBudget) + Number(this.appValForm.value.otherCharges ? this.appValForm.value.otherCharges : 0)
        + Number(this.appValForm.value.additionalCharges ? this.appValForm.value.additionalCharges : 0)
        + Number(this.appValForm.value.materialCharges ? this.appValForm.value.materialCharges : 0)
      this.appValForm.controls['totalJobValue'].setValue(jobValue);
    }
  }
  public trackById = (index, item) => item.customerId;
  GetActiveJobList() {

    let cityIds = [];
    let sids = [];
    this.selectedCity.forEach(function (item) {
      cityIds.push(item.id);
    });
    let csIds = [];
    let townIds = [];
    this.selectedStatuses.forEach(function (item) {
      csIds.push(item.statusId);
    });
    this.selectedTowns.forEach(function (item) {
      townIds.push(item.townId);
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
    this.selectedStatus.forEach(function (item) {
      sids.push(item.statusId);
    });
    if (sids.length == 0)
      sids = [1, 2, 3, 4, 5, 8];
    this.pipe = new DatePipe('en-US');
    this.startDate1 = this.pipe.transform(this.startDate, 'MM/dd/yyyy');
    this.endDate1 = this.pipe.transform(this.endDate, 'MM/dd/yyyy');
    this.fromDate1 = this.pipe.transform(this.fromDate, 'MM/dd/yyyy');
    this.toDate1 = this.pipe.transform(this.toDate, 'MM/dd/yyyy');
    this.Loader.show();
    this.httpService.get(this.httpService.apiRoutes.SpGetActiveJobList.SpGetPendingJobList + "?pageNumber=" + this.pageNumber + "&pageSize=" + this.data.pageSize + "&dataOrderBy=" + this.dataOrderBy
      + "&customerName=" + customerpra + "&jobId=" + this.jobId + "&startDate=" + this.startDate1
      + "&endDate=" + this.endDate1 + "&city=" + cityIds + "&status=" + sids + "&jobdetailid=" + this.jobdetailid + "&fromDate=" + this.fromDate1 + "&toDate=" + this.toDate + "&tradesmanName=" + tradesmanpra
      + "&usertype=" + this.usertype + "&location=" + this.location + "&athorize=" + true + "&cSJobStatusId=" + csIds + "&townId=" + townIds + "&town=" + this.town).subscribe(result => {
        this.getActiveJobList = null;
        if (result.json() != null) {
          this.getActiveJobList = result.json();
          debugger
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

          this.cloneList = this.getActiveJobList;
                         

          this.excelFileList = this.getActiveJobList;
          this.noOfPages = this.getActiveJobList[0].noOfRecoards / this.data.pageSize

          this.noOfPages = Math.floor(this.noOfPages);
          if (this.getActiveJobList[0].noOfRecoards > this.noOfPages) {
            this.noOfPages = this.noOfPages + 1;
          }
          this.totalRecoards = this.getActiveJobList[0].noOfRecoards;
          this.pageing1 = [];
          for (var x = 1; x <= this.noOfPages; x++) {
            this.pageing1.push(
              x
            );
          }
          this.recoardNoFrom = (this.data.pageSize * this.pageNumber) - this.data.pageSize + 1;
          this.recoardNoTo = (this.data.pageSize * this.pageNumber);
          if (this.recoardNoTo > this.getActiveJobList[0].noOfRecoards)
            this.recoardNoTo = this.getActiveJobList[0].noOfRecoards;
          this.dataNotFound = true;
        }
        else {

          this.recoardNoFrom = 0;
          this.recoardNoTo = 0;
          this.noOfPages = 0;
          this.totalRecoards = 0;
          this.getActiveJobList = [];
          this.cloneList = [];
          this.toastr.error("No record found !", "Search");
          this.dataNotFound = false;
        }
        this.Loader.hide();
      },
        error => {
          alert(error);
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
      ;
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
      console.log(this.businessProfile);
      if (this.businessProfile.skills != null) {
        this.businessProfile.skills = this.businessProfile.skills.split("&amp;").join(" ");
      }
      this._modalService.open(content);
      this.Loader.hide();
    })
  }
  populateData() {
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

  JobDetailsFunction(customerId, jobStatus, jobDetailsContent) {
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));

    this.jobStatusByName = jobStatus;
    this.activitytab = "2";

    this.Loader.show();
    this.httpService.get(this.httpService.apiRoutes.Jobs.GetJobDetailsList + "?pageNumber=" + this.pageNumber + "&pageSize=" + this.pageSize + "&customerId=" + customerId).subscribe(result => {
      this.jobDetails = null;
      this.lastdate = null;
      this.jobDetails = result.json();
      debugger
      let obj = {
        estimatedCommission: this.jobDetails.estimatedCommission,
        otherCharges: this.jobDetails.serviceCharges,
        agreedBudget: (this.jobDetails.tradesmanBudget as any) - (this.jobDetails.additionalCharges as any),
        jobQuotationId: this.jobDetails.jobQuotationId,
        totalJobValue: this.jobDetails.totalJobValue? this.jobDetails.totalJobValue : this.jobDetails.workBudget,
        materialCharges: this.jobDetails.materialCharges,
        additionalCharges: this.jobDetails.additionalCharges,
        chargesDescription: this.jobDetails.chargesDescription,
        csJobStatus: this.jobDetails.csjqJobStatusId,
        //csJobStatus: this.jobDetails.csJobStatusId == 0 ? this.jobDetails.csjqJobStatusId : this.jobDetails.csJobStatusId,
      }
      this.jobStatusName = this.cloneList.filter(x => x.customerId == this.jobDetails.jobQuotationId).map(x => x.statusId)[0];
      this.csjqJobStatusId = obj.csJobStatus;
      this.appValForm.patchValue(obj);
      if (this.jobDetails.notificationDTO == null) {
        this.nodata = "Data Not Exist";
      }
      else if (this.jobDetails.notificationDTO.length < 1) {
        this.nodata = "Data Not Exist";
      }
      else {
        this.nodata = "";
      }
      debugger
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
      let modelRef = this._modalService.open(jobDetailsContent, { windowClass: 'my-class', backdrop: 'static', keyboard: false });
      modelRef.result.then(() => {
        this.appValForm.reset();
        this.submittedApplicationForm = false;
        this.GetActiveJobList();
      });
      this.Loader.hide();
    },
      error => {
        this.Loader.hide();
        alert(error)
      }
    );

  }
  GetJobQoutation(jobQoutationId) {

    this.httpService.NavigateToRouteWithQueryString(this.httpService.apiRoutes.Jobs.getqoutationId, { queryParams: { id: jobQoutationId } });
  }
  DeleteJobByJobQuotationId(jobQoutationId) {

    this.quotationId = jobQoutationId;
    this.postJobModal.show();
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
    ;
    if (this.customerjobspageNumber > 1) {
      this.customerjobspageNumber = parseInt(this.customerjobspageNumber.toString()) - 1;
      this.GetCustomerJobsList();
    }
  }
  customerNextClick() {
    if (this.customerjobspageNumber < this.noOfPagesCustomerJobs) {
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
  hidemodal() {

    this.postJobModal.hide();
    this.JobDetailsModal.hide();
    this.jobDetailsContent.hide();
    this.peopleByCountry = [];
    this.activitytab = "1";
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
  deletejobconfirm() {

    if ((this.totalRecoards % this.data.pageSize) == 1 && this.pageNumber == this.noOfPages) {
      if (this.pageNumber > 1)
        this.pageNumber = parseInt(this.pageNumber.toString()) - 1;
    }

    this.httpService.get(this.httpService.apiRoutes.Jobs.deleteJobWithJobQuotationId + "?jobQuotationId=" + this.quotationId + "&actionPageName=" + this.actionPageName).subscribe(result => {
      this.responseVm = result.json();
      if (this.responseVm.status = httpStatus.Ok) {
        this.GetActiveJobList();
        this.quotationId = "";
        this.postJobModal.hide();
        this.toastr.error(this.responseVm.message, "Delete");
      }
    });
  }
  // Customer Model //
  GetBussinessProfile(CustomerId, contentCustomer) {

    this._modalService.open(contentCustomer, { windowClass: 'my-class' });
    this.httpService.get(this.httpService.apiRoutes.Users.getBussinessProfile + "?userId=" + CustomerId + "&role=" + 'Customer').subscribe(result => {
      this.businessProfile = null;
      this.businessProfile = result.json();
      console.log(this.businessProfile);

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
  public getCsJobStatus() {

    this.httpService.get(this.httpService.apiRoutes.Jobs.GetCsJobStatusDropdown).subscribe(result => {
      this.csJobStatusDropdown = result.json();
    })
  }
  public gettownList() {

    this.httpService.get(this.httpService.apiRoutes.Common.GetAllTown).subscribe(result => {
      this.townDropdown = result.json();

      console.log(this.townDropdown);
    })
  }
  onCSSelectAll(item: any) {
    this.selectedCsStatus = item;
  }
  onCSDeSelectALL(item: any) {
    this.selectedCsStatus = [];
  }
  onTownDeSelectALL(item: any) {
    this.selectedTown = [];
  }
  onCSSelect(item: any) {
    this.selectedCsStatus.push(item);
  }
  onTownSelect(item: any) {
    this.selectedTown.push(item);
  }
  onCSDeSelect(item: any) {

    this.selectedCsStatus = this.selectedCsStatus.filter(
      function (value, index, arr) {
        return value.id != item.id;
      });
  }

  onTownDeSelect(item: any) {

    this.selectedTown = this.selectedTown.filter(
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
  get f() {
    return this.appValForm.controls;
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
      title: 'PostedjobsReport',
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
      this.exportAsExcelFile(this.excelFileList, "PostedjobsReport")
    else
      this.exportAsExcelFile(this.selecteddatasingle, "PostedjobsReport")
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
    doc.save('Customer.pdf')


  }
  uncheckAll() {
    this.checkboxes.forEach((element) => {
      element.nativeElement.checked = false;
    });
  }
  onCheckboxChange(e, obj) {
    console.log(obj);
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
    } else {
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
    this.selectedTowns = [];
    this.jobdetailid = "";
    this.fromDate = null;
    this.toDate = null;
    this.tradesmanName = "";
    this.selecteddatasingle = [];
    this.usertype = 3;
    this.restChecked.nativeElement.checked = true;
    this.restTradesManChecked.nativeElement.checked = true;
    this.restUserType.nativeElement.checked = true;
  }
public CustomerJobsLoadNewestRecoards(){

}
public CustomerJobsLoadOldestRecoards(){

  }


  public exportAsExcelFile(json: any[], excelFileName: string): void {
    let jobList: any = [];
    json.map(data => {
      var obj = {
        JobId: data.customerId,
        TradesmanId: data.tradesmanId,
        CustomerStatus: data.customerStatus,
        CustomerId: data.jobId,
        JobDetailId: data.jobDetailId,
        StatusId: data.statusId,
        StreetAddress: data.streetAddress,
        JobTitle: data.jobTitle,
        NoOfRecoards: data.noOfRecoards,
        RecordNo: data.recordNo,
        CsJobStatusName: data.csJobStatusName,
        City: data.city,
        CustomerName: data.customerName,
        Town: data.town,
        MobileNumber: data.mobileNumber,
        SkillName: data.skillName,
        SubSkillName: data.subSkillName,
        TradesmanName: data.tradesmanName,
        WorkBudget: data.workBudget,
        VisitCharges: data.sitCharges,
        Quantity: data.quantity,
        CustomerJobs: data.customerJobs,
        ServiceCharges: data.serviceCharges,
        OtherCharges: data.otherCharges,
        AdditionalCharges: data.additionalCharges,
        AgreedAmount: data.greedAmount,
        TotalJobAmount: data.totalJobAmount,
        EstimatedCommission: data.estimatedCommission,
        RecivedBids: data.recivedBids,
        CreatedOn: data.createdOn,
        WorkStartDate: data.workStartDate,
        IsTestUser: data.isTestUser,
        IsAuthorize: data.isAuthorize,
        Area: data.area,
        Isselectedforexport: data.isselectedforexport
      }
      jobList.push(obj)
    })


    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(jobList);
    console.log('worksheet', worksheet);
    const workbook: XLSX.WorkBook = { Sheets: { 'data': worksheet }, SheetNames: ['data'] };
    const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
    //const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'buffer' });
    this.excelService.saveAsExcelFile(excelBuffer, excelFileName);
  }

}

