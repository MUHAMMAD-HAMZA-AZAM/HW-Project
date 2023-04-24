import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from "ngx-spinner";
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { httpStatus } from '../../Shared/Enums/enums';
import { CommonService } from '../../Shared/HttpClient/_http';
@Component({
  selector: 'app-paidpaymentslist',
  templateUrl: './paidpaymentslist.component.html',
  styleUrls: ['./paidpaymentslist.component.css']
})
export class PaidpaymentslistComponent implements OnInit {
  public authorizerForm: FormGroup;
  public withdrawalRequestList = [];
  public withdrawalRequestId: number;
  public withdrawalTradesmanId: number;
  public withdrawalAmount: number;
  public userId: string;
  public appValForm: FormGroup;
  public selectedUser: string = 'Customer';
  public role :string ='';
  public userTypes :string = 'Customer';
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  @ViewChild('postJobModalConfirm', { static: true }) postJobModal: ModalDirective;
  public jwtHelperService: JwtHelperService = new JwtHelperService();


  constructor(public fb: FormBuilder, public toastr: ToastrService, private router: Router, public service: CommonService, public Loader: NgxSpinnerService,
    public _modalService: NgbModal) {
  }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Paid Payments List"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.authForm();
    this.getAuthorizerList();
  }
  // Filter Authorize Form 
  public authForm() {
    this.appValForm = this.fb.group({
      tradesmanId: '',
      phoneNumber: '',
      userType : 'Customer'
    });
  }

  get f() {
    return this.appValForm.controls;
  }

  // Reset  Filter Authorize Form
  public resetForm() {
    this.appValForm.reset();
    this.appValForm.controls.tradesmanId.setValue(0);
    this.appValForm.controls.phoneNumber.setValue('');
    this.selectedUser = 'Customer';
    this.role = 'Customer';
    this.getAuthorizerList();
  }
 
  public getAuthorizerList() {
    this.checkUserType();
    let formData = this.appValForm.value;
    formData.tradesmanId = formData.tradesmanId == null ? formData.tradesmanId = 0 : formData.tradesmanId;
    formData.phoneNumber = formData.phoneNumber == null ? formData.phoneNumber = '' : formData.phoneNumber;
    this.Loader.show();
    this.service.get(this.service.apiRoutes.PackagesAndPayments.GetWithdrawalRequestList + "?paymentStatus=" + 5 + "&tradesmanId=" + formData.tradesmanId + "&phoneNumber=" + formData.phoneNumber + "&role=" + this.role).subscribe(response => {
      this.withdrawalRequestList = response.json();
      if (this.withdrawalRequestList.length == 0) {
        this.toastr.error("No Record Found !!", "Alert");
      }
      this.userTypes = this.role;
      this.Loader.hide();
    })
  }
  ApprovePayment(item, postJobModalConfirm) {
    this._modalService.open(postJobModalConfirm);
    this.withdrawalRequestId = item.withdrawalRequestId;
    this.withdrawalAmount = item.amount;
    this.withdrawalTradesmanId = item.tradesmanId;
  }
  hidemodal1() {
    this._modalService.dismissAll();
  }
  ConfirmJob() {
   
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    this.userId = decodedtoken.UserId;
    
    this.service.get(this.service.apiRoutes.PackagesAndPayments.UpdateWithdrawalRequestStatus + "?withdrawalRequestId=" + this.withdrawalRequestId + "&userId=" + this.userId + "&amount=" + this.withdrawalAmount + "&tradesmanId=" + this.withdrawalTradesmanId).subscribe(data => {
      var res = data.json();
      if (res.status == httpStatus.Ok) {
        this.toastr.success("Payment Authorize successfully", "Authorize");
        this._modalService.dismissAll();
        this.getAuthorizerList();
      }
    })
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
