import { AfterViewChecked, Component, ElementRef, OnInit, QueryList, TemplateRef, ViewChild } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';
import { httpStatus, BidStatus, AspnetRoles } from '../../../shared/Enums/enums';
import { ActiveBidsVM } from '../../../models/tradesmanModels/tradesmanModels';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Events } from '../../../common/events';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IBidDetails, IBidWeb, IEsclateOption, ITradesmanObj } from '../../../shared/Interface/tradesman';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class MyBidsListComponent implements OnInit,AfterViewChecked {
  public ActiveBids: ActiveBidsVM[] = [];
  public DeclinBids: ActiveBidsVM[] = [];
  // public activeMybids: object[] = [];
  public activeConcatMybids: ActiveBidsVM[] = [];
  // public inActiveMybids: object[] = [];
  public inActiveConcatMybids: ActiveBidsVM[] = [];
  public acceptedBidsList: IBidDetails[] = [];
  public decodedtoken: any="";
  public customerId: number=0;
  public activePageNumber = 1;
  public inActivePageNumber = 1;
  private pageSize: number = 1;
  private appFormVal: FormGroup;
  private tradesmanId: number=0;
  private jobQuotationId: number=0;
  public esclateOption:IEsclateOption[]= [];
  public errorMessage = false;
  public submittedApplicationForm = false;
  public responseMessage = false;
  public strtJobBtn: boolean = false;
  
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(
    public common: CommonService,
    private events: Events,
    private _modalService: NgbModal,
    public Loader: NgxSpinnerService,
    public fb: FormBuilder,
  ) {
    this.appFormVal = {} as FormGroup;
  }

  ngOnInit() {
    var token = localStorage.getItem("auth_token");
    this.decodedtoken = token != null ? this.jwtHelperService.decodeToken(token):"";
    this.appFormVal = this.fb.group({
      esclateOption: ['', Validators.required],
      comment: ['', Validators.required]
    });
    this.PopulateData();
  }
  ngAfterViewChecked() {

  }
  get f() {
    return this.appFormVal.controls;
  }
  public getEsclateOption() {
    this.common.get(this.common.apiRoutes.Jobs.getEscalateOptions + `?userRole=${AspnetRoles.TRole}`).subscribe(res => {
      this.esclateOption = <IEsclateOption[]>res ;
    });
  }
  showEsclateModal(content: TemplateRef<any>, item: IBidDetails) {
    this.jobQuotationId = item.jobQuotationId;
    this.customerId = item.customerId;
    this._modalService.open(content, { centered: true });
  }
  public continueEsclateIssue() {
    this.common.GetData(this.common.apiRoutes.Jobs.getEscalateIssueByJQID + `?jobQuotationId=${this.jobQuotationId}&userRole=${AspnetRoles.TRole}&status=${BidStatus.StandBy}`, true).then(result => {
      let response = result ;
      if (response.status == httpStatus.Ok) {
        this.submittedApplicationForm = true;
        if (this.appFormVal.valid) {
          var formData = this.appFormVal.value;
          let data = {
            esclateOptionId: formData.esclateOption,
            comment: formData.comment,
            customerId: this.customerId,
            tradesmanId: this.decodedtoken.Id,
            jobQuotationId: this.jobQuotationId,
            status: BidStatus.StandBy,
            userRole: AspnetRoles.TRole,
            active: 1
          };
          this.common.PostData(this.common.apiRoutes.Jobs.submitIssue, data, true).then(res => {
            let response = res;
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
  public PopulateData() {
    
    this.ActiveBidsList(this.activePageNumber);
    this.DeclineBidsList(this.inActivePageNumber);
    this.getAcceptedBidsList();
  }
  public getAcceptedBidsList() {
    
    this.Loader.show();
    let obj = {
      TradesmanId: this.decodedtoken.Id
    }
    this.common.post(this.common.apiRoutes.Jobs.GetAcceptedBidsList, obj).subscribe(response => {
      let res = <any>response ;
      
      if (res) {
        this.acceptedBidsList = res.resultData;
        this.getEsclateOption();
        this.Loader.hide();
      }
      else {
        this.Loader.hide();
      }
    });
  }
  showModal(content: TemplateRef<any>,obj: object) {
    this._modalService.open(content, { centered: true, size: 'md', backdrop: 'static', keyboard: false }).result.then(response => {
      console.log(response);
    }, (reason) => {
      this.startJob(obj)
    })
  }
  public startJob(obj: any) {
    let startJobobj = {
      jobQuotationId: obj.jobQuotationId,
      tradesmanId: obj.tradesmanId,
      action: "start"
    }
    this.common.PostData(this.common.apiRoutes.Jobs.StartOrFinishJob, startJobobj, true).then(response => {
      let res = response;
      if (res.status == httpStatus.Ok) {
        this.common.get(this.common.apiRoutes.Notifications.NotifyJobStartFromTradesman + `?tradesmanId=${obj.tradesmanId}&customerId=${obj.customerId}&bidId=${obj.bidId}&jobTitle=${obj.jobTitle}&jobQuotationId=${obj.jobQuotationId}`).subscribe(response => {
          this.getAcceptedBidsList();
        })
      }
    })
  }
  public ActiveBidsList(pageNumber: number) {
    this.common.GetData(this.common.apiRoutes.Tradesman.GetActiveBidsDetailsWeb + "?pageNumber=" + pageNumber + "&pageSize=" + 10 + "&bidsStatusId=" + BidStatus.Active, true).then(result => {
      if (status = httpStatus.Ok) {
        this.ActiveBids = result ;
        if (this.ActiveBids != null) {
          for (var i = 0; i < this.ActiveBids.length; i++) {
            if (this.ActiveBids[i].bidImage != null) {
              var imgURl = 'data:image/png;base64,' + this.ActiveBids[i].bidImage;
              this.ActiveBids[i].bidImage = imgURl.toString();
            }
          }
          this.activeConcatMybids = [...this.activeConcatMybids, ...this.ActiveBids];
        }
      }
    },
      error => {
        console.log(error);
        this.common.Notification.error("Some thing went wrong.");
      });
  }
  public DeclineBidsList(pageNumber: number) {
    this.common.GetData(this.common.apiRoutes.Tradesman.GetDeclinedBidsDetailsWeb + "?pageNumber=" + pageNumber + "&pageSize=" + 10 + "&bidsStatusId=" + BidStatus.Declined, true).then(result => {
      if (status = httpStatus.Ok) {

        this.DeclinBids = result ;
        if (this.DeclinBids != null) {
          for (var i = 0; i < this.DeclinBids.length; i++) {
            if (this.DeclinBids[i].bidImage != null) {
              var imgURl = 'data:image/png;base64,' + this.DeclinBids[i].bidImage;
              this.DeclinBids[i].bidImage = imgURl.toString();
            }
          }
          this.inActiveConcatMybids = [...this.inActiveConcatMybids, ...this.DeclinBids];
        }
      }
    },
      error => {
        console.log(error);
      });
  }

  public OnScrollActiveBids() {
    this.ActiveBidsList(++this.activePageNumber);
  }
  public OnScrollInActiveBids() {
    this.DeclineBidsList(++this.inActivePageNumber);
  }
  public JobDetail(jqId: number, pageName: string) {
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Tradesman.LiveLeadsDeatils, { queryParams: { jobDetailId: jqId, PageName: pageName } });
  }

  public DeclineJobDetail(jqId: number, pageName:string) {
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Tradesman.LiveLeadsDeatils, { queryParams: { jobDetailId: jqId, PageName: pageName } });

  }
}
