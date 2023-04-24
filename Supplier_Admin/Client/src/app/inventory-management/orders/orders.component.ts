import { Component, OnInit, TemplateRef } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { StatusCode } from '../../Shared/Enums/common';
import { AspnetRoles, TrackingStatusType } from '../../Shared/Enums/enums';
import { IDetails, IOrders, IPersonalDetails, IResponseVM } from '../../Shared/Enums/Interface';
import { Orders } from '../../Shared/Enums/Inventory/orderEnum';
import { MessagingService } from '../../Shared/HttpClient/messaging.service';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
  public userId: string = "";
  public id:string = "";
  public userRole: string = '';
  public ordersList: IOrders[]=[];
  public ordersListByCustomers = new Array();
  public Orders = Orders;
  public decodeToken: any = "";
  public status: number = 0;
  public orderId: number = 0;
  public firebaseClientId: string = '';
  public customerId: number = 0;
  public getRecord: IDetails[] = [];
  public trackingRecord: IDetails[] = [];
  public loggedUserDetails: IPersonalDetails;
  public response: IResponseVM;
  constructor(public common: CommonService, public toastr: ToastrService, private _messagingService: MessagingService, private _modalService: NgbModal) {
    this.loggedUserDetails = {} as IPersonalDetails;
  }

  ngOnInit(): void {
    this.decodeToken = this.common.decodedToken();
console.log(this.decodeToken);
    this.id = this.decodeToken.UserId;
    this.userId = this.decodeToken.Id;
    this.userRole = this.decodeToken.Role;
    this.getLoggedUserDetails(this.userRole, this.id);
    this.getOrdersList();
  }

  public getLoggedUserDetails(role: string, id: string) {

    this.common.GetData(this.common.apiUrls.Supplier.Profile.GetUserDetailsByUserRole + `?userId=${id}&userRole=${role}`, true).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        this.loggedUserDetails = <any>this.response.resultData;
        console.log(this.loggedUserDetails.mobileNumber);
      }
    }, error => {
      this.common.Loader.show();
      console.log(error);
    });
  }
  public getOrdersList() {
   
    let data = {
      supplierId: this.userId
    };
    this.common.PostData(this.common.apiUrls.Supplier.Orders.GetOrdersList, JSON.stringify(data),true).then(result => {
      if (result.status == StatusCode.OK) {
        this.ordersList = result.resultData;
        this.ordersList.forEach(item => {
          if (!this.ordersListByCustomers.some(x => x.orderId == item.orderId)) {
            let obj = {
              orderId: item.orderId,
              supplierId: item.supplierId,
              orderFrom: item.orderFrom,
              firebaseClientId: item.firebaseClientId,
              mobileNumber: item.mobileNumber,
              orderStatus: item.orderStatus,
              orderDate: item.orderDate,
              quantity: item.quantity,
              shippingAddress: item.shippingAddress,
              shippingCost: item.shippingCost,
              totalPayable : item.totalPayable,
              orderStatusName: item.orderStatusName,
              orderMessage : item.orderMessage,
              customerId: item.customerId
            };
            this.ordersListByCustomers.push(obj);
          }
        });
        this.ordersList = this.ordersListByCustomers;

      }
    });
  }

  public showModal(status: number, orderId: number, firebaseClientId: string, customerId: number, modalName: TemplateRef<any>): void {
    this._modalService.open(modalName);
    this.status = status;
    this.orderId = orderId;
    this.firebaseClientId = firebaseClientId;
    this.customerId = customerId;
  }

  public confirmOrder(): void  {
    this.updateOrder(this.status, this.orderId, this.firebaseClientId, this.customerId);
    this._modalService.dismissAll();
  }
  public updateOrder(status: number, orderId: number, firebaseClientId: string, customerId: number) {
      let newOrderStatus = (status == Orders.Inprogress) ? Orders.Received : Orders.PackedAndShipped;
      let data = {
        orderStatus: newOrderStatus,
        orderId: orderId,
        supplierId: this.userId,
        customerId,
        firebaseClientId,
        isFromWeb: true
      };
      this.common.PostData(this.common.apiUrls.Supplier.Orders.UpdateOrderStatus, JSON.stringify(data),true).then(result => {
        if (result.status == StatusCode.OK) {
          this.ordersListByCustomers = [];
          this.ordersList = [];
          this.getOrdersList();
          //if (firebaseClientId) {
          //  let body = '';
          //  switch (newOrderStatus) {
          //    case 6:
          //      body = 'Your order has been Packed & Shipped'
          //      break;
          //    case 1:
          //      body = 'Your order has been Received'
          //      break;
          //  }
          //  this._messagingService.sendMessage("Order Status", body, firebaseClientId);
          //}
          this.toastr.success("Order status updated", "Success");
        }
      });
        

  }

  ShowDetails(orderId?:number, supplierId?:number) {
    this.common.NavigateToRouteWithQueryString(this.common.apiRoutes.Order.OrderDetails, {
      queryParams: {
        "orderId": orderId,
        "supplierId": supplierId
      }
    });
  }

  public orderTrack(orderId: number){
    this.common.NavigateToRouteWithQueryString(this.common.apiRoutes.Order.orderTrack, { queryParams: { orderId: orderId } });
  }

  //------------- Show Order Track Modal
  public showTrackModal(orderId: number, orderTrackModal: TemplateRef<any>) {
    this.trackingRecord = [];
    this.orderId = orderId;
    this.getOrderTrackingForSupplier(orderTrackModal);
  }

  //-------------- For Order Tracking
  public getOrderTrackingForSupplier(orderTrackModal: TemplateRef<any>) {
    let obj = {
      order_id: this.orderId,
      type: TrackingStatusType.ShipperRelatedTracking,
      userRole: AspnetRoles.SRole,
    };
    this.common.Loader.show();
    this.common.PostData(this.common.apiUrls.Supplier.Orders.GetOrderTrackingForSupplier, JSON.stringify(obj)).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        this.getRecord = <any>this.response.resultData;
        this.trackingRecord = this.getRecord.filter(item => item.pickup.phone_number == this.loggedUserDetails.mobileNumber);
       this._modalService.open(orderTrackModal, { centered: true, size: 'lg', backdrop: 'static', keyboard: false });
        console.log(this.trackingRecord);
      }
      this.common.Loader.hide();
    });
  }

  //------------- Close Confirmation Modal
  public closeModal(): void {
    this._modalService.dismissAll();

  }

  
}
