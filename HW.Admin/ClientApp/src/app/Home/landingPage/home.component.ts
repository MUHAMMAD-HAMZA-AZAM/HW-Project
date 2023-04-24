import { Component, OnInit, Inject, ViewChild, ElementRef, VERSION } from '@angular/core';
import { CommonService } from '../../Shared/HttpClient/_http';
import { AdminDashboardVm } from 'src/app/Shared/Models/HomeModel/HomeModel';
import { NgxSpinnerService } from "ngx-spinner";
//import { Highcharts } from 'highcharts';

import  Highcharts from 'highcharts';
import Exporting from 'highcharts/modules/exporting';
import funnel from 'highcharts/modules/funnel';
import { Router } from '@angular/router';
import { MessagingService } from '../../shared/CommonServices/messaging.service';
import { JwtHelperService } from '@auth0/angular-jwt';

Exporting(Highcharts);
funnel(Highcharts);

@Component({
  selector: 'app-LandingPage',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  name = `Angular! v${VERSION.full}`;
  public adminDashboardVm: AdminDashboardVm = new AdminDashboardVm();
  public home = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  public allUsersCount;
  public authorizeJobsCount;
  public createdJobsCount;
  public liveAdsCount;
  public registeredCustomersCount;
  public supplierCounts;
  public totalCountGujWala;
  public totalCountGujrat;
  public totalCountIsb;
  public totalCountKhi;
  public totalCountLhr;
  public tradesmanCount;
  public dataArray: any[] = [];
  public customerDataArray: any[] = [];
  public supplierDataArray: any[] = [];
  public orgnizationDataArray: any[] = [];
  public supplierSeries: any[] = [];
  public tradesmanSeries: any[] = [];
  public customerSeries: any[] = [];
  jwtHelperService: JwtHelperService = new JwtHelperService();
  @ViewChild("positiveContainer") positiveContainer: ElementRef;
  @ViewChild("negativeContainer") negativeContainer: ElementRef;
  @ViewChild("nuetralContainer") nuetralContainer: ElementRef;
  @ViewChild("orgnizationContainer") orgnizationContainer: ElementRef;
  @ViewChild("barChartContainer") barChartContainer: ElementRef;

  constructor(private _messagingService: MessagingService, public router: Router, public service: CommonService, public Loader: NgxSpinnerService) {
  }

  ngOnInit() {
    this.home = JSON.parse(localStorage.getItem("Home"));
    this.populateData();
    this.getLocalStorageData();
  }
  public getLocalStorageData() {
    let token = localStorage.getItem("auth_token");
    var decodedtoken = token != null ? this.jwtHelperService.decodeToken(token) : "";

    if (decodedtoken) {
      this.getLoggedUserDetails(decodedtoken.Role, decodedtoken.UserId);
    }
  }
  getLoggedUserDetails(userRole :string , userId:string) {
    this.service.get(this.service.apiRoutes.UserManagement.GetUserDetailsByUserRole + `?userRole=${userRole}&userId=${userId}`).subscribe(result => {
      let loggedUserDetails = result.json();
        this._messagingService.requestPermission(loggedUserDetails.firebaseClientId)
    });
  }
  mapCharts() {
      Highcharts.chart(this.positiveContainer.nativeElement, this.positiveDoughnutOptions);
      Highcharts.chart(this.orgnizationContainer.nativeElement, this.orgnizationDoughnutOptions);
      Highcharts.chart(this.negativeContainer.nativeElement, this.negativeDoughnutOptions);
      Highcharts.chart(this.nuetralContainer.nativeElement, this.nuetralDoughnutOptions);
      Highcharts.chart(this.barChartContainer.nativeElement, this.barChart  
    );
  }
  populateData() {
    //
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Customers.SpGetAdminDashBoard).subscribe(result => {
      debugger;
      this.adminDashboardVm = result.json();
      let data = result.json();

    /* BAR CHART */
      for (var i = 0; i < data.length; i++) {
        if (data[i].spLhr != null && data[i].spKhi != null && data[i].spIsb != null && data[i].spGuj != null && data[i].spGwa != null) {
          this.supplierSeries.push(data[i].spLhr, data[i].spKhi, data[i].spIsb, data[i].spGuj, data[i].spGwa);
          break;
        }
      }
      for (var i = 0; i < data.length; i++) {
        if (data[i].trdLhr != null && data[i].trdKhi != null && data[i].trdIsb != null && data[i].trdGuj != null && data[i].trdGwa != null) {
          this.tradesmanSeries.push(data[i].trdLhr, data[i].trdKhi, data[i].trdIsb, data[i].trdGuj, data[i].trdGwa);
          break;
        }
      }
      for (var i = 0; i < data.length; i++) {
        if (data[i].csLhr != null && data[i].csKhi != null && data[i].csIsb != null && data[i].csGuj != null && data[i].csGwa != null) {
          this.customerSeries.push(data[i].csLhr, data[i].csKhi, data[i].csIsb, data[i].csGuj, data[i].csGwa);
          break;
        }
      }
    /* BAR CHART END */



      /*   PIE CHART   */
      for (var i = 0; i < data.length; i++) {
        if (data[i].skillName != null && data[i].tradesmanCountBySkill != null) {
          let obj = { name: data[i].skillName, y: data[i].tradesmanCountBySkill }
          this.dataArray.push(obj);
        }
      }
      for (var i = 0; i < data.length; i++) {
        if (data[i].customerSkillName != null && data[i].customerCount != null) {
          let obj = { name: data[i].customerSkillName, y: data[i].customerCount }
          this.customerDataArray.push(obj);
        }
      }
      for (var i = 0; i < data.length; i++) {
        if (data[i].supplierCategory != null && data[i].supplieCount != null) {
          let obj = { name: data[i].supplierCategory, y: data[i].supplieCount }
          this.supplierDataArray.push(obj);
        }
      }
      for (var i = 0; i < data.length; i++) {
        if (data[i].orgSkillName != null && data[i].orgCount != null) {
          let obj = { name: data[i].orgSkillName, y: data[i].orgCount }
          this.orgnizationDataArray.push(obj);
        }
      }
      /*   PIE CHART END*/
      this.allUsersCount = this.adminDashboardVm[0].allUsersCount
      this.createdJobsCount = this.adminDashboardVm[0].createdJobsCount
      this.liveAdsCount = this.adminDashboardVm[0].liveAdsCount
      this.registeredCustomersCount = this.adminDashboardVm[0].registeredCustomersCount
      this.supplierCounts = this.adminDashboardVm[0].supplierCounts
      this.totalCountGujWala = this.adminDashboardVm[0].totalCountGujWala
      this.totalCountGujrat = this.adminDashboardVm[0].totalCountGujrat
      this.totalCountIsb = this.adminDashboardVm[0].totalCountIsb
      this.totalCountKhi = this.adminDashboardVm[0].totalCountKhi
      this.totalCountLhr = this.adminDashboardVm[0].totalCountLhr
      this.tradesmanCount = this.adminDashboardVm[0].tradesmanCount
      this.authorizeJobsCount = this.adminDashboardVm[0].authorizeJobsCount
      this.adminDashboardVm.allUsersCount = this.adminDashboardVm.allUsersCount - (this.adminDashboardVm.registeredCustomersCount)
      this.Loader.hide();
      this.mapCharts();

    },
      error => {
        this.Loader.hide();
        console.log(error);
        alert(error);
      });
  }

  public positiveDoughnutOptions: any = {
    chart: {
      plotBorderWidth: null,
      plotShadow: false
    },
    title: {
      text: 'Lahore Sole Tradesman'
    },
    tooltip: {
      pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
    },
    plotOptions: {
      pie: {
        shadow: false,
        center: ['50%', '50%'],
        size: '80%',
        innerSize: '60%'
      }
    },
    series: [{
      type: 'pie',
      name: 'Percentage',
      data: 
        this.dataArray
    }]
  }
  public orgnizationDoughnutOptions: any = {
    chart: {
      plotBorderWidth: null,
      plotShadow: false
    },
    title: {
      text: 'Lahore Orgnization Tradesman'
    },
    tooltip: {
      pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
    },
    plotOptions: {
      pie: {
        shadow: false,
        center: ['50%', '50%'],
        size: '80%',
        innerSize: '60%'
      }
    },
    series: [{
      type: 'pie',
      name: 'Percentage',
      data:
        this.orgnizationDataArray
    }]
  }
  public negativeDoughnutOptions: any = {
    chart: {
      plotBorderWidth: null,
      plotShadow: false
    },
    title: {
      text: 'Lahore Customers'
    },
    tooltip: {
      pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
    },
    plotOptions: {
      pie: {
        shadow: false,
        center: ['50%', '50%'],
        size: '80%',
        innerSize: '60%'
      }
    },
    series: [{
      type: 'pie',
      name: 'Percentage',
      data: this.customerDataArray
    }]
  }
  public nuetralDoughnutOptions: any = {
    chart: {
      plotBorderWidth: null,
      plotShadow: false
    },
    title: {
      text: 'Lahore Supplier'
    },
    tooltip: {
      pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
    },
    plotOptions: {
      pie: {
        shadow: false,
        center: ['50%', '50%'],
        size: '80%',
        innerSize: '60%'
      }
    },
    series: [{
      type: 'pie',
      name: 'Percentage',
      data:
        this.supplierDataArray
    }]
  }

  public barChart: any = {
    chart: {
      type: 'column'
    },
    title: {
      text: 'Total User'
    },
    subtitle: {
      text: ''
    },
    xAxis: {
      categories: [
        'Lahore',
        'Karachi',
        'Islamabad',
        'Gujrat',
        'Gujranwala'
      ],
      crosshair: true
    },
    yAxis: {
      min: 0,
      title: {
        text: 'Users'
      }
    },
    tooltip: {
      headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
      pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
        '<td style="padding:0"><b>{point.y:.1f}</b></td></tr>',
      footerFormat: '</table>',
      shared: true,
      useHTML: true
    },
    plotOptions: {
      column: {
        pointPadding: 0.2,
        borderWidth: 0
      }
    },
    series: [{
      name: 'Customer',
      data: this.customerSeries //[49.9, 71.5, 106.4, 129.2, 144.0]

    }, {
        name: 'Tradesman',
        data: this.tradesmanSeries //[83.6, 78.8, 98.5, 93.4, 106.0]

    }, {
        name: 'Supplier',
        data: this.customerSeries //[48.9, 38.8, 39.3, 41.4, 47.0]

    }
      ]
  }
}
