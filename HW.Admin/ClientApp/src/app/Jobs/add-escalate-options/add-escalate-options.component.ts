import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { error } from 'pdf-lib';
import { httpStatus } from '../../Shared/Enums/enums';
import { CommonService } from '../../Shared/HttpClient/_http';
import { SortList } from '../../Shared/Sorting/sortList';

@Component({
  selector: 'app-add-escalate-options',
  templateUrl: './add-escalate-options.component.html',
  styleUrls: ['./add-escalate-options.component.css']
})
export class AddEscalateOptionsComponent implements OnInit {
  public formData: any;
  public modelName: string;
  public appValForm: FormGroup;
  public escalateOptionsList = [];
  public decodedtoken: any;
  public allowview;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(
    public _modalService: NgbModal,
    public toastr: ToastrService,
    public common: CommonService,
    public Loader: NgxSpinnerService,
    public fb: FormBuilder,
    public sortList: SortList,
    private router: Router) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Add Escalate Options"));
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.appValForm = this.fb.group({
      id: [0],
      name: ['', [Validators.required]],
      userRole: [null, [Validators.required]],
      active: [false, [Validators.required]],
      createdOn: [null],
      createdBy: [''],
      modifiedOn: [null],
      modifiedBy: ['']
    });
    this.populateEscalateOptionsList();
  }

  //------------------- Show Escalate Issues List 
  public populateEscalateOptionsList() {
    this.Loader.show();
    this.common.get(this.common.apiRoutes.Jobs.GetEscalateOptionsList).subscribe(result => {
      this.escalateOptionsList = result.json();
      this.Loader.hide();

    }, error => {

      this.Loader.show();
      console.log(error);
    });
  }

  //------------------- Add & Update 
  public addAndUpdate() {
    if (this.appValForm.invalid) {
      return;
    }
    else {
      this.formData = this.appValForm.value;
    
      if (this.formData.id <= 0) {
        //add Escalate Option
        this.formData.action = 'add';
        this.formData.userId = this.decodedtoken.UserId;
        console.log(this.formData);
        this.Loader.show();
        debugger;
        this.common.PostData(this.common.apiRoutes.Jobs.AddAndUpdateEscalateOption, this.formData, true).then(result => {
          let res = result.json();
          if (res.status == httpStatus.Ok) {
            this._modalService.dismissAll();
            this.populateEscalateOptionsList();
            this.resetForm();
            this.Loader.hide();
          }
        }, error => {
          this.Loader.show();
          console.log(error);
        });
      }
      else {
        //update Escalate Option
        this.formData.userId = this.decodedtoken.UserId;
        this.formData.action = "update";
        console.log(this.formData);
        debugger;
        this.common.PostData(this.common.apiRoutes.Jobs.AddAndUpdateEscalateOption, this.formData,true).then(result => {
          let res = result.json();
          if (res.status == httpStatus.Ok) {
            this._modalService.dismissAll();
            this.populateEscalateOptionsList();
            this.resetForm();
            this.Loader.hide();
          }
        }, error => {
          this.Loader.show();
          console.log(error);
        });
      }
    }
  }
  //-------------------  Show Modal For Update 
  public update(data, content) {

    this.Loader.show();
    this.showModal(content);
    this.modelName = "Update Escalate Option";
    this.appValForm.patchValue(data);
    this.Loader.hide();
  }

  public delete(escalateId) {
    this.Loader.show();
    let obj = {
      id: escalateId,
      action: "delete",
      userId: this.decodedtoken.UserId
    };
    this.formData = obj;
    this.Loader.show();
    debugger;
    this.common.post(this.common.apiRoutes.Jobs.AddAndUpdateEscalateOption, this.formData).subscribe(result => {
      let res = result.json();
      if (res.status == httpStatus.Ok) {
        this._modalService.dismissAll();
        this.populateEscalateOptionsList();
        this.resetForm();
        this.Loader.hide();
      }
    }, error => {
      this.Loader.show();
      console.log(error);
    });
  }

  //-------------------  Show Modal For Add
  public showModal(content) {
    this.modelName = "Add New Escalate Option";
     this._modalService.open(content, { backdrop: 'static', keyboard: false });
  }

  //-------------------  Reset Form
  public resetForm() {
    this.appValForm.reset();
    this.appValForm.controls.id.setValue(0);
  }
  get f() {
    return this.appValForm.controls;
  }

  
}
