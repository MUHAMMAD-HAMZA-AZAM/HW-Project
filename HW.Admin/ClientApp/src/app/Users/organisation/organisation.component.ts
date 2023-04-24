import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { CommonService } from 'src/app/Shared/HttpClient/_http';
import { SpTradesmanStatsVM, SpTradesmanListVM, ITownVM,TradesmanPayments } from 'src/app/Shared/Models/UserModel/SpTradesmanVM';
import { NgxSpinnerService } from "ngx-spinner";
import { SortList } from 'src/app/Shared/Sorting/sortList';
import { SpUserProfileVM, SpBusinessProfileVM, tradesmanProfile, PersonalDetailsUpdatetrd, BusinessDetailsupdatetrd, IAnalyticsModal } from 'src/app/Shared/Models/PrimaryUserModel/PrimaryUserModel';
import { ModalDirective } from 'ngx-bootstrap/modal/modal.directive';
import { GetAllaJobsCount } from '../../Shared/Models/JobModel/JobModel';
import { ToastrService } from 'ngx-toastr';
import { DatePipe } from '@angular/common';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ExcelFileService } from '../../Shared/CommonServices/excel-file.service';
import { DynamicScriptLoaderService } from '../../Shared/CommonServices/dynamicScriptLoaderService';
import { ExportToCsv } from 'export-to-csv';
import html2canvas from 'html2canvas';

import { jsPDF } from 'jspdf';
import 'jspdf-autotable';
import { UserOptions } from 'jspdf-autotable';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ResponseVm } from '../../Shared/Models/HomeModel/HomeModel';
import { BasicRegistration, CheckEmailandPhoneNumberAvailability } from '../../Shared/Models/UserModel/SpSupplierVM';
import { httpStatus } from '../../Shared/Enums/enums';
import { JwtHelperService } from '@auth0/angular-jwt';
import { loginsecurity } from '../../Shared/Enums/enums';
import { Router } from '@angular/router';
//declare const jsPDF: any;
interface jsPDFWithPlugin extends jsPDF {
  autoTable: (options: UserOptions) => jsPDF;
}

@Component({
  selector: 'app-organisation',
  templateUrl: './organisation.component.html',
  styleUrls: ['./organisation.component.css']
})
export class OrganisationComponent implements OnInit {
  public tradesmanStats: SpTradesmanStatsVM = new SpTradesmanStatsVM();
  public tradesmanList: SpTradesmanListVM[] = [];
  public cloneList: SpTradesmanListVM[] = [];
  public getInvoicesList: TradesmanPayments[] = [];
  public data: GetAllaJobsCount = new GetAllaJobsCount();

  public businessProfile: SpBusinessProfileVM = new SpBusinessProfileVM;
  public profile: tradesmanProfile = new tradesmanProfile();
  public personalDetailsUpdate: PersonalDetailsUpdatetrd = new PersonalDetailsUpdatetrd();
  public businessDetailsupdate: BusinessDetailsupdatetrd = new BusinessDetailsupdatetrd();
  public response: ResponseVm = new ResponseVm();
  public basicRegistrationVm: BasicRegistration = new BasicRegistration();
  public checkEmailEvailabilityVm: CheckEmailandPhoneNumberAvailability = new CheckEmailandPhoneNumberAvailability();
  public townVm: ITownVM[] = [];
  public pageing1 = [];
  public pageNumber: number = 1;
  public totalJobs: number;
  public totalPages: number;
  public emailupdate = '';
  public status: boolean = true;
  public firstPageactive: boolean;
  public patchValue: boolean = false;
  public lastPageactive: boolean;
  public previousPageactive: boolean;
  public nextPageactive: boolean;
  public listOfIds: Array<number> = [];
  public getLeadgerList = [];
  public recoardNoFrom = 0;
  public recoardNoTo = 50;
  public noOfRecoardShowOnPage;
  public totalRecoards = 101;
  public noOfPages;
  public dataOrderBy = "DESC";
  public quotationId;
  public allowview;
  public trademanId = "";
  public BirthDate: string;
  public newgetLeadgerList= [];
  public skillList = [];
  SelectedSkillsList = [];
  public selectedSkills = [];
  public tielsList = [];
  public pipe;
  public searchBy = 1;
  public startDate: Date;
  public endDate: Date;
  public startDate1: Date;
  public endDate1: Date;
  public location = "";
  public tradesmanName = "";
  public organizationId = "";
  public cityList = [];
  public selectedCities = [];
  public selectedCity = [];
  public selectedColumn = [];
  public dropdownListForCity = {};
  public skillsdropdownSettings = {};
  public dropdownListForColumn = {};

  public isAddress = false;
  public searchedAddress = [];
  public cnic = "";
  public mobile = "";
  public dataNotFound: boolean;
  public sprights = 'false';
  public allowEditTreadesman;
  public excelFileList = [];
  public selecteddatasingle = [];


  public startDateErrorMessage: string;
  public endDateErrorMessage: string;
  public submittedApplicationForm = false;
  public usertype = 3;
  public emailtype = 1;
  public mobileType = 1;
  public activityType = 1;
  public SourceOfReg1: number;
  public mobileNumberupdate: number;
  public email = '';
  public appValForm: FormGroup;
  public isOrganization = true;
  public CitiesList = [];
  public submittedForm = false;
  public selectedItemsSubCategory = [];
  public fullName: string;
  public id: number;
  public cityName: string = "";
  public TradesmanSkillsSelected = [''];
  public salesmanList = [];
  public salesmanList1 = [];
  public pdfrequest: boolean = true;
  public salesmandropdownSettings = {};
  public pkgForm: FormGroup;
  public selectedsalesman: string = '';
  public submited: boolean = false;
  public securityRecord: any;
  public securityRoleId: number;
  public analytic: IAnalyticsModal;
  public tradesmanRating = [];
  public totalTradesmanJobs: number;
  public jobsRating: number = 0;
  public userRoleName = "Organization";
  public selectedTradesmanskillCategory = [];
  keyword = 'name';
  public walletAmount: number = 0;

  jwtHelperService: JwtHelperService = new JwtHelperService();
  @ViewChild('postJobModalConfirm', { static: true }) postJobModal: ModalDirective;
  @ViewChild('dataTable', { static: true }) dataTable: ElementRef;
  @ViewChild('checkParent', { static: true }) checkParent: ElementRef;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  constructor(public router: Router, public dynamicScripts: DynamicScriptLoaderService,
    public excelService: ExcelFileService, private _modalService: NgbModal, public service: CommonService,
    public toastr: ToastrService, public Loader: NgxSpinnerService, public sortList: SortList, private formBuilder: FormBuilder) {
    this.tradesmanStats.pageSize = 10;
    this.populateData();
    this.populateOrganiztionList();
  }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Organization List"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.BirthDate = '';
    this.skillsdropdownSettings = {
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
    this.salesmandropdownSettings = {
      singleSelection: true,
      idField: 'salesmanId',
      textField: 'name',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };
    this.pkgForm = this.formBuilder.group({
      salesmanId: [0, [Validators.required]],
    });
    this.appValForm = this.formBuilder.group({
      tradesmanId: [0],
      firstName: ['', [Validators.required, Validators.minLength(1)]],
      lastName: ['', [Validators.required, Validators.minLength(1)]],
      email: ['', [Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]],
      cnic: [''],
      mobileNumber: ['', [Validators.required, Validators.pattern("^[0-9]{4}[0-9]{7}$")]],
      dateOfBirth: ['', [Validators.required]],
      gender: [0, [Validators.required]],
      businessAddress: ['', [Validators.required]],
      town: ['', [Validators.required]],
      id: [0],
      tradesmanSkills: [0, [Validators.required]],
      travelingDistance: ['', [Validators.required]],
      cityId: [0, [Validators.required]],
      isOrganization: [null, [Validators.required]],
      companyName: ['', [Validators.minLength(1)]],
      companyRegNo: [''],
      userId: [],
    });
    this.pdfrequest = true;
    this.populateSkills();
    this.getAllCities();
    this.SourceOfReg1 = 0;
    this.getPromotionTypeList();
  }
  get f() {
    return this.appValForm.controls;
  }
  handleChange(event, userId) {

    let status = event.target.checked;
    let tradesmanId = event.target.id;
    this.service.get(this.service.apiRoutes.TrdesMan.BlockTradesman + "?tradesmanId=" + tradesmanId + "&userId=" + userId + "&status=" + status).subscribe(result => {
      let response = result.json();
      if (response.status == 200) {
        if (status) {
          this.toastr.success("Tradesman Unblocked successfully!", "Success");
        }
        else {
          this.toastr.warning("Tradesman blocked successfully!", "Warning");
        }
        this.populateOrganiztionList();
      }
    })
  }

  resetForm() {
    this.location = "";
    this.organizationId = "";
    this.endDate = null;
    this.startDate = null;
    this.tradesmanName = "";
    this.selectedCities = []
    this.selectedCity = [];
    this.SelectedSkillsList = [];
    this.selectedSkills = [];
    this.cnic = "";
    this.mobile = "";
    this.id = 0;
    this.email = "";
    this.SourceOfReg1 = 0;
    const parent = this.checkParent.nativeElement
    parent.querySelector("#start").checked = true;
    parent.querySelector("#all").checked = true;
    parent.querySelector("#all1").checked = true;
    parent.querySelector("#allMobileUsers").checked = true;
    parent.querySelector("#allMobileUsers11").checked = true;
    this.populateOrganiztionList();
  }
  setAddressValue(e) {
    this.location = e.target.text;
    this.isAddress = false;
  }
  getRadioValue(e) {
    this.searchBy = e.target.value;
  }
  getRadioValueemail(e) {
    this.emailtype = e.target.value;
  }
  getRadioValueMobileNo(e) {
    this.mobileType = e.target.value;
  }
  exportCSV() {
    const options = {
      fieldSeparator: ',',
      quoteStrings: '"',
      decimalSeparator: '.',
      showLabels: true,
      showTitle: true,
      title: 'orgnizationReport',
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
      this.excelService.exportAsExcelFile(this.excelFileList, "orgnizationReport")
    else
      this.excelService.exportAsExcelFile(this.selecteddatasingle, "orgnizationReport")
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
    doc.save('Organisation.pdf')
  }
  get f1() {
    return this.pkgForm.controls;
  }
  getRadioValuesource(e) {
    this.SourceOfReg1 = e.target.value;
  }
  getRadioValueut(e) {
    this.usertype = e.target.value;
  }
  showModel1(id: any, content1) {
    this.trademanId = id;
    this._modalService.open(content1)
  }
  getRadioValueActivity(e) {
    this.activityType = e.target.value;
  }
  confrimCreateTest() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Login.changeusertype + "?userid=" + this.trademanId).subscribe(result => {
      let response = result.json();
      if (response.status == 200) {
        this.populateOrganiztionList();
        this.toastr.success("User type change successfully!", "Success");
        this.Loader.hide();
      }
      else {

        this.Loader.hide();
      }
    })

  }
  onCheckboxChange(e, obj) {

    if (e.target.checked) {
      this.selecteddatasingle.push({//obj

        'tradesmanId': obj.tradesmanId,
        'tradesmanName': obj.tradesmanName,
        'Mobile': obj.mobileNo,
        'CNIC': obj.cNIC,
        'skills': obj.skills,
        'Address': obj.tradesmanAddress,
        'completedJobsCount': obj.completedJobsCount,
        'CreatedOn': obj.createdOn,
        'IsTestUser': obj.isTestUser,
        'Id': obj.id,
      });
    } else {
      this.selecteddatasingle = this.selecteddatasingle.filter(item => item.tradesmanId != e.target.value);

    }
  }
  populateOrganiztionList() {
    let cityIds = [];
    let skillIds = [];
    this.searchBy = 1;
    this.selectedCity.forEach(function (item) {
      cityIds.push(item.id);
    });
    this.selectedSkills.forEach(function (item) {

      skillIds.push(item.id);
    });
    var customerpra = "";
    if (this.tradesmanName != "" && this.tradesmanName != undefined) {
      if (this.searchBy == 1) {
        customerpra = this.tradesmanName + "%";
      }
      else if (this.searchBy == 2) {
        customerpra = "%" + this.tradesmanName + "%";
      }
      else if (this.searchBy == 3) {
        customerpra = "%" + this.tradesmanName;
      }
    }
    if (this.SourceOfReg1 == undefined)
      this.SourceOfReg1 = 0;
    this.pipe = new DatePipe('en-US');
    this.startDate1 = this.pipe.transform(this.startDate, 'MM/dd/yyyy');
    this.endDate1 = this.pipe.transform(this.endDate, 'MM/dd/yyyy');

    let obj = {
      pageNumber: this.pageNumber,
      pageSize: this.tradesmanStats.pageSize,
      dataOrderBy: this.dataOrderBy,
      userName: customerpra,
      startDate: this.startDate1,
      endDate: this.endDate1,
      city: cityIds.toString(),
      skills: skillIds.toString(),
      cnic: this.cnic,
      id: this.id,
      mobile: this.mobile,
      usertype: this.usertype.toString(),
      emailtype: this.emailtype.toString(),
      mobileType: this.mobileType.toString(),
      activityType: this.activityType.toString(),
      location: this.location,
      sourceOfReg: this.SourceOfReg1.toString(),
      email: this.email,
      isOrganisation: true,
      tradesmanId: this.organizationId
    };
    this.Loader.show();

    this.service.post(this.service.apiRoutes.Users.SpGetTradesmanList, obj).subscribe(result => {
      if (result.json() != null) {
        this.tradesmanList = result.json();
        if (this.selecteddatasingle.length > 0) {

          for (var x = 0; x < this.tradesmanList.length; x++) {
            this.tradesmanList[x].isselectedforexport = false;
            var xx = this.selecteddatasingle.find(z => z.tradesmanId == this.tradesmanList[x].tradesmanId)
            if (xx != null && xx != undefined && xx != "")
              this.tradesmanList[x].isselectedforexport = true;
          }

        }
        else {
          for (var x = 0; x < this.tradesmanList.length; x++) {
            this.tradesmanList[x].isselectedforexport = false;
          }
        }
        this.excelFileList = this.tradesmanList;
        this.cloneList = this.tradesmanList;
        this.dataNotFound = true;
        for (var i = 0; i < this.tradesmanList.length; i++) {
          this.listOfIds.push(this.tradesmanList[i].tradesmanId);
          if (this.tradesmanList[i].skills != "" && this.tradesmanList[i].skills != null && this.tradesmanList[i].skills != undefined) {
            let spl = this.tradesmanList[i].skills.split(",")
            if (spl.length > 2) {
              this.tradesmanList[i].smallSkills = spl[0].replace("&amp;", "") + "," + spl[1].replace("&amp;", "") + "";
            }
            else {
              this.tradesmanList[i].smallSkills = this.tradesmanList[i].skills.replace("&amp;", "");
            }
          }
        }
        for (var i = 0; i < this.tradesmanList.length; i++) {
          this.tradesmanList[i].skills = (this.tradesmanList[i].skills != null ? this.tradesmanList[i].skills.split('&amp;').join('') : '');
        }
        this.noOfPages = this.tradesmanList[0].noOfRecoards / this.tradesmanStats.pageSize

        this.noOfPages = Math.floor(this.noOfPages);
        if (this.tradesmanList[0].noOfRecoards > this.noOfPages) {
          this.noOfPages = this.noOfPages + 1;
        }

        this.totalRecoards = this.tradesmanList[0].noOfRecoards;
        this.pageing1 = [];
        for (var x = 1; x <= this.noOfPages; x++) {
          this.pageing1.push(
            x
          );
        }
        this.recoardNoFrom = (this.tradesmanStats.pageSize * this.pageNumber) - this.tradesmanStats.pageSize + 1;
        this.recoardNoTo = (this.tradesmanStats.pageSize * this.pageNumber);
        if (this.recoardNoTo > this.tradesmanList[0].noOfRecoards)
          this.recoardNoTo = this.tradesmanList[0].noOfRecoards;
        this.Loader.hide();
      }
      else {
        this.recoardNoFrom = 0;
        this.recoardNoTo = 0;
        this.noOfPages = 0;
        this.totalRecoards = 0;
        this.tradesmanList = [];
        this.cloneList = [];
        this.toastr.error("No record found !", "Search")
        this.dataNotFound = false;
        this.Loader.hide();
      }
    },
      error => {
        console.log(error);
        this.Loader.hide();
      });
    this.Loader.hide();
  }
  populateData() {
    this.Loader.show();
    this.tradesmanStats.pageSize = 50;
    this.service.get(this.service.apiRoutes.Users.SpGetTradesmanStats).subscribe(result => {

      this.tradesmanStats = result.json();
      this.tradesmanStats.pageSize = 50;
      this.totalJobs = this.tradesmanStats.organisationCount;
      this.NumberOfPages();
      this.Loader.hide();
    },
      error => {
        console.log(error);
        this.Loader.hide();
        alert(error);
      });
  }

  customerDetail(customerId: number) {

    this.service.NavigateToRouteWithQueryString(this.service.apiRoutes.Users.UserProfile, { queryParams: { userId: customerId, userRole: "Tradesman", subRole: "Organization", list: this.listOfIds } });

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
    this.populateOrganiztionList();
  }
  NumberOfPages() {

    this.totalPages = Math.ceil(this.totalJobs / this.tradesmanStats.pageSize);
  }
  changePageSize() {
    this.pageNumber = 1;
    this.NumberOfPages();
    this.populateOrganiztionList();
  }
  lastPage() {
    this.status = false;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = true;
    this.pageNumber = this.totalPages;
    this.populateOrganiztionList();

  }
  FirstPage() {
    this.status = false;
    this.firstPageactive = true;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = false;
    this.pageNumber = 1;
    this.populateOrganiztionList();
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
    this.populateOrganiztionList();
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
    this.populateOrganiztionList();
  }

  Filter(value: any) {
    if (!value) {
      this.cloneList = this.tradesmanList;
    }
    else {
      this.cloneList = this.tradesmanList.filter(x => x.tradesmanId.toString().includes(value) || x.mobileNo.includes(value) || x.tradesmanName.toLowerCase().includes(value.toString().toLowerCase()));
    }
  }

  clickchange() {
    this.populateOrganiztionList();
  }
  PageSizeChange() {

    this.pageNumber = 1;
    this.populateOrganiztionList();
  }
  PriviousClick() {
    if (this.pageNumber > 1) {
      this.pageNumber = parseInt(this.pageNumber.toString()) - 1;
      this.populateOrganiztionList();
    }

  }
  NextClick() {

    if (this.pageNumber < this.noOfPages) {
      this.pageNumber = parseInt(this.pageNumber.toString()) + 1;
      this.populateOrganiztionList();
    }

  }
  LoadNewestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "DESC";
    this.populateOrganiztionList();

  }
  LoadOldestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "ASC";
    this.populateOrganiztionList();
  }
  hidemodal() {

    this.postJobModal.hide();
    this.quotationId = "";
  }
  deletejobconfirm() {

    if ((this.totalRecoards % this.data.pageSize) == 1 && this.pageNumber == this.noOfPages) {
      if (this.pageNumber > 1)
        this.pageNumber = parseInt(this.pageNumber.toString()) - 1;
    }

    this.service.get(this.service.apiRoutes.Jobs.deleteJobWithJobQuotationId + "?jobQuotationId=" + this.quotationId).subscribe(result => {

      if (result.status = 200) {
        this.populateOrganiztionList();
        this.quotationId = "";
        this.postJobModal.hide();
        this.toastr.error("Record deleted successfully", "Delete");
      }
    });
  }

  GetBussinessProfile(tradesmanId, profileModal) {
    this.service.get(this.service.apiRoutes.Users.getBussinessProfile + "?userId=" + tradesmanId + "&role=" + 'Tradesman').subscribe(result => {
      this.businessProfile = null;
      this.businessProfile = result.json();
      this._modalService.open(profileModal, { size:'xl' });
      //this.getLeadgerReport(tradesmanId);
      this.getInvoices(tradesmanId);
      this.GetTradesmanWallet(tradesmanId);
      console.log(this.businessProfile);
      this.tradesmanRating = result.json().trradesmanJobsFeedbackVMs;
      this.jobsRating = 0;
      this.totalTradesmanJobs = 0;
      this.totalTradesmanJobs = this.tradesmanRating.length;
      for (var i = 0; i < this.tradesmanRating.length; i++) {
        this.jobsRating += this.tradesmanRating[i].tradesmanRating;
      }
      this.businessProfile.skills = this.businessProfile.skills.replace('&amp;', "");
      if (this.businessProfile.skills != null) {
        this.businessProfile.skills = this.businessProfile.skills.split("&amp;").join(" ");
      }
      this.Loader.hide();

    },
      error => {
        console.log(error);
        alert(error);
        this.Loader.hide();
      });
  }
public getInvoices(tradesmanId){
  debugger;
  this.service.get(this.service.apiRoutes.TrdesMan.GetInvoiceJobReceiptsById  + "?tradesmanId=" + tradesmanId).subscribe(result => {
      debugger;
          this.getInvoicesList = result.json();
    },
      error => {
        console.log(error);
      });
}
public GetTradesmanWallet(tradesmanId) {
    this.service.get(this.service.apiRoutes.TrdesMan.GetTradesmanWallet + "?tradesmanId=" + tradesmanId).subscribe(res => {
      this.response = res.json();
      if (this.response.status == httpStatus.Ok) {
        if (this.response.resultData) {
          this.walletAmount = this.response.resultData;
        }
        else
          this.walletAmount = 0;
      }
    }, error => {
      console.log(error);
    });
  }
  public getLeadgerReport(tradesmanId) {
    debugger;
    let obj = {
      customerId: 0,
      tradesmanId: tradesmanId,
      jobQuotationId: 0,
      jobDetailsId: 0
    };
    this.Loader.show();
    this.service.PostData(this.service.apiRoutes.PackagesAndPayments.GetLeaderReport, obj).then(res => {
      debugger;
      this.newgetLeadgerList = res.json();
      this.tielsList = [];
      if (this.newgetLeadgerList.length > 0) {
        this.newgetLeadgerList.forEach(y => {
          if (y.section == '1-Totals') {
            this.tielsList.push(y);
          }
        });
        this.getLeadgerList = [];
        this.newgetLeadgerList.forEach(x => {
          if (x.section == '2-Detail') {
            this.getLeadgerList.push(x);
          }
        });
      }
      this.Loader.hide();
    });
  }


  onItemSelectAll(items: any) {
    console.log(items);
    this.SelectedSkillsList = items;
  }
  OnItemDeSelectALL(items: any) {
    this.SelectedSkillsList = [];
    console.log(items);
  }
  onItemSelect(item: any) {
    this.SelectedSkillsList.push(item);
    console.log(this.SelectedSkillsList);
  }
  OnItemDeSelect(item: any) {
    this.SelectedSkillsList = this.SelectedSkillsList.filter(
      function (value, index, arr) {
        return value.id != item.id;
      }
    );
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
    this.service.get(this.service.apiRoutes.Common.GetCityList).subscribe(result => {

      this.cityList = result.json();
    })
  }

  public populateSkills() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.TrdesMan.GetTradesManSkills).subscribe(result => {
      this.skillList = result.json();
      console.log(result.json());
      this.Loader.hide();
    },
      error => {
        console.log(error);
        this.Loader.hide();
      });
  }
  public showModalAdd1(userid, id, modalName) {
    this.appValForm.reset();

    this.emailupdate = "";
    this.mobileNumberupdate = 0;
    this.selectedTradesmanskillCategory = [];
    this.BirthDate = null;
    this._modalService.open(modalName, { size: 'lg' });

  }
  public setfields(obj) {
    if (obj.target.value == 1) {
      this.isOrganization = true;

    }
    else if (obj.target.value == 2) {
      this.isOrganization = false;
    }
  }
  public showModalAdd(userid, email, mobileNo, id, modalName) {


    this.emailupdate = email;
    this.mobileNumberupdate = mobileNo;
    this._modalService.open(modalName, { size: 'lg' });
    this.PopulateData(userid, id);
  }
  DeleteSelectedUser(contentdeleteuser) {
    this._modalService.open(contentdeleteuser)
  }
  DeleteUsersConfirm() {
    for (var x = 0; x < this.selecteddatasingle.length; x++) {
      this.selecteddatasingle[x].IsTestUser = this.cloneList.find(g => g.id == this.selecteddatasingle[x].Id).isTestUser;
    }
    let obj = this.selecteddatasingle.filter(x => x.IsTestUser != true)
    if (obj.length > 0) {
      this.toastr.error("before delete please make selected organisation as test organisation", "Error");
      return;
    }
    for (var x = 0; x < this.selecteddatasingle.length; x++) {
      if (this.selecteddatasingle[x].IsTestUser) {
        this.deleteUser(this.selecteddatasingle[x].Id)
      }
    }
    this.selecteddatasingle = [];
  }

  public deleteUser(userid) {
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    let formData = {
      userId: userid,
      deletedBy: decodedtoken.UserId
    }
    this.service.post(this.service.apiRoutes.registration.DeleteUserInfo, formData).subscribe(result => {
      var res = result.json();
      if (res.message == "No data found agianst given info") {
        this.toastr.error(res.message, "Error");
      }
      else if (res.message == "User Successfully deleted") {
        this.cloneList = this.cloneList.filter(obj => obj.id != userid)
        this.toastr.success(res.message, "Success");
        this._modalService.dismissAll();
      }
    })
  }
  public PopulateData(userId, id) {
    this.appValForm.reset();
    this.Loader.show();
    this.service.get(this.service.apiRoutes.TrdesMan.GetBusinessAndPersnalProfileWeb + "?tradesmanId=" + userId).subscribe(result => {
      if (status = httpStatus.Ok) {

        this.profile = result.json();
        this.appValForm.patchValue(this.profile.persnalDetails);
        this.appValForm.patchValue(this.profile.businessDetails);
        if (this.profile.businessDetails.cityId) {
          this.patchValue = true;
          this.getTownList(this.profile.businessDetails.cityId);
          this.appValForm.controls.town.setValue(this.profile.businessDetails.town);
        }
        else {
          this.profile.businessDetails.cityId = 64;
          this.patchValue = true;
          this.appValForm.controls.cityId.setValue(this.profile.businessDetails.cityId);
          this.getTownList(this.profile.businessDetails.cityId);
          this.appValForm.controls.town.setValue(this.profile.businessDetails.town);
        }
        this.isOrganization = this.profile.businessDetails.isOrganization;

        if (this.isOrganization == true) {
          this.appValForm.controls.isOrganization.setValue(1);
        }
        else
          this.appValForm.controls.isOrganization.setValue(2);
        this.BirthDate = this.profile.persnalDetails.dateOfBirth;
        this.fullName = this.profile.persnalDetails.firstName + " " + this.profile.persnalDetails.lastName;
        this.TradesmanSkillsSelected = this.profile.businessDetails.tradesmanSkills;
        this.appValForm.controls.tradesmanSkills.setValue(this.profile.businessDetails.tradesmanSkills);
        this.selectedTradesmanskillCategory = this.profile.businessDetails.tradesmanSkills;
        this.Loader.hide();
      }
      else {
        this.Loader.hide();
      }
    },
      error => {
        console.log(error);
        this.Loader.hide();
      });
  }


  public submitDetails() {
    if (this.appValForm.valid) {
      this.appValForm.value.role = "Organization";
      var data = this.appValForm.value;
      this.basicRegistrationVm.password = "P@ss" + 1 + 2 + 3 + 4 + 5;
      this.checkEmailEvailabilityVm.password = this.basicRegistrationVm.password;
      this.checkEmailEvailabilityVm.role = data.role;

      if (data.userId != '' && data.userId != null && data.userId != undefined) {
        if (this.emailupdate != data.email) {
          this.checkEmailEvailabilityVm.email = data.email;
          this.checkEmailEvailabilityVm.phoneNumber = 0;
          this.service.PostData(this.service.apiRoutes.registration.CheckEmailandPhoneNumberAvailability, this.checkEmailEvailabilityVm, true).then(result => {
            this.response = result.json();
            if (this.response.status == httpStatus.Ok) {

              if (this.mobileNumberupdate != data.mobileNumber) {
                this.checkEmailEvailabilityVm.phoneNumber = data.mobileNumber;
                this.checkEmailEvailabilityVm.email = '';
                this.service.PostData(this.service.apiRoutes.registration.CheckEmailandPhoneNumberAvailability, this.checkEmailEvailabilityVm, true).then(result => {
                  this.response = result.json();
                  if (this.response.status == httpStatus.Ok) {
                    this.Save();
                  }
                  else {
                    this.toastr.error("Phone Number Already Exist !!");
                  }
                })
              }
              else {
                this.Save();
              }
            }
            else {
              this.toastr.error("Email Already Exist !!");
            }
          })
        }
        else if (this.mobileNumberupdate != data.mobileNumber) {
          this.checkEmailEvailabilityVm.email = '';
          this.checkEmailEvailabilityVm.phoneNumber = data.mobileNumber;
          this.service.PostData(this.service.apiRoutes.registration.CheckEmailandPhoneNumberAvailability, this.checkEmailEvailabilityVm, true).then(result => {
            this.response = result.json();
            if (this.response.status == httpStatus.Ok) {
              this.Save();
            }
            else {
              this.toastr.error("Phone Number Already Exist !!");
            }
          })
        }
        else {
          this.Save();
        }
      }
      else {
        this.VerifyAndSendOtp();
      }
    }
    else {
      this.appValForm.markAllAsTouched();

    }
  }


  public Save() {
    
    var data = this.appValForm.value;
    var tradesMainSkills = data.tradesmanSkills;
    if (tradesMainSkills.length) {
      for (var i in tradesMainSkills) {
        var pd = tradesMainSkills[i].id;
        this.businessDetailsupdate.skillIds.push(pd);
      }
    }
    else {
      this.businessDetailsupdate.skillIds.push(pd);
    }
    this.businessDetailsupdate.businessAddress = data.businessAddress
    this.businessDetailsupdate.city = this.cityName;
    this.businessDetailsupdate.cityId = data.cityId;
    this.businessDetailsupdate.town = data.town.name;
    this.businessDetailsupdate.travelingDistance = data.travelingDistance;
    this.businessDetailsupdate.isOrganization = this.isOrganization;
    this.businessDetailsupdate.companyRegNo = data.companyRegNo;
    this.businessDetailsupdate.companyName = data.companyName;
    this.businessDetailsupdate.tradesmanSkills = [];
    this.businessDetailsupdate.tradesmanId = data.tradesmanId;
    this.businessDetailsupdate.userId = data.userId;

    this.personalDetailsUpdate.cnic = data.cnic;
    this.personalDetailsUpdate.dateOfBirth = data.dateOfBirth;
    this.personalDetailsUpdate.email = data.email;
    this.personalDetailsUpdate.firstName = data.firstName;
    this.personalDetailsUpdate.lastName = data.lastName;
    this.personalDetailsUpdate.gender = data.gender;
    this.personalDetailsUpdate.mobileNumber = data.mobileNumber;
    this.personalDetailsUpdate.tradesmanId = data.tradesmanId;
    this.personalDetailsUpdate.userId = data.userId;

    this.service.post(this.service.apiRoutes.TrdesMan.UpdatePersonalDetails, this.personalDetailsUpdate).subscribe(result => {

      if (status = httpStatus.Ok) {
        this.service.post(this.service.apiRoutes.TrdesMan.AddEditTradesmanWithSkills, this.businessDetailsupdate).subscribe(result => {
          if (status = httpStatus.Ok) {

            this.toastr.success("Profile Successfully Updated.");
            this.appValForm.reset();
            this.populateOrganiztionList();
            this._modalService.dismissAll();
            this.patchValue = false;
          }
        },
          error => {
            console.log(error);
            this.toastr.error("Some thing went wrong.");
          });
      }
    });
  }

  VerifyAndSendOtp() {

    if (this.appValForm.value.role == null || this.appValForm.value.role == "")

      var data = this.appValForm.value;
    this.appValForm.value.role = "Tradesman"
    this.basicRegistrationVm.password = "P@ss" + 1 + 2 + 3 + 4 + 5;
    this.basicRegistrationVm.googleUserId = "";
    this.basicRegistrationVm.facebookUserId = "";
    this.checkEmailEvailabilityVm.email = data.email;
    this.checkEmailEvailabilityVm.phoneNumber = data.mobileNumber;
    this.checkEmailEvailabilityVm.role = data.role;
    this.checkEmailEvailabilityVm.googleUserId = this.basicRegistrationVm.googleUserId;
    this.checkEmailEvailabilityVm.facebookUserId = this.basicRegistrationVm.facebookUserId;
    this.checkEmailEvailabilityVm.password = this.basicRegistrationVm.password;
    this.basicRegistrationVm.termsAndcondition = true;
    this.basicRegistrationVm.firstName = data.firstName;
    this.basicRegistrationVm.lastName = data.lastName;
    this.basicRegistrationVm.city = data.city;
    this.basicRegistrationVm.cnic = data.cnic;
    this.basicRegistrationVm.phoneNumber = data.mobileNumber;
    this.basicRegistrationVm.emailAddress = data.email;
    this.basicRegistrationVm.role = data.role;

    this.service.PostData(this.service.apiRoutes.registration.CheckEmailandPhoneNumberAvailability, this.checkEmailEvailabilityVm, true).then(result => {

      this.response = result.json();
      if (this.response.status == httpStatus.Ok) {

        this.service.PostData(this.service.apiRoutes.registration.verifyOtpAndRegisterUser, this.basicRegistrationVm, true).then(result => {

          this.response = result.json();
          if (this.response.status == httpStatus.Ok) {

            this.appValForm.controls.tradesmanId.setValue(this.response.resultData.tradesmanId);
            this.appValForm.controls.userId.setValue(this.response.resultData.userId);
            this.Savetrd();
          }
          else {
          }
        })
      }
      else {

        if (this.response.resultData[0].key == "DuplicatePhoneNumber") {

          this.toastr.error("Phone Number Already Exist !!");

        }
        else if (this.response.resultData[0].key == "DuplicateEmail") {

          this.toastr.error("Email Already Exist !! ");
        }
        else {

          this.toastr.error("Email And PhoneNumber Already Exist !! ");
        }
      }
    });
  }
  public Savetrd() {
    if (!this.appValForm.valid)
      return;
    var data = this.appValForm.value;
    var data = this.appValForm.value;
    var tradesMainSkills = data.tradesmanSkills;
    if (tradesMainSkills.length) {
      for (var i in tradesMainSkills) {
        var pd = tradesMainSkills[i].id;
        this.businessDetailsupdate.skillIds.push(pd);
      }
    }
    else {
      this.businessDetailsupdate.skillIds.push(pd);
    }
    this.businessDetailsupdate.businessAddress = data.businessAddress
    this.businessDetailsupdate.city = this.cityName;
    this.businessDetailsupdate.cityId = data.cityId;
    this.businessDetailsupdate.town = data.town;
    this.businessDetailsupdate.travelingDistance = data.travelingDistance;
    this.businessDetailsupdate.isOrganization = this.isOrganization;
    this.businessDetailsupdate.companyRegNo = data.companyRegNo;
    this.businessDetailsupdate.companyName = data.companyName;
    this.businessDetailsupdate.tradesmanSkills = [];
    this.businessDetailsupdate.tradesmanId = data.tradesmanId;
    this.businessDetailsupdate.userId = data.userId;


    this.service.post(this.service.apiRoutes.TrdesMan.AddEditTradesmanWithSkills, this.businessDetailsupdate).subscribe(result => {
      this.response = result.json();

      if (this.response.status == httpStatus.Ok) {
      }
      this.toastr.success("Profile Inserted Successfully.");
      this.appValForm.reset();
      this.populateOrganiztionList();
      this._modalService.dismissAll();
      this.populateData();
    }, error => {
      console.log(error);
    })
  }

  public CityValue(obj) {
    let selectElementText = obj.target['options']
    [obj.target['options'].selectedIndex].text;
    this.cityName = selectElementText;
    this.getTownList(obj.target.value);
  }

  public SalesmanConfirm(contentsales) {
    this.GetSalesmanlist(contentsales);
  }

  public GetSalesmanlist(contentsales) {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.UserManagement.getAllSalesman).subscribe(result => {
      this.salesmanList = result.json();
      this.salesmanList = this.salesmanList1.filter(x => x.isActive == true);
      this._modalService.open(contentsales);
      this.Loader.hide();
    })
  }
  public getPromotionTypeList() {

    this.service.get(this.service.apiRoutes.UserManagement.getAllSalesman).subscribe(result => {

      this.salesmanList1 = result.json();
      this.salesmanList1 = this.salesmanList1.filter(x => x.isActive == true);

    })
  }
  public SavelinkedSaleman() {


    if (this.pkgForm.invalid) {
      this.submited = true;
      return;
    }
    if (this.selecteddatasingle.length > 0) {
      var cusId = '';
      for (var x = 0; x < this.selecteddatasingle.length; x++) {
        cusId += this.selecteddatasingle[x].tradesmanId + ',';
      }
      var data = this.pkgForm.value;
      this.Loader.show();
      this.service.get(this.service.apiRoutes.TrdesMan.AddLinkedSalesmantrd + "?SalesmanId=" + data.salesmanId + "&CustomerId=" + cusId).subscribe(result => {

        if (result.ok) {
          //this._modalService.open(contentsales);
          this.Loader.hide();
          this.submited = false;
          this.toastr.success("Update Successfully !", "Success");
          this._modalService.dismissAll();
          this.selecteddatasingle = [];
          this.populateOrganiztionList();

        }
      })
      this.Loader.hide();
    }
  }
  public getTownList(cityId) {
    this.service.get(this.service.apiRoutes.UserManagement.getTownListByCityId + "?cityId=" + cityId).subscribe(result => {
      if (this.patchValue) {
        this.townVm = result.json();
        this.patchValue = false;

      }
      else {
        this.appValForm.controls['town'].setValue("");
        this.townVm = result.json();

      }
    })
  }

  selectEvent(item) {
    // do something with selected item
  }

  unselectEvent(item) {

  }
  public showAnalyticsModal(userId, modalName) {

    this._modalService.open(modalName, { size: 'xl' });

    this.populateAnalyticsData(userId);
  }

  public populateAnalyticsData(userId) {

    console.log(userId);

    this.service.get(this.service.apiRoutes.Analytics.UserAnalytics + "?userId=" + userId + "&userRole=" + this.userRoleName).subscribe(response => {
      this.analytic = response.json();

      console.log(this.analytic);

    }, error => {

    });


  }

}
