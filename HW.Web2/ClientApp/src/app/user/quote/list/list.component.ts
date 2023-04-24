import { Component, OnInit, ViewChild } from '@angular/core';
import { httpStatus, CommonErrors } from '../../../shared/Enums/enums';
import { CommonService } from '../../../shared/HttpClient/_http';
import { UserPaymentInformation, GetPostedJobs } from '../../../models/userModels/userModels';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { SocialAuthService } from 'angularx-social-login';
import { JwtHelperService } from '@auth0/angular-jwt';
import { IMyQuotations } from '../../../shared/Enums/Interface';

@Component({
  selector: 'app-get-posted-jobs',
  templateUrl: './list.component.html',
})
export class QuoteListComponent implements OnInit {

  public loggedUserDetails: any;
  public userId: string="";
  public loginCheck: boolean = false;
  public isUserBlocked: boolean = false;
  public notFound: boolean = false;
  //public postedJob: GetPostedJobs[] = [];
  public postedJob: IMyQuotations[] = [];
  jwtHelperService: JwtHelperService = new JwtHelperService();
  @ViewChild('blockAccountMessageModal', { static: false }) blockAccountMessageModal: ModalDirective;

  constructor(
    public common: CommonService,
    private authService: SocialAuthService
  ) {
    this.blockAccountMessageModal = {} as ModalDirective;
  }

  ngOnInit() {
    let token = localStorage.getItem("auth_token");
    var decodedtoken = token!=null ? this.jwtHelperService.decodeToken(token):"";
    this.userId = decodedtoken.UserId
    this.checkUserStatus();
  }

  public PopulateData() {
  //  this.common.GetData(this.common.apiRoutes.Jobs.GetPostedJobsByCustomerId, true).then(result => {
      
  //    if (status = httpStatus.Ok) {
  //      this.postedJob = result ;
  //      if (this.postedJob.length > 0) {
  //        for (var i = 0; i < this.postedJob.length; i++) {
  //          if (this.postedJob[i].jobImage != null) {
  //            var imgURl = 'data:image/png;base64,' + this.postedJob[i].jobImage;
  //            this.postedJob[i].jobImage = imgURl.toString();
  //          }
  //        }
  //       }

  //    }
  //  }, error => {
  //    console.log(error);
  //    this.common.Notification.error(CommonErrors.commonErrorMessage);
    //  });
    this.common.GetData(this.common.apiRoutes.Jobs.SpGetPostedJobsByCustomerId + "?pageNumber=" + 1 + "&pageSize=" + 100 + "&statusId=" + 1 + "&bidStatus=" + false, true).then(result => {
      if (status = httpStatus.Ok) {
        var data: IMyQuotations[] = result;
        if (data?.length > 0) {
          this.postedJob = data;
          this.notFound = false;
        }
        else {
          this.notFound = true;
        } 
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }

  public JobDetail(jobQuotationId: number, bidCount: number) {
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.GetPostedJobDetail, { queryParams: { jobQuotationId: jobQuotationId, bidCount: bidCount } });
  }

  public ReceivedBids(quotationId: number) {
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.ReceivedBids, { queryParams: { quotationId: quotationId } });
  }


  public HomePage() {
    this.common.NavigateToRoute(this.common.apiUrls.User.UserDefault);
  }

  public checkUserStatus() {
    this.common.GetData(this.common.apiRoutes.Login.GetUserBlockStatus + "?userId=" + this.userId).then(result => {
      this.isUserBlocked = result 
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
