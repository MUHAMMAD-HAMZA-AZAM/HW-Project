import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { JwtHelperService } from '@auth0/angular-jwt/src/jwthelper.service';
import { CommonService } from '../../Shared/HttpClient/_http';
import { NgxSpinnerService } from "ngx-spinner";
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { userdelete } from '../../Shared/Models/UserModel/SpTradesmanVM';
import { Router } from '@angular/router';

@Component({
  selector: 'app-delete-user-info',
  templateUrl: './delete-user-info.component.html',
  styleUrls: ['./delete-user-info.component.css']
})
export class DeleteUserInfoComponent implements OnInit {
  public deleteUserForm: FormGroup;
  public userdata: userdelete[] = []
  public emptyFieldError: string;
  public sprights = 'false';
  public selectfordelete: string;
  public dataNotFound: boolean;
  public userIdd: string;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  constructor(public router: Router,public fb: FormBuilder, public toastr: ToastrService, public _modalService: NgbModal, public service: CommonService, public Loader: NgxSpinnerService, ) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Delete User Info"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.deleteUserForm = this.fb.group({
      UserName: ["", [ Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]],
      Phone: ["", [ Validators.pattern("^[0-9]{4}[0-9]{7}$")]],
      UserRole: [null],
      userId: [null],    
    })
    this.deleteUserForm.valueChanges.subscribe(newValue => {
      if (newValue.UserName.length > 0 || newValue.Phone.length > 0) {
        this.deleteUserForm.setErrors(null);
      } else {
        this.deleteUserForm.setErrors({ required: true });
      }
    });
  }
  public deleteUser() {
    //if (this.deleteUserForm.value.UserName == "" && this.deleteUserForm.value.phone == "") {
    //  this.emptyFieldError = "Please enter Username or Phone number!";
    //  setTimeout(() => {
    //    this.emptyFieldError = "";
    //  },6000)
    //  return;
    //}
 //   else {
      //let formData = this.deleteUserForm.value;
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    let formData ={
      userId: this.selectfordelete,
      deletedBy: decodedtoken.UserId
    }
      this.service.post(this.service.apiRoutes.registration.DeleteUserInfo, formData).subscribe(result => {
        var res = result.json();
        console.log(res);
        if (res.message == "UsernameAndPhoneNull") {
          this.toastr.error("Please enter User name or Phone", "Error");
        }
        else if (res.message == null)
        {

        }
        else {
          if (res.message == "No data found against given info") {
            this.toastr.error(res.message, "Error");
          }
          else if (res.message == "User Successfully deleted") {
            this.toastr.success(res.message, "Success");
           // this.getuserData();
            
            this._modalService.dismissAll();
            this.userdata = this.userdata.filter(x => x.id != this.selectfordelete)
            this.selectfordelete = "";
          }
        }

      })
      console.log(this.deleteUserForm.value);
  //  }
  }
  public resetForm() {
    
    this.deleteUserForm.reset();
  }
  get f() {
    return this.deleteUserForm.controls;
  }


  public getuserData() {
    
    this.userdata = [];
    //if (this.deleteUserForm.value.UserName == "" && this.deleteUserForm.value.Phone == "") {
    //  this.emptyFieldError = "Please enter Username or Phone number!";
    //  setTimeout(() => {
    //    this.emptyFieldError = "";
    //  }, 6000)
    //  return;
    //}
   // else {
      let formData = this.deleteUserForm.value;
    this.service.post(this.service.apiRoutes.registration.GetDeleteUserInfo, formData).subscribe(result => {
      if (result.json() != null) {
        this.userdata = result.json();
        console.log(this.userdata);
        this.dataNotFound = true;
      }
      else {
        this.dataNotFound = false;
        this.toastr.error("No Record Found!", "Success");
        this.userdata = [];
      }

      });
   // }
  }
  deleteuserconfirm(id, userid , deleteContent) {
    this.selectfordelete = id;
    this.userIdd = userid;
    this._modalService.open(deleteContent);
  }
}
