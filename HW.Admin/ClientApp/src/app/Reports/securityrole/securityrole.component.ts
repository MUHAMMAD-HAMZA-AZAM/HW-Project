import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../Shared/HttpClient/_http';
import { NgxSpinnerService } from "ngx-spinner";
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DynamicScriptLoaderService } from '../../Shared/CommonServices/dynamicScriptLoaderService';
import { securityRoleDetail } from '../../Shared/Models/UserModel/securityrolevm';
import { ToastrService } from 'ngx-toastr';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EventsService } from '../../Shared/events.service';

@Component({
  selector: 'app-securityrole',
  templateUrl: './securityrole.component.html',
  styleUrls: ['./securityrole.component.css']
})
export class SecurityroleComponent implements OnInit {
  public SecurityRoleItem = [];
  public SecurityRole = [];
  public securityRoleDetail: securityRoleDetail[] = [];
  public appValForm: FormGroup;
  public selectedRole: number;
  public RoleId;
  public securityRoleName;
  public securityRoleId;
  public isValid = true;
  public spectial = false;
  public modelText;
  public navItemsName = [];
  public menuItemsList = []
  public subMenuItemsList = []
  public securityMenuItems = []
  public appValueForm: FormGroup;
  public submited: boolean = false;
  public isModule: boolean = false;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };

  constructor(private events: EventsService,public toastr: ToastrService, public _modalService: NgbModal, public service: CommonService, public Loader: NgxSpinnerService, private router: Router, private route: ActivatedRoute, private fb: FormBuilder, public dynamicScripts: DynamicScriptLoaderService) {
    
  }
  ngOnInit() {
    debugger;
      this.userRole = JSON.parse(localStorage.getItem("Security Roles"));
      if (!this.userRole.allowView)
        this.router.navigateByUrl('/login');
      this.route.paramMap.subscribe(params => {
        this.RoleId = params.get('id'); // (+) converts string 'id' to a number
        //this.GetData();
        this.GetSecurityRole();
       this.PapulateData();
      });
      this.appValueForm = this.fb.group({
        id: [0],
        securityRoleItem: [null, [Validators.required]],
        subRoleItem: [null, [Validators.required]],
        isModule: [false],
        routingPath: ['', [Validators.required]]
      })
    this.getMenuItemsList();
    this.getSubMenuItemsList();
    this.getSecurityMenuItems();
    //this.makeData();
  }
  get f() {
    return this.appValueForm.controls;
  }
  makeData(srl) {
    //var srl = JSON.parse(localStorage.getItem("SecurityRole"));
    let dataArray = [];
    for (var i in srl) {
      let subArray = []
      srl.filter(x => x.menuId == srl[i].menuId).map(x => {
        let subObj = {
          securityRoleItemName: x.securityRoleItemName,
          allowDelete: x.allowDelete,
          allowEdit: x.allowEdit,
          allowExport: x.allowExport,
          allowPrint: x.allowPrint,
          allowView: x.allowView,
          securityRoleId: x.securityRoleId,
          securityRoleItems: x.securityRoleItems,
          securityRoleName: x.securityRoleName,
          spectialrights: x.spectialrights,
          subRoleItem: x.subMenuId,
          menuId: x.menuId,
          isModule: x.isModule,
          routingPath: x.routingPath,
          baseSecurityRoleItemId: x.baseSecurityRoleItemId,
        }
        subArray.push(subObj)
      })
      if (srl[i].isModule) {
        let objWithName = {
          pageName: srl[i].securityRoleItemName,
          subArray
        }
        dataArray.push(objWithName)
        this.securityRoleDetail = dataArray;
      }
    }
  }
  getSecurityMenuItems() {
    var securityrolelist = JSON.parse(localStorage.getItem("SecurityRole"));
    this.securityMenuItems = securityrolelist.filter(x => x.isModule).map(x => x.securityRoleItemName);
  }
  checkIsModule() {
    ;
    let value = this.appValueForm.controls['isModule'].value;
    if (value) {
      this.isModule = true;
      this.appValueForm.controls['routingPath'].clearValidators();
      this.appValueForm.controls['routingPath'].updateValueAndValidity();
      this.appValueForm.controls['subRoleItem'].clearValidators();
      this.appValueForm.controls['subRoleItem'].updateValueAndValidity();
    }
    else {
      this.isModule = false;
      this.appValueForm.controls['routingPath'].setValidators([Validators.required])
    }
  }
  saveAndUpdate() {  
    let formData;
    if (this.appValueForm.invalid) {
      this.submited = true;
      return;
    }
    //add
    ;
    if (this.appValueForm.value.id <= 0) {   
      formData = this.appValueForm.value;
      formData.id = 0;
      this.Loader.show();
      this.service.post(this.service.apiRoutes.Users.AddUpdateSecurityRoleItem, formData).subscribe(result => {
        var res = result.json();
        if (res != null) {
          this.resetForm();
          this.toastr.success(res.message, "Response");
          this.PapulateData();
          this._modalService.dismissAll()
          this.submited = false;
        }
        //else {
        //  this.toastr.error(res.message, "Error");
        //}
        this.Loader.hide();
      })
    }
    // update
    else {
      formData = this.appValueForm.value;
      console.log(formData);
      this.Loader.show();
      this.service.post(this.service.apiRoutes.Users.AddUpdateSecurityRoleItem, formData).subscribe(result => {
        var res = result.json();
        if (res.status == 200) {
          this.resetForm();
          this.toastr.success(res.message, "Success");
          this.PapulateData();
          this._modalService.dismissAll()
          this.submited = false;
        }
        else {
          this.toastr.error("Somthing went wrong", "Error");
        }
        this.Loader.hide();
      })

    }
  }
  setRolesInLocalStorage() {
    var srl = JSON.parse(localStorage.getItem("SecurityRole"));
    for (var i in srl) {
      let lsObj = {
        allowView: srl[i].allowView,
        allowPrint: srl[i].allowPrint,
        allowEdit: srl[i].allowEdit,
        allowExport: srl[i].allowExport,
        allowDelete: srl[i].allowDelete
      }
      localStorage.setItem(srl[i].securityRoleItemName, JSON.stringify(lsObj));
    }
  }
  PapulateData() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Users.GetSecurityRoleDetails + "?roleId=" + this.RoleId).subscribe(result => {     
      let response = result.json();
      //if (this.RoleId > 0) {
      //  localStorage.removeItem("SecurityRole");
      //  localStorage.setItem("SecurityRole", JSON.stringify(response));
      //  this.setRolesInLocalStorage();
      //}
      this.securityRoleDetail = response.sort(function (a, b) {
        var nameA = a.securityRoleItemName.toLowerCase(), nameB = b.securityRoleItemName.toLowerCase()
        if (nameA < nameB) //sort string ascending
          return -1
      });
      this.securityRoleName = this.securityRoleDetail[0].securityRoleName;
      this.securityRoleId = this.securityRoleDetail[0].securityRoleId;
      this.spectial = this.securityRoleDetail[0].spectialrights;
      this.makeData(this.securityRoleDetail);
      console.log(this.securityRoleDetail);
    });
    this.Loader.hide();
  }
  public save() {
    if (this.securityRoleName != "" && this.securityRoleName != null) {
      let securityRolesItems= [];
      for (var i in this.securityRoleDetail) {
        let singleSecurityRoleItem = this.securityRoleDetail[i].subArray;
        securityRolesItems.push(singleSecurityRoleItem);
      }
      var mergedArrays = [].concat.apply([], securityRolesItems);
      for (var i in mergedArrays) {
        mergedArrays[i].spectialrights = this.spectial;
        mergedArrays[i].securityRoleName = this.securityRoleName;
      }
      this.service.PostData(this.service.apiRoutes.Users.AddSecurityRoleDetails, mergedArrays, true).then(result => {
        var response = result.json();
        if (response) {
          this.PapulateData();
        }
        console.log(response);
        this.events.RoleChanges.emit();
        localStorage.setItem("roleChanges", "Success");
        this.router.navigateByUrl('/reports/app-securityrollist');
        this.toastr.success("Data added successfully", "Success");
      });
      this.isValid = true;
    }
    else {
      this.isValid = false;
    }

  }
  GetSecurityRole() {
    this.SecurityRole = [];
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Users.GetSecurityRoles).subscribe(result => {
      this.SecurityRole = result.json();
    });
    this.Loader.hide();
  }
  public getMenuItemsList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Users.GetMenuItemsList).subscribe(res =>{
      this.menuItemsList = res.json();
      this.Loader.hide();
    })
  }
  public getSubNavItems(value) {
    this.getSubMenuItemsList(value);
  }
  public getSubMenuItemsList(value?: any ) {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Users.GetSubMenuItemsList).subscribe(res => {
      this.subMenuItemsList = res.json();
      if (value != null) {
        var smList = this.subMenuItemsList.filter(x => x.menuId == value)
        this.subMenuItemsList = smList;
        console.log(this.subMenuItemsList);
      }
      this.Loader.hide();
    })
  }
  SecurityRoles(value: any) {
    
    this.selectedRole = value;
    if (this.selectedRole != null) {
      this.PapulateData();
    }
  }
  cancel() {
    this.router.navigateByUrl('/reports/app-securityrollist');
  }

  showModal(content) {
    this.modelText = "Add New Security Role Item"
    let modRef = this._modalService.open(content, { backdrop: 'static', keyboard: false });
    modRef.result.then(() => { this.resetForm() })
  }
  resetForm(): void {
    this.appValueForm.reset();
    this.appValueForm.controls['id'].setValue(0);
    this.appValueForm.controls['isModule'].setValue(false);
    this.getSubMenuItemsList();
  }
  updateSecurityRoleItem(item, content) {
    console.log(item);
    this.appValueForm.controls.securityRoleItem.setValue(item.menuId);
    this.appValueForm.controls.id.setValue(item.securityRoleItems);
    if (item.subRoleItem <= 0)
      this.appValueForm.controls.subRoleItem.setValue(null);
    else
      this.appValueForm.controls.subRoleItem.setValue(item.subRoleItem);
    this.appValueForm.controls.isModule.setValue(item.isModule);
    this.appValueForm.controls.routingPath.setValue(item.routingPath);

    this.modelText = "Update Security Role Item"
    this._modalService.open(content)
  }
  deleteSecurityRoleItem(securityRoleItemId) {
    
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Users.DeleteSecurityRoleItem + "?securityRoleItemId=" + securityRoleItemId).subscribe(result => {
      var res = result.json();
      if (res.status == 200) {
        this.appValueForm.reset()
        this.toastr.success(res.message, "Success");
        this.PapulateData();
      }
      else {
        this.toastr.error(res.message, "Error");
      }
      this.Loader.hide();
    })
  }
}
