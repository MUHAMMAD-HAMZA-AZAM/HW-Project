import { Component, OnInit } from '@angular/core';
import { Adminuservm } from '../../Shared/Models/UserModel/adminuservm';
import { CommonService } from '../../Shared/HttpClient/_http';
import { NgxSpinnerService } from "ngx-spinner";
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ResponseVm } from '../../Shared/Models/HomeModel/HomeModel';
import { httpStatus, RegistrationErrorMessages } from '../../Shared/Enums/enums';
import { isNull } from 'util';

@Component({
  selector: 'app-security-roles-list',
  templateUrl: './security-roles-list.component.html',
  styleUrls: ['./security-roles-list.component.css']
})
export class SecurityRolesListComponent implements OnInit {
  public AdminUserVM: Adminuservm[] = [];
  public adminvm: Adminuservm;
  public adminRole: number = 5;
  public hide: boolean = true;
  public appValForm: FormGroup;
  public response: ResponseVm = new ResponseVm();
  public userAvailabiltyErrorMessage: string;
  public userAvailabilty: boolean = false;
  public usernameErrorMessage: string;
  public emailErrorMessage: string;
  public passwordErrorMessage: string;
  public phonenumberErrorMessage: string;
  public submitted = false;
  public dataSave = false;
  public status: boolean;
  public SecurityRole = [];
  public securityRole: string
  public securityMessege: string;
  public allowdelete;
  public allowedit;
  public allowadd;
  public allowview;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  constructor(public service: CommonService, public Loader: NgxSpinnerService, private formBuilder: FormBuilder, private router: Router) {
    this.GetSecurityRole();
  }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Security Roles"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.GetAdminUserDetails();
    //this.appValForm = this.formBuilder.group({
    //  userName: ['', [Validators.required]],
    //  email: ['', [Validators.required, Validators.minLength(8)]],
    //  phoneNumber: ['', [Validators.required, Validators.minLength(11), Validators.maxLength(12)]],
    //  password: [0],
    //  securityRole: ['', [Validators.required]],

    //});
  }
  //get f() {
  //  return this.appValForm.controls;
  //}
  GetAdminUserDetails() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Users.GetAdminUserDetails + "?roleId=" + this.adminRole).subscribe(result => {
      
      this.AdminUserVM = result.json();
    });
    this.Loader.hide();
  }

  public save() {
    this.usernameErrorMessage = RegistrationErrorMessages.UserNameErrorMessage;
    this.emailErrorMessage = RegistrationErrorMessages.emailErrorMessage;
    this.phonenumberErrorMessage = RegistrationErrorMessages.phonenumberErrorMessage;
    this.passwordErrorMessage = RegistrationErrorMessages.passwordErrorMessage;
    this.securityMessege = RegistrationErrorMessages.UserSecurityRoleMessage;
    this.submitted = true;
    if (this.appValForm.valid) {
      
      var data = this.appValForm.value;
      if (this.adminvm.id != null) {
        
        this.adminvm.userName = data.userName;
        this.adminvm.email = data.email;
        this.adminvm.phoneNumber = data.phoneNumber;
        this.adminvm.securityRole = data.securityRole;
        this.service.PostData(this.service.apiRoutes.Users.UpdateAdminUserdetails, this.adminvm, true).then(result => {
          
          //var response = result.json();
          if (result == true) {
            this.dataSave = true;
            this.GetAdminUserDetails();
            //this.hide = true;

          }
        });
      }
      else {
        this.Loader.show();
        
        this.adminvm.userName = data.userName;
        this.adminvm.email = data.email;
        this.adminvm.phoneNumber = data.phoneNumber;
        this.adminvm.password = data.password;
        this.adminvm.securityRole = data.securityRole;
        this.adminvm.role = "Admin";
        
        this.service.PostData(this.service.apiRoutes.registration.CheckEmailandPhoneNumberAvailability, this.adminvm, true).then(result => {
          this.Loader.hide();
          
          this.response = result.json();
          if (this.response.status == httpStatus.Ok) {
            this.service.PostData(this.service.apiRoutes.registration.CreateAdminUser, this.adminvm, true).then(result => {
              
              var response = result.json();
              if (response == true) {
                this.dataSave = true;
                this.GetAdminUserDetails();

                this.hide = true;

              }
            });
          }
          else {
            this.userAvailabilty = true;
            this.userAvailabiltyErrorMessage = this.response.message;
          }
        });

      }
    }

  }


  public delete(adminUserId: string) {
    this.Loader.show();
    
    this.service.get(this.service.apiRoutes.Users.DeleteAdminUser + "?userId=" + adminUserId).subscribe(result => {
      
      var res = result.json();
    });
    this.Loader.hide();

  }
  public CreateAdmin() {
   // this.hide = false;
    this.adminvm = new Adminuservm();
    this.router.navigateByUrl('/reports/app-securityrole/' + 0);
  }

  public Edit(adminvm) {
    debugger;
    this.adminvm = adminvm;

    var ids = this.adminvm.securityRoleId;
    //this.hide = false;
    // this.appValForm.patchValue(adminvm);
    this.router.navigateByUrl('/reports/app-securityrole/'+ ids);
   // this.router.navigateByUrl('Reports/securityrole/' + ids.join(","));

  }

  GetSecurityRole() {
    this.SecurityRole = [];
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Users.GetSecurityRoles).subscribe(result => {
      
      this.SecurityRole = result.json();
    });
    this.Loader.hide();
  }
  cancel() {
   // this.hide = true;
  }
}
