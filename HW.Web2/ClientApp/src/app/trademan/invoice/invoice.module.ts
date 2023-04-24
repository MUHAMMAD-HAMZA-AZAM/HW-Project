import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { InvoiceListComponent } from './list/list.component';
import { TrsdesmanwithdrawalComponent } from './trsdesmanwithdrawal/trsdesmanwithdrawal.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TopupComponent } from './topup/topup.component';

const routes: Routes = [
  { path: 'List', component: InvoiceListComponent, },
  { path: 'trsdesmanwithdrawal', component: TrsdesmanwithdrawalComponent, },
  { path: 'trsdesmantopup', component: TopupComponent, },

]

@NgModule({
  declarations: [
    InvoiceListComponent,
    TrsdesmanwithdrawalComponent,
    TopupComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    ModalModule.forRoot(),
    ReactiveFormsModule,
    RouterModule.forChild(routes)
  ]

})
export class InvoiceModule { }
