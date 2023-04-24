import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../HttpClient/_http';
import { httpStatus } from '../../Enums/enums';
import { Customer } from '../../../models/userModels/userModels';
import { JwtHelperService } from '@auth0/angular-jwt/src/jwthelper.service';
import { loginsecurity } from '../../../shared/Enums/enums';
@Component({
  selector: 'app-header',
  templateUrl: './app-dasboard-header.component.html',
  styleUrls: ['./app-dasboard-header.component.css']
})
export class AppDasboardHeaderComponent implements OnInit {
  public loginCheck: boolean = false;
  public profile: Customer = new Customer();
  public role;
  public userImage;
  public userName;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  public check: {
    T: { role: number, Show: boolean },
    O: { role: number, Show: boolean },
    U: { role: number, Show: boolean },
    S: { role: number, Show: boolean }
  } = {
      T: { role: 1, Show: true },
      O: { role: 2, Show: true },
      U: { role: 3, Show: true },
      S: { role: 4, Show: true }
    };
  constructor(public common: CommonService) { }

  ngOnInit() {
    this.IsUserLogIn();

    debugger;
    var token = localStorage.getItem("auth_token");
    if (token != null && token != '') {
      var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token").replace("Bearer", ""));
      //var image = localStorage.getItem("user_profileImage")
      if (decodedtoken.Role == loginsecurity.CRole && localStorage.getItem("user_profileImage") == null) {
        this.PopulateDataUser();
      }
      else if (decodedtoken.Role == loginsecurity.SRole && localStorage.getItem("user_profileImage") == null) {
        this.PopulateDataSupplier();
      }
      else {
        this.userImage = localStorage.getItem("user_profileImage");
        this.userName = localStorage.getItem("user_userName");
      }
    }

  }

  public IsUserLogIn() {
    debugger
    var token = localStorage.getItem("auth_token");
    if (token != null && token != '') {
      this.loginCheck = true;
    }
    else {
      this.loginCheck = false;
    }
  }




  public RegisterEntityCheck(role, show) {
    localStorage.setItem("Role", role);
    localStorage.setItem("Show", show);
  }


  public PopulateDataUser() {
    //if (localStorage.getItem("user_profileImage") == null && isUndefined(localStorage.getItem("user_profileImage"))) {
    this.common.get(this.common.apiRoutes.Users.CustomerProfile).subscribe(result => {
      debugger;
      if (status = httpStatus.Ok) {
        this.profile = result.json();
        if (this.profile.profileImage != null) {
          this.profile.profileImage = 'data:image/png;base64,' + this.profile.profileImage;
          localStorage.setItem("user_profileImage", this.profile.profileImage);
          localStorage.setItem("user_userName", this.profile.firstName + " " + this.profile.lastName);
          this.userImage = localStorage.getItem("user_profileImage");
          this.userName = localStorage.getItem("user_userName");
        }
      }
      else {
        this.userImage = localStorage.getItem("user_profileImage");
        this.userName = localStorage.getItem("user_userName");
      }
    });
    //}
  }
  public PopulateDataSupplier() {
    //if (localStorage.getItem("user_profileImage") == null ) {
    this.common.get(this.common.apiRoutes.Supplier.GetPersonalInformation).subscribe(result => {
      debugger;
      if (status = httpStatus.Ok) {
        var data = result.json();
        if (data != null) {
          //this.profile.profileImage = 'data:image/png;base64,' + data.profileImage;
          localStorage.setItem("user_profileImage", 'data:image/png;base64,' + data.profileImage);
          localStorage.setItem("user_userName", data.firstName + " " + data.lastName);
          this.userImage = localStorage.getItem("user_profileImage");
          this.userName = localStorage.getItem("user_userName");
        }
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    });
    //}
  }
  public logout() {
    debugger
    localStorage.clear();
    this.loginCheck = false;
    this.common.NavigateToRoute("/Index");
  }

  public DashBoard() {
    debugger;
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token").replace("Bearer", ""));
    if (decodedtoken.Role == loginsecurity.CRole)
      this.role = decodedtoken.Role;
    this.common.NavigateToRoute(this.common.apiUrls.User.UserDefault)
    if (decodedtoken.Role == loginsecurity.SRole)
      this.common.NavigateToRoute(this.common.apiUrls.Supplier.Home)
  }
}
