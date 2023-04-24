import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { CommonService } from '../../../shared/HttpClient/_http';
import { AdsStatus, httpStatus, CommonErrors} from '../../../shared/Enums/enums';

@Component({
  selector: 'app-promotad',
  templateUrl: './promotad.component.html',
  styleUrls: ['./promotad.component.css']
})
export class PromotadComponent implements OnInit {
  public AdId: number=0;
  public subCat: number=0;
  public adTitle: string="";
  public adImage:any;

  constructor(private route: ActivatedRoute, private common: CommonService) {
    this.route.queryParams.subscribe((params: Params) => {
      
      this.AdId = params['supplierAdsId'];
      this.subCat = params['subCategoryValue'];
      this.adTitle = params['adTitle'];
      this.adImage = params['adImage'];
      if (this.adImage == null) {
        this.adImage = (localStorage.getItem("adImage") !="undefined") ? localStorage.getItem("adImage") : "";
      }
    });
  }

  ngOnInit() {

  }


  public Skipped() {
    localStorage.removeItem("adImage");
    this.common.GetData(this.common.apiRoutes.Supplier.UpdateSupplierAdsstatus + "?supplierAdsId=" + this.AdId + "&supplieradsStatusId=" + AdsStatus.Regular + "&days=" + 21).then(data => {
      
      var result = data;
      if (result.status == httpStatus.Ok) {
        this.common.NavigateToRoute(this.common.apiUrls.Supplier.ManageAd);
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }

}
