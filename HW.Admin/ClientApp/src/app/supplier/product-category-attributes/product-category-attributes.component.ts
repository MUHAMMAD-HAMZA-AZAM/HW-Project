import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { httpStatus } from '../../Shared/Enums/enums';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-product-category-attributes',
  templateUrl: './product-category-attributes.component.html',
  styleUrls: ['./product-category-attributes.component.css']
})
export class ProductCategoryAttributesComponent implements OnInit {
  public productAttributeForm: FormGroup;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  public updateData:any;
  public productAttributeList: any = [];
  public productCategoryAttributeList: any = [];
  public productCategoryGroupList: any = [];
  public productsList: any = [];
  public subProductsList: any = [];
  public formData: any;
  public isUpdate: boolean = false;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  constructor(public router: Router, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Tooltip Form"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.getProductList();
    this.getProductAttributeList();
    this.getProductCategoryAttributeList();
    this.productAttributeForm = this.fb.group({
      id: [0],
      attributeId: [null, [Validators.required]],
      categoryId: [null, [Validators.required]],
      subCategoryId:[null],
      categoryGroupId:[null],
      active:[false],
    })
  }

  public handleSubmit() {
    this.Loader.show();
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    this.formData = this.productAttributeForm.value;

    if (this.formData.id <= 0) {
      this.formData.Action = 'add';
      this.formData.CreatedBy = decodedtoken.UserId;
    }
    else {
      this.formData.Action = 'update';
      this.formData.ModifiedBy = decodedtoken.UserId;
    }
    this.service.post(this.service.apiRoutes.Supplier.AddUpdateProductCategoryAttribute, this.formData).subscribe(res => {
      let response = res.json();
      if (response.status == 200) {
        this.toastr.success(response.message, "Success");
        this.getProductCategoryAttributeList();
        this.resetForm();
        this.Loader.hide();
      }
      else if (response.status == 400) {
        this.toastr.error(response.message, "Error");
        this.resetForm();
        this.Loader.hide();
      }
    });
  }
  update(data) {
    this.isUpdate = true;
    this.updateData = data;
    let catId = this.updateData.categoryId;
    let subCatId = this.updateData.subCategoryId;
    this.getSubProductList(catId);
    this.getProductCategoryGroupList(subCatId);
  }
  getProductList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Supplier.GetActiveProducts).subscribe(res => {
      this.productsList = res.json();
      this.Loader.hide();
    });
  }
  getSubProductList(productId) {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Supplier.GetProductSubCategoryById + `?productCatgoryId=${productId}`).subscribe(res => {
      this.subProductsList = res.json().filter(x => x.isActive);
      if (this.isUpdate) {
        this.productAttributeForm.patchValue(this.updateData);
        //this.productAttributeForm.controls['categoryGroupId'].setValue(null);
        this.isUpdate = false;
      }
      this.Loader.hide();
    });
  }
  public getProductAttributeList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Supplier.GetProductAttributeList).subscribe(res => {
      debugger;
      let data = JSON.parse(res.json());
      this.productAttributeList = data.resultData;
      this.Loader.hide();
    });
  }
  public getProductCategoryAttributeList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Supplier.GetProductCategoryAttributeList).subscribe(res => {
      let data = JSON.parse(res.json());
      this.productCategoryAttributeList = data.resultData;
      this.Loader.hide();
    });
  }
  getProductCategoryGroupList(subCategoryId: any) {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Supplier.GetProductCategoryGroupListById + `?subCategoryId=${subCategoryId}`).subscribe(res => {
                     
      this.productCategoryGroupList = (<any>res.json()).resultData;
      this.Loader.hide();
    });
  }
  get f() {
    return this.productAttributeForm.controls;
  }
  resetForm() {
    this.productAttributeForm.reset();
    this.productAttributeForm.controls['id'].setValue(0);
  }

}
