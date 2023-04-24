import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Meta, Title } from '@angular/platform-browser';
import { ActivatedRoute, NavigationEnd, NavigationStart, Router } from '@angular/router';
import { filter, map, mergeMap } from 'rxjs/operators';
import { metaTagsService } from '../../shared/CommonServices/seo-dynamictags.service';
import { ISkill } from '../../shared/Enums/Interface';
import { CommonService } from '../../shared/HttpClient/_http';

@Component({
  selector: 'app-skillcategory',
  templateUrl: './skillcategory.component.html',
  styleUrls: ['./skillcategory.component.css']
})
export class SkillcategoryComponent implements OnInit {

  public skillId: number = 0;
  seoData: any;
  public skillDetails: ISkill;
  constructor(private title: Title, private meta: Meta, public service: CommonService, private router: Router, private route: ActivatedRoute, private _metaService: metaTagsService) {
    this.skillId = Number(this.route.snapshot.paramMap.get('id'));
    this.skillDetails = {} as ISkill;
    //this.route.data.subscribe(data => {
    //  //let a = data ;
    //  console.log(data);
    //  this._metaService.updateTitle(data.title);
    //  this._metaService.setDescription(data.description);
    //})
    //console.log(this.router.getCurrentNavigation()?.extras.state);
    //this.seoData = this.location.getState();
    //this.seoData = this.router.getCurrentNavigation()?.extras.state;
    //this.skillDetails = (this.route.snapshot.data['skillCategoryDetails']) ;
    //this.bindMetaTags(this.skillDetails.metaTags, this.skillDetails.skillTitle ? this.skillDetails.skillTitle : this.skillDetails.name)
    //this._metaService.updateDescription("Test description");
    //this.title.setTitle(((Math.random()) * 1000).toFixed(2).toString());
    //this._metaService.updateTitle(this.skillId.toString());
  }

  ngOnInit() {
    //this.bindMetaTags("test description" + (Math.random()).toString(), "test title" + ((Math.random()) * 1000).toFixed(2).toString())
    this.getSkillDetails();
  }
  public getSkillDetails() {
    this.service.get(this.service.apiRoutes.Tradesman.GetSkillTagsById + "?skillId=" + this.skillId).subscribe(response => {
      debugger;
      this.skillDetails = (<ISkill>response);
      this._metaService.updateTags(this.skillDetails.seoPageTitle, this.skillDetails.skillTitle ? this.skillDetails.skillTitle : this.skillDetails.seoPageTitle, this.skillDetails.metaTags,this.skillDetails.skillTitle ? this.skillDetails.skillTitle : this.skillDetails.seoPageTitle,this.skillDetails.ogTitle,this.skillDetails.ogDescription)
     // console.log(this.skillDetails);
    })
  }
}
