import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from "ngx-spinner";
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { httpStatus, loginsecurity } from '../../Shared/Enums/enums';
import { CommonService } from '../../Shared/HttpClient/_http';
import { ResponseVm } from '../../Shared/Models/HomeModel/HomeModel';
@Component({
  selector: 'app-withdrawalrequest',
  templateUrl: './withdrawalrequest.component.html',
  styleUrls: ['./withdrawalrequest.component.css']
})
export class WithdrawalrequestComponent implements OnInit {
  public response: ResponseVm;
  public authorizerForm: FormGroup;
  public withdrawalRequestList = [];
  public withdrawalRequestId: number;
  public id: number;
  public withdrawalAmount: number;
  public userId: string;
  public decodedToken;
  public closeResult;
  public appValForm: FormGroup;
  public role :string ='';
  public userTypes :string = 'Customer';
  public userNotExist: boolean = false;

  public selectedUser: string = 'Customer';

  public withdrawlRequestForm: FormGroup;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  @ViewChild('postJobModalConfirm', { static: true }) postJobModal: ModalDirective;
  public jwtHelperService: JwtHelperService = new JwtHelperService();


  constructor(public fb: FormBuilder, public toastr: ToastrService, private router: Router, public service: CommonService, public Loader: NgxSpinnerService,
    public _modalService: NgbModal) {
  }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Withdrawal Request"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.decodedToken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    console.log(this.decodedToken);
    this.userId = this.decodedToken.UserId;
    this.authForm();
    this.adminWithDrawlRequestForm();
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

  // Payment Withdrawal Request List
  public getAuthorizerList() {
    this.checkUserType();
    let formData = this.appValForm.value;
    formData.tradesmanId = formData.tradesmanId == null ? formData.tradesmanId = 0 : formData.tradesmanId;
    formData.phoneNumber = formData.phoneNumber == null ? formData.phoneNumber = '' : formData.phoneNumber;

    this.Loader.show();
    debugger;
    this.service.get(this.service.apiRoutes.PackagesAndPayments.GetWithdrawalRequestList + "?paymentStatus=" + 1 + "&tradesmanId=" + formData.tradesmanId + "&phoneNumber=" + formData.phoneNumber + "&role=" + this.role ).subscribe(response => {
      this.withdrawalRequestList = response.json();
      if (this.withdrawalRequestList.length == 0) {
        this.toastr.error("No Record Found !!", "Alert");
      }
      this.Loader.hide();
      this.userTypes = this.role;
    });
  }

  // Approved Payment Withdrawal Request List
  ApprovePayment(item, postJobModalConfirm) {
    this._modalService.open(postJobModalConfirm);
    this.withdrawalRequestId = item.withdrawalRequestId;
    this.withdrawalAmount = item.amount;
    this.id = item.role == loginsecurity.CRole ? item.customerId : item.role == loginsecurity.TRole ? item.tradesmanId : item.role == loginsecurity.SRole ? item.supplierId : 0;
    this.role = item.role;
    //if (item.tradesmanId) {
    //}
    //else if(item.role=="Supplier") {
    //  this.role = "Supplier";
    //}
   
  }
  hidemodal1() {
    this._modalService.dismissAll();
  }
  ConfirmJob() {

    this.service.get(this.service.apiRoutes.PackagesAndPayments.UpdateWithdrawalRequestStatus + "?withdrawalRequestId=" + this.withdrawalRequestId + "&userId=" + this.userId + "&amount=" + this.withdrawalAmount + "&tradesmanId=" + this.id + "&role=" + this.role).subscribe(data => {
      var res = data.json();

      if (res.status == httpStatus.Ok) {
        this.toastr.success("Payment Authorize successfully", "Authorize");
        this._modalService.dismissAll();
        this.getAuthorizerList();
      }
    })
  }
  public declinePaymentRequest(item, declineRequestModal) {
    let modref = this._modalService.open(declineRequestModal);
    modref.result.then((result) => {

      this.closeResult = result;
      if (this.closeResult == 'yes') {
        this.service.get(this.service.apiRoutes.PackagesAndPayments.DeclineWithdrawRequest + "?withdrawalRequestId=" + item.withdrawalRequestId + "&userId=" + this.userId).subscribe(data => {
          var res = data.json();
          if (res.status == httpStatus.Ok) {
            this.toastr.success("Withdrawal request declined successfully!", "Withdrawal Request");
            this._modalService.dismissAll();
            this.getAuthorizerList();
          }
        })
      }
    }, (reason) => {
      this.closeResult = reason;
    });
  }

  // ------------------------------ Add New Withdrawal List ----------------------------------

  public adminWithDrawlRequestForm() {
    this.withdrawlRequestForm = this.fb.group({
      tradesmanId: ['', Validators.required],
      tradesmanName: ['', Validators.required],
      phoneNumber: ['', [Validators.required, Validators.minLength(11)]],
      cnic: ['', [Validators.required, Validators.minLength(13)]],
      amount: ['', Validators.required]
    });
  }

  get g() {
    return this.withdrawlRequestForm.controls;
  }

  // Show Modal For Clearance Request
  public showModal(content) {
    this.withdrawlRequestForm.reset();
    this._modalService.open(content)
  }

  // Add New Clearance Request

  public addAndClearanceWithdrawlRequest() {

    let formData = this.withdrawlRequestForm.value;
    formData.paymentStatusId = 1;
    formData.createdBy = this.userId;
    formData.customerId = formData?.customerId ? formData.customerId : 0;
    formData.tradesmanId = formData?.tradesmanId ? Number(formData.tradesmanId) : 0;
    formData.supplierId = formData?.supplierId ? formData.supplierId : 0;
   // console.log(formData);
    let obj = {
      id: formData.tradesmanId,
    };
   // console.log(obj);
    this.Loader.show();

    this.service.PostData(this.service.apiRoutes.UserManagement.CheckUserExist, JSON.stringify(obj)).then(res => {
      this.response = JSON.parse(res.json());
      console.log(this.response);
      if (this.response.status == httpStatus.Ok) {
        if (this.response.resultData.tradesmanId == Number(formData.tradesmanId)) {
          formData.tradesmanId = formData?.tradesmanId ? (formData.tradesmanId).toString() : 0;
          console.log(formData);
          console.log("Tradesman Existed !!");
          this.service.post(this.service.apiRoutes.PackagesAndPayments.addPaymentWithdrawalRequest, formData).subscribe(res => {
            this.response = res.json();
            if (this.response.status == httpStatus.Ok) {
              this.toastr.success(this.response.message);
              this._modalService.dismissAll();
              this.getAuthorizerList();
            }
          }, error => {
            console.log(error);
          });
        }
        else {
          this.userNotExist = true;
          setTimeout(() => {
            this.userNotExist = false;
          },3000);
          console.log("Tradesman Not Existed !!");
          this.Loader.hide();
        }
        
      }
    }, error => {
      console.log(error);
    });


  }

  // Reset Clearance Form
  public resetClearanceWithdrawlRequestForm() {
    this.withdrawlRequestForm.reset();
    this.withdrawlRequestForm.controls.tradesmanId.setValue(0);
    this.withdrawlRequestForm.controls.phoneNumber.setValue(0);
    this.withdrawlRequestForm.controls.cnic.setValue(0);
    this.withdrawlRequestForm.controls.amount.setValue(0);
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





