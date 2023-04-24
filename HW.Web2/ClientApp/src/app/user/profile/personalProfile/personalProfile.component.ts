 import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '../../../shared/HttpClient/_http';
import { ActivatedRoute } from '@angular/router';
import { Customer, ProfileImage } from '../../../models/userModels/userModels';
import { httpStatus, CommonErrors, RegistrationErrorMessages } from '../../../shared/Enums/enums';
import { Location } from '@angular/common';
import { BasicRegistration, ResponseVm, IdValueVm } from '../../../models/commonModels/commonModels';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Response } from '@angular/http';
import { ToastrService } from 'ngx-toastr';
import { NgxImageCompressService } from 'ngx-image-compress';
import { Events } from '../../../common/events';
import { SocialAuthService } from 'angularx-social-login';
import { JwtHelperService } from '@auth0/angular-jwt';
import { threadId } from 'worker_threads';


@Component({
  selector: 'app-personal-profile',
  templateUrl: './personalProfile.component.html',
})
export class PersonalProfileComponent implements OnInit {

  public profile: Customer = {} as  Customer;
  public CitiesList: IdValueVm[] = [];
  public cityName: string = "";
  public verifyPhone: boolean | undefined = false;
  public IsRunning: boolean = false;
  public verifyEmail: boolean | undefined = false;
  public isFirtTimeverifyEmail: boolean = true;
  public isFirtTimeverifyPhone: boolean = true;
  public phoneNumber: string  = "";
  public email: string = "";
  public noNotCorrect: boolean = false;
  public token: string | null="";
  public isVerified: boolean = false;
  public loginCheck: boolean = false;
  public appVal: FormGroup;
  public verficationCodeErrorMessage: string="";
  public submitt: boolean = false;
  public IsPhoneNumberTimerRunning: boolean = false;
  public IsEmailTimerRunning: boolean = false;
  public code: string="";
  public statusCode: boolean = false;
  public otpCodeExpire : boolean = false;
  public responseMessage: string | undefined = "";
  public verificationCode: string="";
  public basicRegistrationVm: BasicRegistration = {} as BasicRegistration;
  public response: ResponseVm = {} as ResponseVm;
  public statusMessage: string | undefined = "";
  public status: boolean = false;
  public profileImageCheck = false;
  public ImageVm: ProfileImage = {} as  ProfileImage;
  public imageBase64: string="";
  public imagePath: any;
  public file: any;
  public ptimer: any;
  public etimer: any;
  public phoneNumberdisplay: any;
  public emaildisplay: any;
  public localUrl: any;
  public localCompressedURl: any;
  public isFlip: boolean = false;
  public sizeOfOriginalImage: number=0;
  public sizeOFCompressedImage: number=0;
  public imgResultBeforeCompress: string="";
  public imgResultAfterCompress: string="";
  public imageChangedEvent: any = '';
  public croppedImage: any = '';
  public loggedUserDetails: any;
  public userId: string="";
  display: any;
  public isUserBlocked: boolean = false;
  public dDate: Date = new Date('1900-01-01');
  jwtHelperService: JwtHelperService = new JwtHelperService();

  @ViewChild('varifyAcountModel', { static: true }) varifyAcountModel: ModalDirective;
  @ViewChild('blockAccountMessageModal', { static: false }) blockAccountMessageModal: ModalDirective;

  constructor(private events: Events,
    public common: CommonService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private authService: SocialAuthService,
    private imageCompress: NgxImageCompressService,
  ) {
    this.appVal = {} as FormGroup;
    this.varifyAcountModel = {} as ModalDirective;
    this.blockAccountMessageModal = {} as ModalDirective;
  }

  ngOnInit() {
    
    //this.GetVerifyStatus();
   // this.PopulateData();

    this.token = localStorage.getItem("auth_token");

    if (this.token != null && this.token != '') {
      this.loginCheck = true;
    }
    else {
      this.loginCheck = false;
    }
    var token = localStorage.getItem("auth_token");
    var decodedtoken = token != null ? this.jwtHelperService.decodeToken(token):"";
    this.userId = decodedtoken.UserId

    this.appVal = this.formBuilder.group(
      {
        verificationCode: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(6)]],
      }
    );
    this.checkUserStatus();
  }

  public checkUserStatus() {
  
    this.common.GetData(this.common.apiRoutes.Login.GetUserBlockStatus + "?userId=" + this.userId).then(result => {
      this.isUserBlocked = result 
      
      if (!this.isUserBlocked) {
        this.getCityList();
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

  emailtimer(minute: number) {
    
    this.IsEmailTimerRunning = true;
    let seconds: number = minute * 60;
    let textSec: number = 0;
    let statSec: number = 60;

    const prefix = minute < 10 ? "0" : "";

    if (minute == 0) {
      clearInterval(this.etimer);
      this.IsEmailTimerRunning = false;
      this.emaildisplay = null;
    }
    else {
      this.etimer = setInterval(() => {
        seconds--;
        if (statSec != 0) statSec--;
        else statSec = 59;

        if (statSec < 10) {
          textSec = 0 + statSec;
        } else textSec = statSec;

        this.emaildisplay = `Your Email OTP will be Expired in: ${prefix}${Math.floor(seconds / 60)}:${textSec}`;

        if (seconds == 0) {
          this.IsEmailTimerRunning = false;
          this.emaildisplay = null;
          console.log("finished");
          clearInterval(this.etimer);
        }
      }, 1000);
    }
  }

  phoneNumbertimer(minute: number) {
    
    this.IsPhoneNumberTimerRunning = true;
    let seconds: number = minute * 60;
    let textSec: any = "0";
    let statSec: number = 60;

    const prefix = minute < 10 ? "0" : "";

    

    if (minute == 0) {
      clearInterval(this.ptimer);
      this.IsPhoneNumberTimerRunning = false;
      this.phoneNumberdisplay = null;
    }
    else {
       this.ptimer = setInterval(() => {
        seconds--;
        if (statSec != 0) statSec--;
        else statSec = 59;

        if (statSec < 10) {
          textSec = "0" + statSec;
        } else textSec = statSec;

        this.phoneNumberdisplay = `Your Phone Number OTP will be Expired in: ${prefix}${Math.floor(seconds / 60)}:${textSec}`;

        if (seconds == 0) {
          this.IsPhoneNumberTimerRunning = false;
          this.phoneNumberdisplay = null;
          console.log("finished");
          clearInterval(this.ptimer);
        }
      }, 1000);
    }
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

  public getCityList() {
    this.common.GetData(this.common.apiRoutes.Common.getCityList, true).then(result => {      
      this.CitiesList = result ;
      this.PopulateData();
    }, error => {
      console.log(error);
    });
  }

  public hideModal() {
    this.varifyAcountModel.hide();
  }

  get g() { return this.appVal.controls; }


  public OnSelectFile(event: Event) {
    this.profileImageCheck = true;
    if ((<HTMLInputElement>event.target).files && (<HTMLInputElement>event.target).files?.[0]) {
      this.file = (<HTMLInputElement>event.target).files?.[0];
      var filename = this.file['name'];
      var reader = new FileReader();
      this.imagePath = (<HTMLInputElement>event.target).files;
      let file = (<HTMLInputElement>event.target).files?.[0];
      file != undefined ? reader.readAsDataURL(file):"";
      reader.onload = (event) => {
        this.localUrl = reader.result;
        var test = this.compressFile(this.localUrl, filename);
      }
    }
  }
  public dataURItoBlob(dataURI: string) {
    const byteString = window.atob(dataURI);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8Array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) {
      int8Array[i] = byteString.charCodeAt(i);
    }
    const blob = new Blob([int8Array], { type: 'image/png' });
    return blob;
  }

  public compressFile(image: string, fileName: string) {
    var orientation = -1;
    this.sizeOfOriginalImage = this.imageCompress.byteCount(image) / (1024 * 1024);
    this.imageCompress.compressFile(image, orientation, 50, 50).then(
      result => {
        this.imgResultAfterCompress = result;
        this.localCompressedURl = result;
        this.sizeOFCompressedImage = this.imageCompress.byteCount(result) / (1024 * 1024)
        const imageBlob = this.dataURItoBlob(this.imgResultAfterCompress.split(',')[1]);
        this.ImageVm.imageBase64 = result;
      })
  }

  fileChangeEvent(event: Event): void {
    
    var fileType = (<HTMLInputElement>event.target).files?.[0].name.split('.').pop();
    if (fileType == "jpg" || fileType == "jpeg" || fileType == "png") {
      this.imageChangedEvent = event;
      //this.cropImageModel.show();
    }
    else {
      this.toastr.error("Invalid File Format", "Please Select PNG,JPG,JPEG Image");
    }

  }

  public PopulateData() {
    this.common.GetData(this.common.apiRoutes.Users.CustomerProfile, true).then(result => {
      if (status = httpStatus.Ok) {
        
        this.profile = result ;
        let cityName = this.CitiesList.filter(c => c.id == this.profile.cityId);
        if (cityName.length > 0) {
          this.profile.city = cityName[0].value;
        }
        let defaultDate = this.common.formatDate(this.dDate, 'DD-MMM-YYYY');
        this.profile.dateOfBirth = this.profile.dateOfBirth != undefined ? this.profile.dateOfBirth : new Date;
        this.profile.dob = this.common.formatDate(this.profile.dateOfBirth, 'DD-MMM-YYYY');
        if (this.profile.dob == defaultDate) {
          this.profile.dob = 'dd/mm/yyyy';
        }
        ;
        if (this.profile.profileImage != "") {
          this.profileImageCheck = true;
          this.ImageVm.imageBase64 = 'data:image/png;base64,' + this.profile.profileImage;
        }
        else {
          this.profile.profileImage = "";
        }
        
        this.verifyPhone = this.profile.isNumberConfirmed;
        this.verifyEmail = this.profile.isEmailConfirmed;
        
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }


  // verification Email Module

  VerifyEmailAddress() {
    this.email = this.profile.email;
    if (this.email) {
      this.phoneNumber = "";
      this.SentOtpCode();
      if (!this.IsEmailTimerRunning) {
        this.emailtimer(1);
      }
    }
  }

  

  // verification Phone Number Module
  VerifyPhoneNumber() {
    
    this.phoneNumber = this.profile.mobileNumber;
    if (this.phoneNumber) {
      this.common.GetData(this.common.apiRoutes.Users.CustomerProfile, true).then(result => {
        if (status = httpStatus.Ok) {
          this.profile = result ;
          if (this.profile.mobileNumber == this.phoneNumber) {
            if (this.phoneNumber != null) {
              this.email = "";
              this.SentOtpCode();
              if (!this.IsPhoneNumberTimerRunning) {
                this.phoneNumbertimer(1);
              }
            }
          }
          else {
            this.noNotCorrect = true;
            setTimeout(() => {
              this.noNotCorrect = false;
            }, 2000);
          }
        }
      });
    }
  }

  public SentOtpCode() {
    this.basicRegistrationVm.email = this.email;
    this.basicRegistrationVm.phoneNumber = this.phoneNumber;
    this.statusMessage = this.basicRegistrationVm.email != "" ? this.basicRegistrationVm.email : this.basicRegistrationVm.phoneNumber;
    this.common.PostData(this.common.apiRoutes.Common.getOtp, this.basicRegistrationVm, true).then(result => {
      this.response = result;
     // console.log(this.response);
      if (status = httpStatus.Ok) {
        this.varifyAcountModel.show();
      }
      
      this.response = result;
       this.status = true;
       this.responseMessage = this.response.message;
      
    }, error =>{
      console.log();
    });
  }

  public VarifyAccount() {
    
    this.verficationCodeErrorMessage = RegistrationErrorMessages.verificationErrorMessage;
    this.submitt = true;
    if (this.appVal.invalid) {
      return;
    }
    
    this.code = this.appVal.value.verificationCode;
    this.common.GetData(this.common.apiRoutes.Common.VerifyOtp + "?code=" + this.code + "&phoneNumber=" + this.phoneNumber + "&email=" + this.email + "&userId=" + this.token, true).then(result => {
     this.response = result;
      console.log(this.response);

      if (this.response.status = httpStatus.Ok) {
        if(this.response.resultData == false){
          this.statusCode = true;
          setTimeout(() =>{
            this.statusCode = false;

          },3000);
          //this.response.status = httpStatus.Ok;
        }
        else if (this.response.resultData == null) {
          this.otpCodeExpire = true;
          setTimeout(() =>{
            this.otpCodeExpire = false;

          },3000);
          return;
        }
        else {
          
          if (this.phoneNumber != null) {
            this.phoneNumbertimer(0);
            this.verficationCodeErrorMessage = "";
                }
          else {
                  this.emailtimer(0);
                }
          this.statusCode = false;
          localStorage.setItem("accountVerfication", 'true');
          // this.GetVerifyStatus();
          this.appVal.reset();
          this.PopulateData();
          this.varifyAcountModel.hide();
          this.events.account_verfication.emit();
        }
      }
      else {
        console.log("Heavy Issues !!");
      }
    });
  }

  //public GetVerifyStatus() {
  //  
  //  if (this.token != null) {
  //    this.common.GetData(this.common.apiRoutes.IdentityServer.GetPhoneNumberVerificationStatus + "?userId=" + this.token, true).then(result => {
  //      
  //      this.isVerified = result ;
  //    });
  //  }
  //}

  numberOnly(event: KeyboardEvent): boolean {
    //this.noNotCorrect = true;
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }

    return true;

  }
  public ResendCode() {
    this.verificationCode = '';
    this.SentOtpCode();
    
    if (this.statusMessage?.includes('@')) {
      if (!this.IsEmailTimerRunning) {
        this.emailtimer(1);
      }
    }
    else if (!this.IsEmailTimerRunning) {
      if (!this.IsPhoneNumberTimerRunning) {
        this.phoneNumbertimer(1);
      }
    }
  }
}
