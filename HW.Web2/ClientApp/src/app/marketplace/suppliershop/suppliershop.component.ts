import { Component, OnInit, ElementRef, NgZone } from '@angular/core';
import { SupplierShopVM } from '../../models/userModels/userModels';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from '../../shared/HttpClient/_http';
import { DomSanitizer } from '@angular/platform-browser';
import { MapsAPILoader } from '@agm/core';
import { CommonErrors } from '../../shared/Enums/enums';
import { IShopSupplierAd, ISuppliersFeedback, ISupplierShopWeb } from '../../shared/Enums/Interface';

@Component({
  selector: 'app-suppliershop',
  templateUrl: './suppliershop.component.html',
  styleUrls: ['./suppliershop.component.css']
})
export class SuppliershopComponent implements OnInit {
  public shopSupplier: ISupplierShopWeb;
  public shopSupplierProduct: IShopSupplierAd[]=[];
  public Feedbacks: ISuppliersFeedback[]=[];
  public latitude: number=0;
  public longitude: number=0;
  public zoom: number = 18;
  public address: string="";
  public geoCoder = new google.maps.Geocoder();
  public searchElementRef: ElementRef;

  constructor(private route: ActivatedRoute, private common: CommonService, public sanitizer: DomSanitizer,
    public mapsAPILoader: MapsAPILoader,
    public ngZone: NgZone) {
    this.shopSupplier = {} as ISupplierShopWeb;
    this.searchElementRef = {} as ElementRef;
  }

  ngOnInit() {
    this.PopulateData();
  }

  public PopulateData() {
    var id = this.route.snapshot.paramMap.get('id');
    this.common.GetData(this.common.apiRoutes.Users.MarketPlace.SupplierShop + "?supplierId=" + id, true).then(data => {
      this.shopSupplier = data ;
      console.log(this.shopSupplier);
      this.shopSupplierProduct = this.shopSupplier.supplierAds;
      console.log(this.shopSupplierProduct);
      this.Feedbacks = this.shopSupplier.supplierFeedbacks;
      console.log(this.Feedbacks);
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

  public transform(base64Image: string) {
    return this.sanitizer.bypassSecurityTrustResourceUrl('data:image/png;base64,' + base64Image);
  }
}

