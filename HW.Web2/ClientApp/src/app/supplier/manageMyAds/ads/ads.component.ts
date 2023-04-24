import { Component, OnInit, HostListener } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';
import { DomSanitizer } from '@angular/platform-browser';
import { CommonErrors } from '../../../shared/Enums/enums';
import { NgxSpinnerService } from 'ngx-spinner';
import { IInactiveManageAdsVMWithImages, IManageAdsVMWithImage } from '../../../shared/Enums/Interface';

@Component({
  selector: 'app-managemyads',
  templateUrl: './ads.component.html',
  styleUrls: ['./ads.component.css']
})
export class AdsComponent implements OnInit {
  public activeAds: IManageAdsVMWithImage[] = [];
  public activeConcatAds: IManageAdsVMWithImage[] = [];
  public inActiveAds: IInactiveManageAdsVMWithImages[] = [];
  public inActiveConcatAds: IInactiveManageAdsVMWithImages[] = [];
  public activeCount = 1;
  public inActiveCount = 1;

  constructor(private common: CommonService, private sanitizer: DomSanitizer, public Loader: NgxSpinnerService) { }

  ngOnInit() {
    this.activejobs(this.activeCount);
    this.completedjobs(this.inActiveCount);
  }

  public activejobs(count: number) {
    
    this.common.GetData(this.common.apiRoutes.Supplier.GetActiveAds + "?pageNumber=" + count + "&pageSize=" + 10, true).then(data => {
      this.activeAds = data ;
      console.log(this.activeAds );
      if (this.activeAds != null) {
        this.activeConcatAds = [...this.activeConcatAds, ...this.activeAds];
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }
  public completedjobs(count: number) {
    
    this.common.GetData(this.common.apiRoutes.Supplier.GetInActiveAds + "?pageNumber=" + count + "&pageSize=" + 10, true).then(data => {
      this.inActiveAds = data ;
      console.log(this.inActiveAds);
      if (this.inActiveAds != null) {
        this.inActiveConcatAds = [...this.inActiveConcatAds, ...this.inActiveAds];
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }

  public onScroll() {
    this.activejobs(++this.activeCount);
  }
  public onScrollCompletedjobs() {
    this.completedjobs(++this.inActiveCount);
  }

  public JobDetail(adId: number) {
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Supplier.EditAd, { queryParams: { supplierAdsId: adId } });
  }

  public transform(base64Image: string) {
    return this.sanitizer.bypassSecurityTrustResourceUrl('data:image/png;base64,' + base64Image);
  }

  public DifferenceOfDate(startDate: string, endDate: string) {
    var startDates = new Date(startDate);
    var endDates = new Date(endDate);
    var Difference_In_Time = endDates.getTime() - startDates.getTime();
    var Difference_In_Days = Difference_In_Time / (1000 * 3600 * 24);

    var todayDate = new Date();
    var Remaing_Times =todayDate.getTime() - startDates.getTime() ;
    var remaing_In_Days = Remaing_Times / (1000 * 3600 * 24);

    var days = Math.ceil(Difference_In_Days - remaing_In_Days);

    return days;
  }

  public Promote(adId: number, subCat: number, title: string, image: string) {
    localStorage.setItem("adImage", image);
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Supplier.PromoteAd, { queryParams: { supplierAdsId: adId, subCategoryValue: subCat, adTitle: title, adImage: null } });
  }

}
