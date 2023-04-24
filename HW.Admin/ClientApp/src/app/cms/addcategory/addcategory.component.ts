import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-addcategory',
  templateUrl: './addcategory.component.html',
  styleUrls: ['./addcategory.component.css']
})
export class AddcategoryComponent implements OnInit {
  public catList: any = []
  jwtHelperService: JwtHelperService = new JwtHelperService();
  public categoryForm: FormGroup;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };

  constructor(public router: Router, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) {
    this.addNewCategoryForm();
  }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Add Category"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.GetCategoryList();
  }
  public GetCategoryList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.CMS.GetCategoryList).subscribe(response => {
      var res = response.json();
      this.catList = res;
      this.Loader.hide();
    })
  }
  updateCat(cat) {
    let obj = {
      CategoryName: cat.categoryName,
      CategoryId: cat.categoryId
    }
    this.categoryForm.patchValue(obj);
  }
  deleteCat(catId) {
    this.service.PostData(this.service.apiRoutes.CMS.DeleteCategory, catId, true).then(response => {
      var res = response.json();
      if (res.status == 200) {
        this.toastr.success(res.message, "Success");
        this.GetCategoryList();
      }
      else if (res.message == "Something went wrong") {
        this.toastr.error(res.message, "Error");
      }

    });
  }
  public addNewCategoryForm() {
    this.categoryForm = this.fb.group({
      CategoryId: [0],
      CategoryName: ['', [Validators.required]],
    })
  }
  public handleSubmit() {
    var obj = {};
    var decodedToken = this.jwtHelperService.decodeToken(localStorage.getItem('auth_token'));
    if (this.categoryForm.value.CategoryId <= 0) {
      obj = {
        CategoryName: this.categoryForm.value.CategoryName,
        Createdy: decodedToken.UserId,
      }
    }
    else {
      obj = {
        CategoryName: this.categoryForm.value.CategoryName,
        ModifiedBy: decodedToken.UserId,
        CategoryId: this.categoryForm.value.CategoryId
      }
    }
    this.service.PostData(this.service.apiRoutes.CMS.InsertAndUpDateCategory, obj, true).then(response => {
      var res = response.json();
      if (res.status == 200) {
        this.toastr.success(res.message, "Success");
        this.GetCategoryList();
        this.resetForm();
      }
      else if (res.message == "alreadyexist") {
        this.toastr.error("Category name already exist!", "Duplicate");
      }
      console.log(res);
    })
  }
  get f() {
    return this.categoryForm.controls;
  }
  resetForm() {
    this.categoryForm.reset();
    this.categoryForm.controls['CategoryId'].setValue(0);

  }
}
