import { Component, OnInit, VERSION, ElementRef, ViewChild } from '@angular/core';
import { CommonService } from '../../Shared/HttpClient/_http';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";
import { Router } from '@angular/router';
import Highcharts from 'highcharts';
import Exporting from 'highcharts/modules/exporting';
import funnel from 'highcharts/modules/funnel';
import { FormBuilder, FormGroup } from '@angular/forms';

Exporting(Highcharts);
funnel(Highcharts);
@Component({
  selector: 'app-jobs-by-category',
  templateUrl: './jobs-by-category.component.html',
  styleUrls: ['./jobs-by-category.component.css']
})
export class JobsByCategoryComponent implements OnInit {
  userType: boolean = false;
  jobCategoryList = [];
  totalJobs: number;
  skillList = [];
  jobsByCategoryForm: FormGroup;
  @ViewChild("positiveContainer", { static: false }) positiveContainer: ElementRef;
  @ViewChild('restUserType', { static: true }) restUserType: ElementRef;
  constructor(public fb: FormBuilder, public service: CommonService, public toastr: ToastrService, public Loader: NgxSpinnerService, private router: Router) {
  }

  ngOnInit() {
    this.jobsByCategoryForm = this.fb.group({
      startDate: [null],
      endDate: [null],

    });
    this.getJobsListByCategory();
  }
  resetForm() {
    this.jobsByCategoryForm.reset();
    this.userType = false;
    this.restUserType.nativeElement.checked = true;
    this.getJobsListByCategory();
  }
  public getUserTypeValue(event) {
    let userTypeValue = event.target.value;
    this.userType = userTypeValue == 1 ? true : false;
  }
  getJobsListByCategory() {
    this.Loader.show();
    let formData = this.jobsByCategoryForm.value;
    formData.userType = this.userType;
    console.log(formData);
    
    this.service.post(this.service.apiRoutes.Jobs.GetJobsListByCategory, formData).subscribe(response => {
      this.jobCategoryList = response.json();
      if (!this.jobCategoryList) {
        this.toastr.error("No Data Found", "Error");
        this.Loader.hide();
      }
      else {
        this.totalJobs = this.jobCategoryList[0].totalJobsCount;
        let graphData = [];
        for (let item in this.jobCategoryList) {
          let obj = { name: this.jobCategoryList[item].name, y: this.jobCategoryList[item].jobCategoryCount }
          graphData.push(obj);
        }
        this.positiveDoughnutOptions.title.text = '';
        this.positiveDoughnutOptions.title.text = 'Job By Category';
        this.positiveDoughnutOptions.series[0].data = null;
        this.positiveDoughnutOptions.series[0].data = graphData;
        this.mapCharts();
        this.Loader.hide();
        
      }
     
    }, error => {
      console.log(error);
    });
  }
  mapCharts() {
    Highcharts.chart(this.positiveContainer.nativeElement, this.positiveDoughnutOptions);
  }
  public positiveDoughnutOptions: any = {
    chart: {
      plotBackgroundColor: null,
      plotBorderWidth: null,
      plotShadow: false,
      type: 'pie'
    },
    title: {
      // text: this.tradesmanCity + ' Tradesman'
    },
    tooltip: {
      pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
    },
    plotOptions: {
      pie: {
        allowPointSelect: true,
        cursor: 'pointer',
        dataLabels: {
          enabled: true,
          format: '<b>{point.name}</b>: {point.percentage:.1f} %'
        }
      }
    },
    series: [{
      type: 'pie',
      name: 'Jobs Percentage',
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

}
