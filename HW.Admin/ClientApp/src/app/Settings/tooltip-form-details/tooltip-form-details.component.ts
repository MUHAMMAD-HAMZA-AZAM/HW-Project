import { Component, OnInit, isDevMode } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-tooltip-form-details',
  templateUrl: './tooltip-form-details.component.html',
  styleUrls: ['./tooltip-form-details.component.css']
})
export class TooltipFormDetailsComponent implements OnInit {
  public toolTipFormsList: any = [];
  public toolTipFormsDetailList: any = [];
  public activeToolTipFormsList: any = [];
  public toolTipFormSingleDetail: any = [];
  public toolTipForm: FormGroup;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  public formData: any;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    
  constructor(public router: Router,public _modalService: NgbModal, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService, ) { }

  ngOnInit() {
  this.userRole = JSON.parse(localStorage.getItem("Tooltip Form Details"));
  if (!this.userRole.allowView)
    this.router.navigateByUrl('/login');
    this.getToolTipFormsList();
    this.getToolTipFormsDetailList();
    this.toolTipForm = this.fb.group({
      formDetailId:[0],
      formId: [null, [Validators.required]],
      name: ['', [Validators.required, Validators.maxLength(50), Validators.minLength(5)]],
      description: ['', [Validators.required, Validators.minLength(5)]],
    })
  }
  formDetails(id, ref) {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.UserManagement.GetSingleFormDetails+"?id="+ id).subscribe(res => {
      this.toolTipFormSingleDetail = res.json();
      this._modalService.open(ref, { size: 'xl'});
      this.Loader.hide();
    });
  }
  public getToolTipFormsDetailList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.UserManagement.GetToolTipFormsDetailsList).subscribe(res => {
      this.toolTipFormsDetailList = res.json();
      this.Loader.hide();
    });
  }
  public getToolTipFormsList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.UserManagement.GetToolTipFormsList).subscribe(res => {
      this.toolTipFormsList = res.json();
      this.activeToolTipFormsList = this.toolTipFormsList.filter(x => x.isActive);
      console.log(this.activeToolTipFormsList);
      this.Loader.hide();
    });
  }
  public handleSubmit() {
    this.Loader.show();
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    this.formData = this.toolTipForm.value;
    if (this.formData.formDetailId <= 0) {
      this.formData.Action = 'add';
      this.formData.CreatedBy = decodedtoken.UserId;
    }
    else {
      this.formData.Action = 'update';
      this.formData.ModifiedBy = decodedtoken.UserId;
    }
    this.service.post(this.service.apiRoutes.UserManagement.AddUpdateToolTipFormDetails, this.formData).subscribe(res => {
      let response = res.json();
      if (response.status == 200) {
        this.toastr.success(response.message, "Success");
        this.getToolTipFormsDetailList();
        this.resetForm();
        this.Loader.hide();
      }
    });
  }
  update(data) {
    this.toolTipForm.patchValue(data);
  }
  delete(id) {
    this.Loader.show();
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    let obj = { Action: "delete", FormDetailId: id, ModifiedBy: decodedtoken.UserId }
    this.service.post(this.service.apiRoutes.UserManagement.AddUpdateToolTipFormDetails, obj).subscribe(res => {
      let response = res.json();
      if (response.status == 200) {
        this.toastr.success(response.message, "Success");
        this.getToolTipFormsDetailList();
        this.Loader.hide();
      }
    });
  }
  get f() {
    return this.toolTipForm.controls;
  }
  resetForm() {
    this.toolTipForm.reset();
    this.toolTipForm.controls['formDetailId'].setValue(0);
  }
}
