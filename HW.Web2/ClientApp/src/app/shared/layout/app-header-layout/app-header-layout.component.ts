import { Component, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { loginsecurity } from '../../Enums/enums';
import { CommonService } from '../../HttpClient/_http';
import { Events } from '../../../common/events';
import { SocialAuthService } from 'angularx-social-login';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';
import { IPersonalDetails, ISkill } from '../../Interface/tradesman';
import { IApplicationSetting } from '../../Enums/Interface';
import { MessagingService } from '../../CommonServices/messaging.service';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-app-header-layout',
  templateUrl: './app-header-layout.component.html',
  styleUrls: ['./app-header-layout.component.css']
})
export class AppHeaderLayoutComponent implements OnInit {
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  public loginCheck: boolean = false;
  public pageNumber: number = 1;
  public loggedUserDetails: IPersonalDetails;
  public routeUrl: string = "";
  public settingList: IApplicationSetting[] = [];
  public btnMarketPlace: boolean = false;
  public skillId: number = 0;
  public activeskillList: any = [];
  public commonSkills: ISkill = {} as ISkill;
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
  constructor(public router: Router, public Loader: NgxSpinnerService, public authService: SocialAuthService, public common: CommonService, private event: Events, @Inject(PLATFORM_ID) private platformId: Object) {
    this.loggedUserDetails = {} as IPersonalDetails;
    if (isPlatformBrowser(this.platformId)) {
      window.scroll(0, 0);
    }
  }

  ngOnInit() {
    //this._messagingService.receiveMessage();
    this.applicationSetting();
    this.getLocalStorageData();
    this.routeUrl = this.router.url;
    this.populateTradesmanSkills();
  }
  public populateTradesmanSkills() {
    this.common.get(this.common.apiRoutes.Tradesman.GetSkillList + "?skillId=" + this.skillId).subscribe(result => {
      this.activeskillList = result;
      this.commonSkills.name = this.activeskillList[0].name;
      this.commonSkills.slug = this.activeskillList[0].slug;
      this.commonSkills.skillId = this.activeskillList[0].skillId;
    });
  }
  public getLocalStorageData() {

    var token = localStorage.getItem("auth_token");

    var decodedtoken = token != null ? this.jwtHelperService.decodeToken(token.replace("Bearer", "")) : "";
    if (decodedtoken) {
      this.getLoggedUserDetails(decodedtoken.Role, decodedtoken.UserId);
    }
  }
  getLoggedUserDetails(userRole: string, userId: string) {
    this.Loader.show();
    this.common.get(this.common.apiRoutes.UserManagement.GetUserDetailsByUserRole + `?userRole=${userRole}&userId=${userId}`).subscribe(result => {

      this.loggedUserDetails = (<IPersonalDetails>result);
      this.Loader.hide();
    });
  }

  public logOut() {
    this.loggedUserDetails = {} as IPersonalDetails;
    localStorage.clear();
    this.authService.signOut();
    this.loginCheck = false;
    this.common.NavigateToRoute('');
    //window.location.href = '';
  }
  public RegisterEntityCheck(role: string, show: string, clickPage: string) {
    localStorage.setItem("Role", role);
    localStorage.setItem("Show", show);
    if (clickPage == "login") {
      if (role == "3") {
        //window.location.href = '/login/Customer';
        this.common.NavigateToRoute('/login/Customer');

      }
      else if (role == "1" || role == "2") {
        //window.location.href = '/login/Tradesman';
        this.common.NavigateToRoute('/login/Tradesman');
      }
    }
  }
  public DashBoard() {
    var token = localStorage.getItem("auth_token");
    var decodedtoken = token != null ? this.jwtHelperService.decodeToken(token.replace("Bearer", "")) : "";
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
  public PostJob() {

    if (this?.common.IsUserLogIn) {
      this.common.NavigateToRoute(this.common.apiUrls.User.Quotations.getQuotes1);
    }
    else {
      localStorage.setItem("Role", '3');
      localStorage.setItem("Show", 'true');
      this.common.NavigateToRoute(this.common.apiUrls.Login.customer);
    }
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
  //public scrollTop(event: Event) {
  //  window.scroll(0, 0);
  //}
  //application setting
  public applicationSetting() {

    this.common.get(this.common.apiRoutes.UserManagement.GetSettingList).subscribe(result => {
      this.settingList = (<IApplicationSetting[]>result);
      if (this.settingList.length > 0) {
        this.settingList.forEach(x => {
          if (x.settingName == "MarketPlace" && x.isActive) {
            this.btnMarketPlace = true;
          }
        });
      }
    });

  }
}
