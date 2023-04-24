import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from "@angular/router";
import { CommonService } from '../../HttpClient/_http';
import { SpUserProfileVM } from '../../Models/PrimaryUserModel/PrimaryUserModel';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { MessagingService } from '../../CommonServices/messaging.service';

@Component({
  selector: 'app-header',
  templateUrl: './app-header.component.html',
  styleUrls: ['./app-header.component.css']
})
export class AppHeaderComponent {
  public userProfile: SpUserProfileVM = new SpUserProfileVM;

  public userId: number = 1;
  public role: string = "Admin";
  public userName;
  public sRolesList = [];
  public notificationList = [];
  public settings = false;
  public submmited: boolean;
  public notificaioncount: number;
  public reports = false;
  public pkgAndPromo = false;
  public customerreport = false;
  public tradesmanreport = false;
  public supplierreport = false;
  public postedjobsreport = false;
  public bidsreport = false;
  public postdadsreport = false;
  public isCompare = true ;
  public loggeduserId : string ;
  public changePasswordForm: FormGroup;
  public menuAndSubMenuItem: any = [];
  jwtHelperService: JwtHelperService = new JwtHelperService();

  constructor(private _modalService: NgbModal,
    private formBuilder: FormBuilder,
    public toastr: ToastrService,
    private _messagingService: MessagingService,
    public service: CommonService,
    private router: Router) {
  }

  ngOnInit() {
    this._messagingService.receiveMessage();
    this.reports = true;
    this.changePasswordForm = this.formBuilder.group({
      //currentPassword: ['', [Validators.required, Validators.maxLength(50)]],
      newPassword: ['', [Validators.required, Validators.maxLength(20), Validators.minLength(8)]], //Validators.pattern(/^[\w 0-9 ]$/)
      confirmNewPassword: ['', [Validators.required]],
    });
    this.makeData();
    this.getNotification();
    this.submmited = false;
  }
  makeData() {
    var srl = JSON.parse(localStorage.getItem("SecurityRole"));
    this.userName = srl[0].userName;
    let dataArray = [];
    for (var i in srl) {
      let subArray = []
      srl.filter(x => x.menuId == srl[i].menuId && !x.isModule).map(x => { //get all subitems by menu id
        let subObj = {
          securityRoleItemName: x.securityRoleItemName,
          allowDelete: x.allowDelete,
          allowEdit: x.allowEdit,
          allowExport: x.allowExport,
          allowPrint: x.allowPrint,
          allowView: x.allowView,
          spectialrights: x.spectialrights,
          routingPath: x.routingPath
        }
        subArray.push(subObj)
      })
      let objWithName = { pageName: "", allowDelete: false, allowEdit: false, allowExport: false, allowPrint: false, allowView: false, routingPath: "", subArray: [], };
      objWithName = {
        pageName: srl[i].securityRoleItemName,
        allowDelete: srl[i].allowDelete,
        allowEdit: srl[i].allowEdit,
        allowExport: srl[i].allowExport,
        allowPrint: srl[i].allowPrint,
        allowView: srl[i].allowView,
        routingPath: srl[i].routingPath,
        subArray: subArray
      }
      let data = objWithName.subArray.filter(x => x.securityRoleItemName == srl[i].securityRoleItemName);
      if (data.length <= 0) {
        if (srl[i].isModule && subArray.length >= 1) {
          objWithName.subArray = subArray;
          dataArray.push(objWithName)
        }
        else {
          objWithName.subArray = [];
          dataArray.push(objWithName);
        }
        this.menuAndSubMenuItem = dataArray.filter(x => (x.pageName == "Settings") || (x.pageName == "Financial") || (x.pageName == "Reports") || (x.pageName == "Packages And Promotion"));
      }
    }
  }
  logOut() {
    localStorage.clear();
    this.service.NavigateToRoute(this.service.apiRoutes.Login.loginurl);
  }

  setNotificationCountZero() {
    this.notificaioncount = null;
  }

  customerDetail() {
    //pass userId: Customer = 1, Tradesman = 2, Supplier = 3
    this.service.NavigateToRouteWithQueryString(this.service.apiRoutes.Users.UserProfile, { queryParams: { userId: this.userId, userRole: this.role } });

  }
  getNotification() {
    this.service.get(this.service.apiRoutes.Notifications.GetAdminNotifications + "?pageSize=" + 10 +"&pageNumber="+1).subscribe(result => {
      var res = result.json();
      if (res != null) {
        this.notificationList = res;
        //if (this.notificationList[0].unreadNotifictionsRecord > 99) {
        //  this.notificaioncount = 99;
        //}
        //else {
        //  this.notificaioncount = this.notificationList[0].unreadNotifictionsRecord;
        //}
      }

    })
  }
  getUserProfile() {
  }
  redirectToPage(data) {

    if (data == "NewOrderPlace") {
      this.service.NavigateToRoute(this.service.apiRoutes.Supplier.GetSuppliersOrderlistUrl);
    }
  }
  changePassword() {
    this.submmited = true;
    if (this.changePasswordForm.valid) {
      this.comparePassword();
      if (this.isCompare) {
        let formData = this.changePasswordForm.value;

        if (this.loggeduserId != null && this.loggeduserId != "") {
          formData.adminId = this.loggeduserId;
        }
        else {
          alert("invalid user id")
        }
        this.service.PostData(this.service.apiRoutes.Login.ChangeAdminUserPassword, formData, true).then(result => {
          var res = result.json();
          if (res.status == 200) {
            this.toastr.success("Password changed successfully!", "Success");
            this._modalService.dismissAll();
            this.changePasswordForm.reset();
            this._modalService.dismissAll();
          }

        });
      }
    }
    else {
      this.changePasswordForm.markAllAsTouched();
    }
  }

  showModel(content) {
    this._modalService.open(content);
    var auth = localStorage.getItem("auth_token");
    var decodedtoken = this.jwtHelperService.decodeToken(auth);
    this.loggeduserId = decodedtoken.UserId;
  }
  get t() {
    return this.changePasswordForm.controls;
  }
  comparePassword() {
   
    if (this.changePasswordForm.value.newPassword != this.changePasswordForm.value.confirmNewPassword) {
      this.isCompare = false
    }
    else {
      this.isCompare = true;
      setTimeout(() => {
        this.isCompare = false;
      }, 3000)
    }
  }
  hideModel() {
    this._modalService.dismissAll();
    this.changePasswordForm.reset();
  }
}
