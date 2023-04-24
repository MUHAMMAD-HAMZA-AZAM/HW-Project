import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from "ngx-spinner";
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { GLAccountTypes, httpStatus } from '../../Shared/Enums/enums';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-general-ledger-screen',
  templateUrl: './general-ledger-screen.component.html',
  styleUrls: ['./general-ledger-screen.component.css']
})
export class GeneralLedgerScreenComponent implements OnInit {
  public dataNotFound: boolean = false;
  public decodedtoken: any;
  public selectedAccountType: any;
  public userLedgerRecord: any;
  public refUserTypeAccountId: any;
  public generalLedgerTransactionList = [];
  public accountTypes: any = GLAccountTypes;
  public appValForm: FormGroup;
  public ledgerTransectionForm: FormGroup;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(public fb: FormBuilder,
    public common: CommonService,
    public toaster: ToastrService,
    public Loader: NgxSpinnerService,
    public router: Router, public _modalService: NgbModal) { }

  ngOnInit() {
  
    this.userRole = JSON.parse(localStorage.getItem("General Ledger Screen"));
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    console.log(this.accountTypes);
    this.filterLedgerTransectionReport();
    this.generalLedgerForm();
  }

  // Ledger Transection Form
  public filterLedgerTransectionReport() {
    this.appValForm = this.fb.group({
      accountId: [null],
      refCustomerSubAccountId: '',
      refTradesmanSubAccountId: ''
    });

  }

  get f() {
    return this.appValForm.controls;
  }

  // Reset FilterTransection Form 
  public resetFilForm() {
    this.appValForm.reset();
    this.appValForm.controls.accountId.setValue(0);
    this.showGeneralLedgerTransectionScreen();
  }

  // General Ledger Transection Screen Display
  public showGeneralLedgerTransectionScreen() {
    let formData = this.appValForm.value;
    formData.accountId = formData.accountId == null ? formData.accountId = 0 : formData.accountId;
    formData.refCustomerSubAccountId = formData.refCustomerSubAccountId == '' ? formData.refCustomerSubAccountId = 0 : formData.refCustomerSubAccountId;
    formData.refTradesmanSubAccountId = formData.refTradesmanSubAccountId == '' ? formData.refTradesmanSubAccountId = 0 : formData.refTradesmanSubAccountId;
    console.log(formData);
    if (formData.refCustomerSubAccountId > 0) {
      this.refUserTypeAccountId = formData.refCustomerSubAccountId;
    }
    else if (formData.refTradesmanSubAccountId > 0) {
      this.refUserTypeAccountId = formData.refTradesmanSubAccountId;
    }
    else {

    }
    this.Loader.show();
    this.common.PostData(this.common.apiRoutes.PackagesAndPayments.GetLedgerTransectionReportByAccountRef, formData).then(result => {
      this.generalLedgerTransactionList = result.json();
      console.log(this.generalLedgerTransactionList);
      if (!this.generalLedgerTransactionList) {
        this.toaster.error("No Record Found !!");
        this.Loader.hide();
        this.dataNotFound = true;
      }

      if (this.refUserTypeAccountId > 0) {  
          this.userLedgerRecord = this.generalLedgerTransactionList[0];
          console.log(this.userLedgerRecord);
      }

      this.Loader.hide();
    });
  }

  // Show Modal For General Ledger Entry
  public showModal(content) {
    this.appValForm.reset();
    this._modalService.open(content)
  }

  // Add New General Ledger Entery Form
  public generalLedgerForm() {
    this.ledgerTransectionForm = this.fb.group({

      ledgerTransactionId: 0,
      accountId: [null, Validators.required],
      subAccountId: 0,
      debit: [null],
      credit: [null],
      active: '',
      refCustomerSubAccountId: '',
      refTradesmanSubAccountId: '',
      refSupplierSubAccountId: '',
      reffrenceDocumentNo: ['', Validators.required],
      reffrenceDocumentId: ['', Validators.required],
      reffrenceDocumentType: '',
      createdOn: [''],
      modifiedOn: ['']

    });
  }
  get g() {
    return this.ledgerTransectionForm.controls;
  }

  // Add New General Ledger Transection Entry
  public save() {
    let ledgerFormdata = this.ledgerTransectionForm.value;
    ledgerFormdata.subAccountId = this.userLedgerRecord.subAccountId;
    ledgerFormdata.refCustomerSubAccountId = this.userLedgerRecord.refCustomerSubAccountId == null ? this.userLedgerRecord.refCustomerSubAccountId = 0 : this.userLedgerRecord.refCustomerSubAccountId;
    ledgerFormdata.refTradesmanSubAccountId = this.userLedgerRecord.refTradesmanSubAccountId == null ? this.userLedgerRecord.refTradesmanSubAccountId = 0 : this.userLedgerRecord.refTradesmanSubAccountId;
    ledgerFormdata.refSupplierSubAccountId = this.userLedgerRecord.refSupplierSubAccountId == null ? this.userLedgerRecord.refSupplierSubAccountId = 0 : this.userLedgerRecord.refSupplierSubAccountId;
    ledgerFormdata.credit = ledgerFormdata.credit == null ? ledgerFormdata.credit = 0 : ledgerFormdata.credit;
    ledgerFormdata.debit = ledgerFormdata.debit == null ? ledgerFormdata.debit = 0 : ledgerFormdata.debit;
    ledgerFormdata.active = this.userLedgerRecord.active;
    ledgerFormdata.reffrenceDocumentType = this.userLedgerRecord.reffrenceDocumentType;
    ledgerFormdata.createdOn = this.userLedgerRecord.createdOn;
    ledgerFormdata.createdBy = this.decodedtoken.UserId;
    ledgerFormdata.modifiedBy = this.decodedtoken.UserId;
    console.log(ledgerFormdata);
    this.Loader.show();
    this.common.PostData(this.common.apiRoutes.PackagesAndPayments.AddAndUpdateGeneralLedgerTransection, ledgerFormdata).then(response => {

      if (response.status == httpStatus.Ok) {
        this.toaster.success("Ledger Transection added Successfully !!", "Success");
        this.Loader.hide();
        this._modalService.dismissAll();
        this.ledgerTransectionForm.reset();
        this.showGeneralLedgerTransectionScreen();
      }
    });
  }

  // Select User Accoount Type

  public onAccountTypeChange(event) {
    this.selectedAccountType = event.target.value;
    console.log(this.selectedAccountType);
  }

 // Reset Ledger Form
  public resetLedgerForm() {
    this.ledgerTransectionForm.reset();
    this.ledgerTransectionForm.controls.accountId.setValue(0);
    this.ledgerTransectionForm.controls.debit.setValue(0);
    this.ledgerTransectionForm.controls.credit.setValue(0);
  }
}
