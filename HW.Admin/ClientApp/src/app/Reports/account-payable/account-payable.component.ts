import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { GLAccounts, httpStatus } from '../../Shared/Enums/enums';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-account-payable',
  templateUrl: './account-payable.component.html',
  styleUrls: ['./account-payable.component.css']
})
export class AccountPayableComponent implements OnInit {
  public selectedDocumentype: any;
  public accounts: any = GLAccounts;
  public appValForm: FormGroup;
  public decodedtoken: any;
  public usersName = [];
  public chartOfAccountList = [];
  public debitsum: Number = 0;
  public creditsum: Number = 0;
  public totalsum: Number = 0;
  public userName: string = '';
  public submitted: boolean = false;
  public hide: boolean = false;
  keyword = 'userName';
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

    this.appValForm = this.fb.group({
      documentType: '',
      fromDate: '',
      toDate: '',
      userName: ['', Validators.required],
      userId: '',
      jobQuotationId: ''
    });

    this.getAccountReceiveableUserNames();
  }

  get f() {
    return this.appValForm.controls;
  }

  public getAccountReceiveableUserNames() {
    this.Loader.show();
    this.common.get(this.common.apiRoutes.PackagesAndPayments.GetAccountUsersName + "?accountType=" + this.accounts.Payable).subscribe(result => {
      var responce = result.json();
      this.usersName = responce.resultData;
      debugger
    });
    this.Loader.hide();
  }
  //  // General Ledger Transection Screen Display
  public showDetailedLedgerTransectionScreen() {
    this.submitted = true;
    let formData = this.appValForm.value;
    if (!this.appValForm.invalid) {
      this.userName = '';
      this.debitsum = 0;
      this.creditsum = 0;
      if (typeof formData.userName == "object")
        formData.userName = formData.userName ? formData.userName.userName : '';
      else
        formData.userName = formData.userName ? formData.userName : '';
      formData.accountId = this.accounts.Payable;
      this.Loader.show();
      this.common.PostData(this.common.apiRoutes.PackagesAndPayments.GetChartOfAccount, JSON.stringify(formData)).then(result => {
        var responce = result.json();
        if (responce.status == httpStatus.Ok) {
          this.hide = true;
          this.chartOfAccountList = responce.resultData;
          this.userName = this.chartOfAccountList[0].userName;
          for (let item of this.chartOfAccountList) {
            this.debitsum += item.debit
            this.creditsum += item.credit

          }
          this.totalsum = Number(this.debitsum) - Number(this.creditsum);
        }
        else {
          this.hide = false;
          this.toaster.error("No Record Found !!");
          this.chartOfAccountList = responce.resultData;
        }

        this.Loader.hide();
      });
    }
    else {

    }
    

  }
  // Reset FilterTransection Form 
  public resetFilterForm() {
    this.submitted = false;
    this.appValForm.reset();
  }

  // Select User Accoount Type

  public onDocumentTypeChange(event) {
    this.selectedDocumentype = event.target.value;
  }
  selectEvent(item) {

  }
  unselectEvent(item) {

  }
}
