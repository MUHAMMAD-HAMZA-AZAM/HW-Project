import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from '../../../shared/HttpClient/_http';
import { NgxSpinnerService } from 'ngx-spinner';
import { skillsVM } from '../../../models/tradesmanModels/tradesmanModels';
import { metaTagsService } from '../../../shared/CommonServices/seo-dynamictags.service';
import { ISubSkill } from '../../../shared/Interface/tradesman';

@Component({
  selector: 'app-skillprofile',
  templateUrl: './skillprofile.component.html',
  styleUrls: ['./skillprofile.component.css']
})
export class SkillprofileComponent implements OnInit {
  public skillList: ISubSkill;
  //public subSkillList: any;
  public skillsvm: skillsVM = {} as skillsVM;
  public skillId: string | null="";
  constructor(private route: ActivatedRoute, public common: CommonService, public Loader: NgxSpinnerService,
    public _seoService: metaTagsService) {
    this.skillList = {} as ISubSkill;
  }

  ngOnInit() {
    
    
    this.skillId = this.route.snapshot.paramMap.get('id');
    this.populateSkillsMetaTags(this.skillId);
    ///this.populateSubSkill(this.skillId);
  }

  public populateSkillsMetaTags(id: string | null) {
    

    this.Loader.show();
    this.common.get(this.common.apiRoutes.Tradesman.GetSkillTagsById + "?SkillId=" + id).subscribe(result => {

      this.skillsvm = <skillsVM>result;
      this.skillsvm.metaTags = this.skillsvm.metaTags ? this.skillsvm.metaTags:";"
      this._seoService.updateKeyWords(this.skillsvm.metaTags)
      this.skillsvm.description = this.skillsvm.description ? this.skillsvm.description : "";
      this._seoService.updateDescription(this.skillsvm.description)
      this.Loader.hide();
    },
      error => {
        console.log(error);
        this.Loader.hide();
      });
  }
  public populateSubSkill(id: string) {
    

    this.Loader.show();
    this.common.get(this.common.apiRoutes.Tradesman.GetSubSkillTagsBySkillId + "?SkillId=" + id).subscribe(result => {
      
      this.skillList = <ISubSkill>result ;
      this._seoService.updateKeyWords(this.skillList.metaTags)
      this._seoService.updateDescription(this.skillList.description)
      this.Loader.hide();
    },
      error => {
        console.log(error);
        this.Loader.hide();
      });
  }
  
}
