import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegistrationBasicComponent } from './registration-basic/registration-basic.component';
import { RegistrationOtpCodeComponent } from './registration-otp-code/registration-otp-code.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'appregistrationbasic/tradesman', component: RegistrationBasicComponent },
  { path: 'appregistrationbasic/organization', component: RegistrationBasicComponent },
  { path: 'appregistrationbasic/user', component: RegistrationBasicComponent },
  { path: 'appregistrationbasic/supplier', component: RegistrationBasicComponent },
  { path: 'appregistrationotpcode', component: RegistrationOtpCodeComponent },
];

@NgModule({
  declarations: [
    RegistrationBasicComponent,
    RegistrationOtpCodeComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes)
  ]
})
export class RegistrationBasicModule { }
