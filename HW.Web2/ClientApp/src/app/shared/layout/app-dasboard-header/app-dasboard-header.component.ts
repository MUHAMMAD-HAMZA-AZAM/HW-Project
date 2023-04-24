import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../HttpClient/_http';
import { Events } from '../../../common/events';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Customer } from '../../../models/userModels/userModels';
import { loginsecurity, httpStatus } from '../../Enums/enums';
@Component({
  selector: 'app-header',
  templateUrl: './app-dasboard-header.component.html',
  styleUrls: ['./app-dasboard-header.component.css']
})
export class AppDasboardHeaderComponent implements OnInit {
  public loginCheck: boolean = false;
  public role: string|null="";
  public profile: Customer = {} as  Customer;
  public BlogUrl: string = "";
  public userImage: string | null = "";
  public userName: string|null="";
  public profileImagecheck = false;
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

  constructor(public common: CommonService, private event: Events) {
    //this.event.pic_Changed.subscribe(res => {
    //  
    //  let img = localStorage.getItem("image");
    //  if (img != null)
    //    this.userImage = img;
    //  localStorage.removeItem("image");
    //});
  }

  ngOnInit() {
    if (this.common.IsUserLogIn()) {
      this.role = localStorage.getItem("Role");
    }
    this.IsUserLogIn();
    var token = localStorage.getItem("auth_token");
    if (token != null && token != '') {
      var decodedtoken = this.jwtHelperService.decodeToken(token.replace("Bearer", ""));
      switch (decodedtoken.Role) {
        case 'Supplier': this.BlogUrl = this.common.apiUrls.Supplier.Blogs;
          break;
        case 'Customer': this.BlogUrl = "/" + this.common.apiUrls.User.Home.Blogs;
          break;
        default: break;
      }
      if (decodedtoken.Role == loginsecurity.CRole) {
        this.PopulateDataUser();
      }
      else if (decodedtoken.Role == loginsecurity.SRole) {
        this.PopulateDataSupplier();
      }
      else if (decodedtoken.Role == loginsecurity.TRole || decodedtoken.Role == loginsecurity.ORole) {
        this.PopulateDataTradesmanOrg();
      }
      else {
        this.userImage = localStorage.getItem("user_profileImage");
        this.userName = localStorage.getItem("user_userName");
      }
    }


  }
  public PopulateDataUser() {
    //if (localStorage.getItem("user_profileImage") == null && isUndefined(localStorage.getItem("user_profileImage"))) {
    this.common.get(this.common.apiRoutes.Users.CustomerProfile).subscribe(result => {
      if (status = httpStatus.Ok) {
        this.profile = <Customer>result;
        if (this.profile.profileImage != null) {
          this.profileImagecheck = true;
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
      if (status = httpStatus.Ok) {
        var data = <any>result ;
        if (data.profileImage != null) {
          this.profileImagecheck = true;
          //this.profile.profileImage = 'data:image/png;base64,' + data.profileImage;
          localStorage.setItem("user_profileImage", 'data:image/png;base64,' + data.profileImage);
          localStorage.setItem("user_userName", data.firstName + " " + data.lastName);
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
  public PopulateDataTradesmanOrg() {
    this.common.get(this.common.apiRoutes.Tradesman.GetPersonalDetails).subscribe(result => {
      if (status = httpStatus.Ok) {
        var data = <any>result ;
        if (data.profileImage != null) {
          this.profileImagecheck = true;
          localStorage.setItem("user_profileImage", 'data:image/png;base64,' + data.profileImage);
          localStorage.setItem("user_userName", data.firstName + " " + data.lastName);
          this.userImage = localStorage.getItem("user_profileImage");
          this.userName = localStorage.getItem("user_userName");
        }
      }
      else {
        this.userImage = localStorage.getItem("user_profileImage");
        this.userName = localStorage.getItem("user_userName");
      }
    });
  }
  public IsUserLogIn() {
    var token = localStorage.getItem("auth_token");
    if (token != null && token != '') {
      this.loginCheck = true;
    }
    else {
      this.loginCheck = false;
    }
  }

  public PostJob() {
    
    if (this.common.loginCheck) {
      this.common.NavigateToRoute(this.common.apiUrls.User.Quotations.getQuotes1);
    }
    else {
      localStorage.setItem("Role", '3');
      localStorage.setItem("Show", 'true');
      this.common.NavigateToRoute(this.common.apiUrls.Login.customer);
    }
  }
  public RegisterEntityCheck(role: number, show: boolean) {
    localStorage.setItem("Role", role.toString());
    localStorage.setItem("Show", show.toString());
  }

  public DashBoard() {
    var token = localStorage.getItem("auth_token");
    var decodedtoken = token!=null ? this.jwtHelperService.decodeToken(token.replace("Bearer", "")):"";
    if (decodedtoken.Role == loginsecurity.CRole) {
      this.common.NavigateToRoute(this.common.apiUrls.User.UserDefault)
    }
    else if (decodedtoken.Role == loginsecurity.SRole) {
      this.common.NavigateToRoute(this.common.apiUrls.Supplier.Home)
    }
    else if (decodedtoken.Role == loginsecurity.TRole || decodedtoken.Role == loginsecurity.ORole) {
      this.common.NavigateToRoute(this.common.apiUrls.Tradesman.LiveLeads)
    }
  }

  public logout() {
    localStorage.clear();
    this.loginCheck = false;
    window.location.href = '';

  }
}
