import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { aspNetUserRoles, loginsecurity } from '../../Shared/Enums/enums';
import { CommonService } from '../../Shared/HttpClient/_http';
import { AdminDashboardVm } from '../../Shared/Models/HomeModel/HomeModel';
import { GetAllaJobsCount } from '../../Shared/Models/JobModel/JobModel';

@Component({
  selector: 'app-statistical-analysis',
  templateUrl: './statistical-analysis.component.html',
  styleUrls: ['./statistical-analysis.component.css']
})
export class StatisticalAnalysisComponent implements OnInit {
  public pageSize = 50;
  public pageNumber = 1;
  public noOfPages;
  public dataOrderBy = 'DESC';
  public totalPages: number;
  public status: boolean = true;
  public firstPageactive: boolean;
  public lastPageactive: boolean;
  public previousPageactive: boolean;
  public nextPageactive: boolean;
  public totalRecoards;
  public recoardNoFrom = 1;
  public recoardNoTo = 50;
  public pageing1 = [];
  public totalJobs: number;
  public appValForm: FormGroup;
  public statisticalAnalysisDataList = [];
  public userRoleName;
  public userRoleId: any;
  public roleId;
  public hiddenData: boolean = false;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  public adminDashboardVm: AdminDashboardVm = new AdminDashboardVm();
  public getAllaJobsCount: GetAllaJobsCount = new GetAllaJobsCount();

  constructor(public Loader: NgxSpinnerService,
    public common: CommonService,
    public router: Router,
    public toaster: ToastrService,
    private route: ActivatedRoute,
    public fb: FormBuilder
  ) { }

  ngOnInit() {
   
    this.userRoleId = this.route.paramMap.subscribe(params => {
      this.userRoleId = params.get('id');
      this.roleId = this.userRoleId;
      if (aspNetUserRoles.CRole == this.roleId) {
        this.userRole = JSON.parse(localStorage.getItem("Customer Statistical Analysis"));
        if (!this.userRole.allowView)
          this.router.navigateByUrl('/login');
      }
      else if (aspNetUserRoles.TRole == this.roleId) {
        this.userRole = JSON.parse(localStorage.getItem("Tradesman Statistical Analysis"));
        if (!this.userRole.allowView)
          this.router.navigateByUrl('/login');
      }
      else if (aspNetUserRoles.ORole == this.roleId) {
        this.userRole = JSON.parse(localStorage.getItem("Organization Statistical Analysis"));
        if (!this.userRole.allowView)
          this.router.navigateByUrl('/login');
      }
      else {
        this.userRole = JSON.parse(localStorage.getItem("Supplier Statistical Analysis"));
        if (!this.userRole.allowView)
          this.router.navigateByUrl('/login');
      }
    
      this.showUserRoleName(this.roleId);
      // Filter Analysis Data
      this.appValForm = this.fb.group({
        name: [''],
        userId: [0],
        fromDate: [''],
        toDate: [''],
      });
     this.showStatisticalAnalysisData();
    });
  }



  // Statistical Analysis List
  public showStatisticalAnalysisData() {
    debugger;
    this.Loader.show();
    let formData = this.appValForm.value;
    //formData.userId = formData.userId == '' ? formData.userId = 0 : formData.userId;
    formData.userRole =this.roleId;
    formData.pageNumber = this.pageNumber;
    formData.pageSize = this.pageSize;
    console.log(formData);
    this.Loader.show();
    
    this.common.PostData(this.common.apiRoutes.Analytics.UserAnalytics, formData, true).then(result => {
      this.statisticalAnalysisDataList = result.json();
      if (!this.statisticalAnalysisDataList) {
        this.toaster.error("No data found", "Error");
        this.hiddenData = true;
      }
      else {

        this.noOfPages = this.statisticalAnalysisDataList[0].noOfRecoards / this.pageSize;

        this.noOfPages = Math.floor(this.noOfPages);
        if (this.statisticalAnalysisDataList[0].noOfRecoards > this.noOfPages) {
          this.noOfPages = this.noOfPages + 1;
        }
        this.totalRecoards = this.statisticalAnalysisDataList[0].noOfRecoards;
        this.pageing1 = [];
        for (var x = 1; x <= this.noOfPages; x++) {
          this.pageing1.push(x);
        }
        this.recoardNoFrom = (this.pageSize * this.pageNumber) - this.pageSize + 1;
        this.recoardNoTo = (this.pageSize * this.pageNumber);
        if (this.recoardNoTo > this.statisticalAnalysisDataList[0].noOfRecoards)
          this.recoardNoTo = this.statisticalAnalysisDataList[0].noOfRecoards;
      }

      this.Loader.hide();
    }, error => {
      this.Loader.show();
      console.log(error);
    });


  }

  //pagination start
  SelectedPageData(page) {
    this.pageNumber = page;
    this.status = true;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = false;
    this.showStatisticalAnalysisData();
  }
  NumberOfPages() {

    this.totalPages = Math.ceil(this.statisticalAnalysisDataList[0].noOfRecoards / this.pageSize);
  }
  changePageSize() {
    this.pageNumber = 1;
    this.NumberOfPages();
    this.showStatisticalAnalysisData();
  }
  lastPage() {
    this.status = false;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = true;
    this.pageNumber = this.totalPages;
    this.showStatisticalAnalysisData();

  }
  FirstPage() {
    this.status = false;
    this.firstPageactive = true;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = false;
    this.pageNumber = 1;
    this.showStatisticalAnalysisData();
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
    this.showStatisticalAnalysisData();
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
    this.showStatisticalAnalysisData();
  }
  clickchange() {
    this.showStatisticalAnalysisData();
  }
  PageSizeChange() {

    this.pageNumber = 1;
    this.showStatisticalAnalysisData();
  }
  PriviousClick() {
    if (this.pageNumber > 1) {
      this.pageNumber = parseInt(this.pageNumber.toString()) - 1;
      this.showStatisticalAnalysisData();
    }

  }
  NextClick() {
    if (this.pageNumber < this.noOfPages) {
      this.pageNumber = parseInt(this.pageNumber.toString()) + 1;
      this.showStatisticalAnalysisData();
    }

  }
  LoadNewestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "DESC";
    this.showStatisticalAnalysisData();

  }
  LoadOldestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "ASC";
    this.showStatisticalAnalysisData();

  }
  //pagination end

  public resetFrom() {
    this.pageSize = 50;
    this.pageNumber = 1;
    this.hiddenData = false;
    this.appValForm.reset();
    this.appValForm.controls.userId.setValue(0);
    this.showStatisticalAnalysisData();
  }


  // Show User Role Name

  public showUserRoleName(roleId) {
    if (this.roleId == aspNetUserRoles.CRole) {
      this.userRoleName = loginsecurity.CRole
    }
    else if (this.roleId == aspNetUserRoles.TRole) {
      this.userRoleName = loginsecurity.TRole;
    }
    else if (this.roleId == aspNetUserRoles.ORole) {
      this.userRoleName = loginsecurity.ORole;
    }
    else {
      this.userRoleName = loginsecurity.SRole;
    }
  }

}
