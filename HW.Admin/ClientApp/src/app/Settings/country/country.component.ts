import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { IResponseVM, StatusCode } from '../../Shared/Enums/enums';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-country',
  templateUrl: './country.component.html',
  styleUrls: ['./country.component.css']
})
export class CountryComponent implements OnInit {
  public modelName: string;
  public btnReset: boolean = false;
  public formData: any;
  public appValForm: FormGroup;
  public decodedtoken: any;
  public countrylist: any = [];
  public response: IResponseVM;
  public allowview;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(
    public _modalService: NgbModal,
    public toaster: ToastrService,
    public _httpService: CommonService,
    public Loader: NgxSpinnerService,
    public fb: FormBuilder,
    private router: Router) { }

  ngOnInit(): void {

    this.userRole = JSON.parse(localStorage.getItem("Country"));
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.appValForm = this.fb.group({
      countryId: [0],
      countryName: ['', [Validators.required]],
      active: ['', [Validators.required]],
      createdOn: [''],
      createdBy: [''],
      modifiedOn: [''],
      modifiedBy: ['']
    });
    this.getCountryList();
  }

  /* ..................Get country List......................... */
  public getCountryList() {
    debugger;
    this.Loader.show();
    this._httpService.get(this._httpService.apiRoutes.Supplier.GetCountryListForAdmin).subscribe(result => {
      this.response = result.json();
      if (this.response.status == StatusCode.OK) {
        this.countrylist = this.response.resultData;
        this.Loader.hide();
      }

    })
  }
/*  ....................get controls...................*/
  get f() {
    return this.appValForm.controls;
  }

  public delete(countryId) {
    this.Loader.show();
    let obj = {
      countryId: countryId,
      userId: this.decodedtoken.UserId
    };
 this._httpService.PostData(this._httpService.apiRoutes.Supplier.deleteCountryStatus, JSON.stringify(obj), true).then(res => {
      this.response = res.json();
      if (this.response.status == StatusCode.OK) {
        this._modalService.dismissAll();
        this.getCountryList();
        this.resetForm();
        this.btnReset = false;
        this.Loader.hide();
        this.toaster.success(this.response.message, "Success");
      }
    }, error => {
      this.Loader.show();
      console.log(error);
    });

  }

     //-------------------  Show Modal For Add country/........................
  public showModal(content) {
    this.modelName = "Add  new Country";
    this._modalService.open(content, { backdrop: 'static', keyboard: false });
  }

  //-------------------  Show Modal For Update Country.......................//
  public update(data, content) {
    this.Loader.show();
    this.btnReset = true;
    this.showModal(content);
    this.modelName = "Update Country";
    this.appValForm.patchValue(data);
    this.Loader.hide();
  }
  /* ................. add update countruy name.................*/
  public saveAndUpdateCountry() {
    this.formData = this.appValForm.value;
    this.formData.userId = this.decodedtoken.UserId;
    this.Loader.show();
    this._httpService.PostData(this._httpService.apiRoutes.Supplier.AddUpdateCountry, JSON.stringify(this.formData), true).then(res => {
      this.response = res.json();
      if (this.response.status == StatusCode.OK) {
        if (this.response.resultData) {
          this.toaster.error("Country Name already Exist", "Alert", { timeOut: 5000 });
        }
        else {
          this.toaster.success(this.response.message, "Success");
        }
        this._modalService.dismissAll();
        this.getCountryList();
        this.resetForm();
        this.btnReset = false;
        this.Loader.hide();
      }
    }, error => {
      this.Loader.show();
      console.log(error);
    });

  }
  //-------------------  Reset Form...................
  public resetForm() {
    this.appValForm.reset();
    this.appValForm.controls.countryId.setValue(0);
  }


  //-------------------  Close Modal............
  public closeModal() {
    this._modalService.dismissAll();
    this.resetForm();
    this.btnReset = false;
  }
}

