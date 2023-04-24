import { Component, OnInit } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal/public_api';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CommonService } from '../../Shared/HttpClient/_http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { strict } from 'assert';
import { ResponseVm } from '../../Shared/Models/HomeModel/HomeModel';
import { HomemoduleModule } from '../../Home/homemodule.module';
import { NgxSpinnerService } from "ngx-spinner";
import { ImageCroppedEvent } from 'ngx-image-cropper';
import { Router } from '@angular/router';
@Component({
  selector: 'app-add-sub-products',
  templateUrl: './add-sub-products.component.html',
  styleUrls: ['./add-sub-products.component.css']
})
export class AddSubProductsComponent implements OnInit {

  public stateList = [];
  public selectedState = [];
  public selectedColumn = [];

  public selectedSupplierType = [];
  public categoryList = [];
  public productList: [] = [];
  public statesList: [] = [];
  public productName: string = "";
  public seoTitle: string = "";
  public seoDescription: string = "";
  public ogTitle: string = "";
  public ogDescription: string = "";
  public canonical: string = "";
  public productNameExist: string = "";
  public Name: string = "";
  public commission: number;
  public productDate: any;
  public productdeleteId: any;
  public selectedCategories = [];
  public responcelist = new ResponseVm;
  public supplierdropdownSettings;
  public orderByColumn: number;
  public priviousvalue = "";
  jwtHelperService: JwtHelperService = new JwtHelperService();
  ////// Image //////
  imageChangedEvent: any = '';
  croppedImage: any = '';
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  constructor(public router: Router,public toastrService: ToastrService, public _modalService: NgbModal, public service: CommonService, public Loader: NgxSpinnerService, ) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Sub Products"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.supplierdropdownSettings = {
      singleSelection: true,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };
    this.populateCategories();
    this.getProductsList();
  }

  ////// Image //////
  fileChangeEvent(event: any): void {
    this.imageChangedEvent = event;
  }
  imageCropped(event: ImageCroppedEvent) {
    this.croppedImage = event.base64;
  }
  imageLoaded() {
  }
  cropperReady() {
  }
  loadImageFailed() {
  }


  populateCategories() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Supplier.GetCategories).subscribe(result => {
      this.categoryList = result.json();
      console.log(result.json());
      this.Loader.hide();
    },
    error => {
      console.log(error);      
    });
  }

  save() {
    
     let productId;
    
       productId = Number(this.selectedCategories[0].id);
        if (this.productDate == null || this.productDate == "" || this.productDate == undefined) {
          var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
          if (this.croppedImage != "") {
            let Obj = {
              subCategoryName: this.productName,
              CreatedBy: decodedtoken.UserId,
              ProductCategoryId: productId,
              IsActive: true,
              OrderByColumn: this.orderByColumn,
              imagebase64: this.croppedImage,
              commission: this.commission,
              SeoTitle: this.seoTitle,
              OgTitle: this.ogTitle,
              SeoDescription: this.seoDescription,
              OgDescription: this.ogDescription,
              Canonical: this.canonical
            }
            debugger;
            this.service.post(this.service.apiRoutes.Supplier.AddNewSubProduct, Obj).subscribe(result => {
              
              this.responcelist = result.json();
              if (this.responcelist.message == 'AlreadyExists') {
                this.productDate.name = this.priviousvalue;
                this.toastrService.error("Product Name Already Exist", "Error");
              }
              else {
                this.productName = "";
                this.toastrService.success("Data added Successfully", "Success");
                this._modalService.dismissAll();
                this.getProductsList();
              }
            })
          }
          else {
            this.toastrService.error("Select Product Image", "Error");
          }
        }
        else {
          
          this.productDate.subCategoryName = this.productName;
           this.productDate.seoTitle = this.seoTitle,
            this.productDate.ogTitle = this.ogTitle,
            this.productDate.seoDescription = this.seoDescription,
            this.productDate.ogDescription = this.ogDescription,
            this.productDate.canonical = this.canonical
          var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
          this.productDate.CreatedBy = decodedtoken.UserId;
          this.productDate.ProductCategoryId = productId;
          this.productDate.OrderByColumn = this.orderByColumn;
          this.productDate.commission = this.commission;
          if (this.croppedImage != "")
            this.productDate.imagebase64 = this.croppedImage;
          else
            this.productDate.imagebase64 = this.productDate.subProductImage;
          console.log((this.productDate) + "Updates" )
          this.service.post(this.service.apiRoutes.Supplier.AddNewSubProduct, this.productDate).subscribe(result => {
            
            this.responcelist = result.json();
            if (this.responcelist.message == 'AlreadyExists') 
            {
            
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
    if (city != null && city != "") {
      this.productName = city.subCategoryName;
      this.selectedSupplierType = [{ id: city.productCategoryId, value: city.productName }];
      this.selectedCategories = [];
      this.selectedCategories = this.selectedSupplierType;
      this.priviousvalue = city.subCategoryName;
      this.seoTitle = city.seoTitle;
      this.ogTitle = city.ogTitle;
      this.canonical = city.canonical;
      this.ogDescription = city.ogDescription;
      this.seoDescription = city.seoDescription;
      this.productDate = city;
      this.orderByColumn = city.orderByColumn;
      console.log(this.selectedSupplierType);
      this._modalService.open(content, { centered: true, size: 'lg', backdrop: 'static', keyboard: false });

    }
  }
  deleteProduct(city, deleteContent) {
    
    this.productName = city.subCategoryName;
    this.seoTitle = city.seoTitle;
    this.ogTitle = city.ogTitle;
    this.canonical = city.canonical;
    this.ogDescription = city.ogDescription;
    this.seoDescription = city.seoDescription;
    this.productDate = city;
    this._modalService.open(deleteContent);
  }
  confirmDeleteCity() {
    
    if (this.productName != null && this.productName != "") {
      this.productDate.isActive = !this.productDate.isActive;
      this.service.post(this.service.apiRoutes.Supplier.AddNewSubProduct, this.productDate).subscribe(result => {
        
        if (result.status == 200) {
          this.toastrService.warning("Status change successfully!", "Information");
          this._modalService.dismissAll();
          this.getProductsList();
        }
        else {
          this.toastrService.warning("Something went wrong please try again", "Error");
          this._modalService.dismissAll();
        }
      })
    }
  }
  public getProductsList() {
    this.service.get(this.service.apiRoutes.Supplier.GetSubCategoriesForListing).subscribe(result => {
      this.productList = result.json();
      console.log(this.productList);
    })
  }
  showModal(content) {
    this.productDate = [];
    this.selectedSupplierType = [];
    this.productName = "";
this.seoTitle = "";
this.ogTitle = "";
this.canonical = "";
this.ogDescription = "";
this.seoDescription = "";
    this.orderByColumn = 0;
    this._modalService.open(content, { centered: true, size: 'lg', backdrop: 'static', keyboard: false })
  }

  //  State Drop Setting

  onCitySelectAll(item: any) {
    console.log(item);
    this.selectedState = item;
  }
  onCityDeSelectALL(item: any) {
    this.selectedState = [];
    console.log(item);
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
    console.log(item);
    this.selectedColumn = item;
  }
  onColumnDeSelectALL(item: any) {
    this.selectedColumn = [];
    console.log(item);
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
  onItemSelectAll(items: any) {
    console.log(items);
    this.selectedCategories = items;


  }
  OnItemDeSelectALL(items: any) {
    this.selectedCategories = [];
    console.log(items);
  }
  onItemSelect(item: any) {
    this.selectedCategories = [];
    this.selectedCategories.push(item);
    console.log(this.selectedCategories);
  }
  OnItemDeSelect(item: any) {

    this.selectedCategories = this.selectedCategories.filter(
      function (value, index, arr) {
        return value.id != item.id;
      }

    );
    console.log(this.selectedCategories);
  }
  public getStatesList() {
    this.service.get(this.service.apiRoutes.Common.GetStatesList).subscribe(result => {
      this.stateList = result.json();
      console.log(this.statesList);
    })
  }

}
