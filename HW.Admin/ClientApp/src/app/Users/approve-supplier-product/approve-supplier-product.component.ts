import { formatDate } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { httpStatus } from '../../Shared/Enums/enums';
import { UploadFilesServiceService } from '../../Shared/HttpClient/upload-files-service.service';
import { CommonService } from '../../Shared/HttpClient/_http';
import { ResponseVm } from '../../Shared/Models/HomeModel/HomeModel';

@Component({
  selector: 'app-approve-supplier-product',
  templateUrl: './approve-supplier-product.component.html',
  styleUrls: ['./approve-supplier-product.component.css']
})
export class ApproveSupplierProductComponent implements OnInit {
  public userId: any;
  public supplierId: any;
  public productId: any;
  public formData: any;
  public modelName: string;
  public appValForm: FormGroup;
  public ordersByImagesFileId: any = [];
  public supplierProductImagesList = [];
  public supplierProductDetailByProductId :any;
  public suppliersProductsList: any = [];
  public productImages: any = [];
  public noDataFound: boolean = false;
  public productImagesFound: boolean = true;
  public response = new ResponseVm();
  pageSize = 50;
  pageNumber = 1;
  noOfPages;
  dataOrderBy = 'DESC';
  public totalPages: number;
  public status: boolean = true;
  public firstPageactive: boolean = false;
  public lastPageactive: boolean = false;
  public previousPageactive: boolean = false;
  public nextPageactive: boolean = false;
  totalRecords;
  recoardNoFrom = 0;
  recoardNoTo = 50;
  pageing1 = [];
  public decodedtoken: any;
  public allowview;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(
    public _modalService: NgbModal,
    public toaster: ToastrService,
    public common: CommonService,
    public Loader: NgxSpinnerService,
    public fb: FormBuilder,
    private router: Router,
    public _fileService: UploadFilesServiceService) { }


  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Authorize SupplierProduct"));
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    this.userId = this.decodedtoken.Id;
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.appValForm = this.fb.group({
      supplierId: [''],
      productId: [''],
      mobileNumber: [''],
      startDate: [''],
      endDate: ['']

    });
    this.getSupplierProducsListForApprovel();
  }

  //--------------- Show SupplierProducts List For Approval 
  
  public getSupplierProducsListForApprovel() {
    this.Loader.show();
    let formData = this.appValForm.value;
    formData.supplierId = formData.supplierId == '' ? formData.supplierId = 0 : formData.supplierId;
    formData.productId = formData.productId == '' ? formData.productId = 0 : formData.productId;
    formData.pageNumber = this.pageNumber;
    formData.pageSize = this.pageSize;
    formData.isBlock = false;
    console.log(formData);
    this.common.PostData(this.common.apiRoutes.Supplier.GetSuppliersProductsListForApproval, JSON.stringify(formData), true).then(result => {
      debugger;
      this.response = JSON.parse(result.json());
      if (this.response.status == httpStatus.Ok) {
        this.suppliersProductsList = this.response.resultData;
        console.log(this.suppliersProductsList);
        if (!this.suppliersProductsList) {
          this.toaster.error("No Data Found !!");
          this.Loader.hide();
          this.noDataFound = true;
        }
        else {
          this.noOfPages = this.suppliersProductsList[0].noOfRecords / this.pageSize

          this.noOfPages = Math.floor(this.noOfPages);
          if (this.suppliersProductsList[0].noOfRecords > this.noOfPages) {
            this.noOfPages = this.noOfPages + 1;
          }
          this.totalRecords = this.suppliersProductsList[0].noOfRecords;
          console.log(this.totalRecords);
          this.pageing1 = [];
          for (var x = 1; x <= this.noOfPages; x++) {
            this.pageing1.push(x);
          }
          this.recoardNoFrom = (this.pageSize * this.pageNumber) - this.pageSize + 1;
          this.recoardNoTo = (this.pageSize * this.pageNumber);
          if (this.recoardNoTo > this.suppliersProductsList[0].noOfRecords)
            this.recoardNoTo = this.suppliersProductsList[0].noOfRecords;
        }
        this.Loader.hide();
      }

    }, error => {
      console.log(error);
      this.Loader.show();
    });
  }

  //---------------- Show Modal for Product Authorization

  public showAuthorizeModal(prodId, suppId, authorizeProductModel) {
    this._modalService.open(authorizeProductModel);
    this.productId = prodId;
    this.supplierId = suppId;
  }

  //----------------- Authorize Supplier Product

  public authorizeSupplierProduct() {
    this.Loader.show();
    let obj = {
      supplierId: this.supplierId,
      productId: this.productId,
      modifiedBy: this.userId
    };
    console.log(obj);
    this.common.PostData(this.common.apiRoutes.Supplier.ApproveSupplierProduct, JSON.stringify(obj), true).then(res => {
      this.response = res.json();
      if (this.response.status == httpStatus.Ok) {
        this.toaster.success(this.response.message);
        this._modalService.dismissAll();
        this.Loader.hide();
        this.getSupplierProducsListForApprovel();
      }
    }, error => {
      console.log(error);
    });
  }

  //----------------- Reset Filter Form 

  public resetFrom() {
    this.pageSize = 50;
    this.pageNumber = 1
    this.appValForm.reset();
    this.getSupplierProducsListForApprovel();
  }


  //----------------- Show Modal for Product Details

  public showDetailsModal(prodId, productDetailsModel) {
    this.productId = prodId;
    this.getSupplierProductImagesByProductId(this.productId, productDetailsModel);
  
  }

  //----------------- Show Product Images by Product Id

  getSupplierProductImagesByProductId(productId, modal: TemplateRef<any>) {
    debugger;
    this.Loader.show();
    this.productId = productId;
    this.common.get(this.common.apiRoutes.Supplier.GetSupplierProductImagesbyProductId + "?productId=" + this.productId).subscribe(res => {
      this.productImages = [];
      this.response = JSON.parse(res.json());
      this.supplierProductImagesList = this.response.resultData;
      if (this.response.status == httpStatus.Ok) {
        if (this.supplierProductImagesList.length > 0) {
          this.supplierProductDetailByProductId = this.supplierProductImagesList[0];
        this._modalService.open(modal, { size: 'xl', centered: true });
          this.supplierProductImagesList.forEach((x, i) => {
            if (x.fileName) {
              this._fileService.getFileByName(x.filePath + x.fileName).then(file => {
                this.supplierProductImagesList[i].fileName = file;
                this.productImages.push(file);
              });
            }
            else {
              this.supplierProductImagesList[i].fileName = '';
            }
          });
          console.log(this.supplierProductImagesList);
          this.Loader.hide();
        }
        else {
          this.productImagesFound = false;
        }
      }

    }, error => {
      console.log(error);
    });
  }

  //----------------- Close Any Modal
  public closeModal() {
    this._modalService.dismissAll();
  }

  //----------------- pagination start
  SelectedPageData(page) {
    this.pageNumber = page;
    this.status = true;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = false;
    this.getSupplierProducsListForApprovel();
  }
  NumberOfPages() {

    this.totalPages = Math.ceil(this.suppliersProductsList[0].noOfRecords / this.pageSize);
  }
  changePageSize() {
    this.pageNumber = 1;
    this.NumberOfPages();
    this.getSupplierProducsListForApprovel();
  }
  lastPage() {
    this.status = false;
    this.firstPageactive = false;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = true;
    this.pageNumber = this.totalPages;
    this.getSupplierProducsListForApprovel();

  }
  FirstPage() {
    this.status = false;
    this.firstPageactive = true;
    this.previousPageactive = false;
    this.nextPageactive = false;
    this.lastPageactive = false;
    this.pageNumber = 1;
    this.getSupplierProducsListForApprovel();
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
    this.getSupplierProducsListForApprovel();
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
    this.getSupplierProducsListForApprovel();
  }
  clickchange() {
    this.getSupplierProducsListForApprovel();
  }
  PageSizeChange() {

    this.pageNumber = 1;
    this.getSupplierProducsListForApprovel();
  }
  PriviousClick() {
    if (this.pageNumber > 1) {
      this.pageNumber = parseInt(this.pageNumber.toString()) - 1;
      this.getSupplierProducsListForApprovel();
    }

  }
  NextClick() {
    if (this.pageNumber < this.noOfPages) {
      this.pageNumber = parseInt(this.pageNumber.toString()) + 1;
      this.getSupplierProducsListForApprovel();
    }

  }
  LoadNewestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "DESC";
    this.getSupplierProducsListForApprovel();

  }
  LoadOldestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "ASC";
    this.getSupplierProducsListForApprovel();

  }
  //---------------------- pagination end

  public numberOnly(event): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }
}
