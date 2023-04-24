import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../Shared/HttpClient/_http';
import { ResponseVm } from '../../Shared/Models/HomeModel/HomeModel';

@Component({
  selector: 'app-product-colors',
  templateUrl: './product-colors.component.html',
  styleUrls: ['./product-colors.component.css']
})
export class ProductColorsComponent implements OnInit {
  public appValForm: FormGroup
  public modelText: string = "Add Product Variant";
  public variantData: any;
  public variantList = [];
  public colorName: string;
  public responcelist = new ResponseVm;

  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  constructor(public router: Router, public fb: FormBuilder, public toastrService: ToastrService, public _modalService: NgbModal, public service: CommonService, public Loader: NgxSpinnerService) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Add Product Variant"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.ColorsForm();
    this.getProductVariantList();
  }

  public ColorsForm() {
    this.appValForm = this.fb.group({
      id: [0],
      colorName: ['', Validators.required],
      hexCode: ['', Validators.required],
      isActive:[false]
    })
  }

  save() {
    debugger
    if (this.appValForm.invalid) {
      //this.submited = true;
      return;
    }
    else {
      this.variantData = this.appValForm.value;
      if (this.variantData.id == null) {
        this.variantData.id = 0;
      }
      this.service.post(this.service.apiRoutes.Supplier.AddUpdateNewVariant, this.variantData).subscribe(result => {

        this.responcelist = result.json();
        debugger
        if (this.responcelist.message == 'AlreadyExists') {

          this.toastrService.error("ColorName or HexCode Already Exist", "Error");
        }
        else if (this.responcelist.message == 'Error') {
          this.toastrService.error("Unable to save Please Try again", "Error");
        }
        else {
          this.colorName = "";
          this.variantData = null;
          this.getProductVariantList();
          this.toastrService.success("Data " + this.responcelist.message + " Successfully", "Success");
          this._modalService.dismissAll();
          this.appValForm.reset();
        }
      })
    }

  }

  update(data, content) {
    this.Loader.show();
    this.appValForm.patchValue(data);
    this.showModal(content);
    this.Loader.hide();
  }

  public getProductVariantList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Supplier.GetAllProductVariant).subscribe(result => {
      this.Loader.hide();
      this.variantList = result.json();
      debugger
      console.log(this.variantList);
    })
  }
  showModal(content) {
    this.modelText = "Add Product Category Group";
    let modRef = this._modalService.open(content, { backdrop: 'static', keyboard: false });
    modRef.result.then(() => { this.resetForm() })
  }

  resetForm(): void {
    this.appValForm.reset();
  }

}
