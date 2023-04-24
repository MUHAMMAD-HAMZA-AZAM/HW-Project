import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { CommonService } from '../../shared/HttpClient/_http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { customerDashBoardCountVM } from '../../models/userModels/userModels';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { IApplicationSetting, IGetMarkeetPlaceProducts, IPersonalDetails, ISkillAndSubSkill } from '../../shared/Enums/Interface';
import { NgbCarouselConfig, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BsModalRef, BsModalService, ModalDirective } from 'ngx-bootstrap/modal';
import { MessagingService } from '../../shared/CommonServices/messaging.service';
import { metaTagsService } from '../../shared/CommonServices/seo-dynamictags.service';

declare var $: any;
@Component({
  selector: 'app-default',
  templateUrl: './default.component.html',

})

export class DefaultComponent implements OnInit {
  public activeSkillList: ISkillAndSubSkill[] = [];
  public activeListOfSkills :ISkillAndSubSkill[] =[];
  public populateSupplierCatList: IGetMarkeetPlaceProducts[] = [];
  public sliderLoaded: boolean = false;
  public skillSliderLoaded: boolean = false;
  public loggedUserCustomerId: number = 0;
  public skillId: number = 0;
  public pageSize = 10;
  public pageNumber = 1;
  public settingList: IApplicationSetting[] = [];
    public btnMarketPlace: boolean = false;
    public isUserVerified: boolean = false;
    public token: string | null = "";

  //customOptions: OwlOptions = {
  //  mouseDrag: false,
  //  touchDrag: false,
  //  pullDrag: false,
  //  autoplay: false,
  //  margin: 10,
  //  autoplayHoverPause: true,
  //  loop: true,
  //  navSpeed: 700,
  //  navText: [
  //    "<i class='fa fa-chevron-left'></i>",
  //    "<i class='fa fa-chevron-right'></i>"
  //  ],
  //  dots: false,
  //  responsive: {
  //    0: {
  //      items: 1
  //    },
  //    400: {
  //      items: 2
  //    },
  //    740: {
  //      items: 3
  //    },
  //    940: {
  //      items: 3
  //    }
  //  },
  //  nav: true
  //}
  public role: string="";
  public loggedUserDetails: IPersonalDetails;
  public customerDashBoardCountVM: customerDashBoardCountVM = {} as  customerDashBoardCountVM;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  pauseOnHover = true;
  pauseOnFocus = true;

  //@ViewChild('verifyAccountMessageModal', { static: false }) verifyAccountMessageModal: ModalDirective;
  constructor(public common: CommonService, config: NgbCarouselConfig, public _modalService: NgbModal, private _messagingService: MessagingService, public _metaService: metaTagsService) {
    config.interval = 7000;
    config.wrap = true;
    config.keyboard = true;
    config.pauseOnHover = true;
    config.showNavigationIndicators = false;
    this.loggedUserDetails = {} as IPersonalDetails;


  }

  ngOnInit() {
    
    this.applicationSetting();
    //this.recentMarketPlaceSlider();
    this.token = localStorage.getItem("auth_token");
    if (this.token != null && this.token != '') {
      var decodedtoken = this.jwtHelperService.decodeToken(this.token.replace("Bearer", ""));
      
      this.role = decodedtoken.Role;
      this.loggedUserCustomerId = decodedtoken.id;
    }
    this.getLocalStorageData();
    /* this.verifyAccountMessageModal.show();*/
    this.bindMetaTags();
  }


  ngAfterViewInit() {
    this.populateSkillList();
  }

  public populateSkillList() {   
    this.common.get(this.common.apiRoutes.Tradesman.GetSkillListAdmin).subscribe(result => {
      this.activeSkillList = <ISkillAndSubSkill[]>result;
      this.sliderLoaded = true;
      this.activeListOfSkills = this.activeSkillList.filter(x => x.isActive);
      this.skillSliderLoaded = true;
   
      //this.populateSupplierCategoriesList();
    });
  }
  public populateSupplierCategoriesList() {    
    this.common.get(this.common.apiRoutes.Supplier.GetMarkeetPlaceTopRatedProductsforWeb + `?pageSize=${this.pageSize}`).subscribe(result => {
      this.populateSupplierCatList = <IGetMarkeetPlaceProducts[]>result;
        this.sliderLoaded = true;   
      });
  }
  postJob(skillId: number) {
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.QuoteStep1, { queryParams: { skillId: skillId } });
  }
  public AdDetail(AdId: number) {

    if (this.common.IsUserLogIn()) {
      this.common.NavigateToRoute(this.common.apiUrls.User.AdDetail + AdId);
    }
    else {
      this.common.NavigateToRoute(this.common.apiUrls.User.AdDetail + AdId);
    }
  }
  public getLocalStorageData() {
    var token = localStorage.getItem("auth_token");
    var decodedtoken =token!=null ? this.jwtHelperService.decodeToken(token):"";
    if (decodedtoken) {
      this.getLoggedUserDetails(decodedtoken.Role, decodedtoken.UserId);
      this.getCustomerDashBoardCount(decodedtoken.Id, decodedtoken.UserId);
    }
  }
  public getLoggedUserDetails(userRole: string, userId: string) {

    this.common.get(this.common.apiRoutes.UserManagement.GetUserDetailsByUserRole + `?userRole=${userRole}&userId=${userId}`).subscribe(result => {
      this.loggedUserDetails = <IPersonalDetails>result;
      this._messagingService.requestPermission(this.loggedUserDetails.firebaseClientId);
    });
  }

  public getCustomerDashBoardCount(customerId: number,userId: string) {
    this.common.get(this.common.apiRoutes.Users.CustomerDashboard + `?customerId=${customerId}&userId=${userId}`).subscribe(result => {
      this.customerDashBoardCountVM = <customerDashBoardCountVM>result;
     
    });
  }
  public Blogs(blog: string) {
    if (blog == 'BlogDetails') {
      this.common.NavigateToRoute(this.common.apiUrls.User.Home.BlogDetails)
    }
    else if (blog == 'BlogDetails1') {
      this.common.NavigateToRoute(this.common.apiUrls.User.Home.BlogDetails1)
    }
    else if (blog == 'BlogDetails2') {
      this.common.NavigateToRoute(this.common.apiUrls.User.Home.BlogDetails2)
    }
  }
  //application setting
  public applicationSetting() {
    
    this.common.get(this.common.apiRoutes.UserManagement.GetSettingList).subscribe(result => {
      this.settingList = <IApplicationSetting[]>result;
      if (this.settingList.length > 0) {
        this.settingList.forEach(x => {
          if (x.settingName == "MarketPlace" && x.isActive) {
            this.btnMarketPlace = true;
          }
        });
      }
    });

  }
  public recentMarketPlaceSlider() {
    $("#rmp-posts").owlCarousel({
      items: 3,
      autoplay: true,
      margin: 10,
      autoplayHoverPause: true,
      loop: true,
      nav: true,
      navText: [
        "<i class='fa fa-chevron-left'></i>",
        "<i class='fa fa-chevron-right'></i>"
      ],
      dots: false,
      responsiveClass: true,
      responsive: {
        0: {
          items: 1,
        },
        575: {
          items: 2,
        },
        767: {
          items: 2,
        },
        991: {
          items: 3,
        }
      }

    });
  }


  //------------------- Check User Verify Status
  public checkVarifyAccount(url: string, skillId : number) {

    if (this.token != null) {
      this.common.GetData(this.common.apiRoutes.IdentityServer.GetPhoneNumberVerificationStatus + "?userId=" + this.token, true).then(result => {
        if (result == true && this.skillId == skillId) {
          this.common.NavigateToRoute(url);
        }
        else if (result == true && skillId > 0) {
          this.postJob(skillId);
        }
        else {
          this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.QuoteStep1, { queryParams: { status: this.isUserVerified } });
        }

      });
    }
  }

  public verifyAccount() {
    if (this.role == "Customer") {
      this._modalService.dismissAll();
      this.common.NavigateToRoute(this.common.apiUrls.User.PersonalProfile);
    }
    else {
      this._modalService.dismissAll();
      // this.common.NavigateToRoute(this.common.apiUrls.Tradesman.Profile);
    }
  }

  public bindMetaTags() {
    this._metaService.updateTitle("Hoomwork");
    this._metaService.updateTag({ name: "robots", content: 'index,follow' })
    this._metaService.updateKeyWords("Home Maintenance Services | Home Repair Services | Hoomwork");
    this._metaService.updateDescription("Hoomwork Is Pakistan's Biggest Trusted Marketplace That Connects People With Rated Suppliers And Skilled Electricians, Plumbers, Cleaners And More. .");
  }
}
