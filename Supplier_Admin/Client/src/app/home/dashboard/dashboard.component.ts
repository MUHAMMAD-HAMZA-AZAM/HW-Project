import { HttpStatusCode } from '@angular/common/http';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { StatusCode } from '../../Shared/Enums/common';
import { ILocation, IProfileVerification } from '../../Shared/Enums/Interface';
import { MessagingService } from '../../Shared/HttpClient/messaging.service';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  public PersonalDetailsCountList: ILocation[] = [];
  public suppId:string="";
  profileVerification: any = [];
  public userProfileStatus: IProfileVerification = {
    bankAccountVerification: false,
    businessInformationVerification: false,
    returnAddressVerification: false,
    wareHouseAddressVerification: false,
    sellerAccountVerification: false,
    isAllGoodStatus:false
  };
  @ViewChild('openAllGoodStatusModal', { static: true }) openAllGoodStatusModal: ElementRef;
  constructor(public _httpService: CommonService, public Loader: NgxSpinnerService, private messagingService: MessagingService, public _modalService: NgbModal) { }
  ngOnInit() {
    var decodedtoken = this._httpService.decodedToken();
    this.suppId = decodedtoken.Id;
    this.getData();
  }
  getData() {
    this.Loader.show();
    this._httpService.GetData(this._httpService.apiUrls.Supplier.Profile.GetProfileVerification + "?supplierId=" + this.suppId, true).then(res => {
      let response = res;
      if (response.status == HttpStatusCode.Ok) {
        this.userProfileStatus = response.resultData[0];
        if (this.userProfileStatus.isAllGoodStatus) {
          console.log("On Live Supplier");

        }
        else {
          // this._modalService.open(this.openAllGoodStatusModal, { size: 'md', centered: true});
          console.log("Off Line Supplier");
        }
      }
      this.Loader.hide();
    }, error => {
      console.log(error);
    });
  }
  ShowCard(tabName:string) {
    this._httpService.NavigateToRouteWithQueryString(this._httpService.apiRoutes.ProfileManagement.ProfileManagement, {
      queryParams: {
        "tabId": tabName
      }
    });
  }
  GetPersonalDetailsCount() {
    this._httpService.GetData(this._httpService.apiUrls.Supplier.Profile.GetLocationList + "?areaId=" + this.suppId)?.then(res => {
        if (res.status == StatusCode.OK) {
          this.PersonalDetailsCountList = res.resultData;
        }
      });
  }
}
