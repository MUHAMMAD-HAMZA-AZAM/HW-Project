import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../Shared/HttpClient/_http';
import { ResponseVm } from '../../Shared/Models/HomeModel/HomeModel';

@Component({
  selector: 'app-product-category-group',
  templateUrl: './product-category-group.component.html',
  styleUrls: ['./product-category-group.component.css']
})
export class ProductCategoryGroupComponent implements OnInit {
  public appValForm: FormGroup;

  public modelText: string = "Add Product Category Group";
  public subcategoryList = [];
  public categoryList = [];
  public productcategorygroupList: [] = [];
  public productNameExist: string = "";
  public Name: string = "";
  public subcategoryShow: boolean=false;
  public productDate: any;
  public selectedSubCategories = [];
  public responcelist = new ResponseVm;

  jwtHelperService: JwtHelperService = new JwtHelperService();
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  constructor(public router: Router, public fb: FormBuilder, public toastrService: ToastrService, public _modalService: NgbModal, public service: CommonService, public Loader: NgxSpinnerService) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Add Product Category Group"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
      this.populateCategories();
      this.getSubProductsList();
      this.accountForm();
    }

  public accountForm() {
    this.appValForm = this.fb.group({
      id: [null],
      productCategoryId:[null],
      productCategoryGroupId: [null],
      subCategoryId: [null, Validators.required],
      name: ['', [Validators.required, Validators.maxLength(150)]],
      active: [false],
      seoTitle: ['', Validators.required,],
      seoDescription: ['', Validators.required,],
      ogTitle: ['', Validators.required,],
      ogDescription: ['', Validators.required,],
      canonical: [null],
    })
  }

  onOptionsSelected(categoryId: string) {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Supplier.GetSubCategoriesForDropDown + "?categoryId=" + categoryId).subscribe(result => {
      this.subcategoryList = result.json();
      this.subcategoryShow = true;
      console.log(result.json());
      this.Loader.hide();
    },
      error => {
        console.log(error);
      });
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
    if (this.appValForm.invalid) {
      //this.submited = true;
      return;
    }
    else {
      this.productDate = this.appValForm.value;
      if (this.productDate.productCategoryGroupId == null) {
        this.productDate.id = 0;
      }
      else {
        this.productDate.id = this.productDate.productCategoryGroupId;
      }
      this.service.post(this.service.apiRoutes.Supplier.AddUpdateProductsCategoryGroup, this.productDate).subscribe(result => {

        this.responcelist = result.json();
        if (this.responcelist.message == 'AlreadyExists') {

          this.toastrService.error("Product Name Already Exist", "Error");
        }
        else if (this.responcelist.message == 'Error')
        {
          this.toastrService.error("Unable to save Please Try again", "Error");
        }
        else {
          this.Name = "";
          this.productDate = null;
          this.getSubProductsList();
          this.toastrService.success("Data " + this.responcelist.message + " Successfully", "Success");
          this._modalService.dismissAll();
          this.appValForm.reset();
        }
      })
    }

  }

  update(data, content) {
    this.onOptionsSelected(data.productCategoryId);
    this.Loader.show();
    data.id = data.productCategoryId;
    this.appValForm.patchValue(data);
    this.showModal(content);
    this.Loader.hide();
  }

  deleteSubProductGroup(product, deleteContent) {
    this.productDate = product
    this.Name = product.name;
    this.productDate.Id = product.productCategoryGroupId;
    this._modalService.open(deleteContent);
  }

  confirmDeleteCategoryGroup() {
    if (this.Name != null && this.Name != "") {
      this.productDate.active = !this.productDate.active;
      this.service.post(this.service.apiRoutes.Supplier.AddUpdateProductsCategoryGroup, this.productDate).subscribe(result => {

        if (result.status == 200) {
          this.toastrService.warning("Status change successfully!", "Information");
          this._modalService.dismissAll();
          this.getSubProductsList();
        }
        else {
          this.toastrService.warning("Something went wrong please try again", "Error");
          this._modalService.dismissAll();
        }
      })
    }
  }


  public getSubProductsList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Supplier.GetAllProduCatcategoryGroup).subscribe(result => {
      this.Loader.hide();
      this.productcategorygroupList = result.json();
      console.log(this.productcategorygroupList);
    })
  }
  showModal(content) {
    this.subcategoryShow = false;
    this.modelText = "Add Product Category Group";
    let modRef = this._modalService.open(content, { centered: true, size: 'lg', backdrop: 'static', keyboard: false });
    modRef.result.then(() => { this.resetForm() })
  }

  resetForm(): void {
    this.appValForm.reset();
    this.appValForm.controls['subcategoryId'].setValue(0);
  }
  
}
