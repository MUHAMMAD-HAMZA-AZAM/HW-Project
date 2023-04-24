import { Component, ViewChild, ElementRef, OnInit, AfterViewInit, asNativeElements, HostListener } from '@angular/core';
import { SessiontimeoutService } from '../../CommonServices/sessiontimeout.service';
import { Observable, Subject, Subscription, timer } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { NgxSpinnerService } from "ngx-spinner";
import { EventsService } from '../../events.service';
import { apiUrls } from '../../ApiRoutes/ApiRoutes';
import { CommonService } from '../../HttpClient/_http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LoginVM } from '../../Models/HomeModel/HomeModel';
import { httpStatus, loginsecurity } from '../../Enums/enums';

@Component({
  selector: 'app-leftmenu',
  templateUrl: './app-leftmenu.component.html',
  styleUrls: ['./app-leftmenu.component.css']
})
export class AppLeftMenuComponet implements AfterViewInit {

  @ViewChild('accordionSidebar') accordionSidebar: ElementRef;
  
 // public securityrolelist = [];
  public home = false;
  public lmPosts = false;
  public lmJobs = false;
  public lmReports = false;
  public lmUserView = false;
  public lmElmahView = false;

  public activeJobs = false;
  public pendingJobs = false;
  public inProgress = false;
  public completed = false;
  public approveJob = false;
  public primaryUser = false;
  public tradesman = false;
  public tradesmanList = false;
  public supplier = false;
  public supplierList = false;
  public postedAds = false;
  public dailyReports = false;
  public weeklyRports = false;
  public twoWeekReports = false;
  public monthlyReports = false;
  public inActiveUser = false;
  public salesman = false;
  public securityRoles = false;
  public createAdminUser = false;
  public spectialright = false;
  public menuAndSubMenuItem = [];

  private _count = 0;
  private _serviceId: string = 'idleTimeoutSvc-' + Math.floor(Math.random() * 10000);
  private _timeoutMilliseconds = 5400000;
  private timerSubscription: Subscription;
  private roleChanges: Subscription;
  loginVm: LoginVM = new LoginVM();
  jwtHelperService: JwtHelperService = new JwtHelperService();
  private timer: Observable<number>;
  private _timer: Observable<number>;
  private resetOnTrigger: Boolean = false;
  private lastTime: number;
  private dateTimer: Observable<number>;
  private dateTimerSubscription: Subscription;
  private dateTimerInterval: number = 1000 * 60 * 5;
  private dateTimerTolerance: number = 1000 * 10;
  public timeoutExpired: Subject<number> = new Subject<number>();
  public id: string;
  public isToggled = false;
  public splittedUrl = [];
  public url = "";
  public selectedIndex: number = null;
  public urlList = ["app-manual-reports-customer",
    "app-manual-reports-tradesman",
    "app-manual-reports-supplier",
    "app-ManualPostedJob-report",
    "app-ManualBids-report",
    "app-ManualPostedAds-report",
    "app-customer-report", "app-tradesman-report", "app-supplier-report", "app-PostedJob-report", "app-ActiveBids-report", "app-PostedAdsDaily-report"
  ]
  constructor(private event: EventsService, public httpService: CommonService, private router: Router, public sesstimout: SessiontimeoutService, public Loader: NgxSpinnerService,) {
    this.roleChanges = this.event.RoleChanges.subscribe(res => {
      let isChange = localStorage.getItem("roleChanges");
      if (isChange != null) {
      }
        localStorage.removeItem("roleChanges");
    });
    this.timeoutExpired.subscribe(n => {
    });


    this.startTimer();
    this.startDateCompare();
  }
  ngOnDestroy() {
    if (this.roleChanges) {
      this.roleChanges.unsubscribe();
    }
  }
  ngOnInit() {
    //this.getSecurityRoles();
    this.makeData();
  }
  setIndex(index: number) {
    this.selectedIndex = index;
  }
  makeData() {
    var srl = JSON.parse(localStorage.getItem("SecurityRole"));
    let dataArray = [];
    for (var i in srl) {
      let lsObj = {       
        allowView: srl[i].allowView,
        allowAdd: srl[i].allowPrint,
        allowEdit: srl[i].allowEdit,
        allowExport: srl[i].allowExport,
        allowDelete: srl[i].allowDelete
      }
      localStorage.setItem(srl[i].securityRoleItemName, JSON.stringify(lsObj));
      let subArray = []
      srl.filter(x => x.menuId == srl[i].menuId && !x.isModule).map(x => { //get all subitems by menu id
        let subObj = {
          securityRoleItemName: x.securityRoleItemName,
          allowDelete: x.allowDelete,
          allowEdit: x.allowEdit,
          allowExport: x.allowExport,
          allowPrint: x.allowPrint,
          allowView: x.allowView,
          spectialrights: x.spectialrights,
          routingPath: x.routingPath
        }
        subArray.push(subObj)
      })
      let objWithName = { pageName: "", allowDelete: false, allowEdit: false, allowExport: false, allowPrint: false, allowView: false, routingPath:"",iconName : "",subArray:[],};
      objWithName = {
        pageName: srl[i].securityRoleItemName,
        allowDelete: srl[i].allowDelete,
        allowEdit: srl[i].allowEdit,
        allowExport: srl[i].allowExport,
        allowPrint: srl[i].allowPrint,
        allowView: srl[i].allowView,
        routingPath: srl[i].routingPath,
        iconName : srl[i].iconName,
        subArray: subArray 
      }
      let data = objWithName.subArray.filter(x => x.securityRoleItemName == srl[i].securityRoleItemName);
      if (data.length <= 0) {
        if (srl[i].isModule && subArray.length >= 1) {
          objWithName.subArray = subArray;
          dataArray.push(objWithName) 
        }
        else {
          objWithName.subArray = [];
          dataArray.push(objWithName);
        }
        this.menuAndSubMenuItem = dataArray.filter(x => (x.pageName != "Settings") && (x.pageName != "Reports") && (x.pageName != "Packages And Promotion") && (x.pageName != "Financial"));
      }
    }
  }
  getSecurityRoles() {
    var securityrolelist = JSON.parse(localStorage.getItem("SecurityRole"));
    if (securityrolelist[0].spectialrights == true)
      localStorage.setItem("SpectialRights", 'true');
    //console.log(securityrolelist);
    for (var x = 0; x < securityrolelist.length; x++) {

      if (securityrolelist[x].securityRoleItemName == "Home") {
        if (securityrolelist[x].allowView == true) {
          this.home = true;
          localStorage.setItem("HomeView", 'true');
        }
      }
      else if (securityrolelist[x].securityRoleItemName == "Posts") {
        this.lmPosts = securityrolelist[x].allowView;
        if (this.lmPosts)
          localStorage.setItem("LmPostsView", 'true');
      }
      //-------------------------------------------------JOBS-------------------------------------------------//
      //-------------------------------------------------JOBS-------------------------------------------------//

      else if (securityrolelist[x].securityRoleItemName == "Jobs") {
        this.lmJobs = securityrolelist[x].allowView;
        if (this.lmJobs)
          localStorage.setItem("LmJobsView", 'true');
      }
      else if (securityrolelist[x].securityRoleItemName == "Posted Jobs") {
        if (securityrolelist[x].allowView == true) {
          this.pendingJobs = true;
          localStorage.setItem("PendingJobsView", 'true');

        }
        if (securityrolelist[x].allowDelete == true)
          localStorage.setItem("PendingJobsDelete", 'true');
      }
      else if (securityrolelist[x].securityRoleItemName == "Approve Job") {
        if (securityrolelist[x].allowView == true) {
          this.approveJob = true;
          localStorage.setItem("ApproveJobView", 'true');
        }
        if (securityrolelist[x].allowEdit == true)
          localStorage.setItem("AllowEdit", 'true');
        if (securityrolelist[x].allowDelete == true)
          localStorage.setItem("AllowDelete", 'true');
      }
      else if (securityrolelist[x].securityRoleItemName == "Job Assignment") {
        if (securityrolelist[x].allowEdit)
          localStorage.setItem("AssignJobAllowEdit", 'true');
        if (securityrolelist[x].allowView)
          localStorage.setItem("AssignJobAllowView", 'true');
      }
      //-------------------------------------------------CUSTOMER-------------------------------------------------//
      //-------------------------------------------------CUSTOMER-------------------------------------------------//
      else if (securityrolelist[x].securityRoleItemName == "Customer") {
        if (securityrolelist[x].allowView == true) {
          this.primaryUser = true;
          localStorage.setItem("PrimaryUserView", 'true');
        }
        if (securityrolelist[x].allowEdit) {
          localStorage.setItem("CustomerEdit", 'true');
        }
      }
      //-------------------------------------------------TRADESMAN-------------------------------------------------//
      //-------------------------------------------------TRADESMAN-------------------------------------------------//
      else if (securityrolelist[x].securityRoleItemName == "Tradesman") {
        if (securityrolelist[x].allowView == true) {
          this.tradesman = true;
          localStorage.setItem("TradesmanView", 'true');
        }
      }
      else if (securityrolelist[x].securityRoleItemName == "Tradesman List") {
        if (securityrolelist[x].allowView == true) {
          this.tradesmanList = true;
          localStorage.setItem("Tradesmanlist", 'true');
        }
        if (securityrolelist[x].allowEdit) {
          localStorage.setItem("TradesmanlistEdit", 'true');
        }
      }
      //-------------------------------------------------SUPPLIER-------------------------------------------------//
      //-------------------------------------------------SUPPLIER-------------------------------------------------//
      else if (securityrolelist[x].securityRoleItemName == "Supplier") {
        if (securityrolelist[x].allowView) {
          this.supplier = true;
          localStorage.setItem("SupplierView", 'true');
        }
      }
      else if (securityrolelist[x].securityRoleItemName == "Supplier List") {
        if (securityrolelist[x].allowView) {
          this.supplierList = true;
          localStorage.setItem("SupplierView", 'true');
        }
        if (securityrolelist[x].allowEdit) {
          localStorage.setItem("SupplierEdit", 'true');
        }
      }
      else if (securityrolelist[x].securityRoleItemName == "Posted Ads") {
        if (securityrolelist[x].allowView) {
          this.postedAds = true;
          localStorage.setItem("PostedAdsView", 'true');
        }
        if (securityrolelist[x].allowDelete) {
          localStorage.setItem("PostedAdsDelete", 'true');
        }
      }
      //-------------------------------------------------REPORTS-------------------------------------------------//
      //-------------------------------------------------REPORTS-------------------------------------------------//
      else if (securityrolelist[x].securityRoleItemName == "Report By Day(s)") {
        this.lmPosts = securityrolelist[x].allowView;
        if (this.lmReports)
          localStorage.setItem("LmReportsView", 'true');
      }
      else if (securityrolelist[x].securityRoleItemName == "Daily Reports") {
        if (securityrolelist[x].allowView == true) {
          this.dailyReports = true;
          localStorage.setItem("DailyReportsView", 'true');
        }
      }
      else if (securityrolelist[x].securityRoleItemName == "Weekly Rports") {
        if (securityrolelist[x].allowView == true) {
          this.weeklyRports = true;
          localStorage.setItem("WeeklyReportsView", 'true');
        }
      }
      else if (securityrolelist[x].securityRoleItemName == "Two Week Reports") {
        if (securityrolelist[x].allowView == true) {
          this.twoWeekReports = true;
          localStorage.setItem("TwoWeekReportView", 'true');
        }
      }
      else if (securityrolelist[x].securityRoleItemName == "Monthly Reports") {
        if (securityrolelist[x].allowView == true) {
          this.monthlyReports = true;
          localStorage.setItem("MonthlyReportsView", 'true');
        }
      }
      //-------------------------------------------------USER-------------------------------------------------//
      //-------------------------------------------------USER-------------------------------------------------//

      else if (securityrolelist[x].securityRoleItemName == "User") {
        this.lmUserView = securityrolelist[x].allowView;
        if (this.lmUserView)
          localStorage.setItem("LmUserView", 'true');
      }
      else if (securityrolelist[x].securityRoleItemName == "InActive User") {
        if (securityrolelist[x].allowView == true) {
          this.inActiveUser = true;
          localStorage.setItem("InActiveUserView", 'true');
        }
      }
      else if (securityrolelist[x].securityRoleItemName == "Security Roles") {
        if (securityrolelist[x].allowView == true) {
          this.securityRoles = true;
          localStorage.setItem("SecurityRolesView", 'true');
        }
        if (securityrolelist[x].allowDelete == true)
          localStorage.setItem("SecurityRolesDelete", 'true');
        if (securityrolelist[x].allowPrint == true)
          localStorage.setItem("SecurityRolesAdd", 'true');
        if (securityrolelist[x].allowEdit == true)
          localStorage.setItem("SecurityRolesEdit", 'true');
      }
      else if (securityrolelist[x].securityRoleItemName == "Create Admin User") {
        if (securityrolelist[x].allowView == true) {
          this.createAdminUser = true;
          localStorage.setItem("CreateAdminView", 'true');

        }
        if (securityrolelist[x].allowDelete == true)
          localStorage.setItem("AdminUserDelete", 'true');
        if (securityrolelist[x].allowPrint == true)
          localStorage.setItem("AdminUserAdd", 'true');
        if (securityrolelist[x].allowEdit == true)
          localStorage.setItem("AdminUserEdit", 'true');
      }
      else if (securityrolelist[x].securityRoleItemName == "Salesman") {
        if (securityrolelist[x].allowView) {
          this.salesman = true;
          localStorage.setItem("SalesmanView", 'true');
        }
      }
      //-------------------------------------------------ELMAH-------------------------------------------------//
      //-------------------------------------------------EMLAH-------------------------------------------------//
      else if (securityrolelist[x].securityRoleItemName == "Elmah") {
        this.lmElmahView = securityrolelist[x].allowView;
        if (this.lmElmahView)
          localStorage.setItem("LmElmahView", 'true');
      }

    }

  }
  ngAfterViewInit()
  {
    this.splittedUrl = (location.pathname).split('/').join(',').split(',');
    this.url = this.splittedUrl[2];
    console.log(this.splittedUrl);
    //for (var i = 0; i < this.urlList.length; i++) {
    //  if (this.urlList[i].includes(this.url)) {
    //    this.accordionSidebar.nativeElement.classList.add('toggled');
    //    break;
    //  }
    //}
  }
  toggle() {

    this.isToggled = !this.isToggled;

    if (this.isToggled) {
      this.accordionSidebar.nativeElement.classList.add('toggled');
    }
    else {
      this.accordionSidebar.nativeElement.classList.remove('toggled');
    }
  }
  private setSubscription() {
    this._timer = timer(this._timeoutMilliseconds);
    this.timerSubscription = this._timer.subscribe(n => {
      this.timerComplete(n);
    });
  }


  private startDateCompare() {
    this.lastTime = (new Date()).getTime();
    this.dateTimer = timer(this.dateTimerInterval); // compare every five minutes
    this.dateTimerSubscription = this.dateTimer.subscribe(n => {
      const currentTime: number = (new Date()).getTime();
      if (currentTime > (this.lastTime + this.dateTimerInterval + this.dateTimerTolerance)) { // look for 10 sec diff
       console.log('Looks like the machine just woke up.. ');
      } else {
      console.log('Machine did not sleep.. ');
      }
      this.dateTimerSubscription.unsubscribe();
      this.startDateCompare();
    });
  }


  public startTimer() {
    if (this.timerSubscription) {
      this.stopTimer();
    }


    this.setSubscription();
  }


  public stopTimer() {
    this.timerSubscription.unsubscribe();
  }


  public resetTimer() {
    this.setSubscription();
    this.startTimer();
  }
 

  private timerComplete(n: number) {
    this.timeoutExpired.next(++this._count);


    if (this.resetOnTrigger) {
      this.startTimer();
    }
    else
      //this.router.navigateByUrl('/login');
      window.location.href = window.location.origin + '/login'
  }

}
