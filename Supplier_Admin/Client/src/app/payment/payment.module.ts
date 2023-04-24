import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Route } from 'aws-sdk/clients/apigatewayv2';
import { RouterModule, Routes } from '@angular/router';
import { PaymentHistoryComponent } from './payment-history/payment-history.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PaymentHistoryDetailsComponent } from './payment-history-details/payment-history-details.component';
import { WithdrawalComponent } from './withdrawal/withdrawal.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { WithdrawallistComponent } from './withdrawallist/withdrawallist.component';

const routes: Routes = [
  { path: 'app-payment-history', component: PaymentHistoryComponent, },
  { path: 'app-payment-history-details', component: PaymentHistoryDetailsComponent, },
  { path: 'app-withdrawal', component: WithdrawalComponent, },
  { path: 'app-withdrawallist', component: WithdrawallistComponent, }
];

@NgModule({
  declarations: [
PaymentHistoryComponent,
PaymentHistoryDetailsComponent,
WithdrawalComponent,
WithdrawallistComponent
],
  imports: [
    CommonModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class PaymentModule { }
