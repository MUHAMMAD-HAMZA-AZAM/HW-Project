import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { CommonService } from '../../Shared/HttpClient/_http';
import { NgxSpinnerService } from "ngx-spinner";
import { Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SupplierReportVM } from '../../Shared/Models/UserModel/SpTradesmanVM';
import { ReportDatesValidation } from '../../Shared/Enums/enums';
import html2canvas from 'html2canvas';
import { DynamicScriptLoaderService } from '../../Shared/CommonServices/dynamicScriptLoaderService';
import { CategorryCount } from '../../Shared/Models/HomeModel/HomeModel';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { DatePipe } from '@angular/common';
import { ExcelFileService } from '../../Shared/CommonServices/excel-file.service';
import { ExportToCsv } from 'export-to-csv';
import { ToastrService } from 'ngx-toastr';
import { jsPDF } from 'jspdf';
import 'jspdf-autotable';
import { UserOptions } from 'jspdf-autotable';
//declare const jsPDF: any;
interface jsPDFWithPlugin extends jsPDF {
  autoTable: (options: UserOptions) => jsPDF;
}

@Component({
  selector: 'app-manual-reports-supplier',
  templateUrl: './manual-reports-supplier.component.html',
  styleUrls: ['./manual-reports-supplier.component.css']
})
export class ManualReportsSupplierComponent implements OnInit {
  public appValForm: FormGroup;
  public mobile = "";
  public cnic = "";
  public startDate: Date;
  public endDate: Date;
  public fromDate: Date;
  public toDate: Date;
  public startDateErrorMessage: string;
  public endDateErrorMessage: string;
  public submittedApplicationForm = false;
  public showTable = false;
  public showNullMessage = false;
  public SupplierList = [];
  public excelFileList = [];
  public completeList: number;
  public categoryList;
  selectedCategories = [];
  supplierdropdownSettings;
  public skipDate: boolean = false;
  public cityList = [];
  public dropdownListForCity = {};
  public selectedCity = [];
  public supplierName = "";
  public selectedSupplierType = [];
  public selectedCities = [];
  public pipe;
  public lastActive = false;
  public location = "";
  public customerSearch = 1;
  public customerAddressList = [];
  public searchFromAddress = [];
  public searchedAddress = [];
  public isAddress = false;
  public locationSearchKeyMatch = false;
  public reportFilter = false;
  public allowview;
  public emailtype = 1;
  public mobileType = 1;
  public userType = 3;

  @ViewChild('checkParent', { static: true }) checkParent: ElementRef;
  @ViewChild('dataTable', { static: true }) dataTable: ElementRef;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  constructor(public toastrService: ToastrService,public excelService: ExcelFileService , public service: CommonService, public Loader: NgxSpinnerService, private router: Router, private formBuilder: FormBuilder, public dynamicScripts: DynamicScriptLoaderService) { }

  ngOnInit() {

    this.userRole = JSON.parse(localStorage.getItem("Supplier Registration"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
      this.appValForm = this.formBuilder.group({
        supplierSearch: [''],
      });
    this.populateCustomerAddress();
    this.supplierdropdownSettings = {
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
    this.selectedCategories = [];
    this.selectedCity = [];
    this.populateCategories();
    this.getAllCities();
    this.SupplierCustomeReportS();
  }
  resetForm() {
    
    this.location = "";
    this.endDate = null;
    this.startDate = null;
    this.supplierName = "";
    this.selectedSupplierType = [];
    this.selectedCities = []
    this.selectedCity = [];
    this.selectedCategories = [];
    this.cnic = "";
    this.mobile = "";
    const parent = this.checkParent.nativeElement
    parent.querySelector("#start").checked = true;
    parent.querySelector("#all").checked = true;
    parent.querySelector("#all1").checked = true;
    parent.querySelector("#all2").checked = true;
    this.userType = 3;
    this.mobileType = 1;
    this.emailtype = 1;
  }

  funReportFilter() {
    this.reportFilter = !this.reportFilter;
  }

  exportCSV() {
    const options = {
      fieldSeparator: ',',
      quoteStrings: '"',
      decimalSeparator: '.',
      showLabels: true,
      showTitle: true,
      title: 'SupplierCSV',
      useTextFile: false,
      useBom: true,
      useKeysAsHeaders: true,
    };
    const csvExporter = new ExportToCsv(options);
    csvExporter.generateCsv(this.excelFileList);
  }

  DownloadXlsx() {
    this.excelService.exportAsExcelFile(this.excelFileList, "SupplierManualReport")
  }

  populateCustomerAddress() {
    this.service.get(this.service.apiRoutes.Supplier.GetSupplierAddressList).subscribe(result => {
      console.log(result.json());
      this.customerAddressList = result.json();
      this.customerAddressList.forEach(value => {
        let add = { "customerAddress": value.businessAddress }
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
  getRadioValueut(e) {
    this.userType = e.target.value;

  }
  getRadioValue(e) {
    this.customerSearch = e.target.value;
  }
  getRadioValuemobile(e) {
    this.mobileType = e.target.value;
  }

  get f() {
    return this.appValForm.controls;
  }
    populateCategories()
    {
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
        //console.log(value);
        return value.id != item.id;
      }

    );
    console.log(this.selectedCategories);
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
  getRadioValueemail(e) {
    this.emailtype = e.target.value;
  }
   cateobjectArray = [];
  SupplierCustomeReportS() {
    
    var data = this.appValForm.value;
    var supplierpra = "";
    let ids = [];
    let ctids = [];
    this.cateobjectArray = [];
    this.selectedCategories.forEach(function (item) {
      ids.push(item.id);
    })
    this.selectedCity.forEach(function (item) {

      ctids.push(item.id);
    });
    
    if (this.supplierName != "" && this.supplierName != undefined) {
      if (this.customerSearch == 1) {
        supplierpra = this.supplierName + "%";
      }
      else if (this.customerSearch == 2) {
        supplierpra = "%" + this.supplierName + "%";
      }
      else if (this.customerSearch == 3) {
        supplierpra = "%" + this.supplierName;
      }
    }
    this.pipe = new DatePipe('en-US');
    this.fromDate = this.pipe.transform(this.startDate, 'MM/dd/yyyy');
    this.toDate = this.pipe.transform(this.endDate, 'MM/dd/yyyy');
    console.log(this.startDate);
    let url = "";
    
    let obj = {
      StartDate: "",
      endDate: "",
      skills: ids,
      supplier: supplierpra,
      categories: "1",
      city: ctids,
      lastActive: this.lastActive,
      location: this.location,
      mobile: this.mobile,
      cnic: this.cnic,
      userType: String(this.userType),
      emailtype: String(this.emailtype),
      mobileType: String(this.mobileType),
    }
    let x = obj;
    console.log(x);
    url = this.service.apiRoutes.Supplier.GetSupplierForReport + "?StartDate=" + this.startDate + "&EndDate=" + this.endDate + "&skills=" + ids + "&supplier=" + supplierpra + "&city=" + ctids +"&lastActive=" + this.lastActive + "&location=" + this.location + "&mobile=" + this.mobile + "&cnic=" + this.cnic + "&userType=" + this.userType + "&emailtype=" + this.emailtype + "&mobileType=" + this.mobileType;
    // url = this.service.apiRoutes.Supplier.GetSupplierForReport + "?StartDate=" + this.startDate + "&EndDate=" + this.endDate + "&skills=" + ids + "&supplier=" + supplierpra + "&city=" + ctids +"&lastActive=" + this.lastActive + "&location=" + this.location + "&mobile=" + this.mobile + "&cnic=" + this.cnic + "&userType=" + this.userType + "&emailtype=" + this.emailtype + "&mobileType=" + this.mobileType;
      this.Loader.show();
    
    this.service.get(url).subscribe(result => {
      
      if (result.json() != null) {
        this.SupplierList = result.json();
        console.log(this.SupplierList)
        this.completeList = this.SupplierList.length;
        if (this.SupplierList.length > 0) {
          this.excelFileList = this.SupplierList.map((supplier) => {
            return {
              firstName: supplier.firstName,
              businessAddress: supplier.businessAddress,
              cityName: supplier.cityName,
              cnic: supplier.cnic,
              createdOn: new Date(supplier.createdOn).toLocaleDateString(),
              lastActive: new Date(supplier.lastActive).toLocaleDateString(),
              lastName: supplier.lastName,
              mobileNumber: supplier.mobileNumber,
              supplierType: supplier.supplierType,
              supplierId: supplier.supplierId,
            }
          })
          this.reportFilter = true;
          this.showTable = true;
          this.showNullMessage = false;
          var counts = {};
          for (var i = 0; i < this.SupplierList.length; i++) {
            counts[this.SupplierList[i].name] = 1 + (counts[this.SupplierList[i].name] || 0);
          }

          console.log("counts");
          console.log(JSON.stringify(counts));
          
          //var cateobjectArray= [];
          var categstring = [] = JSON.stringify(counts).replace('{', '').replace('}', '').replace(/["']/g, "").split(",");

          for (var ctr = 0; ctr < categstring.length; ctr++) {

            let citem = categstring[ctr].split(":");
            let categoryObj: CategorryCount = new CategorryCount();
            
            categoryObj.category = citem[0];
            categoryObj.count = citem[1];
            this.cateobjectArray.push(categoryObj);

          }

          console.log(this.cateobjectArray);
        }
      }
      else {
        this.toastrService.error("No record found !","Search")
        this.showNullMessage = false;
          this.showTable = false;
        }
        this.Loader.hide();
      },
        error => {
          console.log(error);
          this.Loader.hide();
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
    //
    //this.Loader.show();
    //this.dynamicScripts.load("pdf").then(data => {
    //  var htmltable = document.getElementById('dataTable');
    //  var shtmltable = document.getElementById('summary');
    //  html2canvas(htmltable).then(canvas => {
    //    // Few necessary setting options  
    //    var imgWidth = 208;
    //    var pageHeight = 295;
    //    var imgHeight = canvas.height * imgWidth / canvas.width;
    //    var heightLeft = imgHeight;

    //    const contentDataURL = canvas.toDataURL('image/png')
    //    let pdf = new jsPDF('p', 'mm'); // A4 size page of PDF  
    //    var position = 0;
    //    pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight)
    //    pdf.save('SuppliersManualReport.pdf'); // Generated PDF  
    //    this.Loader.hide();
    //  });

    //})
   
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
