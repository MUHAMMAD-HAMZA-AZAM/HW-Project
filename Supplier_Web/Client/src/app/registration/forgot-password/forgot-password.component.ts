import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AspnetRoles, ResponseVm, StatusCode } from '../../Shared/Enums/enum';
import { IPageSeoVM } from '../../Shared/Enums/Interface';
import { CommonService } from '../../Shared/HttpClient/HttpClient';
import { metaTagsService } from '../../Shared/HttpClient/seo-dynamic.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  public mobileNumberNotFound: boolean = false;
  public appValForm: FormGroup;
  public response: ResponseVm = new ResponseVm();
  constructor(
    public _httpService: CommonService,
    public formBuilder: FormBuilder,
    private router: ActivatedRoute,
    private _metaService: metaTagsService,
    private toastr: ToastrService
  ) {
this.appValForm={} as FormGroup;
 }

  ngOnInit(): void {

    this.appValForm = this.formBuilder.group({
      mobileNumber: [null, [Validators.required, Validators.pattern("^[0-9]{4}[0-9]{7}$")]]
    });
    this.bindMetaTags();
  }

  get f() { return this.appValForm.controls; }

  
  public Send() {
    if (this.appValForm.invalid) {
      this.appValForm.markAllAsTouched();
      return;
    }
    let formData = this.appValForm.value;
    this._httpService.GetData(this._httpService.apiUrls.Customer.GetUserIdByEmailOrPhoneNumber + "?emailOrPhoneNumber=" + formData.mobileNumber + "&userRoles=" + AspnetRoles.CRole, true).then(res => {
      this.response = res;
      console.log(this.response);
      if (this.response.status == StatusCode.OK) {
        localStorage.setItem("forgotPassword", JSON.stringify(this.response.resultData));  
        this._httpService.NavigateToRoute(this._httpService.apiRoutes.ForgotPassword.forgotPasswordCode);
        this.appValForm.reset();
      }
      else {
        this.mobileNumberNotFound = true;
        setTimeout(() => {
          this.mobileNumberNotFound = false;
        },3000);
      }
    }, error => {
      this._httpService.Loader.show();
      console.log(error);
    });
  }
  public bindMetaTags() {
    this._httpService.get(this._httpService.apiUrls.CMS.GetSeoPageById + "?pageId=22").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description, res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }

}
