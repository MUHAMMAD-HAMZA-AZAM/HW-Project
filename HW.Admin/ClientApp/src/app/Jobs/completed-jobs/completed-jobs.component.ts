import { Component, ViewChild, ElementRef } from '@angular/core';
import { CommonService } from 'src/app/Shared/HttpClient/_http';
import { AllJobsCountVm, GetAllaJobsCount } from 'src/app/Shared/Models/JobModel/JobModel';
import { NgxSpinnerService } from "ngx-spinner";
import { SortList } from 'src/app/Shared/Sorting/sortList';
import { ModalDirective } from 'ngx-bootstrap/modal/modal.directive';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { JobDetails } from 'src/app/Shared/Models/JobModel/JobModel';
import { DatePipe } from '@angular/common';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-completed-jobs',
  templateUrl: './completed-jobs.component.html',
  styleUrls: ['./completed-jobs.component.css']
})
export class CompletedJobsComponent {

  public data: GetAllaJobsCount = new GetAllaJobsCount();
  public completedJobsList: AllJobsCountVm[] = [];
  public cloneList: AllJobsCountVm[] = [];
  public pageing1 = [];

  public customerSearch: number = 1;
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
  public totalRecoards = 101;
  public noOfPages;
  public dataOrderBy = "DESC";
  public quotationId;
  public deleterights;
  public allowview; 
  public pagercoards;
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

  public searchBy: 1;
  public startDate: Date;
  public endDate: Date;
  public startDate1: Date;
  public endDate1: Date;
  public fromDate: Date;
  public toDate: Date;
  public fromDate1: Date;
  public toDate1: Date;
  public location = "";
  public customerName = "";
  public jobId = "";
  public jobDetailId = "";
  public cityList = [];
  public selectedCities = [];
  public selectedCity = [];
  public selectedColumn = [];
  public dropdownListForCity = {};
  public dropdownListForColumn = {};
  public dataNotFound: boolean;
  public searchedAddress = [];
  public isAddress = false;


  public startDateErrorMessage: string;
  public endDateErrorMessage: string;
  public submittedApplicationForm = false;
  public activitytab = "1";
  public businessProfile: any;



  @ViewChild('postJobModalConfirm', { static: true }) postJobModal: ModalDirective;
  @ViewChild('JobsDetailsModal', { static: true }) JobDetailsModal: ModalDirective;
  @ViewChild('restChecked', { static: true }) restChecked: ElementRef;
  constructor(public _modalService: NgbModal,public toastr: ToastrService , public httpService: CommonService, public Loader: NgxSpinnerService, public sortList: SortList, private router: Router) {
    
    this.pagercoards = 50;
    this.populateData();
    this.populateCompletedJobList();
  }
  ngOnInit() {
    this.allowview = localStorage.getItem("CompletedView");
    if (this.allowview != 'true')
      this.router.navigateByUrl('/login');

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
    this.getAllCities();

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


  setAddressValue(e) {
    this.location = e.target.text;
    this.isAddress = false;
  }
  getRadioValue(e) {
    this.searchBy = e.target.value;
  }
  resetForm() {
    
    this.location = "";
    this.endDate = null;
    this.startDate = null;
    this.customerName = "";
    this.selectedCities = []
    this.selectedCity = [];
    this.jobId = "";
    this.fromDate = null;
    this.toDate = null;
    this.jobDetailId = "";
    this.restChecked.nativeElement.checked = true
  }

  populateCompletedJobList() {
    let cityIds = [];
    this.searchBy = 1;
    
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
    this.pipe = new DatePipe('en-US');
    this.startDate1 = this.pipe.transform(this.startDate, 'MM/dd/yyyy');
    this.endDate1 = this.pipe.transform(this.endDate, 'MM/dd/yyyy');
    this.toDate1 = this.pipe.transform(this.toDate, 'MM/dd/yyyy');
    this.fromDate1 = this.pipe.transform(this.fromDate, 'MM/dd/yyyy');
    this.Loader.show();
    this.httpService.get(this.httpService.apiRoutes.Jobs.CompletedJobListAdmin + '?pageNumber=' + this.pageNumber + '&pageSize=' + this.data.pageSize + "&dataOrderBy=" + this.dataOrderBy
      + "&customerName=" + customerpra + "&jobId=" + this.jobId + "&jobDetailId="+ this.jobDetailId + "&startDate=" + this.startDate1
      + "&endDate=" + this.endDate1 + "&city=" + cityIds + "&location=" + this.location + "&fromDate=" + this.fromDate1 + "&toDate=" + this.toDate).subscribe(result => {
        if (result.json() != null) {
          this.completedJobsList = result.json();
          this.cloneList = this.completedJobsList;
          this.noOfPages = this.completedJobsList[0].noOfRecoards / this.data.pageSize

          this.noOfPages = Math.floor(this.noOfPages);
          if (this.completedJobsList[0].noOfRecoards > this.noOfPages) {
            this.noOfPages = this.noOfPages + 1;
          }
          this.totalRecoards = this.completedJobsList[this.completedJobsList.length - 1].noOfRecoards;
          
          this.pageing1 = [];
          for (var x = 1; x <= this.noOfPages; x++) {
            this.pageing1.push(
              x
            );
          }
          this.recoardNoFrom = (this.data.pageSize * this.pageNumber) - this.data.pageSize + 1;
          this.recoardNoTo = (this.data.pageSize * this.pageNumber);
          if (this.recoardNoTo > this.completedJobsList[0].noOfRecoards)
            this.recoardNoTo = this.completedJobsList[0].noOfRecoards;
          this.dataNotFound = true;
          this.JobDetailsFunction(this.completedJobsList[0].jobQuotationId, 'completed');
        }
        else {         
          this.recoardNoFrom = 0;
          this.recoardNoTo = 0;
          this.noOfPages = 0;
          this.totalRecoards = 0;
          this.completedJobsList = [];
          this.cloneList = [];
          this.toastr.error("No record found !", "Search");
          this.dataNotFound = false;
          this.Loader.hide();       
        }
      
      this.Loader.hide();
    },
      error => {
        alert(error)
      }
    );
    this.Loader.hide();
  }
  populateData() {
    this.data.pageSize = this.pagercoards;
    this.Loader.show();
    //Get Job Count
    this.httpService.get(this.httpService.apiRoutes.Jobs.GetAllJobsCount).subscribe(result => {
      this.data = result.json();
      
      this.data.pageSize = this.pagercoards;
      this.totalJobs = this.data.jobsCompleted;
      this.NumberOfPages();
      this.Loader.hide();

    },
      error => {
        alert(error)
      }
    );
  }
  GetTotalPageList() {
    var array = new Array();
    for (var i = 1; i <= this.totalPages; i++) {
      array.push(i);
    }
    return array;
  }
  SelectedPageData(page) {
    this.status = true;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = false;
    this.pageNumber = page;
    this.populateCompletedJobList();
  }
  NumberOfPages() {
    this.totalPages = Math.ceil(this.totalJobs / this.data.pageSize);
  }
  changePageSize() {
    this.pageNumber = 1;
    this.NumberOfPages();
    this.populateCompletedJobList();
  }
  lastPage() {
    this.status = false;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = true;
    this.pageNumber = this.totalPages;
    this.populateCompletedJobList();
  }
  FirstPage() {
    this.status = false;
    this.firstPageactive = true;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = false;
    this.pageNumber = 1;
    this.populateCompletedJobList();
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
    this.populateCompletedJobList();
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
    this.populateCompletedJobList();
  }

  JobDetails(jobDetailId, comments, overallRating, title) {
    localStorage.setItem("cmjobpageno", JSON.stringify(this.pageNumber));
    localStorage.setItem("cmjobpagesize", JSON.stringify(this.data.pageSize));
    localStorage.setItem("cmjoborderby", this.dataOrderBy);
    
    this.httpService.NavigateToRouteWithQueryString(this.httpService.apiRoutes.Jobs.jobdetails, { queryParams: { id: jobDetailId, comment: comments, overallRat: overallRating, activity: title } });
  }

  Filter(value: any) {
    
    if (!value) {
      this.cloneList = this.completedJobsList;
    }
    else {
      this.cloneList = this.completedJobsList.filter(x => x.jobDetailId.toString().includes(value) || x.MobileNumber.includes(value));
    }
  }
  DeleteJobByJobQuotationId(jobQoutationId) {
    this.quotationId = jobQoutationId;
    this.postJobModal.show();
  }
  GetReCoards() {
  }
  clickchange() {
    this.populateCompletedJobList();
  }
  PageSizeChange() {
    this.pageNumber = 1;
    this.populateCompletedJobList();
  }
  PriviousClick() {
    if (this.pageNumber > 1) {
      this.pageNumber = parseInt(this.pageNumber.toString()) - 1;
      this.populateCompletedJobList();
    }

  }
  NextClick() {
    
    if (this.pageNumber < this.noOfPages) {
      this.pageNumber = parseInt(this.pageNumber.toString()) + 1;
      this.populateCompletedJobList();
    }

  }
  LoadNewestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "DESC";
    this.populateCompletedJobList();

  }
  LoadOldestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "ASC";
    this.populateCompletedJobList();

  }
  hidemodal() {
    this.postJobModal.hide();
    this.JobDetailsModal.hide();
    this.quotationId = "";
    this.peopleByCountry = [];
    this.activitytab = "1";
  }
  deletejobconfirm() {
    if ((this.totalRecoards % this.data.pageSize) == 1 && this.pageNumber == this.noOfPages) {
      if (this.pageNumber > 1)
        this.pageNumber = parseInt(this.pageNumber.toString()) - 1;
    }
    
    this.httpService.get(this.httpService.apiRoutes.Jobs.deleteJobWithJobQuotationId + "?jobQuotationId=" + this.quotationId).subscribe(result => {
      if (result.status = 200) {
        this.populateCompletedJobList();
        this.quotationId = "";
        this.postJobModal.hide();
        this.toastr.error("Record deleted successfully", "Delete");
      }
      this.populateCompletedJobList();
      this.quotationId = "";
      this.postJobModal.hide();
    });
  }
  JobDetailsFunction(customerId, activeJob) {
    this.Loader.show();
    
    this.httpService.get(this.httpService.apiRoutes.Jobs.GetJobDetailsList + "?pageNumber=" + this.pageNumber + "&pageSize=" + this.pageSize + "&customerId=" + customerId).subscribe(result => {
      
      this.jobDetails = null;
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
          ;
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
      console.log(this.peopleByCountry1);
      console.log(this.peopleByCountry);
      this.Loader.hide();
    },
      error => {
        this.Loader.hide();
        alert(error)
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
 
}
