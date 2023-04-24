import { Component, OnInit, OnDestroy, ViewChild, TemplateRef, PLATFORM_ID, Inject } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';
import { AudioRecordingService } from '../../../shared/CommonServices/audioRecording.service';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { FormGroup, FormBuilder, Validators} from '@angular/forms';
import { BidErrorsMessage, BidStatus, httpStatus } from '../../../shared/Enums/enums';
import { ActivatedRoute, Params } from '@angular/router';
import { BidVM, AudioVM, jobDetails } from '../../../models/tradesmanModels/tradesmanModels';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { NgbModal} from '@ng-bootstrap/ng-bootstrap';
import { SocialAuthService } from 'angularx-social-login';
import { JwtHelperService } from '@auth0/angular-jwt';
import { MessagingService } from '../../../shared/CommonServices/messaging.service';
import { IPersonalDetails } from '../../../shared/Interface/tradesman';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-makebid',
  templateUrl: './makeAndEditBid.component.html',
  styleUrls: ['./makeAndEditBid.component.css']
})
export class MakeAndEditBidComponent implements OnInit, OnDestroy {
  public isRecording = false;
  public recordedTime: string = "";
  public blobUrl: SafeUrl = "";
  audio: string="";
  public bidsErrors: any;
  audioURL: string="";
  public customerName: string="";
  public jobQuotaionId: number=0;
  public jobBudget: string="";
  public bidTitle: string="";
  public token: string | null="";
  public bidForm: FormGroup;
  public submitted: boolean = false;
  public bidVM: BidVM = {} as BidVM;;
  public audioVM: AudioVM =  {} as AudioVM;;
  public jobdetail: jobDetails = {} as jobDetails;
  public voiceMessage : boolean = false
  public submitBtn: boolean = false;
  public loggedUserDetails:IPersonalDetails;
  public userId: string = "";
  public userRole: string = "";
  public fireBaseId: string = "";
  public loginCheck: boolean = false;
  public isUserBlocked: boolean = false;
  public jobQuotes: string="";
  jwtHelperService: JwtHelperService = new JwtHelperService();
  public base64String: string="";
  @ViewChild('TradesmanGuidelines', { static: true }) AcceptBidsModal: ModalDirective;
  @ViewChild('verifyAccountMessageModal', { static: false }) verifyAccountMessageModal: ModalDirective;
  @ViewChild('blockAccountMessageModal', { static: false }) blockAccountMessageModal: ModalDirective;

  constructor(
    public common: CommonService,
    public audioRecordingService: AudioRecordingService,
    public sanitizer: DomSanitizer,
    private formBuilder: FormBuilder,
    private authService: SocialAuthService,
    private route: ActivatedRoute,
      public _modalService: NgbModal,
      private _messagingService: MessagingService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    if (isPlatformBrowser(this.platformId)) {
      window.scroll(0,0);
    }
    this.bidForm = {} as FormGroup;
    this.AcceptBidsModal = {} as ModalDirective;
    this.verifyAccountMessageModal = {} as ModalDirective;
    this.blockAccountMessageModal = {} as ModalDirective;
    this.loggedUserDetails = {} as IPersonalDetails;

  }
  ngOnInit() {
    this.route.queryParams.subscribe((param: Params) => {
      this.jobQuotes = param['jobQuotes'];
      var obj = JSON.parse(this.jobQuotes);
      console.log(obj);
      this.token = localStorage.getItem("auth_token");
      var decodedtoken = this.token != null ? this.jwtHelperService.decodeToken(this.token) : "";
      //console.log(decodedtoken);
      this.userId = decodedtoken.UserId
      this.userRole = decodedtoken.Role;
      this.fireBaseId = obj.firebaseClientId;
      var test = 's';
      this.bidVM.CustomerBudget = obj.budget;
      this.bidVM.JobTitle = obj.jobTitle;
      this.bidVM.JobQuotationId = obj.jobQuotationId;
      this.bidVM.CustomerId = obj.customerId;
      this.bidVM.BidsId = obj.bidId;
      this.bidVM.Amount = obj.tradesmanBid;
      this.bidVM.Comments = obj.tradesmanMessage;
      this.customerName = obj.customerName;
    });
    this.audioRecordingService.recordingFailed().subscribe(() => {
      this.isRecording = false;
    });
    this.audioRecordingService.getRecordedTime().subscribe((time) => {      
      this.recordedTime = time;
      if (this.recordedTime == '00:30' || this.recordedTime == '00:31' || this.recordedTime == "00:30") {
        this.stopRecording();
      }
    });
    this.audioRecordingService.getRecordedBlob().subscribe((data) => {
      this.blobUrl = this.sanitizer.bypassSecurityTrustUrl(URL.createObjectURL(data.blob));
      this.audioVM.FileName = data.title;
      var reader = new FileReader();
      var base64data;
      reader.readAsDataURL(data.blob);
      reader.onload = (event: any) => {
        base64data = reader.result;
        this.audioVM.Base64String = base64data ? base64data.toString():"";
      }
    });
    this.bidForm = this.formBuilder.group({
      Amount: ['', [Validators.required]],
      Comments: [''],
      CustomerBudget: [''],
    });
    this.bidsErrors = {
      amountErrors: BidErrorsMessage.AmountError
    }
    this.bidForm.patchValue(this.bidVM);
  //  this.checkUserStatus();
  }
  getLoggedUserDetails(userRole: string, userId: string) {
    this.common.get(this.common.apiRoutes.UserManagement.GetUserDetailsByUserRole + `?userRole=${userRole}&userId=${userId}`).subscribe(result => {
      this.loggedUserDetails = result;
      //this.sendNotification(this.loggedUserDetails.firstName, this.loggedUserDetails.lastName, this.fireBaseId);
    });
  }
  public checkUserStatus() {
    this.common.GetData(this.common.apiRoutes.Login.GetUserBlockStatus + "?userId=" + this.userId).then(result => {
      this.isUserBlocked = result 
      if (!this.isUserBlocked) {
        this.save();
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
    this.loggedUserDetails = {} as IPersonalDetails;
    localStorage.clear();
    this.authService.signOut();
    this.loginCheck = false;
    this.common.NavigateToRoute("");
  }
  public hideBlockModal() {
    this.blockAccountMessageModal.hide();
    this.logout();
  }
  get f() {
    return this.bidForm.controls;
  }
  public SubmitAndUpdateBid(voiceMessageRef: TemplateRef<any>) {
    this.stopRecording();
    this.submitted = true;
    var obj = this.bidForm.value;
    this.bidVM.Amount = obj.Amount;
    this.bidVM.Comments = obj.Comments;
    this.bidVM.StatusId = BidStatus.Active;
    this.audioVM.AudioId = 0;
    this.bidVM.Audio = this.audioVM;
    if (this.bidForm.valid) {
      this.common.PostData(this.common.apiRoutes.Tradesman.SubmitAndEditBid, this.bidVM, true).then(result => {      
        var response = result ;
        if (response.status == httpStatus.Ok) {
          //this.sendNotification(this.customerName, this.fireBaseId)
          this.AcceptBidsModal.hide();
          this._modalService.open(voiceMessageRef, { size: 'md', centered: true, backdrop: 'static', keyboard: false });
        }
      },
        error => {
          console.log(error);
        });
    }
  }
  sendNotification(customerName: string, firebaseId: string) {
    this._messagingService.sendMessage("Bid", `${customerName} placed bid on Job`, firebaseId);
  }
  public noVoiceMessage() {
    this._modalService.dismissAll();
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Tradesman.MyBids);
  }
  public confirm() {
    this.voiceMessage = true;
    this._modalService.dismissAll();
    this.common.GetData(this.common.apiRoutes.Tradesman.GetCustomerDetailsByIdWeb + "?id=" + this.bidVM.JobQuotationId, true).then(data => {
      this.jobdetail = data ;
      console.log(this.jobdetail);
      this.bidVM.BidsId = this.jobdetail.bidId;
    },
      error => {
        console.log(error);
      });
   
  }
  public sendVoiceMessage() {
    this.common.PostData(this.common.apiRoutes.Tradesman.SubmitBidsVoice, this.bidVM, true).then(result => {
      let res = result ;
      console.log(res);
      if (res.status == httpStatus.Ok) {
        this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Tradesman.MyBids);
      }
    });
  }
  public save() {
    this.submitted = true;
    if (this.bidForm.invalid) {
      this.bidForm.markAllAsTouched();
    }
    else {
      if (this.token != null) {
        this.submitBtn = true;
        this.common.GetData(this.common.apiRoutes.IdentityServer.GetPhoneNumberVerificationStatus + "?userId=" + this.token, true).then(result => {
          
          if (result  == true) {
            this.AcceptBidsModal.show();
            
          }
          else {
            this.verifyAccountMessageModal.show();
            this.submitBtn = false;
          }
         
        });
      }
      
    }
  }
  public closeVerifyAccountMessageModal() {
    this.verifyAccountMessageModal.hide();

  }
  public dataURItoBlob(dataURI: string) {
    const byteString = window.atob(dataURI);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8Array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) {
      int8Array[i] = byteString.charCodeAt(i);
    }
    const blob = new Blob([int8Array], { type: 'audio/wav' });
    return blob;
  }
  public blobToFile(theBlob: Blob, fileName: string) {
    
    var b: any = theBlob;
    b.lastModifiedDate = new Date();
    b.name = fileName;
    //Cast to a File() type
    return <File>theBlob;
  }
  public startRecording() {
    if (!this.isRecording) {
      this.isRecording = true;
      this.audioRecordingService.startRecording();
    }
  }
  public abortRecording() {
    if (this.isRecording) {
      this.isRecording = false;
      this.audioRecordingService.abortRecording();
    }
  }
  public stopRecording() {
    if (this.isRecording) {
      this.audioRecordingService.stopRecording();
      this.isRecording = false;
    }
  }
  public clearRecordedData() {
    this.blobUrl = "";
  }
  ngOnDestroy(): void {
    this.abortRecording();
  }
  public JobDetail() {
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Tradesman.LiveLeadsDeatils, { queryParams: { jobDetailId: this.bidVM.JobQuotationId, PageName: 'Liveleads'} });
  }
  public hideModal() {
    this.AcceptBidsModal.hide();
  }
  AllowNonZeroIntegers(e: KeyboardEvent): boolean {
    
    var val = e.keyCode;
    // var target = event.target ? event.target : event.srcElement;
    if (val == 48 && (<HTMLInputElement>e.target).value == "" || val == 101 || val == 45 || val == 46 || ((val >= 65 && val <= 90)) || ((val >= 97 && val <= 122))) {
      return false;
    }
    else if ((val >= 48 && val < 58) || ((val > 96 && val < 106)) || val == 46 || val == 8 || val == 127 || val == 189 || val == 109 || val == 9) {
      return true;
    }
    else {
      return false;
    }
  }

}
