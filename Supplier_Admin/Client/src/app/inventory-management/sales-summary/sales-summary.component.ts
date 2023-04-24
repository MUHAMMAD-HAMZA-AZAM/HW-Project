import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { StatusCode } from '../../Shared/Enums/common';
import { IOrderItem, IOrders } from '../../Shared/Enums/Interface';
import { Orders } from '../../Shared/Enums/Inventory/orderEnum';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-sales-summary',
  templateUrl: './sales-summary.component.html',
  styleUrls: ['./sales-summary.component.css']
})
export class SalesSummaryComponent implements OnInit {
  public userId: string = "";
  public ordersList: IOrders[] = [];
  public salesSummaryList: IOrders[] = [];
  public ordersListByCustomers = new Array();
  public Orders = Orders;
  public decodeToken: any = "";
  public status: number = 0;
  public ordersByProducts = new Array();
  public orderId: number = 0;
  public dataNotfond: boolean = false;
  public firebaseClientId: string = '';
  public customerId: number = 0;
  public appValForm: FormGroup;
  public ordersItems: IOrderItem[] = [];
  public totalCommission: number = 0;
  public totalOrderAmount: number = 0;
  public totalPayable: number = 0;
  public totalPromotion: number = 0;
  constructor(public common: CommonService, private _modalService: NgbModal, public fb: FormBuilder) { }

  ngOnInit(): void {
    this.decodeToken = this.common.decodedToken();
    this.userId = this.decodeToken.Id;
    this.appValForm = this.fb.group({
      fromDate: [''],
      toDate: [''],
    });
    this.getSalesSummary();
  }

  public getSalesSummary() {


    let data = {
      supplierId: this.userId,
      toDate: this.appValForm.value.toDate,
      fromDate:this.appValForm.value.fromDate
    };

    this.totalCommission = 0;
    this.totalOrderAmount = 0;
    this.totalPayable = 0;
    this.common.PostData(this.common.apiUrls.Supplier.Orders.GetSalesSummary, JSON.stringify(data)).then(result => {
      if (result.status == StatusCode.OK) {
        this.ordersList = result.resultData;
        this.ordersList.forEach(item => {
          this.totalCommission += item.commission;
          this.totalOrderAmount += item.actualAmount;
          this.totalPayable += item.totalPayable;
          this.totalPromotion += item.promotionAmount;
         //item.totalPayable = item.discount > 0 ? item.totalPayable - item.discount : item.totalPayable
        });

        if(this.ordersList!=null)
        {
        this.dataNotfond = false;
        }
        else{
        this.dataNotfond = true;
        }
      }
    });
  }

  resetSearchForm() {
      this.appValForm = this.fb.group({
            fromDate: [''],
            toDate: [''],
          });
      this.getSalesSummary();
  }

  ShowDetails(orderId?: number, supplierId?: number, variantModalTemplate?: TemplateRef<any>) {
    this.common.GetData(this.common.apiUrls.Supplier.Orders.GetOrderDetailsById + "?orderId=" + orderId + "&supplierId=" + supplierId, true).then(result => {
      this.ordersItems = [];
      this.ordersByProducts = new Array();
      if (result.status == StatusCode.OK) {
        this.ordersItems = result.resultData;
        this.ordersItems.forEach(item => {

          if (!this.ordersByProducts.some(x => x.variantId == item.variantId)) {
            let obj = {
              orderId: item.orderId,
              orderFrom: item.orderFrom,
              orderDate: item.orderDate,
              quantity: item.quantity,
              orderStatus: item.orderStatus,
              price: item.price,
              actualAmount: item.actualAmount,
              discountedAmount: item.discountedAmount > 0 ? item.actualAmount - item.discountedAmount : item.actualAmount,
              promotionAmount: item.promotionAmount,
              totalPayable: item.commission > 0 ? item.totalPayable - item.commission : item.totalPayable,
              shippingCost: item.shippingCost,
              productId: item.productId,
              productTitle: item.productTitle,
              variant: item.variant,
              commission: item.commission,
              totalShippingAmount: item.totalShippingAmount
            }

            this.ordersByProducts.push(obj)
          }
        });
        this.ordersItems = this.ordersByProducts;
        this._modalService.open(variantModalTemplate, { size: 'lg', centered: true });
      }
    });
  }

}
