import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { ProfilesComponent } from './profiles/profiles.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { NgxImageCompressService } from 'ngx-image-compress';
import { ImageCropperModule } from 'ngx-image-cropper';
import { ModalModule } from 'ngx-bootstrap/modal';
import { SkillprofileComponent } from './skillprofile/skillprofile.component';
import { AutocompleteLibModule } from 'angular-ng-autocomplete';


const routes: Routes = [
  { path: 'Profile', component: ProfilesComponent, },
  { path: 'Skill/:id/:slug', component: SkillprofileComponent, },

]

@NgModule({
  declarations: [
    ProfilesComponent,
    SkillprofileComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgMultiSelectDropDownModule,
    ReactiveFormsModule,
    ImageCropperModule,
    RouterModule.forChild(routes),
    ModalModule.forRoot(),
    AutocompleteLibModule,

  ],
  providers: [
    NgxImageCompressService
  ],
})
export class ProfileModule { }
