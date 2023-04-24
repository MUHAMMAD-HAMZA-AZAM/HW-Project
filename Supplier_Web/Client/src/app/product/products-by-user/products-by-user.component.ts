import { HttpStatusCode } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ResponseVm, StatusCode } from '../../Shared/Enums/enum';
import { IIdValue, IProductByUser, IProductDetail, IProductVM } from '../../Shared/Enums/Interface';
import { CartService } from '../../Shared/HttpClient/cart.service';
import { CommonService } from '../../Shared/HttpClient/HttpClient';
import { metaTagsService } from '../../Shared/HttpClient/seo-dynamic.service';
import { UploadFileService } from '../../Shared/HttpClient/upload-file.service';
import { OrderItem } from '../../Shared/Models/IOrderItem';
import { IProduct, SmsVM } from '../../Shared/Models/IProduct';

@Component({
  selector: 'app-products-by-user',
  templateUrl: './products-by-user.component.html',
  styleUrls: ['./products-by-user.component.css']
})
export class ProductsByUserComponent implements OnInit {
  smsForm = this.fb.group({
    mobile: ['', [Validators.required, Validators.pattern("^[0-9]{4}[0-9]{7}$")]]
  });
  public productList: IProductByUser[] = [];
  public categoryList: IIdValue[] = [];
  public pageNumber = 1;
  public pageSize = 12;
  notFound = false;
  shopDetails;
  public shopUrl: string="";
  public noOfRecords: number=0;
  public imageLoader: boolean = false;
  public skeltonArr: any = "";
  public supplierId: Number;
  public categoryId = null;
  public smsVm: SmsVM = new SmsVM();
  public result: boolean = false;
  public socialLinks: any;
  count = 0;
  public response: ResponseVm = new ResponseVm();
  public coverImg: any;
  productDetails: IProductVM[] = [];
  productDetail: any = "";
  cartItems: OrderItem[] = [];
  public prodId: number = 0;
  public cartProductVariantIdAvailableStock: number = 0;
  public selectedVariantIdAvailableStock: number = 0;
  quantity: number = 1
  variantDetails: any = null;
  public varientPrice: number = 0;
  public actualVarientPrice: number = 0;
  public variantId: number = 0;
  public bulkInventory = new Array();
  selectedFileName: string = "";
  public varientId: number = 0;
  public variantColor = '';
  public variantHex = '';
  public bulkInventorybyVarient = new Array();
  constructor(public _fileService: UploadFileService, public _httpService: CommonService, private route: ActivatedRoute, public Loader: NgxSpinnerService, private fb: FormBuilder, private toastr: ToastrService, private _metaService: metaTagsService, private _modalService: NgbModal, public _cartService: CartService) {
    this.skeltonArr = { catLength: Array<number>(3), productLength: Array<number>(6) }
  }

  ngOnInit(): void {
    debugger;
    this.route.params.subscribe(params => {

      this.shopUrl = params['shopUrl'];
      this.getSupplierShopDetails();
      
    });
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
  filterByCategory() {
    this.getSupplierProductsList(this.categoryId);
  }
  resetFilter(){
    this.categoryId = null
    this.pageNumber = 1 
    this.getSupplierProductsList();
  }
  getSupplierShopDetails() {
    this._httpService.get(this._httpService.apiUrls.Supplier.Registration.GetSupplierShopDetails + `?shopUrl=${this.shopUrl}`).subscribe(details => {
      debugger;
      if (details.status = HttpStatusCode.Ok) {
        this.shopDetails = details.resultData;
        this.supplierId = this.shopDetails.supplierId;
        this.populateSocialLinks();
        //this.coverImg = this._httpService.transform(this.shopDetails?.shopCoverImage);
        console.log(this.coverImg);
        this.getSupplierProductsList();
      }
    })
  }
  getSupplierProductsList(categoryId: Number = 0) {
    this.count++
    this.Loader.show();
    let obj = {
      supplierId:this.supplierId,
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      count:this.count,
      categoryId
    }
    this._httpService.PostData(this._httpService.apiUrls.Supplier.Product.GetSupplierProductListWeb, obj, true).then(response => {
      if (response.status == HttpStatusCode.Ok) {
        this.productList = response.resultData.productList;
        if (this.count== 1) { 
          this.categoryList = response.resultData.categoryList;
        }
        this.imageLoader = true;
        this.noOfRecords = this.productList[0].noOfRecords;
        this.Loader.hide();
      }
      else {
        this.imageLoader = true;
        this.notFound = true;
        this.noOfRecords = 0;
        this.Loader.hide();
      }
    });
  }
  get f() {
    return this.smsForm.controls;
  }
  getPostByPage(page:number) {
    this.pageNumber = page;
    this.getSupplierProductsList();
  }
  Send() {
    var origin = window.location.origin;
    var currentPath = window.location.pathname;
    let formData = this.smsForm.value;
    this.smsVm.MobileNumber = formData.mobile;
    this.smsVm.Message = 'Hi there, this product may interest you. Check it out at : ' + origin + currentPath;
    this.smsVm.MobileNumberList = [];
    this._httpService.PostData(this._httpService.apiUrls.Supplier.Product.SendMessage, this.smsVm, true).then(res => {
      this.result = res;
      if (this.result == true) {
        this.smsForm.reset();
        this.toastr.success("Message has been Sent Successfully.", "Success");
        console.log(this.result);
      }
      else {
        this.toastr.error("Something went Wrong.", "Error");
      }
    });

  }
  openLink(url: string) {
    window.open(url);
  }
  public populateSocialLinks() {
    let supplierId = this.supplierId;
    
    this._httpService.GetData(this._httpService.apiUrls.Supplier.GetSocialLinks + "?supplierId=" + supplierId, true).then(res => {
      this.response = res;
      
      if (this.response.status == StatusCode.OK) {
        this.socialLinks = this.response.resultData;

      }

    }, error => {
      this._httpService.Loader.show();
      console.log(error);
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
          }
            accumulated.push({ id, name, slug, price, discountedPrice, weight, variantName, traxCityId, discount, variantId, hexCode, shopName, shopUrl, attributeName, attributeValue, description, youtubeURL, supplierId, firebaseClientId, availability, categoryId, categoryName, subCategoryName, subCategoryId, categoryGroupId, categoryGroupName, groupItem, attributes, images, isFreeShipping });
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
  getVariantPrice(variantId: number) {
    if (this.cartItems?.length) {
      for (var item = 0; item < this.cartItems.length; item++) {
        if ((this.prodId && variantId) == (this.cartItems[item].product.id && this.cartItems[item].product.variantId)) {
          this.cartProductVariantIdAvailableStock = this.cartItems[item].product.stockQuantity - this.cartItems[item].quantity;
          // console.log(`Cart Product VariantId Quantity in Cart ${this.cartItems[item].quantity}`);
          this.selectedVariantIdAvailableStock = this.cartProductVariantIdAvailableStock;
          // console.log(`Selected VariantId Available Stock ${this.selectedVariantIdAvailableStock}`);
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
      debugger
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
      this.toastr.success("Item added to cart", "Cart");
      this.quantity = 1;
      this._modalService.dismissAll();
    }

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
