import { HttpResponse, HttpStatusCode } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-forgotpassword',
  templateUrl: './forgotpassword.component.html',
  styleUrls: ['./forgotpassword.component.css']
})
export class ForgotpasswordComponent implements OnInit {
  public submitted = false;
  public errorCode: boolean = false
  public appValForm: FormGroup;
  public roleType:number = 4;
  constructor(public _httpService: CommonService, public formBuilder: FormBuilder) {
    this.appValForm = {} as FormGroup;
  }

  ngOnInit(): void {
    this.appValForm = this.formBuilder.group({
      mobileNo: ['', Validators.required]
    });
  }
  get f() { return this.appValForm.controls; }
  Send() {
    this.submitted = true;

    if (this.appValForm.valid) {
      this._httpService.GetData(this._httpService.apiUrls.Supplier.ForgetPassword.GetUserIdByEmailOrPhoneNumber + "?emailOrPhoneNumber=" + this.appValForm.value.mobileNo + "&userRoles=" + this.roleType, true).then(result => {
        let response = result;
        
        if (response.status == HttpStatusCode.Ok) {
          localStorage.setItem("forgotPassword", JSON.stringify(response.resultData));
          this.errorCode = false;
          this._httpService.NavigateToRoute(this._httpService.apiRoutes.ForgotPassword.ForgotPasswordCode);
        }
        else {
          this.errorCode = true;
        }
      });
    }
  }
}
