import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxImageCompressService } from 'ngx-image-compress';
import { ModalModule } from 'ngx-bootstrap/modal';
import { CreateAdComponent } from './create/create.component';
import { AutocompleteLibModule } from 'angular-ng-autocomplete';
const routes: Routes = [
  { path: 'Create', component: CreateAdComponent}
]



@NgModule({
  declarations: [
   CreateAdComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    ModalModule.forRoot(),
    AutocompleteLibModule,
  ],
  providers: [
    NgxImageCompressService
  ],
})
export class PostAdModule { }
