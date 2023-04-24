import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { httpStatus } from '../../Shared/Enums/enums';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-linkedsalesman',
  templateUrl: './linkedsalesman.component.html',
  styleUrls: ['./linkedsalesman.component.css']
})
export class LinkedsalesmanComponent implements OnInit {
  public linkedSalesmanList = [];
  public salesmanFilter: FormGroup;
  pageSize = 50;
  pageNumber = 1;
  noOfPages;
  dataOrderBy = 'DESC';
  public totalPages: number;
  public status: boolean = true;
  public firstPageactive: boolean;
  public lastPageactive: boolean;
  public previousPageactive: boolean;
  public nextPageactive: boolean;
  totalRecoards;
  recoardNoFrom = 0;
  recoardNoTo = 50;
  pageing1 = [];
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  constructor(public fb: FormBuilder, public toaster: ToastrService, public router: Router, public service: CommonService, public Loader: NgxSpinnerService,) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Linked Salesman"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.salesmanFilterForm();
    this.getLinkedSalesManList();
  }
  public getLinkedSalesManList() {
    this.Loader.show();
    let formData = this.salesmanFilter.value
    formData.salesmanId == null ? formData.salesmanId = 0 : formData.salesmanId
    formData.pageNumber = this.pageNumber;
    formData.pageSize = this.pageSize;
    this.service.PostData(this.service.apiRoutes.UserManagement.LinkedSalesManList, formData, true).then(result => {
      this.linkedSalesmanList = result.json();
      if (!this.linkedSalesmanList) {
        this.toaster.error("No data found", "Error")
      }
      else {
        this.noOfPages = this.linkedSalesmanList[0].noOfRecoards / this.pageSize

        this.noOfPages = Math.floor(this.noOfPages);
        if (this.linkedSalesmanList[0].noOfRecoards > this.noOfPages) {
          this.noOfPages = this.noOfPages + 1;
        }
        this.totalRecoards = this.linkedSalesmanList[0].noOfRecoards;
        this.pageing1 = [];
        for (var x = 1; x <= this.noOfPages; x++) {
          this.pageing1.push(x);
        }
        this.recoardNoFrom = (this.pageSize * this.pageNumber) - this.pageSize + 1;
        this.recoardNoTo = (this.pageSize * this.pageNumber);
        if (this.recoardNoTo > this.linkedSalesmanList[0].noOfRecoards)
          this.recoardNoTo = this.linkedSalesmanList[0].noOfRecoards;
      }
      this.Loader.hide();
    });
  }
  //pagination start
  SelectedPageData(page) {
    this.pageNumber = page;
    this.status = true;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = false;
    this.getLinkedSalesManList();
  }
  NumberOfPages() {

    this.totalPages = Math.ceil(this.linkedSalesmanList[0].noOfRecoards / this.pageSize);
  }
  changePageSize() {
    this.pageNumber = 1;
    this.NumberOfPages();
    this.getLinkedSalesManList();
  }
  lastPage() {
    this.status = false;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = true;
    this.pageNumber = this.totalPages;
    this.getLinkedSalesManList();

  }
  FirstPage() {
    this.status = false;
    this.firstPageactive = true;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = false;
    this.pageNumber = 1;
    this.getLinkedSalesManList();
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
    this.getLinkedSalesManList();
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
    this.getLinkedSalesManList();
  }
  clickchange() {
    this.getLinkedSalesManList();
  }
  PageSizeChange() {

    this.pageNumber = 1;
    this.getLinkedSalesManList();
  }
  PriviousClick() {
    if (this.pageNumber > 1) {
      this.pageNumber = parseInt(this.pageNumber.toString()) - 1;
      this.getLinkedSalesManList();
    }

  }
  NextClick() {
    if (this.pageNumber < this.noOfPages) {
      this.pageNumber = parseInt(this.pageNumber.toString()) + 1;
      this.getLinkedSalesManList();
    }

  }
  LoadNewestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "DESC";
    this.getLinkedSalesManList();

  }
  LoadOldestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "ASC";
    this.getLinkedSalesManList();

  }
  //pagination end
  public unlinkSalesman(salesmanId, customerId) {
    let obj = { salesmanId, customerId}
    this.service.PostData(this.service.apiRoutes.UserManagement.UnlinkSalesman, obj, true).then(response => {
      let res = response.json();
      if (res.status == httpStatus.Ok) {
        this.toaster.success(res.message, "Success");
        this.getLinkedSalesManList();
      }
    })
  }
  public salesmanFilterForm() {
    this.salesmanFilter = this.fb.group({
      name: [''],
      salesmanId: [null], 
      customerId: [''],
      campaignName: [''],
      customerPhoneNumber: [''],
      mobileNumber: [''],
    });
  }
  public resetFrom() {
    this.pageSize = 50;
    this.pageNumber = 1
    this.salesmanFilter.reset();
    this.salesmanFilter.controls['salesmanId'].setValue(0);
    this.getLinkedSalesManList();
  }
}
