import { DatePipe } from '@angular/common';
import { HttpClient, HttpStatusCode } from '@angular/common/http';
import { Component, ElementRef, OnInit, Pipe, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Http } from '@angular/http';
import { ActivatedRoute, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { isInteger } from '@ng-bootstrap/ng-bootstrap/util/util';
import { timeStamp } from 'console';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { BasicRegistration, CheckEmailandPhoneNumberAvailability, ResponseVm } from 'src/app/models/commonModels/commonModels';
import { CampaignTypes, CampaignTypesName, httpStatus, loginsecurity } from 'src/app/shared/Enums/enums';
import { metaTagsService } from '../../shared/CommonServices/seo-dynamictags.service';
import { IActivePromotion, IIdValue, IPageSeoVM, ISmsVM } from '../../shared/Enums/Interface';
import { CommonService } from '../../shared/HttpClient/_http';

@Component({
  selector: 'app-promotion-details',
  templateUrl: './promotion-details.component.html',
  styleUrls: ['./promotion-details.component.css']
})
export class PromotionDetailsComponent implements OnInit {

  public promotionId: any;
  public isMessageModal: any;
  public userRoleId: any;
  public pipe: DatePipe;
  public date: Date = new Date;
  public day: string="";
  public month: string="";
  public year: string="";
  public maxdate1: string="";
  public submitted: boolean = false;
  public appValForm: FormGroup;
  private facebookId: string="";
  private googleId: string="";
  public subSkillsNames: any[] = [];
  public selectedSkillName: string = "";
  public selectdSubSkillName: string | null = "" ;
  public loginCheck: boolean = false;
  public openModal: boolean = false;
  public campaignType: string = '';
  public campaignTypeId: number = 0;
  public campaignName: string | null = '';
  public showCategories: boolean = false;
  public campaignTypeName = CampaignTypes;
  public response: ResponseVm = {} as ResponseVm;
  public subAccountResponse: ResponseVm = {} as ResponseVm;
  public smsUsers: ISmsVM = {} as ISmsVM;
  public promoDetails = {} as IActivePromotion;
  public activePromotionList: IActivePromotion[] = [];
  public campaignList: IActivePromotion[] = [];
  public activeCampaignList: IActivePromotion[] = [];
  public listOfSkills: IIdValue[] = [];
  public listOfSubSkills: IIdValue[] = [];
  public listOfSubCategories: IIdValue[] = [];
  public basicRegistrationVm: BasicRegistration = {} as BasicRegistration;
  public checkEmailEvailabilityVm: CheckEmailandPhoneNumberAvailability = {} as CheckEmailandPhoneNumberAvailability;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  @ViewChild('bookNowMessageModal', { static: false }) bookNowMessageModal: ModalDirective;

  @ViewChild("promoContent", { static: true }) promoContent: ElementRef;

  constructor(public common: CommonService,
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router,
    private route: ActivatedRoute,
    public modalService: NgbModal,
    public Loader: NgxSpinnerService, private _metaService: metaTagsService) {
    this.appValForm = {} as FormGroup;
    this.pipe = {} as DatePipe;
    this.bookNowMessageModal = {} as ModalDirective;
    this.promoContent = {} as ElementRef;
  }
  ngOnInit(): void {
    this.GetSkills();
    this.route.queryParams.subscribe(params => {
      this.userRoleId = params['rId'];
      this.promotionId = params['pId'];
      this.isMessageModal = params['isMessageModal'];
      this.campaignType = params['cTId'];
      if (this.isMessageModal == 1) {
        this.openModal = true;
        this.modalService.open(this.promoContent);
      }

    });
    this.pipe = new DatePipe('en-US');
    this.date = new Date();
    this.month = (this.date.getMonth() + 1).toString();
    if (this.date.getMonth() == 12) {
      this.month = '01';
    }

    this.day = this.date.getDate().toString();
    this.year = this.date.getFullYear().toString();

    if (this.month.length == 1) {
      this.month = '0' + this.month;
    }
    if (this.day.length == 1) {
      this.day = '0' + this.day;
    }


    this.maxdate1 = this.year + "-" + this.month + "-" + this.day;
    let currentDate = new Date();
    //------------ Post Job From
    this.appValForm = this.fb.group({
      skillId: [null],
      subSkillId: [null],
      name:['',[Validators.required]],
      phoneNumber: ['', [Validators.required, Validators.pattern("^[0-9]{4}[0-9]{7}$")]],
      budget:[''],
      startedDate:[null],
      role:[loginsecurity.CRole],
      termsAndConditions:[true],
      password:['P@ss'],
      tab1: ['1'],
      tab2: ['1'],
      tab3: ['1'],
      tab4: ['1'],
      tab5: ['1'],
    });
    if (this.userRoleId > 0 && this.promotionId > 0 && this.campaignType) {
      this.bindMetaTags();
      this.getCampaignsList(this.campaignType);
    }
    else if (this.userRoleId > 0 && this.promotionId > 0) {
      this.getPromotionList();
      this.bindMetaTags();
    }
    else {

    }
    console.log(this.showCategories);
  }
  public GetSkills() {

    this.common.get(this.common.apiRoutes.Jobs.GetTradesmanSkillsByParentId + "?parentId=0").subscribe(result => {
      this.listOfSkills = <IIdValue[]>result;
    },
      error => {
        console.log(error);
      })
  }
   get f(){
    return this.appValForm.controls;
  }

      //------------- Post A Task By FaceBook Lead
  public postJob(){
    if(this.appValForm.invalid){
      this.appValForm.markAllAsTouched();
      return;
    }
    else{
      let data = this.appValForm.value;      
      //----------- Check User Already Exist or Not 
      this.checkEmailEvailabilityVm.email = this.basicRegistrationVm?.emailAddress ? this.basicRegistrationVm.emailAddress : '';
      this.checkEmailEvailabilityVm.phoneNumber = data?.phoneNumber ? data.phoneNumber: '';
      this.checkEmailEvailabilityVm.role = data.role;
      this.checkEmailEvailabilityVm.googleUserId = this.basicRegistrationVm?.googleUserId ? this.basicRegistrationVm?.googleUserId : '';
      this.checkEmailEvailabilityVm.facebookUserId = this.basicRegistrationVm?.facebookUserId ? this.basicRegistrationVm.facebookUserId : '';
      this.checkEmailEvailabilityVm.password = "P@ss" + data.tab1 + data.tab2 + data.tab3 + data.tab4 + data.tab5;
      console.log(this.checkEmailEvailabilityVm);

      //----------- If User Available for Registration 
      this.basicRegistrationVm.firstName = data.name.split(' ').slice(0, -1).join(' ');
      this.basicRegistrationVm.lastName = data.name.split(' ').slice(-1).join(' ');
      this.basicRegistrationVm.phoneNumber = data?.phoneNumber ? data.phoneNumber : '';
      this.basicRegistrationVm.role = data.role;
      this.basicRegistrationVm.password = "P@ss" + data.tab1 + data.tab2 + data.tab3 + data.tab4 + data.tab5;
      this.basicRegistrationVm.facebookUserId = this.basicRegistrationVm?.googleUserId ? this.basicRegistrationVm?.googleUserId : '';
      this.basicRegistrationVm.googleUserId = this.basicRegistrationVm?.facebookUserId ? this.basicRegistrationVm.facebookUserId : '';
      this.basicRegistrationVm.city = "64";

      console.log(this.basicRegistrationVm);

      //----------- Job Post By FaceBook Lead
      let formData = {
        fullName: data.name,  
        phoneNumber: data.phoneNumber,
        skillId: data.skillId,
        subSkillId: data?.subSkillId ? data.subSkillId:0,
        budget: data?.budget ? data.budget :0 ,
        startedDate: data?.startedDate ? data.startedDate:''
      };

      let bitrixObj= {
        TITLE:'Facebook Lead',
        name:data.name,
        Skill:this.selectedSkillName,
        SUBSKILL:"",
        PhoneNumber : Number(formData.phoneNumber),
        budegt:formData.budget
      };

      //let getSubSkillName = this.listOfSubSkills.filter(item => item.id == formData.subSkillId);
      //this.selectdSubSkillName = getSubSkillName[0].value;      
      console.log(formData);
      this.common.PostData(this.common.apiRoutes.registration.CheckEmailandPhoneNumberAvailability, this.checkEmailEvailabilityVm, true).then(result => {       
        this.response = result ;  
        if (this.response.status == httpStatus.Ok) {
          //----------- Register a new user for Job Post By Facebook Lead 
          this.common.PostData(this.common.apiRoutes.registration.verifyOtpAndRegisterUser, this.basicRegistrationVm, true).then(result => {         
            this.response = result;
            console.log(this.response);
            if (this.response.status == httpStatus.Ok) {
              this.basicRegistrationVm.emailOrPhoneNumber = this.basicRegistrationVm.phoneNumber.toString();
              this.common.PostData(this.common.apiRoutes.Login.Login, this.basicRegistrationVm, true).then(result => {   
                this.response = result;
                console.log(this.response);
                localStorage.setItem("auth_token", "Bearer " + this.response.resultData);   
                this.common.PostData(this.common.apiRoutes.PackagesAndPayments.AddSubAccount, this.response.resultData, true).then(result => {

                  this.subAccountResponse = result ;
                });
                
                if (this.response.status == httpStatus.Ok) {

                  //------------- Save data for Job Post by Facebook Lead

                  this.common.PostData(this.common.apiRoutes.Jobs.JobPostByFacebookLeads, JSON.stringify(formData)).then(res => {
                    this.response = res;
                    console.log(this.response);
                    if (this.response.status == httpStatus.Ok) {
                      //----------- Save Data on bitrix24 for Job Post by Facebook Lead
                      let url = 'https://hoomwork.bitrix24.com/rest/32/7t94rhu9k3d999ry/crm.lead.add';
                      this.http.post(url,
                        {
                          fields:
                          { 
                            "TITLE": "Need "+this.selectedSkillName,  
                              "NAME": bitrixObj.name, 
                              "SECOND_NAME": "A.", 
                              "LAST_NAME": "Nibot", 
                              "STATUS_ID": "NEW", 
                              "OPENED": "Y", 
                              "ASSIGNED_BY_ID": 1, 
                              "CURRENCY_ID": "USD", 
                              "OPPORTUNITY": bitrixObj.budegt,
                              "PHONE": [ { "VALUE": bitrixObj.PhoneNumber, "VALUE_TYPE": "WORK" } ] 
                          }
                      }
                        ).subscribe(res => {
                       console.log(res);
                        });

                      let obj = {
                        mobileNumber: this.basicRegistrationVm.phoneNumber,
                        message: "Welcome to HoomWork Family!" + "<br/>" + "Your Pincode for HoomWork Homes App is 11111." + "<br/>" +"Android: https://bit.ly/3yuE3AT " + "<br/>"+"IOS: https://apple.co/3ysQeOG to download HoomWork Homes App. For any help,contact us at 0309-9994531" + "<br/><br/>" + "Regards" + "<br/>" + "Hoomwork",
                        mobileNumberList: [this.basicRegistrationVm.phoneNumber]
                      };
                      console.log(obj);
                      this.common.post(this.common.apiRoutes.Communication.SendRegisterUserSms, obj).subscribe(result => {
                        var data = result;
                        if (data) {
                          console.log("Success Message Sent to new user !!");
                        }
                        else {
                          console.log("Success Message  Failed to new user  !!");
                        }
                      });
                    }
                  }, error => {
                    console.log(error);
                  });
                  var decodeToken = this.jwtHelperService.decodeToken(this.response.resultData);
                  if (decodeToken.Role == loginsecurity.CRole) {                  
                    this.appValForm.reset();
                    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.UserManagement.SuccessMessage, { queryParams: { cTId: this.campaignType, rId: this.userRoleId } });
                  }
                }
              });
            }

          });

        }
        else {
          //----------- Already Registered user visit to  Job Post By Facebook Lead 
          this.common.PostData(this.common.apiRoutes.Jobs.JobPostByFacebookLeads, JSON.stringify(formData)).then(res => {
            this.response = res;
            console.log(this.response);
            if (this.response.status == httpStatus.Ok) {
              //----------- Save Data on bitrix24 for Job Post by Facebook Lead
              let url = 'https://hoomwork.bitrix24.com/rest/32/7t94rhu9k3d999ry/crm.lead.add';
              this.http.post(url,
                {
                  fields:
                  { 
                    "TITLE": "Need "+this.selectedSkillName,  
                      "NAME": bitrixObj.name, 
                      "SECOND_NAME": "A.", 
                      "LAST_NAME": "Nibot", 
                      "STATUS_ID": "NEW", 
                      "OPENED": "Y", 
                      "ASSIGNED_BY_ID": 1, 
                      "CURRENCY_ID": "USD", 
                      "OPPORTUNITY": bitrixObj.budegt,
                      "PHONE": [ { "VALUE": bitrixObj.PhoneNumber, "VALUE_TYPE": "WORK" } ] 
                  }
              }
                ).subscribe(res => {
               console.log(res);
                });

              let obj = {
                mobileNumber: formData.phoneNumber,
                message: "Woo hoo!" + "<br/>" + "Welcome back again.You have availed our promo.We are on it ! Stay tuned !" +"<br/>"+"For more information contact at 0309-9994531." + "<br/><br/>" + "Regards" + "<br/>" + "Hoomwork",
                mobileNumberList: [formData.phoneNumber]
              };
              console.log(obj);
              this.common.post(this.common.apiRoutes.Communication.SendRegisterUserSms, obj).subscribe(result => {
                var data = result;
                if (data) {
                  console.log("Success Message Sent to existed user !!");
                }
                else {
                  console.log("Success Message Failed to existed user !!");
                }
              });
            }
          }, error => {
            console.log(error);
          });
          var token = localStorage.getItem("auth_token");
          if (token != null && token != '') {
            this.loginCheck = true;
            var decodeToken = this.jwtHelperService.decodeToken(token);
            if (decodeToken.Role == loginsecurity.CRole) {
              this.appValForm.reset();
              this.common.NavigateToRouteWithQueryString(this.common.apiUrls.UserManagement.SuccessMessage, { queryParams: { cTId: this.campaignType, rId: this.userRoleId} });
            }
          }
          else {
            localStorage.setItem("Role", '3');
            localStorage.setItem("Show", 'true');
            this.appValForm.reset();
            this.common.NavigateToRouteWithQueryString(this.common.apiUrls.UserManagement.SuccessMessage, { queryParams: { cTId: this.campaignType, rId: this.userRoleId} });
          }
        }
      })
    }
  }

  //---------------------- Show Promotion Details
  getPromotionList() {

    this.Loader.show();
    this.common.get(this.common.apiRoutes.UserManagement.GetPromotionListForWeb).subscribe(result => {
      let response = result;
      this.activePromotionList = <IActivePromotion[]>response;

      this.activePromotionList.filter(x => x.promotionId == this.promotionId && x.userRoleId == this.userRoleId).map(y => {
        this.promoDetails = y;
        console.log(this.promoDetails);
      });
      this.campaignName = CampaignTypesName.PCampaign;
      this.appValForm.controls['skillId'].setValue(this.promoDetails?.skillId);
      this.getPromotionAppliedSubSkillsList(this.promoDetails?.skillId, this.promoDetails?.subSkillIds);
      this.selectedSkillName = this.promoDetails.skillName;
      this.Loader.hide();
    });
  }
  //---------------------- Show Campaign Details
  getCampaignsList( cId:string) {
    this.Loader.show();
    this.campaignTypeId = Number(cId);
    this.common.get(this.common.apiRoutes.UserManagement.GetCampaignTypeList + `?campaignTypeId=${this.campaignTypeId}`).subscribe(result => {
      this.response = result;
      if (this.response.status == HttpStatusCode.Ok) {
        this.campaignList = this.response.resultData;
      }
      this.campaignList.filter(x => x.promotionId == this.promotionId && x.userRoleId == this.userRoleId).map(y => {
        this.promoDetails = y;
        console.log(this.promoDetails);
      });
      this.appValForm.controls['skillId'].setValue(this.promoDetails?.skillId);
      this.getPromotionAppliedSubSkillsList(this.promoDetails?.skillId, this.promoDetails?.subSkillIds);
      this.selectedSkillName = this.promoDetails.skillName;
      if (CampaignTypes.Promotion == this.campaignType) {
        this.campaignName = CampaignTypesName.PCampaign;
      }
      else if (CampaignTypes.Advertisement == this.campaignType) {
        this.campaignName = CampaignTypesName.ACampaign;
      }
      this.Loader.hide();
    });
  }
  public getPromotionAppliedSubSkillsList(parentId:number,subSkillsIds : string) {
    this.common.get(this.common.apiRoutes.Jobs.GetTradesmanSkillsByParentId + `?parentId=${parentId}`).subscribe(res => {
      this.listOfSubCategories = res;
      this.listOfSubSkills = this.listOfSubCategories.filter(item => subSkillsIds.includes((item.id).toString()));
      console.log(this.listOfSubSkills);
    }, error => {
      console.log(error);
    });
  }
  public bindMetaTags() {
    this.common.get(this.common.apiRoutes.CMS.GetSeoPageById + "?pageId=5").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description, res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }

  public closeModal() {
    this.bookNowMessageModal.hide();
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.UserManagement.GetPromotionDetailsByUserRole,{queryParams:{roleId: this.userRoleId,promotionId:this.promotionId}});

  }

  public AllowNonZeroIntegers(event: KeyboardEvent): boolean {

    var val = event.keyCode;
    // var target = event.target ? event.target : event.srcElement;
    if (val == 48 && (<HTMLInputElement>event.target).value == "" || val == 101 || val == 45 || val == 46 || ((val >= 65 && val <= 90)) || ((val >= 97 && val <= 122))) {
      return false;
    }
    else if ((val >= 48 && val < 58) || ((val > 96 && val < 106)) || val == 8 || val == 127 || val == 189 || val == 109 || val == 9) {
      return true;
    }
    else {
      return false;
    }
  }

}
