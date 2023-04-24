import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { GLAccounts, GLAccountTypes, httpStatus } from '../../Shared/Enums/enums';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-general-account',
  templateUrl: './general-account.component.html',
  styleUrls: ['./general-account.component.css']
})
export class GeneralAccountComponent implements OnInit {
  public selectedDocumentype: any;
  public accounts: any = GLAccounts;
  public appValForm: FormGroup;
  public decodedtoken: any;
  public chartOfAccountList = [];
  public debitsum: Number = 0;
  public creditsum: Number = 0;
  public totalsum: Number = 0;
  public hide: boolean = false;
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
      jobQuotationId: ''
    });

  }

  // Detailed Ledger Transection Form
  get f() {
    return this.appValForm.controls;
  }

  //  // General Ledger Transection Screen Display
  public showDetailedLedgerTransectionScreen() {
    let formData = this.appValForm.value;
    formData.accountId = this.accounts.Assets;
    this.Loader.show();
    this.common.PostData(this.common.apiRoutes.PackagesAndPayments.GetChartOfAccount, JSON.stringify(formData)).then(result => {
      var responce = result.json();
      if (responce.status == httpStatus.Ok) {
        this.hide = true;
        this.chartOfAccountList = responce.resultData;
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
  // Reset FilterTransection Form 
  public resetFilterForm() {
    this.appValForm.reset();
  }


}
