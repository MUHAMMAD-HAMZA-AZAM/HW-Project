import { Component, OnInit } from '@angular/core';
import { metaTagsService } from '../../shared/CommonServices/seo-dynamictags.service';
import { IPageSeoVM } from '../../shared/Enums/Interface';
import { CommonService } from '../../shared/HttpClient/_http';

@Component({
  selector: 'app-AboutUs',
  templateUrl: './aboutUs.component.html',
  styleUrls: ['./aboutUs.component.css']
})
export class AboutUsComponent implements OnInit {

  constructor(private _httpService: CommonService, private _metaService: metaTagsService) { }

  ngOnInit() {
    this.getPagesList();
    //document.getElementById("headerText").innerHTML = "About Us";
  }
  getPagesList() {
    this._httpService.get(this._httpService.apiRoutes.CMS.GetSeoPageById + "?pageId=3").subscribe(response => {
      debugger;
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description, res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    })
  }
}
