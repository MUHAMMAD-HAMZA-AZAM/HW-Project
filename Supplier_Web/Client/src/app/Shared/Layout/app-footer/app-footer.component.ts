import { Component, OnInit } from '@angular/core';
import { IProductCategory, SocialLinks } from '../../Enums/Interface';
import { CommonService } from '../../HttpClient/HttpClient';

@Component({
  selector: 'app-app-footer',
  templateUrl: './app-footer.component.html',
  styleUrls: ['./app-footer.component.css']
})
export class AppFooterComponent implements OnInit {
  topFiveProductList: IProductCategory[] = [];
  public socialLinks: SocialLinks = new SocialLinks();
;
  constructor(public _httpService: CommonService) { }

  ngOnInit(): void {
    this.populateSocialLinks();
    this.getTopFiveProductCategory();
  }
  populateSocialLinks() {
    this.socialLinks.facebookUrl = 'https://web.facebook.com/hoomwork.pk/?_rdc=1&_rdr';
    this.socialLinks.youtubeUrl = 'https://www.youtube.com/channel/UCcQyjWuLSXNXL9olWUb7-MQ?view_as=subscriber';
    this.socialLinks.twitterUrl = 'https://twitter.com/hoomworkpk';
    this.socialLinks.linkedinUrl = 'https://www.linkedin.com/company/hoomwork/';
  }
  getTopFiveProductCategory() {
    this._httpService.GetData1(this._httpService.apiUrls.Supplier.Product.GetTopFiveProductCategory, false).then(result => {
      this.topFiveProductList = (<any>result).resultData;
    })
  }
  openLink(url: string) {
    window.open(url);
  }
}
