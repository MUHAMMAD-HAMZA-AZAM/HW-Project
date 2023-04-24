import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { aspNetUserRoles, httpStatus, loginsecurity } from '../../Shared/Enums/enums';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-user-receipts',
  templateUrl: './user-receipts.component.html',
  styleUrls: ['./user-receipts.component.css']
})
export class UserReceiptsComponent implements OnInit {
  public tradesmanNoDataFound: boolean = false;
  public customerNoDataFound: boolean = false;
  public showTradesmanReceipts: boolean = false;
  public showCustomerReceipts: boolean = false;
  public customerPaymentReceipts = [];
  public tradesmanPaymentReceipts = [];
  public userType: any = aspNetUserRoles;
  public appValForm: FormGroup;
  public dataNotFound: boolean = false;
  public decodedtoken: any;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  jwtHelperService: JwtHelperService = new JwtHelperService();
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
    this.filterUserReceiptsForm();
  }

  // User Receipts Form
  public filterUserReceiptsForm() {
    this.appValForm = this.fb.group({
      userTypeId: [null, Validators.required],
      userId: [null, Validators.required],
    });

  }

  get f() {
    return this.appValForm.controls;
  }

  // Show User Receipts 
  public showUserReceipts() {
    let formData = this.appValForm.value;
    console.log(formData);
    if (formData.userTypeId == aspNetUserRoles.CRole) {
      this.Loader.show();
      this.common.get(this.common.apiRoutes.Users.GetUserPaymentRecords + '?customerId=' + formData.userId).subscribe(result => {
        this.customerPaymentReceipts = result.json();
        console.log(this.customerPaymentReceipts);
        if (!this.customerPaymentReceipts) {
          this.toaster.error("No Data Found !!");
          this.Loader.hide();
          this.customerNoDataFound = true;

        }
        else {
          this.showCustomerReceipts = true;
          this.showTradesmanReceipts = false;
        }
        this.Loader.hide();
      }, error => {
        console.log(error);
        this.Loader.show();
      });
    }
    else if (formData.userTypeId == aspNetUserRoles.TRole || aspNetUserRoles.ORole) {
      this.Loader.show();
      this.common.get(this.common.apiRoutes.TrdesMan.GetTradesmanPaymentRecords + '?tradesmanId=' + formData.userId).subscribe(result => {
        this.tradesmanPaymentReceipts = result.json();
        console.log(this.tradesmanPaymentReceipts);
        if (!this.tradesmanPaymentReceipts) {
          this.toaster.error("No Data Found !!");
          this.Loader.hide();
          this.tradesmanNoDataFound = true;

        }
        else {
          this.showCustomerReceipts = false;
          this.showTradesmanReceipts = true;
        }
        this.Loader.hide();
      }, error => {
        console.log(error);
        this.Loader.show();
      });
    }
    else {
      console.log("Show Supplier Invoices");
    }
  }

  // Reset Receipts Filter Form
  public resetForm() {
    this.appValForm.reset();
    this.appValForm.controls.userTypeId.setValue(null);
    this.appValForm.controls.userId.setValue(null);
    this.showCustomerReceipts = false;
    this.showTradesmanReceipts = false;
  }

}
