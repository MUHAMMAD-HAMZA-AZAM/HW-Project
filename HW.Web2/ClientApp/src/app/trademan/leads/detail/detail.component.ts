import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';
import { ActivatedRoute, Params } from '@angular/router';
import { jobDetails } from '../../../models/tradesmanModels/tradesmanModels';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { httpStatus } from '../../../shared/Enums/enums';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class LeadsDetailComponent implements OnInit {
  public jobQuatationid: number = 0;
  public jobdetail: jobDetails = {} as jobDetails;
  @ViewChild('ImageModel', { static: true }) ImageModel: ModalDirective;
  @ViewChild('CallBackModel', { static: true }) CallBackModel: ModalDirective;
  public imageContent: any;
  public PageName='';
    audio: any;
  constructor(public common: CommonService, private route: ActivatedRoute) {
    this.ImageModel = {} as ModalDirective;
    this.CallBackModel = {} as ModalDirective;
  }

  ngOnInit() {
    this.route.queryParams.subscribe((param: Params) => {
      this.jobQuatationid = param['jobDetailId'];
      this.PageName = param['PageName'];
      
      if (this.jobQuatationid == 0)
        this.jobQuatationid = 0;
      else
        this.PopulateData();
    });
  }



  public PopulateData() {
    
    this.common.GetData(this.common.apiRoutes.Tradesman.GetCustomerDetailsByIdWeb + "?id=" + this.jobQuatationid, true).then(data => {
      
      var result = data ;
      this.jobdetail = data ;
      this.audio = this.jobdetail.audioMessage;
      if (this.audio.audioContent != null) {
        this.jobdetail.audioMessage = 'data:audio/mp3;base64,' + this.audio.audioContent;
      }
      console.log(result);
    });
  }
  public CallBackPopup(QuoteID: number, customerId: number) {
    
    this.common.GetData(this.common.apiRoutes.Tradesman.NotificationCallRequest + "?jobQuotationId=" + QuoteID + "&customerId" + customerId, true).then(data => {
      var result = data;
      if (result.status == httpStatus.Ok)
        this.CallBackModel.show();
    })
  }
  public Close() {
    this.ImageModel.hide();
  }
  public CloseCallBack() {
    this.CallBackModel.hide();
  }
  public ImagePopUp(image: string) {
    
    this.imageContent = image;
    this.ImageModel.show()
  }
  public PostBid(jobQuotationId: number) {
    
    this.jobdetail.jobQuotationId = jobQuotationId;
    this.jobdetail.jobImages = [];
    this.jobdetail.video = [];
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Tradesman.MakeBids, { queryParams: { jobQuotes: JSON.stringify(this.jobdetail) } });
  }
  public EditBid(bidId: number) {
    
    this.jobdetail.bidId = bidId;
    this.jobdetail.jobImages = [];
    this.jobdetail.video = [];
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Tradesman.MakeBids, { queryParams: { jobQuotes: JSON.stringify(this.jobdetail) } })
  }
}
