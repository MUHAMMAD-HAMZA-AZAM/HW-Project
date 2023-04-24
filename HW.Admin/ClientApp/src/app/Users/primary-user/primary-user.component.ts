import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { CommonService } from 'src/app/Shared/HttpClient/_http';
import { PrimaryUserVM, Customer, BusinessDetailUpdate, PersonalDetailsUpdate, IAnalyticsModal } from 'src/app/Shared/Models/PrimaryUserModel/PrimaryUserModel';
//import { debug } from 'util';
import { GetAllaJobsCount, GetCustomerJobsCount } from 'src/app/Shared/Models/JobModel/JobModel';
import { Http } from '@angular/http';
import { AdminDashboardVm, ResponseVm } from 'src/app/Shared/Models/HomeModel/HomeModel';
import { NgxSpinnerService } from "ngx-spinner";
import { SortList } from 'src/app/Shared/Sorting/sortList';
import { ActivatedRoute, Router } from '@angular/router';
import { ModalDirective } from 'ngx-bootstrap/modal/modal.directive';
import { SpUserProfileVM, SpBusinessProfileVM } from 'src/app/Shared/Models/PrimaryUserModel/PrimaryUserModel';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DatePipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { DynamicScriptLoaderService } from '../../Shared/CommonServices/dynamicScriptLoaderService';
import { ExcelFileService } from '../../Shared/CommonServices/excel-file.service';
import { ExportToCsv } from 'export-to-csv';
import html2canvas from 'html2canvas';

import { jsPDF } from 'jspdf';
import 'jspdf-autotable';
import { UserOptions } from 'jspdf-autotable';
import { httpStatus } from '../../Shared/Enums/enums';
import { BasicRegistration, CheckEmailandPhoneNumberAvailability, IdValueVm } from '../../Shared/Models/UserModel/SpSupplierVM';
import { JwtHelperService } from '@auth0/angular-jwt/src/jwthelper.service';
import { loginsecurity } from '../../Shared/Enums/enums';
import { rendererTypeName } from '@angular/compiler';



//declare const jsPDF: any;

interface jsPDFWithPlugin extends jsPDF {
  autoTable: (options: UserOptions) => jsPDF;
}
@Component({
  selector: 'app-primary-user',
  templateUrl: './primary-user.component.html',
  styleUrls: ['./primary-user.component.css']
})

export class PrimaryUserComponent implements OnInit {
  editProfileForm: FormGroup;
  public adminDashboardVm: AdminDashboardVm = new AdminDashboardVm();
  public getAllaJobsCount: GetAllaJobsCount = new GetAllaJobsCount();
  public primaryUserList: PrimaryUserVM[] = [];
  public cloneList: PrimaryUserVM[] = [];
  public businessProfile: SpBusinessProfileVM = new SpBusinessProfileVM;
  public profile: Customer = new Customer();
  public businessDetailUpdate: BusinessDetailUpdate = new BusinessDetailUpdate();
  public personalDetailsUpdate: PersonalDetailsUpdate = new PersonalDetailsUpdate();
  public response: ResponseVm = new ResponseVm();
  public basicRegistrationVm: BasicRegistration = new BasicRegistration();
  public checkEmailEvailabilityVm: CheckEmailandPhoneNumberAvailability = new CheckEmailandPhoneNumberAvailability();

  public pageNumber: number = 1;
  public totalJobs: number;
  public totalPages: number;
  public status: boolean = true;
  public firstPageactive: boolean;
  public customerjobspageNumber: number = 1;
  public customernotificationpageNumber: number = 1;
  public lastPageactive: boolean;
  public previousPageactive: boolean;
  public nextPageactive: boolean;
  public allowview;
  public noOfPagesCustomerJobs;
  public noOfPagesCustomerNotification;
  public allowCustomerEdit;
  public pageing1 = [];
  public recoardNoFrom = 0;
  public customerIdforpagination;
  public recoardNoTo = 50;
  public totalRecoards = 0;
  public noOfPages;
  public dataOrderBy = "DESC";
  public quotationId;
  public listOfIds: Array<number> = [];
  public notificationList = [];
  public userId: number;
  public mobileNumber: number;
  public mobileNumberupdate: number;
  public role: string;
  public customerIdFilter = "";
  public BirthDate: Date;

  public pipe;
  public searchBy = 1;
  public startDate;
  public endDate;
  public startDate1="";
  public endDate1="";
  public location = "";
  public customerName = "";
  public cityList = [];
  public selectedCities = [];
  public selectedCity = [];
  public selectedColumn = [];
  public dropdownListForCity = {};


  public customerpagercoards;
  public customernotificationpagercoards;
  public isAddress = false;
  public searchedAddress = [];
  public cnic = "";
  public mobile = "";
  public sprights = 'false';
  public dataNotFound: boolean;
  public CustomerNotificationShow: boolean;
  public usertype = 3;
  public CustomerJobsShow: boolean = false;
  public excelFileList = [];
  public salesmanList = [];
  public salesmanList1 = [];
  public selecteddatasingle = [];
  public emailtype = 1;
  public mobileType = 1;
  public jobsType = 1;
  public customerjobspageing = [];
  public customernotificationpageing = [];
  public formSubmiited = false;
  public SourceOfReg1: number;
  public email = '';
  public emailupdate = '';
  public customertotalRecoards = 101;
  public customernotificationtotalRecoards = 101;
  public id: number;
  public selectedsalesman = [];
  public pdfrequest: boolean = true;
  public submited: boolean = false;
  public securityRecord: any;
  public customerrecoardNoFrom = 0;
  public customerrecoardNoTo = 50;
  public customernotificationrecoardNoFrom = 0;
  public customernotificationrecoardNoTo = 50;
  public securityRoleId: number;
  public customerjobstatus: boolean = true;
  public customernotificationtatus: boolean = true;
  public customernotificationstatus: boolean = true;
  public customerjobsfirstPageactive: boolean;
  public customernotificationfirstPageactive: boolean;
  public customerjobslastPageactive: boolean;
  public customernotificationlastPageactive: boolean;
  public customerjobspreviousPageactive: boolean;
  public customernotificationpreviousPageactive: boolean;
  public customerjobsnextPageactive: boolean;
  public customernotificationnextPageactive: boolean;
  public customerId: any;
  public customerjobstotalPages: number;
  public customernotificationtotalPages: number;
  public customerjobsdataOrderBy = "DESC";
  public newgetLeadgerList = [];
  public compaignsList = [];
  public customerJobsList = [];
  public decodedtoken: any;
  public skillList = [];
  public SelectedSkillsList = [];
  public selectedSkills = [];
  public skillsdropdownSettings = {};
  public salesmandropdownSettings = {};
  public tielsList = [];
  public getLeadgerList = [];
  public appValForm: FormGroup;
  public pkgForm: FormGroup;
  public analytic: IAnalyticsModal;
  public idValueVm: IdValueVm[] = [];
  public customerRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };

  jwtHelperService: JwtHelperService = new JwtHelperService();
  @ViewChild('postJobModalConfirm', { static: true }) postJobModal: ModalDirective;
  @ViewChild('UserDetailsModal', { static: true }) UserDetailsModal: ModalDirective;
  @ViewChild('checkParent', { static: true }) checkParent: ElementRef;
  @ViewChild('dataTable', { static: true }) dataTable: ElementRef;
  @ViewChild('addNewSupplier') addNewSupplier: ModalDirective;
  public customerjobsdata: GetCustomerJobsCount = new GetCustomerJobsCount();
  constructor(public dynamicScripts: DynamicScriptLoaderService,
    public excelService: ExcelFileService, public _modalService: NgbModal, public toastr: ToastrService, public service: CommonService,
    public Loader: NgxSpinnerService, public sortList: SortList, private router: Router, private fb: FormBuilder, private modalService: NgbModal) {
    this.getAllaJobsCount.pageSize = 50;
    this.customerpagercoards = 5;
    this.customernotificationpagercoards = 5;
    this.populateData();
    this.GetUsers();
    this.populateListData();
  }
  ngOnInit() {

    this.customerRole = JSON.parse(localStorage.getItem("Customer"));
    if (!this.customerRole.allowView)
      this.router.navigateByUrl('/login');
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    this.getCityList();
    this.BirthDate = new Date();
    this.editProfileForm = this.fb.group({
      firstName: [''],
      lastname: [''],
      username: [''],
      email: ['']
    });

    this.pkgForm = this.fb.group({
      salesmanId: [null, Validators.required],
      campaignId: [null, Validators.required],
    })
    this.appValForm = this.fb.group({
      dateOfBirth: ['', Validators.required],
      firstName: ['', [Validators.required, Validators.minLength(1)]],
      lastName: ['', [Validators.required, Validators.minLength(1)]],
      gender: [null, Validators.required],
      mobileNumber: ['', [Validators.required, Validators.pattern("^[0-9]{4}[0-9]{7}$")]],
      email: ['', [Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]],
      cnic: ['', [Validators.pattern("^[0-9]{5}[0-9]{7}[0-9]$")]],
      customerId: [''],
      userId: [''],
      cityId: [null, [Validators.required]]
    });
    this.usertype = 3;
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

    this.skillsdropdownSettings = {
      singleSelection: true,
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
    this.selectedsalesman = [];
    this.submited = false;
    this.CustomerNotificationShow = false;
    this.pdfrequest = true;
    this.populateSkills();
    this.getPromotionTypeList();
    this.getAllCities();
    this.SourceOfReg1 = 0;
  }
  public getCompaignsList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.UserManagement.GetCompaignsList).subscribe(res => {
      this.compaignsList = res.json().filter(x=> x.isActive);
      this.Loader.hide();
    })
  }
  populateListData() {
    this.getCompaignsList();
    this.formSubmiited = true;
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
      pageSize: this.getAllaJobsCount.pageSize,
      dataOrderBy: this.dataOrderBy,
      userName: customerpra,
      customerId: this.customerIdFilter,
      startDate: this.startDate1,
      endDate: this.endDate1,
      city: cityIds.toString(),
      skills: skillIds.toString(),
      cnic: this.cnic,
      //id: this.id,
      mobile: this.mobile,
      usertype: this.usertype.toString(),
      emailtype: this.emailtype.toString(),
      mobileType: this.mobileType.toString(),
      jobsType: this.jobsType.toString(),
      location: this.location,
      sourceOfReg: this.SourceOfReg1.toString(),
      email: this.email,
      SalesmanId: salesmandata
    };
    console.log(obj);
    this.Loader.show();
    this.service.post(this.service.apiRoutes.Customers.SpGetPrimaryUsersList, obj).subscribe(result => {
      if (result.json() != null) {
        this.primaryUserList = result.json();
        if (this.selecteddatasingle.length > 0) {
          for (var x = 0; x < this.primaryUserList.length; x++) {
            this.primaryUserList[x].isselectedforexport = false;
            var xx = this.selecteddatasingle.find(z => z.CustomerId == this.primaryUserList[x].customerId)
            if (xx != null && xx != undefined && xx != "")
              this.primaryUserList[x].isselectedforexport = true;
          }
        }
        else {
          for (var x = 0; x < this.primaryUserList.length; x++) {
            this.primaryUserList[x].isselectedforexport = false;
          }
        }
        this.cloneList = this.primaryUserList;
        this.excelFileList = this.primaryUserList;
        for (var i = 0; i < this.primaryUserList.length; i++) {
          this.listOfIds.push(this.primaryUserList[i].customerId);
        }
        this.totalRecoards = this.primaryUserList[0].noOfRecoards;
        this.noOfPages = this.primaryUserList[0].noOfRecoards / this.getAllaJobsCount.pageSize;
        this.noOfPages = Math.floor(this.noOfPages);
        if (this.primaryUserList[0].noOfRecoards > this.noOfPages) {
          this.noOfPages = this.noOfPages + 1;
        }
        this.pageing1 = [];
        for (var x = 1; x <= this.noOfPages; x++) {
          this.pageing1.push(
            x
          );
        }
        this.recoardNoFrom = (this.getAllaJobsCount.pageSize * this.pageNumber) - this.getAllaJobsCount.pageSize + 1;
        this.recoardNoTo = (this.getAllaJobsCount.pageSize * this.pageNumber);
        if (this.recoardNoTo > this.primaryUserList[0].noOfRecoards)
          this.recoardNoTo = this.primaryUserList[0].noOfRecoards;
        this.dataNotFound = true;
        this.Loader.hide();
      }
      else {
        this.recoardNoFrom = 0;
        this.recoardNoTo = 0;
        this.noOfPages = 0;
        this.totalRecoards = 0;
        this.primaryUserList = [];
        this.cloneList = [];
        this.toastr.error("No record found !", "Search")
        this.dataNotFound = false;
        this.Loader.hide();
      }
      this.Loader.hide();
    },
      error => {
        console.log(error);
        this.Loader.hide();
      });
  }
  public getCityList() {
    this.service.get(this.service.apiRoutes.Common.GetCityList).subscribe(result => {
      this.idValueVm = result.json();
    });
  }
  handleChange(event, userId) {

    let status = event.target.checked;
    let customerId = event.target.id;
    this.service.get(this.service.apiRoutes.Customers.BlockCustomer + "?customerId=" + customerId + "&userId=" + userId + "&status=" + status).subscribe(result => {
      let response = result.json();
      if (response.status == 200) {
        if (status) {
          this.toastr.success("Cutomer Unblocked successfully!", "Success");
        }
        else {
          this.toastr.warning("Customer blocked successfully!", "Warning");
        }
        this.populateListData();
      }
    })
  }
  get f() {
    return this.appValForm.controls;
  }
  get f1() {
    return this.pkgForm.controls;
  }

  public showAddCustomerModal(modalName) {
    this._modalService.open(modalName, { size:'lg' });
  }
  public showModalAdd(userid, id, email, customerMobile, ModalName,) {
    ;
    this.emailupdate = email;
    this.mobileNumberupdate = customerMobile;
    this._modalService.open(ModalName, { size: 'lg' });
    this.PopulateDatauser(userid, id);
  }

  resetForm() {
    this.pageNumber = 1,
      this.getAllaJobsCount.pageSize = 50,
      this.location = "";
    this.endDate = null;
    this.startDate = null;
    this.customerName = "";
    this.customerIdFilter = "";
    this.selectedCities = []
    this.selectedCity = [];
    this.selectedsalesman = [];
    this.mobile = "";
    this.cnic = "";
    this.id = 0;
    this.SourceOfReg1 = 0;
    const parent = this.checkParent.nativeElement
    parent.querySelector("#start").checked = true;
    parent.querySelector("#all").checked = true;
    parent.querySelector("#all1").checked = true;
    parent.querySelector("#allMobileUsers").checked = true;
    parent.querySelector("#allMobileUsers1").checked = true;

    this.jobsType = 1;
    this.usertype = 3;
    this.mobileType = 1;
    this.emailtype = 1;
    this.populateListData();
  }
  setAddressValue(e) {
    this.location = e.target.text;
    this.isAddress = false;
  }
  getRadioValue(e) {
    this.searchBy = e.target.value;
  }
  getRadioValueut(e) {
    this.usertype = e.target.value;
  }
  getRadioValueemail(e) {
    this.emailtype = e.target.value;
  }
  getRadioValueMobileNo(e) {
    this.mobileType = e.target.value;
  }
  getRadioValuejobstype(e) {
    ;
    this.jobsType = e.target.value;
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
      title: 'CustomersCSV',
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
      this.excelService.exportAsExcelFile(this.excelFileList, "customerReport")
    else
      this.excelService.exportAsExcelFile(this.selecteddatasingle, "customerReport")
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
  showModel1(id: any, content1) {
    ;
    this.customerId = id;
    this._modalService.open(content1)
  }
  confrimCreateTest() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Login.changeusertype + "?userid=" + this.customerId).subscribe(result => {
      let response = result.json();
      if (response.status == 200) {
        this.populateListData();
        this.toastr.success("User type change successfully!", "Success");
        this.Loader.hide();
      }
      else {
        alert('eee');
        this.Loader.hide();
      }
    })
  }

  openModal(UserDetailsModal, user) {
    this.modalService.open(UserDetailsModal, {
      centered: true,
      backdrop: 'static'
    });
    this.editProfileForm.patchValue({
    });
  }

  GetUsers() {
    this.service.get(this.service.apiRoutes.Customers.SpGetAdminDashBoard).subscribe(result => {
      this.adminDashboardVm = result.json();
      this.totalJobs = this.adminDashboardVm.registeredCustomersCount;
      this.NumberOfPages();
    },
      error => {
        this.Loader.hide();
        console.log(error);
        alert(error);
      });
  }
  getNotification() {
    this.customernotificationpagercoards = this.customerjobsdata.notificationpageSize;
    this.service.get(this.service.apiRoutes.Customers.GetCustomerById + "?customerId=" + this.customerIdforpagination).subscribe(result => {
      var data = result.json();
      if (data != null) {
        this.service.get(this.service.apiRoutes.Notifications.GetAdminNotifications + "?pageSize=" + this.customernotificationpagercoards + "&pageNumber=" + this.customernotificationpageNumber + "&userId=" + data.userId).subscribe(result => {
          var res = result.json();
          this.Loader.hide();
          this.notificationList = res;
          debugger
          if (this.notificationList.length>0) {
            this.CustomerNotificationShow = true;
            this.noOfPagesCustomerNotification = this.notificationList.length / this.customerjobsdata.notificationpageSize
            this.noOfPagesCustomerNotification = Math.floor(this.noOfPagesCustomerNotification);
            if (this.notificationList.length > this.noOfPagesCustomerNotification) {
              this.noOfPagesCustomerNotification = this.noOfPagesCustomerNotification + 1;
            }
            this.customernotificationtotalRecoards = this.notificationList.length;
            this.customernotificationpageing = [];
            for (var x = 1; x <= this.noOfPagesCustomerNotification; x++) {
              this.customernotificationpageing.push(
                x
              );
            }
            this.customernotificationrecoardNoFrom = (this.customerjobsdata.notificationpageSize * this.customernotificationpageNumber) - this.customerjobsdata.notificationpageSize + 1;
            this.customernotificationrecoardNoTo = (this.customerjobsdata.notificationpageSize * this.customernotificationpageNumber);
            if (this.customernotificationrecoardNoTo > this.notificationList.length)
              this.customernotificationrecoardNoTo = this.notificationList.length;
          }
          else {
            this.CustomerNotificationShow = false;
            this.customernotificationrecoardNoFrom = 0;
            this.customernotificationrecoardNoTo = 0;
            this.customernotificationpageNumber = 0;
            this.customernotificationtotalRecoards = 0;
            this.notificationList = [];
          }

        })
      }
    })
  }
  populateData() {
    this.getAllaJobsCount.pageSize = 50;
    this.service.get(this.service.apiRoutes.Jobs.GetAllJobsCount).subscribe(result => {
      this.getAllaJobsCount = result.json();
      this.getAllaJobsCount.pageSize = 50;
    },
      error => {
        console.log(error);
        this.Loader.hide();
      });
  }
  onCheckboxChange(e, obj) {
    if (e.target.checked) {
      this.selecteddatasingle.push({
        'CustomerId': obj.customerId,
        'CustomerName': obj.customerName,
        'Mobile': obj.customerMobile,
        'Address': obj.customerAddress,
        'jobsPosted': obj.jobsPosted,
        'cnic': obj.customerCNIC,
        'CreatedOn': obj.createdOn,
        'IsTestUser': obj.isTestUser,
        'Id': obj.id,
      });
    }
    else {
      this.selecteddatasingle = this.selecteddatasingle.filter(item => item.CustomerId != e.target.value);
    }
  }
  public SavelinkedSaleman() {
    if (this.pkgForm.invalid) {
      this.submited = true;
      return;
    }
    if (this.selecteddatasingle.length > 0) {
      var cusId = '';
      for (var x = 0; x < this.selecteddatasingle.length; x++) {
        cusId += this.selecteddatasingle[x].CustomerId + ',';
      }
      var data = this.pkgForm.value;
      data.customerId = cusId;
      data.createdBy = this.decodedtoken.UserId;
      this.Loader.show();
      this.service.post(this.service.apiRoutes.Customers.AddLinkedSalesman, data).subscribe(result => {
        let res = result.json();
        if (res.status == httpStatus.Ok) {
          this.Loader.hide();
          this.submited = false;
          this.toastr.success(res.message, "Success");
          this._modalService.dismissAll();
          this.selecteddatasingle = [];
          this.populateListData();
        }
      })
      this.Loader.hide();
    }
  }

  public SalesmanConfirm(contentsales) {
    this.GetSalesmanlist(contentsales);
  }
  public GetSalesmanlist(contentsales) {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.UserManagement.getAllSalesman).subscribe(result => {

      this.salesmanList = result.json();
      this.salesmanList = this.salesmanList1.filter(x => x.isActive == true);
      this._modalService.open(contentsales).result.then(() => {
        this.pkgForm.reset();
      });
      this.Loader.hide();
    })
  }

  customerDetail(customerId, type) {

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
    this.populateListData();
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
  SelectedCustomerNotificationPageData(page) {
    this.customernotificationpageNumber = page;
    this.customernotificationstatus = true;
    this.customernotificationfirstPageactive = false;
    this.customernotificationpreviousPageactive = false;
    this.customernotificationnextPageactive = false;
    this.customernotificationlastPageactive = false;
    this.getNotification();
  }
  NumberOfPages() {
    this.totalPages = Math.ceil(this.totalJobs / this.getAllaJobsCount.pageSize);
  }
  CustomerJobsNumberOfPages() {
    this.customerjobstotalPages = Math.ceil(this.customerjobstotalPages / this.customerjobsdata.pageSize);
  }
  CustomerNotificationNumberOfPages() {
    this.customernotificationtotalPages = Math.ceil(this.customernotificationtotalPages / this.customerjobsdata.notificationpageSize);
  }
  changePageSize() {
    this.pageNumber = 1;
    this.NumberOfPages();
    this.populateListData();
  }
  customerjobschangePageSize() {
    this.customerjobspageNumber = 1;
    this.CustomerJobsNumberOfPages();
    this.GetCustomerJobsList();
  }
  customernotificationchangePageSize() {
    this.customernotificationpageNumber = 1;
    this.CustomerNotificationNumberOfPages();
    this.getNotification();
  }
  lastPage() {
    this.status = false;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = true;
    this.pageNumber = this.totalPages;
    this.populateListData();

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
  customernotificationlastPage() {
    this.customernotificationtatus = false;
    this.customerjobsfirstPageactive = false;
    this.customernotificationpreviousPageactive = false;
    this.customernotificationnextPageactive = false;
    this.customernotificationlastPageactive = true;
    this.customernotificationpageNumber = this.totalPages;
    this.getNotification();
  }
  FirstPage() {
    this.status = false;
    this.firstPageactive = true;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = false;
    this.pageNumber = 1;
    this.populateListData();
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
  CustomerNotificationFirstPage() {
    this.customernotificationtatus = false;
    this.customerjobsfirstPageactive = true;
    this.customernotificationpreviousPageactive = false;
    this.customernotificationnextPageactive = false;
    this.customernotificationlastPageactive = false;
    this.customernotificationpageNumber = this.totalPages;
    this.getNotification();
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
    this.populateListData();
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
  customernotificationnextPage() {
    this.customernotificationtatus = false;
    this.customerjobsfirstPageactive = false;
    this.customernotificationpreviousPageactive = false;
    this.customernotificationnextPageactive = true;
    this.customernotificationlastPageactive = false;
    if (this.customernotificationtotalPages > this.customernotificationpageNumber) {
      this.customernotificationpageNumber++;
    }
    this.getNotification();
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
    this.populateListData();
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
  customernotificationpreviousPage() {
    this.customernotificationtatus = false;
    this.customerjobsfirstPageactive = false;
    this.customernotificationpreviousPageactive = true;
    this.customernotificationnextPageactive = false;
    this.customernotificationlastPageactive = false;
    if (this.customernotificationpageNumber > 1) {
      this.customernotificationpageNumber--;
    }
    this.getNotification();
  }
  Filter(value: any) {
    if (!value) {

      this.cloneList = this.primaryUserList;
    }
    else {
      this.cloneList = this.primaryUserList.filter(x => x.customerId.toString().includes(value) || x.customerMobile.includes(value) || x.customerName.toLowerCase().includes(value.toString().toLowerCase()));
    }
  }
  clickchange() {
    this.populateListData();
  }
  customerjobsclickchange() {
    this.GetCustomerJobsList();
  }
  customernotificationclickchange() {
                   
    this.getNotification();
  }
  PageSizeChange() {
    this.pageNumber = 1;
    this.populateListData();
  }
  customerjobsPageSizeChange() {

    this.customerjobspageNumber = 1;
    this.GetCustomerJobsList();
  }
  customernotificationPageSizeChange() {
                   
    this.customernotificationpageNumber = 1;
    this.getNotification();
  }
  PriviousClick() {
    if (this.pageNumber > 1) {
      this.pageNumber = parseInt(this.pageNumber.toString()) - 1;
      this.populateListData();
    }

  }
  NextClick() {
    if (this.pageNumber < this.noOfPages) {
      this.pageNumber = parseInt(this.pageNumber.toString()) + 1;
      this.populateListData();
    }

  }
  customerPriviousClick() {
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
  customernotificationPriviousClick() {
                   
    if (this.customernotificationpageNumber > 1) {
      this.customernotificationpageNumber = parseInt(this.customernotificationpageNumber.toString()) - 1;
      this.getNotification();
    }
  }
  customernotificationNextClick() {
    debugger
    if (this.customernotificationpageNumber < this.noOfPagesCustomerNotification && this.notificationList.length != 5) {
      this.customernotificationpageNumber = parseInt(this.customernotificationpageNumber.toString()) + 1;
      this.getNotification();
    }
  }
  LoadNewestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "DESC";
    this.populateListData();

  }
  LoadOldestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "ASC";
    this.populateListData();

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
  hidemodal() {
    this.UserDetailsModal.hide();
    this.quotationId = "";
  }
  deletejobconfirm() {
    if ((this.totalRecoards % this.getAllaJobsCount.pageSize) == 1) {
      if (this.pageNumber > 1)
        this.pageNumber = parseInt(this.pageNumber.toString()) - 1;
    }

    this.service.get(this.service.apiRoutes.Jobs.deleteJobWithJobQuotationId + "?jobQuotationId=" + this.quotationId).subscribe(result => {

      if (result.status = 200) {
        this.populateListData();
        this.quotationId = "";
        this.postJobModal.hide();
      }
    });
  }

  public getLeadgerReport(customerId) {
    debugger;
    let obj = {
      customerId: customerId,
      //customerId: 341215,
      tradesmanId: 0,
      jobQuotationId: 0,
      jobDetailsId:0
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


  GetBussinessProfile(CustomerId, profileModal: any) {
    this.service.get(this.service.apiRoutes.Users.getBussinessProfile + "?userId=" + CustomerId + "&role=" + 'Customer').subscribe(result => {
      this.businessProfile = null;
      this.businessProfile = result.json();
      this._modalService.open(profileModal, { size: 'xl'});
      if (this.businessProfile.tradeName != null) {
        this.businessProfile.tradeName = this.businessProfile.tradeName.split("&amp;").join(" ");
      }
      this.customerjobsdata.pageSize = this.customerpagercoards;
      this.customerjobsdata.notificationpageSize = this.customernotificationpagercoards;
      this.customerIdforpagination = CustomerId;
      this.GetCustomerJobsList();
      this.getLeadgerReport(CustomerId);
      this.getNotification();
      this.Loader.hide();

    },
      error => {
        console.log(error);
        alert(error);
        this.Loader.hide();
      });
  }



  GetCustomerJobsList() {
    this.Loader.show();

    let obj = {
      pageNumber: this.customerjobspageNumber,
      pageSize: this.customerjobsdata.pageSize,
      dataOrderBy: this.dataOrderBy,
      customerId: this.customerIdforpagination
    };

    this.service.post(this.service.apiRoutes.Jobs.SpGetJobsByCustomerId, obj).subscribe(result => {
      //  this.customerJobsList = null;
      this.customerJobsList = result.json();
      this.Loader.hide();
      if (this.customerJobsList!=null) {
        debugger
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
    })
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

  public populateSkills() {
    this.service.get(this.service.apiRoutes.TrdesMan.GetTradesManSkills).subscribe(result => {
      this.skillList = result.json();
    },
      error => {
        console.log(error);
        this.Loader.hide();
      });
  }



  public PopulateDatauser(userid, id) {

    this.service.get(this.service.apiRoutes.Customers.CustomerProfile + "?customerId=" + userid).subscribe(result => {
      if (status = httpStatus.Ok) {
        this.profile = result.json();
        this.appValForm.patchValue(this.profile);
        this.BirthDate = this.profile.dateOfBirth;
        this.appValForm.controls.customerId.setValue(this.profile.entityId);
      }
    }, error => {
      console.log(error);
    });
  }

  public submitDetails() {
    
    if (this.appValForm.valid) {

      var data = this.appValForm.value;
      this.basicRegistrationVm.password = "P@ss" + 1 + 2 + 3 + 4 + 5;
      this.basicRegistrationVm.googleUserId = "";
      this.basicRegistrationVm.facebookUserId = "";
      this.checkEmailEvailabilityVm.role = 'Customer';
      this.checkEmailEvailabilityVm.googleUserId = this.basicRegistrationVm.googleUserId;
      this.checkEmailEvailabilityVm.facebookUserId = this.basicRegistrationVm.facebookUserId;
      this.checkEmailEvailabilityVm.password = this.basicRegistrationVm.password;

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
    this.personalDetailsUpdate.UserId = data.userId;
    this.personalDetailsUpdate.EntityId = data.customerId;
    this.personalDetailsUpdate.UserRole = "Customer";
    this.personalDetailsUpdate.FirstName = data.firstName;
    this.personalDetailsUpdate.LastName = data.lastName;
    this.personalDetailsUpdate.MobileNumber = data.mobileNumber;
    this.personalDetailsUpdate.Gender = data.gender;
    this.personalDetailsUpdate.Email = data.email;
    this.personalDetailsUpdate.Cnic = data.cnic;
    this.personalDetailsUpdate.DateOfBirth = data.dateOfBirth;
    this.personalDetailsUpdate.cityId = data.cityId;
    this.service.Loader.show();
    this.service.post(this.service.apiRoutes.Supplier.UpdatePersonalDetails, this.personalDetailsUpdate).subscribe(result => {
      if (status = httpStatus.Ok) {
        this.toastr.success("Customer Profile  Updated.", "Successfully");
        this._modalService.dismissAll();
        this.service.Loader.hide();
        this.appValForm.reset();
        this.populateListData();
        this.addNewSupplier.hide();
      }
    }, error => {
      console.log(error);
      this.toastr.error("Some thing went wrong.");
    });
  }
  VerifyAndSendOtp() {

    if (this.appValForm.value.role == null || this.appValForm.value.role == "")
      this.appValForm.value.role = "Customer"


    var data = this.appValForm.value;
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
    this.basicRegistrationVm.city = data.cityId;
    this.basicRegistrationVm.cnic = data.cnic;
    this.basicRegistrationVm.phoneNumber = data.mobileNumber;
    this.basicRegistrationVm.emailAddress = data.email;
    this.basicRegistrationVm.role = data.role;
    this.basicRegistrationVm.dateOfBirth = data.dateOfBirth;
    this.basicRegistrationVm.gender = data.gender;

    this.service.PostData(this.service.apiRoutes.registration.CheckEmailandPhoneNumberAvailability, this.checkEmailEvailabilityVm, true).then(result => {

      this.response = result.json();

      if (this.response.status == httpStatus.Ok) {

        this.service.PostData(this.service.apiRoutes.registration.verifyOtpAndRegisterUser, this.basicRegistrationVm, true).then(result => {

          this.response = result.json();
          if (this.response.status == httpStatus.Ok) {

            this._modalService.dismissAll();
            this.toastr.success("Customer Profile  Inserted.", "Successfully");
            this.service.Loader.hide();
            this.appValForm.reset();
            this.populateListData();
            this.addNewSupplier.hide();

          }
        }, error => {
          console.log(error);
          this.toastr.error("Some thing went wrong.");
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

  closeModal() {
    this.appValForm.reset();
    this._modalService.dismissAll();
  }
  public getPromotionTypeList() {
    this.service.get(this.service.apiRoutes.UserManagement.getAllSalesman).subscribe(result => {
      this.salesmanList1 = result.json();
      this.salesmanList1 = this.salesmanList1.filter(x => x.isActive == true);

    })
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
      this.toastr.error("Before delete please make sure that selected Customers as testcustomer", "Error");
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
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    let formData = {
      userId: userid,
      deletedBy: this.decodedtoken.UserId
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


  public showAnalyticsModal(userId, modalName) {
    this._modalService.open(modalName, { size: 'xl' });
    this.populateAnalyticsData(userId);
  }

  public populateAnalyticsData(userId) {
    this.service.get(this.service.apiRoutes.Analytics.UserAnalytics + "?userId=" + userId + "&userRole=" + loginsecurity.CRole).subscribe(response => {
      this.analytic = response.json();
    }, error => {
    });
  }
  public closeProfileModal() {
    this._modalService.dismissAll();
  }
}
