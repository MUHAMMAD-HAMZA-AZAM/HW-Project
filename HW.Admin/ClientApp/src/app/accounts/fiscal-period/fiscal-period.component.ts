import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { httpStatus } from '../../Shared/Enums/enums';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-fiscal-period',
  templateUrl: './fiscal-period.component.html',
  styleUrls: ['./fiscal-period.component.css']
})
export class FiscalPeriodComponent implements OnInit {
  public currentYear: number;
  public currentMonth: number;
  public periodId: number;
  public appValForm: FormGroup;
  public firstDay: string;
  public lastDay: string;
  public userId: string;
  public records: any = [];
  public disableBtn: boolean = false
  public currentDate: Date;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(public fb: FormBuilder, public service: CommonService, public toastr: ToastrService) { }
  
  ngOnInit() {
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    this.userId = decodedtoken.UserId;

    this.currentDate = new Date();
    this.currentMonth = this.currentDate.getMonth() + 1; //January is 0!
    this.currentYear = this.currentDate.getFullYear();
   
    this.appValForm = this.fb.group({
      fiscalYear: ['', Validators.required],
      periodId: ['', Validators.required],
      periodName: ['', Validators.required],
      status: ['', Validators.required],
      startDate: '',
      endDate: '',
    });

    this.GetData();

  }
  formPatchValue() {
    var datePipe = new DatePipe("en-US");
    this.firstDay = datePipe.transform(new Date(this.currentDate.getFullYear(), (this.periodId-1), 1), 'yyyy-MM-dd');
    this.lastDay = datePipe.transform(new Date(this.currentDate.getFullYear(), (this.periodId-1) + 1, 0), 'yyyy-MM-dd');

    this.appValForm.patchValue({
      fiscalYear: this.currentYear,
      periodId: this.periodId,
      startDate: this.firstDay,
      endDate: this.lastDay,
    });
    
  }
  GetData() {
    this.service.get(this.service.apiRoutes.ChartOfAccounts.GetFiscalPeriodsByYear + "?fiscalYear=" + this.currentYear).subscribe(result => {
      let response = result.json();
      if (response.status == httpStatus.Ok) {
        this.records = response.resultData;
        
        this.periodId = (this.records[0].periodId < 12) ? this.records[0].periodId + 1 : this.records[0].periodId;
        this.formPatchValue();
        this.records[0].periodId >= 12 ? this.disableBtn = true : this.disableBtn = false;
        
      }
      else {
        this.periodId = this.currentMonth;
        this.toastr.error("No Record Found.", "Error");
        this.formPatchValue();
      }

    });
  }
  get f() {
    return this.appValForm.controls;
  }
  Save() {
    let formdata = this.appValForm.value;
    if (this.appValForm.valid) {
      formdata.userId = this.userId;
      this.service.PostData(this.service.apiRoutes.ChartOfAccounts.AddFiscalPeriod, JSON.stringify(formdata), true).then(result => {
        let responce = result.json();
        if (responce.status == httpStatus.Ok) {
          this.appValForm.reset();
          this.GetData();
          this.toastr.success("Data Saved Successfully.", "Success");
        }
        else {
          this.toastr.error("Something went wrong.", "Error");
        }
      })

    }
    else {
      this.toastr.error("Something went wrong.", "Error");
    }
  }


}
