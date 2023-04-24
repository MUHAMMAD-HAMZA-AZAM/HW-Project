import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt/src/jwthelper.service';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { debounce } from 'rxjs/operators';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-addsubcategory',
  templateUrl: './addsubcategory.component.html',
  styleUrls: ['./addsubcategory.component.css']
})
export class AddsubcategoryComponent implements OnInit {
  public selectedState = []; 
  public selectedColumn = [];
  public dropdownListForState = {};
  public dropdownListForColumn = {};

  public catList: any = []
  public catDropdownList = [];
  jwtHelperService: JwtHelperService = new JwtHelperService();
  public categoryForm: FormGroup;
  constructor(public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) {
    this.addNewCategoryForm();
  }

  ngOnInit() {
    this.GetCategoryDrodownList();
    this.dropdownListForState = {
      singleSelection: true,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      closeDropDownOnSelection: true,
      enableCheckAll: true
    };
    this.dropdownListForColumn = {
      singleSelection: false,
      idField: 'categoryId',
      textField: 'categoryName',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };
  }
  public dropConfig() {

  }
  public GetCategoryDrodownList() {
    this.service.get(this.service.apiRoutes.CMS.GetCategoryList).subscribe(response => {
      var res = response.json();
      this.catDropdownList = res;
      
      res.forEach(value => {
        if (value.isActive) {
          let obj = {
            id: value.categoryId,
            value  : value.categoryName,
          }
          //this.catDropdownList.push(obj)
        }
      })
      //console.log(this.catDropdownList);
    })
  }
  updateCat(cat) {
    let obj = {
      SubCategoryName: cat.subCategoryName,
      SubCategoryId: cat.subCategoryId
    }
    this.categoryForm.patchValue(obj);
  }
  deleteCat(catId) {
    this.service.PostData(this.service.apiRoutes.CMS.DeleteCategory, catId, true).then(response => {
      var res = response.json();
      if (res.status == 200) {
        this.toastr.success(res.message, "Success");
      }
      else if (res.message == "Something went wrong") {
        this.toastr.error(res.message, "Error");
      }

    });
  }
  public addNewCategoryForm() {
    this.categoryForm = this.fb.group({
      SubCategoryId: [0],
      SubCategoryName: ['', [Validators.required]],
      CategoryId: ['0', Validators.required] 
    })
  }
  public handleSubmit() {
    var obj = {};
    var decodedToken = this.jwtHelperService.decodeToken(localStorage.getItem('auth_token'));
    console.log(decodedToken);
    if (this.categoryForm.value.CategoryId <= 0) {
      obj = {
        SubCategoryName: this.categoryForm.value.SubCategoryName,
        Createdy: decodedToken.UserId,
      }
    }
    else {
      obj = {
        SubCategoryName: this.categoryForm.value.SubCategoryName,
        ModifiedBy: decodedToken.UserId,
        SubCategoryId: this.categoryForm.value.SubCategoryId
      }
    }
    this.service.PostData(this.service.apiRoutes.CMS.InsertAndUpDateSubCategory, obj, true).then(response => {
      var res = response.json();
      if (res.status == 200) {
        this.toastr.success(res.message, "Success");
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
    console.log(item);
    
    this.selectedState = [];
    this.selectedState.push(item);
    //console.log(this.selectedCategories);
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
    //console.log(this.selectedCategories);
  }
  onColumnDeSelect(item: any) {

    this.selectedColumn = this.selectedColumn.filter(
      function (value, index, arr) {
        return value.stateId != item.stateId;
      });
  }
}
