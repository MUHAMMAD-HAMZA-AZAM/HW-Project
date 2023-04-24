import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { ManagemyadsComponent } from './managemyads/managemyads.component';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { EditadComponent } from './editad/editad.component';
import { PromotadComponent } from './promotad/promotad.component';


const routes: Routes = [
  { path: 'Ads', component: ManagemyadsComponent },
  { path: 'EditAd', component: EditadComponent },
  { path: 'PromoteAd', component: PromotadComponent },
]

@NgModule(
  {
    declarations: [
    ManagemyadsComponent,
    EditadComponent,
    PromotadComponent],
    imports: [
      CommonModule,
      HttpClientModule,
      InfiniteScrollModule,
      FormsModule,
      ReactiveFormsModule,
      RouterModule.forChild(routes)
    ]
  })
export class ManagemyadsModule { }
