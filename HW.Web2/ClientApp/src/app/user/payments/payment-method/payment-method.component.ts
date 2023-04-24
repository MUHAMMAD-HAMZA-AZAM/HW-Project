import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';
import { ActivatedRoute, Params } from '@angular/router';
import { httpStatus, CommonErrors, BidStatus } from '../../../shared/Enums/enums';
import { Customer, InProgressJobList, PromotionRedemptionsVM, PromotionTypeVM, ReceivedBidVM, VoucherVM } from '../../../models/userModels/userModels';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { strict } from 'assert';
import { empty } from 'rxjs';
import { ResponseVm } from '../../../models/commonModels/commonModels';
import { stringify } from 'querystring';
import { Local } from 'protractor/built/driverProviders';
import { ToastrService } from 'ngx-toastr';
import { debug } from 'console';
import { jobDetails, MarkAsFinished } from '../../../models/tradesmanModels/tradesmanModels';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { IAdditionalChargesObj, IApplicationSetting } from '../../../shared/Enums/Interface';


@Component({
  selector: 'app-payment-method',
  templateUrl: './payment-method.component.html',
  styleUrls: ['./payment-method.component.css']
})
export class PaymentMethodComponent implements OnInit {
  public bidId: number = 0;
  public otherCharges: number = 0;
  public jobDetailId: number = 0;
  public tradesmanId: number = 0;
  public discountedPrice: number | undefined = 0;
  public finalPrice: number = 0;
  public walletAmount: number = 0;
  public useWallet_Amount: number = 0;
  public profile: Customer = {} as Customer;
  public finalPayment: number | undefined = 0;
  public hide: boolean = true;
  public additionalhide: boolean = true;
  public walletDiv: boolean = true;
  public token: string | null = "";
  public bidDetail: ReceivedBidVM = {} as ReceivedBidVM;
  public promotionTypes: PromotionTypeVM[] = [];
  public voucherList: VoucherVM[] = [];
  public promotionalCodeValue: PromotionTypeVM = {} as PromotionTypeVM;
  public voucherCodeValue: VoucherVM = {} as VoucherVM;
  public promotionalRedemption: PromotionRedemptionsVM = {} as PromotionRedemptionsVM;
  public response: ResponseVm = {} as ResponseVm;
  public isDiscountAvail: boolean = false;
  public paymentMethod: any;
  public existingPromoCode: boolean = false;
  public invalidPromoCode: boolean = false;
  public existingVoucherCode: boolean = false;
  public invalidVoucherCode: boolean = false;
  public redeemValue: boolean = false;
  public isPayment: boolean = false;
  public isFreeJob: boolean = false;
  public redeemBtn: boolean = false;
  public walletBtn: boolean = true;
  public job_price: number | undefined = 0;
  public ResponseCode: string = "";
  public fromWallet: boolean = false;
  public pp_ResponseMessage: string = "";
  public isPaymentStatus: boolean = false;
  public getBidId: any;
  public inProgressJobList: InProgressJobList[] = [];
  public markAsFinished: MarkAsFinished = {} as MarkAsFinished;
  public jobdetail: jobDetails = {} as jobDetails;
  public settingList: IApplicationSetting[] = [];
  public isActivePromotionRedempetion: boolean = false;
  public isActiveVoucherRedemption: boolean = false;
  public appFormVal1: FormGroup;
  public previousAmount: number | undefined = 0;
  public additionalAmount: number = 0;
  public finalBudget: number = 0;
  public discountPrice: number = 0;
  @ViewChild('ConfirmQuotes', { static: true }) ConfirmQuotes: ModalDirective;

  constructor(public common: CommonService, private route: ActivatedRoute, private modalService:
    NgbModal, private toaster: ToastrService, private Loader: NgxSpinnerService, private formBuilder: FormBuilder
  ) {
    this.appFormVal1 = {} as FormGroup;
    this.ConfirmQuotes = {} as ModalDirective;
  }

  ngOnInit() {

    this.token = localStorage.getItem("auth_token");
    this.route.queryParams.subscribe((params: Params) => {
      this.bidId = params['bidId'];
      this.otherCharges = Number(params['otherCharges']);
      this.jobDetailId = Number(params['jobDetailId']);
      if (this.jobDetailId) {
        localStorage.setItem("jobDetailId", JSON.stringify(this.jobDetailId));
      }

      this.tradesmanId = Number(params['tradesmanId']);
      if (this.bidId == undefined || this.bidId <= 0) {
        var bidId = localStorage.getItem("bidId");
        var res = localStorage.getItem("promotionalRedemption");
        var wAmount = localStorage.getItem("useWallet_Amount");
        this.useWallet_Amount = wAmount != null ? parseFloat(wAmount) : 0;
        this.promotionalRedemption = res != null ? JSON.parse(res) : {} as PromotionRedemptionsVM;
        this.bidId = bidId != null ? parseInt(bidId) : 0;
      }
      this.getBidId = localStorage.setItem("getBidId", JSON.stringify(this.bidId));
      if (this.bidId > 0) {
        //localStorage.removeItem("promotionalRedemption");
        //localStorage.removeItem("useWallet_Amount");
        //localStorage.removeItem("previousAmount");
        //localStorage.removeItem("additionalAmount");
        this.PopulateData();
        this.getVouchers();
        this.getWalletRecord();
      }
    });
    this.route.queryParams.subscribe((params: Params) => {
      this.ResponseCode = params['responseCode'];
      this.pp_ResponseMessage = params['responseMessage'];
      if (this.ResponseCode) {
        if (this.ResponseCode == '000') {
          this.paymentMethod = 5;

          this.AcceptBid();
        }
        else {
          localStorage.removeItem("useWallet_Amount");
          this.isPaymentStatus = true;
          setTimeout(() => {
            this.isPaymentStatus = false;
          }, 8000);
        }
      }
    });
    this.getSettingList();

    this.appFormVal1 = this.formBuilder.group({
      addtionalCharges: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
    });
  }
  get g() {
    return this.appFormVal1.controls;
  }
  public startJob() {
    //if (this.appFormVal1.value.addtionalCharges == "") {
    //  this.appFormVal1.controls["addtionalCharges"].setValidators([Validators.required]);
    //  this.appFormVal1.controls["addtionalCharges"].updateValueAndValidity();
    //}
    if (this.appFormVal1.valid) {

      if (this.useWallet_Amount > 0) {
        this.useWallet_Amount = 0;
        this.walletBtn = false;
        this.isFreeJob = false;
        this.fromWallet = true;

      }

      this.additionalhide = false;
      this.previousAmount = this.bidDetail.tradesmanOffer != undefined ? this.bidDetail.tradesmanOffer : 0;
      this.additionalAmount = Number(this.appFormVal1.value.addtionalCharges);
      localStorage.setItem("previousAmount", this.previousAmount.toString());
      localStorage.setItem("additionalAmount", this.additionalAmount.toString());
      this.job_price = this.previousAmount + this.additionalAmount;
      this.finalBudget = this.job_price;
      // this.bidDetail.tradesmanOffer = this.job_price;
      if (this.discountPrice > 0) {

        this.finalPrice = this.job_price - this.discountPrice;
        if (this.finalPrice > 0) {
          this.finalPayment = this.job_price - this.discountPrice;
          this.job_price = this.finalPayment;
          this.walletBtn = false;
          this.isFreeJob = false;
          this.fromWallet = false;

        }
        else {
          this.fromWallet = true;
          this.walletBtn = true;
          this.isFreeJob = true;
          this.finalPayment = 0;
          this.job_price = 0;
        }

      }
      else {
        this.fromWallet = false;
        this.finalPayment = this.job_price;
        this.getPromotionTypes();
        this.getVouchers();
      }


      this.modalService.dismissAll();


    }
    else {
      return;
    }

  }
  public getSettingList() {
    this.common.get(this.common.apiRoutes.UserManagement.GetSettingList).subscribe(res => {
      this.settingList = <IApplicationSetting[]>res;
      if (this.settingList.length > 0) {
        this.settingList.forEach(x => {
          if (x.settingName == "Promotion" && x.isActive) {
            this.isActivePromotionRedempetion = true;
          }
          else if (x.settingName == "Voucher" && x.isActive) {
            this.isActiveVoucherRedemption = true;
          }
        })
      }
    })
  }

  public getWalletRecord() {
    this.common.get(this.common.apiRoutes.Users.CustomerProfile).subscribe(result => {
      if (status = httpStatus.Ok) {
        this.profile = <Customer>result;
        this.common.GetData(this.common.apiRoutes.PackagesAndPayments.GetLedgerTransactionCustomer + "?refercustomerId=" + this.profile.entityId, true).then(result => {
          if (status = httpStatus.Ok) {
            ;
            var data = result ;
            //this.walletAmount = data.resultData;
            if (data.resultData > 0) {
              this.walletAmount = data.resultData;
              this.walletDiv = false;
              this.walletBtn = false;
              this.getInprogressJob();
            }
            else {
              this.walletDiv = true;
            }
            this.walletAmount = data.resultData;
          }
        });
      }
    });

  }
  getInprogressJob() {
    this.common.GetData(this.common.apiRoutes.Jobs.GetAlljobDetails + "?statusId=" + BidStatus.Accepted, true).then(result => {
      if (status = httpStatus.Ok) {
        this.inProgressJobList = result ;
        ;
        if (this.inProgressJobList.length > 0) {
          this.walletDiv = false;
        }
        else {
          this.walletDiv = false;
        }

      }
    });
  }

  public PopulateData() {
    this.common.GetData(this.common.apiRoutes.Jobs.GetBidDetails + "?bidId=" + this.bidId, true).then(result => {
      if (status = httpStatus.Ok) {
        this.bidDetail = result ;
        localStorage.removeItem("promotionalRedemption");
        localStorage.removeItem("useWallet_Amount");
        localStorage.removeItem("previousAmount");

        this.finalPayment = this.bidDetail.tradesmanOffer;
        this.previousAmount = this.bidDetail.tradesmanOffer;
        localStorage.setItem("previousAmount", this.previousAmount.toString());
        this.job_price = this.bidDetail.tradesmanOffer;
        this.bidDetail.jobQuotationId = this.bidDetail.jobQuotationId != undefined ? this.bidDetail.jobQuotationId : 0;
        localStorage.setItem("jobQuotationId", this.bidDetail.jobQuotationId.toString());
        this.getPromotionTypes();
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }
  public getPromotionTypes() {
    var url = this.common.apiRoutes.PackagesAndPayments.GetPromotionTypes;
    this.common.GetData(url, true).then(result => {
      if (status = httpStatus.Ok) {
        this.promotionTypes = result ;
        if (this.promotionTypes.length > 0) {

          this.common.get(this.common.apiRoutes.UserManagement.GetSettingList).subscribe(res => {
            this.settingList = <IApplicationSetting[]>res;
            if (this.settingList.length > 0) {
              this.settingList.forEach(x => {
                if (x.settingName == "Promotion On Category" && x.isActive) {

                  this.promotionTypes.forEach(a => {
                    if (a.categoryId == this.bidDetail.skillId) {
                      this.promotionalCodeValue = a;
                    }
                  });
                  if (this.promotionalCodeValue.promotionTypeId != null) {
                    this.promotionalRedemption.promotionId = this.promotionalCodeValue.promotionTypeId;
                    this.promotionalRedemption.redeemBy = "";
                    this.promotionalRedemption.redeemOn = new Date();
                    this.promotionalRedemption.totalDiscount = this.promotionalCodeValue.amount;
                    this.promotionalRedemption.jobQuotationId = this.bidDetail.jobQuotationId;
                    this.promotionalRedemption.isVoucher = false;
                    this.promotionalRedemption.categoryId = this.promotionalCodeValue.categoryId

                    localStorage.setItem("promotionalRedemption", JSON.stringify(this.promotionalRedemption));
                    this.isDiscountAvail = true;
                    this.discountedPrice = this.promotionalCodeValue.amount;
                    this.finalPrice = this.job_price && this.promotionalCodeValue?.amount != undefined ? this.job_price - this.promotionalCodeValue?.amount : 0;

                    if (this.finalPrice <= 0) {
                      this.finalPayment = 0;
                      this.job_price = 0;
                      this.hide = false;
                      this.isFreeJob = true;
                      this.walletBtn = true;

                    }
                    else {
                      this.finalPayment = this.finalPrice;
                      this.job_price = this.finalPrice;
                      this.hide = false;
                      this.isFreeJob = false;
                    }
                  }


                }

              })
            }
          })
        }
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    });
  }
  showModal(content: TemplateRef<any>) {
    console.log(content);


    this.modalService.open(content, { centered: true, size: 'md', backdrop: 'static', keyboard: false }).result.then(response => {
      this.modalService.dismissAll();

    })
  }
  public getVouchers() {

    var url = this.common.apiRoutes.PackagesAndPayments.GetVoucherList;
    this.common.GetData(url, true).then(result => {


      if (status = httpStatus.Ok) {
        this.voucherList = result ;

        //console.log(this.voucherList);
      }
      else {

        this.common.Notification.error("Some thing went wrong.");
      }
    });
  }
  public AcceptBid() {

    if (this.paymentMethod == "1") {
      this.Loader.show();
      var aa = this.bidId;
      localStorage.setItem("bidId", this.bidId.toString());
      var origin = window.location.origin;
      var d = new Date();
      var n = d.toString();
      var datTime = new Date(n).getTime() / 1000;
      var PaymentRefNo = "JP" + datTime;
      let job_price = this.job_price ? this.job_price : 0;
      window.location.href = origin + "/Message/JazzCashPayment?amount=" + job_price * 100 + "&paymentRefNo=" + PaymentRefNo + "&billReference=" + this.bidDetail.jobQuotationId + "&transactionType=" + "MWALLET";
    }
    else {
      if (this.promotionalRedemption) {

        this.common.PostData(this.common.apiRoutes.PackagesAndPayments.AddPromotionRedemptions, this.promotionalRedemption).then(result => {
          
          this.response = result ;
        });
      }

      if (this.paymentMethod == 6) {
        this.paymentMethod = 3;
      }
      if (this.paymentMethod == 5) {
        var jobPreviousAmount = localStorage.getItem("previousAmount");
        this.previousAmount = Number(jobPreviousAmount);
        this.paymentMethod = 1;
      }
      if (this.useWallet_Amount > 0) {
        let jobQuotationId = this.bidDetail.jobQuotationId != undefined ? this.bidDetail.jobQuotationId : 0;
        if (jobQuotationId > 0) {
          this.common.GetData(this.common.apiRoutes.PackagesAndPayments.AddLeaderTransactionForCreditUser + "?walletValue=" + this.useWallet_Amount + "&jobqoutationId=" + this.bidDetail.jobQuotationId, true).then(result => {
            this.response = result ;
          });
        }
        else {
          var jobQuotation = localStorage.getItem("jobQuotationId");
          jobQuotation != null ? this.common.GetData(this.common.apiRoutes.PackagesAndPayments.AddLeaderTransactionForCreditUser + "?walletValue=" + this.useWallet_Amount + "&jobqoutationId=" + parseInt(jobQuotation), true).then(result => {
            this.response = result ;
          }) : "";
        }

      }
      if (Number(this.previousAmount) > 0) {
        this.updateJobAdditioanlCharges();
      }
      else {

        var jobPreviousAmount = localStorage.getItem("previousAmount");
        var jobAdditionalAmount = localStorage.getItem("additionalAmount");
        this.previousAmount = Number(jobPreviousAmount);
        this.additionalAmount = Number(jobAdditionalAmount);
        if (this.previousAmount == 0) {
          this.previousAmount = this.job_price;
          this.additionalAmount = 0;

        }
        this.updateJobAdditioanlCharges();

      }


      //this.common.GetData(this.common.apiRoutes.Jobs.AddJobDetails + "?bidId=" + this.bidId + "&paymentMethod=" + this.paymentMethod + "&statusId=" + BidStatus.Accepted, true).then(result => {
      //  if (status = httpStatus.Ok) {
      //    this.CloseModel();
      //    var data = result ;
      //    this.common.Notification.success(data.message);
      //    this.common.NavigateToRoute(this.common.apiUrls.User.InProgressJobList)
      //  }
      //}, error => {
      //  console.log(error);
      //  this.common.Notification.error(CommonErrors.commonErrorMessage);
      //});


    }




  }
  public updateJobAdditioanlCharges() {
    var jobQuotation = localStorage.getItem("jobQuotationId");
    var jobAdditionalAmount = localStorage.getItem("additionalAmount");
    this.additionalAmount = Number(jobAdditionalAmount);
    var id = jobQuotation != null ? parseInt(jobQuotation) : 0;
    let obj: IAdditionalChargesObj = {
      paymentMethod: this.paymentMethod,
      bidId: this.bidId,
      jobQuotationId: this.bidDetail.jobQuotationId ? this.bidDetail.jobQuotationId : id,
      tradesmanOffer: this.previousAmount ? this.previousAmount : 0,
      otherCharges: this.additionalAmount
    }
    localStorage.removeItem("additionalAmount");

    this.common.post(this.common.apiRoutes.Jobs.UpdateJobAdditionalCharges, obj).subscribe(res => {
      let response = <any>res ;
      
      if (response.status == httpStatus.Ok) {
        this.getJobDetailsById();
      }
    })
  }
  public getJobDetailsById() {
    let jobDetailId = localStorage.getItem("jobDetailId");
    let jobId = jobDetailId != null ? JSON.parse(jobDetailId) : "";
    var jobQuotation = localStorage.getItem("jobQuotationId");
    var id = jobQuotation != null ? parseInt(jobQuotation) : 0;

    this.common.GetData(this.common.apiRoutes.Tradesman.GetJobDetailsByIdWeb + "?jobDetailId=" + jobId, true).then(data => {
      this.jobdetail = data ;
      //this.markAsFinished.BidId = this.jobdetail.bidId;
      this.markAsFinished.BidId = this.getBidId;
      this.markAsFinished.CustomerId = this.jobdetail.customerId;
      this.markAsFinished.EndDate = new Date();
      this.markAsFinished.isPaymentDirect = true;
      this.markAsFinished.JobDetailId = this.jobdetail.jobDetailId;
      this.markAsFinished.JobQuotationId = this.jobdetail.jobQuotationId > 0 ? this.jobdetail.jobQuotationId : id;
      this.markAsFinished.StartDate = new Date(); /*this.jobdetail.expectedJobStartDate*/;
      this.markAsFinished.StatusId = BidStatus.Completed;
      this.markAsFinished.TradesmanId = this.tradesmanId;
      this.common.PostData(this.common.apiRoutes.Tradesman.MarkAsFinishedJob, this.markAsFinished).then(response => {
        let res = response ;
        if (res) {
          this.common.NavigateToRoute(this.common.apiUrls.User.GetFinishedJobs);
        }
        else {
          this.common.NavigateToRoute(this.common.apiUrls.User.GetFinishedJobs);

        }
      })

    },
      error => {
        console.log(error);
      });
  }
  public Proceed() {
    if (this.paymentMethod) {
      this.isPayment = false;
      this.ConfirmQuotes.show();
    }
    else {
      this.isPayment = true;
    }
  }
  public RedeemBtn(redeemEntry: string) {
    
    if (redeemEntry != "") {
      var code = redeemEntry.substring(0, 3);
      var discount: number | undefined;
      if (code.toLowerCase() == "pro" && this.promotionTypes.length > 0) {
        if (this.isDiscountAvail == false) {
          ;
          this.promotionTypes.forEach(a => {

            if (a.promotionTypeCode?.toLowerCase() == redeemEntry.toLowerCase()) {
              this.promotionalCodeValue = a;
            }
          }
          );
          if (this.promotionalCodeValue.promotionTypeId != null) {
            this.promotionalRedemption.promotionId = this.promotionalCodeValue.promotionTypeId;
            this.promotionalRedemption.redeemBy = "";
            this.promotionalRedemption.redeemOn = new Date();
            this.promotionalRedemption.totalDiscount = this.promotionalCodeValue.amount;
            this.discountPrice = this.promotionalCodeValue.amount ? this.promotionalCodeValue.amount : 0;
            this.promotionalRedemption.jobQuotationId = this.bidDetail.jobQuotationId;
            this.promotionalRedemption.isVoucher = false;
            console.log(this.promotionalRedemption);
            this.common.PostData(this.common.apiRoutes.PackagesAndPayments.getRedemptionRecord, this.promotionalRedemption).then(result => {
              this.response = result ;
              if (this.response.status == httpStatus.Ok) {
                localStorage.setItem("promotionalRedemption", JSON.stringify(this.promotionalRedemption));
                this.isDiscountAvail = true;
                this.toaster.success("Promotion Applied", "Successfully!");
                this.discountedPrice = this.promotionalCodeValue.amount;
                this.finalPrice = this.job_price && this.promotionalCodeValue.amount ? this.job_price - this.promotionalCodeValue.amount : 0;

                if (this.finalPrice <= 0) {
                  this.finalPayment = 0;
                  this.job_price = 0;
                  this.hide = false;
                  this.isFreeJob = true;
                  this.walletBtn = true;
                }
                else {
                  this.finalPayment = this.finalPrice;
                  this.job_price = this.finalPrice;
                  this.hide = false;
                  this.isFreeJob = false;
                }
              }
              else if (this.response.status == httpStatus.Restricted) {

                window.alert('You are already used this Promotional code.');
              }
              else {
                this.common.Notification.error("Something Went Wrong.");
              }
            }, error => {
              console.log(error);
            });
          }
          else {
            this.invalidPromoCode = true;
            setTimeout(() => {
              this.invalidPromoCode = false;
            }, 2000);
          }
        }
        else {
          this.toaster.error("You already avail promotion discount.!!", 'warning');
          // this.common.Notification.error("You already avail discount by code.!!");
        }
      }
      else {
        if (this.voucherList.length > 0) {
          if (this.isDiscountAvail == false) {
            this.voucherList.forEach(a => {
              if (a.voucherNo?.toLowerCase() == redeemEntry.toLowerCase()) {
                this.voucherCodeValue = a;
                console.log(this.voucherCodeValue);
              }
            }
            );
            if (this.voucherCodeValue.voucherBookLeavesId != null) {
              if (this.voucherCodeValue.persentageDiscount != null) {
                discount = (this.job_price ? this.job_price : 0 * this.voucherCodeValue.persentageDiscount) / 100;
              }
              else {
                discount = this.voucherCodeValue.discountedAmount;
              }
              this.promotionalRedemption.voucherBookLeavesId = this.voucherCodeValue.voucherBookLeavesId;
              this.promotionalRedemption.redeemBy = "";
              this.promotionalRedemption.redeemOn = new Date();
              this.promotionalRedemption.totalDiscount = discount;
              this.discountPrice = discount ? discount : 0;
              this.promotionalRedemption.jobQuotationId = this.bidDetail.jobQuotationId;
              this.promotionalRedemption.isVoucher = true;

              this.common.PostData(this.common.apiRoutes.PackagesAndPayments.getRedemptionRecord, this.promotionalRedemption).then(result => {

                this.response = result ;
                if (this.response.status == httpStatus.Ok) {
                  localStorage.setItem("promotionalRedemption", JSON.stringify(this.promotionalRedemption));
                  //this.isDiscountAvail = true;
                  //setTimeout(() => {
                  //  this.isDiscountAvail = false;
                  //}, 2000);
                  this.discountedPrice = discount;
                  this.finalPrice = this.job_price && discount ? this.job_price - discount : 0;

                  if (this.finalPrice <= 0) {
                    this.finalPayment = 0;
                    this.job_price = 0;
                    this.hide = false;
                    this.isFreeJob = true;
                    this.walletBtn = true;
                  }
                  else {
                    this.finalPayment = this.finalPrice;
                    this.job_price = this.finalPrice;
                    this.hide = false;
                    this.isFreeJob = false;

                  }
                }
                else if (this.response.status == httpStatus.Restricted) {
                  //this.existingVoucherCode = true;
                  //setTimeout(() => {
                  //  this.existingVoucherCode = false;
                  //}, 2000);
                  window.alert('You are already used this Voucher code.');
                }
                else {
                  this.common.Notification.error("Something Went Wrong.");
                  // window.alert('Something Went Wrong.');
                }
              }, error => {
                console.log(error);
              });
            }

            else {
              this.invalidVoucherCode = true;
              setTimeout(() => {
                this.invalidVoucherCode = false;
              }, 2000);
              // window.alert('Invalid Voucher Code.');
            }
          }
          else {
            //this.isDiscountAvail = true;
            //setTimeout(() => {
            //  this.isDiscountAvail = false;
            //}, 2000);
            // window.alert('You already avail discount by code.');
            this.toaster.warning("You already avail discount by code", "Warning");
          }
        }
        else {
          this.invalidVoucherCode = true;
          setTimeout(() => {
            this.invalidVoucherCode = false;
          }, 2000);
        }

      }
    }
    else {
      this.redeemValue = true;
      setTimeout(() => {
        this.redeemValue = false;
      }, 2000);
    }

  }

  public CloseModel() {
    this.ConfirmQuotes.hide();
  }
  public walletRedeem() {
    this.walletBtn = true;
    let job_price = this.job_price ? this.job_price : 0
    if (this.walletAmount < job_price) {
      this.finalPayment = job_price - this.walletAmount;
      this.finalPrice = this.finalPayment;
      this.job_price = this.finalPrice;
      this.fromWallet = false;
      this.useWallet_Amount = this.walletAmount;
      localStorage.setItem("useWallet_Amount", this.useWallet_Amount.toString());
    }
    else {
      this.finalPayment = this.job_price;
      this.useWallet_Amount = job_price;
      this.job_price = 0;
      this.redeemBtn = false;
      this.fromWallet = true;
      this.isFreeJob = true;
      localStorage.setItem("useWallet_Amount", this.useWallet_Amount.toString());
    }
  }
  public AllowNonZeroIntegers(event: KeyboardEvent): boolean {
    var val = event.keyCode;
    // var target = event.target ? event.target : event.srcElement;
    if (val == 48 && (<HTMLInputElement>event.target).value == "" || val == 101 || val == 45 || val == 46 || ((val >= 65 && val <= 90)) || ((val >= 97 && val <= 122))) {
      return false;
    }
    else if ((val >= 48 && val < 58) || ((val > 96 && val < 106)) || val == 8 || val == 127 || val == 189 || val == 109 || val == 9) {
      return true;
    }
    else {
      return false;
    }
  }


}
