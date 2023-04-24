import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { DynamicScriptLoaderService } from '../../Shared/CommonServices/dynamicScriptLoaderService';
import { ExcelFileService } from '../../Shared/CommonServices/excel-file.service';
import {httpStatus } from '../../Shared/Enums/enums';
import { ResponseVm } from '../../Shared/Models/HomeModel/HomeModel';
import { SortList } from '../../Shared/Sorting/sortList';
import { DatePipe } from '@angular/common';
import { CommonService } from '../../Shared/HttpClient/_http';
import { JwtHelperService } from '@auth0/angular-jwt';


@Component({
  selector: 'app-shipping-charges',
  templateUrl: './shipping-charges.component.html',
  styleUrls: ['./shipping-charges.component.css']
})
export class ShippingChargesComponent implements OnInit {
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  constructor(public toastrService: ToastrService,
    public _httpService: CommonService,
    public Loader: NgxSpinnerService,
    private router: Router,
    private fb: FormBuilder,
    public dynamicScripts: DynamicScriptLoaderService,
    public excelService: ExcelFileService,
    public _modalService: NgbModal,
    public sortBy: SortList) { }
  public responce = new ResponseVm;
  public shippingChargesList = [];
  public appValForm: FormGroup;
  public cityList = [];
  public decodedtoken: any;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  public btnReset: boolean = false;
  public modelName: string;



  ngOnInit() {
 this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    this.userRole = JSON.parse(localStorage.getItem("Shipping Charges"));
    if (!this.userRole.allowView) {
      this.router.navigateByUrl('/login');
    }

    this.appValForm = this.fb.group({
    cost: ['', [Validators.required]],
      id: [0],
      cityId: [null, [Validators.required]],
    
    });
    this.getShippingChargesList();
    this.getAllCities();

  }
  /* ................ show City list...................*/
  public getAllCities() {
    this._httpService.get(this._httpService.apiRoutes.Common.GetCityList).subscribe(result => {
      this.cityList = result.json();

    })
  }
/* ................ Add Update Shipping Cost...................*/
  public save() {
    this.appValForm.value.userId = this.decodedtoken.UserId;
    this.Loader.show();
    this._httpService.post(this._httpService.apiRoutes.Supplier.AddUpdateShippingCost, JSON.stringify(this.appValForm.value)).subscribe(result => {
      this.responce = result.json();
      if (this.responce.status == httpStatus.Ok) {
        if (this.responce.resultData[0].cityId > 0) {
          this.toastrService.error("Shipping Cost already exist", "opps", { timeOut: 5000 });
       }
        else {
          this.toastrService.success(this.responce.message, "Success");
        } 
      }
      else {
        this.toastrService.error(this.responce.message,  "Opps", { timeOut: 5000 });
      }
      this.Loader.hide();
      this.ResetForm();
      this.btnReset = false
      this._modalService.dismissAll();
      this.getShippingChargesList();
      }) 
  }

  get f() { return this.appValForm.controls }
  /* ................ Reset Shipping Cost Form...................*/
 public ResetForm() {
   this.appValForm.reset();
   this.appValForm.controls['id'].setValue(0) 
 }
               /* ..................Get Shipping Cost List......................... */
  public getShippingChargesList() {
    this.Loader.show();
    this._httpService.get(this._httpService.apiRoutes.Supplier.GetShippingChargesList).subscribe(result => {
      this.responce =JSON.parse(result.json());
      if (this.responce.status == httpStatus.Ok) {
        this.shippingChargesList = this.responce.resultData;
        this.Loader.hide();
      }

    })
  }
            /* .....................Show Modal For Update......................... */
  public update(data, content) {
    this.showModal(content);
    this.modelName = "Update Shipping Cost";
    this.appValForm.patchValue(data);
    this.btnReset = true;
  }
          /*............ Show Modal For Add...............*/
  public showModal(content) {
    this.ResetForm();
    this.btnReset = false
    this.modelName = "Add Shipping Cost";
    this._modalService.open(content, { backdrop: 'static', keyboard: false });
  }

}
