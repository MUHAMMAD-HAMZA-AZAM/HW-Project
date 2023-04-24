import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { ReceivedBidsComponent } from './receivedBids/receivedBids.component';


const routes: Routes = [
  { path: 'ReceivedBids', component: ReceivedBidsComponent, }
]

@NgModule({
  declarations: [
    ReceivedBidsComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forChild(routes)
  ]
})
export class BidsModule { }
