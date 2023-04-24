import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ResetPassword, ResetPasswordVm, ResponseVm, forgotPasswrodVm } from '../../../models/commonModels/commonModels';
import { CommonService } from '../../../shared/HttpClient/_http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { resetPassword, httpStatus } from '../../../shared/Enums/enums';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

//import { $} from 'jquery';
@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  public resetPasswordVm: ResetPassword = {} as  ResetPassword;
  public resetPassword: ResetPasswordVm = {} as  ResetPasswordVm;
  public responseVm: ResponseVm = {} as ResponseVm;
  public confirmPassword: string="";
  
  public password: string="";
  public appValForm: FormGroup;
  public passwordErrorMessage: string="";
  public confirmPasswordErrorMessage: string="";
  public confirmPasswordErrorMessageMustMatch: string="";
  public submittedApplicationForm: boolean = false;
  public mustmatchcheck: boolean = false;
  public error: boolean = false;
  public hasPinentered: boolean = true;

  constructor(private toast: ToastrService, public router: Router, public service: CommonService, private formBuilder: FormBuilder, public Loader: NgxSpinnerService) {
        this.appValForm = {} as FormGroup;
  }

  ngOnInit() {
    
    var data = localStorage.getItem("resetPasswordToken");
    this.resetPasswordVm = data!=null ? JSON.parse(data):"";
    this.appValForm = this.formBuilder.group({
      tab1: ['', [Validators.required]],
      tab2: ['', [Validators.required]],
      tab3: ['', [Validators.required]],
      tab4: ['', [Validators.required]],
      tab5: ['', [Validators.required]],
    });
    
  }

  get f() {
    return this.appValForm.controls;
  }

  SaveNewPassword() {
    
    var forgotPassword = localStorage.getItem("forgotPassword");
    let userVm: forgotPasswrodVm = forgotPassword ? JSON.parse(forgotPassword) : {} as forgotPasswrodVm;
    this.passwordErrorMessage = resetPassword.password;
    this.confirmPasswordErrorMessage = resetPassword.confirmPassword;
    this.confirmPasswordErrorMessageMustMatch = resetPassword.passwordMatch;
    this.resetPassword = this.appValForm.value;

    this.submittedApplicationForm = true;
    if (this.appValForm.valid) {
      if (this.appValForm.value.tab1 != "") {
        localStorage.removeItem("resetPasswordToken");
        // this.resetPasswordVm.confirmPassword = this.resetPassword.confirmPassword;
        //this.resetPasswordVm.password = this.resetPassword.password;
        this.resetPasswordVm.password = "P@ss" + this.appValForm.value.tab1 + this.appValForm.value.tab2 + this.appValForm.value.tab3 + this.appValForm.value.tab4 + this.appValForm.value.tab5;
        this.resetPasswordVm.confirmPassword = "P@ss" + this.appValForm.value.tab1 + this.appValForm.value.tab2 + this.appValForm.value.tab3 + this.appValForm.value.tab4 + this.appValForm.value.tab5;
        
        this.service.PostData(this.service.apiRoutes.resetPassword.resetPassword, this.resetPasswordVm, true).then(result => {
          

          this.responseVm = result ;
           localStorage.removeItem("forgotPassword");
          //this.toast.success("Password Changed Successfully!");
         // if (this.responseVm.status == httpStatus.Ok) {
            if (userVm.role == "Customer")
              this.service.NavigateToRouteWithQueryString(this.service.pagesUrl.CommonRegistrationPages.login);

           else if (userVm.role == "Tradesman")
              this.service.NavigateToRouteWithQueryString(this.service.pagesUrl.CommonRegistrationPages.loginTradesman);

           else if (userVm.role == "Supplier")
              this.service.NavigateToRouteWithQueryString(this.service.pagesUrl.CommonRegistrationPages.loginSupplier);
         // }

         else if (userVm.role == "Organization")
            this.service.NavigateToRouteWithQueryString(this.service.pagesUrl.CommonRegistrationPages.loginTradesman);

          else {
            this.error = true;
          }
        })
      }
      else {
        this.mustmatchcheck = true;
      }
    }
    else {
      this.hasPinentered = false;
     // this.toast.error("Please enter five digit code!" , "Error");
    }
  }

  public movetoNext(privious: string, nextFieldID: string, obj: Event) {
    if ((<HTMLInputElement>obj.target).value == "") {
      document.getElementById(privious)?.focus();
    }
    else
      document.getElementById(nextFieldID)?.focus();

    if ((<HTMLInputElement>obj.target).value != "" && privious == 'fourth') {
      this.hasPinentered = true;
    }
    else if ((<HTMLInputElement>obj.target).value == "") {
      this.hasPinentered = false;
    }
  }

  numberOnly(event: KeyboardEvent): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }

    return true;

  }
}
