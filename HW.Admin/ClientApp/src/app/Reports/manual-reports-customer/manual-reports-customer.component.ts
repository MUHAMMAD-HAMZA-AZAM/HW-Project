import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { CommonService } from '../../Shared/HttpClient/_http';
import { NgxSpinnerService } from "ngx-spinner";
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReportDatesValidation } from '../../Shared/Enums/enums';
import { ReportDateVm } from '../../Shared/Models/HomeModel/HomeModel';
//import { debug } from 'util';
import { CustomerReportVM } from '../../Shared/Models/UserModel/SpTradesmanVM';
import html2canvas from 'html2canvas';
import { DynamicScriptLoaderService } from '../../Shared/CommonServices/dynamicScriptLoaderService';
import { DatePipe } from '@angular/common';
import { ExcelFileService } from '../../Shared/CommonServices/excel-file.service';
import { ExportToCsv } from 'export-to-csv';
import { ToastrService } from 'ngx-toastr';
import autoTable from 'jspdf-autotable'
import { jsPDF } from 'jspdf';
import 'jspdf-autotable';
import { UserOptions } from 'jspdf-autotable';
import { empty } from 'rxjs';
//declare const jsPDF: any;
interface jsPDFWithPlugin extends jsPDF {
  autoTable: (options: UserOptions) => jsPDF;
}

@Component({
  selector: 'app-manual-reports-customer',
  templateUrl: './manual-reports-customer.component.html',
  styleUrls: ['./manual-reports-customer.component.css']
})
export class ManualReportsCustomerComponent implements OnInit {
  public appValForm: FormGroup;
  public mobile = "";
  public cnic = "";
  public startDate: Date;
  public endDate: Date;
  public fromDate: Date;
  public toDate: Date;
  public selectedItems = [];
  public ctids = [];
  public startDateErrorMessage: string;
  public endDateErrorMessage: string;
  public cityid: "";
  public lastActive = false;
  public location = "";
  public submittedApplicationForm = false;
  public showTable = true;
  public showNullMessage = false;
 // public CustomerList: CustomerReportVM[] = [];
  public CustomerList = [];
  public excelFileList = [];
  public statusList: [];
  public customerdropdownSettings;
  public statusdropdownSettings;
  public completeList: number;
  public completeStatusList: number;
  public selectedCustomers = [];
  public selectedStatus = [];
  public cityList = [];
  public dropdownListForCity = {};
  public dropdownListForColumn = {};
  public selectedCity = [];
  public selectedColumn = [];
  public columnList = [];
  public customerName = "";
  public pipe;
  public customerSearch = 1;
  public customerAddressList: CustomerReportVM[] = [];
  public searchFromAddress = [];
  public searchedAddress = [];
  public isAddress = false;
  public locationSearchKeyMatch = false;
  public reportFilter = false;
  public allowview;
  public emailtype = 1;
  public mobileType = 1;
  public jobsType = 1;
  public userType = 3;

  //jobs
  public jobList = [];
  public jobcount = 0;
  public skipDate: boolean = false;


  @ViewChild('dataTable', { static: true }) dataTable: ElementRef;
  @ViewChild('checkParent', { static: true }) checkParent: ElementRef;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    
  constructor(
    public toastrService: ToastrService,
    public service: CommonService,
    public Loader: NgxSpinnerService,
    private router: Router,
    private formBuilder: FormBuilder,
    public dynamicScripts: DynamicScriptLoaderService,
    public excelService: ExcelFileService
    
  )
  {
    //this.appValForm = this.formBuilder.group({
    //  startDate: [null , Validators.required],
    //  endDate: [null , Validators.required]
    //})
  }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Customer Registration"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.customerdropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };
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
    

    this.selectedCustomers = [];
    this.selectedStatus = [];
    this.selectedCity = [];
    this.selectedColumn = [];
    this.CustomerCustomeReportS();
    this.getAllCities();


    this.columnList = [{ Id: 1, value: "CNIC" },
                       { Id: 2, value: "Last Modified" },
                       { Id: 3, value: "City" },
                       { Id: 4, value: "Town" },
                       { Id: 5, value: "Last Modified" }
    ];
    console.log(this.columnList);
    this.populateCustomerAddress();
    this.loadJobs();
  }
  resetFrom() {
    
    this.location = "";
    this.cnic = "";
    this.mobile = "";
    this.endDate = null;
    this.startDate = null;  
    this.customerName = "";
    this.selectedItems = [];
    this.selectedCity = [];
    this.checkParent.nativeElement.checked = true;
    const parent = this.checkParent.nativeElement
    parent.querySelector("#start").checked = true;
    parent.querySelector("#all").checked = true;
    parent.querySelector("#all1").checked = true;
    parent.querySelector("#all2").checked = true;
    parent.querySelector("#allMobileUsers1").checked = true;


  }
  funReportFilter() {
    this.reportFilter = !this.reportFilter;
  }
  getRadioValueemail(e) {
    this.emailtype = e.target.value;
  }
  getRadioValuemobile(e) {
    this.mobileType = e.target.value;
  }
  getRadioValueut(e) {
    this.userType = e.target.value;
  }
  getRadioValuejobstype(e) {
    this.jobsType = e.target.value;
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
    csvExporter.generateCsv(this.excelFileList);
  }

  DownloadXlsx() {
    this.excelService.exportAsExcelFile(this.excelFileList, "CustomerReport")
  }

  populateCustomerAddress() {
    
    this.service.get(this.service.apiRoutes.Customers.GetCustomerAddressList).subscribe(result => {
      this.customerAddressList = result.json();
      this.customerAddressList.forEach(value => {
        let add = { "customerAddress": value.streetAddress }
        this.searchFromAddress.push(add);
      })
    })
  }
  serachAddress(input) {
    
    if (input.length > 0) {
      this.searchedAddress = [];
      if (this.searchFromAddress.length > 0) {
        this.searchFromAddress.forEach(value => {
          if (value.customerAddress.toLowerCase().includes(input.toLowerCase())) {
            let add = { "customerAddress": value.customerAddress };
            this.searchedAddress.push(add);
          }
          if (this.searchedAddress.length > 0) {
            this.locationSearchKeyMatch = true;
          }
          else {
            this.locationSearchKeyMatch = false;
          }
        })
        this.isAddress = true;
      }
      else {
        this.isAddress = true;
        this.locationSearchKeyMatch = false;

      }
    } else {
      this.searchedAddress = [];
      this.isAddress = false;
    }
  }

  setAddressValue(e) {
    this.location = e.target.text;
    this.isAddress = false;
  }
  getRadioValue(e) {
    this.customerSearch = e.target.value;
  }

  get f() {
    return this.appValForm.controls;
  }

  loadStatusdropdown()
  {
    this.service.get(this.service.apiRoutes.Jobs.GetJobStatusForDropdown).subscribe(result => {
      
      this.statusList = result.json();
      console.log(this.statusList);
      this.completeStatusList = this.statusList.length; 
    },
      error => {
        console.log(error);
        this.Loader.hide();
      });

  }
  CustomerCustomeReportS() {
    //this.submittedApplicationForm = true;
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Users.getAllCustonmersDropdown).subscribe(result => {
      this.CustomerList = result.json();
      this.excelFileList = this.CustomerList.map((customer) => {
        return {
          customerId: customer.customerId,
          firstName: customer.firstName,
          lastName: customer.lastName,
          mobileNumber: customer.mobileNumber,
          cnic: Number(customer.cnic),
          streetAddress: customer.streetAddress,
          cityName: customer.cityName,
          createdOn: new Date(customer.createdOn).toLocaleDateString(),
          lastActive: new Date(customer.lastActive).toLocaleDateString(),
          modifiedOn: new Date(customer.modifiedOn).toLocaleDateString(),
        }
      })
        this.completeList = this.CustomerList.length;
        if (this.CustomerList.length > 0) {
         
          this.loadStatusdropdown();
        }
        else {
         
          this.loadStatusdropdown();
        }
        this.Loader.hide();
      },
        error => {
          console.log(error);
          this.Loader.hide();
          this.loadStatusdropdown();
        });
    
  }
  DownloadPdf() {

    const doc = new jsPDF('l', 'px', 'a4') as jsPDFWithPlugin;
    const pdfTable = this.dataTable.nativeElement;
    doc.autoTable({
      styles: { fontSize: 8 },
      html: pdfTable
    });
    doc.save('table.pdf')
  }
  public loadJobs() {
  var customerpra = "";
  //var data = this.appValForm.value;
  this.jobList = [];
  this.jobcount = 0;
  let cids = [];
  let sids = [];
  let ctids = [];
  //this.selectedCustomers.forEach(function (item) {
  //  cids.push(item.id);
  //});
  this.selectedStatus.forEach(function (item) {
    sids.push(item.statusId);
  });
  this.selectedCity.forEach(function (item) {

    ctids.push(item.id);
  });
  

  if (this.customerName != "" && this.customerName != undefined) {
    if (this.customerSearch == 1) {
      customerpra = this.customerName + "%";
    }
    else if (this.customerSearch == 2) {
      customerpra = "%" + this.customerName + "%";
    }
    else if (this.customerSearch == 3) {
      customerpra = "%" + this.customerName;
    }
    }
    
    
  this.pipe = new DatePipe('en-US');
  this.fromDate = this.pipe.transform(this.startDate, 'MM/dd/yyyy');
    this.toDate = this.pipe.transform(this.endDate, 'MM/dd/yyyy');

    
    let query = ""
    query = this.service.apiRoutes.Customers.getRegitredCustomerDaynamicReport + "?startDate=" + this.fromDate + "&endDate=" + this.toDate + "&customer=" + customerpra + "&status=" + sids + "&city=" + ctids + "&lastActive=" + this.lastActive + "&location=" + this.location + "&mobile=" + this.mobile + "&cnic="
      + this.cnic + "&emailtype=" + this.emailtype + "&mobileType=" + this.mobileType + "&userType=" + this.userType + "&jobsType=" + this.jobsType;
    this.Loader.show();
    
  this.service.get(query).subscribe(result => {
    this.jobList = [];
    this.CustomerList = result.json();
    if (this.CustomerList) {
      this.excelFileList = this.CustomerList.map(customer => {
        return {
          firstName: customer.firstName,
          lastName: customer.lastName,
          //streetAddress: customer.streetAddress,
          cityName: customer.cityName,
          cnic: customer.cnic,
          createdOn: new Date(customer.createdOn).toLocaleDateString(),
          lastActive: new Date(customer.lastActive).toLocaleDateString(),
          mobileNumber: customer.mobileNumber,
          customerId: customer.customerId,
        }
      })
      this.showTable = false;
      this.showNullMessage = false;
      this.jobcount = this.CustomerList.length;
      this.reportFilter = true;
    }
    else {
      this.showNullMessage = false;
      this.showTable = true;
      this.toastrService.error("No record found !", "Search")

    }
    this.Loader.hide();
  },
    error => {
      console.log(error);
      this.Loader.hide();

    });
}

  onCustomerSelectAll(items: any) {
    console.log(items);
    this.selectedCustomers = items;


  }
  onCustomerDeSelectALL(items: any) {
    this.selectedCustomers = [];
    console.log(items);
  }
  onCustomerSelect(item: any) {
    this.selectedCustomers.push(item);
    //console.log(this.selectedCategories);
  }
  onCustomerDeSelect(item: any) {

    this.selectedCustomers = this.selectedCustomers.filter(
      function (value, index, arr) {
        //console.log(value);
        return value.id != item.id;
      }

    );

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
        return value.id != items.id;
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
    this.service.get(this.service.apiRoutes.Common.GetCityList).subscribe(result => {
    this.cityList = result.json();

    })
  }


  numberOnly(event): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }



}

