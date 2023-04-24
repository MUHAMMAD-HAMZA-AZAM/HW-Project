import { Component, OnInit, ViewChild } from '@angular/core';
import { TrademanProfileVm, ReceivedBids } from '../../../models/userModels/userModels';
import { CommonService } from '../../../shared/HttpClient/_http';
import { ActivatedRoute, Params } from '@angular/router';
import { httpStatus, CommonErrors } from '../../../shared/Enums/enums';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { SocialAuthService } from 'angularx-social-login';
import { JwtHelperService } from '@auth0/angular-jwt';
import { IJobImages, IReceivedBids, IResponse } from '../../../shared/Enums/Interface';

@Component({
  selector: 'app-received-bids',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})

export class BidListComponent implements OnInit {

  public quotationId: number=0;
  public jobTitle: string="";
  public jobImages: string="";
  public jobImage: string="";
  public Selected = '';
  public showJobImage :boolean = false;
  public isUserBlocked: boolean = false;
  public loggedUserDetails: any;
  public loginCheck: boolean = false;
  public userId: string = "";
  public notFound: boolean = false;
  public receivedBids: IReceivedBids[] = [];
  jwtHelperService: JwtHelperService = new JwtHelperService();
  @ViewChild('blockAccountMessageModal', { static: false }) blockAccountMessageModal: ModalDirective;

  constructor(
    public common: CommonService,
    private route: ActivatedRoute,
    private authService: SocialAuthService,
  ) {
    this.blockAccountMessageModal = {} as ModalDirective;
  }

  ngOnInit() {
    var token = localStorage.getItem("auth_token");
    var decodedtoken = token != null ? this.jwtHelperService.decodeToken(token):"";
    this.userId = decodedtoken.UserId
    this.route.queryParams.subscribe((params: Params) => {
      
      this.quotationId = params['quotationId'];
      if (this.quotationId > 0) {
        this.common.GetData(this.common.apiRoutes.Login.GetUserBlockStatus + "?userId=" + this.userId).then(result => {
          this.isUserBlocked = result ;
          if (!this.isUserBlocked) {
            this.PopulateData();
          }
          else {
            this.isUserBlocked = true;
            this.blockAccountMessageModal.show();
            setTimeout(() => {
              this.isUserBlocked = false;
              this.blockAccountMessageModal.hide();
              this.logout();
            }, 5000);
          }

        }, error => {
            console.log(error);
        });
        
      }
    });
  }

  public PopulateData() {
    var url = this.common.apiRoutes.Jobs.GetQuotationBids + "?quotationId=" + this.quotationId;
    this.common.GetData(url, true).then(result => {
      
      if (status = httpStatus.Ok) {
        this.receivedBids = result;
        if (this.receivedBids.length > 0) {
          console.log(this.receivedBids);
          this.jobTitle = this.receivedBids[0].jobQuotationTitle;
          this.common.GetData(this.common.apiRoutes.Jobs.GetJobMainImage + "?quotationId=" + this.quotationId).then(result => {

            if (status == httpStatus.Ok) {
              if(result)
              {
                this.showJobImage = true;
                let imagedata: IJobImages = result;
                this.jobImage = 'data:image/png;base64,' + imagedata.bidImage;
              }
              else{
                this.showJobImage = false;
              }
            
            }

          });
          if (this.receivedBids[0].bidImage != null) {
            this.jobImages = 'data:image/png;base64,' + this.receivedBids[0].bidImage;

          }
          this.notFound = false;
        }
        else {
          this.notFound = true;
        }
      }
    },
      error => {
        console.log(error);
        this.common.Notification.error(CommonErrors.commonErrorMessage);
      });
  }
  public BidSorted(event: Event) {
    var sort = (<HTMLInputElement>event.target).value
    var url = this.common.apiRoutes.Jobs.GetQuotationBids + "?quotationId=" + this.quotationId + "&sortId=" + sort;
    this.common.GetData(url, true).then(result => {
      if (status = httpStatus.Ok) {
        this.receivedBids = result ;
        this.jobTitle = this.receivedBids[0].jobQuotationTitle;
        if (this.receivedBids[0].bidImage != null) {
          this.jobImages = 'data:image/png;base64,' + this.receivedBids[0].bidImage;
        }
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }

  public BidDetail(bidId: number) {
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.ReceivedBidDetail, { queryParams: { bidId: bidId } });
  }

  public QuoteConfirmation(confirmbId: number, jobQuotationTitle?:string ,tradesmanId?:number) {
    console.log(this.quotationId + " " + confirmbId);
    let obj = {
      jobQuotationId: this.quotationId,
      bidId: confirmbId
    }
    
    this.common.PostData(this.common.apiRoutes.Jobs.UpdateBidByStatusId, obj, true).then(result => {
      let res: IResponse = result ;
      
      if (res.status == httpStatus.Ok)
        this.common.get(this.common.apiRoutes.Notifications.NotifyBidAcceptance + `?tradesmanId=${tradesmanId}&bidId=${confirmbId}&jobTitle=${jobQuotationTitle}`).subscribe(res => {
          let result = res;
          this.common.NavigateToRoute(this.common.apiUrls.User.Mybids);
        })
    })
  }

  public DeclineBid(declinebId: number, jobQuotationTitle?: string, tradesmanId?: number) {
    this.common.get(this.common.apiRoutes.Notifications.NotifyBidAcceptance + `?tradesmanId=${tradesmanId}&bidId=${declinebId}&jobTitle=${jobQuotationTitle}`).subscribe(res => {
      this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.ReceivedBidDetail, { queryParams: { declinebId: declinebId}});
    })
  }
  public IsSelected(isSelected: boolean, BidId: number, index: number) {
    if (isSelected == true) {
      this.receivedBids[index].isSelected = false;
      isSelected = false
    }
    else {
      this.receivedBids[index].isSelected = true;
      isSelected = true
    }
    var url = this.common.apiRoutes.Jobs.UpdateSelectedBid + "?BidId=" + BidId + "&IsSelected=" + isSelected;
    this.common.GetData(url, false).then(result => {
      if (status = httpStatus.Ok) {
        var data: boolean = result ;
      }
    },
      error => {
        console.log(error);
        this.common.Notification.error(CommonErrors.commonErrorMessage);
      });
  }

  public checkUserStatus(confirmbId: number) {
    this.common.GetData(this.common.apiRoutes.Login.GetUserBlockStatus + "?userId=" + this.userId).then(result => {
      this.isUserBlocked = result 
      if (!this.isUserBlocked) {
        this.QuoteConfirmation(confirmbId);
      }
      else {
        this.isUserBlocked = true;
        this.blockAccountMessageModal.show();
        setTimeout(() => {
          this.isUserBlocked = false;
          this.blockAccountMessageModal.hide();
          this.logout();
        }, 5000);
      }
    }, error => {

      console.log(error);
    });

  }

  public logout() {
    this.loggedUserDetails = null;
    localStorage.clear();
    this.authService.signOut();
    this.loginCheck = false;
    this.common.NavigateToRoute("");
  }

  public hideBlockModal() {
    this.blockAccountMessageModal.hide();
    this.logout();
  }
}

