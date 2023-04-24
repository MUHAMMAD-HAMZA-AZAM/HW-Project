import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { GLAccountTypes } from '../../Shared/Enums/enums';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-detailed-general-ledger',
  templateUrl: './detailed-general-ledger.component.html',
  styleUrls: ['./detailed-general-ledger.component.css']
})
export class DetailedGeneralLedgerComponent implements OnInit {
  public userLedgerRecord: object;
  public detailedGeneralLedgerTransactionList = [];
  public selectedAccountType: any;
  public accountTypes: any = GLAccountTypes;
  public appValForm: FormGroup;
  public decodedtoken: any;
  public hiddenData: boolean = false;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(public fb: FormBuilder,
    public common: CommonService,
    public toaster: ToastrService,
    public Loader: NgxSpinnerService,
    public router: Router, public _modalService: NgbModal) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Detailed General Ledger"));
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    console.log(this.accountTypes);
    this.filterDetailedLedgerTransectionReport();
  }

  // Detailed Ledger Transection Form
  public filterDetailedLedgerTransectionReport() {
    this.appValForm = this.fb.group({
      accountId: [null, Validators.required],
      fromDate: '',
      toDate: '',
      userId: ['', Validators.required]
    });

  }

  get f() {
    return this.appValForm.controls;
  }


  //  // General Ledger Transection Screen Display
  public showDetailedLedgerTransectionScreen() {
    let formData = this.appValForm.value;
    formData.accountId = formData.accountId == null ? formData.accountId = 0 : formData.accountId;
    formData.userId = formData.userId == null ? formData.userId = 0 : formData.userId;
    console.log(formData);
    this.Loader.show();
    
    this.common.PostData(this.common.apiRoutes.PackagesAndPayments.GetDetailedLedgerTransectionReportByAccountRef, formData).then(result => {
      this.detailedGeneralLedgerTransactionList = result.json();
      console.log(this.detailedGeneralLedgerTransactionList);
      if ( !this.detailedGeneralLedgerTransactionList ) {
        this.toaster.error("No Record Found !!");
        this.Loader.hide();
       /* this.hiddenData = true;*/
      }
      else {
        this.userLedgerRecord = this.detailedGeneralLedgerTransactionList[0];
        console.log(this.userLedgerRecord);
      }
      this.Loader.hide();
     
    });
  }
  // Reset FilterTransection Form 
  public resetFilterForm() {
    this.appValForm.reset();
    this.hiddenData = false;
    this.appValForm.controls.accountId.setValue(0);
    this.appValForm.controls.userId.setValue(0);
    this.showDetailedLedgerTransectionScreen();
  }

  // Select User Accoount Type

  public onAccountTypeChange(event) {
    this.selectedAccountType = event.target.value;
    console.log(this.selectedAccountType);
  }
}
