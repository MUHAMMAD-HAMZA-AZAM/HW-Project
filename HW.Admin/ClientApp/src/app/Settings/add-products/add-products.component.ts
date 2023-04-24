import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal/public_api';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CommonService } from '../../Shared/HttpClient/_http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { strict } from 'assert';
import { ResponseVm } from '../../Shared/Models/HomeModel/HomeModel';
import { NgxSpinnerService } from "ngx-spinner";
import { ImageCroppedEvent } from 'ngx-image-cropper';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-products',
  templateUrl: './add-products.component.html',
  styleUrls: ['./add-products.component.css']
})
export class AddProductsComponent implements OnInit {

  public stateList = [];
  public selectedStates = [];
  public selectedState = [];
  public selectedColumn = [];
  public dropdownListForState = {};
  public dropdownListForColumn = {};

  public priviousvalue = "";

  public productList = [];
  public statesList: [] = [];
  public productName: string = "";
 public seoTitle: string = "";
 public seoDescription: string = "";
 public ogTitle: string = "";
 public ogDescription: string = "";
  public canonical: string = "";
  public productNameExist: string = "";
  public Name: string = "";
  public productDate: any;
  public imagebase64: any;

  public confirmDelete: boolean = false;
  public productdeleteId: any;
  public orderByColumn: "";
  public responcelist = new ResponseVm;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    

  ////// Image //////
  croppedImage: any = null;
  imageChangedEvent: any = '';


  constructor(public router: Router,public toastrService: ToastrService, public _modalService: NgbModal, public service: CommonService, public Loader: NgxSpinnerService) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Products"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.getProductsList();
  }
  public populateProducts() {
    debugger;
    this.productList = [];
    this.service.get(this.service.apiRoutes.Supplier.GetActiveProducts + "?productCategoryId=" + 0).subscribe(result => {
      
      var resList = result.json();
      resList.forEach(x => {
        this.service.get(this.service.apiRoutes.Supplier.GetActiveProducts + "?productCategoryId=" + x.productCategoryId).subscribe(res => {
          
          var images = res.json();
          var img = this.service.transform(images[0].productImage);
          let obj = {
            productCategoryId: x.productCategoryId,
            name: x.name,
            isActive: x.isActive,
            orderByColumn: x.orderByColumn,
           seoTitle: x.SeoTitle,
           seoDescription: x.SeoDescription,
             ogTitle: x.OgTitle,
          ogDescription: x.OgDescription,
          canonical: x.Canonical,
            createdOn: x.createdOn,
            createdBy: x.createdBy,
            modifiedOn: x.modifiedOn,
            modifiedBy: x.modifiedBy,
            productImage: img
          }
          this.productList.push(obj);
          
        });
      });
      console.log(this.productList);
      
    });
  }

  ////// Image //////
  fileChangeEvent(event: any): void {
    var fReader = new FileReader();
    var isThumb = event.target.files[0];
    fReader.readAsDataURL(isThumb);
    fReader.onload = (event: any) => {
      this.croppedImage = event.target.result;
    };
  }

  save() {
    
    debugger;
    if (this.productDate == null || this.productDate == "" || this.productDate == undefined) {

      var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
      if (this.croppedImage != "") {
        let Obj = {
          Name: this.productName,
          CreatedBy: decodedtoken.UserId,
          OrderByColumn: this.orderByColumn,
          IsActive: true,
          imagebase64: this.croppedImage,
          SeoTitle: this.seoTitle,
          OgTitle: this.ogTitle,
          SeoDescription: this.seoDescription,
          OgDescription: this.ogDescription,
          Canonical: this.canonical

        }
        this.service.PostData(this.service.apiRoutes.Supplier.AddNewProduct, Obj).then(result => {
          this.responcelist = result.json();
          if (this.responcelist.message == 'AlreadyExists') {
            this.toastrService.error("Product Name Already Exist", "Error");
          }
          else {
            this.productName = "";
            this.toastrService.success("Data added Successfully", "Success");
            this._modalService.dismissAll();
            //this.populateProducts();
            this.getProductsList();
          }
        })
      }
      else {
        this.toastrService.error("Select Product Image", "Error");
      }
    }
    else {
      
      var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
      this.productDate.name = this.productName;
        this.productDate.seoTitle = this.seoTitle,
        this.productDate.ogTitle = this.ogTitle,
            this.productDate.seoDescription = this.seoDescription,
             this.productDate.ogDescription = this.ogDescription,
        this.productDate.canonical = this.canonical
      this.productDate.modifiedBy = decodedtoken.UserId;

      if (this.croppedImage != "" && this.croppedImage != null)
        this.productDate.imagebase64 = this.croppedImage;
      else
        this.productDate.imagebase64 = this.imagebase64;
      this.productDate.OrderByColumn = this.orderByColumn,
        this.productDate.productImage = null;
      console.log(this.productDate);
      this.service.post(this.service.apiRoutes.Supplier.AddNewProduct, this.productDate).subscribe(result => {
        this.responcelist = result.json();
        if (this.responcelist.message == 'AlreadyExists') {
          this.productDate.name = this.priviousvalue;
          this.toastrService.error("Product Name Already Exist", "Error");
        }
        else {
          this.productName = "";
          this.productDate = null;
          this.getProductsList();
          this.toastrService.success("Data updated Successfully", "Success");
          this._modalService.dismissAll();
        }
      })
    }
  }
  updateProducts(city, content) {
    debugger;
    this.productName = "";
    this.orderByColumn = "";
    if (city != null && city != "") {
      this.productName = city.name;
      this.priviousvalue = city.name;
      this.orderByColumn = city.orderByColumn;
       this.seoTitle = city.seoTitle;
      this.ogTitle = city.ogTitle;
     this.canonical = city.canonical;
     this.ogDescription = city.ogDescription;
    this.seoDescription = city.seoDescription;
      this.productDate = city;
      this.imagebase64 = city.productImage;
      this._modalService.open(content, { centered: true, size: 'lg', backdrop: 'static', keyboard: false });

    }
  }
  deleteProduct(city, deleteContent) {
    
    this.productName = city.name;
    this.productDate = city;
    this._modalService.open(deleteContent);

  }
  confirmDeleteCity() {
    
    if (this.productName != null && this.productName != "") {
      this.productDate.isActive = !this.productDate.isActive;
      let obj = {
        createdBy: this.productDate.createdBy,
        createdOn: this.productDate.createdOn,
        isActive: this.productDate.isActive,
        modifiedBy: this.productDate.modifiedBy,
        modifiedOn: this.productDate.modifiedOn,
        name: this.productDate.name,
        orderByColumn: this.productDate.orderByColumn,
          seoTitle: this.seoTitle,
          ogTitle: this.ogTitle,
          seoDescription: this.seoDescription,
          ogDescription: this.ogDescription,
          canonical: this.canonical,

        productCategoryId: this.productDate.productCategoryId,
        imagebase64: this.productDate.productImage
      }
      this.service.post(this.service.apiRoutes.Supplier.AddNewProduct, obj).subscribe(result => {
        if (result.status == 200) {
          this.toastrService.error("Product deleted successfully!", "Delete");
          this._modalService.dismissAll();
          this.getProductsList();
        }
        else {
          
          this.toastrService.warning("Something went wrong please try again", "Error");
          this._modalService.dismissAll();
        }
      })
    }
    this._modalService.dismissAll();
  }
  public getProductsList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Supplier.GetCategoriesForAdminListing).subscribe(result => {
      this.productList = result.json();
      this.Loader.hide();

    })
  }
  showModal(content) {
    this.productDate = [];
    this.croppedImage = null;
    this.productName = "";
   this.seoTitle = "";
  this.ogTitle = "";
  this.canonical = "";
  this.ogDescription = "";
  this.seoDescription = "";
    this.orderByColumn = "";
    this._modalService.open(content, { centered: true, size: 'lg', backdrop: 'static', keyboard: false })
  }

  // State Drop Setting

  onCitySelectAll(item: any) {
    this.selectedState = item;
  }
  onCityDeSelectALL(item: any) {
    this.selectedState = [];
  }
  onCitySelect(item: any) {
    this.selectedState = [];
    this.selectedState.push(item);
  }
  onCityDeSelect(item: any) {
    this.selectedState = this.selectedState.filter(
      function (value, index, arr) {
        return value.stateId != item.stateId;
      });
  }

  onColumnSelectAll(item: any) {
    this.selectedColumn = item;
  }
  onColumnDeSelectALL(item: any) {
    this.selectedColumn = [];
  }
  onColumnSelect(item: any) {
    this.selectedColumn.push(item);
  }
  onColumnDeSelect(item: any) {

    this.selectedColumn = this.selectedColumn.filter(
      function (value, index, arr) {
        return value.stateId != item.stateId;
      });
  }

  public getStatesList() {
    this.service.get(this.service.apiRoutes.Common.GetStatesList).subscribe(result => {
      this.stateList = result.json();
    });
  }


}
