import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrdersComponent } from './orders/orders.component';
import { RouterModule, Routes } from '@angular/router';
import { OrderDetailsComponent } from './order-details/order-details.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ProductsInventoryComponent } from './products-inventory/products-inventory.component';
import { SupplierCancelledOrdersComponent } from './supplier-cancelled-orders/supplier-cancelled-orders.component';
import { SalesSummaryComponent } from './sales-summary/sales-summary.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { OrderTrackingComponent } from './order-tracking/order-tracking.component';

const routes: Routes = [
  { path: 'orderlist', component: OrdersComponent },
  { path: 'orderDetails', component: OrderDetailsComponent },
  { path: 'products-inventory', component: ProductsInventoryComponent },
  { path: 'cancelled-orders', component: SupplierCancelledOrdersComponent },
  { path: 'app-sales-summary', component: SalesSummaryComponent },
  { path: 'orderTrack', component: OrderTrackingComponent }
];

@NgModule({
  declarations: [
    OrdersComponent,
    OrderDetailsComponent,
    ProductsInventoryComponent,
    SupplierCancelledOrdersComponent,
    SalesSummaryComponent,
    OrderTrackingComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    NgbModule,
ReactiveFormsModule
  ]
})
export class InventoryManagementModule { }
