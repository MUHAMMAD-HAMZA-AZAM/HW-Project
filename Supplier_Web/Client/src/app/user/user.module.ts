import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { OrdersComponent } from './orders/orders.component';
import { WishlistProductsComponent } from './wishlist-products/wishlist-products.component';
import { OrderDetailsComponent } from './order-details/order-details.component';
import { OrderPaymentComponent } from './order-payment/order-payment.component';
import { OrderPaymentReceiptComponent } from './order-payment-receipt/order-payment-receipt.component';
import { CustomerCancelledOrdersComponent } from './customer-cancelled-orders/customer-cancelled-orders.component';
import { OrderTrackingComponent } from './order-tracking/order-tracking.component';


const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent },
  { path: 'orders', component: OrdersComponent },
  { path: 'paymentreceipt', component: OrderPaymentReceiptComponent },
  { path: 'order-details', component: OrderDetailsComponent },
  { path: 'wishlist', component: WishlistProductsComponent },
 // { path: 'payment', component: OrderPaymentComponent },
  { path: 'payment/:id', component: OrderPaymentComponent },
  { path: 'trackorder', component: OrderTrackingComponent},
  {path:'cancelled-orders',component:CustomerCancelledOrdersComponent}

];

@NgModule({
  declarations: [
    DashboardComponent,
    OrdersComponent,
    WishlistProductsComponent,
    OrderDetailsComponent,
    OrderPaymentComponent,
    OrderPaymentReceiptComponent,
    CustomerCancelledOrdersComponent,
    OrderTrackingComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    NgbModule,
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class UserModule { }
