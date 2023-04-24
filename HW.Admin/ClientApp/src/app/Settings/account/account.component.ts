import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../Shared/HttpClient/_http';
import { NgxSpinnerService } from "ngx-spinner";
import { DatePipe } from '@angular/common';
import { httpStatus } from '../../Shared/Enums/enums';
import { Router } from '@angular/router';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {
  public appValForm: FormGroup;
  public modelText: string = "Add New Account";
  public decodedtoken: any;
  public formData: any;
  public submited: boolean = false;
  public accountTypeList: any = [];
  public accountList: any = [];
  public activeAccountTypeList: any = [];
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    


  jwtHelperService: JwtHelperService = new JwtHelperService();

  constructor(public router: Router,public _modalService: NgbModal, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Account"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.accountForm();
    this.getAccountTypeList();
    this.getAccountList();
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
  }
  public accountForm() {
    this.appValForm = this.fb.group({
      id: [0],
      accountNo: ['00', [Validators.required, Validators.maxLength(50)]],
      accountName: ['this is account name', [Validators.required, Validators.maxLength(50)]],
      accountTypeId: [null, [Validators.required, Validators.maxLength(50)]],
      isActive: [null, [Validators.required]],
    })
  }

  saveAndUpdate() {
    ;
    if (this.appValForm.invalid) {
      this.submited = true;
      return;
    }
    else {
      this.formData = this.appValForm.value;
      this.formData.active = this.formData.isActive == 1 ? true : false;
      ;
      if (this.formData.id <= 0) {
        //add
        this.formData.action = 'add';
        this.formData.createdBy = this.decodedtoken.UserId;
        this.postData(this.formData);
      }
      else {
        //update
        this.formData.modifiedBy = this.decodedtoken.UserId;
        this.formData.action = "update";
        this.postData(this.formData);
      }
    }
  }
  public postData(data) {
    this.Loader.show();
    this.service.PostData(this.service.apiRoutes.PackagesAndPayments.AddAndUpdateAccount, data, true).then(res => {
      let response = res.json();
      if (response.status == httpStatus.Ok) {
        this.toastr.success(response.message, "Success");
        this._modalService.dismissAll();
        this.getAccountList();
        this.resetForm();
        this.Loader.hide;
      }
    })
  }
  update(data, content) {
    this.Loader.show();
    this.showModal(content);
    this.modelText = "Update Account";
    data.isActive = data.active ? 1 : 2;
    this.appValForm.patchValue(data);
    this.Loader.hide();
  }
  delete(id) {
    ;
    let obj = { id, action: "delete", modifiedBy: this.decodedtoken.UserId }
    this.postData(obj);
  }
  public getAccountList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.PackagesAndPayments.GetAccountList).subscribe(res => {
      this.accountList = res.json();
      this.Loader.hide();
    })
  }
  public getAccountTypeList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.PackagesAndPayments.GetAccountTypeList).subscribe(res => {
      this.accountTypeList = res.json();
      if (this.accountTypeList != null) {
        this.activeAccountTypeList = this.accountTypeList.filter(x => x.active);
      }
      this.Loader.hide();
    })
  }
  showModal(content) {
    this.modelText = "Add New Account";
    let modRef = this._modalService.open(content, { backdrop: 'static', keyboard: false });
    modRef.result.then(() => { this.resetForm() })
  }
  get f() {
    return this.appValForm.controls;
  }
  resetForm(): void {
    this.appValForm.reset();
    this.appValForm.controls['id'].setValue(0);
  }
}
