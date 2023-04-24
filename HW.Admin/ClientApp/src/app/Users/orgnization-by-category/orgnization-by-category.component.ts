import { Component, OnInit, VERSION, ViewChild, ElementRef } from '@angular/core';
import { CommonService } from '../../Shared/HttpClient/_http';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";

import Highcharts from 'highcharts';
import Exporting from 'highcharts/modules/exporting';
import funnel from 'highcharts/modules/funnel';
import { DatePipe } from '@angular/common';

Exporting(Highcharts);
funnel(Highcharts);


@Component({
  selector: 'app-orgnization-by-category',
  templateUrl: './orgnization-by-category.component.html',
  styleUrls: ['./orgnization-by-category.component.css']
})
export class OrgnizationByCategoryComponent implements OnInit {
  name = `Angular! v${VERSION.full}`;
  public skillList = [];
  SelectedSkillsList = [];
  public selectedSkills = [];
  public tradesmansList = [];

  public location = "";
  public tradesmanName = "";
  public cityList = [];
  public selectedCities = [{ id: 64, value: "Lahore" }];
  public selectedCity = [];
  public selectedColumn = [];
  public dropdownListForCity = {};
  public skillsdropdownSettings = {};
  public dropdownListForColumn = {};
  public tradesmanList: any = [];
  public showTable = false;
  public showAllCity: boolean;
  public isCityNull = true;
  public dataArray: any[] = [];
  public reloadChart = false;

  public totalSum;
  public tradesmanCity;
  public totalCities: any[] = [];
  public totalGujratSum = 0;
  public gujratList: any[] = [];
  public gujratChartData: any[] = [];

  public totalLhrSum = 0;
  public lhrList: any[] = [];
  public lhrChartData: any[] = [];

  public totalIsbSum = 0;
  public isbList: any[] = [];
  public isbChartData: any[] = [];

  public totalKhiSum = 0;
  public khiList: any[] = [];
  public khiChartData: any[] = [];

  public totalGujwaSum = 0;
  public gujwaList: any[] = [];
  public gujwaChartData: any[] = [];

  public usertype = 3;
  public emailtype = 1;
  public mobileType = 1;
  public activityType = 1;

  public startDate: Date;
  public endDate: Date;
  public startDate1: Date;
  public endDate1: Date;
  public pipe;

  @ViewChild("positiveContainer") positiveContainer: ElementRef;
  @ViewChild('checkParent', { static: true }) checkParent: ElementRef;
  @ViewChild('gujratChartContainer', { static: true }) gujratChartContainer: ElementRef;
  @ViewChild('lahoreChartContainer', { static: true }) lahoreChartContainer: ElementRef;
  @ViewChild('islamabadChartContainer', { static: true }) islamabadChartContainer: ElementRef;
  @ViewChild('karachiChartContainer', { static: true }) karachiChartContainer: ElementRef;
  @ViewChild('gujranwalChartContainer', { static: true }) gujranwalChartContainer: ElementRef;
  constructor(public service: CommonService, public toastr: ToastrService, public Loader: NgxSpinnerService) {
    this.selectedCity = this.selectedCities;
  }

  ngOnInit() {
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
      singleSelection: true,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };
    this.populateSkills();
    this.getAllCities();
    this.populateByTradesmanCategory();
  }

  resetForm() {
    this.startDate = null;
    this.endDate = null;
    this.location = "";
    this.selectedCities = []
    this.selectedCity = [];
    this.SelectedSkillsList = [];
    this.selectedSkills = [];
    const parent = this.checkParent.nativeElement
    parent.querySelector("#all").checked = true;
    parent.querySelector("#all1").checked = true;
    parent.querySelector("#allMobileUsers").checked = true;
    parent.querySelector("#allMobileUsers11").checked = true;

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

  populateByTradesmanCategory() {
    let cityIds = [];
    let skillIds = [];
    let sid = "";
    let cid = "";
    this.selectedCity.forEach(function (item) {
      cityIds.push(item.id);
      cid = String(cityIds);
    });
    this.selectedSkills.forEach(function (item) {
      skillIds.push(item.id);
      sid = String(skillIds);
    });
    this.pipe = new DatePipe('en-US');
    this.startDate1 = this.pipe.transform(this.startDate, 'MM/dd/yyyy');
    this.endDate1 = this.pipe.transform(this.endDate, 'MM/dd/yyyy');
    let obj = {
      startDate: this.startDate1,
      endDate: this.endDate1,
      cityIds: cid,
      skillIds: sid,
      location: this.location,
      isOrgnization: true,
      usertype: this.usertype.toString(),
      mobileType: this.mobileType.toString(),
      emailtype: this.emailtype.toString(),
      activityType: this.activityType.toString()
    }
    console.log(obj)
    this.service.PostData(this.service.apiRoutes.TrdesMan.TradesmanByCategory, obj, true).then(result => {
      this.tradesmanList = result.json();
      if (this.tradesmanList != null && this.tradesmanList.length > 0) {
        this.showTable = true;
        this.showAllCity = true;
        this.isCityNull = true;
        // single Graph  //
        this.tradesmanCity = this.tradesmanList[0].city;
        let data = this.tradesmanList;
        this.dataArray = [];
        this.totalSum = 0;
        for (var i = 0; i < data.length; i++) {
          this.totalSum += data[i].tradesmanCount;
          let obj = { name: data[i].skillName, y: data[i].tradesmanCount }
          this.dataArray.push(obj);
        }
        this.positiveDoughnutOptions.title.text = '';
        this.positiveDoughnutOptions.title.text = this.tradesmanCity + " Tradesman";
        this.positiveDoughnutOptions.series[0].data = null;
        this.positiveDoughnutOptions.series[0].data = this.dataArray;
        // Single Graph end
        
        if (cid == "" || cid == null) {
          this.showTable = false;
          this.showAllCity = false;
          this.dataArray = [];
          this.isCityNull = false;
          this.totalGujratSum = 0;
          this.totalGujwaSum = 0;
          this.totalIsbSum = 0;
          this.totalLhrSum = 0;
          this.totalKhiSum = 0
          this.isbChartData = [];
          this.gujratChartData = [];
          this.gujwaChartData = [];
          this.khiChartData = [];
          this.lhrChartData = [];
          this.isbList = [];
          this.lhrList = [];
          this.khiList = [];
          this.gujwaList = [];
          this.gujratList = [];
          // gujrat data //
          for (var i = 0; i < data.length; i++) {
            if (data[i].city == 'Gujrat') {
              this.totalGujratSum += data[i].tradesmanCount;
              let gujForChart = { name: data[i].skillName, y: data[i].tradesmanCount }
              this.gujratChartData.push(gujForChart)
              let gujForList = { city: data[i].city, skillName: data[i].skillName, tradesmanCount: data[i].tradesmanCount }
              this.gujratList.push(gujForList);
            }
            else if (data[i].city == 'Lahore') {
              this.totalLhrSum += data[i].tradesmanCount;
              let lhrForChart = { name: data[i].skillName, y: data[i].tradesmanCount }
              this.lhrChartData.push(lhrForChart)
              let lhrForList = { city: data[i].city, skillName: data[i].skillName, tradesmanCount: data[i].tradesmanCount }
              this.lhrList.push(lhrForList);
            }
            else if (data[i].city == 'Islamabad') {
              this.totalIsbSum += data[i].tradesmanCount;
              let isbForChart = { name: data[i].skillName, y: data[i].tradesmanCount }
              this.isbChartData.push(isbForChart)
              let isbForList = { city: data[i].city, skillName: data[i].skillName, tradesmanCount: data[i].tradesmanCount }
              this.isbList.push(isbForList);
            }
            else if (data[i].city == 'Karachi') {
              this.totalKhiSum += data[i].tradesmanCount;
              let khiForChart = { name: data[i].skillName, y: data[i].tradesmanCount }
              this.khiChartData.push(khiForChart)
              let khiForList = { city: data[i].city, skillName: data[i].skillName, tradesmanCount: data[i].tradesmanCount }
              this.khiList.push(khiForList);
            }
            else {
              this.totalGujwaSum += data[i].tradesmanCount;
              let gujwaForChart = { name: data[i].skillName, y: data[i].tradesmanCount }
              this.gujwaChartData.push(gujwaForChart)
              let gujwaForList = { city: data[i].city, skillName: data[i].skillName, tradesmanCount: data[i].tradesmanCount }
              this.gujwaList.push(gujwaForList);
            }
          }
          
          this.totalCities = [];
          this.totalCities.push(this.lhrList, this.gujwaList, this.gujratList, this.khiList, this.isbList)
          console.log(this.totalCities);

          this.gujratPieChart.title.text = '';
          this.gujratPieChart.title.text = "Gujrat Organization";
          this.gujratPieChart.series[0].data = null;
          this.gujratPieChart.series[0].data = this.gujratChartData;

          this.lahorePieChart.title.text = '';
          this.lahorePieChart.title.text = "Lahore Organization";
          this.lahorePieChart.series[0].data = null;
          this.lahorePieChart.series[0].data = this.lhrChartData;

          this.karachiPieChart.title.text = '';
          this.karachiPieChart.title.text = "Karachi Organization";
          this.karachiPieChart.series[0].data = null;
          this.karachiPieChart.series[0].data = this.khiChartData;

          this.gujranwalaPieChart.title.text = '';
          this.gujranwalaPieChart.title.text = "Gujranwala Organization";
          this.gujranwalaPieChart.series[0].data = null;
          this.gujranwalaPieChart.series[0].data = this.gujwaChartData;

          this.lslamabadPieChart.title.text = '';
          this.lslamabadPieChart.title.text = "Islamabad Organization";
          this.lslamabadPieChart.series[0].data = null;
          this.lslamabadPieChart.series[0].data = this.isbChartData;

        }

        this.mapCharts();
      }
      else {
        this.showTable = false;
        this.isCityNull = true;
        this.showAllCity = true;
        this.toastr.error("Data not found!", "Feedback")
      }
    })
  }

  // chart

  mapCharts() {
    Highcharts.chart(this.positiveContainer.nativeElement, this.positiveDoughnutOptions);
    Highcharts.chart(this.gujratChartContainer.nativeElement, this.gujratPieChart);
    Highcharts.chart(this.lahoreChartContainer.nativeElement, this.lahorePieChart);
    Highcharts.chart(this.islamabadChartContainer.nativeElement, this.lslamabadPieChart);
    Highcharts.chart(this.karachiChartContainer.nativeElement, this.karachiPieChart);
    Highcharts.chart(this.gujranwalChartContainer.nativeElement, this.gujranwalaPieChart);
  }
  public positiveDoughnutOptions: any = {
    chart: {
      plotBorderWidth: null,
      plotShadow: false
    },
    title: {
      // text: this.tradesmanCity + ' Tradesman'
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
      name: '',
      //data: //[
      //this.dataArray
      //{
      //  name: 'Chrome',
      //  y: 20,
      //  sliced: true,
      //  selected: true
      //},
      //]
    }]
  }
  public gujratPieChart: any = {
    chart: {
      plotBorderWidth: null,
      plotShadow: false
    },
    title: {
      text: 'Gujrat Tradesman'
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
      name: '',
    }]
  }
  public lahorePieChart: any = {
    chart: {
      plotBorderWidth: null,
      plotShadow: false
    },
    title: {
      text: 'Lahore Tradesman'
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
      name: '',
    }]
  }
  public lslamabadPieChart: any = {
    chart: {
      plotBorderWidth: null,
      plotShadow: false
    },
    title: {
      text: 'Islamabad Tradesman'
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
      name: '',
    }]
  }
  public karachiPieChart: any = {
    chart: {
      plotBorderWidth: null,
      plotShadow: false
    },
    title: {
      text: 'Karachi Tradesman'
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
      name: '',
    }]
  }
  public gujranwalaPieChart: any = {
    chart: {
      plotBorderWidth: null,
      plotShadow: false
    },
    title: {
      text: 'Gujranwala Tradesman'
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
      name: '',
    }]
  }

  // dropdown City
  onCitySelectAll(item: any) {
    console.log(item);
    this.selectedCity = item;
  }
  onCityDeSelectALL(item: any) {
    this.selectedCity = [];
    console.log(item);
  }
  onCitySelect(item: any) {
    this.selectedCity = [];
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
  // Skills Dropdown

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
        //console.log(value);
        return value.id != item.id;
      }
    );
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

}
