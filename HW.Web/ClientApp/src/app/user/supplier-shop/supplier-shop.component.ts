import { Component, OnInit, NgZone, ElementRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from '../../shared/HttpClient/_http';
import { DomSanitizer } from '@angular/platform-browser';

import { MouseEvent, MapsAPILoader } from '@agm/core';

@Component({
  selector: 'app-supplier-shop',
  templateUrl: './supplier-shop.component.html',
  styleUrls: ['./supplier-shop.component.css']
})
export class SupplierShopComponent implements OnInit {
  public shopSupplier: any;
  public shopSupplierProduct: any;
  public Feedbacks: any;
  latitude: number;
  longitude: number;
  zoom: number = 18;
  address: string;
  public geoCoder = new google.maps.Geocoder();
  public searchElementRef: ElementRef;

  constructor(private route: ActivatedRoute, private common: CommonService, public sanitizer: DomSanitizer,
    public mapsAPILoader: MapsAPILoader,
    public ngZone: NgZone) { }

  ngOnInit() {
    this.PopulateData();
  }
  PopulateData() {
    var id = +this.route.snapshot.paramMap.get('id');
    this.common.GetData(this.common.apiRoutes.Users.MarketPlace.SupplierShop + "?supplierId=" + id,true).then(data => {
      debugger;
      this.shopSupplier = data.json();
      this.shopSupplierProduct = this.shopSupplier.supplierAds;
      this.Feedbacks = this.shopSupplier.supplierFeedbacks;
      if (this.shopSupplier.latLong != null) {
        var splits = this.shopSupplier.latLong.split(",")
        if (splits.length) {
          debugger
          this.latitude = parseFloat(splits[0]);
          this.longitude = parseFloat(splits[1]);
          // this.getAddress(this.latitude, this.longitude);
        }
      }
      
    })
  }

  transform(base64Image) {
    return this.sanitizer.bypassSecurityTrustResourceUrl('data:image/png;base64,' + base64Image);
  }
}
