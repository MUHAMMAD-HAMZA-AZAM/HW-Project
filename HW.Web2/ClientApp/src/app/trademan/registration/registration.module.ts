import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BussinessdetailComponent } from './bussinessdetail/bussinessdetail.component';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { AgmCoreModule } from '@agm/core';
import { googleApiKey } from '../../shared/Enums/enums';
import { AutocompleteLibModule } from 'angular-ng-autocomplete';



const routes: Routes = [
  { path: 'Bussiness', component: BussinessdetailComponent, },

]
@NgModule({
  declarations: [BussinessdetailComponent],
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
