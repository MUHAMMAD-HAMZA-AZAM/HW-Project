import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { StatusCode } from 'src/app/Shared/Enums/common';
import { AspnetRoles, TrackingStatusType } from 'src/app/Shared/Enums/enums';
import { IDetails, IPersonalDetails, IResponseVM } from 'src/app/Shared/Enums/Interface';
import { CommonService } from 'src/app/Shared/HttpClient/_http';

@Component({
  selector: 'app-order-tracking',
  templateUrl: './order-tracking.component.html',
  styleUrls: ['./order-tracking.component.css']
})
export class OrderTrackingComponent implements OnInit {
  public userId: string = "";
  public id:string = "";
  public userRole: string = '';
  public decodeToken: any = "";
  public orderId: number = 0;
  public trackingNumber: number | undefined = 0;
  public loggedUserDetails: IPersonalDetails;
  public response: IResponseVM;
  public getRecord: IDetails[] = [];
  public trackingRecord: IDetails[] = [];
  constructor(
    public _httpService: CommonService,
    private router: ActivatedRoute,
    public Loader: NgxSpinnerService,
    private _modalService: NgbModal
    
  )
  {
    this.response = {} as IResponseVM;
  }

  ngOnInit(): void {
    this.router.queryParams.subscribe(params => {
      this.orderId = params['orderId'];
      this.trackingNumber = params['trackId'];
      
    });
      this.decodeToken = this._httpService.decodedToken();
       console.log(this.decodeToken);
        this.id = this.decodeToken.UserId;
        this.userId = this.decodeToken.Id;
        this.userRole = this.decodeToken.Role;
        this.getLoggedUserDetails(this.userRole, this.id);
      
  }
  public getLoggedUserDetails(role: string, id: string) {

    this._httpService.GetData(this._httpService.apiUrls.Supplier.Profile.GetUserDetailsByUserRole + `?userId=${id}&userRole=${role}`, true).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        this.loggedUserDetails = <any>this.response.resultData;
        console.log(this.loggedUserDetails.mobileNumber);
        if (this.orderId) {
          this.getOrderTrackingForSupplier(this.orderId);
        }
        else {
          this.getOrderedItemTrackingForSupplier(this.trackingNumber);
        }
      }
    }, error => {
      this._httpService.Loader.show();
      console.log(error);
    });
  }
   //-------------- For Order Tracking
   public getOrderTrackingForSupplier(oderId:number) {
    this.trackingRecord = [];
    let obj = {
      order_id: this.orderId,
      type: TrackingStatusType.ShipperRelatedTracking,
      userRole: AspnetRoles.SRole,
    };
    this._httpService.Loader.show();
    this._httpService.PostData(this._httpService.apiUrls.Supplier.Orders.GetOrderTrackingForSupplier, JSON.stringify(obj)).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        this.getRecord = <any>this.response.resultData;
        this.trackingRecord = this.getRecord.filter(item => item.pickup.phone_number == this.loggedUserDetails.mobileNumber);
        // console.log(this.trackingRecord);
      }
      this._httpService.Loader.hide();
    });
  }

   
  //------------------- For Shippers' Tracking

  public getOrderedItemTrackingForSupplier(trackingNumber: number){
    this.trackingRecord = [];
    let obj = {
      tracking_number: this.trackingNumber,
      type: TrackingStatusType.ShipperRelatedTracking,
      userRole: AspnetRoles.SRole
    };
    this._httpService.Loader.show();
    this._httpService.PostData(this._httpService.apiUrls.Supplier.Orders.getOrderedItemTrackingForSupplier, JSON.stringify(obj)).then(res =>{
     this.response = res;
     if(this.response.status == StatusCode.OK){
      let orderItemTrackRecord = <any>this.response.resultData;
      console.log(orderItemTrackRecord);
      this.trackingRecord.push(orderItemTrackRecord);
       this._httpService.Loader.hide();
     }
     this._httpService.Loader.hide();
    });
  }
}
