import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SupplierRegisterComponent } from './supplierRegister/supplierRegister.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { AgmCoreModule } from '@agm/core';
import { googleApiKey } from '../../shared/Enums/enums';
import { AutocompleteLibModule } from 'angular-ng-autocomplete';

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
      AgmCoreModule.forRoot({
        apiKey: googleApiKey.mapApiKey,
        libraries: ['places', 'geometry', 'name']
      }),
      RouterModule.forChild(routes),
      NgMultiSelectDropDownModule,
      AutocompleteLibModule,
    ]
  })
export class RegistrationModule { }
