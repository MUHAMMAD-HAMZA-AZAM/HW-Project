import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { DynamicScriptLoaderService } from '../../Shared/CommonServices/dynamicScriptLoaderService';
import { ExcelFileService } from '../../Shared/CommonServices/excel-file.service';
import { httpStatus } from '../../Shared/Enums/enums';
import { ResponseVm } from '../../Shared/Models/HomeModel/HomeModel';
import { SortList } from '../../Shared/Sorting/sortList';
import { DatePipe } from '@angular/common';
import { CommonService } from '../../Shared/HttpClient/_http';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-site-pages',
  templateUrl: './site-pages.component.html',
  styleUrls: ['./site-pages.component.css']
})
export class SitePagesComponent implements OnInit {
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  constructor(public toastrService: ToastrService,
    public _httpService: CommonService,
    public Loader: NgxSpinnerService,
    private router: Router,
    private fb: FormBuilder,
    public _modalService: NgbModal,
) { }
  public response = new ResponseVm;
  public sitePagesList = [];
  public appValForm: FormGroup;
  public decodedtoken: any;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  public btnReset: boolean = false;
  public modelName: string;



  ngOnInit() {
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    this.userRole = JSON.parse(localStorage.getItem("Site Pages"));
    if (!this.userRole.allowView) {
      this.router.navigateByUrl('/login');
    }

    this.appValForm = this.fb.group({
      pageName: ['', [Validators.required]],
     projectId: ['', [Validators.required]],
      pageId: [0],
    });
    this.getSitePagesList();
  }
 
  /* ................ Add Update Site Pages...................*/
  public save() {
    this.appValForm.value.userId = this.decodedtoken.UserId;
    this.Loader.show();
    this._httpService.post(this._httpService.apiRoutes.CMS.AddUpdateSitePage, this.appValForm.value).subscribe(result => {
      this.response = result.json();
      if (this.response.status == httpStatus.Ok) {
        if (this.response.resultData?.length > 0) {
          this.toastrService.error("Site Page already exist", "opps", { timeOut: 5000 });
        }
        else {
          this.toastrService.success(this.response.message, "Success");
        }
      }
      else {
        this.toastrService.error(this.response.message, "Opps", { timeOut: 5000 });
      }
      this.Loader.hide();
      this.ResetForm();
      this.btnReset = false
      this._modalService.dismissAll();
      this.getSitePagesList();
    })
  }

  get f() { return this.appValForm.controls }
  /* ................ Reset Site Pages Form...................*/
  public ResetForm() {
    this.appValForm.reset();
    this.appValForm.controls['pageId'].setValue(0)
  }
  /* ..................Get Site Pages List......................... */
  public getSitePagesList() {
    this.Loader.show();
    this._httpService.get(this._httpService.apiRoutes.CMS.GetSitePagesList).subscribe(result => {
      this.response = result.json();
      if (this.response.status == httpStatus.Ok) {
        this.sitePagesList = this.response.resultData;
        this.Loader.hide();
      }

    })
  }
  /* .....................Show Modal For Update......................... */
  public update(data, content) {
    this.showModal(content);
    this.modelName = "Update Site Page";
    this.appValForm.patchValue(data);
    this.btnReset = true;
  }
  /*............ Show Modal For Add...............*/
  public showModal(content) {
    this.ResetForm();
    this.btnReset = false
    this.modelName = "Add Site Page";
    this._modalService.open(content, { backdrop: 'static', keyboard: false });
  }

}
