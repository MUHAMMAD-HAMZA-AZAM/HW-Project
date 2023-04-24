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
  selector: 'app-location',
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.css']
})
export class LocationComponent implements OnInit {


  public modelName: string;
  public btnReset: boolean = false;
  public formData: any;
  public appValForm: FormGroup;
  public decodedtoken: any;
  public areaList: any = [];
  public locationlist: any = [];
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
    this.userRole = JSON.parse(localStorage.getItem("Location"));
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.appValForm = this.fb.group({
      locationId: [0],
      areaId: ['null', [Validators.required]],
      locationName: ['', [Validators.required]],
      active: ['', [Validators.required]],
      createdOn: [''],
      createdBy: [''],
      modifiedOn: [''],
      modifiedBy: ['']
    });
    this.getLocationList();
    this.getAreaList();
  }

  ///* ..................Get location List......................... */
  public getLocationList() {
    this.Loader.show();
    this._httpService.get(this._httpService.apiRoutes.Supplier.GetLocationListForAdmin).subscribe(result => {
      this.response = result.json();
      if (this.response.status == StatusCode.OK) {
        this.locationlist = this.response.resultData;
        this.Loader.hide();
      }

    })
  }
  /*  ....................get controls...................*/
  get f() {
    return this.appValForm.controls;
  }

  public delete(locationId) {
    this.Loader.show();
    let obj = {
      locationId: locationId,
      userId: this.decodedtoken.UserId
      /*  this.decodedtoken.UserId*/
    };
    this._httpService.PostData(this._httpService.apiRoutes.Supplier.deletelocationStatus, JSON.stringify(obj), true).then(res => {
      this.response = res.json();
      if (this.response.status == StatusCode.OK) {
        this._modalService.dismissAll();
        this.getLocationList();
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

  //-------------------  Show Modal For Add location/........................
  public showModal(content) {
    this.modelName = "Add  new Location";
    this._modalService.open(content, { backdrop: 'static', keyboard: false });
  }

  //-------------------  Show Modal For Update location.......................//
  public update(data, content) {
    this.Loader.show();
    this.btnReset = true;
    this.showModal(content);
    this.modelName = "Update Location";
    this.appValForm.patchValue(data);
    this.Loader.hide();
  }
  /* ................. add update location name.................*/
  public saveAndUpdateLocation() {
    this.formData = this.appValForm.value;
    this.formData.userId = this.decodedtoken.UserId
  /*    this.decodedtoken.UserId;*/
    this.Loader.show();
    this._httpService.PostData(this._httpService.apiRoutes.Supplier.AddUpdateLocation, JSON.stringify(this.formData), true).then(res => {
      this.response = res.json();
      if (this.response.status == StatusCode.OK) {
        if (this.response.resultData) {
          this.toaster.error("Location Name already Exist", "Alert", { timeOut: 5000 });
        }
        else {
          this.toaster.success(this.response.message, "Success");
        }
        this._modalService.dismissAll();
        this.getLocationList();
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
    this.appValForm.controls.locationId.setValue(0);
  }


  //-------------------  Close Modal............
  public closeModal() {
    this._modalService.dismissAll();
    this.resetForm();
    this.btnReset = false;
  }

  //------------------------ Show location List
  public getAreaList() {
    let stateId = 0;
    this._httpService.GetData
      (this._httpService.apiRoutes.Supplier.GetAreaList + "?stateId=" + stateId).then(res => {
        this.response = res;
        if(this.response.status == StatusCode.OK) {
          this.areaList = this.response.resultData;
        }
      }, error => {
        console.log(error);
      });
  }
}



