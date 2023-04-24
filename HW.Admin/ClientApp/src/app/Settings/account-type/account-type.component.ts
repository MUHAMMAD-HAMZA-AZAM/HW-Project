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
  selector: 'app-account-type',
  templateUrl: './account-type.component.html',
  styleUrls: ['./account-type.component.css']
})
export class AccountTypeComponent implements OnInit {
  public appValForm: FormGroup;
  public modelText: string = "Add Account Type";
  public decodedtoken: any;
  public formData: any;
  public submited: boolean = false;
  public accountTypeList: any = [];


  jwtHelperService: JwtHelperService = new JwtHelperService();
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    
  constructor(public router: Router,public _modalService: NgbModal, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Account Type"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.accountTypeForm();
    this.getAccountTypeList();
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
  }
  public accountTypeForm() {
    this.appValForm = this.fb.group({
      id: [0],
      accountTypeCode: ['9875', [Validators.required, Validators.maxLength(50)]],
      accounTypeName: ['test 1', [Validators.required,Validators.maxLength(50)]],
      isActive: [null, [Validators.required]],
    })
  }

  saveAndUpdate() {
    if (this.appValForm.invalid) {
      this.submited = true;
      return;
    }
    else {
      this.formData = this.appValForm.value;
      this.formData.active = this.formData.isActive == 1 ? true : false;
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
    this.service.PostData(this.service.apiRoutes.PackagesAndPayments.AddAndUpdateAccountType, data, true).then(res => {
      let response = res.json();
      if (response.status == httpStatus.Ok) {
        this.toastr.success(response.message, "Success");
        this._modalService.dismissAll();
        this.getAccountTypeList();
        this.resetForm();
        this.Loader.hide;
      }
    })
  }
  update(data,content) {
    this.Loader.show();
    this.showModal(content);
    this.modelText = "Update Account Type";
    data.isActive = data.active ? 1 : 2;
    this.appValForm.patchValue(data);
    this.Loader.hide();
  }
  delete(id) {
    let obj = { id, action: "delete", modifiedBy: this.decodedtoken.UserId}
    this.postData(obj);
  }
  public getAccountTypeList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.PackagesAndPayments.GetAccountTypeList).subscribe(res => {
      this.accountTypeList = res.json();
      this.Loader.hide();
    })
  }
  showModal(content) {
    this.modelText = "Add Account Type";
    let modRef = this._modalService.open(content, { backdrop: 'static', keyboard: false });
    modRef.result.then(() =>
    { this.resetForm() })
  }
  get f() {
    return this.appValForm.controls;
  }
  resetForm(): void {
    this.appValForm.reset();
    this.appValForm.controls['id'].setValue(0);
  }
}
