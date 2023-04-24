import { Component, OnInit, ViewChild, ElementRef, Inject, ViewEncapsulation } from '@angular/core';
import { CommonService } from '../shared/HttpClient/_http';
import { Router, ActivatedRoute, NavigationBehaviorOptions } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormControl, FormGroup, FormBuilder } from '@angular/forms';
//import * as $ from 'jquery';
import { DOCUMENT } from '@angular/common';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { businessDetails } from '../models/supplierModels/supplierModels';
//import { WOW } from 'wowjs/dist/wow.min';
import { JwtHelperService } from '@auth0/angular-jwt';
import { IDropdownSettings, IIdValue, IProductCategory, ISkill } from '../shared/Interface/tradesman';
import { IPageSeoVM } from '../shared/Enums/Interface';
import { metaTagsService } from '../shared/CommonServices/seo-dynamictags.service';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { Events } from '../common/events';
declare var $: any;

// import Swiper core and required modules
import SwiperCore, { Navigation, Pagination, SwiperOptions } from "swiper";
SwiperCore.use([Pagination, Navigation]);
@Component({
  selector: 'app-landing-page2',
  templateUrl: './landing-page2.component.html',
  styleUrls: ['./landing-page2.component.css'],
})
export class LandingPage2Component implements OnInit {
  swiperConfig: SwiperOptions = {
    //pagination: { clickable: true },
    navigation: true,
    spaceBetween: 10,
    breakpoints: {
      300: {
        slidesPerView: 1
      },
      577: {
        slidesPerView: 2
      },
      992: {
        slidesPerView: 3
      },
    },
    loop: true,
    slidesPerGroup:3,
  };
  public loginCheck: boolean = false;
  public subCategory: any;
  public Hidden = false;
  public hideSelect = false;
  //public selectedItems = [];
  public role: string|null="";
  public skillId: number = 0;
  //public skillList = [];
  //public supplierskillList = [];
  public activeskillList: any = [];
  public customActiveskillList: ISkill[] = [];
  public commonSkills: ISkill[] = [];
  public profile: businessDetails = new businessDetails();
  //public SelectedSubCategoriesList = [];
  public productList: IProductCategory[] = [];
  public SelectedSkillsList: IIdValue[] = [];
  public flag: boolean = false;
  public skill: IIdValue;
  //public searcedSkills = [];
  //public ProductImages = [];
  //public skillImages = [];
  //public supplierInfo = [];
  //public supplierImages = [];
  public showskillImages: boolean = false;
  public showproductImages: boolean = false;
  //public supplierProfileSlider = [];
  //public supplierProfileSlider1 = [];
  public town: string="";
  public appValForm: FormGroup;
  public skillsdropdownSettings: IDropdownSettings;
  public myControl = new FormControl();
  //public imagesUri;
  public imageObject_tradecat: Array<object>;
  public imageObject_market: Array<object>;
  public imageObject_category: Array<object>;
  public videoObject: Array<object>;
  public profileImage: any;
  public currentIndex: number = 0;
  public decodedtoken: any;
  public pageUrl: string | null = "";
  public videoUrl: string = "";
  public isShowModal = false;
  //public ipAddress: any;
  //public currentList: any = [];
  //public currentList2: any = [];
  //public list1: any = [];
  //public list2: any = [];
  //public list3: any = [];
  //public mainServices: any = [];
  public safeURL: SafeResourceUrl;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  @ViewChild('blockuserModal', { static: true }) blockuserModal: ModalDirective;
  @ViewChild('hideSearch', { static: true }) hideSearch: ElementRef;
  @ViewChild('content', { static: true }) content: ElementRef;
  @ViewChild('youtubeVideoModal', { static: true }) youtubeVideoModal: ModalDirective;
  constructor(@Inject(DOCUMENT) private document: Document, public Loader: NgxSpinnerService,
    public common: CommonService, private router: Router, private aroute: ActivatedRoute, private modalService: NgbModal, config: NgbModalConfig, private formBuilder: FormBuilder
    , private _metaService: metaTagsService, protected sanitizer: DomSanitizer, private event: Events) {
    this.imageObject_tradecat = {} as Array<object>;
    this.imageObject_market = {} as Array<object>;
    this.imageObject_category = {} as Array<object>;
    this.videoObject = {} as Array<object>;
    this.skill = {} as IIdValue;
    this.blockuserModal = {} as ModalDirective;
    this.youtubeVideoModal = {} as ModalDirective;
    this.hideSearch={} as ElementRef;
    this.content = {} as ElementRef;
    this.appValForm = {} as FormGroup;
    this.skillsdropdownSettings = {} as IDropdownSettings;
    this.safeURL = {} as SafeResourceUrl;
    this.appValForm = this.formBuilder.group({
      primaryTrade: [''],
      companyName: [''],
      registrationNumber: [''],
      deliveryRadius: [''],
      cityName: [''],
      businessAddress: [''],
      selectedSubCategory: [''],
    });
  }

  ngOnInit() {
    this.testimonailsSlider();
    if (this.common.IsUserLogIn()) {
      this.role = localStorage.getItem("Role");

      $(document).onkeypress = function (evt: KeyboardEvent) {
        if (evt.keyCode == 27) {
          alert("Esc was pressed");
        }
      };
    }
    this.skillsdropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };
    this.SelectedSkillsList = [];

    this.populateTradesmanSkills();
    //this.populateProducts();
   this.bindMetaTags();

    
  }
  public bindMetaTags() {
    this.common.get(this.common.apiRoutes.CMS.GetSeoPageById + "?pageId=14").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description,res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }
  ngAfterViewInit() {
    //new WOW().init();
  }
  get f() {
    return this.appValForm.controls;
  }
  onSlideRangeChange() {
    alert();
  }
  hideSkillSearch(e: Event) {
    this.flag = false;
  }
  public keyPress(event: any) {

    if (event.length > 0) {
      this.common.get(this.common.apiRoutes.Home.GetAllSubcategory + "?search=" + event).subscribe(data => {
        var result = <any>data ;
        if (result.length > 0) {
          this.subCategory = data ;
          this.hideSelect = true;
          this.Hidden = true;
        }
        else this.Hidden = false;
      });
    }
    else {
      this.hideSelect = false;
    }
  }

  public onselectClient(client: IIdValue) {
    this.skill.id = client.id;
    this.skill.value = client.value;
    this.flag = false;
    
    
  }
  // test 
  //public search() {
  //  this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Tradesman.Searchtradesmanbyskill, { queryParams: { 'skill': this.skill.id, 'town': this.town } });
  //}

  //public searchClient(skill, eve) {
  //  this.searcedSkills = [];
  //  this.skillList.forEach(value => {
  //    if (value.value.toLowerCase().includes(skill.value.toLowerCase())) {
  //      let skilltoAdd = { "id": value.id, "value": value.value };
  //      this.searcedSkills.push(skilltoAdd);
  //    }
  //  });
  //  if (this.searcedSkills != null && this.searcedSkills.length > 0) {
  //    this.flag = true;
  //  }
  //  else {
  //    this.flag = false;
  //  }
  //}

  //public populateSkills() {
  //  this.common.get(this.common.apiRoutes.Tradesman.GetTradesmanSkills).subscribe(result => {
  //    this.skillList = result ;
  //    localStorage.setItem("skills", JSON.stringify(this.productList));
  //    this.skillList.forEach(value => {
  //      let skill = { "id": value.id, "value": value.value };
  //      this.searcedSkills.push(skill);
  //    });
  //  },
  //    error => {
  //      console.log(error);
  //    });
  //}

  public populateProducts() {
    
    this.common.get(this.common.apiRoutes.Supplier.GetActiveProducts + "?productCategoryId=" + 0).subscribe(result => {
      
      this.productList = <IProductCategory[]>result ;
     // this.loadNextProducts(0);
    });
  }

  //public loadNextProducts(index) {
  //  if (!(index == -1 && this.currentIndex == 0)) {
  //    this.currentIndex = this.currentIndex + index;
  //    var startIndex = (this.currentIndex * 8)
  //    var endIndex = ((this.currentIndex + 1) * 8)
  //    this.currentList2 = this.productList.slice(startIndex, endIndex);
  //    if (this.currentList2.length <= 0) {
  //      this.currentList2 = this.productList.slice(0, 8);
  //      this.currentIndex = 0;
  //    }
  //  }
  //}


  public populateTradesmanSkills() {
     
    this.common.get(this.common.apiRoutes.Tradesman.GetSkillList + "?skillId=" + this.skillId).subscribe(result => {     
      this.activeskillList = result;
      this.event.skills_obj.emit(this.activeskillList[0]);
      this.customActiveskillList = this.activeskillList.slice(0, 9);
      this.commonSkills = this.activeskillList.slice(0, 16);
    });
  }

  //public loadNext(index) {
  //  if (!(index == -1 && this.currentIndex == 0)) {
  //    this.currentIndex = this.currentIndex + index;
  //    var startIndex = (this.currentIndex * 8)
  //    var endIndex = ((this.currentIndex + 1) * 8)
  //    this.currentList = this.activeskillList.slice(startIndex, endIndex);
  //    if (this.currentList.length <= 0) {
  //      this.currentList = this.activeskillList.slice(0, 8);
  //      this.currentIndex = 0;
  //    }
  //  }
  //}

  public selectSkill(skillId: number) {
    
    var aa = skillId;

  }

  public selectProducts(productId: number) {
    if (this.common.loginCheck) {
      this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Supplier.EditAd, { queryParams: { 'CategoryId': productId } });
    }
    else {
      localStorage.setItem("Role", '4');
      localStorage.setItem("Show", 'true');
      this.common.NavigateToRoute(this.common.apiUrls.Login.supplier);
    }


  }

  public PostJob(obj: number) {

    if (this.common.loginCheck) {
      this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.Quotations.getQuotes1, { queryParams: { 'id': obj } });
    }
    else {
      localStorage.setItem("Role", '3');
      localStorage.setItem("Show", 'true');
      this.common.NavigateToRoute(this.common.apiUrls.Login.customer);
    }
  }

  public openSupLogin() {
    if (this.common.loginCheck) {
      this.common.NavigateToRoute(this.common.apiUrls.Supplier.Home);
    }
    else {
      localStorage.setItem("Role", '4');
      localStorage.setItem("Show", 'true');
      this.common.NavigateToRoute(this.common.apiUrls.Login.supplier);
    }

  }

  searchSupplierCategory(item: ISkill) {
    let obj = { id: item.name, value: item.productCategoryId }
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Supplier.MarkeetPlace, { queryParams: { 'value': item.name, 'id': item.productCategoryId }});
  }

  selectedSubCategory(subCategoryId: number) {
    
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Supplier.MarkeetPlace, { queryParams: { 'subCatId': subCategoryId } });
  }

  public openTradesmanLogin() {
    debugger;
    var token = localStorage.getItem("auth_token"); 
    this.decodedtoken = token != null ? this.jwtHelperService.decodeToken(token) : "";
    console.log(this.decodedtoken.Role)
    if (this.common.loginCheck && this.decodedtoken.Role == 'Tradesman' || this.decodedtoken.Role =='Organization') {
      this.common.NavigateToRoute(this.common.apiUrls.Tradesman.LiveLeads);
    }
    else {
      localStorage.setItem("Role", '1');
      localStorage.setItem("Show", 'true');
      this.common.NavigateToRoute(this.common.apiUrls.Login.tradesman);
    }
  }

  public ImageClick(obj2: number) {
    this.PostJob(obj2);
  }
  public OpenMarkeetplace(obj: NavigationBehaviorOptions) {
    this.router.navigateByUrl('/MarketPlace/MarketPlaceIndex/ProductCategoryHome/' + obj + '');
  }
  public Reset() {
    this.town = '';
    this.skill.value = '';

  }
  
  public hide() {
    this.blockuserModal.hide();
  }

  //GetIpAddress() {

  //  this.common.get('https://jsonip.com')
  //    .subscribe(data => {
  //      console.log('th data', data);

  //      this.ipAddress = data
  //    });

  //}
  public testimonailsSlider() {
    //$(".tm-slider-wrapper").owlCarousel({
    //  loop: true,
    //  autoplay: true,
    //  autoplayHoverPause: true,
    //  autoplaySpeed: 2000,
    //  items: 1,
    //});
  }
  public openVideoModal(videoLink: string) {
    this.isShowModal = true;
    this.safeURL = 'https://www.youtube.com/embed/' + videoLink;
    this.youtubeVideoModal.show();
  }
  public hideVideoModal() {
    this.isShowModal = false;
    this.youtubeVideoModal.hide();
  }
}

