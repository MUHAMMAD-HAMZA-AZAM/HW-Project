import { Component, OnInit, HostListener } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-managemyads',
  templateUrl: './managemyads.component.html',
  styleUrls: ['./managemyads.component.css']
})
export class ManagemyadsComponent implements OnInit {
  public activeAds: object[] = [];
  public activeConcatAds: object[] = [];
  public inActiveAds: object[] = [];
  public inActiveConcatAds: object[] = [];
  activeCount = 1;
  inActiveCount = 1;

  constructor(private common: CommonService, private sanitizer: DomSanitizer) { }

  ngOnInit() {
    this.activejobs(this.activeCount);
    this.completedjobs(this.inActiveCount);
  }

  public activejobs(count) {
    
    this.common.GetData(this.common.apiRoutes.Supplier.GetActiveAds + "?pageNumber=" + count + "&pageSize=" + 10, true).then(data => {
      
      this.activeAds = data.json();
      this.activeConcatAds = [...this.activeConcatAds, ...this.activeAds];
      console.log(this.activeConcatAds);
    });
  }
  public completedjobs(count) {
    this.common.GetData(this.common.apiRoutes.Supplier.GetInActiveAds + "?pageNumber=" + count + "&pageSize=" + 10,true).then(data => {
      
      this.inActiveAds = data.json();
      this.inActiveConcatAds = [...this.inActiveConcatAds, ...this.inActiveAds];
      console.log(this.inActiveConcatAds);
    });
  }

  onScroll() {
    this.activejobs(++this.activeCount);
  }
  onScrollCompletedjobs() {
    this.activejobs(++this.inActiveCount);
  }

  public JobDetail(adId) {

    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Supplier.EditAd, { queryParams: { supplierAdsId: adId } });
    //this.common.NavigateToRouteWithQueryString(this.common.apiRoutes.Jobs.GetFinishedJobDetail);

  }

  transform(base64Image) {
    return this.sanitizer.bypassSecurityTrustResourceUrl('data:image/png;base64,' + base64Image);
  }

  DifferenceOfDate(startDate,endDate) {
    var date1 = new Date(startDate);
    var date2 = new Date(endDate);
    var Difference_In_Time = date2.getTime() - date1.getTime();
    var Difference_In_Days = Difference_In_Time / (1000 * 3600 * 24);
    return Difference_In_Days;
  }

  Promote(adId, subCat, title, image) {
    debugger;
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Supplier.PromoteAd, { queryParams: { supplierAdsId: adId, subCategoryValue: subCat, adTitle: title, adImage: image } });
  }

}
