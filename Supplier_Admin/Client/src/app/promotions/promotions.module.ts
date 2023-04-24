import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PromotionTypesComponent } from './promotion-types/promotion-types.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PromotionsComponent } from './promotions/promotions.component';
const routes: Routes = [
  { path: 'PromotionTypes', component: PromotionTypesComponent },
  { path: 'Promotion', component: PromotionsComponent }
];

@NgModule({
  declarations: [
    PromotionTypesComponent,
    PromotionsComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ModalModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
  ]
})
export class PromotionsModule { }
