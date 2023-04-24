import { Component, HostListener, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { IGroupItem, IProduct, IProductDetail, ISupplierDetails, IVariantDetails, IVarientDetails } from '../../Shared/Enums/Interface';
import { UploadFileService } from '../../Shared/HttpClient/upload-file.service';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  productDetails: IProduct[]=[];
  productDetail: IProductDetail;
  variantDetails: IVariantDetails;
  public bulkInventory = new Array();
  public bulkInventorybyVarient = new Array();
  public imageLoader: boolean = false;
  public skeltonArr: any;
  constructor(public _httpService: CommonService, public Loader: NgxSpinnerService, private route: ActivatedRoute, public _fileService: UploadFileService) {
    this.skeltonArr = { productLength: Array<number>(5) }
    this.variantDetails = {} as IVariantDetails;
    this.productDetail = {} as IProductDetail;
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      
      this.getSupplierProductDetail(params['id'])
    });
  }
  getSupplierProductDetail(productId: Number) {
    this._httpService.GetData(this._httpService.apiUrls.Supplier.Product.GetSupplierProductDetail + `?productId=${productId}`, true).then(response => {
     // this.Loader.show();

      this.productDetails = (<any>response).resultData;
      let productDetail = this.productDetails.reduce((accumulated: IProductDetail[], { id, name, price, discountedPrice, description, availability, categoryName, categoryId, subCategoryName, subCategoryId, categoryGroupId, categoryGroupName }) => {
        if (!accumulated.some(o => o.id == id)) {
          let groupItem = new Array();
          let images = new Array();
          let fileIds = new Array();
          
          for (var i = 0; i < this.productDetails.length; i++) {
            if (!groupItem.some(x => x.variantId == this.productDetails[i].variantId)) {
              let obj = {
                hexCode: this.productDetails[i].hexCode,
                price: this.productDetails[i].price,
                discountedPrice: this.productDetails[i].discountedPrice,
                attributeName: this.productDetails[i].attributeName,
                attributeValue: this.productDetails[i].attributeValue,
                variantId: this.productDetails[i].variantId,
                quantity: this.productDetails[i].quantity
              }
              groupItem.push(obj)
            }
            if (this.productDetails[i].fileId) {
              if (!fileIds.some(y => y.fileId == this.productDetails[i].fileId)) {
                fileIds.push({ isMain: this.productDetails[i].isMain, fileId: this.productDetails[i].fileId, fileName: this.productDetails[i].fileName, filePath: this.productDetails[i].filePath });
                images = fileIds;
              }
            }
            //if (this.productDetails[i].fileId) {
            //  if (!fileIds.some(y => y == this.productDetails[i].fileId)) {
            //    fileIds.push(this.productDetails[i].fileId);
            //    this._fileService.getFileByName(this.productDetails[i].filePath + this.productDetails[i].fileName).then(file => {
            //      images.push(file)
            //    })
            //  }
            //}
            if (this.productDetails[i].bulkId) {
              if (!this.bulkInventory.some(y => y.bulkId == this.productDetails[i].bulkId)) {
                this.bulkInventory.push(
                  {
                    bulkId: this.productDetails[i].bulkId, minQuantity: this.productDetails[i].minQuantity, maxQuantity: this.productDetails[i].maxQuantity, bulkDiscount: this.productDetails[i].bulkdiscount, bulkPrice: this.productDetails[i].bulkPrice, bulkVarientId: this.productDetails[i].bulkVarientId
                  }
                );
              }
            }
          }
          
          accumulated.push({ id, name, price, discountedPrice,description, availability, categoryId, categoryName, subCategoryName, subCategoryId, categoryGroupId, categoryGroupName, groupItem, images});
        }
        this.imageLoader = true;
        return accumulated;
      }, []);
      this.productDetail = productDetail[0];
      console.log(this.productDetail);
      this.getVariantPrice(this.productDetail.groupItem?.[0].variantId);
      //this.Loader.hide();
    });
  }
  getVariantPrice(variantId: number) {
    this.productDetails.filter(x => x.variantId == variantId).map(x => {
      this.variantDetails = {
        price: x.price,
        discountedPrice: x.discountedPrice,
        availability: x.availability,
        quantity: x.quantity
      }
    });
    this.bulkInventorybyVarient = [];
    if (this.bulkInventory.length > 0) {
      for (let items of this.bulkInventory) {
        if (items.bulkVarientId == variantId) {
          this.bulkInventorybyVarient.push(items);
        }
      }
    }
  }
}
