import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-tooltip-form',
  templateUrl: './tooltip-form.component.html',
  styleUrls: ['./tooltip-form.component.css']
})
export class TooltipFormComponent implements OnInit {
  public toolTipFormsList: any = [];
  public toolTipForm: FormGroup;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  public formData: any;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    
  constructor(public router: Router,public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService, ) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Tooltip Form"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.getToolTipFormsList();
    this.toolTipForm = this.fb.group({
      formId:[0],
      name: ['', [Validators.required, Validators.maxLength(50), Validators.minLength(5)]]
    })
  }
  public getToolTipFormsList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.UserManagement.GetToolTipFormsList).subscribe(res => {
      this.toolTipFormsList = res.json();
      this.Loader.hide();
    });
  }
  public handleSubmit() {
    this.Loader.show();
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    this.formData = this.toolTipForm.value;
    if (this.formData.formId <= 0) {
      this.formData.Action = 'add';
      this.formData.CreatedBy = decodedtoken.UserId;
    }
    else {
      this.formData.Action = 'update';
      this.formData.ModifiedBy = decodedtoken.UserId;
    }
    this.service.post(this.service.apiRoutes.UserManagement.AddUpdateToolTipForm, this.formData).subscribe(res => {
      let response = res.json();
      if (response.status == 200) {
        this.toastr.success(response.message, "Success");
        this.getToolTipFormsList();
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
    let obj = { Action: "delete", FormId: id, ModifiedBy: decodedtoken.UserId}
    this.service.post(this.service.apiRoutes.UserManagement.AddUpdateToolTipForm, obj).subscribe(res => {
      let response = res.json();
      if (response.status == 200) {
        this.toastr.success(response.message, "Success");
        this.getToolTipFormsList();
        this.Loader.hide();
      }
    });
  }

  get f() {
    return this.toolTipForm.controls;
  }
  resetForm() {
    this.toolTipForm.reset();
    this.toolTipForm.controls['formId'].setValue(0);
  }
}
