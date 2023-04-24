import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JazzCashInitComponent } from './jazz-cash-init/jazz-cash-init.component';
import { SuccessMessageComponent } from './success-message/success-message.component';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

const routes: Routes = [
  { path: 'JazzCashPayment', component: JazzCashInitComponent, },
  { path: 'SuccessMessage', component: SuccessMessageComponent, },
]

@NgModule({
  declarations: [
    JazzCashInitComponent,
    SuccessMessageComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forChild(routes)
  ]
})
export class MessageModule { }
