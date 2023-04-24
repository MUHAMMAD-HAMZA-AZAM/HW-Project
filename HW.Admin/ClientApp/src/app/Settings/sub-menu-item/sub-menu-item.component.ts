import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../Shared/HttpClient/_http';
import { NgxSpinnerService } from "ngx-spinner";
import { DatePipe } from '@angular/common';
import { httpStatus } from '../../Shared/Enums/enums';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sub-menu-item',
  templateUrl: './sub-menu-item.component.html',
  styleUrls: ['./sub-menu-item.component.css']
})
export class SubMenuItemComponent implements OnInit {
  public appValForm: FormGroup;
  public modelText: string = "Add New Sub Menu Item";
  public decodedtoken: any;
  public formData: any;
  public submited: boolean = false;
  public menuItemsList: any = [];
  public subMenuItemsList: any = [];

  jwtHelperService: JwtHelperService = new JwtHelperService();
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    

  constructor(
    public router: Router,public _modalService: NgbModal, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Sub Menu"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.accountForm();
    this.getSubMenuItemsList();
    this.getMenuItemsList();
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
  }
  public accountForm() {
    this.appValForm = this.fb.group({
      subMenuId: [0],
      menuId: [null, [Validators.required]],
      subMenuItemName: ['', [Validators.required, Validators.maxLength(150)]],
      active: [false],
    })
  }

  saveAndUpdate() {
    ;
    if (this.appValForm.invalid) {
      this.submited = true;
      return;
    }
    else {
      this.formData = this.appValForm.value;
      this.formData.userId = this.decodedtoken.UserId
      //console.log(this.formData);
      this.postData(this.formData);

      //if (this.formData.id <= 0) {
      //  //add
      //  this.formData.action = 'add';
      //  this.postData(this.formData);
      //}
      //else {
      //  //update
      //  this.formData.action = "update";
      //  this.postData(this.formData);
      //}
    }
  }
  public postData(data) {
    
    this.Loader.show();
    this.service.PostData(this.service.apiRoutes.Users.AddUpdateSubMenuItem, data, true).then(res => {
      let response = res.json();
      if (response.status == httpStatus.Ok) {
        this.toastr.success(response.message, "Success");
        this._modalService.dismissAll();
        this.getSubMenuItemsList();
        this.resetForm();
        this.Loader.hide;
      }
    })
  }
  update(data, content) {
    this.Loader.show();
    this.showModal(content);
    this.modelText = "Update Item";
    this.appValForm.patchValue(data);
    this.Loader.hide();
  }

  public getMenuItemsList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Users.GetMenuItemsList).subscribe(res => {
      this.menuItemsList = res.json();
      console.log(this.menuItemsList);
      this.Loader.hide();
    })
  }
  public getSubMenuItemsList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Users.GetSubMenuItemsList).subscribe(res => {
      this.subMenuItemsList = res.json();
      console.log(this.menuItemsList);
      this.Loader.hide();
    })
  }
  showModal(content) {
    this.modelText = "Add New Sub Menu Item";
    let modRef = this._modalService.open(content, { backdrop: 'static', keyboard: false });
    modRef.result.then(() => { this.resetForm() })
  }
  get f() {
    return this.appValForm.controls;
  }
  resetForm(): void {
    this.appValForm.reset();
    this.appValForm.controls['subMenuId'].setValue(0);
  }
}
