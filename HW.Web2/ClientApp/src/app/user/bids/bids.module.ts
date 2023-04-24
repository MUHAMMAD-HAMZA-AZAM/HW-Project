import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { BidListComponent } from './list/list.component';
import { BidDetailComponent } from './detail/detail.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ReceivedBidsComponent } from './receivedBids/receivedBids.component';

const routes: Routes = [
  { path: 'Detail', component: BidDetailComponent, },
  { path: 'List', component: BidListComponent, }
]

@NgModule({
  declarations: [
    BidListComponent,
    BidDetailComponent,
    ReceivedBidsComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    ModalModule.forRoot(),
    RouterModule.forChild(routes)
  ]
})
export class BidsModule { }
