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
  selector: 'app-state',
  templateUrl: './state.component.html',
  styleUrls: ['./state.component.css']
})
export class StateComponent implements OnInit {

  public modelName: string;
  public btnReset: boolean = false;
  public formData: any;
  public appValForm: FormGroup;
  public decodedtoken: any;
  public countrylist: any = [];
  public statelist: any = [];
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
    this.userRole = JSON.parse(localStorage.getItem("State"));
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.appValForm = this.fb.group({
      stateId: [0],
      countryId: ['null', [Validators.required]],
      name: ['', [Validators.required]],
      code: ['', [Validators.required]],
      active: ['', [Validators.required]],
      createdOn: [''],
      createdBy: [''],
      modifiedOn: [''],
      modifiedBy: ['']
    });
    this.getStateList();
    this.getCountryList();
  }

  ///* ..................Get state List......................... */
  public getStateList() {
    debugger;
    this.Loader.show();
    this._httpService.get(this._httpService.apiRoutes.Supplier.GetStateListForAdmin).subscribe(result => {
      this.response = result.json();
      if (this.response.status == StatusCode.OK) {
        this.statelist = this.response.resultData;
        this.Loader.hide();
      }

    })
  }
  /*  ....................get controls...................*/
  get f() {
    return this.appValForm.controls;
  }

  public delete(stateid) {
    this.Loader.show();
    let obj = {
      stateId: stateid,
      userId: this.decodedtoken.UserId
    };
    this._httpService.PostData(this._httpService.apiRoutes.Supplier.deletestateStatus, JSON.stringify(obj), true).then(res => {
      this.response = res.json();
      if (this.response.status == StatusCode.OK) {
        this._modalService.dismissAll();
        this.getStateList();
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

  //-------------------  Show Modal For Add state/........................
  public showModal(content) {
    this.modelName = "Add  new State";
    this._modalService.open(content, { backdrop: 'static', keyboard: false });
  }

  //-------------------  Show Modal For Update state.......................//
  public update(data, content) {
    this.Loader.show();
    this.btnReset = true;
    this.showModal(content);
    this.modelName = "Update State";
    this.appValForm.patchValue(data);
    this.Loader.hide();
  }
  /* ................. add update State name.................*/
  public saveAndUpdateState() {
    debugger;
    this.formData = this.appValForm.value;
    this.formData.userId = this.decodedtoken.UserId;
    this.Loader.show();
    this._httpService.PostData(this._httpService.apiRoutes.Supplier.AddUpdateState, JSON.stringify(this.formData), true).then(res => {
      this.response = res.json();
      if (this.response.status == StatusCode.OK) {
        if (this.response.resultData) {
          this.toaster.error("State Name already Exist", "Alert", { timeOut: 5000 });
        }
        else {
          this.toaster.success(this.response.message, "Success");
        }
        this._modalService.dismissAll();
     this.getStateList();
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
    this.appValForm.controls.stateId.setValue(0);
  }


  //-------------------  Close Modal............
  public closeModal() {
    this._modalService.dismissAll();
    this.resetForm();
    this.btnReset = false;
  }

  //------------------------ Show Country List.............................
  public getCountryList() {
    this._httpService.GetData(this._httpService.apiRoutes.Supplier.GetCountryList).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK)  {
          this.countrylist = this.response.resultData;
        }
      }, error => {
        console.log(error);
      });
  }
}


