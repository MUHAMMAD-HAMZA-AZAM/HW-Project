import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { httpStatus } from '../../Shared/Enums/enums';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-journal-entry',
  templateUrl: './journal-entry.component.html',
  styleUrls: ['./journal-entry.component.css']
})
export class JournalEntryComponent implements OnInit {
  public appValForm: FormGroup;
  public accounts = [];
  public totalDebit = 0;
  public totalCredit = 0;
  keyword = 'name';
  public journalEntrySavedId: number;
  public userId: string;
  public disableBtn: boolean = false;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(public _modalService: NgbModal, public fb: FormBuilder, public service: CommonService, public toastr: ToastrService, public Loader: NgxSpinnerService) { }

  ngOnInit() {
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    this.userId = decodedtoken.UserId;
    this.appValForm = this.fb.group({
      id: [0],
      date: ['', Validators.required],
      referenceNo: ['', Validators.required],
      narration: ['', Validators.required],
      notes: ['', Validators.required],
      journalEntry: this.fb.array([this.initJEFormGroup()]),
    })
    this.getAccountNames();
  }
  addNewRow() {
    (<FormArray>this.appValForm.get('journalEntry')).push(this.initJEFormGroup());
  }
  initJEFormGroup(): FormGroup {    return this.fb.group({      accountName: ['', [Validators.required]],      description: ['', [Validators.required]],      debit: [''],      credit: [''],      tax: ['']    })  }
  get journalEntryControls() {
    return this.appValForm ? this.appValForm.get('journalEntry') as FormArray : null
  }
  get f() {
    return this.appValForm.controls;
  }
  removeJournalEntryFromGrop(index): void {
    (<FormArray>this.appValForm.get('journalEntry')).removeAt(index);
  }
  Save() {
    
    let formData = this.appValForm.value;
    if (this.appValForm.valid) {
      formData.userId = this.userId;
      
      this.service.PostData(this.service.apiRoutes.ChartOfAccounts.AddJournalEntry, JSON.stringify(formData), true).then(result => {
        let responce = result.json();
        
        if (responce.status == httpStatus.Ok) {
          this.journalEntrySavedId = responce.resultData;
          
          this.disableBtn = true;
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
    
  public getAccountNames() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.PackagesAndPayments.GetSubAccountsLastLevel).subscribe(result => {
      var responce = result.json();
      this.accounts = responce.resultData;
    });
    this.Loader.hide();
  }

  calculateDebit() {
    var journalLinesEntries: any = this.appValForm.value.journalEntry;
    this.totalDebit = journalLinesEntries.reduce((accumulator, current) => accumulator + Number(current.debit), 0);
  }
  calculateCredit() {
    var journalLinesEntries: any = this.appValForm.value.journalEntry;
    this.totalCredit = journalLinesEntries.reduce((accumulator, current) => accumulator + Number(current.credit), 0);
  }
  Post() {
    let obj = {
      id: this.journalEntrySavedId,
      userId: this.userId
    }
    
    this.service.PostData(this.service.apiRoutes.ChartOfAccounts.AddLeadgerTransactionEntry, JSON.stringify(obj), true).then(result => {
      let responce = result.json();
      
      if (responce.status == httpStatus.Ok) {
        this.resetForm();
        this.toastr.success("Data Posted Successfully.", "Success");
      }
      else {
        this.toastr.error("Something went wrong.", "Error");
      }
    })
  }
  resetForm() {
    this.appValForm.reset();
    (<FormArray>this.appValForm.get('journalEntry')).clear();
    (<FormArray>this.appValForm.get('journalEntry')).push(this.initJEFormGroup());
    this.totalDebit = 0;
    this.totalCredit = 0;
    this.journalEntrySavedId = 0;
    this.disableBtn = false;
  }
}
