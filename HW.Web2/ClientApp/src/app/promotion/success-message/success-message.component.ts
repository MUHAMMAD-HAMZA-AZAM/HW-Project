import { HttpClient, HttpStatusCode } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ResponseVm } from '../../models/commonModels/commonModels';
import { metaTagsService } from '../../shared/CommonServices/seo-dynamictags.service';
import { AspnetRoles, CampaignTypes, CampaignTypesName } from '../../shared/Enums/enums';
import { IActivePromotion } from '../../shared/Enums/Interface';
import { CommonService } from '../../shared/HttpClient/_http';
import { IActivePromotionVM } from '../../shared/Interface/tradesman';

@Component({
  selector: 'app-success-message',
  templateUrl: './success-message.component.html',
  styleUrls: ['./success-message.component.css']
})
export class SuccessMessageComponent implements OnInit {
  public campaignTypeId: string | null = '';
  public campaignId: number = 0;
  public userRoleId: any;
  public userRoles = AspnetRoles;
  public campaignTypes = CampaignTypes;
  public campaignTypesName = CampaignTypesName;
  public campaignName: string | null = '';
  public campaignList: IActivePromotion[] = [];
  public response: ResponseVm = {} as ResponseVm;
  constructor(public common: CommonService,
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router,
    private route: ActivatedRoute,
    public Loader: NgxSpinnerService, private _metaService: metaTagsService)  { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.campaignTypeId = params['cTId'];
      this.userRoleId = params['rId'];
      console.log(this.campaignTypeId);
      if (this.campaignTypeId) {
        this.getCampaignsList(this.campaignTypeId);
        if (CampaignTypes.Promotion == this.campaignTypeId) {
          this.campaignName = CampaignTypesName.PCampaign;
        }
        else if (CampaignTypes.Advertisement == this.campaignTypeId) {
          this.campaignName = CampaignTypesName.ACampaign;
        }
      }
    });
  }
  //---------------------- Show Campaign Details
  getCampaignsList(cId: string) {
    this.Loader.show();
    this.campaignId = Number(cId);
    this.common.get(this.common.apiRoutes.UserManagement.GetCampaignTypeList + `?campaignTypeId=${this.campaignId}`).subscribe(result => {
      this.response = result;
      if (this.response.status == HttpStatusCode.Ok) {
        this.campaignList = this.response.resultData;
        this.campaignList = this.campaignList.filter(x => x.userRoleId == this.userRoleId).slice(0, 4);
        console.log(this.campaignList);
      }
      this.Loader.hide();
    });
  }

}
