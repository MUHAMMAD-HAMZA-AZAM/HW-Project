import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ViewcartComponent } from './viewcart/viewcart.component';
import { RouterModule, Routes } from '@angular/router';
import { CheckoutComponent } from './checkout/checkout.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


const routes: Routes = [
  { path: 'viewcart', component: ViewcartComponent },
  { path: 'checkout', component: CheckoutComponent },
]
@NgModule({
  declarations: [
    ViewcartComponent,
    CheckoutComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NgbModule,
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class CartModule { }
