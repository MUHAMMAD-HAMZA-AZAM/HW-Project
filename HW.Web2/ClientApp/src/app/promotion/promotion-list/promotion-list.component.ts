import { PlatformLocation } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ResponseVm } from '../../models/commonModels/commonModels';
import { metaTagsService } from '../../shared/CommonServices/seo-dynamictags.service';
import { AspnetRoles } from '../../shared/Enums/enums';
import { IActivePromotion, IPageSeoVM } from '../../shared/Enums/Interface';
import { CommonService } from '../../shared/HttpClient/_http';

@Component({
  selector: 'app-promotion-list',
  templateUrl: './promotion-list.component.html',
  styleUrls: ['./promotion-list.component.css']
})
export class PromotionListComponent implements OnInit {
  public userRoles: object = AspnetRoles;
  public promoDetails = {};
  public campaignTypeId: number = 1;
  public notFound: boolean = false;
  public response: ResponseVm = {} as ResponseVm;
  public promotionList: IActivePromotion[] = [];
  public activePromotionList: IActivePromotion[] = [];
  public customerPromotionsList: IActivePromotion[] = [];
  public tradesmanPromotionsList: IActivePromotion[] = [];
  public organizationPromotionsList: IActivePromotion[] = [];
  public supplierPromotionsList: IActivePromotion[] = [];
  constructor(public common: CommonService,
    private router: Router,
    private route: ActivatedRoute,
    public modalService: NgbModal,
    public Loader: NgxSpinnerService,
    private _metaService: metaTagsService  ) {
  }

  ngOnInit() {
    this.modalService.dismissAll();
      this.getPromotionList();
      this.bindMetaTags();
  }

  navigateWithSkillId(skillId: number) {
    this.common.NavigateToRouteWithQueryString('User/JobQuotes/step1', { queryParams: { skillId: skillId } })
    this.modalService.dismissAll();
  }

  //------------------- Show Promotions List
  getPromotionList() {
    this.Loader.show();
    this.common.get(this.common.apiRoutes.UserManagement.GetPromotionListForWeb).subscribe(result => {
      let response = result;
      this.promotionList = <IActivePromotion[]>response;
      if (this.promotionList?.length > 0) {
        this.activePromotionList = this.promotionList.filter(p => p.campaignTypeId == this.campaignTypeId);
        console.log(this.activePromotionList);
        if (this.activePromotionList?.length > 0) {
          this.customerPromotionsList = this.activePromotionList.filter(p => p.userRoleId == AspnetRoles.CRole && p.campaignTypeId == this.campaignTypeId).slice(0, 4);
          this.tradesmanPromotionsList = this.activePromotionList.filter(p => p.userRoleId == AspnetRoles.TRole && p.campaignTypeId == this.campaignTypeId).slice(0, 4);
          this.organizationPromotionsList = this.activePromotionList.filter(p => p.userRoleId == AspnetRoles.ORole && p.campaignTypeId == this.campaignTypeId).slice(0, 4);
          this.supplierPromotionsList = this.activePromotionList.filter(p => p.userRoleId == AspnetRoles.SRole && p.campaignTypeId == this.campaignTypeId).slice(0, 4);
          this.notFound = false;
        }
        else {
          this.notFound = true;
        }
      }
      else {
        this.notFound = true;
      }
     
      this.Loader.hide();
    });
  }

  //------------------- View Promotion Details
  viewPromotionDetails(itemId: number, promoContent: TemplateRef<any>) {
    this.activePromotionList.filter(x => x.promotionId == itemId).map(y => {
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

  //------------------- See More Promotions by User Role
  public viewPromotionsListByUserRole(roleId: string) {
    if (roleId == AspnetRoles.CRole) {
      this.common.NavigateToRouteWithQueryString(this.common.apiUrls.UserManagement.GetPromotionsListByUserRole, { queryParams: { roleId: roleId } });
    }
    else if (roleId == AspnetRoles.TRole) {
      this.common.NavigateToRouteWithQueryString(this.common.apiUrls.UserManagement.GetPromotionsListByUserRole, { queryParams: { roleId: roleId } });
    }
    else if (roleId == AspnetRoles.ORole) {
      this.common.NavigateToRouteWithQueryString(this.common.apiUrls.UserManagement.GetPromotionsListByUserRole, { queryParams: { roleId: roleId } });
    }
    else {
      roleId == AspnetRoles.SRole;
      this.common.NavigateToRouteWithQueryString(this.common.apiUrls.UserManagement.GetPromotionsListByUserRole, { queryParams: { roleId: roleId } });
    }
  }
  public bindMetaTags() {
    this.common.get(this.common.apiRoutes.CMS.GetSeoPageById + "?pageId=6").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description,res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }
}
