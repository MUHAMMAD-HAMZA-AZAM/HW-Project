import { Component, OnInit, NgZone, ElementRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from '../../shared/HttpClient/_http';
import { DomSanitizer } from '@angular/platform-browser';

import { MouseEvent, MapsAPILoader } from '@agm/core';
import { CommonErrors } from '../../shared/Enums/enums';
import { SupplierShopVM } from '../../models/userModels/userModels';
import { IShopSupplierAd, ISuppliersFeedback, ISupplierShopWebVM } from '../../shared/Enums/Interface';

@Component({
  selector: 'app-supplier-shop',
  templateUrl: './supplier-shop.component.html',
  styleUrls: ['./supplier-shop.component.css']
})
export class SupplierShopComponent implements OnInit {
  public shopSupplier: ISupplierShopWebVM;
  public shopSupplierProduct: IShopSupplierAd[] = [];
  public Feedbacks: ISuppliersFeedback[] = [];
  public latitude: number=0;
  public longitude: number=0;
  public zoom: number = 18;
  public address: string="";
  public geoCoder = new google.maps.Geocoder();
  public searchElementRef: ElementRef;

  constructor(private route: ActivatedRoute, private common: CommonService, public sanitizer: DomSanitizer,
    public mapsAPILoader: MapsAPILoader,
    public ngZone: NgZone) {
    this.shopSupplier = {} as ISupplierShopWebVM;
    this.searchElementRef = {} as ElementRef;
  }

  ngOnInit() {
    this.PopulateData();
  }

  public PopulateData() {
    var id = this.route.snapshot.paramMap.get('id');
    this.common.GetData(this.common.apiRoutes.Users.MarketPlace.SupplierShop + "?supplierId=" + id, true).then(data => {
      this.shopSupplier = data ;
      this.shopSupplierProduct = this.shopSupplier.supplierAds;
      console.log(this.shopSupplierProduct);  
      this.Feedbacks = this.shopSupplier.supplierFeedbacks;
      if (this.shopSupplier.latLong != null) {
        var splits = this.shopSupplier.latLong.split(",")
        if (splits.length) {
          this.latitude = parseFloat(splits[0]);
          this.longitude = parseFloat(splits[1]);
        }
      }

    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    })
  }

  public transform(base64Image:string) {
    return this.sanitizer.bypassSecurityTrustResourceUrl('data:image/png;base64,' + base64Image);
  }
}
