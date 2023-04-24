import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { Routes, RouterModule } from '@angular/router';
import { ForgotPasswordCodeComponent } from './forgot-password-code/forgot-password-code.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [
  { path: 'app-reset-password', component: ResetPasswordComponent },
  { path: 'app-forgot-password-code', component: ForgotPasswordCodeComponent },
  { path: 'app-forgot-password', component: ForgotPasswordComponent },
];

@NgModule({
  declarations: [
    ForgotPasswordComponent,
    ForgotPasswordCodeComponent,
    ResetPasswordComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes)
  ]
})
export class ResetpasswordModule { }
