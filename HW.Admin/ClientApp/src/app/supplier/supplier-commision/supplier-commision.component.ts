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
  selector: 'app-supplier-commision',
  templateUrl: './supplier-commision.component.html',
  styleUrls: ['./supplier-commision.component.css']
})
export class SupplierCommisionComponent implements OnInit {

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
  public supplierCommissionList = [];
  public appValForm: FormGroup;
  public supplierList = [];
  public decodedtoken: any;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  public btnReset: boolean = false;
  public modelName: string;
  pageSize = 50;
  pageNumber = 1;
  public totalPages: number;
  noOfPages;
  dataOrderBy = 'DESC';
  totalRecords;
  recoardNoFrom = 0;
  recoardNoTo = 50;
  pageing1 = [];
  keyword = 'supplierName';




  ngOnInit() {
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    this.userRole = JSON.parse(localStorage.getItem("Suppliers Commission"));
    if (!this.userRole.allowView) {
      this.router.navigateByUrl('/login');
    }

    this.appValForm = this.fb.group({
      supplierCommision: ['', [Validators.required]],
      id: [0],
      supplierId: [null, [Validators.required]],
      isActive: [null, [Validators.required]],
    

    });
    this.CommissionList();
    this.getAllSupplierList();

  }
  /* ................ show Supplier list...................*/
  public getAllSupplierList() {
    this._httpService.get(this._httpService.apiRoutes.Supplier.SupplierList).subscribe(result => {
      this.responce = result.json();
      this.supplierList = this.responce.resultData;

    })
  }
  /* ................ Add Update Supplier Commission...................*/
  public save() {
    debugger;
    this.appValForm.value.userId = this.decodedtoken.UserId;
    if (typeof this.appValForm.value.supplierId === 'object') {
      this.appValForm.value.supplierId = this.appValForm.value['supplierId'].supplierId;
    }
    else {
      this.appValForm.controls['supplierId'].setErrors({ supplierInvalidInput: true });
      return;


    }

    this.Loader.show();
    this._httpService.post(this._httpService.apiRoutes.Supplier.AddUpdateSupplierCommission, JSON.stringify(this.appValForm.value)).subscribe(result => {
      this.responce = result.json();
      if (this.responce.status == httpStatus.Ok) {
        if (this.responce.resultData) {
          this.toastrService.error("Suppliers Commission already exist", "opps", { timeOut: 5000 });
        }
        else {
          this.toastrService.success(this.responce.message, "Success");
        }
      }
      else {
        this.toastrService.error(this.responce.message, "Opps", { timeOut: 5000 });
      }
      this.Loader.hide();
      this.ResetForm();
      this.btnReset = false
      this._modalService.dismissAll();
      this.CommissionList();
    })
  }

  get f() { return this.appValForm.controls }
  /* ................ Reset Supplier Commission Cost Form...................*/
  public ResetForm() {
    this.appValForm.reset();
    this.appValForm.controls['id'].setValue(0)
  }
  /* ..................Get Supplier Commission List......................... */
  public CommissionList() {
    let obj = {
      pageSize: this.pageSize,
        pageNumber: this.pageNumber
    }
    this.Loader.show();
    this._httpService.post(this._httpService.apiRoutes.Supplier.GetSupplierCommissionList, JSON.stringify(obj)).subscribe(result => {
      this.responce = result.json();
      
      if (this.responce.status == httpStatus.Ok) {
        this.supplierCommissionList = this.responce.resultData;
        this.Loader.hide();
        this.noOfPages = this.supplierCommissionList[0].noOfRecords / this.pageSize

        this.noOfPages = Math.floor(this.noOfPages);
        if (this.supplierCommissionList[0].noOfRecords > this.noOfPages) {
          this.noOfPages = this.noOfPages + 1;
        }
        this.totalRecords = this.supplierCommissionList[0].noOfRecords;
        console.log(this.totalRecords);
        this.pageing1 = [];
        for (var x = 1; x <= this.noOfPages; x++) {
          this.pageing1.push(x);
        }
        this.recoardNoFrom = (this.pageSize * this.pageNumber) - this.pageSize + 1;
        this.recoardNoTo = (this.pageSize * this.pageNumber);
        if (this.recoardNoTo > this.supplierCommissionList[0].noOfRecords)
          this.recoardNoTo = this.supplierCommissionList[0].noOfRecords;
      }
      

    })
  }
  /* .....................Show Modal For Update......................... */
  public update(data, content) {
    this.showModal(content);
    this.modelName = "Update Supplier Commission Amount";
    this.appValForm.patchValue(data);
    let filterSupplier = this.supplierList.find(x => x.supplierId == data.supplierId)
    if (filterSupplier) {
      this.appValForm.controls['supplierId'].setValue(filterSupplier)

    }
    this.btnReset = true;
  }
  /*............ Show Modal For Add...............*/
  public showModal(content) {
    this.ResetForm();
    this.btnReset = false
    this.modelName = "Add Supplier Commission";
    this._modalService.open(content, { backdrop: 'static', keyboard: false });
  }
  /*............ Show Modal For Add...............*/
  public delete(content) {
    
    content.isActive = !content.isActive;
    content.userId = this.decodedtoken.UserId;
    this.Loader.show();
    this._httpService.post(this._httpService.apiRoutes.Supplier.AddUpdateSupplierCommission, JSON.stringify(content)).subscribe(result => {
      this.responce = result.json();
      if (this.responce.status == httpStatus.Ok) {

        this.toastrService.success("Suppliers Commission Status Changed", "Success", { timeOut: 5000 });
    
      }
      else {
        this.toastrService.error(this.responce.message, "Opps", { timeOut: 5000 });
      }
      this.Loader.hide();
      this.CommissionList();
    })
  }
  changePageSize() {
    this.pageNumber = 1;
    this.NumberOfPages();
    this.CommissionList();
  }
  NumberOfPages() {

    this.totalPages = Math.ceil(this.supplierCommissionList[0].noOfRecords / this.pageSize);
  }
  clickchange() {
    this.CommissionList();
  }
  PriviousClick() {
    
    if (this.pageNumber > 1) {
      this.pageNumber = parseInt(this.pageNumber.toString()) - 1;
      this.CommissionList();
    }

  }
  NextClick() {
    if (this.pageNumber < this.noOfPages) {
      this.pageNumber = parseInt(this.pageNumber.toString()) + 1;
      this.CommissionList();
    }

  }
  selectEvent(item) {
    // do something with selected item
  }

  unselectEvent(item) {

  }
}

