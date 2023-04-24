import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-product-attributes',
  templateUrl: './product-attributes.component.html',
  styleUrls: ['./product-attributes.component.css']
})
export class ProductAttributesComponent implements OnInit {
  public isUpdate: boolean = false;
  public updateData: any;
  public productAttributeList: any = [];
  public productCategoryGroupList: any = [];
  public productsList: any = [];
  public subProductsList: any = [];
  public productAttributeForm: FormGroup;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  public formData: any;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  constructor(public router: Router, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Tooltip Form"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.getProductAttributeList();
    this.productAttributeForm = this.fb.group({
      id: [0],
      name: ['', [Validators.required, Validators.maxLength(50), Validators.minLength(3)]],
      active: [false],
    })
  }

  public getProductAttributeList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Supplier.GetProductAttributeList).subscribe(res => {
      let data = JSON.parse(res.json());
      this.productAttributeList = data.resultData;
      this.Loader.hide();
    });
  }
  public handleSubmit() {
    console.log(this.productAttributeForm.value)
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
    this.service.post(this.service.apiRoutes.Supplier.AddUpdateProductAttribute, this.formData).subscribe(res => {
      let response = res.json();
      if (response.status == 200) {
        this.toastr.success(response.message, "Success");
        this.getProductAttributeList();
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
    this.productAttributeForm.patchValue(data);
  }
  delete(id) {
    this.Loader.show();
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    let obj = { Action: "delete", FormId: id, ModifiedBy: decodedtoken.UserId }
    this.service.post(this.service.apiRoutes.UserManagement.AddUpdateToolTipForm, obj).subscribe(res => {
      let response = res.json();
      if (response.status == 200) {
        this.toastr.success(response.message, "Success");
        this.getProductAttributeList();
        this.Loader.hide();
      }
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
