import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FreeshippingComponent } from './freeshipping.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [
  { path: 'free-shipping', component: FreeshippingComponent, },
];

@NgModule({
  declarations: [
    FreeshippingComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes)
  ]
})
export class FreeshippingModule { }
