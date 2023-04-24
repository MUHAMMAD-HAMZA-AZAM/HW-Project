import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Response } from '@angular/http';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { jsPDF } from 'jspdf';
import { UserOptions } from 'jspdf-autotable';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { aspNetUserRoles, httpStatus, TransectionTypes } from '../../Shared/Enums/enums';
import { CommonService } from '../../Shared/HttpClient/_http';
import { ResponseVm } from '../../Shared/Models/HomeModel/HomeModel';
interface jsPDFWithPlugin extends jsPDF {
  autoTable: (options: UserOptions) => jsPDF;
}
@Component({
  selector: 'app-transection-report',
  templateUrl: './transection-report.component.html',
  styleUrls: ['./transection-report.component.css']
})
export class TransectionReportComponent implements OnInit {
  public noDataFound: boolean = false;
  public userTransectionReport = [];
  public userTransectionTypes: any = TransectionTypes;
  public userType: any = aspNetUserRoles;
  public appValForm: FormGroup;
  public dataNotFound: boolean = false;
  public decodedtoken: any;
  public reponse: ResponseVm = new ResponseVm();
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  jwtHelperService: JwtHelperService = new JwtHelperService();

  @ViewChild('dataTable', { static: true }) dataTable: ElementRef;
  constructor(public fb: FormBuilder,
    public common: CommonService,
    public toaster: ToastrService,
    public Loader: NgxSpinnerService,
    public router: Router) { }

  ngOnInit() {

    this.userRole = JSON.parse(localStorage.getItem("User Receipts"));
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.filterUserTransectionReportForm();
  }


  DownloadPdf() {

    const doc = new jsPDF('l', 'px', 'a4') as jsPDFWithPlugin;
    const pdfTable = this.dataTable.nativeElement;
    doc.autoTable({
      styles: { fontSize: 8 },
      html: pdfTable
    });
    doc.save('TransectionReport.pdf')
  }

  //--------------- User Transection Report Form
  public filterUserTransectionReportForm() {
    this.appValForm = this.fb.group({
      userTypeId: [null, Validators.required],
      transectionType: [null, Validators.required],
      userId: [null, Validators.required],
    });
  }

  get f() {
    return this.appValForm.controls;
  }

  //--------------- Show User Transection Report 
  public showUserTransectionReport() {
    let formData = this.appValForm.value;
    console.log(formData);             
    this.common.PostData(this.common.apiRoutes.PackagesAndPayments.GetUserTransectionReport, JSON.stringify(formData),true).then(result => {
      this.reponse = result.json();
      if (this.reponse.status == httpStatus.Ok) {
        this.userTransectionReport = this.reponse.resultData;
        console.log(this.userTransectionReport);
        if (!this.userTransectionReport) {
          this.toaster.error("No Data Found !!");
          this.Loader.hide();
          this.noDataFound = true;

        }
        this.Loader.hide();
      }
       
      }, error => {
        console.log(error);
        this.Loader.show();
      });
  }

  //----------- Reset Transection Report Filter Form
  public resetForm() {
    this.appValForm.reset();
    this.appValForm.controls.userTypeId.setValue(null);
    this.appValForm.controls.userId.setValue(null);
    //this.noDataFound = true;
   
  }
  exportCSV() {


  }
  DownloadXlsx() {

  }
}
