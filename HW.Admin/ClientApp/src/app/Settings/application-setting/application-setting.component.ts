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
  selector: 'app-application-setting',
  templateUrl: './application-setting.component.html',
  styleUrls: ['./application-setting.component.css']
})
export class ApplicationSettingComponent implements OnInit {
  public appValForm: FormGroup;
  public modelText: string = "Add Referral";
  public decodedtoken: any;
  public formData: any;
  public submited: boolean = false;
  public btnReset: boolean = false;
  public settingList: any = [];


  jwtHelperService: JwtHelperService = new JwtHelperService();
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  constructor(public router: Router, public _modalService: NgbModal, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Application Setting"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    this.apllicationSettingForm();
    this.getSettingList();
  }
  public apllicationSettingForm() {
    this.appValForm = this.fb.group({
      applicationSettingId: [0],
      settingName: ["", [Validators.required]],
      isActive: [false, [Validators.required]],
     
    })
  }

  saveAndUpdate() {
    
    if (this.appValForm.invalid) {
      this.submited = true;
      return;
    }
    else {
      this.formData = this.appValForm.value;
      if (this.formData.applicationSettingId <= 0) {
        //add
        this.formData.action = 'add';
        this.formData.userId = this.decodedtoken.UserId;
        this.postData(this.formData);
      }
      else {
        //update
        this.formData.userId = this.decodedtoken.UserId;
        this.formData.action = "update";
        this.postData(this.formData);
      }
    }
  }
  public postData(data) {
    this.Loader.show();
    this.service.PostData(this.service.apiRoutes.UserManagement.AddAndUpdateApplicationSetting, data, true).then(res => {
      let response = res.json();
      if (response.status == httpStatus.Ok) {
        this.toastr.success(response.message, "Success");
        this._modalService.dismissAll();
        this.getSettingList();
        this.resetForm();
        this.Loader.hide;
   this.btnReset=false;
      }
    })
  }
  update(data, content) {
    
    this.Loader.show();
    this.showModal(content);
    this.btnReset=true;
    this.modelText = "Update Setting";
    this.appValForm.patchValue(data);
    this.Loader.hide();
  }
  delete(applicationSettingId) {
  
    let obj = { applicationSettingId, action: "delete", userId: this.decodedtoken.UserId }
    this.postData(obj);
  }
  public getSettingList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.UserManagement.GetSettingList).subscribe(res => {
      this.settingList = res.json();
      this.Loader.hide();
    })
  }
  showModal(content) {
    this.modelText = "Add New Setting";
    let modRef = this._modalService.open(content, { backdrop: 'static', keyboard: false });
    modRef.result.then(() => { this.resetForm() })
  this.btnReset=false;
  }
  get f() {
    return this.appValForm.controls;
  }
  resetForm(): void {
    this.appValForm.reset();
    this.appValForm.controls['applicationSettingId'].setValue(0);
  }

}
