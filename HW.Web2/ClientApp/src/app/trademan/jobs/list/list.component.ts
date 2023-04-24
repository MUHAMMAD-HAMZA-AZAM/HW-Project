import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';
import { AspnetRoles, BidStatus, httpStatus } from '../../../shared/Enums/enums';
import { ModalDirective } from 'ngx-bootstrap/modal';
import * as $ from 'jquery';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IEsclateOption, IJobDetailWebVM } from '../../../shared/Interface/tradesman';
import { ResponseVm } from '../../../models/commonModels/commonModels';
@Component({
  selector: 'app-jobs',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class JobListComponent implements OnInit {
  public activeAds: IJobDetailWebVM[] = [];
  public activeConcatAds: IJobDetailWebVM[] = [];
  public inActiveAds: IJobDetailWebVM[] = [];
  public inActiveConcatAds: IJobDetailWebVM[] = [];
  public activeCount = 1;
  public inActiveCount = 1;
  public decodedtoken: any;
  private appFormVal: FormGroup;
  public customerId: number = 0;
  private tradesmanId: number = 0;
  private jobQuotationId: number = 0;
  public esclateOption: IEsclateOption[] = [];
  public errorMessage = false;
  public notFound: boolean = false;
  public submittedApplicationForm = false;
  public responseMessage = false;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  @ViewChild('FeedbackModel', { static: true }) FeedbackModel: ModalDirective;


  constructor(public common: CommonService, public fb: FormBuilder, private _modalService: NgbModal,
  ) {
    this.appFormVal = {} as FormGroup;
    this.FeedbackModel = {} as ModalDirective;
  }

  ngOnInit() {
    var token = localStorage.getItem("auth_token");
    this.decodedtoken = token != null ? this.jwtHelperService.decodeToken(token) : "";
    this.appFormVal = this.fb.group({
      esclateOption: ['', Validators.required],
      comment: ['', [Validators.required]]
    });
    this.activejobs(this.activeCount);
    this.completedjobs(this.inActiveCount);
    let cflag = localStorage.getItem("cFlag");
    if (cflag) {
      $("#activelink").removeClass("active");
      $("#activejobs").removeClass("active");
      $("#inactivelink").addClass("active");
      $("#completedjobs").addClass("active");
    }
    localStorage.removeItem("cFlag");
  }
  get f() {
    return this.appFormVal.controls;
  }
  public getEsclateOption() {

    this.common.get(this.common.apiRoutes.Jobs.getEscalateOptions + `?userRole=${AspnetRoles.TRole}`).subscribe(res => {
      this.esclateOption = <IEsclateOption[]>res;
    });
  }
  showEsclateModal(content: TemplateRef<any>, item: IJobDetailWebVM) {
    this.jobQuotationId = item.jobQuotationId;
    this.customerId = item.customerId;
    this._modalService.open(content, { centered: true });
  }
  public continueEsclateIssue() {
    this.common.GetData(this.common.apiRoutes.Jobs.getEscalateIssueByJQID + `?jobQuotationId=${this.jobQuotationId}&userRole=${AspnetRoles.TRole}&status=${BidStatus.Accepted}`, true).then(result => {
      let response = <ResponseVm>result;
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
            status: BidStatus.Accepted,
            userRole: AspnetRoles.TRole,
            active: 1
          };
          this.common.PostData(this.common.apiRoutes.Jobs.submitIssue, data, true).then(res => {
            let response = <ResponseVm>res;
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
  public activejobs(count: number) {

    this.common.GetData(this.common.apiRoutes.Tradesman.GetJobDetailWeb + "?jobStatusId=" + BidStatus.Active + "&pageNumber=" + count + "&pageSize=" + 10, true).then(data => {
      let response = <ResponseVm>data;
      if (response.status == httpStatus.Ok) {
        this.activeAds = response.resultData;
        if (this.activeAds) {
          this.activeConcatAds = [...this.activeConcatAds, ...this.activeAds];
          this.getEsclateOption();
          this.notFound = false;
        }
      }
     
    },
      error => {
        console.log(error);
      });
  }


  public completedjobs(count: number) {
    this.common.GetData(this.common.apiRoutes.Tradesman.GetJobDetailWeb + "?jobStatusId=" + BidStatus.Completed + "&pageNumber=" + count + "&pageSize=" + 10, true).then(data => {
      let response = <ResponseVm>data;
      if (response.status == httpStatus.Ok) {
        this.inActiveAds = response.resultData;
        if (this.inActiveAds) {
          this.inActiveConcatAds = [...this.inActiveConcatAds, ...this.inActiveAds];
          this.notFound = false;
        }
      }
      
    },
      error => {
        console.log(error);
      });
  }

  public onScroll() {
    this.activejobs(++this.activeCount);
  }
  public onScrollCompletedjobs() {
    this.completedjobs(++this.inActiveCount);
  }

  public JobDetail(adId: number,jqId:number) {
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Tradesman.ViewDetialJobs, { queryParams: { jobDetailId: adId,jqId} });
  }

  public FeedbackPopup(JobDetailID: number) {
    this.common.GetData(this.common.apiRoutes.Tradesman.FeedbackRequest + "?jobDetailId=" + JobDetailID, true).then(data => {
      var result = data;
      if (result.status == httpStatus.Ok)
        this.FeedbackModel.show();
    },
      error => {
        console.log(error);
      });

  }

  public CloseFeedback() {
    this.FeedbackModel.hide();
  }

  public RewiewJob(jobDetailId: number) {
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Tradesman.ReviewCompletedJob, { queryParams: { jobDetailId: jobDetailId } });
  }
}
