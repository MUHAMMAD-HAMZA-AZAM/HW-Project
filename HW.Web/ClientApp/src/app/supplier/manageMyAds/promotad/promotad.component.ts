import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { CommonService } from '../../../shared/HttpClient/_http';
import { AdsStatus, httpStatus} from '../../../shared/Enums/enums';

@Component({
  selector: 'app-promotad',
  templateUrl: './promotad.component.html',
  styleUrls: ['./promotad.component.css']
})
export class PromotadComponent implements OnInit {
  public AdId;
  public subCat;
  public adTitle;
  public adImage;

  constructor(private route: ActivatedRoute, private common: CommonService) { }

  ngOnInit() {
    this.route.queryParams.subscribe((params: Params) => {
      debugger;
      this.AdId = params['supplierAdsId'];
      this.subCat = params['subCategoryValue'];
      this.adTitle = params['adTitle'];
      this.adImage = params['adImage'];
    });
  }


  Skipped() {
    debugger;
    this.common.GetData(this.common.apiRoutes.Supplier.UpdateSupplierAdsstatus + "?supplierAdsId=" + this.AdId + "&supplieradsStatusId=" + AdsStatus.Regular + "&days=" + 0).then(data => {
      debugger;
      var result = data;
      if (result.status == httpStatus.Ok) {
        this.common.NavigateToRoute(this.common.apiUrls.Supplier.ManageAd);
      }
    });
  }

}
