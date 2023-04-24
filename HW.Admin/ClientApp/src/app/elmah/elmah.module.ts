import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ElmahLogComponent } from './elmah-log/elmah-log.component';
//import { Ng4LoadingSpinnerModule } from 'ng4-loading-spinner';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { RouterModule, Routes } from '@angular/router';
import { RatingModule } from 'ng-starrating';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ClickOutsideModule } from 'ng-click-outside';
import { DynamicScriptLoaderService } from '../Shared/CommonServices/dynamicScriptLoaderService';
//import { DeviceDetectorService } from 'ngx-device-detector';
import { DeviceInformationComponent } from './device-information/device-information.component';
const routes: Routes = [

  { path: 'elmah-log', component: ElmahLogComponent },
  { path: 'device-information', component: DeviceInformationComponent }

];

@NgModule({
  declarations: [ElmahLogComponent, DeviceInformationComponent],
  imports: [
    //Ng4LoadingSpinnerModule.forRoot(),
    CommonModule,
    FormsModule,
    NgMultiSelectDropDownModule,
    RouterModule.forChild(routes),
    NgxSpinnerModule,
    ClickOutsideModule,
    ReactiveFormsModule,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  exports: [RouterModule],
  providers: []
})
export class ElmahModule { }
