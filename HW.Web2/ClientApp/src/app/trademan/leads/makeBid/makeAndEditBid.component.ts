import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';
import { AudioRecordingService } from '../../../shared/CommonServices/audioRecording.service';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BidErrorsMessage, BidStatus } from '../../../shared/Enums/enums';
import { ActivatedRoute, Params } from '@angular/router';
import { BidVM, AudioVM } from '../../../models/tradesmanModels/tradesmanModels';

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
  public bidForm: FormGroup;
  public submitted: boolean = false;
  public bidVM: BidVM = {} as BidVM;;
  public audioVM: AudioVM =  {} as AudioVM;;
  
  public jobQuotes: string="";
  public base64String: string="";
  constructor(
    public common: CommonService,
    public audioRecordingService: AudioRecordingService,
    public sanitizer: DomSanitizer, private formBuilder: FormBuilder,
    private route: ActivatedRoute
  ) {
    this.bidForm = {} as FormGroup;
  }

  ngOnInit() {
    this.route.queryParams.subscribe((param: Params) => {
      
      this.jobQuotes = param['jobQuotes'];
      var obj = JSON.parse(this.jobQuotes);
      console.log(obj);
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
        console.log(this.base64String);
      }
      console.log(this.base64String);
    });
    this.bidForm = this.formBuilder.group({
      Amount: ['', Validators.required],
      Comments: [''],
      CustomerBudget: [''],
    });
    this.bidsErrors = {
      amountErrors: BidErrorsMessage.AmountError
    }
    this.bidForm.patchValue(this.bidVM);
  }

  get f() {
    return this.bidForm.controls;
  }
  public SubmitAndUpdateBid() {
    
    this.submitted = true;
    var obj = this.bidForm.value; 
    this.bidVM.Amount = obj.Amount;
    this.bidVM.Comments = obj.Comments;
    this.bidVM.StatusId = BidStatus.Active;
    this.audioVM.AudioId = 0;
    this.bidVM.Audio = this.audioVM;
    if (this.bidForm.valid) {
      this.common.PostData(this.common.apiRoutes.Tradesman.SubmitAndEditBid, this.bidVM, true).then(result => {
         var test = result ;
        this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Tradesman.MyBids);
      })
    }
  }


  public dataURItoBlob(dataURI: string) {
    
    const byteString = window.atob(dataURI);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8Array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) {
      int8Array[i] = byteString.charCodeAt(i);
    }
    const blob = new Blob([int8Array], { type: 'audio/wav' });
    console.log("audio byte Array:" + blob);
    return blob;
    //this.common.GetData(this.common.apiRoutes.Tradesman.GetCustomerDetailsById + "?id=" + this.jobId, true).then(data => {
    //  var result = data ;
    //  console.log(result);

    //});
  }

  public blobToFile(theBlob: Blob, fileName: string) {
    
    var b: any = theBlob;
    b.lastModifiedDate = new Date();
    b.name = fileName;
    //Cast to a File() type
    return <File>theBlob;
  }
  public startRecording() {
    //alert("this module is under process.")

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

}
