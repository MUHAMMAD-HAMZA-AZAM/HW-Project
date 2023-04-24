import { Component, OnInit, TemplateRef } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { StatusCode } from '../../Shared/Enums/common';
import { ICustomerOrderItems, IOrders } from '../../Shared/Enums/Interface';
import { Orders } from '../../Shared/Enums/Inventory/orderEnum';
import { MessagingService } from '../../Shared/HttpClient/messaging.service';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-supplier-cancelled-orders',
  templateUrl: './supplier-cancelled-orders.component.html',
  styleUrls: ['./supplier-cancelled-orders.component.css']
})
export class SupplierCancelledOrdersComponent implements OnInit {
  public userId: string = "";
  public decodeToken: any = "";
  public dataNotfond: boolean = false;
  public firebaseClientId: string = '';
  public customerId: number = 0;
  public supplierCancelledOrdersList: ICustomerOrderItems[]=[]
  constructor(public common: CommonService, public toastr: ToastrService, private _messagingService: MessagingService, private _modalService: NgbModal, public Loader: NgxSpinnerService) { }

  ngOnInit(): void {
    debugger;
    this.decodeToken = this.common.decodedToken();
    this.userId = this.decodeToken.Id;
    this.getLoggedSupplierCancelledOrdersList()
  }


public   getLoggedSupplierCancelledOrdersList() {
    let obj = {
      supplierId:this.userId 
  }
  this.Loader.show();
    this.common.PostData(this.common.apiUrls.Supplier.Orders.GetLoggedSupplierCanelledOrdersList,JSON.stringify(obj), true)?.then(res => {
      let response = res;
      if (response.status == StatusCode.OK) {
        debugger;
        if (response.resultData) {
          this.supplierCancelledOrdersList = response.resultData;
          this.dataNotfond = false;

        }
        else {
          this.dataNotfond = true;
        }
        this.Loader.hide();
              }   
      });

    }
  


  ShowDetails(orderId?: number, supplierId?: number) {
    this.common.NavigateToRouteWithQueryString(this.common.apiRoutes.Order.OrderDetails, {
      queryParams: {
        "orderId": orderId,
        "supplierId": supplierId
      }
    });
  }


}
