import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../Shared/HttpClient/_http';
import { NgxSpinnerService } from "ngx-spinner";
import { httpStatus } from '../../Shared/Enums/enums';
import { Router } from '@angular/router';
@Component({
  selector: 'app-campaigns',
  templateUrl: './campaigns.component.html',
  styleUrls: ['./campaigns.component.css']
})
export class CampaignsComponent implements OnInit {

  public appValForm: FormGroup;
  public modelText: string = "Add Compaign";
  public decodedtoken: any;
  public formData: any;
  public submited: boolean = false;
  public compaignsList: any = [];


  jwtHelperService: JwtHelperService = new JwtHelperService();
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };

  constructor(public router: Router, public _modalService: NgbModal, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Campaigns List"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.compaignForm();
    this.getCompaignsList();
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
  }
  public compaignForm() {
    this.appValForm = this.fb.group({
      campaignId: [0],
      fullName: ['', [Validators.required]],
      shortName: ['', [Validators.required]],
      description: ['', [Validators.required]],
      startDate: ['', [Validators.required]],
      endDate: ['', [Validators.required]],
      isActive: [false, [Validators.required]],
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
      if (this.formData.campaignId <= 0) {
        //add
        this.formData.action = 'Add';
        this.formData.createdBy = this.decodedtoken.UserId;
        this.postData(this.formData);
      }
      else {
        //update
        this.formData.modifiedBy = this.decodedtoken.UserId;
        this.formData.action = "Update";
        this.postData(this.formData);
      }
    }
  }
  public postData(data) {
    console.log(data);
    this.Loader.show();
    this.service.PostData(this.service.apiRoutes.UserManagement.AddAndUpdateCampaigns, data, true).then(res => {
      let response = res.json();
      if (response.status == httpStatus.Ok) {
        this.toastr.success(response.message, "Success");
        this._modalService.dismissAll();
        this.getCompaignsList();
        this.resetForm();
        this.Loader.hide;
      }
    })
  }
  update(data, content) {
    ;
    this.Loader.show();
    this.showModal(content);
    this.modelText = "Update Compaigns";
    data.startDate = this.service.formatDate(data.startDate, "YYYY-MM-DD");
    data.endDate = this.service.formatDate(data.endDate, "YYYY-MM-DD");
    this.appValForm.patchValue(data);
    this.Loader.hide();
  }
  delete(campaignId, isActive) {
    let obj = { campaignId, isActive, action: "Delete", modifiedBy: this.decodedtoken.UserId }
    this.postData(obj);
  }
  public getCompaignsList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.UserManagement.GetCompaignsList).subscribe(res => {
      this.compaignsList = res.json();
      this.Loader.hide();
    })
  }
  showModal(content) {
    this.modelText = "Add New Campaign";
    let modRef = this._modalService.open(content, { backdrop: 'static', keyboard: false });
    modRef.result.then(() => { this.resetForm() })
  }
  get f() {
    return this.appValForm.controls;
  }
  resetForm(): void {
    this.appValForm.reset();
    this.appValForm.controls['campaignId'].setValue(0);
  }

}
