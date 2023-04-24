import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { sum } from 'pdf-lib';
import { report } from 'process';
import { httpStatus } from '../../Shared/Enums/enums';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-detailed-gl-report',
  templateUrl: './detailed-gl-report.component.html',
  styleUrls: ['./detailed-gl-report.component.css']
})
export class DetailedGlReportComponent implements OnInit {
  public appValForm: FormGroup;
  keyword = 'name';
  public accounts = [];
  reportData = [];
  Object = Object;
  public dropdownListForAccounts = {};
  public selectedAccounts = [];
  public selectedAccountIds = [];
  public totalCredit: number;
  public totalDebit: number;
  public totalOpeningBalance: number;
  constructor(public fb: FormBuilder, public service: CommonService, public toastr: ToastrService, public Loader: NgxSpinnerService) { }

  ngOnInit() {
    this.dropdownListForAccounts = {
      singleSelection: false,
      idField: 'id',
      textField: 'name',
      allowSearchFilter: true,
      itemsShowLimit: 10,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };
    //this.reportData = [
    //  { ledgerTransectionId:70972,accountId:58,subAccountId:80584,debit:0.00,credit:450.00,referenceNo:5515568,transactionDate:'2021-12-03',openingBalance:250.0000},
    //  { ledgerTransectionId: 70973, accountId: 58, subAccountId: 80584, debit: 500.00, credit: 0.00, referenceNo: 5515568,transactionDate:'2021-12-03',openingBalance:250.0000},
    //  { ledgerTransectionId: 70979, accountId: 59, subAccountId: 80585, debit: 0.00, credit: 900.00, referenceNo: 9999, transactionDate: '2021-12-04', openingBalance: 545.0000 },
    //  { ledgerTransectionId: 70980, accountId: 58, subAccountId: 80590, debit: 700.00, credit: 0.00, referenceNo: 111, transactionDate: '2021-12-04', openingBalance: 0.0000 },
    //  { ledgerTransectionId: 70981, accountId: 58, subAccountId: 80590, debit: 0.00, credit: 600.00, referenceNo: 111, transactionDate: '2021-12-04', openingBalance: 0.0000 },
    //]
    this.appValForm = this.fb.group({
      fiscalPeriod: '',
      accountName: '',
      startDate: '',
      endDate: '',
    });
    this.getAccountNames();
    //this.getDetailedGLReport()
  }
  public getDetailedGLReport() {
    this.Loader.show();
    this.selectedAccountIds=[];
    //let obj = {
    //  fiscalPeriod: '2021-12-01 23:34:25.187',
    //  startDate: '2021-12-02 00:11:07.610',
    //  endDate: '2021-12-09 23:34:25.187',
    //  subAccountId: null
    //};
    let formData = this.appValForm.value;
    
    if (this.selectedAccounts.length > 0) {
      this.selectedAccounts.forEach(value => {
        this.selectedAccountIds.push(value.id);
      });
      formData.subAccountId = this.selectedAccountIds.toString();
    }
    else
      formData.subAccountId = null;
    
    this.service.post(this.service.apiRoutes.PackagesAndPayments.GetDetailedGLReport, JSON.stringify(formData)).subscribe(result => {
      var response = JSON.parse(result.json());
      
      let res = response.resultData;
      if (response.status == httpStatus.Ok)
        this.generateData(res);
      this.Loader.hide();
    },
      error => {
        this.Loader.hide();
        console.log(error);
      });



  }
  generateData(reportDetails) {
    this.totalCredit = 0;
    this.totalDebit = 0;
    this.totalOpeningBalance = 0;
    let obj;
    let arr = [];
    reportDetails.forEach(x => {
      let filteredArr = reportDetails.filter(f => f.subAccountId == x.subAccountId)
      if (!arr.some(o=> o.entries.some(p=> p.subAccountId == x.subAccountId)))
      {
        obj = {
          subAccountId: x.subAccountId,
          subAccountName: x.subAccountName,
          entries: filteredArr,
          balance: {
            credit: filteredArr.reduce((acc, value) => { return acc += value.credit }, 0),
            debit: filteredArr.reduce((acc, value) => { return acc += value.debit }, 0),
            openingBalance: x.openingBalance,

          }
          
        }
        
        this.totalCredit += Number(obj.balance.credit);
        this.totalDebit += Number(obj.balance.debit);
        this.totalOpeningBalance += Number(obj.balance.openingBalance);

        arr.push(obj);
      }

    });
    this.reportData = arr;
    console.log(arr);
  }
  public getAccountNames() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.PackagesAndPayments.GetSubAccountsLastLevel).subscribe(result => {
      var responce = result.json();
      this.accounts = responce.resultData;
    });
    this.Loader.hide();
  }
  //data() {
  //  this.reportData = this.reportData.reduce((accumulated, value) => {
  //    if (!accumulated[value.subAccountId]) {
  //      accumulated[value.subAccountId] = [];
  //    }
  //    accumulated[value.subAccountId].push(value);
  //    return accumulated;
  //  }, {});
  //  console.log(this.reportData);
  //}
  onSelectAll(item: any) {
    console.log(item);
    this.selectedAccounts = item;
  }
  onAccountDeSelectALL(item: any) {
    this.selectedAccounts = [];
    console.log(item);
  }
  onAccountSelect(item: any) {
    console.log(item);
    this.selectedAccounts.push(item);
  }
  onAccountDeSelect(item: any) {
    this.selectedAccounts = this.selectedAccounts.filter(
      function (value, index, arr) {
        return value.id != item.id;
      });
  }
}
