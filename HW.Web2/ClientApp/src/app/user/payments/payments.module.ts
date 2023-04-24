import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { PaymentsListComponent } from './list/list.component';
import { PaymentMethodComponent } from './payment-method/payment-method.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TopupComponent } from './topup/topup.component';
import { CustomerwithdrawalComponent } from './customerwithdrawal/customerwithdrawal.component';

const routes: Routes = [
  { path: 'List', component: PaymentsListComponent, },
  { path: 'PaymentMethod', component: PaymentMethodComponent, },
  { path: 'topup', component: TopupComponent, },
  { path: 'customerwithdrawal', component: CustomerwithdrawalComponent, }

]

@NgModule({
  declarations: [
    PaymentsListComponent,
    PaymentMethodComponent,
    TopupComponent,
    CustomerwithdrawalComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ModalModule.forRoot(),
    HttpClientModule,
    FormsModule,
    RouterModule.forChild(routes)
  ]
})
export class PaymentsModule { }
