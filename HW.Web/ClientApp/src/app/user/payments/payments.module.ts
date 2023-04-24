import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaymentsInformationComponent } from './paymentsInformation/paymentsInformation.component';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

const routes: Routes = [
  { path: 'PaymentsInformation', component: PaymentsInformationComponent, }
]

@NgModule({
  declarations: [
    PaymentsInformationComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forChild(routes)
  ]
})
export class PaymentsModule { }
