import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '../Shared/HttpClient/_http';
import { ForgotPassword } from '../Shared/Models/HomeModel/HomeModel';
import { forgotPassword } from '../Shared/Enums/enums';
import { apiUrls } from '../Shared/ApiRoutes/ApiRoutes';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  public email: string;
  public emailErrorMessage: string;
  public submittedApplicationForm: boolean = false;
  public appValForm: FormGroup;
  public unauthorizedErrorMessage: string;
  public roleType;
  public role: string;
  public roleerror = false;
  forgotPasswordVm: ForgotPassword = new ForgotPassword();
  constructor(public httpService: CommonService, private formBuilder: FormBuilder) {
    this.appValForm = this.formBuilder.group({
      email: ['', Validators.required],
    })}

  ngOnInit() {
  }
  get f() {
    return this.appValForm.controls;
  }
  forgotPassword() {
    
    this.unauthorizedErrorMessage = forgotPassword.unauthorizedErrormessage;
    this.submittedApplicationForm = true;
    this.emailErrorMessage = forgotPassword.forgotPasswordEmailError;
    this.forgotPasswordVm = this.appValForm.value;
    if (this.appValForm.valid) {
      var data;
      this.httpService.get(apiUrls.Login.forgotPasswordUrl + "?email=" + this.forgotPasswordVm.email).subscribe(result => {
        data = result.json();
        
        if (data == forgotPassword.unauthorizedUser) {
          this.submittedApplicationForm = true;
        }
      })
      
    }
  }
}
