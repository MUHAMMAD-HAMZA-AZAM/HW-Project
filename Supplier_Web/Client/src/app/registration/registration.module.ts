import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { SignupComponent } from './signup/signup.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ForgotPasswordCodeComponent } from './forgot-password-code/forgot-password-code.component';
import { ResetPinCodeComponent } from './reset-pin-code/reset-pin-code.component';

const routes: Routes = [
  {
    path: 'signup', component: SignupComponent
  },
  {
    path: 'login',component: LoginComponent
  },
  {
    path: 'home',component: HomeComponent
  },
  {
    path: 'forgotPassword', component: ForgotPasswordComponent
  },
  {
    path: 'forgotPasswordCode', component: ForgotPasswordCodeComponent
  },
  {
    path: 'resetPinCode', component: ResetPinCodeComponent
  }
];

@NgModule({
  declarations: [
    SignupComponent,
    LoginComponent,
    HomeComponent,
    ForgotPasswordComponent,
    ForgotPasswordCodeComponent,
    ResetPinCodeComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule]
})
export class RegistrationModule { }
