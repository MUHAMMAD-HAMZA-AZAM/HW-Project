import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { IResponseVM, StatusCode } from 'src/app/Shared/Enums/enums';
import { CommonService } from 'src/app/Shared/HttpClient/_http';

@Component({
  selector: 'app-cancellation-reasons',
  templateUrl: './cancellation-reasons.component.html',
  styles: [
  ]
})
export class CancellationReasonsComponent implements OnInit {
  public modelName: string;
  public formData: any;
  public appValForm: FormGroup;
  public decodedtoken: any;
  public cancellationReasonsList :any = [];
  public response : IResponseVM;
  public allowview;
    public btnReset: boolean = false;
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
    this.userRole = JSON.parse(localStorage.getItem("Cancellation Reasons"));
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
      this.appValForm = this.fb.group({
        id: [0],
        reasonName: ['', [Validators.required]],
        userRole: [null, [Validators.required]],
        isActive: [false, [Validators.required]],
        createdOn: [''],
        createdBy: [''],
        modifiedOn: [''],
        modifiedBy: ['']
      });
      this.getCancellationReasonsList();
  }
  get f() {
    return this.appValForm.controls;
  }

   //------------------- Show Cancellation Reasons List 
   public getCancellationReasonsList() {
    this.Loader.show();
    this._httpService.get(this._httpService.apiRoutes.Supplier.GetCanellationReasonsListForAdmin).subscribe(res => {
       this.response= res.json();
       if(this.response.status == StatusCode.OK){
         this.cancellationReasonsList = this.response.resultData;
         console.log(this.cancellationReasonsList);
         this.Loader.hide();
       }
    }, error => {
      this.Loader.show();
      console.log(error);
    });
  }
 //------------------- Add & Update Reason
 public saveAndUpdateCancellationReason() {
  if (this.appValForm.invalid) {
    return;
  }
  else {
    this.formData = this.appValForm.value;
  
    if (this.formData.id <= 0) {
      //add Cancellation Reason
      this.formData.action = 'add';
      this.formData.userId = this.decodedtoken.UserId;
      console.log(this.formData);
      this.Loader.show();
      this._httpService.PostData(this._httpService.apiRoutes.Supplier.AddUpdateCancellationReason, JSON.stringify(this.formData), true).then(res => {
        this.response = res.json();
        if (this.response.status == StatusCode.OK) {
          this.toaster.success(this.response.message,"Success");
          this._modalService.dismissAll();
          this.getCancellationReasonsList();
          this.resetForm();
          this.Loader.hide();
        }
      }, error => {
        this.Loader.show();
        console.log(error);
      });
    }
    else {
      //update Cancellation Reason
      this.formData.userId = this.decodedtoken.UserId;
      this.formData.action = "update";
      console.log(this.formData);
      this._httpService.PostData(this._httpService.apiRoutes.Supplier.AddUpdateCancellationReason, JSON.stringify(this.formData),true).then(result => {
        let res = result.json();
        if (res.status == StatusCode.OK) {
          this.toaster.success(this.response.message,"Success");
          this._modalService.dismissAll();
          this.getCancellationReasonsList();
          this.resetForm();
          this.Loader.hide();
          this.btnReset = false;
        }
      }, error => {
        this.Loader.show();
        console.log(error);
      });
    }
  }
}

   //-------------------  Show Modal For Add Reason
   public showModal(content) {
    this.modelName = "Add Cancellation Reason";
     this._modalService.open(content, { backdrop: 'static', keyboard: false });
  }

//-------------------  Show Modal For Update Reason
public update(data, content) {
  this.Loader.show();
  this.btnReset = true;
  this.showModal(content);
  this.modelName = "Update Cancellation Reason";
  this.appValForm.patchValue(data);
  this.Loader.hide();
}

public delete(reasonId) {
  this.Loader.show();
  let obj = {
    id: reasonId,
    action: "delete",
    userId: this.decodedtoken.UserId
  };
  this.formData = obj;
  console.log(this.formData);
  this.Loader.show();
  this._httpService.PostData(this._httpService.apiRoutes.Supplier.AddUpdateCancellationReason, JSON.stringify(this.formData),true).then(result => {
    let res = result.json();
    if (res.status == StatusCode.OK) {
      this._modalService.dismissAll();
      this.getCancellationReasonsList();
      this.resetForm();
      this.Loader.hide();
    }
  }, error => {
    this.Loader.show();
    console.log(error);
  });
}

   //-------------------  Reset Form
   public resetForm() {
    this.appValForm.reset();
    this.appValForm.controls.id.setValue(0);
  }

     //-------------------  Close Modal
  public closeModal(){
   this.btnReset = false;
    this._modalService.dismissAll();
    this.appValForm.reset();
  }
}
