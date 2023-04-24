import { Component, OnInit, TemplateRef } from '@angular/core';
import { httpStatus, BidStatus, CommonErrors, AspnetRoles } from '../../../shared/Enums/enums';
import { CommonService } from '../../../shared/HttpClient/_http';
import { InProgressJobList } from '../../../models/userModels/userModels';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { get } from 'http';
import { ResponseVm } from '../../../models/commonModels/commonModels';
import { setTime } from 'ngx-bootstrap/chronos/utils/date-setters';
import { JwtHelperService } from '@auth0/angular-jwt';
import { IEsclateObj, IEsclateOption, IInProgressJobList, IResponse, IUpdateAdditionalChargesObj } from '../../../shared/Enums/Interface';


@Component({
  selector: 'app-inprogress',
  templateUrl: './list.component.html',
})
export class InProgressListComponent implements OnInit {
  public inProgressJobList: IInProgressJobList[] = [];
  public esclateOption: IEsclateOption[] = [];
  public appFormVal: FormGroup;
  public appFormVal1: FormGroup;
  public submittedApplicationForm: boolean = false;
  public jobQuotationId: number=0;
  public tradesmanId: number=0;
  public response: IResponse;
  public responeMessage: boolean = false;
  public errorMessage: boolean = false;
  public previousBudget: number=0;
  public inProgressDetails: any;
  public formSubmit: boolean = false;
  public decodedtoken: any;
  public notFound: boolean = false;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(
    public common: CommonService, private modalService: NgbModal, private formBuilder: FormBuilder
  ) {
    this.appFormVal = {} as FormGroup;
    this.appFormVal1 = {} as FormGroup;
    this.response = {} as IResponse;
  }
  ngOnInit() {
    var token = localStorage.getItem("auth_token");
    this.decodedtoken = token != null ? this.jwtHelperService.decodeToken(token):"";
    this.PopulateData();
    this.getEsclateOption();

    this.appFormVal = this.formBuilder.group({
      esclateOption: ['', Validators.required],
      comment: ['', [Validators.required]]
    });
    this.appFormVal1 = this.formBuilder.group({
      addtionalCharges: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],    });
  }

  get f() {
    return this.appFormVal.controls;
  }
  get g() {
    return this.appFormVal1.controls;
  }
  showModal(content: TemplateRef<any>, item: IInProgressJobList) {
    console.log(item);
    
    //this.inProgressDetails = item;
    //this.previousBudget = item.tradesmanOffer;
    //this.modalService.open(content, { centered: true, size: 'md', backdrop: 'static', keyboard: false }).result.then(response => {
    //  this.modalService.dismissAll();
      this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.Payments,
        { queryParams: { bidId: item.bidsId, otherCharges: 0, jobDetailId: item.jobDetailId, tradesmanId: item.tradesmanId } });
    
   // })
  }
  public startJob() {
    this.formSubmit = true;
    let item = this.inProgressDetails;
    //if (this.appFormVal1.value.addtionalCharges == "") {
    //  this.appFormVal1.controls["addtionalCharges"].setValidators([Validators.required]);
    //  this.appFormVal1.controls["addtionalCharges"].updateValueAndValidity();
    //}
    if (this.appFormVal1.valid) {
      let obj: IUpdateAdditionalChargesObj  = {
        bidId: item.bidsId,
        otherCharges: Number(this.appFormVal1.value.addtionalCharges),
        jobQuotationId: item.jobQuotationId,
        tradesmanOffer: item.tradesmanOffer,
        action: "beforePayment"
      }
      this.common.post(this.common.apiRoutes.Jobs.UpdateJobAdditionalCharges, obj).subscribe(res => {
        let response: IResponse = <IResponse>res;
        if (response.status == httpStatus.Ok) {
          this.modalService.dismissAll();
          this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.Payments,
            { queryParams: { bidId: item.bidsId, otherCharges: Number(this.appFormVal1.value.addtionalCharges), jobDetailId: item.jobDetailId, tradesmanId: item.tradesmanId } });
          this.appFormVal1.reset();
        }
      })
    }
    else {
      return;
    }

  }

  public PopulateData() {
    this.common.GetData(this.common.apiRoutes.Jobs.GetAlljobDetails + "?statusId=" + BidStatus.Accepted, true).then(result => {
      if (status = httpStatus.Ok) {
        this.inProgressJobList = result ;
        console.log(this.inProgressJobList);
        if (this.inProgressJobList.length > 0) {
          for (var i = 0; i < this.inProgressJobList.length; i++) {
            if (this.inProgressJobList[i].jobImage != null) {
              var imgURl = 'data:image/png;base64,' + this.inProgressJobList[i].jobImage;
              this.inProgressJobList[i].jobImage = imgURl.toString();
            }
          }
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

  public getEsclateOption() {
    this.common.get(this.common.apiRoutes.Jobs.getEscalateOptions + `?userRole=${AspnetRoles.CRole}`).subscribe(res => {
      this.esclateOption = <IEsclateOption[]>res;
      console.log(this.esclateOption);
    });
  }

  public continueEsclateIssue() {
    this.common.GetData(this.common.apiRoutes.Jobs.getEscalateIssueByJQID + `?jobQuotationId=${this.jobQuotationId}&userRole=${AspnetRoles.CRole}&status=${BidStatus.Accepted}`, true).then(r => {

      this.response = r ;
      if (this.response.status == httpStatus.Ok) {
        this.submittedApplicationForm = true;
        if (this.appFormVal.valid) {
          var formData = this.appFormVal.value;
          let data: IEsclateObj = {
            esclateOptionId: formData.esclateOption,
            comment: formData.comment,
            tradesmanId: this.tradesmanId,
            customerId: this.decodedtoken.Id,
            jobQuotationId: this.jobQuotationId,
            status: BidStatus.Accepted,
            userRole: AspnetRoles.CRole,
            active: 1
          };
          this.common.PostData(this.common.apiRoutes.Jobs.submitIssue, data, true).then(res => {

            this.response = res ;
            if (this.response.status = httpStatus.Ok) {
              this.responeMessage = true;
              setTimeout(() => {
                this.responeMessage = false;
                this.modalService.dismissAll();
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
          this.modalService.dismissAll();
        }, 4000)
      }
    });
  }

  public JobDetail(jobQuotationId: number) {
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.InProgreesJobDetails, { queryParams: { jobQuotationId: jobQuotationId } });
  }

  public closeModal() {
    this.appFormVal.reset();
    this.responeMessage = false;
    this.errorMessage = false;
    this.modalService.dismissAll();
  }

  open(content: TemplateRef<any> , item: IInProgressJobList) {
    this.jobQuotationId = item.jobQuotationId;
    this.tradesmanId = item.tradesmanId;
    this.modalService.open(content, { centered: true });
  }
}
