import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SupplierRegisterComponent } from './supplierRegister/supplierRegister.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';

const routes: Routes = [
  { path: 'SupplierRegister', component: SupplierRegisterComponent },
]

@NgModule(
  {
    declarations: [
      SupplierRegisterComponent,
    ],
    imports: [
      CommonModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      RouterModule.forChild(routes),
      NgMultiSelectDropDownModule
    ]
  })
export class RegistrationModule { }
