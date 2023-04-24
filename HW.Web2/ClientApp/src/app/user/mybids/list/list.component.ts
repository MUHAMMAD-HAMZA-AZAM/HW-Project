import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SocialAuthService } from 'angularx-social-login';
import { NgxSpinnerService } from 'ngx-spinner';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { Events } from '../../../common/events';
import { GetPostedJobs } from '../../../models/userModels/userModels';
import { AspnetRoles, BidStatus, CommonErrors, httpStatus } from '../../../shared/Enums/enums';
import { CommonService } from '../../../shared/HttpClient/_http';
import { IBidDetails, IEsclateObj, IEsclateOption, IResponse } from '../../../shared/Enums/Interface';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  public loggedUserDetails: any;
  public userId: string="";
  public decodedtoken: any;
  public tradesmanId: number=0;
  public jobQuotationId: number=0;
  public errorMessage: boolean = false;
  public responseMessage: boolean = false;
  public loginCheck: boolean = false;
  public isUserBlocked: boolean = false;
  public postedJob: GetPostedJobs[] = [];
  //public imagesList = [];
  public acceptedBidsList: IBidDetails[] = [];
  public esclateOption: IEsclateOption[] = [];
  public appFormVal: FormGroup;
  public submittedApplicationForm: boolean = false;
  public pageNumber = 1;
  public pageSize = 20;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  public eventSubscription: Subscription
  @ViewChild('blockAccountMessageModal', { static: false }) blockAccountMessageModal: ModalDirective;

  constructor(
    public common: CommonService,
    private authService: SocialAuthService,
    private _modalService: NgbModal,
    private fb: FormBuilder,
    private events: Events,
    public Loader: NgxSpinnerService,
  ) {
    this.appFormVal = {} as FormGroup;
    this.eventSubscription = {} as Subscription;
    this.blockAccountMessageModal = {} as ModalDirective;
  }

  ngOnInit() {
    var token = localStorage.getItem("auth_token");
    this.decodedtoken = token != null ? this.jwtHelperService.decodeToken(token):"";
    this.userId = this.decodedtoken.UserId
    this.appFormVal = this.fb.group({
      esclateOption: ['', Validators.required],
      comment: ['', [Validators.required]]
    });
    this.checkUserStatus();
    this.getAcceptedBidsList();
  }
  get f() {
    return this.appFormVal.controls;
  }
  confirmModal(confirmContent: TemplateRef<any>, obj: IBidDetails) {
    this._modalService.open(confirmContent, { centered: true, size: 'md', backdrop: 'static', keyboard: false }).result.then(response => {
    }, (reason) => {
      this.startJob(obj)
    })
  }
  public startJob(obj: IBidDetails) {
    this.common.get(this.common.apiRoutes.Jobs.AddJobDetails + `?bidId=${obj.bidId}&paymentMethod=1&statusId=${BidStatus.Accepted}`).subscribe(response => {
        let res = response;
      if (res.status == httpStatus.Ok) {
        this.common.get(this.common.apiRoutes.Notifications.NotifyJobStartFromTradesman + `?tradesmanId=${obj.tradesmanId}&customerId=${this.decodedtoken.Id}&bidId=${obj.bidId}&jobTitle=${obj.jobTitle}&jobQuotationId=${obj.jobQuotationId}`).subscribe(response => {
          this.common.NavigateToRoute(this.common.apiUrls.User.InProgressJobList);
        })

      }
    })
  }
  public getEsclateOption() {
    this.common.get(this.common.apiRoutes.Jobs.getEscalateOptions + `?userRole=${AspnetRoles.CRole}`).subscribe(res => {
      this.esclateOption = <IEsclateOption[]>res;
    });
  }
  showModal(content: TemplateRef<any> , item: IBidDetails) {
    this.jobQuotationId = item.jobQuotationId;
    this.tradesmanId = item.tradesmanId; 
    this._modalService.open(content, { centered: true });
  }
  public continueEsclateIssue() {
    this.common.GetData(this.common.apiRoutes.Jobs.getEscalateIssueByJQID + `?jobQuotationId=${this.jobQuotationId}&userRole=${AspnetRoles.CRole}&status=${BidStatus.StandBy}`, true).then(result => {
      let response: IResponse = result ;
      if (response.status == httpStatus.Ok) {
        this.submittedApplicationForm = true;
        if (this.appFormVal.valid) {
          var formData = this.appFormVal.value;
          let data: IEsclateObj = {
            esclateOptionId: formData.esclateOption,
            comment: formData.comment,
            tradesmanId: this.tradesmanId,
            customerId: this.decodedtoken.Id,
            jobQuotationId: this.jobQuotationId,
            status: BidStatus.StandBy,
            userRole: AspnetRoles.CRole,
            active: 1
          };
          this.common.PostData(this.common.apiRoutes.Jobs.submitIssue, data, true).then(res => {
            let response: IResponse = res;
            if (response.status == httpStatus.Ok) {
              this.responseMessage = true;
              setTimeout(() => {
                this.responseMessage = false;
                this._modalService.dismissAll();
              }, 4000)
            }
          });
        }
        else {
          this.appFormVal.markAllAsTouched();
          return;
        }
      }
      else {
        this.errorMessage = true;
        setTimeout(() => {
          this.errorMessage = false;
          this._modalService.dismissAll();
        }, 4000)
      }
    });
  }

  public closeModal() {
    this.appFormVal.reset();
    this.responseMessage = false;
    this.errorMessage = false;
    this._modalService.dismissAll();
  }
  public getAcceptedBidsList() {
    
    this.Loader.show();
    let obj = {
      CustomerId: this.decodedtoken.Id,
      PageNumber: this.pageNumber,
      PageSize: this.pageSize
    };

    this.common.post(this.common.apiRoutes.Jobs.GetAcceptedBidsList, obj).subscribe(response => {
      let res: IResponse = <IResponse>response;
      if (res) {
        this.acceptedBidsList = res.resultData;
        console.log(this.acceptedBidsList);
        this.getEsclateOption();
        this.Loader.hide();
      }
      else {
        this.Loader.hide();
      }
    });
  }
  public PopulateData() {
    this.Loader.show();
    this.common.GetData(this.common.apiRoutes.Jobs.SpGetPostedJobsByCustomerId + "?pageNumber=" + 1 + "&pageSize=" + 100 + "&statusId=" + 1 + "&bidStatus=" + true, true).then(result => {
      if (status = httpStatus.Ok) {
        var data: GetPostedJobs[] = result ;
        if (data) {
          this.postedJob = data;
          console.log(this.postedJob);
          this.Loader.hide();
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

  public acceptedBidJobDetail(jobQuotationId: number, bidId: number) {
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.GetPostedJobDetail, { queryParams: { jobQuotationId: jobQuotationId, bidId: bidId } });
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
