import { HttpStatusCode } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ResponseVm } from '../../models/commonModels/commonModels';
import { metaTagsService } from '../../shared/CommonServices/seo-dynamictags.service';
import { AspnetRoles, CampaignTypes, CampaignTypesName, loginsecurity } from '../../shared/Enums/enums';
import { IActiveCampaignType, IActivePromotion, IPageSeoVM, IPromotionDetails } from '../../shared/Enums/Interface';
import { CommonService } from '../../shared/HttpClient/_http';

@Component({
  selector: 'app-promotion-by-user',
  templateUrl: './promotion-by-user.component.html',
  styleUrls: ['./promotion-by-user.component.css']
})
export class PromotionByUserComponent implements OnInit {
  public userRoleName: any;
  public userRoleId: any;
  public campaignId: number = 0;
  public campaignType: string = '';
  public campaignName: string | null = '';
  public promoDetails: IPromotionDetails;
  public promotionList: IActivePromotion[] = [];
  public userTypePromotionList: IActivePromotion[] = [];
  public campaignList: IActivePromotion[] = [];
  public activeCampaignList: IActivePromotion[] = [];
  public campaignTypesList: IActivePromotion[] = [];
  public response: ResponseVm = {} as ResponseVm;
  //public userPromotionList = []; 
  constructor(public common: CommonService,
    private router: Router, public modalService: NgbModal, public Loader: NgxSpinnerService, private route: ActivatedRoute, private _metaService: metaTagsService) {
    this.promoDetails = {} as IPromotionDetails;
  }

  ngOnInit() {
    this.route.queryParams.subscribe((params: Params) => {
      this.userRoleId = params['rId'];
      this.campaignType = params['cTId'];
      if (this.campaignType) {
        this.getCampaignList(this.campaignType);
      }
      else {
      this.showPromotionsList();
      }
      this.bindMetaTags();
    });
  }
  navigateWithSkillId(skillId: number) {
    this.common.NavigateToRouteWithQueryString('User/JobQuotes/step1', { queryParams: { skillId: skillId } })
    this.modalService.dismissAll();
  }

  //------------------- Show Campaigns List
  getCampaignList(cId: string) {
    this.campaignId = Number(cId);
    this.Loader.show();
    this.common.get(this.common.apiRoutes.UserManagement.GetCampaignTypeList + `?campaignTypeId=${this.campaignId}`).subscribe(result => {
      this.response = result;
      if (this.response.status == HttpStatusCode.Ok) {
        this.campaignList = this.response.resultData;
      }
      this.userTypePromotionList = this.campaignList.filter(p => p.userRoleId == this.userRoleId);
      this.getUserRoleName();
      if (CampaignTypes.Promotion == this.campaignType) {
        this.campaignName = CampaignTypesName.PCampaign;
      }
      else if (CampaignTypes.Advertisement == this.campaignType) {
        this.campaignName = CampaignTypesName.ACampaign;
      }
      this.Loader.hide();
    });
  }
   //------------------- Show Promotions List
  public showPromotionsList() {
    this.Loader.show();
    this.common.get(this.common.apiRoutes.UserManagement.GetPromotionListForWeb).subscribe(result => {
      let response = result;
      this.promotionList = <IActivePromotion[]>response;
      this.userTypePromotionList = this.promotionList.filter(p => p.userRoleId == this.userRoleId);
      this.getUserRoleName();
      this.campaignName = CampaignTypesName.PCampaign;
      this.Loader.hide();
    });
  }

    //------------------- View Promotion Details
  viewPromotionDetails(itemId: number, promoContent: TemplateRef<any>) {
    this.promotionList.filter(x => x.promotionId == itemId).map(y => {
      this.promoDetails = {
        name: y.name,
        description: y.description,
        skillName: y.skillName,
        skillId: y.skillId,
        image: y.image,
      }
    });
    this.modalService.open(promoContent, { size: 'lg', scrollable: true, centered: true });
  }

   //------------------- Get User Role Name
  getUserRoleName() {
    if (this.userRoleId == AspnetRoles.CRole) {
      this.userRoleName = loginsecurity.CRole;
    }
    else if (this.userRoleId == AspnetRoles.TRole) {
      this.userRoleName = loginsecurity.TRole;
    }
    else if (this.userRoleId == AspnetRoles.ORole) {
      this.userRoleName = loginsecurity.ORole;
    }
    else {
      this.userRoleName = loginsecurity.SRole;
    }
  }
  public bindMetaTags() {
    this.common.get(this.common.apiRoutes.CMS.GetSeoPageById + "?pageId=10").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description,res.keywords, res.ogTitle, res.ogDescription, res.canonical);    });
  }
}
