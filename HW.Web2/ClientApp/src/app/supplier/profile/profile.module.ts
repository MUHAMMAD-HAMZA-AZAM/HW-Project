import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProfilesComponent } from './profiles/profiles.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { NgxImageCompressService } from 'ngx-image-compress';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ImageCropperModule } from 'ngx-image-cropper';


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
      ImageCropperModule,
      ReactiveFormsModule,
      NgMultiSelectDropDownModule,
      RouterModule.forChild(routes),
      ModalModule.forRoot(),

    ],
    providers: [
      NgxImageCompressService
    ],
  })
export class ProfileModule { }
