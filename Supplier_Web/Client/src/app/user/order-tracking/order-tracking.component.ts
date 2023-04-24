import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { AspnetUserRoles, IResponseVM, StatusCode, TrackingStatusType } from '../../Shared/Enums/enum';
import { IDetails } from '../../Shared/Enums/Interface';
import { CommonService } from '../../Shared/HttpClient/HttpClient';
import { metaTagsService } from '../../Shared/HttpClient/seo-dynamic.service';

@Component({
  selector: 'app-order-tracking',
  templateUrl: './order-tracking.component.html',
  styleUrls: ['./order-tracking.component.css']
})
export class OrderTrackingComponent implements OnInit {
  public orderId: number = 0;
  public trackingNumber: number | undefined = 0;
  public trackingRecord: IDetails[] = [];
  public response: IResponseVM;
  constructor(
    public _httpService: CommonService,
    private router: ActivatedRoute,
    public Loader: NgxSpinnerService,
    private _metaService: metaTagsService,
    
  )
  {
    this.response = {} as IResponseVM;
  }

  ngOnInit(): void {
    this.router.queryParams.subscribe(params => {
      this.orderId = params['orderId'];
      this.trackingNumber = params['trackId'];
      if (this.orderId) {
        this.getOrderTrackingForCustomer(this.orderId);
      }
      else {
        this.getOrderedItemTrackingForCustomer(this.trackingNumber);
      }
    });
  }



  //-------------- For Order Tracking
  public getOrderTrackingForCustomer(oderId:number) {
    this.trackingRecord = [];
    let obj = {
      order_id: this.orderId,
      type: TrackingStatusType.ShipperRelatedTracking,
      userRole: AspnetUserRoles.CRole,
    };
    this._httpService.Loader.show();
    //console.log(obj);
    this._httpService.PostData(this._httpService.apiUrls.Customer.Order.GetOrderTrackingForCustomer, JSON.stringify(obj)).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        this.trackingRecord = <any>this.response.resultData;
        
        console.log(this.trackingRecord);
      }
      this._httpService.Loader.hide();
    });
  }

  //-------------- For Order Item Tracking
  public getOrderedItemTrackingForCustomer(trackingNumber: number) {
    this.trackingRecord = [];
    let obj = {
     tracking_number: this.trackingNumber,
      type: TrackingStatusType.ShipperRelatedTracking,
      userRole: AspnetUserRoles.CRole,
    };
    this._httpService.Loader.show();
    console.log(obj);
    this._httpService.PostData(this._httpService.apiUrls.Customer.Order.GetOrderedItemTrackingForCustomer, JSON.stringify(obj)).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        let orderItemTrackRecord = <any>this.response.resultData;
        console.log(orderItemTrackRecord);
        this.trackingRecord.push(orderItemTrackRecord);
      }
      this._httpService.Loader.hide();
    });
  }
}
