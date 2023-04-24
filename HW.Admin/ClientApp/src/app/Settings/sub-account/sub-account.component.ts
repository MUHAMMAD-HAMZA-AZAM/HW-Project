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
import { SpSubAccountVM} from '../../Shared/Models/UserModel/SpSubAccountVM'

@Component({
  selector: 'app-sub-account',
  templateUrl: './sub-account.component.html',
  styleUrls: ['./sub-account.component.css']
})
export class SubAccountComponent implements OnInit {
  public subAcountStats: SpSubAccountVM = new SpSubAccountVM()
  public appValForm: FormGroup;
  public searchForm: FormGroup;
  public modelText: string = "Add New Sub Account";
  public decodedtoken: any;
  public formData: any;
  public searchformData: any;
  public submited: boolean = false;
  public isUpdate: boolean = false;
  public toggle: number;
  public id: number;
  public subAccountList: any = [];
  public accountList: any = [];
  public activeAccountList: any = [];
  public searchBy = 1;
  public startDate: Date;
  public endDate: Date;
  public startDate1: Date;
  public totalPages: number;
  public endDate1: Date;
  public location = "";
  public pageSize: number = 50;
  public noOfPages;
  public pageing1 = [];
  public pageNumber: number = 1;
  public status: boolean = true;
  public firstPageactive: boolean;
  public dataOrderBy = "DESC";
  public lastPageactive: boolean;
  public previousPageactive: boolean;
  public nextPageactive: boolean;
  public recoardNoFrom = 0;
  public recoardNoTo = 50;
  public totalRecoards = 101;
  public dataNotFound: boolean;

  jwtHelperService: JwtHelperService = new JwtHelperService();
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    

  constructor(public router: Router,public _modalService: NgbModal, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Sub Account"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.accountForm();
    this.getAccountList();
    this.searchForms();
    this.populateSubAccountList();
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
  }
  public searchForms() {
    this.searchForm = this.fb.group({
      subaccountname: [''],
      startdate: [],
      enddate: [],
      subaccountno: [],
      userid: [],
      username: [''],
      pagenumber: [],
      pagesize:[]
    })
  }
  public accountForm() {
    this.appValForm = this.fb.group({
      id: [0],
      subAccountName: ['', [Validators.required, Validators.maxLength(50)]],
      subAccountNo: [null, [Validators.required, Validators.maxLength(30)]],
      accountId: [null, [Validators.required, Validators.maxLength(30)]],
      customerId: ['', [Validators.required, Validators.maxLength(10)]],
      customerName: ['', [Validators.required, Validators.maxLength(50)]],
      tradesmanId: ['', [Validators.required, Validators.maxLength(10)]],
      tradesmanName: ['', [Validators.required, Validators.maxLength(50)]],
      supplierId: ['', [Validators.required, Validators.maxLength(10)]],
      supplierName: ['', [Validators.required, Validators.maxLength(50)]],
      isActive: [null, [Validators.required]],
      userType: [null, [Validators.required]],
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
      this.formData.active = this.formData.isActive == 1 ? true : false;
      ;
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

  populateSubAccountList() {

    this.searchForm.value.pagenumber = this.pageNumber;
    this.searchForm.value.pagesize = this.pageSize;
    this.Loader.show();
    this.service.post(this.service.apiRoutes.PackagesAndPayments.GetSubAccountList, this.searchForm.value).subscribe(result => {
      if (result.json() != null) {
        this.accountList = result.json();

        if (this.accountList.filter(x => x.active).length > 0) {
          this.accountList = this.accountList.filter(x => x.active);

          this.dataNotFound = true;
          this.subAccountList = this.accountList;
          this.noOfPages = this.accountList[0].noOfRecoards / this.pageSize
          this.noOfPages = Math.floor(this.noOfPages);
          if (this.accountList[0].noOfRecoards > this.noOfPages) {
            this.noOfPages = this.noOfPages + 1;
          }
          this.totalRecoards = this.accountList[0].noOfRecoards;
          this.pageing1 = [];
          for (var x = 1; x <= this.noOfPages; x++) {
            this.pageing1.push(x);
          }
          this.recoardNoFrom = (this.pageSize * this.pageNumber) - this.pageSize + 1;
          this.recoardNoTo = (this.pageSize * this.pageNumber);
          if (this.recoardNoTo > this.accountList[0].noOfRecoards)
            this.recoardNoTo = this.accountList[0].noOfRecoards;
        }
        else {
          this.searchForm.reset();
          this.recoardNoFrom = 0;
          this.recoardNoTo = 0;
          this.recoardNoTo = 0;
          this.totalRecoards = 0;
          this.subAccountList = [];
          this.toastr.error("No record found !", "Search")
          this.dataNotFound = false;
          this.Loader.hide();
        }
      }
      else {
        this.searchForm.reset();
        this.recoardNoFrom = 0;
        this.recoardNoTo = 0;
        this.noOfPages = 0;
        this.totalRecoards = 0;
        this.subAccountList = [];
        this.toastr.error("No record found !", "Search")
        this.dataNotFound = false;
        this.Loader.hide();
      }
    },
      error => {
        console.log(error);
        this.Loader.hide();
      });
    this.Loader.hide();
  }

  GetTotalPageList() {
    var array = new Array();
    for (var i = 1; i <= this.totalPages; i++) {
      array.push(i);
    }
    return array;
  }
  SelectedPageData(page) {
    this.pageNumber = page;
    this.status = true;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = false;
    this.populateSubAccountList();
  }
  NumberOfPages() {
    this.totalPages = Math.ceil(this.totalPages / this.pageSize);
  }
  changePageSize() {
    this.pageNumber = 1;
    localStorage.setItem("PageSize", this.pageSize.toString());
    this.NumberOfPages();
    this.populateSubAccountList();
  }
  lastPage() {
    this.status = false;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = true;
    this.pageNumber = this.totalPages;
    this.populateSubAccountList();

  }
  FirstPage() {
    this.status = false;
    this.firstPageactive = true;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = false;
    this.pageNumber = 1;
    this.populateSubAccountList();
  }
  nextPage() {
    this.status = false;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = true;
    this.lastPageactive = false;
    if (this.totalPages > this.pageNumber) {
      this.pageNumber++;
    }
    this.populateSubAccountList();
  }
  previousPage() {
    this.status = false;
    this.firstPageactive = false;
    this.previousPageactive = true;
    this.nextPageactive = false;
    this.lastPageactive = false;
    if (this.pageNumber > 1) {
      this.pageNumber--;
    }
    this.populateSubAccountList();
  }

  clickchange() {
    this.populateSubAccountList();
  }
  PageSizeChange() {

    this.pageNumber = 1;
    this.populateSubAccountList();
  }
  PriviousClick() {
    ;
    if (this.pageNumber > 1) {
      this.pageNumber = this.pageNumber - 1;
      this.populateSubAccountList();
    }

  }
  NextClick() {
    ;
    if (this.pageNumber < this.noOfPages) {
      this.pageNumber = parseInt(this.pageNumber.toString()) + 1;
      this.populateSubAccountList();
    }

  }

  LoadNewestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "DESC";
    this.populateSubAccountList();
  }
  LoadOldestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "ASC";
    this.populateSubAccountList();
  }
  public postData(data) {
    this.Loader.show();
    ;
    this.id = data.subAccountNo;
    this.service.get(this.service.apiRoutes.PackagesAndPayments.getSubAccountRecord + "?id=" + this.id).subscribe(result => {
      let response = result.json();
      ;
      if (response.status == httpStatus.Ok) {

        this.service.PostData(this.service.apiRoutes.PackagesAndPayments.AddAndUpdateSubAccount, data, true).then(res => {
          let response = res.json();
          if (response.status == httpStatus.Ok) {
            this.toastr.success(response.message, "Success");
            this._modalService.dismissAll();
            this.resetForm();
            this.populateSubAccountList();
            this.Loader.hide;
          }
        })
      }
      else {
        this.toastr.error(response.message, "Duplicate");
        this._modalService.dismissAll();
        this.resetForm();
        this.populateSubAccountList();
        this.Loader.hide;
      }
    });
  }


  update(data, content) {
    this.Loader.show();
    this.appValForm.controls['userType'].clearValidators();
    this.appValForm.controls['userType'].updateValueAndValidity();
    this.showModal(content);
    this.isUpdate = true;
    this.modelText = "Update Sub Account";
    data.isActive = data.active ? 1 : 2;
    data.customerId != null ? this.toggle = 1 : data.tradesmanId != null ? this.toggle = 2 : this.toggle = 3
    this.userType(this.toggle);
    this.appValForm.patchValue(data);
    this.Loader.hide();
  }
  delete(id) {
    let obj = { id, action: "delete", modifiedBy: this.decodedtoken.UserId }
    this.postData(obj);
  }
  public getAccountList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.PackagesAndPayments.GetAccountList).subscribe(res => {
      this.accountList = res.json();
      if (this.accountList != null) {
        this.activeAccountList = this.accountList.filter(x => x.active);
      }
      this.Loader.hide();
    })
  }
  showModal(content) {
    let modRef = this._modalService.open(content, { backdrop: 'static', keyboard: false });
    modRef.result.then(() => { this.resetForm() })
    this.isUpdate = false;
    this.modelText = "Add New Sub Account";
  }
  get f() {
    return this.appValForm.controls;
  }

  resetSearchForm() {

    this.searchForm.reset();
    this.populateSubAccountList();
  }

  resetForm(): void {
    this.searchForm.reset();
    this.appValForm.reset();
    this.appValForm.controls['id'].setValue(0);
  }
  userType(value) {
    this.toggle = value;
    if (this.toggle == 1) {
      this.appValForm.controls['tradesmanId'].clearValidators();
      this.appValForm.controls['tradesmanId'].updateValueAndValidity();
      this.appValForm.controls['tradesmanName'].clearValidators();
      this.appValForm.controls['tradesmanName'].updateValueAndValidity();
      this.appValForm.controls['supplierId'].clearValidators();
      this.appValForm.controls['supplierId'].updateValueAndValidity();
      this.appValForm.controls['supplierName'].clearValidators();
      this.appValForm.controls['supplierName'].updateValueAndValidity();
    }
    else if (this.toggle == 2) {
      this.appValForm.controls['customerId'].clearValidators();
      this.appValForm.controls['customerId'].updateValueAndValidity();
      this.appValForm.controls['customerName'].clearValidators();
      this.appValForm.controls['customerName'].updateValueAndValidity();
      this.appValForm.controls['supplierId'].clearValidators();
      this.appValForm.controls['supplierId'].updateValueAndValidity();
      this.appValForm.controls['supplierName'].clearValidators();
      this.appValForm.controls['supplierName'].updateValueAndValidity();
    }
    else {
      this.appValForm.controls['customerId'].clearValidators();
      this.appValForm.controls['customerId'].updateValueAndValidity();
      this.appValForm.controls['customerName'].clearValidators();
      this.appValForm.controls['customerName'].updateValueAndValidity();
      this.appValForm.controls['tradesmanId'].clearValidators();
      this.appValForm.controls['tradesmanId'].updateValueAndValidity();
      this.appValForm.controls['tradesmanName'].clearValidators();
      this.appValForm.controls['tradesmanName'].updateValueAndValidity();
    }
  }

}
