import { DatePipe } from '@angular/common';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { jsPDF } from 'jspdf';
import { UserOptions } from 'jspdf-autotable';
import { NgxSpinnerService } from "ngx-spinner";
import { ExcelFileService } from '../../Shared/CommonServices/excel-file.service';
import { CommonService } from '../../Shared/HttpClient/_http';
interface jsPDFWithPlugin extends jsPDF {
  autoTable: (options: UserOptions) => jsPDF;
}

@Component({
  selector: 'app-leadgers',
  templateUrl: './leadgers.component.html',
  styleUrls: ['./leadgers.component.css']
})
export class LeadgersComponent implements OnInit {
  public getLeadgerList = [];
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  public startDate: Date;
  public endDate: Date;
  public startDate1: Date;
  public endDate1: Date;
  public pipe;
  public jobid: number;
  public customerId: number;
  public tradesmanId: number;
  public newgetLeadgerList = [];
  public tielsList = [];
  public appValForm: FormGroup;

  @ViewChild('dataTable', { static: true }) dataTable: ElementRef;
  constructor(private common: CommonService, private router: Router, public Loader: NgxSpinnerService, public fb: FormBuilder, public excelService: ExcelFileService
  ) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Ledgers"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.filterLedgerReport();
    this.getLeadgerReport();
  }
  // Filter Ledger Report
  public filterLedgerReport() {
    this.appValForm = this.fb.group({
      customerId: [''],
      tradesmanId: [''],
      jobQuotationId: [''],
      jobDetailsId: [''],
      startDate: [],
      endDate: []
    });
  }
  public getLeadgerReport() {
    var formData = this.appValForm.value;
    formData.customerId = formData.customerId == '' ? formData.customerId = 0 : formData.customerId;
    formData.tradesmanId = formData.tradesmanId == '' ? formData.tradesmanId = 0 : formData.tradesmanId;
    formData.jobQuotationId = formData.jobQuotationId == '' ? formData.jobQuotationId = 0 : formData.jobQuotationId;
    formData.jobDetailsId = formData.jobDetailsId == '' ? formData.jobDetailsId = 0 : formData.jobDetailsId;
    this.Loader.show();
    this.common.PostData(this.common.apiRoutes.PackagesAndPayments.GetLeaderReport, formData).then(res => {
      this.newgetLeadgerList = res.json();
      this.tielsList = [];
      if (this.newgetLeadgerList.length > 0) {
        this.newgetLeadgerList.forEach(y => {
          if (y.section == '1-Totals') {
            this.tielsList.push(y);
          }
        });
        this.getLeadgerList = [];
        this.newgetLeadgerList.forEach(x => {
          if (x.section == '2-Detail') {
            this.getLeadgerList.push(x);
          }
        });
      }
      this.Loader.hide();
    });
  }

  resetForm() {
    this.appValForm.reset();
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

  numberOnly(event): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }
}
