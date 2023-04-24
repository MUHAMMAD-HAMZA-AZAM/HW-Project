import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-job-authorizer',
  templateUrl: './job-authorizer.component.html',
  styleUrls: ['./job-authorizer.component.css']
})
export class JobAuthorizerComponent implements OnInit {
  public authorizerForm: FormGroup;
  public jobAuthorizerList = [];
  public actionType = "added";
  public deleteId = 0;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };

  constructor(private router: Router,public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) {
  }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Job Authorizer"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.authorizerForm = this.fb.group({
      id:[0],
      userName: [' ', [Validators.required, Validators.maxLength(50), Validators.minLength(3)]],
      phoneNumber: ['', [Validators.required,Validators.minLength(11), Validators.pattern(/^[0-9]+$/)]],
      isActive: [false],
    })
    //this.authForm();
    this.getAuthorizerList();
  }
  public authForm() {
  }
  public getAuthorizerList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Jobs.AdminJobAuthorizerList).subscribe(response => {
      this.jobAuthorizerList = response.json();
      //console.log(response.json());
      this.Loader.hide();
    })
  }
  public save() {
    
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    let formData = this.authorizerForm.value;
    if (this.actionType == "added") {
      formData.action = "added";
      formData.createdBy = decodedtoken.UserId;
    }
    else if (this.actionType == "updated") {
      formData.action = "updated";
      formData.modifiedBy = decodedtoken.UserId;
    }
    else if (this.actionType == "deleted") {
      formData.action = "deleted";
      formData.id = this.deleteId
    }
    console.log(formData);
    this.service.post(this.service.apiRoutes.Jobs.JobAuthorizer, formData).subscribe(response => {
      var res = response.json();
      if (res.status == 200 && res.message == "added") {
        this.toastr.success("Data saved successfully!", "Added");
        this.getAuthorizerList();
        this.resetForm();
      }
      else if (res.status == 200 && res.message == "updated") {
        this.toastr.success("Data updated successfully!", "Updated");
        this.getAuthorizerList();
        this.resetForm();
        //this.actionType = "added";
      }
      else if (res.status == 200 && res.message == "deleted") {
        this.toastr.success("Data deleted successfully!", "Deleted");
        this.getAuthorizerList();
        this.actionType = "added";
      }
      else {
        this.toastr.error("Somthing went wrong!", "Error" )
      }
    })
  }
  resetForm() {
    this.authorizerForm.reset();
    this.authorizerForm.patchValue({
      id: 0,
      isActive: false,
    });
    this.actionType = "added";
  }
  public handleEdit(item) {
    this.authorizerForm.patchValue(item);
    this.actionType = "updated";
  }
  public handleDelete(id) {
    this.actionType = "deleted";
    this.deleteId = id;
    this.save();
  }
  get f() {
    return this.authorizerForm.controls;
  }
}
