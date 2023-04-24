import { Component, OnInit, AfterViewInit, Input, SimpleChanges } from '@angular/core';
import { CommonService } from '../../HttpClient/_http';
import { SafeResourceUrl, DomSanitizer } from '@angular/platform-browser';
import { loginsecurity, httpStatus } from '../../Enums/enums';
import { Events } from '../../../common/events';
import { Customer } from '../../../models/userModels/userModels';
import { JwtHelperService } from '@auth0/angular-jwt';
import { SocialAuthService } from 'angularx-social-login';
import { Subscription } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';
import { IApplicationSetting, ISkill } from '../../Interface/tradesman';
import { MessagingService } from '../../CommonServices/messaging.service';

@Component({
  selector: 'app-header2',
  templateUrl: './app-dasboard-header2.component.html',
  styleUrls: ['./app-dasboard-header2.component.css']
})
export class AppDasboardHeader2Component implements OnInit {
  public loginCheck: boolean = false;
  public profile: Customer = {} as Customer;
  public role: string = "";
  public BlogUrl: string = "";
  public userImage: string|null = "";
  public userName: string|null="";
  public credits: string="";
  public userType: string = "";
  public userId: number=0;
  public profileImagecheck = false;
  public settingList: IApplicationSetting[] = [];
  public isActiveCashWithdrawl: boolean = false;
  public btnMarketPlace: boolean = false;
  public pageNumber: number = 1;
  skillDetails: ISkill = {} as ISkill;
  //Input
  @Input() skill: ISkill = {} as ISkill;;
  eventSubscription: Subscription
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
  constructor(private _messagingService : MessagingService ,public router: Router, public Loader: NgxSpinnerService, public common: CommonService, private event: Events, private authService: SocialAuthService) {
   this.eventSubscription =  this.event.pic_Changed.subscribe(res => {
      
      let img = localStorage.getItem("image");
      if (img != null)
        this.userImage = img;
      console.log(this.userImage);
      localStorage.removeItem("image");
   });
    this.refreshCredits();
  }
  ngOnInit() {
    this.getSettingList();
    this.IsUserLogIn();
    var token = localStorage.getItem("auth_token");
    if (token != null && token != '') {
      var decodedtoken = this.jwtHelperService.decodeToken(token.replace("Bearer", ""));
      this.userType = decodedtoken.Role;
      this.userId = decodedtoken.Id;
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
  ngOnChanges(changes: SimpleChanges) {
    this.skillDetails = changes['skill'].currentValue;
  }
  refreshCredits() {
    this.eventSubscription = this.event.update_Trademan_Credits.subscribe(value => {
      setTimeout(() => {
          this.getWalletTradesman(value);
      },3000)
    });
  }
  ngOnDestroy() {
    if (this.eventSubscription) {
      this.eventSubscription.unsubscribe();
    }
  }
  public collapseTopNav() {
    const el = document.getElementById("navbarNavAltMarkup");
    if (el!=null && el.classList.contains("show")) {
      el.classList.remove("show");
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
  public RegisterEntityCheck(role: string, show: string) {
    localStorage.setItem("Role", role);
    localStorage.setItem("Show", show);
    if (role == "3") {
      //window.location.href = '/login/Customer';
      this.common.NavigateToRoute('/login/Customer');
    }
    else if (role == "1" || role == "2") {
      //window.location.href = '/login/Tradesman';
      this.common.NavigateToRoute('/login/Tradesman');
    }
  }
  public PopulateDataUser() {
    
    //if (localStorage.getItem("user_profileImage") == null && isUndefined(localStorage.getItem("user_profileImage"))) {
    this.common.get(this.common.apiRoutes.Users.CustomerProfile).subscribe(result => {
      if (status = httpStatus.Ok) {
        this.profile = <Customer>result ;
        //get wallet amount
       
        localStorage.setItem("user_userName", this.profile.firstName + " " + (this.profile.lastName != null ? this.profile.lastName : "") );
          this.userName = localStorage.getItem("user_userName");
        if (this.profile.profileImage != null && this.profile.profileImage != "") {
          this.profileImagecheck = true;
          this.profile.profileImage = 'data:image/png;base64,' + this.profile.profileImage;
          localStorage.setItem("user_profileImage", this.profile.profileImage);
          this.userImage = localStorage.getItem("user_profileImage");
        }
      }
      else {
        this.userImage = localStorage.getItem("user_profileImage");
        console.log(this.userImage);

        this.userName = localStorage.getItem("user_userName");
      }
      this.getWalletUser();
    });
    //}
  }
    getWalletUser() {
      this.common.GetData(this.common.apiRoutes.PackagesAndPayments.GetLedgerTransactionCustomer + "?refercustomerId=" + this.profile.entityId, true).then(result => {
        if (status = httpStatus.Ok) {         
          var data = result ;
          this.credits = data.resultData;
        }
      });
    }
  public PopulateDataSupplier() {
    
    //if (localStorage.getItem("user_profileImage") == null ) {
    this.common.get(this.common.apiRoutes.Supplier.GetPersonalInformation).subscribe(result => {
      if (status = httpStatus.Ok) {
        var data = <any>result ;
          localStorage.setItem("user_userName", data.firstName + " " + data.lastName);
          this.userName = localStorage.getItem("user_userName");
        if (data.profileImage != null) {
          this.profileImagecheck = true;
          //this.profile.profileImage = 'data:image/png;base64,' + data.profileImage;
          localStorage.setItem("user_profileImage", 'data:image/png;base64,' + data.profileImage);
          this.userImage = localStorage.getItem("user_profileImage");
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
        //get wallet amount
        this.getWalletTradesman(data.entityId);

          localStorage.setItem("user_userName", data.firstName + " " + data.lastName);
          this.userName = localStorage.getItem("user_userName");
        if (data.profileImage != null) {
          this.profileImagecheck = true;
          localStorage.setItem("user_profileImage", 'data:image/png;base64,' + data.profileImage);
          this.userImage = localStorage.getItem("user_profileImage");
        }
      }
      else {
        this.userImage = localStorage.getItem("user_profileImage");
        this.userName = localStorage.getItem("user_userName");
      }
    });
  }
  getWalletTradesman(entityId: number) {
    
    this.common.GetData(this.common.apiRoutes.PackagesAndPayments.GetLedgerTransaction + "?reftradesmanId=" + entityId, true).then(result => {
        if (status = httpStatus.Ok) {
          
          var data = result ;
          this.credits = data.resultData;
        }
      });
    }
  public logout() {
    localStorage.clear();
    this.authService.signOut();
    this.loginCheck = false;
    //window.location.href = '';
    this.common.NavigateToRoute('');

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

  public getSettingList() {
    this.common.get(this.common.apiRoutes.UserManagement.GetSettingList).subscribe(res => {
      this.settingList = <IApplicationSetting[]>res ;
      if (this.settingList.length > 0) {
        this.settingList.forEach(x => {
          if (x.settingName == "Cash Withdrawl" && x.isActive) {
            this.isActiveCashWithdrawl = true;
          }
        })

        this.settingList.forEach(x => {
          if (x.settingName == "MarketPlace" && x.isActive) {
            this.btnMarketPlace = true;
          }
        });
      }
    })
  }

  }
  


