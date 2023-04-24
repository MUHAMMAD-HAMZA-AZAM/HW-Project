import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContactUsComponent } from './contactUs/contactUs.component';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AgmCoreModule } from '@agm/core';
import { googleApiKey } from '../shared/Enums/enums';


const routes: Routes = [
  { path: 'User/ContactUs', component: ContactUsComponent, },

]

@NgModule({
  declarations: [
    ContactUsComponent,

  ],
  imports: [
    AgmCoreModule.forRoot({
      apiKey: googleApiKey.mapApiKey,
      libraries: ['places', 'geometry']
    }),
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ModalModule.forRoot(),
    RouterModule.forChild(routes)
  ],
   schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
})export class ContactUsModule { }
