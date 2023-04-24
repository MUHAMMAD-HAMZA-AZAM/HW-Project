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
  selector: 'app-area',
  templateUrl: './area.component.html',
  styleUrls: ['./area.component.css']
})
export class AreaComponent implements OnInit {


  public modelName: string;
  public btnReset: boolean = false;
  public formData: any;
  public appValForm: FormGroup;
  public decodedtoken: any;
  public arealist: any = [];
  public statelist: any = [];
  public cityList: any = [];
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
    this.userRole = JSON.parse(localStorage.getItem("Area"));
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.appValForm = this.fb.group({
      areaId: [0],
      stateId: ['null', [Validators.required]],
      areaName: ['', [Validators.required]],
      cityId: ['null', [Validators.required]],
      active: ['', [Validators.required]],
      createdOn: [''],
      createdBy: [''],
      modifiedOn: [''],
      modifiedBy: ['']
    });
    debugger;
    this.getCitiesList(0);
    this.getStatesList();
    this.getAreaList();
  }

  ///* ..................Get Area List......................... */
  public getAreaList() {
    debugger;
    this.Loader.show();
    this._httpService.get(this._httpService.apiRoutes.Supplier.GetAreaListForAdmin).subscribe(result => {
      this.response = result.json();
      debugger;
      if (this.response.status == StatusCode.OK) {
        this.arealist = this.response.resultData;
        this.Loader.hide();
      }

    })
  }
  /*  ....................get controls...................*/
  get f() {
    return this.appValForm.controls;
  }

  /*....................change country status........................*/
  public delete(areaId) {
    this.Loader.show();
    let obj = {
      areaId: areaId,
      userId: this.decodedtoken.UserId
      /*    this.decodedtoken.UserId*/
    };
    this._httpService.PostData(this._httpService.apiRoutes.Supplier.deleteareaStatus, JSON.stringify(obj), true).then(res => {
      this.response = res.json();
      if (this.response.status == StatusCode.OK) {
        this._modalService.dismissAll();
        this.getAreaList();
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

  //-------------------  Show Modal For Add Area/........................
  public showModal(content) {
    this.modelName = "Add  new Area";
    this._modalService.open(content, { backdrop: 'static', keyboard: false });
  }

  //-------------------  Show Modal For Update Area.......................//
  public update(data, content) {
    debugger;
    this.cityList = null;
    this.getCitiesList(data.stateId);
    this.Loader.show();
    this.btnReset = true;
    this.showModal(content);
    this.modelName = "Update Area";
    this.appValForm.patchValue(data);
    this.Loader.hide();
  }
  /* ................. add update Area name.................*/
  public saveAndUpdateArea() {
    debugger;
    this.formData = this.appValForm.value;
    this.formData.userId = this.decodedtoken.UserId;
    /* "39a6b3ac-5ff7-40cc-886a-d79882462607"*/
    /*     this.decodedtoken.UserId;*/
    this.Loader.show();
    this._httpService.PostData(this._httpService.apiRoutes.Supplier.saveAndUpdateArea, JSON.stringify(this.formData), true).then(res => {
      this.response = res.json();
      if (this.response.status == StatusCode.OK) {
        if (this.response.resultData) {
          this.toaster.error("Area Name already Exist", "Alert", { timeOut: 5000 });
        }
        else {
          this.toaster.success(this.response.message, "Success");
        }
        this._modalService.dismissAll();
        this.getAreaList();
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
    this.appValForm.controls.areaId.setValue(0);
  }


  //-------------------  Close Modal............
  public closeModal() {
    this._modalService.dismissAll();
    this.resetForm();
    this.btnReset = false;
  }

  //------------------------ Show States List
  public getStatesList() {
    this._httpService.get(this._httpService.apiRoutes.Common.GetStatesList).subscribe(result => {
      this.statelist = result.json();
      
    })
  }
    //------------------------ Show City List
  public getCitiesList(stateId: number) {
    this.appValForm.controls['cityId'].setValue(null)
    this._httpService.get(this._httpService.apiRoutes.Common.GetCitiesList).subscribe(result => {
      this.cityList = result.json();
      if (stateId == 0) {
        this.cityList = this.cityList;
      }
      else {
        this.cityList = this.cityList.filter(x => x.stateId == stateId)
      }
      //this.appValForm.controls['cityId'].setValue(64)
    });
  }
}

