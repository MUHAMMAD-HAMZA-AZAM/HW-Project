import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { ProfileManagementComponent } from './profile-management/profile-management.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ImageCropperModule } from 'ngx-image-cropper';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AutocompleteLibModule } from 'angular-ng-autocomplete';

const routes: Routes = [
  {
    path: 'management', component: ProfileManagementComponent
  }
];

@NgModule({
  declarations: [
    ProfileManagementComponent
  ],
  imports: [
    ReactiveFormsModule,
    CommonModule,
    NgbModule,
    AutocompleteLibModule,
    ModalModule.forRoot(),
    ImageCropperModule,
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule]
})
export class ProfileManagementModule { }
