import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-declined-withdraw-request',
  templateUrl: './declined-withdraw-request.component.html',
  styleUrls: ['./declined-withdraw-request.component.css']
})
export class DeclinedWithdrawRequestComponent implements OnInit {
  public appValForm: FormGroup;
  public declinedRequestList = [];
public selectedUser: string = 'Customer';
  public role :string ='';
  public userTypes :string = 'Customer';
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  constructor(public fb: FormBuilder, public router: Router, public service: CommonService, public Loader: NgxSpinnerService, public toastr: ToastrService) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Declined Withdraw Request"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.declinedForm();
    this.getDeclinedRequestList();
  }

  // Filter Declined Form 
  public declinedForm() {
    this.appValForm = this.fb.group({
      tradesmanId: '',
      phoneNumber:'',
      userType : 'Customer'
    });
  }

  get f() {
    return this.appValForm.controls;
  }

  public getDeclinedRequestList() {
    this.checkUserType();   
    let formData = this.appValForm.value;
    formData.tradesmanId = formData.tradesmanId == null ? formData.tradesmanId = 0 : formData.tradesmanId;
    formData.phoneNumber = formData.phoneNumber == null  ? formData.phoneNumber = '' : formData.phoneNumber;
    this.Loader.show();
    this.service.get(this.service.apiRoutes.PackagesAndPayments.GetWithdrawalRequestList + "?paymentStatus=" + 4 + "&tradesmanId=" + formData.tradesmanId + "&phoneNumber=" + formData.phoneNumber + "&role=" + this.role).subscribe(response => {
      this.declinedRequestList = response.json();
      if (this.declinedRequestList.length == 0) {
        this.toastr.error("No Record Found !!", "Alert");
      }
      this.userTypes = this.role;
      this.Loader.hide();
    }, error => {
      console.log(error);
      this.Loader.show();
    });
  }

  // Reset  Filter Authorize Form
  public resetForm() {
    this.appValForm.reset();
    this.appValForm.controls.tradesmanId.setValue(0);
    this.appValForm.controls.phoneNumber.setValue('');
    this.selectedUser = 'Customer';
    this.role = 'Customer';
    this.getDeclinedRequestList();
  }
selectChangeHandlerForUser(event: any) {

    this.selectedUser = event.target.value;
  }
checkUserType() {
    debugger;
    if (this.selectedUser == "Tradesman")
      this.role = "Tradesman"
    else if (this.selectedUser == "Supplier")
      this.role = "Supplier"
    else if (this.selectedUser == "Customer")
      this.role = "Customer"
    else {
      this.role = ''
    }
  }

}
