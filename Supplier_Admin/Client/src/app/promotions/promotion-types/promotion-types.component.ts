import { HttpStatusCode } from '@angular/common/http';
import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { StatusCode } from '../../Shared/Enums/common';
import { IPromotionTypes, IResponse } from '../../Shared/Enums/Interface';
import { CommonService } from '../../Shared/HttpClient/_http';
import { PromotionType } from '../../Shared/Models/Promotions/PromotionType';
@Component({
  selector: 'app-promotion-types',
  templateUrl: './promotion-types.component.html',
  styleUrls: ['./promotion-types.component.css']
})
export class PromotionTypesComponent implements OnInit {
  public appValForm: FormGroup;
  public filterPromoForm: FormGroup;
  public submited: boolean = false;
  public userId: any="";
  public promotionType: PromotionType = new PromotionType();
  public promotionTypeList: IPromotionTypes[] = [];
  response: IResponse;
  constructor(public formBuilder: FormBuilder, public service: CommonService, public _modalService: NgbModal, public toastr: ToastrService) {
    this.appValForm = {} as FormGroup;
    this.filterPromoForm = {} as FormGroup;
  }

  ngOnInit(): void {
    this.appValForm = this.formBuilder.group({
      promotionTypeId: [0],
      promotionTypeName: ['', [Validators.required]],
      promotionTypeCode: ['', [Validators.required]],
      entityStatus: [null, [Validators.required]],
      isActive: [0]
    });
 
    this.filterPromoForm = this.formBuilder.group({
      fpromotionIdTypeId: [null],
      fpromotionCode: [''],
      fpromotionName: [''],
      entity: [null],
    });
    var decodedtoken = this.service.decodedToken();
    this.userId = decodedtoken.Id;
    this.getPromotionTypeList();
  }
  get f() {
    return this.appValForm.controls;
  }
  save() {
    
    this.submited = true;
    if (this.promotionType.status != 'Delete') {
      if (this.appValForm.invalid) {
        return;
      }
      this.promotionType = this.appValForm.value;
    }

    if (this.promotionType.promotionTypeId == null)
      this.promotionType.promotionTypeId = 0;
    if (this.promotionType.promotionTypeId == 0) {
      this.promotionType.isActive = true;
      this.promotionType.createdBy = this.userId;
    }
    else {
      this.promotionType.updatedBy = this.userId;
    }
    
    this.service.PostData(this.service.apiUrls.Supplier.PackagesAndPayments.AddNewPromotionTypeForSupplier, JSON.stringify(this.promotionType)).then(data => {
      var result = data;
      if (result.message == 'AlreadyExists') {
        this.toastr.error("Promotion Name Already Exist", "Error");
      }
      else if (result.message == 'CodeAlreadyExists') {
        this.toastr.error("Promotion Code Already Exist", "Error");
      }
      else if (result.message == 'Saved') {
        this.toastr.success("Data added Successfully", "Success");
        this.submited = false;
        this.getPromotionTypeList();
        this.resetForm();
        this._modalService.dismissAll();
      }
      else if (result.message == 'Updated') {
        if (this.promotionType.status == "Update") {
          this.toastr.success("Data Updated Successfully", "Success");
          this.resetForm();
          this._modalService.dismissAll();
          this.submited = false;
        }
        else {
        //  this.toastr.success("Change status Successfully", "Success");
          this._modalService.dismissAll();
          this.resetForm();
          this.submited = false;
        }
        this.getPromotionTypeList();
      }
    });
  }

  GetPromotionTypeById(content: TemplateRef<any>, promotionTypeId:number) {
    
    this.service.Loader.show();
    this.service.GetData(this.service.apiUrls.Supplier.PackagesAndPayments.GetPromotionTypeByIdForSupplier + promotionTypeId).then(data => {
      if (data.status == StatusCode.OK) {
        //
        this.promotionType = data.resultData;
        this.appValForm.patchValue(this.promotionType);

        //console.log(this.promotionType);
        //console.log(this.appValForm.controls.promotionTypeId.value);
        this._modalService.open(content);
        this.service.Loader.hide();
      }
    });
  }
  public getPromotionTypeList() {
    
    var data = this.filterPromoForm.value;
    let obj = {
      promotionName: data.fpromotionName,
      promotionCode: data.fpromotionCode,
      entityStatus: data.entity,
      supplierId: this.userId
    };

    this.service.PostData(this.service.apiUrls.Supplier.PackagesAndPayments.GetAllPromotionTypesForSupplier, obj).then(result => {
      this.response = result;
     if(this.response.status == StatusCode.OK){
       if(this.response.resultData){       
         this.promotionTypeList = this.response.resultData;
      }
      else{
        this.promotionTypeList = [];
        this.toastr.error("No Data Found", "Error");
      }
     }
   
        this.service.Loader.hide();
    });
  }
  resetFForm() {
    this.filterPromoForm.reset();
  }


  deletepackage(packagedata: IPromotionTypes, deleteContent: TemplateRef<any>) {
    this.promotionType.promotionTypeId = packagedata.promotionTypeId;
    this.promotionType.promotionTypeCode = packagedata.promotionTypeCode;
    this.promotionType.promotionTypeName = packagedata.promotionTypeName;
    this.promotionType.entityStatus = packagedata.entityStatus;
    if (packagedata.isActive == true)
      this.promotionType.isActive = false;
    else
      this.promotionType.isActive = true;
    this.promotionType.status = "Delete";
    this._modalService.open(deleteContent);
  }
  confirmDeletePackage() {
    this.save();
  }
  showModal(content: TemplateRef<any>) {  
    this._modalService.open(content);
    this.resetForm();
  }

  resetForm() {
      this.appValForm.reset();
  }
}
