
import { HttpStatusCode } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
//import * as XLSX from 'xlsx';
import { CommonService } from '../../Shared/HttpClient/_http';
@Component({
  selector: 'app-add-bulk-product',
  templateUrl: './add-bulk-product.component.html',
  styleUrls: ['./add-bulk-product.component.css']
})
export class AddBulkProductComponent implements OnInit {
  public finalData: any;
  public products: any;
  public allCatSubGroupCategories: any;
  constructor(public service: CommonService, public Loader: NgxSpinnerService, public _toastr: ToastrService) { }

  ngOnInit(): void {
    debugger;
    this.getAllCatSubCatGroupCategories();
  }

  onFileChange(ev) {
    debugger;
    let workBook = null;
    let jsonData = null;
    const reader = new FileReader();
    const file = ev.target.files[0];
    //reader.onload = (event) => {
    //  const data = reader.result;
    //  workBook = XLSX.read(data, { type: 'binary' });
    //  jsonData = workBook.SheetNames.reduce((initial, name) => {
    //    const sheet = workBook.Sheets[name];
    //    initial["productData"] = XLSX.utils.sheet_to_json(sheet);
    //    return initial;
    //  }, {});
    //  debugger;
    //  this.finalData = JSON.stringify(jsonData);
    //  this.uploadProducts();
    //}
    reader.readAsBinaryString(file);
  }
  uploadProducts() {
    this.Loader.show();
    this.products = JSON.parse(this.finalData);
    this.Loader.hide();
  }
  getAllCatSubCatGroupCategories() {
    this.service.get(this.service.apiUrls.Supplier.Product.GetAllCatSubCatGroupCategories).subscribe(res => {
      debugger;
      this.allCatSubGroupCategories = res.resultData;
    });
  }
  save() {
    this.Loader.show();
    for (var i in this.products.productData) {
      var data = <any>this.products.productData[i];
      debugger;
      let categoryId = this.allCatSubGroupCategories.categories.filter(x => x.text == data.Category)[0].id;
      let subCategoryId = this.allCatSubGroupCategories.subCategories.filter(x => x.text == data.SubCategory)[0].id;
      let gruopCategoryId = this.allCatSubGroupCategories.groupCategories.filter(x => x.text == data.GroupCategory)[0].id;
      let varientId = this.allCatSubGroupCategories.productVariants.filter(x => x.text == data.ProductVariant)[0].id;
      let formData = {
        bulkSku: [{ bulkDiscount: null, bulkPrice: data.BulkPrice, maxQuantity: data.MaxQuantity, minQuantity: data.MinQuantity }],
        categoryId: categoryId,
        description: data.Description,
        isActive: data.IsActive,
        productAttributes: [],
        productSku: [{ DiscountInPercentage: data.DiscountInPercentage, availability: data.Availability, price: data.Price, productVariantId: varientId, quantity: data.Quantity }],
        searchTag: [{ display: data.SearchTag, value: undefined }],
        selectedCategory: "",
        slug: data.Title,
        subCategoryGroupId: gruopCategoryId,
        subCategoryId: subCategoryId,
        supplierId: this.service.decodedToken().Id,
        title: data.Title,
        weight: data.Weight,
        youtubeURL: data.YoutubeURL
      };
      this.service.post(this.service.apiUrls.Supplier.Product.AddNewSupplierProduct, formData).subscribe(response => {
        let res = <any>response;
        if (res.status == HttpStatusCode.Ok) {
          this._toastr.success(response.message, "Success");
        }
      });
    }
    this.Loader.hide();
  }
  public deleteProduct(index) {
    this.products.productData.splice(index, 1);
  }

}

