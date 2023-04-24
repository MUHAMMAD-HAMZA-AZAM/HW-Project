import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PromotionListComponent } from './promotion-list/promotion-list.component';
import { RouterModule, Routes } from '@angular/router';
import { PromotionByUserComponent } from './promotion-by-user/promotion-by-user.component';
import { PromotionDetailsComponent } from './promotion-details/promotion-details.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { CampaignListComponent } from './campaign-list/campaign-list.component';
import { SuccessMessageComponent } from './success-message/success-message.component';

const routes: Routes = [
  { path: 'promotionlist', component: PromotionListComponent },
  { path: 'promotionsByUser', component: PromotionByUserComponent },
  { path: 'promotionDetails', component: PromotionDetailsComponent },
  { path: 'campaignlist', component: CampaignListComponent },
  { path: 'thankyou', component: SuccessMessageComponent }
]


@NgModule({
  declarations: [PromotionListComponent, PromotionByUserComponent, PromotionDetailsComponent, CampaignListComponent, SuccessMessageComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    ModalModule.forRoot(),
  ]
})
export class PromotionModule { }
