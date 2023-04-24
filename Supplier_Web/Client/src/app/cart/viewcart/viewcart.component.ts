import { Template } from '@angular/compiler/src/render3/r3_ast';
import { Component, ElementRef, OnInit, QueryList, TemplateRef, ViewChild, ViewChildren } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { IResponseVM, StatusCode } from '../../Shared/Enums/enum';
import { ICityShippingCost, IPageSeoVM } from '../../Shared/Enums/Interface';
import { CartService } from '../../Shared/HttpClient/cart.service';
import { CommonService } from '../../Shared/HttpClient/HttpClient';
import { metaTagsService } from '../../Shared/HttpClient/seo-dynamic.service';
import { OrderItem } from '../../Shared/Models/IOrderItem';

@Component({
  selector: 'app-viewcart',
  templateUrl: './viewcart.component.html',
  styleUrls: ['./viewcart.component.css']
})
export class ViewcartComponent implements OnInit {
  public obj: object={} as object;
  cartItems: OrderItem[] = [];
  quantity = 1;
  public orderItem: OrderItem;
  public shippingCost: number = 0;
  public cityId: number = 64;
  public response: IResponseVM;
  public cityShippingCost: ICityShippingCost;
  public total: number = 0;
  public bulkInventory = new Array();
  mymodel: number = 1;
  //@ViewChild("qtyInput", { static: false }) qtyInput: ElementRef;
  @ViewChildren("qtyInput", { read: ElementRef }) qtyInputList: QueryList<ElementRef>;
  constructor(private _cartService: CartService,
    private _metaService: metaTagsService,
    public _modalService: NgbModal,
    public _httpService: CommonService, private toastr: ToastrService  ) {
this.qtyInputList={} as  QueryList<ElementRef>;
this.orderItem={} as OrderItem;}

  ngOnInit(): void {
    this.bindMetaTags();
    this.calculateCartTotal();
  }
  calculateCartTotal() {
    this._cartService.getItems().subscribe(res => {
      this.cartItems = res;
      console.log(this.cartItems);
      if (this.cartItems.length == 0) {
        this._httpService.NavigateToRoute(this._httpService.apiRoutes.Home.LandingPage);
        return;
}
      this.total = this.cartItems.reduce((accumulate, item) => {
        return accumulate + (item.product.discountedPrice ? item.product.discountedPrice * item.quantity : item.product.price * item.quantity) + this.shippingCost;
      }, 0)
    });


  }
  //------------------------ Get City Shipping Cost for Order  
  public getCityShippingCostForOrder() {
    this._httpService.GetData1(this._httpService.apiUrls.Customer.Order.GetCityShippingCost + `?id=${this.cityId}`).then(res => {
      this.response = <any>res;
      if (this.response.status == StatusCode.OK) {
        this.cityShippingCost = this.response.resultData;
        this.shippingCost = this.cityShippingCost[0].cost;
        this.calculateCartTotal();
      }

    });
  }

  handleQuantityChange(op: string, item: OrderItem, index:number) {
    
    this.bulkInventory = item.product.bulkInventory;
    let qty = this.qtyInputList.toArray()[index].nativeElement.value;
     
    if (op == "+") {
      if (qty >= item.product.stockQuantity)
      {
        this.toastr.warning("You can only buy available stock quantity ", "Sorry", { timeOut: 5000 });
        this.quantity = item.product.stockQuantity;
        return;
      }
      else
      {
        qty++;
        for (let bulk of this.bulkInventory)
        {
          if (bulk.minQuantity <= qty && qty <= bulk.maxQuantity)
          {
            let discounted = (bulk.bulkPrice / 100) * bulk.bulkDiscount;
            discounted = bulk.bulkPrice - discounted;
            item.product.discountedPrice = discounted;
            break;
          }
        }
        this._cartService.changeItemQuantity(item, qty);
      }
    } 
    else {
      if (qty < 2)
        return;
      else
      {
        qty--;
        if (this.bulkInventory.length > 0)
        {
          for (let bulk of this.bulkInventory)
          {
            if (bulk.minQuantity <= qty && qty <= bulk.maxQuantity)
            {
              let discounted = (bulk.bulkPrice / 100) * bulk.bulkDiscount;
              item.product.discountedPrice = bulk.bulkPrice - discounted;
              break;
            }
            else
            {
              if (bulk.maxQuantity > qty)
              {
                item.product.discountedPrice = item.product.actualVarientPrice;
                break;
              }
              
            }
          }
        }
        this._cartService.changeItemQuantity(item, qty);
      }
    }
  }
  /*  ....................show Model For Remove CartItem...........................*/
  removeCartItem(item: OrderItem, removecartitem: TemplateRef<any>) {
    this._modalService.open(removecartitem, { backdrop: 'static', keyboard: false });
    this.orderItem = item;

  }
/* .............. Confirm to remove cart Item..............*/
  public deleteCartItemconfirm() {
    this._cartService.removeItem(this.orderItem);
    this._modalService.dismissAll();

  }
  public bindMetaTags() {
    this._httpService.get(this._httpService.apiUrls.CMS.GetSeoPageById + "?pageId=17").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description, res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }
  onQuantityChange(item: OrderItem, index: number) {
    let varientQuantity: number = 0;
    let qty = this.qtyInputList.toArray()[index].nativeElement.value;
    this.bulkInventory = item.product.bulkInventory;
    let availableStock: number = item.product.stockQuantity;
    debugger;
    console.log(qty);
    console.log(availableStock);

    if (qty > availableStock) {
      debugger;
      this.qtyInputList.toArray()[index].nativeElement.value = availableStock;
      qty = availableStock;
    }
    else if (qty == 0) {
      this.qtyInputList.toArray()[index].nativeElement.value = 1;
      return;
    }
   
    if (this.bulkInventory.length > 0) {
      for (let bulk of this.bulkInventory) {
        if (bulk.minQuantity <= qty && qty <= bulk.maxQuantity) {
          let discounted = (bulk.bulkPrice / 100) * bulk.bulkDiscount;
          item.product.discountedPrice = bulk.bulkPrice - discounted;
          break;
        }
        else {
          if (bulk.maxQuantity > qty) {
            item.product.discountedPrice = item.product.actualVarientPrice;
            break;
          }

        }
      }
    }
    this._cartService.changeItemQuantity(item, qty);
    
  }

}
