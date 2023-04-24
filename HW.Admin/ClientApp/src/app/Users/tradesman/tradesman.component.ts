import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { CommonService } from 'src/app/Shared/HttpClient/_http';
import { SpTradesmanStatsVM, SpTradesmanListVM, ITownVM,TradesmanPayments } from 'src/app/Shared/Models/UserModel/SpTradesmanVM';
import { NgxSpinnerService } from "ngx-spinner";
import { SortList } from 'src/app/Shared/Sorting/sortList';
import { ActivatedRoute, Router } from '@angular/router';
import { SpUserProfileVM, SpBusinessProfileVM, tradesmanProfile, PersonalDetailsUpdate, BusinessDetailsupdate, BusinessDetailsupdatetrd, PersonalDetailsUpdatetrd, IAnalyticsModal } from 'src/app/Shared/Models/PrimaryUserModel/PrimaryUserModel';
import { ModalDirective } from 'ngx-bootstrap/modal/modal.directive';
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
import { NgForm, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { aspNetUserRoles, httpStatus, RegistrationErrorMessages } from '../../Shared/Enums/enums';
import { ResponseVm } from '../../Shared/Models/HomeModel/HomeModel';
import { BasicRegistration, CheckEmailandPhoneNumberAvailability, IdValueVm } from '../../Shared/Models/UserModel/SpSupplierVM';
import { JwtHelperService } from '@auth0/angular-jwt';
import { loginsecurity } from '../../Shared/Enums/enums';

//declare const jsPDF: any;
interface jsPDFWithPlugin extends jsPDF {
  autoTable: (options: UserOptions) => jsPDF;
}

@Component({
  selector: 'app-tradesman',
  templateUrl: './tradesman.component.html',
  styleUrls: ['./tradesman.component.css']
})
export class TradesmanComponent implements OnInit {

  public tradesmanStats: SpTradesmanStatsVM = new SpTradesmanStatsVM();

  public tradesmanList: SpTradesmanListVM[] = [];
  public cloneList: SpTradesmanListVM[] = [];
public getInvoicesList: TradesmanPayments[] = [];
  public businessProfile: SpBusinessProfileVM = new SpBusinessProfileVM;
  public townVm: ITownVM[] = []
  public profile: tradesmanProfile = new tradesmanProfile();
  public personalDetailsUpdate: PersonalDetailsUpdatetrd = new PersonalDetailsUpdatetrd();
  public businessDetailsupdate: BusinessDetailsupdatetrd = new BusinessDetailsupdatetrd();
  public response: ResponseVm = new ResponseVm();
  public basicRegistrationVm: BasicRegistration = new BasicRegistration();
  public checkEmailEvailabilityVm: CheckEmailandPhoneNumberAvailability = new CheckEmailandPhoneNumberAvailability();
  public idValueVm: IdValueVm[] = [];
  public tradesmanRating = [];
  public totalTradesmanJobs: number;
  public jobsRating: number = 0;
  public pageing1 = [];
  public listOfIds: Array<number> = [];
  public pageNumber: number = 1;
  public totalJobs: number;
  public totalPages: number;
  public status: boolean = true;
  public firstPageactive: boolean;
  public mobileNumberupdate: number;
  public lastPageactive: boolean;
  public previousPageactive: boolean;
  public nextPageactive: boolean;
  public trademanId: "";
  public BirthDate: string;
  public recoardNoFrom = 0;
  public recoardNoTo = 50;
  public totalRecoards = 101;
  public emailupdate = '';
  public noOfPages;
  public dataOrderBy = "DESC";
  public quotationId;
  public allowview;
  public allowEditTreadesman;
  public soleTrader: boolean = false;
  public newgetLeadgerList = [];
  public tielsList = [];
  public getLeadgerList = [];
  public skillList = [];
  public SelectedSkillsList = [];
  public selectedSkills = [];
  public salesmandropdownSettings = {};
  public pipe;
  public searchBy = 1;
  public startDate: Date;
  public endDate: Date;
  public startDate1: Date;
  public endDate1: Date;
  public location = "";
  public tradesmanName = "";
  public cityList = [];
  public selectedCities = [];
  public selectedCity = [];
  public selectedColumn = [];
  public dropdownListForCity = {};
  public skillsdropdownSettings = {};
  public dropdownListForColumn = {};

  public isAddress = false;
  public searchedAddress = [];
  public sprights = 'false';
  public excelFileList = [];
  public SourceOfReg1: number;


  public startDateErrorMessage: string;
  public endDateErrorMessage: string;
  public submittedApplicationForm = false;

  public tradesmanId = "";
  public cnic = "";
  public mobile = "";
  public dataNotFound: boolean;
  public selecteddatasingle = [];
  public usertype = 3;
  public emailtype = 1;
  public mobileType = 1;
  public activityType = 1;
  public appValForm: FormGroup;
  public pkgForm: FormGroup;
  public isOrganization = true;
  public CitiesList = [];
  public salesmanList = [];
  public salesmanList1 = [];
  public submittedForm = false;
  public selectedItemsSubCategory = [];
  public fullName: string;
  public cityName: string = "";
  public TradesmanSkillsSelected = [''];
  public email = '';
  public id: number;
  public pdfrequest: boolean = true;
  public submited: boolean = false;
  public selectedsalesman = [];
  public emailAlreadyRegistered: boolean = false;
  public emailAlreadyRegisteredErrorMessage: string;
  public mobielNumberRegistered: boolean = false;
  public mobileNumberAlreadyRegisteredErrorMessage: string;
  public selectedTradesmanskillCategory = [];
  public analytic: IAnalyticsModal;
  keyword = 'name';
  public walletAmount: number = 0;

  // public tradesManform: FormGroup;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  @ViewChild('postJobModalConfirm', { static: true }) postJobModal: ModalDirective;
  @ViewChild('dataTable', { static: true }) dataTable: ElementRef;
  @ViewChild('checkParent', { static: true }) checkParent: ElementRef;
  @ViewChild('addNewSupplier', { static: true }) addNewSupplier: ModalDirective;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  constructor(public dynamicScripts: DynamicScriptLoaderService,
    public excelService: ExcelFileService, private _modalService: NgbModal,
    public service: CommonService, public toastr: ToastrService,
    public Loader: NgxSpinnerService, public sortList: SortList, private router: Router,
    private formBuilder: FormBuilder,) {
    this.tradesmanStats.pageSize = 50;
    this.populateData();
    this.populateTradesmanList();
  }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Sole Tradesman List"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.BirthDate = '';
    this.usertype = 3;
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
      // email: ['', [ Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]],
      email: ['',],
      cnic: [''],
      id: [0],
      mobileNumber: ['', [Validators.required, Validators.pattern("^[0-9]{4}[0-9]{7}$")]],
      dateOfBirth: ['', [Validators.required]],
      gender: [0, [Validators.required]],
      businessAddress: [''],
      town: [''],
      tradesmanSkills: [0, [Validators.required]],
      travelingDistance: ['', [Validators.required]],
      cityId: [0, [Validators.required]],
      isOrganization: [null, [Validators.required]],
      companyName: ['', [Validators.minLength(1)]],
      companyReg: [''],
      companyRegNo: [''],
      userId: [''],
    });

    this.SourceOfReg1 = 0;
    this.emailupdate = "";
    this.mobileNumberupdate = 0;
    this.selectedsalesman = [];
    this.pdfrequest = true;
    this.populateSkills();
    this.getAllCities();
    this.getPromotionTypeList();
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
        this.populateTradesmanList();
      }
    })
  }
  get f1() {
    return this.pkgForm.controls;
  }
  get f() {
    return this.appValForm.controls;
  }

  resetForm() {

    this.endDate = null;
    this.startDate = null;
    this.tradesmanName = "";
    this.tradesmanId = "";
    this.selectedCities = []
    this.selectedCity = [];
    this.selectedSkills = [];
    this.SelectedSkillsList = [];
    this.selectedsalesman = [];
    this.cnic = "";
    this.mobile = "";
    this.email = "";
    this.id = 0;
    this.location = "";
    this.SourceOfReg1 = 0;
    const parent = this.checkParent.nativeElement;
    parent.querySelector("#start").checked = true;
    parent.querySelector("#all").checked = true;
    parent.querySelector("#all1").checked = true;
    parent.querySelector("#allMobileUsers").checked = true;
    parent.querySelector("#allMobileUsers11").checked = true;
    parent.querySelector("#sourceOfRegAllUser").checked = true;
    this.populateTradesmanList();
  }
  setAddressValue(e) {
    this.location = e.target.text;
    this.isAddress = false;
  }
  getRadioValue(e) {
    this.searchBy = e.target.value;
  }
  getRadioValueMobileNo(e) {
    this.mobileType = e.target.value;
  }
  getRadioValueActivity(e) {
    this.activityType = e.target.value;
  }
  getRadioValueut(e) {
    this.usertype = e.target.value;
  }
  getRadioValueemail(e) {
    this.emailtype = e.target.value;
  }
  getRadioValuesource(e) {

    this.SourceOfReg1 = e.target.value;
  }
  exportCSV() {
    const options = {
      fieldSeparator: ',',
      quoteStrings: '"',
      decimalSeparator: '.',
      showLabels: true,
      showTitle: true,
      title: 'TradesmanReport',
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
      this.excelService.exportAsExcelFile(this.excelFileList, "TradesmanReport")
    else
      this.excelService.exportAsExcelFile(this.selecteddatasingle, "TradesmanReport")
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
    doc.save('Tradesman.pdf')


  }


  showModel1(id: any, content1) {
    this.trademanId = id;
    this._modalService.open(content1)
  }
  confrimCreateTest() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Login.changeusertype + "?userid=" + this.trademanId).subscribe(result => {
      let response = result.json();
      if (response.status == 200) {
        this.populateTradesmanList();
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
  populateTradesmanList() {

    let cityIds = [];
    this.selectedCity.forEach(function (item) {
      cityIds.push(item.id);
    });

    let skillIds = [];
    if (this.selectedSkills != null && this.selectedSkills != undefined) {
      this.selectedSkills.forEach(function (item) {
        skillIds.push(item.id);
      });
    }
    else {
      this.selectedSkills = [];
      this.selectedSkills.forEach(function (item) {
        skillIds.push(item.id);
      });
    }

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
    var salesmandata = '';
    if (this.selectedsalesman != null) {
      if (this.selectedsalesman.length > 0) {

        salesmandata = this.selectedsalesman[0].salesmanId;
      }
    }

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
      mobile: this.mobile,
      id: this.id,
      usertype: this.usertype.toString(),
      emailtype: this.emailtype.toString(),
      mobileType: this.mobileType.toString(),
      activityType: this.activityType.toString(),
      location: this.location,
      sourceOfReg: this.SourceOfReg1.toString(),
      email: this.email,
      isOrganisation: false,
      SalesmanId: salesmandata,
      tradesmanId: this.tradesmanId
    };
    this.Loader.show();
    this.service.post(this.service.apiRoutes.Users.SpGetTradesmanList, obj).subscribe(result => {
      if (result.json() != null) {
        this.tradesmanList = result.json();
        console.log(this.tradesmanList);
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
        this.cloneList = this.tradesmanList;
        console.log(this.cloneList);
        ;
        this.noOfPages = this.tradesmanList[0].noOfRecoards / this.tradesmanStats.pageSize
        this.noOfPages = Math.floor(this.noOfPages);
        if (this.tradesmanList[0].noOfRecoards > this.noOfPages) {
          this.noOfPages = this.noOfPages + 1;
        }
        this.totalRecoards = this.tradesmanList[0].noOfRecoards;
        this.pageing1 = [];
        for (var x = 1; x <= this.noOfPages; x++) {
          this.pageing1.push(x);
        }
        this.recoardNoFrom = (this.tradesmanStats.pageSize * this.pageNumber) - this.tradesmanStats.pageSize + 1;
        this.recoardNoTo = (this.tradesmanStats.pageSize * this.pageNumber);
        if (this.recoardNoTo > this.tradesmanList[0].noOfRecoards)
          this.recoardNoTo = this.tradesmanList[0].noOfRecoards;
        this.Loader.hide();
      //  this.GetBussinessProfile(this.tradesmanList[0].tradesmanId,"");
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
  DeleteSelectedUser(contentdeleteuser) {
    this._modalService.open(contentdeleteuser)
  }
  DeleteUsersConfirm() {

    for (var x = 0; x < this.selecteddatasingle.length; x++) {
      this.selecteddatasingle[x].IsTestUser = this.cloneList.find(g => g.id == this.selecteddatasingle[x].Id).isTestUser;
    }
    let obj = this.selecteddatasingle.filter(x => x.IsTestUser != true)
    if (obj.length > 0) {
      this.toastr.error("before delete please make selected tradesmans as test tradesman", "Error");
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
  populateData() {
    this.Loader.show();
    this.tradesmanStats.pageSize = 50;
    this.service.get(this.service.apiRoutes.Users.SpGetTradesmanStats).subscribe(result => {

      this.tradesmanStats = result.json();
      this.tradesmanStats.pageSize = 50;
      var check = localStorage.getItem("PageSize");
      if (check == null) {
        check = '100';
      }
      this.totalJobs = this.tradesmanStats.tradesmanCount;
      this.NumberOfPages();
      this.Loader.hide();
    },
      error => {
        console.log(error);
        this.Loader.hide();
      });
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
    this.populateTradesmanList();
  }
  NumberOfPages() {
    this.totalPages = Math.ceil(this.totalJobs / this.tradesmanStats.pageSize);
  }
  changePageSize() {
    this.pageNumber = 1;
    localStorage.setItem("PageSize", this.tradesmanStats.pageSize.toString());
    this.NumberOfPages();
    this.populateTradesmanList();
  }
  lastPage() {
    this.status = false;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = true;
    this.pageNumber = this.totalPages;
    this.populateTradesmanList();

  }
  FirstPage() {
    this.status = false;
    this.firstPageactive = true;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = false;
    this.pageNumber = 1;
    this.populateTradesmanList();
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
    this.populateTradesmanList();
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
    this.populateTradesmanList();
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
    this.populateTradesmanList();
  }
  PageSizeChange() {

    this.pageNumber = 1;
    this.populateTradesmanList();
  }
  PriviousClick() {
    if (this.pageNumber > 1) {
      this.pageNumber = this.pageNumber - 1;
      this.populateTradesmanList();
    }

  }
  NextClick() {

    if (this.pageNumber < this.noOfPages) {
      this.pageNumber = parseInt(this.pageNumber.toString()) + 1;
      this.populateTradesmanList();
    }

  }
  LoadNewestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "DESC";
    this.populateTradesmanList();

  }
  LoadOldestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "ASC";
    this.populateTradesmanList();
  }
  hidemodal() {

    this.postJobModal.hide();
    this.quotationId = "";
  }
  deletejobconfirm() {

    if ((this.totalRecoards % this.tradesmanStats.pageSize) == 1 && this.pageNumber == this.noOfPages) {
      if (this.pageNumber > 1)
        this.pageNumber = parseInt(this.pageNumber.toString()) - 1;
    }

    this.service.get(this.service.apiRoutes.Jobs.deleteJobWithJobQuotationId + "?jobQuotationId=" + this.quotationId).subscribe(result => {

      if (result.status = 200) {
        this.populateTradesmanList();
        this.quotationId = "";
        this.postJobModal.hide();
        this.toastr.error("Record deleted successfully", "Delete");
      }
    });
  }
  GetBussinessProfile(CustomerId, profileModal) {

    this.service.get(this.service.apiRoutes.Users.getBussinessProfile + "?userId=" + CustomerId + "&role=" + 'Tradesman').subscribe(result => {
      this.businessProfile = null;

      this.businessProfile = result.json();
      this._modalService.open(profileModal, { size:'xl' });
      console.log(this.businessProfile);
      //this.getLeadgerReport(CustomerId);
      this.getInvoices(CustomerId);
      this.GetTradesmanWallet(CustomerId);
      this.tradesmanRating = result.json().trradesmanJobsFeedbackVMs;
      this.jobsRating = 0;
      this.totalTradesmanJobs = 0;
      this.totalTradesmanJobs = this.tradesmanRating.length;
      for (var i = 0; i < this.tradesmanRating.length; i++) {

        this.jobsRating += this.tradesmanRating[i].tradesmanRating;
      }
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
  public getLeadgerReport(customerId) {
    debugger;
    let obj = {
      customerId: 0,
      tradesmanId: customerId,
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
    this.SelectedSkillsList = items;
  }
  OnItemDeSelectALL(items: any) {
    this.SelectedSkillsList = [];
  }
  onItemSelect(item: any) {
    this.SelectedSkillsList.push(item);
  }
  OnItemDeSelect(item: any) {
    this.SelectedSkillsList = this.SelectedSkillsList.filter(
      function (value, index, arr) {
        return value.id != item.id;
      }
    );
  }

  onCitySelectAll(item: any) {
    this.selectedCity = item;
  }
  onCityDeSelectALL(item: any) {
    this.selectedCity = [];
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
    this.selectedColumn = item;
  }
  onColumnDeSelectALL(item: any) {
    this.selectedColumn = [];
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
    });
  }

  public populateSkills() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.TrdesMan.GetTradesManSkills).subscribe(result => {
      this.skillList = result.json();
      this.Loader.hide();
    },
      error => {
        console.log(error);
        this.Loader.hide();
      });
  }
  public showModalAdd1(userid, id, ModalName) {
    this.appValForm.reset();
    this.emailupdate = "";
    this.mobileNumberupdate = 0;
    this.selectedTradesmanskillCategory = [];
    this.BirthDate = null;
    this._modalService.open(ModalName, { size: 'lg' });

  }
  public setfields(obj) {
    if (obj.target.value == 1) {
      this.isOrganization = true;

    }
    else if (obj.target.value == 2) {
      this.isOrganization = false;
    }
  }
  public showModalAdd(userid, email, mobileNo, id, ModalName) {

                   
    this.appValForm.reset();
    this.emailupdate = email;
    this.mobileNumberupdate = mobileNo;
    this._modalService.open(ModalName, { size: 'lg' });
    this.PopulateData(userid, id);
  }
  public PopulateData(userId, id) {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.TrdesMan.GetBusinessAndPersnalProfileWeb + "?tradesmanId=" + userId).subscribe(result => {
      if (status = httpStatus.Ok) {
        this.profile = result.json();
        this.appValForm.patchValue(this.profile.persnalDetails);
        this.appValForm.patchValue(this.profile.businessDetails);
        var city = this.CitiesList.filter(c => c.id == this.profile.businessDetails.cityId);
        if (city.length > 0) {
          this.cityName = city[0].value;
        }
        this.isOrganization = this.profile.businessDetails.isOrganization;

        if (this.isOrganization == true) {
          this.appValForm.controls.isOrganization.setValue(1);
        }
        else {
          this.appValForm.controls.isOrganization.setValue(0);
        }
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
      var data = this.appValForm.value;
      var tradesMainSkills = data.tradesmanSkills;
      if (tradesMainSkills.length > 2) {
        this.toastr.error('You Can Select Maximuim 2 Skills', "Alert !!");
        return;
      }
      if (data.userId != '' && data.userId != null && data.userId != undefined) {
        this.basicRegistrationVm.password = "P@ss" + 1 + 2 + 3 + 4 + 5;
        this.checkEmailEvailabilityVm.password = this.basicRegistrationVm.password;
        this.checkEmailEvailabilityVm.role = "Tradesman";;
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
                });
              }
              else {
                this.Save();
              }
            }
            else {
              this.toastr.error("Email Already Exist !!");
            }
          });
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
        else if (data.userId != '' && data.userId != null && data.userId != undefined) {
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
    this.businessDetailsupdate.skillIds = [];
    var data = this.appValForm.value;
    var tradesMainSkills = data.tradesmanSkills;
    if (this.appValForm.invalid) {
      return;
    }
    else if (tradesMainSkills.length > 2) {
      this.toastr.error('You Can Select Maximum 2 Skills', "Alert !!");
      return;
    }
    else {

    }
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
    this.businessDetailsupdate.isOrganization = data.isOrganization;
    this.businessDetailsupdate.companyRegNo = data.companyRegNo;
    this.businessDetailsupdate.companyName = data.companyName;
    this.businessDetailsupdate.tradesmanSkills = data.tradesmanSkills;
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
            this._modalService.dismissAll();
            this.populateTradesmanList();
            this.addNewSupplier.hide();
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
                   
    this.emailAlreadyRegisteredErrorMessage = RegistrationErrorMessages.emailAlreadyRegisteredErrorMessage;
    this.mobileNumberAlreadyRegisteredErrorMessage = RegistrationErrorMessages.mobileNumberAlreadyRegisteredErrorMessage;

    if (this.appValForm.value.role == null || this.appValForm.value.role == "")

      var data = this.appValForm.value;
    var tradesMainSkills = data.tradesmanSkills;

    if (tradesMainSkills.length > 2) {
      this.toastr.error('You Can Select Maximum 2 Skills', "Alert !!");
      return;
    }
    this.appValForm.value.role = "Tradesman";
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
    this.basicRegistrationVm.gender = data.gender;
    this.basicRegistrationVm.dateOfBirth = data.dateOfBirth;
    this.service.PostData(this.service.apiRoutes.registration.CheckEmailandPhoneNumberAvailability, this.checkEmailEvailabilityVm, true).then(result => {

      this.response = result.json();
      if (this.response.status == httpStatus.Ok) {
        this.service.PostData(this.service.apiRoutes.registration.verifyOtpAndRegisterUser, this.basicRegistrationVm, true).then(result => {
          this.response = result.json();
          if (this.response.status == httpStatus.Ok) {
            this.appValForm.controls.tradesmanId.setValue(this.response.resultData.tradesmanId);
            this.appValForm.controls.userId.setValue(this.response.resultData.userId);
            this.Save();
          }
          else {

          }
        });

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

  public closeModal() {
    this.appValForm.reset();
    this._modalService.dismissAll();
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
          this.Loader.hide();
          this.submited = false;
          this.toastr.success("Update Successfully !", "Success");
          this._modalService.dismissAll();
          this.selecteddatasingle = [];
          this.populateTradesmanList();

        }
      })
      this.Loader.hide();
    }
  }
  public getPromotionTypeList() {
    this.service.get(this.service.apiRoutes.UserManagement.getAllSalesman).subscribe(result => {
      this.salesmanList1 = result.json();
      this.salesmanList1 = this.salesmanList1.filter(x => x.isActive == true);
    })
  }
  public getTownList(cityId) {
    this.service.get(this.service.apiRoutes.UserManagement.getTownListByCityId + "?cityId=" + cityId).subscribe(result => {
      this.townVm = result.json();
    })
  }


  selectEvent(item) {

  }
  unselectEvent(item) {

  }


}
