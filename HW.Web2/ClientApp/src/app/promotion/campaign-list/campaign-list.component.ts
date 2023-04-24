import { HttpStatusCode } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ResponseVm } from '../../models/commonModels/commonModels';
import { metaTagsService } from '../../shared/CommonServices/seo-dynamictags.service';
import { AspnetRoles, CampaignTypes, CampaignTypesName } from '../../shared/Enums/enums';
import { IActiveCampaignType, IPageSeoVM } from '../../shared/Enums/Interface';
import { CommonService } from '../../shared/HttpClient/_http';

@Component({
  selector: 'app-campaign-list',
  templateUrl: './campaign-list.component.html',
  styleUrls: ['./campaign-list.component.css']
})
export class CampaignListComponent implements OnInit {

  public userRoles = AspnetRoles;
  public campaignType = CampaignTypes;
  public campaignTypeName = CampaignTypesName;
  public campaignName: string | null = '';
  public campaignId: string | null = '';
  public notFound: boolean = false;
  public campaignDetails = {};
  public activeCampaignList: IActiveCampaignType[] = [];
  public customerCampaignsList: IActiveCampaignType[] = [];
  public tradesmanCampaignList: IActiveCampaignType[] = [];
  public organizationCampaignList: IActiveCampaignType[] = [];
  public supplierCampaignList: IActiveCampaignType[] = [];
  public response: ResponseVm = {} as ResponseVm;
  constructor(public common: CommonService,
    private router: Router,
    private route: ActivatedRoute,
    public modalService: NgbModal,
    public Loader: NgxSpinnerService,
    private _metaService: metaTagsService) {
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.campaignId = params['id'];
      console.log(this.campaignId);
      if (this.campaignId) {
        this.getCampaignList();
      }
    });
    this.showCampaignName(this.campaignId);
  }

  showCampaignName(ctId: string | null) {
    if (CampaignTypes.Promotion == ctId) {
      this.campaignName = CampaignTypesName.PCampaign;
    }
    else if (CampaignTypes.Advertisement == ctId) {
      this.campaignName = CampaignTypesName.ACampaign;
    }
  }

  //------------------- Show Campaigns List
  getCampaignList() { 
    this.Loader.show();
    this.common.get(this.common.apiRoutes.UserManagement.GetCampaignTypeList + `?campaignTypeId=${Number(this.campaignId)}`).subscribe(result => {
      this.response = result;
      if (this.response.status == HttpStatusCode.Ok) {
        this.activeCampaignList = this.response.resultData;
        if (this.activeCampaignList?.length > 0) {
          this.customerCampaignsList = this.activeCampaignList.filter(p => p.userRoleId == AspnetRoles.CRole).slice(0, 4);
          this.tradesmanCampaignList = this.activeCampaignList.filter(p => p.userRoleId == AspnetRoles.TRole).slice(0, 4);
          this.organizationCampaignList = this.activeCampaignList.filter(p => p.userRoleId == AspnetRoles.ORole).slice(0, 4);
          this.supplierCampaignList = this.activeCampaignList.filter(p => p.userRoleId == AspnetRoles.SRole).slice(0, 4);

          this.notFound = false;
        }
        else {
          this.notFound = true;
        }
        
      }
      
      this.Loader.hide();
    });
  }
}
