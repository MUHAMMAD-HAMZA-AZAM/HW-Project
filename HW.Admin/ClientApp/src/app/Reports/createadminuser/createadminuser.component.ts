import { Component, OnInit, ViewChild } from '@angular/core';
import { Adminuservm} from '../../Shared/Models/UserModel/adminuservm';
import { CommonService } from '../../Shared/HttpClient/_http';
import { NgxSpinnerService } from "ngx-spinner";
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ResponseVm } from '../../Shared/Models/HomeModel/HomeModel';
import { httpStatus, RegistrationErrorMessages } from '../../Shared/Enums/enums';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { ModalDirective } from 'ngx-bootstrap/modal/modal.directive';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-createadminuser',
  templateUrl: './createadminuser.component.html',
  styleUrls: ['./createadminuser.component.css']
})
export class CreateadminuserComponent implements OnInit {
  public AdminUserVM: Adminuservm[] = [];
  public adminvm: Adminuservm;
  public adminRole: number = 5;
  public hide: boolean = true;
  public appValForm: FormGroup;
  public changePasswordForm: FormGroup;
  public response: ResponseVm = new ResponseVm();
  public userAvailabiltyErrorMessage: string;
  public userAvailabilty: boolean = false;
  public usernameErrorMessage: string;
  public emailErrorMessage: string;
  public passwordErrorMessage: string;
  public phonenumberErrorMessage: string;
  public passwordMinLenghtErrorMessage: string;
  public submitted = false;
  public dataSave = false;
  public status: boolean;
  public SecurityRole = [];
  public securityRole:string
  public securityMessege: string;
  public allowdelete;
  public allowedit;
  public allowadd;
  public allowview;
  public isDelete = false;
  public showEditForm = false;
  public adminUserId = "";
  public isValid = true;
  public adminToChangePassword: string;
  public changePassowrdAdminId: string;
  public sprights: boolean = false;
  public isCompare: boolean = true;
  public passwordMismatch: string;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  //@ViewChild('confirmDeleteModel', { static: true }) confirmDeleteModel: ModalDirective;

  constructor(private _modalService: NgbModal ,public service: CommonService, public Loader: NgxSpinnerService, private formBuilder: FormBuilder, public toastr: ToastrService, private router: Router) {

    this.GetSecurityRole();
  }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Create Admin User"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.GetAdminUserDetails();
    this.appValForm = this.formBuilder.group({
      userName: ['', [Validators.required]],
      email: ['', [Validators.required,Validators.minLength(8)]],
      phoneNumber: ['', [Validators.required, Validators.minLength(11), Validators.maxLength(12)]],
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(20)]],
      securityRole: ['', [Validators.required]],
    });
    this.changePasswordForm = this.formBuilder.group({
      //currentPassword: ['', [Validators.required, Validators.maxLength(50)]],
      newPassword: ['', [Validators.required, Validators.maxLength(50), Validators.minLength(8)] ], //Validators.pattern(/^[\w 0-9 ]$/)
      confirmNewPassword: ['', [Validators.required]],
    });
    console.log(this.t);
    this.submitted = false;
  }
  handleChange(event) {
    
    let status = event.target.checked;
    let userId = event.target.id;
    console.log(status);
    console.log(userId);
    this.service.get(this.service.apiRoutes.Users.DeleteAdminUser + "?userId=" + userId).subscribe(result => {
      let response = result.json();
      if (response) {
        if (!status) {
          this.toastr.warning("Admin User InActive!", "Warning");
        }
        else {
          this.toastr.success("Admin User Active!", "Success");
        }
        this.GetAdminUserDetails();
      }
    })
  }

  get f() {
    return this.appValForm.controls;
  }
  public get currentPassword() { return this.changePasswordForm.get('currentPassword'); }
  GetAdminUserDetails() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Users.GetAdminUserDetails + "?roleId=" + this.adminRole).subscribe(result => {
      this.AdminUserVM = result.json();
      console.log(this.AdminUserVM);
    });
    this.Loader.hide();
  }

  public save() {
   
    this.usernameErrorMessage = RegistrationErrorMessages.UserNameErrorMessage;
    this.emailErrorMessage = RegistrationErrorMessages.emailErrorMessage;
    this.phonenumberErrorMessage = RegistrationErrorMessages.phonenumberErrorMessage;
    this.passwordErrorMessage = RegistrationErrorMessages.passwordErrorMessage;
    this.passwordMinLenghtErrorMessage = RegistrationErrorMessages.passwordMinLenghtErrorMessage;
    this.securityMessege = RegistrationErrorMessages.UserSecurityRoleMessage;
    this.submitted = true;

    //if (this.adminvm.id != "") {
    //  this.appValForm.get('password').setValidators(null);cancel
    //  this.appValForm.get('password').setErrors(null);
    //}
    if (this.appValForm.valid) {
      var data = this.appValForm.value;
      if (this.adminvm.id != null) {
        
        this.adminvm.userName = data.userName;
        this.adminvm.email = data.email;
        this.adminvm.phoneNumber = data.phoneNumber;
        this.adminvm.securityRole = data.securityRole;
        
        this.service.PostData(this.service.apiRoutes.Users.UpdateAdminUserdetails, this.adminvm, true).then(result => {
          var response = result.json();
          if (response == true) {
            this.dataSave = true;
            this.GetAdminUserDetails();
            this.hide = true;
            this.showEditForm = false;
          }
        });
        this.toastr.success('Success', 'Record updated successfully!');
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
              
              var response = result;
              if (response.status == httpStatus.Ok) {
                this.submitted = false;
                this.toastr.success("Record saved successfully", "Success");
                this.appValForm.reset();
                this.GetAdminUserDetails();
                this.userAvailabiltyErrorMessage = "";
                this.userAvailabilty = false;
                this.hide = true;
              }
            });
          }
          else {
            setTimeout(() => {
              this.userAvailabilty = false;
            },3000)
            this.userAvailabilty = true;
            this.toastr.error(this.response.resultData[0].description, "ERROR");
            //this.userAvailabiltyErrorMessage = this.response.resultData[0].description;
          }
        });

      }
    }
   
  }
  
  confrimDelete() {
    if (this.adminUserId != "" && this.adminUserId != null) {
      this.Loader.show();
      this.service.get(this.service.apiRoutes.Users.DeleteAdminUser + "?userId=" + this.adminUserId).subscribe(result => {
        var res = result.json();
        this.GetAdminUserDetails();
      });
      this.Loader.hide();
    }
    this.toastr.error('Record deleted successfully!','Delete')
  }
  public delete(adminUserId: string, content?) {
    this.adminUserId = adminUserId;
    this._modalService.open(content);
  }
  public CreateAdmin() {
    this.hide = false;
    this.showEditForm = false;
    this.adminvm = new Adminuservm();
    this.appValForm.setValue({
      userName: '',
      email: '',
      phoneNumber:'',
      password:'',
      securityRole:''
    })
    this.appValForm.controls.password.setValidators([Validators.required, Validators.minLength(8), Validators.maxLength(20)]);
  }

  //public Edit(adminvm) {
  //  
  //  this.adminvm = adminvm;
  //  console.log(adminvm);
  //  this.hide = false;
  //  this.patchedPassword = adminvm.password;
  //  this.appValForm.patchValue(adminvm);
  //  this.appValForm.controls.password.setValue('');
  //}

  public Edit(adminvm) {
    this.adminvm = adminvm;
    this.adminToChangePassword = adminvm.password;
    this.changePassowrdAdminId = adminvm.id;
    this.showEditForm = true;
    this.hide = false;
    this.appValForm.patchValue(adminvm);
    this.appValForm.controls.password.clearValidators();
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
    this.hide = true;
    this.showEditForm = false;
    this.submitted = false;
    this.appValForm.reset();
  }

  // cahnge password modal
  showModel(changePasswordContent) {
    this._modalService.open(changePasswordContent);
  }
  hideModel() {
    this._modalService.dismissAll();
  }
  comparePassword() {
    
    if (this.changePasswordForm.value.newPassword != this.changePasswordForm.value.confirmNewPassword) {
      this.isCompare = false
    }
    else {
      this.isCompare = true;
    }
  }
  changePassword() {
    
    this.comparePassword();
    if (this.isCompare) {
      let formData = this.changePasswordForm.value;
      formData.adminId = this.changePassowrdAdminId;
      this.service.PostData(this.service.apiRoutes.Login.ChangeAdminUserPassword, formData, true).then(result => {
        var res = result.json();
        console.log(res);
        if (res.status == 200) {
          this.toastr.success("Password changed successfully!", "Success");
          this.changePasswordForm.reset();
          this._modalService.dismissAll();
        }
        else if (res.message == "PasswordMismatch") {
          this.passwordMismatch = "Invalid current password!";
        }


      })
      console.log(formData);
    }
  }
  get t() {
    return this.changePasswordForm.controls;
  }
}

