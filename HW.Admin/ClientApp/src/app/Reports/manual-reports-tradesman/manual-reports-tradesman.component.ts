import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { CommonService } from '../../Shared/HttpClient/_http';
import { NgxSpinnerService } from "ngx-spinner";
import { Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { TradesmanReportVM } from '../../Shared/Models/UserModel/SpTradesmanVM';
import html2canvas from 'html2canvas';
import { DynamicScriptLoaderService } from '../../Shared/CommonServices/dynamicScriptLoaderService'; 
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { httpStatus } from '../../Shared/Enums/enums';
import { CategorryCount } from '../../Shared/Models/HomeModel/HomeModel';
import { PDFDocument, StandardFonts, rgb } from 'pdf-lib'
import { DatePipe } from '@angular/common';
import { ExcelFileService } from '../../Shared/CommonServices/excel-file.service';
import { log } from 'util';
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
  selector: 'app-manual-reports-tradesman',
  templateUrl: './manual-reports-tradesman.component.html',
  styleUrls: ['./manual-reports-tradesman.component.css']
})
export class ManualReportsTradesmanComponent implements OnInit {
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
  public tradesmanList = [];
  public excelFileList = [];
  public completeList: number;
  public cateobjectArray = [];
  public skillList;
  SelectedSkillsList = [];
  skillsdropdownSettings;
  public submittedForm;
  public skipDate: boolean = false;
  public cityList = [];
  public dropdownListForCity = {};
  public dropdownListForColumn = {};
  public selectedCity = [];
  public tradesmanName = "";
  public selectedSkills = []
  public selectedCities = []
  public pipe;
  public location = "";
  public lastActive = true;
  public tradesmanSearch = 1;
  public customerAddressList = [];
  public searchFromAddress = [];
  public searchedAddress = [];
  public isAddress = false;
  public locationSearchKeyMatch = false;
  public reportFilter = false;
  public allowview;
  public emailtype = 1;
  public mobileType = 1;
  public activityType = 1;
  public userType = 3;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    

  @ViewChild('dataTable', { static: true }) dataTable: ElementRef;
  @ViewChild('checkParent', { static: true }) checkParent: ElementRef;
  constructor(
    public toastrService: ToastrService,
    public service: CommonService,
    public Loader: NgxSpinnerService,
    private router: Router,
    private formBuilder: FormBuilder,
    public dynamicScripts: DynamicScriptLoaderService,
    public excelService: ExcelFileService
  )
  { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Tradesman Registration"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.populateCustomerAddress();
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
    this.SelectedSkillsList = [];
    this.selectedCity = [];
    this.populateSkills();
    this.getAllCities();
    this.TradesmanCustomeReportS();
  }
  onClickedOutside(e: Event) {
    this.isAddress = false;
  }
  resetForm() {
    
    this.location = "";
    this.mobile = "";
    this.cnic = "";
    this.endDate = null;
    this.startDate = null;
    this.tradesmanName = "";
    this.selectedSkills = [];
    this.selectedCities = []
    this.selectedCity = [];
    this.SelectedSkillsList = [];
    const parent = this.checkParent.nativeElement
    parent.querySelector("#start").checked = true;
    parent.querySelector("#all").checked = true;
    parent.querySelector("#all1").checked = true;
    parent.querySelector("#all2").checked = true;
    parent.querySelector("#allMobileUsers11").checked = true;
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
      title: 'TradesmanCSV',
      useTextFile: false,
      useBom: true,
      useKeysAsHeaders: true,
    };
    const csvExporter = new ExportToCsv(options);
    csvExporter.generateCsv(this.excelFileList);
  }
  DownloadXlsx() {
    this.excelService.exportAsExcelFile(this.excelFileList, "TradesmanReports")
  }
  populateCustomerAddress() {
    this.service.get(this.service.apiRoutes.TrdesMan.GetTradesmanAddressList).subscribe(result => {
      
      this.customerAddressList = result.json();
      this.customerAddressList.forEach(value => {
        let add = { "customerAddress": value.shopAddress }
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
    this.tradesmanSearch = e.target.value;
  }
  getRadioValueemail(e) {
    this.emailtype = e.target.value;
  }
  getRadioValueActivity(e) {
    this.activityType = e.target.value;
  }
  getRadioValueut(e) {
    this.userType = e.target.value;
  }

  onItemSelectAll(items: any) {
    console.log(items);
    this.SelectedSkillsList = items;
    //this.SelectedSkillsList = [];
    //items.forEach(function (item) {

    //  this.SelectedSkillsList.push(item);


    //});

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
        //console.log(value);
        return value.id != item.id;
      }

    );

    console.log(this.SelectedSkillsList);
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
  getRadioValuemobile(e) {
    this.mobileType = e.target.value;
  }

  get f() {
    return this.appValForm.controls;
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
  TradesmanCustomeReportS() {
    var tradesmanpra = "";
    console.log(this.skipDate);
    let ids = [];
    let ctids = [];
    this.SelectedSkillsList.forEach(function (item) {
      ids.push(item.id);
    })
    this.selectedCity.forEach(function (item) {
      ctids.push(item.id);
    });
    if (this.tradesmanName != "" && this.tradesmanName != undefined) {
      if (this.tradesmanSearch == 1) {
        tradesmanpra = this.tradesmanName + "%";
      }
      else if (this.tradesmanSearch == 2) {
        tradesmanpra = "%" + this.tradesmanName + "%";
      }
      else if (this.tradesmanSearch == 3) {
        tradesmanpra = "%" + this.tradesmanName;
      }
    }

    this.pipe = new DatePipe('en-US');
    this.fromDate = this.pipe.transform(this.startDate, 'MM/dd/yyyy');
    this.toDate = this.pipe.transform(this.endDate, 'MM/dd/yyyy');
    console.log(this.startDate);

    this.cateobjectArray = [];
    this.tradesmanList = [];
    this.Loader.show();
    let url = "";
    url = this.service.apiRoutes.Users.getAllTradesmanFromToReport + "?StartDate=" + this.fromDate + "&EndDate=" + this.toDate + "&skills=" + ids + "&tradesman=" + tradesmanpra + "&city=" + ctids + "&lastActive=" + this.lastActive + "&location=" + this.location
      + "&mobile=" + this.mobile + "&cnic=" + this.cnic + "&emailtype=" + this.emailtype + "&mobileType=" + this.mobileType + "&activityType=" + this.activityType + "&userType=" + this.userType;

   // url = this.service.apiRoutes.Users.getAllTradesmanFromToReport + "?StartDate=" + this.startDate + "&EndDate=" + this.endDate + "&skills=" + ids;
  
    this.service.get(url).subscribe(result => {
      console.log(result);
      if (result.json() != "" && result.json() != null) {
        this.tradesmanList = result.json();
        console.log(this.excelFileList)
        
        this.completeList = this.tradesmanList.length;
        if (this.tradesmanList.length > 0) {
          this.excelFileList = this.tradesmanList.map(trademan => {
            return {
              firstName: trademan.firstName,
              lastName: trademan.lastName,
              mobileNumber: trademan.mobileNumber,
              cnic: trademan.cnic,
              skillName: trademan.skillName,
              addressLine: trademan.addressLine,
              //area: trademan.area,
              city: trademan.city,
              //createdOn: new Date(trademan.createdOn).toLocaleDateString(),
              //lastActive: new Date(trademan.lastActive).toLocaleDateString(),
              //tradesmanId: trademan.tradesmanId,
            }
          })
          this.showTable = true;
          this.showNullMessage = false;
          this.reportFilter = true;

          var counts = {};
          for (var i = 0; i < this.tradesmanList.length; i++) {
            counts[this.tradesmanList[i].name] = 1 + (counts[this.tradesmanList[i].name] || 0);
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
        this.toastrService.error("No record found !", "Search");
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

  public SavePDF(): void {
    this.dynamicScripts.load("pdf").then(data => {
      
      let content = document.getElementById('dataTable');
      let doc = new jsPDF();
      let _elementHandlers =
      {
        '#editor': function (element, renderer) {
          return true;
        }
      };
      //doc.fromHTML(content.innerHTML, 15, 15, {

      //  'width': 190,
      //  'elementHandlers': _elementHandlers
      //});

      doc.save('test.pdf');
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
