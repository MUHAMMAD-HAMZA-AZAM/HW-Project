import { Component, OnInit, ViewChild, ElementRef, TemplateRef } from '@angular/core';
import { CommonService } from 'src/app/Shared/HttpClient/_http';
import { SpSupplierStatsVM, SpSupplierListVM, BasicRegistration, CheckEmailandPhoneNumberAvailability, IdValueVm } from 'src/app/Shared/Models/UserModel/SpSupplierVM';
import { NgxSpinnerService } from "ngx-spinner";
import { SortList } from 'src/app/Shared/Sorting/sortList';
import { Router } from '@angular/router';
import { ModalDirective } from 'ngx-bootstrap/modal/modal.directive';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SpBusinessProfileVM, SupplierProfile, BusinessDetailUpdate, PersonalDetailsUpdate, IAnalyticsModal } from '../../Shared/Models/PrimaryUserModel/PrimaryUserModel';
import { DatePipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { ExcelFileService } from '../../Shared/CommonServices/excel-file.service';
import { DynamicScriptLoaderService } from '../../Shared/CommonServices/dynamicScriptLoaderService';
import { ExportToCsv } from 'export-to-csv';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { httpStatus, SupplierRole } from '../../Shared/Enums/enums';
import { jsPDF } from 'jspdf';
import 'jspdf-autotable';
import { UserOptions } from 'jspdf-autotable';
import { ResponseVm } from '../../Shared/Models/HomeModel/HomeModel';
import { JwtHelperService } from '@auth0/angular-jwt';
import { loginsecurity, IOrders, IOrderItem } from '../../Shared/Enums/enums';
//declare const jsPDF: any;
interface jsPDFWithPlugin extends jsPDF {
  autoTable: (options: UserOptions) => jsPDF;
}
@Component({
  selector: 'app-supplier-local',
  templateUrl: './app-supplier-local.component.html',
  styleUrls: ['./app-supplier-local.component.css']
})
export class AppSupplierLocalComponent implements OnInit {
  public supplierStatsVM: SpSupplierStatsVM = new SpSupplierStatsVM();
  public supplierListVM: SpSupplierListVM[] = [];
  public cloneList: SpSupplierListVM[] = [];
  public listOfIds: Array<number> = [];
  public businessProfile: SpBusinessProfileVM = new SpBusinessProfileVM;
  public profile: SupplierProfile = new SupplierProfile();
  public businessDetailUpdate: BusinessDetailUpdate = new BusinessDetailUpdate();
  public personalDetailsUpdate: PersonalDetailsUpdate = new PersonalDetailsUpdate();
  public fullName: string;
  public supplierProfileDetails: any;
  public orderDetailsBySupplierId = [];
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
  public totalRecoards;
  public noOfPages;
  public dataOrderBy = "DESC";
  public quotationId;
  public allowview;
  public allowEdit;
  public pageing1 = [];
  public userId;
  public role;
  public subRole;
  public BirthDate: string;
  public selectedPrimaryTradeIdName: string;
  public pipe;
  public searchBy = 1;
  public startDate: Date;
  public endDate: Date;
  public startDate1: Date;
  public endDate1: Date;
  public location = "";
  public supplierName = "";
  public cityList = [];
  public selectedCities = [];
  public selectedCity = [];
  public dropdownListForCity = {};
  public supplierId = "";
  public categoryList = [];
  public salesmanList = [];
  public salesmanList1 = [];
  selectedCategories = [];
  supplierdropdownSettings;
  public selectedSupplierType = [];
  public cnic = "";
  public mobile = "";
  public dataNotFound: boolean;
  public isAddress = false;
  public searchedAddress = [];
  public startDateErrorMessage: string;
  public endDateErrorMessage: string;
  public submittedApplicationForm = false;
  public sprights = 'false';
  public usertype = 3;
  public emailtype = 1;
  public mobileType = 1;
  public excelFileList = [];
  public selecteddatasingle = [];
  public SourceOfReg1: number;
  public appValForm: FormGroup;
  public pkgForm: FormGroup;
  public subCategoriesList;
  public SelectedSubCategoriesList = [];
  public selectedItemsSubCategory = [];
  public SubCategoriesdropdownSettings = {};
  public ShowFilter = true;
  public DeliveryRadiousList = [];
  //supplier add
  public response: ResponseVm = new ResponseVm();
  public basicRegistrationVm: BasicRegistration = new BasicRegistration();
  public checkEmailEvailabilityVm: CheckEmailandPhoneNumberAvailability = new CheckEmailandPhoneNumberAvailability();
  public idValueVm: IdValueVm[] = [];
  public firstNameErrorMessage: string;
  public lastNameErrorMessage: string;
  public dataOfBirthErrorMessage: string;
  public passwordErrorMessage: string;
  public genderErrorMessage: string;
  public cityErrorMessage: string;
  public roleErrorMessage: string;
  public emailErrorMessage: string;
  public termsAndConditionErrorMessage: string;
  public userAvailabilty: boolean = false;
  public userAvailabiltyErrorMessage: string;
  public RoleTypeErrorMessage: string;
  public featuredStatus: boolean;
  public disableField: boolean;
  public email = '';
  public pdfrequest: boolean = true;
  public submited: boolean = false;
  public salesmandropdownSettings = {};
  public selectedsalesman = [];
  public securityRecord: any;
  public securityRoleId: number;
  public analytic: IAnalyticsModal;
  public supplierIds: Number;
  public paymentDetailList = [];
  public ordersList: IOrders[] = [];
  public ordersItems: IOrderItem[] = [];
  public ordersByProducts = new Array();
  public totalCommission: number = 0;
  public totalOrderAmount: number = 0;
  public totalPayable: number = 0;
  public totalPromotion: number = 0;
  public walletAmount: number = 0;

  public decodedtoken: any;
  public supplierIdForLinkUpdate: number = 0
  public isAllGoodStatus: boolean;
  public statusType: string;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };

  //Hamza DMT
  jwtHelperService: JwtHelperService = new JwtHelperService();
  @ViewChild('postJobModalConfirm', { static: true }) postJobModal: ModalDirective;
  @ViewChild('supplierdetailsModal', { static: true }) supplierdetailsModal: ModalDirective;
  @ViewChild('addNewSupplier', { static: true }) addNewSupplier: ModalDirective;

  @ViewChild('dataTable', { static: true }) dataTable: ElementRef;
  @ViewChild('checkParent', { static: true }) checkParent: ElementRef;
  @ViewChild('supplierLiveModalConfirm',{ static: true }) supplierLiveModalConfirm: ElementRef;
  constructor(public dynamicScripts: DynamicScriptLoaderService,
    private formBuilder: FormBuilder,
    public excelService: ExcelFileService,
    private _modalService: NgbModal, public service: CommonService, public toastr: ToastrService, public Loader: NgxSpinnerService,
    public sortList: SortList, private router: Router) {
    this.supplierStatsVM.pageSize = 50;
    this.populateSupplierList();
    this.populateData();
    this.role = 'Admin';
    this.subRole = 'Supplier';
  }

  ngOnInit() {
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    this.userRole = JSON.parse(localStorage.getItem("Local Supplier List"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.BirthDate = '';
    this.usertype = 3;
    this.email = ''
    this.supplierdropdownSettings = {
      singleSelection: true,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true,
      closeDropDownOnSelection: true

    };
    this.SubCategoriesdropdownSettings = {
      singleSelection: true,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: this.ShowFilter,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true,
      closeDropDownOnSelection: true
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
      salesmanId: ['', [Validators.required, Validators.minLength(1)]],
    });
    this.appValForm = this.formBuilder.group({

      firstName: ['', [Validators.required, Validators.minLength(1)]],
      lastName: ['', [Validators.required, Validators.minLength(1)]],
      email: ['', [Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]],
      cnic: [''],
      mobileNumber: ['', [Validators.required, Validators.pattern("^[0-9]{4}[0-9]{7}$")]],
      dateOfBirth: ['', [Validators.required]],
      featuredSupplier: [false],
      gender: [0, [Validators.required]],
      primaryTrade: [0],
      companyName: [''],
      primaryTradeId: [0, [Validators.required]],
      tradeName: [''],
      registrationNumber: [''],
      deliveryRadius: ['', [Validators.required]],
      city: [0, [Validators.required]],
      businessAddress: [''],
      SubCategory: [''],
      supplierId: [''],
      userId: [''],
    });
    this.selectedsalesman = [];
    this.submited = false;
    this.pdfrequest = true;
    this.SourceOfReg1 = 0;
    this.getAllCities();
    this.populateCategories();
    this.getPromotionTypeList();
    //  this.getPaymentHistoryDetails();
  }
  get f() {
    return this.appValForm.controls;
  }
  get f1() {
    return this.pkgForm.controls;
  }
  handleChange(event) {

    let status = event.target.checked;
    let userId = event.target.id;
    this.service.get(this.service.apiRoutes.Supplier.BlockSupplier + "?supplierId=" + userId + "&status=" + status).subscribe(result => {
      let response = result.json();
      if (response.status == 200) {
        if (status) {
          this.toastr.success("Supplier Unblocked successfully!", "Success");
        }
        else {
          this.toastr.warning("Supplier blocked successfully!", "Warning");
        }
        this.populateSupplierList();
      }
    })
  }
  resetForm() {

    this.location = "";
    this.endDate = null;
    this.startDate = null;
    this.supplierName = "";
    this.selectedCities = []
    this.selectedCity = [];
    this.selectedCategories = [];
    this.selectedSupplierType = [];
    this.cnic = "";
    this.mobile = "";
    this.email = '';
    this.SourceOfReg1 = 0;
    const parent = this.checkParent.nativeElement
    parent.querySelector("#start").checked = true;
    parent.querySelector("#all").checked = true;
    parent.querySelector("#all1").checked = true;
    parent.querySelector("#allMobileUsers").checked = true;
    this.populateSupplierList();
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
  changeFeaturedValue(e) {

    this.featuredStatus = e.target.value;
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
      this.excelService.exportAsExcelFile(this.excelFileList, "SupplierReport")
    else
      this.excelService.exportAsExcelFile(this.selecteddatasingle, "SupplierReport")
  }
  DownloadPdf() {

    this.pdfrequest = false;

    //if (this.downloadpdf1()) {
    // //this.pdfrequest = true;
    //}
    setTimeout(() => { this.downloadpdf1() }, 1000)
    // this.downloadpdf1();
    setTimeout(() => { this.pdfrequest = true; }, 3000)
  }
  downloadpdf1() {

    const doc = new jsPDF('l', 'px', 'a4') as jsPDFWithPlugin;
    const pdfTable = this.dataTable.nativeElement;
    doc.autoTable({
      styles: { fontSize: 8 },
      html: pdfTable
    });
    doc.save('Supplier.pdf')


  }
  populateData() {

    this.Loader.show();
    this.supplierStatsVM.pageSize = 50;
    this.service.get(this.service.apiRoutes.Users.SpGetSupplierStats).subscribe(result => {

      this.supplierStatsVM = result.json();
      this.supplierStatsVM.pageSize = 50;
      this.totalJobs = this.supplierStatsVM.supplierCount;
      this.NumberOfPages();

      this.Loader.hide();
    },
      error => {
        console.log(error);
        this.Loader.hide();
      });

  }
  getRadioValuesource(e) {
    this.SourceOfReg1 = e.target.value;
  }
  onCheckboxChange(e, obj) {

    if (e.target.checked) {

      this.selecteddatasingle.push({//obj

        'supplierId': obj.supplierId,
        'companyName': obj.companyName,
        'Mobile': obj.mobileNo,
        'CNIC': obj.cNIC,
        'supplierCategory': obj.supplierCategory,
        'supplierAdsCount': obj.supplierAdsCount,
        'Address': obj.supplierAddress,
        'CreatedOn': obj.createdOn,
        'IsTestUser': obj.isTestUser,
        'Id': obj.id,

      });
    } else {
      this.selecteddatasingle = this.selecteddatasingle.filter(item => item.supplierId != e.target.value);

    }
  }
  populateSupplierList() {
    let cityIds = [];
    let categoryIds = [];
    this.selectedCity.forEach(function (item) {
      cityIds.push(item.id);
    });
    this.selectedCategories.forEach(function (item) {
      categoryIds.push(item.id);
    });
    var customerpra = "";
    if (this.supplierName != "" && this.supplierName != undefined) {
      if (this.searchBy == 1) {
        customerpra = this.supplierName + "%";
      }
      else if (this.searchBy == 2) {
        customerpra = "%" + this.supplierName + "%";
      }
      else if (this.searchBy == 3) {
        customerpra = "%" + this.supplierName;
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
      pageSize: this.supplierStatsVM.pageSize,
      dataOrderBy: this.dataOrderBy,
      userName: customerpra,
      startDate: this.startDate1,
      endDate: this.endDate1,
      city: cityIds.toString(),
      categories: categoryIds.toString(),
      cnic: this.cnic,
      mobile: this.mobile,
      usertypeid: this.usertype.toString(),
      emailtype: this.emailtype.toString(),
      mobileType: this.mobileType.toString(),
      location: this.location,
      sourceOfReg: this.SourceOfReg1.toString(),
      email: this.email,
      SalesmanId: salesmandata
    };

    this.Loader.show();

    this.service.post(this.service.apiRoutes.Users.SpGetLocalSupplierList, obj).subscribe(result => {
      if (result.json() != null) {
        this.supplierListVM = result.json();

        //console.log(this.supplierListVM);
        if (this.selecteddatasingle.length > 0) {

          for (var x = 0; x < this.supplierListVM.length; x++) {
            this.supplierListVM[x].isselectedforexport = false;
            var xx = this.selecteddatasingle.find(z => z.supplierId == this.supplierListVM[x].supplierId);
            if (xx != null && xx != undefined && xx != "")
              this.supplierListVM[x].isselectedforexport = true;
          }

        }
        else {
          for (var x = 0; x < this.supplierListVM.length; x++) {
            this.supplierListVM[x].isselectedforexport = false;
          }
        }
        this.excelFileList = this.supplierListVM;
        this.dataNotFound = true;
        this.cloneList = this.supplierListVM;
        this.noOfPages = this.supplierListVM[0].noOfRecoards / this.supplierStatsVM.pageSize
        for (var i = 0; i < this.supplierListVM.length; i++) {
          this.listOfIds.push(this.supplierListVM[i].supplierId);
        }
        this.totalRecoards = this.supplierListVM[0].noOfRecoards;
        this.noOfPages = Math.floor(this.noOfPages);
        if (this.supplierListVM[0].noOfRecoards > this.noOfPages) {
          this.noOfPages = this.noOfPages + 1;
        }

        this.pageing1 = [];
        for (var x = 1; x <= this.noOfPages; x++) {
          this.pageing1.push(
            x
          );
        }
        this.recoardNoFrom = (this.supplierStatsVM.pageSize * this.pageNumber) - this.supplierStatsVM.pageSize + 1;
        this.recoardNoTo = (this.supplierStatsVM.pageSize * this.pageNumber);
        if (this.recoardNoTo > this.supplierListVM[0].noOfRecoards)
          this.recoardNoTo = this.supplierListVM[0].noOfRecoards;
        this.Loader.hide();
      }
      else {
        this.recoardNoFrom = 0;
        this.recoardNoTo = 0;
        this.noOfPages = 0;
        this.totalRecoards = 0;
        this.supplierListVM = [];
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
  }
  getRadioValueut(e) {
    this.usertype = e.target.value;
  }
  showModel1(id: any, content1) {
    this.supplierId = id;
    this._modalService.open(content1)
  }
  confrimCreateTest() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Login.changeusertype + "?userid=" + this.supplierId).subscribe(result => {
      let response = result.json();
      if (response.status == 200) {
        this.populateSupplierList();
        this.toastr.success("User type change successfully!", "Success");
        this.Loader.hide();
      }
      else {

        this.Loader.hide();
      }
    })

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
    this.populateSupplierList();
  }
  NumberOfPages() {

    this.totalPages = Math.ceil(this.totalJobs / this.supplierStatsVM.pageSize);
  }
  changePageSize() {
    this.pageNumber = 1;
    this.NumberOfPages();
    this.populateSupplierList();
  }
  lastPage() {
    this.status = false;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = true;
    this.pageNumber = this.totalPages;
    this.populateSupplierList();

  }
  FirstPage() {
    this.status = false;
    this.firstPageactive = true;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = false;
    this.pageNumber = 1;
    this.populateSupplierList();
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
    this.populateSupplierList();
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
    this.populateSupplierList();
  }
  Filter(value: any) {
    if (!value) {
      this.cloneList = this.supplierListVM;
    }
    else {
      this.cloneList = this.supplierListVM.filter
        (x => x.supplierId.toString().includes(value) || x.mobileNo.includes(value) || x.companyName.toLowerCase().includes(value.toString().toLowerCase())).sort(o => o.supplierId);
    }
  }
  GetReCoards() {
  }
  clickchange() {
    this.populateSupplierList();
  }
  PageSizeChange() {

    this.pageNumber = 1;
    this.populateSupplierList();
  }
  PriviousClick() {
    if (this.pageNumber > 1) {
      this.pageNumber = parseInt(this.pageNumber.toString()) - 1;
      this.populateSupplierList();
    }

  }
  NextClick() {

    if (this.pageNumber < this.noOfPages) {
      this.pageNumber = parseInt(this.pageNumber.toString()) + 1;
      this.populateSupplierList();
    }

  }
  LoadNewestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "DESC";
    this.populateSupplierList();

  }
  LoadOldestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "ASC";
    this.populateSupplierList();

  }
  hidemodal() {

    this.postJobModal.hide();
    this.quotationId = "";
  }
  deletejobconfirm() {
    if ((this.totalRecoards % this.supplierStatsVM.pageSize) == 1) {
      if (this.pageNumber > 1)
        this.pageNumber = this.pageNumber - 1;
    }

    this.service.get(this.service.apiRoutes.Jobs.deleteJobWithJobQuotationId + "?jobQuotationId=" + this.quotationId).subscribe(result => {

      if (result.status = 200) {
        this.populateSupplierList();
        this.quotationId = "";
        this.postJobModal.hide();
      }
    });
  }
  populateCategories() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Supplier.GetCategories).subscribe(result => {

      this.categoryList = result.json();
      console.log(result.json());
      this.Loader.hide();
    },
      error => {
        console.log(error);
        this.Loader.hide();
      });


  }
  onItemSelectAll(items: any) {
    console.log(items);
    this.selectedCategories = items;


  }
  OnItemDeSelectALL(items: any) {
    this.selectedCategories = [];
    console.log(items);
  }
  onItemSelect(item: any) {
    this.selectedCategories.push(item);
    console.log(this.selectedCategories);
  }
  OnItemDeSelect(item: any) {

    this.selectedCategories = this.selectedCategories.filter(
      function (value, index, arr) {
        return value.id != item.id;
      }

    );
    console.log(this.selectedCategories);
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
  public getAllCities() {
    this.service.get(this.service.apiRoutes.Common.GetCityList).subscribe(result => {
      this.cityList = result.json();

    })
  }
  public showModalAdd(userid, id, ModalName) {

    this.disableField = true;
    this._modalService.open(ModalName, { size: 'lg' });
    this.PopulateDataSupplier(userid, id);
  }
  public showModalAdd1(userid, id, ModalName) {
    this.appValForm.reset();
    this.BirthDate = null;
    this._modalService.open(ModalName, { size: 'lg' });
  }
  public onCategorySelect(e) {

    let selectedOptions = e.target['options'];
    let selectedIndex = selectedOptions.selectedIndex;
    var selectElementText = selectedOptions[selectedIndex].text;
    this.businessDetailUpdate.PrimaryTrade = selectElementText;
    console.log(selectElementText);
    var data = this.appValForm.value;
    var productValue = data.primaryTradeId;
    if (productValue > 0) {
      this.service.get(this.service.apiRoutes.Supplier.GetAllProductSubCatagories + "?productId=" + productValue).subscribe(result => {
        this.subCategoriesList = result.json();
        this.selectedItemsSubCategory = [];
      }, error => {
        console.log(error);
      });
    }
  }
  public PopulateDataSupplier(userid, id) {

    this.service.get(this.service.apiRoutes.Supplier.GetBusinessAndPersnalProfileadmin + "?userid=" + userid).subscribe(result => {

      this.profile = result.json();
      console.log(this.profile);
      this.appValForm.patchValue(this.profile.persnalDetails);
      this.appValForm.patchValue(this.profile.businessDetails);
      this.subCategoriesList = this.profile.businessDetails.productsubCategory;
      this.fullName = this.profile.persnalDetails.firstName + " " + this.profile.persnalDetails.lastName;
      if (this.subCategoriesList.length == 0) {
        this.subCategoriesList = this.profile.businessDetails.selectedSubCategory;
      }
      this.SelectedSubCategoriesList = this.profile.businessDetails.selectedSubCategory;

      this.BirthDate = this.profile.persnalDetails.dateOfBirth;
      this.appValForm.controls.SubCategory.setValue(this.profile.businessDetails.selectedSubCategory);
      this.appValForm.controls.userId.setValue(id);
    });
  }
  public submitDetails() {
    if (this.appValForm.valid) {

      var data = this.appValForm.value;
      console.log(data);
      if (data.userId != '' && data.userId != null && data.userId != undefined)
        this.Save();
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
    var productValue = data.SubCategory;
    if (productValue.length) {
      for (var i in productValue) {
        var pd = productValue[i].id;
        this.businessDetailUpdate.ProductIds.push(pd);
      }
    }
    else {
      this.businessDetailUpdate.ProductIds = [];
    }

    this.businessDetailUpdate.SupplierId = data.supplierId;
    this.businessDetailUpdate.CityId = data.city;
    this.businessDetailUpdate.PrimaryTradeId = data.primaryTradeId ? data.primaryTradeId : 0;
    this.businessDetailUpdate.DeliveryRadius = data.deliveryRadius;
    this.businessDetailUpdate.BusinessAddress = data.businessAddress;
    this.businessDetailUpdate.CompanyRegistrationNo = data.registrationNumber;
    this.businessDetailUpdate.FeaturedSupplier = data.featuredSupplier,
      this.businessDetailUpdate.CompanyName = data.companyName;
    this.personalDetailsUpdate.UserId = data.userId;
    this.personalDetailsUpdate.EntityId = data.supplierId;
    this.personalDetailsUpdate.UserRole = "Supplier";
    this.personalDetailsUpdate.FirstName = data.firstName;
    this.personalDetailsUpdate.LastName = data.lastName;
    this.personalDetailsUpdate.MobileNumber = data.mobileNumber;
    this.personalDetailsUpdate.Gender = data.gender;
    this.personalDetailsUpdate.Email = data.email;
    this.personalDetailsUpdate.Cnic = data.cnic;
    this.personalDetailsUpdate.DateOfBirth = data.dateOfBirth;

    this.service.post(this.service.apiRoutes.Supplier.UpdatePersonalDetails, this.personalDetailsUpdate).subscribe(result => {
      if (status = httpStatus.Ok) {
        this.service.post(this.service.apiRoutes.Supplier.AddSupplierBusinessDetails, this.businessDetailUpdate).subscribe(result => {
          if (status = httpStatus.Ok) {
            this.toastr.success("Profile Successfully Updated.");
            this.populateSupplierList();
            this.appValForm.reset();
            this._modalService.dismissAll();
            this.addNewSupplier.hide();
          }
        }, error => {
          console.log(error);
          this.toastr.error("Some thing went wrong.");
        });
      }
    }, error => {
      console.log(error);
      this.toastr.error("Some thing went wrong.");
    });
  }
  VerifyAndSendOtp() {

    if (this.appValForm.value.role == null || this.appValForm.value.role == "")
      this.appValForm.value.role = "Supplier"


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
    this.basicRegistrationVm.city = data.city;
    this.basicRegistrationVm.cnic = data.cnic;
    this.basicRegistrationVm.phoneNumber = data.mobileNumber;
    this.basicRegistrationVm.emailAddress = data.email;
    this.basicRegistrationVm.gender = data.gender;
    this.basicRegistrationVm.role = data.role;

    this.service.PostData(this.service.apiRoutes.registration.CheckEmailandPhoneNumberAvailability, this.checkEmailEvailabilityVm, true).then(result => {

      this.response = result.json();
      if (this.response.status == httpStatus.Ok) {

        this.service.PostData(this.service.apiRoutes.registration.verifyOtpAndRegisterUser, this.basicRegistrationVm, true).then(result => {

          this.response = result.json();
          if (this.response.status == httpStatus.Ok) {
            this.appValForm.controls.supplierId.setValue(this.response.resultData.supplierId);
            this.SendVerificationLink();
          }
          else {
            this.userAvailabilty = true;
            this.userAvailabiltyErrorMessage = this.response.message;
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
  public SendVerificationLink() {

    var test = this.appValForm.value;
    var productValue = test.SubCategory;
    if (productValue.length) {
      for (var i in productValue) {
        var pd = productValue[i].id;
        this.businessDetailUpdate.ProductIds.push(pd);
      }
    }
    else {
      this.businessDetailUpdate.ProductIds = [];
    }

    this.businessDetailUpdate.SupplierId = test.supplierId;
    this.businessDetailUpdate.CityId = test.city;
    this.businessDetailUpdate.PrimaryTradeId = test.primaryTradeId ? test.primaryTradeId : 0;
    this.businessDetailUpdate.DeliveryRadius = test.deliveryRadius;
    this.businessDetailUpdate.BusinessAddress = test.businessAddress;
    this.businessDetailUpdate.CompanyRegistrationNo = test.registrationNumber;
    this.businessDetailUpdate.CompanyName = test.companyName;

    this.service.PostData(this.service.apiRoutes.Supplier.AddSupplierBusinessDetails, this.businessDetailUpdate, true).then(result => {
      this.response = result.json();
      if (this.response.status == httpStatus.Ok) {
        this.toastr.success("Profile Inserted Successfully.");
        this._modalService.dismissAll();
        this.populateSupplierList();
        this.addNewSupplier.hide();
        this.appValForm.reset();
      }
    }, error => {
      console.log(error);
      this.toastr.error("Some thing went wrong.");
    });

  }
  public hide() {
    this.appValForm.reset();
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
        cusId += this.selecteddatasingle[x].supplierId + ',';
      }
      var usertype = "Supplier";
      var data = this.pkgForm.value;
      this.Loader.show();
      this.service.get(this.service.apiRoutes.Supplier.AddLinkedSalesmansupplier + "?SalesmanId=" + data.salesmanId + "&CustomerId=" + cusId).subscribe(result => {

        if (result.ok) {
          this.Loader.hide();
          this.submited = false;
          this.toastr.success("Update Successfully !", "Success");
          this._modalService.dismissAll();
          this.selecteddatasingle = [];
          this.populateSupplierList();

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
  DeleteSelectedUser(contentdeleteuser) {
    this._modalService.open(contentdeleteuser)
  }
  DeleteUsersConfirm() {
    for (var x = 0; x < this.selecteddatasingle.length; x++) {
      this.selecteddatasingle[x].IsTestUser = this.cloneList.find(g => g.id == this.selecteddatasingle[x].Id).isTestUser;
    }
    let obj = this.selecteddatasingle.filter(x => x.IsTestUser != true)
    if (obj.length > 0) {
      this.toastr.error("before delete please make selected supplier as test supplier", "Error");
      return;
    }
    for (var x = 0; x < this.selecteddatasingle.length; x++) {
      if (this.selecteddatasingle[x].IsTestUser) {
        this.deleteUser(this.selecteddatasingle[x].Id)
      }
    }
    //this.toastr.success("User Successfully deleted", "Success");
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
  public showAnalyticsModal(userId, modalName) {

    this._modalService.open(modalName, { size: 'xl' });

    this.populateAnalyticsData(userId);
  }
  public populateAnalyticsData(userId) {

    console.log(userId);

    this.service.get(this.service.apiRoutes.Analytics.UserAnalytics + "?userId=" + userId + "&userRole=" + loginsecurity.SRole).subscribe(response => {
      this.analytic = response.json();

      console.log(this.analytic);

    }, error => {

    });


  }
  //-------------------------- Update Supplier All Good Status 
  public changeStatus(event: Event) {
    this.isAllGoodStatus = (<HTMLInputElement>event.target).checked;
    this.supplierId = (<HTMLInputElement>event.target).value;

    if (this.isAllGoodStatus) {
      this.statusType = "on Live";
      this._modalService.open(this.supplierLiveModalConfirm, { backdrop: 'static', keyboard: false });
    }
    else {
      this.statusType = "off Line";
      this._modalService.open(this.supplierLiveModalConfirm, { backdrop: 'static', keyboard: false });
    }
  }

  public updateAllGoodStatus() {
    //console.log(this.isAllGoodStatus);
    //console.log(this.supplierId);
    this.Loader.show();
    let obj = {
      supplierId: this.supplierId,
      status: this.isAllGoodStatus
    };
    console.log(obj);
    this.service.PostData(this.service.apiRoutes.Supplier.UpdateSupplierAllGoodStatus, JSON.stringify(obj), true).then(res => {
      this.response = res.json();
      if (this.response.status == httpStatus.Ok) {
        this.toastr.success(this.response.message);
        this._modalService.dismissAll();
        this.Loader.hide();
        this.populateSupplierList();
      }
    }, error => {
      console.log(error);
    });
  }
  //Show Supplier Profile   Data
  public BussinessProfile(supplierId: number, ref: any) {
    this.supplierIdForLinkUpdate = supplierId;
    this.supplierIds = supplierId;
    this.service.Loader.show();
    let obj = {
      supplierId: supplierId,
      supplierRole: SupplierRole.localSupplier
    }
    this.service.post(this.service.apiRoutes.Supplier.GetSupplierProfileDetails, JSON.stringify(obj)).subscribe(res => {
      this.response = JSON.parse(res.json());
      console.log(this.response);
      if (this.response.status == httpStatus.Ok) {

        this.supplierProfileDetails = this.response.resultData[0];
        this._modalService.open(ref, { size: 'xl', backdrop: 'static' });
        this.service.Loader.hide();
        //this.GetSupplierOrderDetails();

      }
      this.GetSupplierWallet();

    }, error => {
      this.service.Loader.show();
      console.log(error);
    });
  }
  closeModal() {
    this._modalService.dismissAll();
  }
  ShowDetails(orderId?: number, supplierId?: number, variantModalTemplate?: TemplateRef<any>) {
    this.service.GetData(this.service.apiRoutes.Supplier.GetOrderDetailsById + "?orderId=" + orderId + "&supplierId=" + supplierId, true).then(result => {
      this.ordersItems = [];
      this.ordersByProducts = new Array();
      this.response = result;
      if (this.response.status == httpStatus.Ok) {
        this.ordersItems = this.response.resultData;
        this.ordersItems.forEach(item => {

          if (!this.ordersByProducts.some(x => x.variantId == item.variantId)) {
            let obj = {
              orderId: item.orderId,
              orderFrom: item.orderFrom,
              orderDate: item.orderDate,
              quantity: item.quantity,
              orderStatus: item.orderStatus,
              price: item.price,
              actualAmount: item.actualAmount,
              discountedAmount: item.discountedAmount > 0 ? item.actualAmount - item.discountedAmount : item.actualAmount,
              promotionAmount: item.promotionAmount,
              totalPayable: item.commission > 0 ? item.totalPayable - item.commission : item.totalPayable,
              shippingCost: item.shippingCost,
              productId: item.productId,
              productTitle: item.productTitle,
              variant: item.variant,
              commission: item.commission
            }

            this.ordersByProducts.push(obj)
          }
        });
        this.ordersItems = this.ordersByProducts;
        this._modalService.open(variantModalTemplate, { size: 'lg', centered: true });
      }
    });
  }
  GetSupplierWallet() {
    this.service.get(this.service.apiRoutes.PackagesAndPayments.GetSupplierWallet + "?refSupplierId=" + this.supplierIds).subscribe(res => {
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
  GetSupplierOrderDetails(event: any) {
    this.service.Loader.show();

    let data = {
      supplierId: this.supplierIds,
      toDate: '',
      fromDate: ''
    };
    this.totalCommission = 0;
    this.totalOrderAmount = 0;
    this.totalPayable = 0;
    this.totalPromotion = 0;
    this.service.PostData(this.service.apiRoutes.Supplier.GetSalesSummary, JSON.stringify(data)).then(result => {
      this.response = JSON.parse(result.json());
      if (this.response.status == httpStatus.Ok) {
        this.ordersList = this.response.resultData;
        this.ordersList.forEach(item => {
          this.totalCommission += item.commission;
          this.totalOrderAmount += item.actualAmount;
          this.totalPayable += item.totalPayable;
          this.totalPromotion += item.promotionAmount;
          //  item.totalPayable = item.discount > 0 ? item.totalPayable - item.discount : item.totalPayable
        });
      }
    });
    this.service.Loader.hide();
  }
  /*....................change country status........................*/
  public updateLinkStatus(supplierId: number) {
    let obj = {
      supplierId: supplierId,
      userId: this.decodedtoken.UserId
      /*    this.decodedtoken.UserId*/
    };
    this.Loader.show();

    this.service.PostData(this.service.apiRoutes.Supplier.deleteLinkStatus, JSON.stringify(obj), true).then(res => {
      this.response = res.json();
      if (this.response.status == httpStatus.Ok) {
        this.Loader.hide();
        this.toastr.success(this.response.message, "Success");
        this.closeModal();
      }
    }, error => {
      this.Loader.show();
      console.log(error);
    });

  }
}
