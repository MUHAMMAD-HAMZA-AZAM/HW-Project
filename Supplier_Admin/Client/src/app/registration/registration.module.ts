import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SignupComponent } from './signup/signup.component';
import { LoginComponent } from './login/login.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LandingComponent } from './landing/landing.component';
import { ForgotpasswordComponent } from './forgotpassword/forgotpassword.component';
import { ForgotpasswordcodeComponent } from './forgotpasswordcode/forgotpasswordcode.component';
import { ResetpasswordComponent } from './resetpassword/resetpassword.component';


const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'signup',
    component: SignupComponent,
  },
  {
    path: 'landing',
    component: LandingComponent
  },
  {
    path: 'forgotpassword',
    component: ForgotpasswordComponent
  },
  {
    path: 'forgotpasswordcode',
    component: ForgotpasswordcodeComponent
  },
  {
    path: 'resetpassword',
    component: ResetpasswordComponent
  }
];

@NgModule({
  declarations: [
    SignupComponent,
    LoginComponent,
    LandingComponent,
    ForgotpasswordComponent,
    ForgotpasswordcodeComponent,
    ResetpasswordComponent
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
