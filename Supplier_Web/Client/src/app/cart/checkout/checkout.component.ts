import { DecimalPipe } from '@angular/common';
import { HttpStatusCode } from '@angular/common/http';
import { newArray } from '@angular/compiler/src/util';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { IResponseVM, OrderStatus, ResponseVm, ShippingDetails, StatusCode, TraxServiceType, TraxShippingMode, TraxStatusCode } from '../../Shared/Enums/enum';
import { ICalculateShippingRates, ICityList, ICityListTrax, ICityShippingCost, IPageSeoVM, IPersonalDetails, IPromoCheckOut, IPromotions, IShippingCost } from '../../Shared/Enums/Interface';
import { CartService } from '../../Shared/HttpClient/cart.service';
import { CommonService } from '../../Shared/HttpClient/HttpClient';
import { MessagingService } from '../../Shared/HttpClient/messaging.service';
import { metaTagsService } from '../../Shared/HttpClient/seo-dynamic.service';
import { ShippingService } from '../../Shared/HttpClient/shipping.service';
import { OrderItem } from '../../Shared/Models/IOrderItem';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent implements OnInit {
  cartItems: OrderItem[] = [];
  subTotal: number = 0;
  public appValForm: FormGroup;
  public shipingdetails: any = "";
  public customerId: number = 0;
  public userId: string = "";
  public loggedUserDetails: IPersonalDetails;
  public cityList: ICityListTrax[] = [];
  promotionList: IPromoCheckOut[] = [];
  promotionsTotal = 0;
  public submitted = false;
  isCartEmpty: boolean = false;
  public userRole: string = '';
  //public cityId: number = 64;
  public response: IResponseVM;

  public shippingdata: ShippingDetails;
  public cityShippingCost: ICityShippingCost;
  public shippingCost: number = 0 ;
  public discountAmount: number = 0 ;
  public charges :number = 0;
  public totalShippingCost: number = 0;
  public calculateShippingRates: ICalculateShippingRates;
  public bulkInventorybyVarient = new Array();
  public isFreeShipping: boolean;
  // totalShippingCost: number = 250;
    constructor(private _shippingService: ShippingService, private _toastr: ToastrService, private _cartService: CartService, public formBuilder: FormBuilder, public _httpService: CommonService, private _messageService: MessagingService, private _metaService: metaTagsService, public Loader: NgxSpinnerService,public _modalService :NgbModal) {
    this.appValForm = {} as FormGroup;
    this.loggedUserDetails = {} as IPersonalDetails;
    this.cityShippingCost = {} as ICityShippingCost;
    this.shippingdata = new ShippingDetails();
  }

  ngOnInit(): void {
    this.appValForm = this.formBuilder.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      PhoneNumber: ['', [Validators.required]],
      email: ['', [Validators.required]],
      Address: ['', [Validators.required]],
      City: ['', [Validators.required]],
      orderMessage: ['', [Validators.required]]
    });

    var decodedtoken = this._httpService.decodedToken();
    this.customerId = decodedtoken.Id;
    this.userId = decodedtoken.UserId;
    this.userRole = decodedtoken.Role;
    this.getLoggedUserDetails(this.userRole, this.userId);
    //this.getAllCities();
    //this.bindMetaTags();
    this.getCityListWithTraxCityId();
  }
  cityChangeHandler(e: Event) {
    let traxCityId = (<HTMLSelectElement>e.target).value;
    debugger;
    console.log(traxCityId);
    this.getShippingCharges(traxCityId)

  }

  //------------- Calculate the Shipping Rates for a Destination
  getShippingCharges(cityId) {
    this.totalShippingCost = 0;
    console.log("When City Changes " + this.totalShippingCost);
    this.cartItems.forEach((item, index) => {
      let obj = {
        service_type_id: TraxServiceType.Regular,
        origin_city_id: item.product.traxCityId,
        destination_city_id: Number(cityId),
        estimated_weight: item.product.weight * item.quantity,
        shipping_mode_id: TraxShippingMode.Saverplus,
        amount: item.product.discountedPrice > 0 ? item.product.discountedPrice : item.product.price
      }      

      this._shippingService.PostData('https://sonic.pk/api/charges_calculate', obj).subscribe(res => {
        this.calculateShippingRates = <any>res;        
        if (TraxStatusCode.OK == this.calculateShippingRates.status) {
          this.shippingCost =  (this.calculateShippingRates['information'].charges.gst + this.calculateShippingRates['information'].charges.total_charges);
          console.log(`shippingCost ` + this.shippingCost);
          this.shippingCost = this.shippingCost > 0 ? (1 * this.shippingCost) : ((-1) * this.shippingCost);
          item.product.shippingCost = this.shippingCost;
          this.totalShippingCost += item.product.isFreeShipping ? 0 : this.shippingCost;
          this.shippingCost = 0;
          console.log("Shipping Charges " + this.totalShippingCost);
        }
      });
    });
  }
  getPromotionsOnCategory() {
    this.promotionList = [];
    this.promotionsTotal = 0;
    this.cartItems.forEach(item => {
      this._httpService.GetData1(this._httpService.apiUrls.Promotion.GetPromotionsBySuplierId + `?supplierId=${item.product.supplierId}`).then(res => {
        let response: IPromotions[] = res.resultData;
        if (response) {
          response.forEach(promo => {
            if (item.product.categoryId == promo.categoryId && item.product.subCategoryId == promo.subCategoryId && item.product.categoryGroupId == promo.categoryGroupId) {
              this.promotionList.push({ promotionId: promo.promotionId, amount: promo.amount, productId: item.product.id });
              this.promotionsTotal += promo.amount;
            }
            else if (item.product.categoryId == promo.categoryId && item.product.subCategoryId == promo.subCategoryId && item.product.categoryGroupId == 0) {
              this.promotionList.push({ promotionId: promo.promotionId, amount: promo.amount, productId: item.product.id });
              this.promotionsTotal += promo.amount;
            }
            else if (item.product.categoryId == promo.categoryId && item.product.subCategoryId == 0 && item.product.categoryGroupId == 0) {
              this.promotionList.push({ promotionId: promo.promotionId, amount: promo.amount, productId: item.product.id });
              this.promotionsTotal += promo.amount;
              //this.promotionList.push({ name: promo.promotionName, amount: promo.amount, cat: promo.categoryId, subCat: promo.subCategoryId, groupCat: promo.categoryGroupId });
            }
          })
        }
      })
    })
  //  console.log(this.promotionList);
  }
  getCartItems() {
    debugger;
    this._cartService.getItems().subscribe(res => {
      console.log(res);
      this.isCartEmpty = res.length > 0 ? true : false;
      this.cartItems = res;
      debugger;
      this.getPromotionsOnCategory();
      this.subTotal = this.cartItems.reduce((accumulate, item) => {
        return accumulate + ((item.product?.discountedPrice) > 0 ? (item.product.discountedPrice * item.quantity) : (item.product.price * item.quantity))
      }, 0)
    });
  }
  public getLoggedUserDetails(role: string, userId: string) {
    this._httpService.GetData1(this._httpService.apiUrls.Customer.GetUserDetailsByUserRole + `?userId=${userId}&userRole=${role}`, true).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        this.loggedUserDetails = <any>this.response.resultData;
        this.shippingdata = this.loggedUserDetails;
        this.shippingdata.PhoneNumber = this.loggedUserDetails.mobileNumber;
        this.appValForm.patchValue(this.shippingdata);
        this.getCartItems();
      }
    }, error => {
      this._httpService.Loader.show();
      console.log(error);
    });
  }

  checkOut(modalName:TemplateRef<any>) {
    this.submitted = true;
    if (this.appValForm.valid) {
      this.shipingdetails = this.appValForm.value;
      this.shipingdetails.City = Number(this.shipingdetails.City);
      const customerId = Number(this._httpService.decodedToken()?.Id)
      if (customerId > 0) {
        let items = new Array();
        let supplierIdArr = new Array();
        this.cartItems.forEach((item: OrderItem) => {
          if (!supplierIdArr.some(x => x.supplierId == item.product.supplierId)) {
            supplierIdArr.push({ supplierId: item.product.supplierId, firebaseClientId: item.product.firebaseClientId })
          }
          let [obj] = this.promotionList.filter(x => x.productId == item.product.id);
          this.bulkInventorybyVarient = [];
          this.discountAmount = 0;
          this.bulkInventorybyVarient = item.product.bulkInventory;
          for (let itm of this.bulkInventorybyVarient) {
            debugger;
            console.log(itm.bulkVarientId);
            console.log(item?.product.variantId);
            console.log(itm.bulkProductId);
            console.log(item?.product.id);
            if (itm.minQuantity <= item.quantity && item.quantity <= itm.maxQuantity && itm.bulkVarientId == item?.product.variantId && itm.bulkProductId == item?.product.id) {
              this.discountAmount = item.product.price - item.product.discountedPrice;
              break;
            }
          }
          let cartItem = {
            supplierId: item.product.supplierId,
            variantId: item?.product.variantId,
            productId: item?.product.id,
            quantity: item.quantity,
            price: item.product.price,
            discount: this.discountAmount > 0? this.discountAmount : item?.product.discount,
            promotionId: obj ? obj.promotionId : 0,
            promotionAmount: obj ? obj.amount : 0,
            discountedPrice: item.product.discountedPrice,
            shippingAmount: item.product.shippingCost,
            isFreeShipping: item?.product.isFreeShipping
          }
          items.push(cartItem);
        })
        let obj = {
          isFromWeb: true,
          orderTotal: this.subTotal - this.promotionsTotal,
          customerId: customerId,
          orderStatus: OrderStatus.Inprogress,
          orderMessage: this.shipingdetails.orderMessage,
          items,
          shippingDetails: this.shipingdetails,
          totalShippingCost: this.totalShippingCost,
          supplierIdArr,
        }   
        this._httpService.PostData(this._httpService.apiUrls.Supplier.Product.PlaceOrder, obj).then(res => {
          let response = res;
          if (response.status == HttpStatusCode.Ok) {
            //supplierIdArr.forEach((item) => {
            //  if (item.firebaseClientId) {
            //    console.log(item.firebaseClientId);
            //    this._messageService.sendMessage("Order", "You received an order", item.firebaseClientId,);
            //  }
            //})
            this._cartService.clearCartItems();
            this._toastr.warning("Select Payment Method for Order Place", "Alert")
            this._httpService.NavigateToRoute("/user/payment/" + response.resultData)
          }
        });
      }
      else
        this._toastr.error("Something went wrong", "Error")
    }
  }
  public getAllCities() {
    this._httpService.get(this._httpService.apiUrls.Supplier.City.getCityList).subscribe(result => {
      this.cityList = result;
    })
  }
  public getCityListWithTraxCityId() {
    this._httpService.get(this._httpService.apiUrls.Supplier.City.GetCityListWithTraxCityId).subscribe(result => {
      this.cityList = result.resultData;
    })
  }

  get f() { return this.appValForm.controls; }
  public bindMetaTags() {
    this._httpService.get(this._httpService.apiUrls.CMS.GetSeoPageById + "?pageId=16").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description, res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }
}
