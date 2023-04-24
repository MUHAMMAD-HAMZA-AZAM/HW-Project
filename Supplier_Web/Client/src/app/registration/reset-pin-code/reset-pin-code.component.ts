import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { combineLatest } from 'rxjs';
import { ResponseVm, StatusCode } from '../../Shared/Enums/enum';
import { IPageSeoVM } from '../../Shared/Enums/Interface';
import { CommonService } from '../../Shared/HttpClient/HttpClient';
import { metaTagsService } from '../../Shared/HttpClient/seo-dynamic.service';

@Component({
  selector: 'app-reset-pin-code',
  templateUrl: './reset-pin-code.component.html',
  styleUrls: ['./reset-pin-code.component.css']
})
export class ResetPinCodeComponent implements OnInit {
  public hasPinEntered: boolean = true;
  public forgotPasswordVM: any | null;
  //public resetPasswordVM: any;
  public userVM: any | null;
  public appValForm: FormGroup;
  public submitted: boolean = false;
  public mustMatchCheck: boolean = false;
  public response: ResponseVm = new ResponseVm();
  constructor(
    public _httpService: CommonService,
    public fB: FormBuilder,
    private router: ActivatedRoute,
    private _metaService: metaTagsService,
    private toastr: ToastrService) {
this.appValForm={} as FormGroup;
  }

  ngOnInit(): void {

    //---- PinCode Form
    this.appValForm = this.fB.group({

      tab1: ['', [Validators.required]],
      tab2: ['', [Validators.required]],
      tab3: ['', [Validators.required]],
      tab4: ['', [Validators.required]],
      tab5: ['', [Validators.required]],
    });
    this.bindMetaTags();
  }

  get f() {
    return this.appValForm.controls;
  }
  //-------------- Save and Change User Pin Code
  public saveNewPinCode() {
    let forgotPasswords = localStorage.getItem("forgotPassword");
    this.forgotPasswordVM = forgotPasswords != null ? JSON.parse(forgotPasswords) : "";
    let resetPassword = localStorage.getItem("resetPasswordToken");
    this.userVM = resetPassword != null ? JSON.parse(resetPassword) : "";

    let forgotPassword = localStorage.getItem("forgotPassword");
    this.forgotPasswordVM = forgotPassword != null ? JSON.parse(forgotPassword) : "";
    let resetPasswordToken = localStorage.getItem("resetPasswordToken");
    this.userVM = resetPasswordToken!=null? JSON.parse(resetPasswordToken):"";
    console.log(this.userVM);
    let pinCodeFormData = this.appValForm.value;
    if (this.appValForm.invalid) {
      this.appValForm.markAllAsTouched();
      this.hasPinEntered = false;
      return;
    }
    else {
      console.log("PinCode is Valid");
      if (pinCodeFormData.tab1 != "") {

        let resetPasswordVM = {
          password: "P@ss" + pinCodeFormData.tab1 + pinCodeFormData.tab2 + pinCodeFormData.tab3 + pinCodeFormData.tab4 + pinCodeFormData.tab5,
          confirmPassword: "P@ss" + pinCodeFormData.tab1 + pinCodeFormData.tab2 + pinCodeFormData.tab3 + pinCodeFormData.tab4 + pinCodeFormData.tab5,
          passwordResetToken: this.userVM.passwordResetToken,
          userId: this.userVM.userId
        };
        console.log(resetPasswordVM);
        this._httpService.PostData(this._httpService.apiUrls.ResetPassword.resetPassword, resetPasswordVM, true).then(res => {
          this.response = res;
          console.log(this.response.message);
          if (this.response.status == StatusCode.OK) {
            if (this.forgotPasswordVM.role == "Customer") {
              localStorage.removeItem('web_auth_token');
              localStorage.removeItem("resetPasswordToken");
              localStorage.removeItem("forgotPassword");
              window.location.href = this._httpService.apiRoutes.Login.login;
            }
          }
        }, error => {
          console.log(error);
        });
      }
      else {
        //this.hasPinEntered = false;
      }
    }
    
  }
  public movetoNext(privious:string, nextFieldID:string, obj:Event) {
    if ((<HTMLInputElement>obj.target).value == "") {
      document.getElementById(privious)?.focus();
    }
    else
      document.getElementById(nextFieldID)?.focus();

    if ((<HTMLInputElement>obj.target).value != "" && privious == 'fourth') {
      this.hasPinEntered = true;
    }
    else if ((<HTMLInputElement>obj.target).value == "") {
      this.hasPinEntered = false;
    }
  }
  public bindMetaTags() {
    this._httpService.get(this._httpService.apiUrls.CMS.GetSeoPageById + "?pageId=26").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description, res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }
}
