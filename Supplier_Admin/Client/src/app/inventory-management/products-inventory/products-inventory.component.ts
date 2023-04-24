import { HttpResponse, HttpStatusCode } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ServerResponse } from 'http';
import { ToastrService } from 'ngx-toastr';
import { ResponseVm } from '../../Shared/Enums/common';
import { IProduct, IProductStockDTO } from '../../Shared/Enums/Interface';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-products-inventory',
  templateUrl: './products-inventory.component.html',
  styleUrls: ['./products-inventory.component.css']
})
export class ProductsInventoryComponent implements OnInit {
  pageNumber: number = 1;
  pageSize: number = 20;
  isEditEnabled: boolean = false;
  btnEditText: string = "Update Stock";
  productList: IProduct[] = [];
  variantList: IProduct[] = [];
  productStockList: IProductStockDTO[] = [];
  constructor(public _http: CommonService, private _modalService: NgbModal, public toastr: ToastrService) { }

  ngOnInit(): void {
    
    this.getProductsList();
  }
  getVariantByProductId(productId?: number, variantModalTemplate?: TemplateRef<any>) {
    this.isEditEnabled = false;
    this.btnEditText = "Update Stock";
    this._http.GetData1(this._http.apiUrls.Supplier.Product.GetVariantsByProductId + `?productId=${productId}`,true).then(product => {
      
      this.variantList = product.resultData
      this._modalService.open(variantModalTemplate, { size: 'lg', centered:true });
    })
  }

  eidtQuantity() {
    debugger
    if (this.isEditEnabled) {
      this.variantList.forEach((product) => {
        let obj = {
          productId: product.id,
          varientId: product.variantId,
          quantity: product.quantity
        }
        this.productStockList.push(obj);
      });
      this._http.post(this._http.apiUrls.Supplier.Product.UpdateStockLevel, JSON.stringify(this.productStockList)).subscribe(response => {
        debugger
        if (response.status == HttpStatusCode.Ok) {
          this.isEditEnabled = false;
          this.btnEditText = "Update Stock";
          this._modalService.dismissAll();
          this.toastr.success("Stock level updated successfully","Updated");
        }
        else {
          this.toastr.error("Something went wrong please try again");
        }
      });
    }
    else {
      this.btnEditText = "Update";
      this.isEditEnabled = true;
    }
  }

  getProductsList() {
    
    let obj = {
      supplierId: this._http.decodedToken().Id,
      pageNumber: this.pageNumber,
      pageSize: this.pageSize
    }
    this._http.PostData(this._http.apiUrls.Supplier.Product.GetSupplierProductList, obj,false).then(product => {
      
      this.productList = product.resultData
    })
  }
}
