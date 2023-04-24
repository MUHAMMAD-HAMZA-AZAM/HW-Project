import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as console from 'console';
import {  AspnetRoles, TrackingStatusType } from 'src/app/Shared/Enums/enums';
import { OrderStatus, StatusCode } from '../../Shared/Enums/common';
import { IDetails, IOrderItem, IResponse } from '../../Shared/Enums/Interface';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
})
export class OrderDetailsComponent implements OnInit {
  public trackingNumber: number | undefined = 0;
  public orderId: number=0;
  public supplierId: number=0;
  public ordersItems: IOrderItem[]=[];
  public userId: string="";
  public Orders = OrderStatus;
  public ordersByProducts = new Array();
  public response : IResponse;
  public trackingRecord: IDetails = {} as IDetails;
  constructor(public common: CommonService,
     private route: ActivatedRoute, private _modalService: NgbModal) { }

  ngOnInit(): void {
    var decodedtoken = this.common.decodedToken();
    this.userId = decodedtoken.Id;
    this.route.queryParams.subscribe((param: Params) => {
      this.orderId = param['orderId'];
      this.supplierId = param['supplierId'];
      debugger;
      this.getOrdersDetails();
    });
  }

  //------------------- Get OrderItems Details
  public getOrdersDetails() {

    this.common.GetData(this.common.apiUrls.Supplier.Orders.GetOrderDetailsById + "?orderId=" + this.orderId + "&supplierId=" + this.supplierId, true).then(result => {
      
      if (result.status == StatusCode.OK) {
        
        this.ordersItems = result.resultData;
        
        this.ordersItems.forEach(item => {
          
          if (!this.ordersByProducts.some(x => x.variantId == item.variantId)) {
            let obj = {
              orderId: item.orderId,
              trackingNumber:item.trackingNumber,
              orderFrom: item.orderFrom,
              orderDate: item.orderDate,
              quantity: item.quantity,
              orderStatus: item.orderStatus,
              price: item.price,
              actualAmount: item.actualAmount,
              discountedAmount: item.discountedAmount,
              promotionAmount: item.promotionAmount,
              totalPayable: item.totalPayable,
              shippingCost: item.shippingCost,
              productId: item.productId,
              productTitle: item.productTitle,
              variant: item.variant,
            }

            this.ordersByProducts.push(obj)
          }
        });
        this.ordersItems = this.ordersByProducts;
      }
    });
  }
  //------------------- Show Modal For OrderedItem Tracking
  public showModal(trackId:string,modalName: TemplateRef<any>):void {
    this.trackingNumber = Number(trackId);
    this.getOrderedItemTrackingForSupplier(modalName);

  }

 
   //------------- Show Customer  Order Item Track Details 
   public orderItemTrack(trackId: string) {
    this.trackingNumber = Number(trackId);
    this.common.NavigateToRouteWithQueryString(this.common.apiRoutes.Order.orderTrack, { queryParams: { trackId: this.trackingNumber } });
  }
  //------------------- For Shippers' Tracking

  public getOrderedItemTrackingForSupplier(modalName: TemplateRef<any>){
    let obj = {
      tracking_number: this.trackingNumber,
      type: TrackingStatusType.ShipperRelatedTracking,
      userRole: AspnetRoles.SRole
    };
    this.common.Loader.show();
    this.common.PostData(this.common.apiUrls.Supplier.Orders.getOrderedItemTrackingForSupplier, JSON.stringify(obj)).then(res =>{
     this.response = res;
     if(this.response.status == StatusCode.OK){
       this.trackingRecord = <any>this.response.resultData;
       this.common.Loader.hide();
     }
     this.common.Loader.hide();
    });
  }

}
