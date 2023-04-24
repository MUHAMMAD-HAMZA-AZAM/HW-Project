import { HttpStatusCode } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { ResponseVm, StatusCode } from '../../Shared/Enums/enum';
import { IPageSeoVM, IProductDetail, IProductVM, IQueryPramProductByName, ISupplierProduct } from '../../Shared/Enums/Interface';
import { CartService } from '../../Shared/HttpClient/cart.service';
import { CommonService } from '../../Shared/HttpClient/HttpClient';
import { metaTagsService } from '../../Shared/HttpClient/seo-dynamic.service';
import { UploadFileService } from '../../Shared/HttpClient/upload-file.service';
import { OrderItem } from '../../Shared/Models/IOrderItem';
import { IProduct } from '../../Shared/Models/IProduct';

@Component({
  selector: 'app-products-by-name',
  templateUrl: './products-by-name.component.html',
  styleUrls: ['./products-by-name.component.css']
})
export class ProductsByNameComponent implements OnInit {
  pageSize:number = 18;
  pageNumber: number = 1;
  notFound: boolean = false;
  obj: IQueryPramProductByName;
  public searchProductName: string="";
  public noOfRecords: number=0
  productList: ISupplierProduct[] = [];
  public imageLoader: boolean = false;
  public skeltonArr: any;
  productDetails: IProductVM[] = [];
  productDetail: any = "";
  public socialLinks: any;
  public response: ResponseVm = new ResponseVm();
  cartItems: OrderItem[] = [];
  public prodId: number = 0;
  public cartProductVariantIdAvailableStock: number = 0;
  public variantId: number = 0;
  public selectedVariantIdAvailableStock: number = 0;
  variantDetails: any = null;
  quantity: number = 1;
  public varientPrice: number = 0;
  public actualVarientPrice: number = 0;
  public bulkInventory = new Array();
  selectedFileName: string = "";
  public varientId: number = 0;
  public variantColor = '';
  public variantHex = '';
  public bulkInventorybyVarient = new Array();
  constructor(private route: ActivatedRoute,
        private _metaService: metaTagsService,
        public _httpService: CommonService,
        public _fileService: UploadFileService,
        public _cartService: CartService,
      private toastr: ToastrService,

        private _modalService: NgbModal ) {
        this.skeltonArr = { catLength: Array<number>(3), productLength: Array<number>(6) }
  this.obj={} as IQueryPramProductByName;
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(x => {
      this.obj = {
        pageNumber: this.pageNumber,
        pageSize: this.pageSize,
        name: x.productName,
      }
      this.getProductsByName(this.obj);
    })
    this.bindMetaTags();
    this._cartService.getItems().subscribe(res => {
      this.cartItems = res;
      if (this.cartItems.length > 0) {
        for (var item = 0; item < this.cartItems.length; item++) {
          if ((this.prodId && this.variantId) == (this.cartItems[item].product.id && this.cartItems[item].product.variantId)) {
            this.cartProductVariantIdAvailableStock = this.cartItems[item].product.stockQuantity - this.cartItems[item].quantity;
            //console.log(`Cart Product VariantId Quantity in Cart ${this.cartItems[item].quantity}`);
            this.selectedVariantIdAvailableStock = this.cartProductVariantIdAvailableStock > 0 ? this.cartProductVariantIdAvailableStock : 0;
            //console.log(`Selected VariantId Available Stock ${this.selectedVariantIdAvailableStock}`);
          }
        }
      }
    });
  }

  getProductsByName(obj: IQueryPramProductByName) {
    this._httpService.PostData(this._httpService.apiUrls.Supplier.Product.GetProductsByName, obj, false).then(res => {
      this.productList = res.resultData;
      if (res.status == HttpStatusCode.Ok) {
        //this.productList.forEach((x, i) => {
        //  if (x.fileName) {
        //    this._fileService.getFileByName(x.filePath + x.fileName).then(file => {
        //      this.productList[i].fileName = file;
        //    })
        //  }
        //  else {
        //    this.productList[i].fileName = '';
        //  }
        //})
        this.imageLoader = true;
        this.notFound = false;
        this.noOfRecords = this.productList[0].noOfRecords;
        this.searchProductName = obj.name;


      }
      else {
        this.imageLoader = true;
        this.noOfRecords = 0;
        this.notFound = true;
        this.searchProductName = "Record Not Found!";
      }
    })
  }
  getPostByPage(page:number) {
    this.obj.pageNumber = page;
    this.getProductsByName(this.obj);
  }
  public bindMetaTags() {
    this._httpService.get(this._httpService.apiUrls.CMS.GetSeoPageById + "?pageId=21").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description, res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }
  getProductDetailsWeb(productId: number, buyNowModelPopUp?: TemplateRef<any>) {
   
    this.selectedFileName = "";
    this._httpService.GetData1(this._httpService.apiUrls.Supplier.Product.GetProductDetailWeb + `?productId=${productId}`).then(response => {
      this.productDetails = (<any>response).resultData;
      let productDetail = this.productDetails.reduce((accumulated: IProductDetail[], { id, name, slug, price, discountedPrice, weight, variantName, traxCityId, discount, attributeName, attributeValue, shopUrl, shopName, description, youtubeURL, supplierId, firebaseClientId, availability, categoryName, categoryId, subCategoryName, subCategoryId, categoryGroupId, categoryGroupName, variantId, hexCode, isFreeShipping }) => {
        if (!accumulated.some(o => o.id == id)) {
          let groupItem = new Array();
          let attributes = new Array();
          let images = new Array();
          let fileIds = new Array();
          for (var i = 0; i < this.productDetails.length; i++) {
            if (!groupItem.some(x => x.variantId == this.productDetails[i].variantId)) {
              let obj = {
                hexCode: this.productDetails[i].hexCode,
                price: this.productDetails[i].price,
                discount: this.productDetails[i].discount,
                discountedPrice: this.productDetails[i].discountedPrice,
                attributeName: this.productDetails[i].attributeName,
                attributeValue: this.productDetails[i].attributeValue,
                variantId: this.productDetails[i].variantId,
                supplierId: this.productDetails[i].supplierId,
                quantity: this.productDetails[i].quantity
              }

              groupItem.push(obj)
            }
            if (!attributes.some(x => x.attributeId == this.productDetails[i].attributeId)) {
              let attrobj = {
                attributeId: this.productDetails[i].attributeId,
                attributeName: this.productDetails[i].attributeName,
                attributeValue: this.productDetails[i].attributeValue
              }
              attributes.push(attrobj)
            }
            if (this.productDetails[i].fileId) {
              if (!fileIds.some(y => y.fileId == this.productDetails[i].fileId)) {
                fileIds.push({
                  fileId: this.productDetails[i].fileId,
                  filePath: this.productDetails[i].filePath,
                  fileName: this.productDetails[i].fileName,
                  isMain: this.productDetails[i].isMain
                });
                images = fileIds;
                //this._fileService.getFileByName(this.productDetails[i].filePath + this.productDetails[i].fileName, { isMain: this.productDetails[i].isMain }).then(data => {
                //  images.push({ fileName: data.file, isMain: data.isMain })
                //})
              }
            }
            if (this.productDetails[i].bulkId) {
              if (!this.bulkInventory.some(y => y.bulkId == this.productDetails[i].bulkId)) {
                this.bulkInventory.push(
                  {
                    bulkId: this.productDetails[i].bulkId, minQuantity: this.productDetails[i].minQuantity, maxQuantity: this.productDetails[i].maxQuantity, bulkDiscount: this.productDetails[i].bulkdiscount, bulkPrice: this.productDetails[i].bulkPrice, bulkVarientId: this.productDetails[i].bulkVarientId, bulkProductId: this.productDetails[i].bulkProductId
                  }
                );
              }
            }
          }
            accumulated.push({ id, name, slug, price, discountedPrice, weight, traxCityId, discount, variantId, hexCode, shopName, shopUrl, attributeName, attributeValue, description, youtubeURL, supplierId, firebaseClientId, availability, categoryId, categoryName, subCategoryName, subCategoryId, categoryGroupId, categoryGroupName, groupItem, attributes, images, isFreeShipping });
        }
        this.imageLoader = true;
        return accumulated;
      }, []);
      this.productDetail = productDetail[0];
      if (this.productDetail != null) {
        this.populateSocialLinks();
      }
      this._metaService.updateTags(this.productDetail.name + ": mall.hoomwork", this.productDetail.name + this.productDetail.slug, this.productDetail.description, this.productDetail.youtubeURL, this.productDetail.name + this.productDetail.slug, this.productDetail.slug + ": mall.hoomwork", this.productDetail.description)
      let productattributes = this.productDetail.attributes[0];
      //if (productattributes.attributeId == null) {
      //  this.isSpecification = true;
      //}
      //if (this.variantIdParam != "") {
      //  this.getVariantPrice(this.variantIdParam);
      //this.variantId = this.variantIdParam;
      //}
      //else {
      this.prodId = productId;
      this.getVariantPrice(this.productDetail.groupItem[0].variantId);
      //}
    });
    this._modalService.open(buyNowModelPopUp, { size: 'xl', centered: true });
  }
  checkOut() {
    if (this.quantity > this.selectedVariantIdAvailableStock) {
      this.toastr.warning("Product is out of stock ", "Sorry");
      this.quantity = this.selectedVariantIdAvailableStock;
      this._modalService.dismissAll();
      return;
    }
    else {

      let product: IProduct = {
        id: this.productDetail.id,
        name: this.productDetail.name,
        weight: this.productDetail.weight,
        traxCityId: this.productDetail.traxCityId,
        slug: this.productDetail.slug,
        variantId: (this.variantDetails ? this.variantDetails.variantId : this.productDetail.variantId) || this.variantId,
        hexCode: this.variantDetails.hexCode,
        price: this.varientPrice,
        discount: this.variantDetails ? this.variantDetails.discount : this.productDetail.discount,
        actualVarientPrice: this.actualVarientPrice,
        discountedPrice: this.variantDetails ? this.variantDetails.discountedPrice : this.productDetail.discountedPrice,
        supplierId: this.productDetail.supplierId,
        firebaseClientId: this.productDetail.firebaseClientId,
        categoryId: this.productDetail.categoryId,
        subCategoryId: this.productDetail.subCategoryId ? this.productDetail.subCategoryId : 0,
        categoryGroupId: this.productDetail.categoryGroupId ? this.productDetail.categoryGroupId : 0,
        stockQuantity: this.selectedVariantIdAvailableStock,
        bulkInventory: this.bulkInventory,
        fileName: this.productDetails[0].fileName,
        filePath: this.productDetails[0].filePath,
        varientName: this.variantDetails.varientName,
        isFreeShipping: this.productDetail.isFreeShipping
      }
      let orderItem: OrderItem =
      {
        product, quantity: this.quantity,
      };
      console.log(product);
      this._cartService.addItem(orderItem, this.bulkInventory);
      //this.toastr.success("Item added to cart", "Cart");
      this.quantity = 1;
      this._modalService.dismissAll();
      this._httpService.NavigateToRoute(this._httpService.apiRoutes.Cart.viewcart);
    }
  }
  continueShopping() {
    this._modalService.dismissAll();
  }
  public populateSocialLinks() {
    this.socialLinks = [];
    let supplierId = this.productDetail.supplierId;
    this._httpService.GetData(this._httpService.apiUrls.Supplier.GetSocialLinks + "?supplierId=" + supplierId, true).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        this.socialLinks = this.response.resultData;

      }

    }, error => {
      this._httpService.Loader.show();
    });
  }
  getVariantPrice(variantId: number) {
    debugger;
    if (this.cartItems?.length) {
      debugger;
      console.log(this.prodId);
      console.log(variantId);
      console.log(this.cartItems);

      for (var item = 0; item < this.cartItems.length; item++) {
        if ((this.prodId && variantId) == (this.cartItems[item].product.id && this.cartItems[item].product.variantId)) {
          this.cartProductVariantIdAvailableStock = this.cartItems[item].product.stockQuantity - this.cartItems[item].quantity;
          // console.log(`Cart Product VariantId Quantity in Cart ${this.cartItems[item].quantity}`);
          this.selectedVariantIdAvailableStock = this.cartProductVariantIdAvailableStock;
          debugger;
          
          console.log(`Selected VariantId Available Stock ${this.selectedVariantIdAvailableStock}`);
          if (this.varientId != variantId) {
            this.quantity = 1;
          }
          //if (this.variantIdParam != "" && this.variantId != null && this.varientId != variantId) {
          //  this.variantId = this.variantIdParam;
          //}


          this.productDetails.filter(x => x.variantId == this.cartItems[item].product.variantId).map(x => {
            this.variantDetails = {
              variantId: x.variantId,
              hexCode: x.hexCode,
              price: x.price,
              discount: x.discount,
              availability: x.availability,
              discountedPrice: x.discountedPrice,
              discountInPercentage: x.discountInPercentage,
              quantity: this.cartProductVariantIdAvailableStock,
              varientName: x.variantName
            }
          });

          console.log('this.variantDetails');
          console.log(this.variantDetails);

          this.selectedVariantIdAvailableStock = this.variantDetails.quantity;
          this.varientPrice = this.variantDetails.price;

          this.actualVarientPrice = this.variantDetails.discountedPrice;
          this.variantId = this.variantDetails.variantId;

          // console.log(`actualVarientPrice =  ${this.actualVarientPrice}`);
          // console.log(`Cart Product VariantId Available Stock ${this.variantDetails.quantity}`);

          this.bulkInventorybyVarient = [];
          for (let items of this.bulkInventory) {
            if (items.bulkVarientId == variantId) {
              this.bulkInventorybyVarient.push(items);
            }
          }
          this.variantColor = this.variantDetails.varientName;
          this.variantHex = this.variantDetails.hexCode;
          this.varientId = variantId;
          return;
          // CHILD IF END
        }
        else {
          // CHILD ELSE END
          if (this.varientId != variantId) {
            this.quantity = 1;
          }
          //if (this.variantIdParam != "" && this.variantId != null && this.varientId != variantId) {
          //  this.variantId = this.variantIdParam;
          //}



          this.productDetails.filter(x => x.variantId == variantId).map(x => {
            this.variantDetails = {
              variantId: x.variantId,
              hexCode: x.hexCode,
              price: x.price,
              discount: x.discount,
              availability: x.availability,
              discountedPrice: x.discountedPrice,
              discountInPercentage: x.discountInPercentage,
              quantity: x.quantity,
              varientName: x.variantName
            }
          });

          console.log('this.variantDetails else');
          console.log(this.variantDetails);
          this.selectedVariantIdAvailableStock = this.variantDetails.quantity;
          this.varientPrice = this.variantDetails.price;
          this.variantId = this.variantDetails.variantId;
          this.actualVarientPrice = this.variantDetails.discountedPrice;
          //console.log(`Selected VariantId Available Stock ${this.variantDetails.quantity}`);

          this.bulkInventorybyVarient = [];
          for (let items of this.bulkInventory) {
            if (items.bulkVarientId == variantId) {
              this.bulkInventorybyVarient.push(items);
            }
          }
          this.varientId = variantId;
        }
        this.variantColor = this.variantDetails.varientName;
        this.variantHex = this.variantDetails.hexCode;
      }


      //MAIN IF END
    }
    else {
      //MAIN else START
      debugger;
      console.log(`varientID ${this.varientId}`);
      console.log(`varient param ${variantId}`);

      if (this.varientId != variantId) {
        this.quantity = 1;
      }

      //if (this.variantIdParam != "" && this.variantId != null && this.varientId != variantId) {
      //  this.variantId = this.variantIdParam;
      //}
      console.log("this.productDetails");
      console.log(this.productDetails);

      this.productDetails.filter(x => x.variantId == variantId).map(x => {
        this.variantDetails = {
          variantId: x.variantId,
          hexCode: x.hexCode,
          price: x.price,
          discount: x.discount,
          availability: x.availability,
          discountedPrice: x.discountedPrice,
          discountInPercentage: x.discountInPercentage,
          quantity: x.quantity,
          varientName: x.variantName
        }
      });
      console.log("this.variantDetails");
      console.log(this.variantDetails);
      this.variantColor = this.variantDetails.varientName;
      this.variantHex = this.variantDetails.hexCode;


      this.selectedVariantIdAvailableStock = this.variantDetails.quantity;
      this.varientPrice = this.variantDetails.price;
      this.variantId = this.variantDetails.variantId;
      // console.log(`actualVarientPrice =  ${this.variantDetails.discountedPrice}`);
      this.actualVarientPrice = this.variantDetails.discountedPrice;
      // console.log(`Selected VariantId Available Stock ${this.variantDetails.quantity}`);

      this.bulkInventorybyVarient = [];
      for (let items of this.bulkInventory) {
        if (items.bulkVarientId == variantId) {
          this.bulkInventorybyVarient.push(items);
        }
      }
      this.varientId = variantId;
    }

  }
  openLink(url: string) {
    window.open(url);
  }
  handleQuantityChange(op: string) {
    if (op == "+") {
      if (this.selectedVariantIdAvailableStock == 0) {
        this.toastr.warning("Product is out of stock ", "Sorry");
        this.quantity = this.selectedVariantIdAvailableStock;
        return;
      }
      else if (this.quantity >= this.selectedVariantIdAvailableStock) {

        this.toastr.warning("You can only buy available stock quantity ", "Sorry");
        this.quantity = this.selectedVariantIdAvailableStock;
        return;
      }

      else {
        this.quantity++;
      }
      debugger;
      console.log('this.bulkInventory');
      console.log(this.bulkInventory);
      for (let items of this.bulkInventory) {
        if (items.minQuantity <= this.quantity && this.quantity <= items.maxQuantity && items.bulkVarientId == this.variantId) {
          this.getBulkVarientPrice(items);
        }
      }

    }

    else {
      if (this.quantity < 2) {
        return;
      }
      else {
        this.quantity--;

        for (let items of this.bulkInventory) {
          if (items.minQuantity <= this.quantity && this.quantity <= items.maxQuantity && items.bulkVarientId == this.variantId) {
            this.getBulkVarientPrice(items);
            break;
          }
          else {

            if (items.maxQuantity > this.quantity && items.minQuantity > this.quantity && items.bulkVarientId == this.variantId) {

              this.getVariantPrice(this.varientId);
              break;
            }
            else if (items.maxQuantity > this.quantity && items.bulkVarientId == this.variantId) {

              this.getVariantPrice(this.varientId);
              break;
            }
          }
        }
      }
    }

  }
 
  addToCart() {
    if (this.quantity > this.selectedVariantIdAvailableStock) {
      this.toastr.warning("Product is out of stock ", "Sorry");
      this.quantity = this.selectedVariantIdAvailableStock;
      return;
    }
    else {
      let product: IProduct = {
        id: this.productDetail.id,
        name: this.productDetail.name,
        weight: this.productDetail.weight,
        traxCityId: this.productDetail.traxCityId,
        slug: this.productDetail.slug,
        variantId: (this.variantDetails ? this.variantDetails.variantId : this.productDetail.variantId) || this.variantId,
        hexCode: this.variantDetails.hexCode,
        price: this.varientPrice,
        discount: this.variantDetails ? this.variantDetails.discount : this.productDetail.discount,
        actualVarientPrice: this.actualVarientPrice,
        discountedPrice: this.variantDetails ? this.variantDetails.discountedPrice : this.productDetail.discountedPrice,
        supplierId: this.productDetail.supplierId,
        firebaseClientId: this.productDetail.firebaseClientId,
        categoryId: this.productDetail.categoryId,
        subCategoryId: this.productDetail.subCategoryId ? this.productDetail.subCategoryId : 0,
        categoryGroupId: this.productDetail.categoryGroupId ? this.productDetail.categoryGroupId : 0,
        stockQuantity: this.selectedVariantIdAvailableStock,
        bulkInventory: this.bulkInventory,
        fileName: this.productDetails[0].fileName,
        filePath: this.productDetails[0].filePath,
        varientName: this.variantDetails.varientName,
        isFreeShipping: this.productDetail.isFreeShipping
      }
      let orderItem: OrderItem =
      {
        product, quantity: this.quantity,
      };

      this._cartService.addItem(orderItem, this.bulkInventory);
      this.toastr.success("Item added to cart", "Cart");
      this.quantity = 1;
    }

  }
  selectFile(filePath: string, fileName: string) {
    this.selectedFileName = this._httpService.AWSPATH + filePath + fileName;
  }
  previousImage() {
    let index = 0;
    for (let i = 0; i < this.productDetail?.images.length; i++) {
      if (this.selectedFileName) {
        if (this._httpService.AWSPATH + this.productDetail?.images[i]?.filePath + this.productDetail?.images[i]?.fileName === this.selectedFileName) {
          index = i;
          break;
        }
      }
      else {
        index = this.productDetail?.images.length;
      }
    }
    if (index > 0) {
      index--;
      this.selectedFileName = this._httpService.AWSPATH + this.productDetail?.images[index].filePath + this.productDetail?.images[index].fileName;
    }
    else {
      index = (this.productDetail?.images.length) - 1;
      this.selectedFileName = this._httpService.AWSPATH + this.productDetail?.images[index].filePath + this.productDetail?.images[index].fileName;
    }
  }
  nextImage() {
    let index = 0;
    for (let i = 0; i < this.productDetail?.images.length; i++) {
      if (this.selectedFileName) {
        if (this._httpService.AWSPATH + this.productDetail?.images[i]?.filePath + this.productDetail?.images[i]?.fileName === this.selectedFileName) {
          index = i;
          break;
        }
      }
      else {
        index = 0;
      }
    }
    if (index < this.productDetail?.images.length) {
      index = ((index == (this.productDetail?.images.length - 1)) ? 0 : ++index);
      this.selectedFileName = this._httpService.AWSPATH + this.productDetail?.images[index].filePath + this.productDetail?.images[index].fileName;
    }
    else {
      index++;
      this.selectedFileName = this._httpService.AWSPATH + this.productDetail?.images[index].filePath + this.productDetail?.images[index].fileName;
    }
  }

  getBulkVarientPrice(bulk: any) {
    let discounted = (bulk.bulkPrice / 100) * bulk.bulkDiscount;
    let discountedPrice = bulk.bulkPrice - discounted;
    this.variantDetails = {
      price: this.varientPrice,
      availability: this.productDetails[0].availability,
      discountedPrice: discountedPrice,
      varientName: this.variantColor,
      hexCode: this.variantHex,
    }
  }
  onQuantityChange(quantityValue) {
    if (quantityValue != "")
      this.quantity = Number(quantityValue);
    if (this.quantity > 0) {
      if (this.selectedVariantIdAvailableStock == 0) {
        this.toastr.warning("Product is out of stock ", "Sorry");
        this.quantity = this.selectedVariantIdAvailableStock;
        return;
      }
      else if (this.quantity > this.selectedVariantIdAvailableStock) {
        this.toastr.warning("You can only buy available stock quantity ", "Sorry");
        //this.quantity = this.selectedVariantIdAvailableStock;
        return;
      }
      for (let items of this.bulkInventory) {
        if (items.minQuantity <= this.quantity && this.quantity <= items.maxQuantity && items.bulkVarientId == this.variantId) {
          this.getBulkVarientPrice(items);
        }
        if (items.minQuantity <= this.quantity && this.quantity <= items.maxQuantity && items.bulkVarientId == this.variantId) {
          this.getBulkVarientPrice(items);
          break;
        }
        else {
          if (items.maxQuantity > this.quantity && items.minQuantity > this.quantity && items.bulkVarientId == this.variantId) {
            this.getVariantPrice(this.varientId);
            break;
          }
          else if (items.maxQuantity > this.quantity && items.bulkVarientId == this.variantId) {
            this.getVariantPrice(this.varientId);
            break;
          }
        }
      }
    }
  }
  onFocusOutEvent(event: any) {
    if (event.target.value == "") {
      this.quantity = 1;
    }
  }

}
