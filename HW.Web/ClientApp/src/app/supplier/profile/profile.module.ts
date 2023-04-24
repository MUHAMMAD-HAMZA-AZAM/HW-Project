import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProfilesComponent } from './profiles/profiles.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { NgxImageCompressService } from 'ngx-image-compress';


const routes: Routes = [
  { path: 'Profile', component: ProfilesComponent },
]

@NgModule(
  {
    declarations: [
      ProfilesComponent,
    ],
    imports: [
      CommonModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      NgMultiSelectDropDownModule,
      RouterModule.forChild(routes)
    ],
    providers: [
      NgxImageCompressService
    ],
  })
export class ProfileModule { }
