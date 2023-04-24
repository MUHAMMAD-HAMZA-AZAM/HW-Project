import { Component, OnInit, VERSION, ElementRef, ViewChild } from '@angular/core';
import { CommonService } from '../../Shared/HttpClient/_http';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';

import Highcharts from 'highcharts';
import Exporting from 'highcharts/modules/exporting';
import funnel from 'highcharts/modules/funnel';

Exporting(Highcharts);
funnel(Highcharts);

@Component({
  selector: 'app-supplierbycategory',
  templateUrl: './supplierbycategory.component.html',
  styleUrls: ['./supplierbycategory.component.css']
})
export class SupplierbycategoryComponent implements OnInit {
  name = `Angular! v${VERSION.full}`;

  public categoryList = [];
  selectedCategories = [];
  supplierdropdownSettings;
  public selectedSupplierType = [];
  public dropdownListForCity = {};

  public location = "";
  public tradesmanName = "";
  public cityList = [];
  public selectedCities = [{ id: 64, value: "Lahore" }];
  public selectedCity = [];
  public selectedColumn = [];
  public dropdownListForColumn = {};
  public supplierList: any = [];
  public showTable = false;
  public supplierCity;
  public supplierTotalCount = 0;
  public dataArray: any = [];

  public usertype = 3;
  public emailtype = 1;
  public mobileType = 1;

  public startDate: Date;
  public endDate: Date;
  public startDate1: Date;
  public endDate1: Date;
  public pipe;

  @ViewChild('checkParent', { static: true }) checkParent: ElementRef;
  @ViewChild("positiveContainer") positiveContainer: ElementRef;
  constructor(public service: CommonService, public toastr: ToastrService, public Loader: NgxSpinnerService, private router: Router) {
    this.selectedCity = this.selectedCities;
  }
  ngOnInit() {
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
      singleSelection: true,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };
    this.populateCategories();
    this.getAllCities();
    this.populateBySupplierCategory();
  }

  resetForm() {
    this.location = "";
    this.endDate = null;
    this.startDate = null;
    this.selectedCities = []
    this.selectedCity = [];
    this.selectedCategories = [];
    this.selectedSupplierType = [];
    const parent = this.checkParent.nativeElement
    parent.querySelector("#all").checked = true;
    parent.querySelector("#all1").checked = true;
    parent.querySelector("#allMobileUsers").checked = true;
  }

  getRadioValueemail(e) {
    this.emailtype = e.target.value;
  }
  getRadioValueMobileNo(e) {
    this.mobileType = e.target.value;
  }
  getRadioValueut(e) {
    this.usertype = e.target.value;
  }


  populateBySupplierCategory() {
    let cityIds = [];
    let catIds = [];
    let sid = "";
    let cid = "";
    this.selectedCity.forEach(function (item) {
      cityIds.push(item.id);
      cid = String(cityIds);
    });

    this.selectedCategories.forEach(function (item) {
      catIds.push(item.id);
      sid = String(catIds);
    });
    this.pipe = new DatePipe('en-US');
    this.startDate1 = this.pipe.transform(this.startDate, 'MM/dd/yyyy');
    this.endDate1 = this.pipe.transform(this.endDate, 'MM/dd/yyyy');
    
    let obj = {
      startDate: this.startDate1,
      endDate: this.endDate1,
      cityIds: cid,
      catIds: sid,
      location: this.location,
      isOrgnization: false,
      usertype: this.usertype.toString(),
      mobileType: this.mobileType.toString(),
      emailtype: this.emailtype.toString(),
    }
    this.service.PostData(this.service.apiRoutes.Supplier.SupplierByCategory, obj, true).then(result => {
      
      this.supplierList = result.json();
      if (this.supplierList != null && this.supplierList.length > 0) {
        this.showTable = true;
        this.supplierCity = this.supplierList[0].cityName;
        let data = this.supplierList;
        this.dataArray = [];
        this.supplierTotalCount = 0;
        for (var i = 0; i < data.length; i++) {
          this.supplierTotalCount += data[i].supplierCount;
          let obj = { name: data[i].categoryName, y: data[i].supplierCount }
          this.dataArray.push(obj);
        }
        this.positiveDoughnutOptions.title.text = '';
        this.positiveDoughnutOptions.title.text = this.supplierCity + " Supplier";
        this.positiveDoughnutOptions.series[0].data = null;
        this.positiveDoughnutOptions.series[0].data = this.dataArray;
        this.mapCharts();
      }
      else {
        this.showTable = false;
        this.toastr.error("Data not found!", "Feedback")
      }
    })

  }
  mapCharts() {
    Highcharts.chart(this.positiveContainer.nativeElement, this.positiveDoughnutOptions);
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
      name: 'Browser share',
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

  /// supplier category list setting dropdown
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
  }
  onItemSelect(item: any) {
    this.selectedCategories.push(item);
  }
  OnItemDeSelect(item: any) {

    this.selectedCategories = this.selectedCategories.filter(
      function (value, index, arr) {
        //console.log(value);
        return value.id != item.id;
      }
    );
  }
}
