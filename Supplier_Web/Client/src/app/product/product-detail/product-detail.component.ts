
import { Component, OnInit, TemplateRef, ViewEncapsulation} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ResponseVm, StatusCode, wishListBasket } from '../../Shared/Enums/enum';
import { CommonService } from '../../Shared/HttpClient/HttpClient';
import { UploadFileService } from '../../Shared/HttpClient/upload-file.service';
import { CartService } from '../../Shared/HttpClient/cart.service';
import { ToastrService } from 'ngx-toastr';
import { from } from 'rxjs';
import { delay, toArray } from 'rxjs/operators';
import { bulkModel, IProduct, SmsVM } from '../../Shared/Models/IProduct';
import { OrderItem } from '../../Shared/Models/IOrderItem';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ICustomerFeedBack, ICustomerFeedBackParams, IProductDetail, IProductVM,ISupplierProduct } from '../../Shared/Enums/Interface';
import { metaTagsService } from '../../Shared/HttpClient/seo-dynamic.service';
import { FormBuilder, Validators } from '@angular/forms';
import { DomSanitizer } from "@angular/platform-browser";
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css'],
  encapsulation:ViewEncapsulation.None,
})
export class ProductDetailComponent implements OnInit {
  smsForm = this.fb.group({
    mobile: ['', [Validators.required, Validators.pattern("^[0-9]{4}[0-9]{7}$")]]
  });

  start:number = 0;
  end: number = 1;
  quantity: number = 1;
  favouriteProductId: number=0;
  isProductAddedInWishList: boolean = false;
  userId: string="";
  customerId: number = 0;
  public tags = new Array();
  productId: string="";
  productDetails: IProductVM[]=[];
  productDetail: any="";
  public favouriteProductDetail: any;
  variantDetails: any = null;
  //productSpecification = [];
  public wishListItem: any = wishListBasket;
  public response: ResponseVm = new ResponseVm();
  selectedFileName: string="";
  public imageLoader: boolean = false;
  public isSpecification: boolean = false;
  public isPrdocutfeedBack: boolean = false;
  public skeltonArr: any="";
  public bulkInventory = new Array();
  public bulkInventorybyVarient = new Array();
  public decodedToken: any="";
  public loginCheck: boolean = false;
  public currentPageURL: string="";
  public selectedVariantIdAvailableStock: number=0;
  public cartProductVariantIdAvailableStock: number=0;
  public varientPrice:number=0;
  public actualVarientPrice:number=0;
  public variantId:number=0;
  public variantIdParam:any="";
  public fiveStars: number=0;
  public foureStars: number=0;
  public threeStars: number=0;
  public twoStars: number=0;
  public oneStars: number=0;
  public prodId: number=0;
  public varientId: number=0;
  public productDemo: bulkModel;
  public result: boolean = false;
  public smsVm: SmsVM = new SmsVM();
  public youtubeurl: string = '';
  public transcount: number = 0;
  cartItems: OrderItem[] = [];
  productFeedback: ICustomerFeedBack[] = [];
  pageSize:number = 3;
  pageNumber:number = 1;
  productList: ISupplierProduct[] = [];
  relatedProductList: ISupplierProduct[] = [];
  public socialLinks: any;
  public pageNumberFeedback: number = 1;
  public sizeFeedback: number = 5;
  public noOfRecordsFeedback: number = 0;
  public variantColor = '';
  public variantHex = '';
  constructor(
    private activatedRoute: ActivatedRoute,
    public _httpService: CommonService,
    private router: ActivatedRoute,
    public route: Router,
    private toastr: ToastrService,
    public _fileService: UploadFileService,
    public _cartService: CartService,
    private _metaService: metaTagsService,
    private _modalService: NgbModal, private fb: FormBuilder, private sanitizer: DomSanitizer, public datepipe?: DatePipe)
    {
        this.skeltonArr = { productLength: Array<number>(4) }
        activatedRoute.queryParams.subscribe(data =>{
          this.variantIdParam = data.va;
          this.productId = data.prdt.split('-').pop();
          this.prodId = parseInt(this.productId);
          this.getProductDetailWeb(this.productId);
          this.getCustomerFeedBack();
        });
    this.productDemo = {} as bulkModel;
    }
  ngOnInit(): void
  {
   // this.router.params.subscribe(x => {
   //   
   //   this.variantIdParam = x.varientId;
   //   this.productId = x.id.split('-').pop();
   //   this.prodId = parseInt(this.productId);
   //   this.getProductDetailWeb(this.productId);
   // });   
    this.decodedToken = this._httpService.decodedToken();
    this.customerId = this.decodedToken?.Id;
    this.userId = this.decodedToken?.UserId;
    this.getProductSatusInWishList();
    this._cartService.getItems().subscribe(res =>
    {
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
      else {
      //  console.log("No Items in Cart ");
      
        this.getProductDetailWeb(this.productId);
        this.getCustomerFeedBack();
      }

    });
  }

  getCustomerFeedBack() {
    this.productFeedback = [];
    this.decodedToken = this._httpService.decodedToken();
    this.customerId = this.decodedToken?.Id;
    let obj: ICustomerFeedBackParams = {
      productId: Number(this.productId),
      customerId: this.customerId,
      pageSize: this.sizeFeedback,
      pagesNumber: this.pageNumberFeedback
    };
    this._httpService.post(this._httpService.apiUrls.Supplier.Product.GetCustomerFeedBackList, JSON.stringify(obj)).subscribe(data => {

      let response = data;
      if (response) {
        this.productFeedback = data;
        this.noOfRecordsFeedback = this.productFeedback[0]?.totalReviews;
        console.log(this.productFeedback);
      }
     
    })
  }

  get f() {
    return this.smsForm.controls;
  }
  handleQuantityChange(op:string) {
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
        
        for (let items of this.bulkInventory)
        {
          if (items.minQuantity <= this.quantity && this.quantity <= items.maxQuantity && items.bulkVarientId == this.variantId)
          {
            this.getBulkVarientPrice(items);
            break;
          }
          else
          {
            
            if (items.maxQuantity > this.quantity && items.minQuantity > this.quantity && items.bulkVarientId == this.variantId)
            {
              
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
    if (this.quantity > 0) {
      if (this.quantity > this.selectedVariantIdAvailableStock) {
        this.toastr.warning("Product is out of stock ", "Sorry");
        this.quantity = this.selectedVariantIdAvailableStock;
        return;
      }
      else {


        console.log('this.variantDetails.discount');
        console.log(this.variantDetails.discount);


        console.log('this.productDetail.discount');
        console.log(this.productDetail.discount);


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
        bulkInventory: this.bulkInventorybyVarient,
        fileName:this.productDetails[0].fileName,
        filePath: this.productDetails[0].filePath,
        varientName: this.variantDetails.varientName,
        isFreeShipping: this.productDetail.isFreeShipping
      }
      let orderItem: OrderItem =
      {
        product, quantity: this.quantity,
      };


        console.log('orderItem');
        console.log(orderItem);

        this._cartService.addItem(orderItem, this.bulkInventorybyVarient);
        this.toastr.success("Item added to cart", "Cart");
        
        this.quantity = 1;
        this.getVariantPrice(product.variantId);
      }
    }
    else {
      this.toastr.warning("Quantity must be atleast One ", "Sorry");
      this.quantity = 1;
    }

  }
  getProductDetailWeb(productId: string) {
    this.selectedFileName = "";
    this.youtubeurl = '';
    this.transcount = 0;
    this.productFeedback = [];
    this._httpService.GetData1(this._httpService.apiUrls.Supplier.Product.GetProductDetailWeb + `?productId=${productId}`).then(response => {
    this.productDetails = (<any>response).resultData;
   
      console.log(this.productDetails);
        let productDetail = this.productDetails.reduce((accumulated: IProductDetail[], { id, name, slug, price, discountedPrice, weight, traxCityId, discount, attributeName, attributeValue, shopUrl, shopName, description, youtubeURL, supplierId, firebaseClientId, availability, categoryName, categoryId, subCategoryName, subCategoryId, categoryGroupId, categoryGroupName, variantId, hexCode, isFreeShipping }) => {
        if (!accumulated.some(o => o.id == id)) {
          let groupItem = new Array();
          let attributes = new Array();
          let images = new Array();
          let fileIds = new Array();
          let tags = [];
          for (var i = 0; i < this.productDetails.length; i++) {
            if (!tags.some(x => x.productTag == this.productDetails[i].tagName) && this.productDetails[i].tagName) {
              tags.push({ productTag: this.productDetails[i].tagName })
            }
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
            accumulated.push({ id, name, slug, price, discountedPrice, weight, traxCityId, discount, variantId, hexCode, shopName, shopUrl, attributeName, attributeValue, description, youtubeURL, supplierId, firebaseClientId, availability, categoryId, categoryName, subCategoryName, subCategoryId, categoryGroupId, categoryGroupName, groupItem, attributes, images, tags, isFreeShipping });
        }
        this.imageLoader = true;
        return accumulated;
      }, []);
      this.productDetail = productDetail[0];
        if (this.productDetail != null) {
            this.populateSocialLinks();
          this.populateRelatedProducts();
        this.bindYoutubeUrl(this.productDetail.youtubeURL);
      }
      this._metaService.updateTags(this.productDetail.name + ": mall.hoomwork", this.productDetail.name + this.productDetail.slug, this.productDetail.description, this.productDetail.youtubeURL, this.productDetail.name + this.productDetail.slug, this.productDetail.slug + ": mall.hoomwork", this.productDetail.description)
      let productattributes = this.productDetail.attributes[0];
      if (productattributes.attributeId==null) {
        this.isSpecification = true;
      }
      if(this.variantIdParam != "" ){
        this.getVariantPrice(this.variantIdParam);
        //this.variantId = this.variantIdParam;
      }
      else{
        this.getVariantPrice(this.productDetail.groupItem[0].variantId);
      }
    });
    this.getCustomerFeedBack();
  }
  checkOut() {
    this._modalService.dismissAll();
    this._httpService.NavigateToRoute(this._httpService.apiRoutes.Cart.viewcart);
  }
  continueShopping() {
    this._modalService.dismissAll();
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
          if (this.variantIdParam != "" && this.variantId != null && this.varientId != variantId) {
            this.variantId = this.variantIdParam;
          }

         
          this.productDetails.filter(x => x.variantId == this.cartItems[item].product.variantId).map(x => {
            this.variantDetails = {
              variantId: x.variantId,
              hexCode: x.hexCode,
              price: x.price,
              discount:x.discount,
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
          if (this.variantIdParam != "" && this.variantId != null && this.varientId != variantId) {
            this.variantId = this.variantIdParam;
          }


         
          this.productDetails.filter(x => x.variantId == variantId).map(x => {
            this.variantDetails = {
              variantId: x.variantId,
              hexCode: x.hexCode,
              price: x.price,
              discount:x.discount,
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

      if ( this.varientId != variantId) {
        this.quantity = 1;
      }
     
      if (this.variantIdParam != "" && this.variantId != null && this.varientId != variantId){
        this.variantId = this.variantIdParam;
      }
      console.log("this.productDetails");
      console.log(this.productDetails);
      
      this.productDetails.filter(x => x.variantId == variantId).map(x => {
        this.variantDetails = {
          variantId: x.variantId,
          hexCode: x.hexCode,
          price: x.price,
          discount:x.discount,
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
  selectFile(filePath:string,fileName: string) {
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
      index = (this.productDetail?.images.length)-1;
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
      index =(( index == (this.productDetail?.images.length -1)) ?0: ++index);
      this.selectedFileName = this._httpService.AWSPATH + this.productDetail?.images[index].filePath + this.productDetail?.images[index].fileName;
    }
    else {
      index++;
      this.selectedFileName = this._httpService.AWSPATH + this.productDetail?.images[index].filePath + this.productDetail?.images[index].fileName;
    }
  }
  //----------------- Add Product in CustomerWishList
  public saveAndRemoveProductsInWishList(isFavourite: boolean, modalName:TemplateRef<any>) {
    if (this.decodedToken) {
      if (isFavourite == true) {
        // Add Product From WishList
        let obj = {
          id: 0,
          customerId: this.customerId,
          productId: this.productId,
          supplierId: 0,
          isFavorite: true,
          active: true,
          createdBy: this.userId,
        };
        this._httpService.PostData(this._httpService.apiUrls.Customer.SaveAndRemoveProductsInWishlist, JSON.stringify(obj), true).then(res => {
          this.response = res;
          if (this.response.status == StatusCode.OK) {
            this.isProductAddedInWishList = true;
            this.toastr.info("Item added in wishList ", "Success");
            this.getProductSatusInWishList();
          }

        }, error => {
          this._httpService.Loader.show();
        });

      }

      else {
        // Remove Product From WishList
        let obj = {
          id: this.favouriteProductId,
          customerId: this.customerId,
          productId: this.productId,
        };
        this._httpService.PostData(this._httpService.apiUrls.Customer.SaveAndRemoveProductsInWishlist, JSON.stringify(obj), true).then(res => {

          this.response = res;
          if (this.response.status == StatusCode.OK) {
            this.isProductAddedInWishList = false;
            this.toastr.error("Item removed from your wishList ", "Removed");
          }

        }, error => {
          this._httpService.Loader.show();
        });

      }
    }
    else {
      this.currentPageURL = window.location.pathname;
      //console.log(this.currentPageURL);
      this._modalService.open(modalName, { centered: true });
    }
  }
  //----------------- Go for Account Login
  public loginAccount() {
    this._modalService.dismissAll();
    this._httpService.NavigateToRouteWithQueryString(this._httpService.apiRoutes.Login.login, { queryParams: { returnUrl: this.currentPageURL } });
  }
  //----------------- Get Product Status In Wishlist
  public getProductSatusInWishList() {
    this._httpService.GetData(this._httpService.apiUrls.Customer.CheckProductStatusInWishList + "?customerId=" + this.customerId + "&productId=" + this.productId, false).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        this.favouriteProductDetail = res.resultData;
        if (this.favouriteProductDetail) {
          if (this.favouriteProductDetail.isFavorite == true) {
            this.isProductAddedInWishList = true;
            this.favouriteProductId = this.favouriteProductDetail.id;
          }
          else {
            this.isProductAddedInWishList = false;
          }
        }
        else {
        }
      }
    }, error => {
      console.log(error);
    });
  }
  Send() {
    var origin = window.location.origin;
    let formData = this.smsForm.value;
    let message: string;
    message = 'Hi there, this product may interest you. Check it out at : ' + origin + this._httpService.apiRoutes.Product.ProductDetail + '?va=' + '' + '&prdt=' + this.productDetails[0].slug + '-' + this.productDetails[0].id;
    this.smsVm.MobileNumber = formData.mobile;
    this.smsVm.Message = message;
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
  bindYoutubeUrl(url: string) {
    if (url) {
      const videoId = this.getUrlId(url);
      return this.youtubeurl = '//www.youtube.com/embed/' + videoId;
    }
    else {
      return null;
    }
  }
  getUrlId(url: string) {
    const regExp = /^.*(youtu.be\/|v\/|u\/\w\/|embed\/|watch\?v=|&v=)([^#&?]*).*/;
    const match = url.match(regExp);

    return (match && match[2].length === 11)
      ? match[2]
      : null;
  }
  transform() {
    if (this.transcount == 0 && this.youtubeurl != '') {
      this.transcount++;
      return this.sanitizer.bypassSecurityTrustResourceUrl(
        this.youtubeurl
      );
    }
    else {
      return '';
    }
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
  public populateRelatedProducts() {
  debugger;
    let obj = {
        pageNumber: this.pageNumber,
        pageSize: this.pageSize,
        categoryId: this.productDetail.categoryId,
        categorylevel:"one",
    }
    this.relatedProductList = [];
      this._httpService.PostData(this._httpService.apiUrls.Supplier.Product.GetProductsByCategory, obj, false).then(res => {
        this.productList = res.resultData;
        this.relatedProductList = [];
        for (var item of this.productList) {
          if (item.productId != this.productDetail.id && this.relatedProductList.length < 2) {
              this.relatedProductList.push(item);
          }
        }
      });
  }
  openLink(url: string) {
    window.open(url);
  }
  getfeedbackList(page: number) {
    this.pageNumberFeedback = page;
    this.getCustomerFeedBack();
  }
  navigateByTagName(tagName: string) {
    this._httpService.NavigateToRouteWithQueryString(this._httpService.apiRoutes.Product.SearchByTag, { queryParams: { productTag: tagName } })
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
