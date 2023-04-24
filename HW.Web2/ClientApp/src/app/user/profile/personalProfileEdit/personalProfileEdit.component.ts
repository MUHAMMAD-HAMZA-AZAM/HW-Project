import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Customer, ProfileImage } from '../../../models/userModels/userModels';
import { CommonService } from '../../../shared/HttpClient/_http';
import { ActivatedRoute, Data } from '@angular/router';
import { httpStatus, CommonErrors, AspnetRoles, loginsecurity } from '../../../shared/Enums/enums';
import { NgxImageCompressService } from 'ngx-image-compress';
import { Events } from '../../../common/events';
import { ToastrService } from 'ngx-toastr';
import { ImageCroppedEvent } from 'ngx-image-cropper';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ResponseVm, IdValueVm } from '../../../models/commonModels/commonModels';




@Component({
  selector: 'app-personal-profile-edit',
  templateUrl: './personalProfileEdit.component.html',
})
export class PersonalProfileEditComponent implements OnInit {
  public response: ResponseVm = {} as ResponseVm;
  public userAvailabilty: boolean = false;
  public userAvailabiltyErrorMessage: string | undefined = "";
  public submittedForm = false;
  public appValForm: FormGroup;
  public imageBase64: string="";
  public cropReady = false;
  public imagePath: any;
  public BASE64_MARKER = ';base64,';
  public file: any;
  public predictions: number[]=[];
  public imageDataEvent: any;
  public localUrl: any;
  public localCompressedURl: any;
  public isFlip: boolean = false;
  public sizeOfOriginalImage: number=0;
  public sizeOFCompressedImage: number=0;
  public id: number=0;
  public profile: Customer = {} as  Customer;
  public ImageVm: ProfileImage = {} as  ProfileImage;
  public CitiesList: IdValueVm[] = [];
  public imgResultBeforeCompress: string="";
  public imgResultAfterCompress: string="";
  public phonenumberErrorMessage: string="";
  public imageChangedEvent: any = '';
  public croppedImage: any = '';
  public profileImageCheck: boolean = false;
  public disableEmail: boolean = false;
  public disablePhoneNumber: boolean = false;
  public netUserData: any;
  public socialAvailability: boolean = false;
  public userDOB: string="";

  @ViewChild('cropModal', { static: true }) cropImageModel: ModalDirective;
  @ViewChild('userProfileImage', { static: false }) userProfileImage: ElementRef;

  constructor(private events: Events,
    public common: CommonService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private imageCompress: NgxImageCompressService,
    private toaster: ToastrService,
  ) {
    this.appValForm = {} as FormGroup;
    this.cropImageModel = {} as ModalDirective;
    this.userProfileImage = {} as ElementRef;
  }

  ngOnInit() {
    
    this.appValForm = this.formBuilder.group({
      dateOfBirth: [''],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: [''],
      mobileNumber: ['', [ Validators.minLength(11), Validators.maxLength(12)]],
      email: ['', [Validators.pattern("^[A-Za-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]],
      cnic: ['', [Validators.maxLength(13), Validators.pattern("^[0-9]{5}[0-9]{7}[0-9]$")]],
      cityId: ['', Validators.required]
    });
    this.getCityList();
    this.PopulateData();
    var todayDate = new Date();
    todayDate.setFullYear(todayDate.getFullYear() - 10);
    var currentDate = todayDate;
    this.userDOB = this.common.formatDate(currentDate, "YYYY-MM-DD");  
  }
  public getCityList() {
    this.common.GetData(this.common.apiRoutes.Common.getCityList, true).then(result => {
      this.CitiesList = result ;
      console.log(this.CitiesList);
    }, error => {
      console.log(error);
    });
  }

  public selectedCity(cId: any) {

    this.appValForm.controls.cityId = cId;
  }
  fileChangeEvent(event: Event): void {
    

    var fileType = (<HTMLInputElement>event.target).files?.[0].name.split('.').pop();
    if (fileType == "jpg" || fileType == "jpeg" || fileType == "png") {
      this.imageChangedEvent = event;
      this.cropImageModel.show();
       
    }
    else {

      this.toaster.error("Invalid File Format", "Please Select PNG,JPG,JPEG Image");
    }
    
  }
  imageCropped(event: ImageCroppedEvent) {
    this.croppedImage = event.base64;
    this.userProfileImage.nativeElement.value = "";
  }
  SaveImage() {
    this.ImageVm.imageBase64 = this.croppedImage;
    this.common.PostData(this.common.apiRoutes.Users.AddUpdateUserProfileImage, this.ImageVm).then(result => {
      if (status = httpStatus.Ok) {
        var data = result ;
        this.ImageVm.imageBase64 = this.ImageVm.imageBase64 != undefined ? this.ImageVm.imageBase64 : "";
        localStorage.setItem("image", this.ImageVm.imageBase64);
        this.events.pic_Changed.emit();
        this.toaster.success("Profile Image Successfully Updated.");
        this.croppedImage = '';
        this.common.NavigateToRoute(this.common.apiUrls.User.PersonalProfile);
      }
    });
    this.profileImageCheck = true;
    this.cropImageModel.hide();
  }
  imageLoaded() {
    this.cropReady = true;
    // show cropper
  }
  cropperReady() {
    // cropper ready
  }
  loadImageFailed() {
    console.log('Load failed');
    // show message
  }

  public hideModal() {
    this.cropReady = false;
    this.cropImageModel.hide();

  }

  get f() {
    return this.appValForm.controls;
  }

  public Update() {
    
    this.phonenumberErrorMessage = "Please enter Phone Number.",
      this.submittedForm = true;
    if (this.appValForm.invalid) {
      return;
    }
    this.profile = this.appValForm.value;
    console.log(this.profile);
    // To Check User Email Already Existence or Not 
    this.common.get(this.common.apiRoutes.IdentityServer.GetUserByToken).subscribe(res => {
      this.netUserData = res ;
      if (this.netUserData.status == httpStatus.Ok) {
        if (this.netUserData.resultData != null) {
          this.profile.facebookUserId = this.netUserData.resultData.facebookUserId;
          this.profile.googleUserId = this.netUserData.resultData.googleUserId;
          this.profile.email = this.netUserData.resultData.email == this.appValForm.controls.email.value ? "" :  this.appValForm.controls.email.value;
          this.profile.phoneNumber = this.netUserData.resultData.phoneNumber == this.appValForm.controls.mobileNumber.value ? "" : this.appValForm.controls.mobileNumber.value;
          this.profile.fromPersonalDetails = true;
          this.profile.role = loginsecurity.CRole;
          if (this.profile != null) {
            this.common.PostData(this.common.apiRoutes.registration.CheckEmailandPhoneNumberAvailability, this.profile, true).then(result => {
              this.response = result ;

              if (this.response.status == httpStatus.Ok) {
                this.profile.email = this.appValForm.controls.email.value;
                
                this.common.PostData(this.common.apiRoutes.Users.CustomerProfileUpdate, this.profile, true).then(result => {

                  if (status = httpStatus.Ok) {
                    this.profile = result ;
                    this.appValForm.patchValue(this.profile);
                    let dateOfBirth = this.profile.dateOfBirth != undefined ? this.profile.dateOfBirth : new Date;
                    this.appValForm.controls.dateOfBirth.setValue(this.common.formatDate(dateOfBirth));
                    this.toaster.success("Profile Successfully Updated.");
                    this.common.NavigateToRoute(this.common.apiUrls.User.PersonalProfile);
                  }
                }, error => {
                  console.log(error);
                  this.common.Notification.error(CommonErrors.commonErrorMessage);
                });
              }
              else {
                this.userAvailabilty = true;
                setTimeout(() => {
                  this.userAvailabilty = false;
                }, 5000);
                this.userAvailabiltyErrorMessage = this.response.message;
              }
            }, error => {
              console.log(error);
            });
          }
          else {
            this.common.Notification.error(CommonErrors.commonErrorMessage);
          }
        }
      }
    });
  }


  public PopulateData() {
    this.common.GetData(this.common.apiRoutes.Users.CustomerProfile, true).then(result => {
      if (status = httpStatus.Ok) {
        this.profile = result ;
        this.appValForm.patchValue(this.profile);
        this.appValForm.controls.cityId.setValue(this.profile.cityId);
        this.profile.mobileNumber != " " ? this.disablePhoneNumber = true : this.disablePhoneNumber = false;
        this.profile.isNumberConfirmed == true ? this.disablePhoneNumber = true : this.disablePhoneNumber = false;
        this.profile.email != "" ? this.disableEmail = true : this.disableEmail = false;
        this.profile.isEmailConfirmed == true ? this.disableEmail = true : this.disableEmail = false;
        
        this.ImageVm.imageBase64 = this.profile.profileImage;
        if (this.profile.profileImage != null && this.profile.profileImage != "") {
        this.profileImageCheck = true;
        }
        this.ImageVm.imageBase64 = 'data:image/png;base64,' + this.profile.profileImage;
        let dateOfBirth = this.profile?.dateOfBirth != undefined ? this.profile.dateOfBirth : new Date;
        this.appValForm.controls.dateOfBirth.setValue(this.common.formatDate(dateOfBirth,"YYYY-MM-DD"));
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }

  public OnSelectFile(event: Event) {
    if ((<HTMLInputElement>event.target).files && (<HTMLInputElement>event.target).files?.[0]) {
      this.file = (<HTMLInputElement>event.target).files?.[0];
      var filename = this.file['name'];
      var reader = new FileReader();
      this.imagePath = (<HTMLInputElement>event.target).files;
      let file = (<HTMLInputElement>event.target).files?.[0]
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

  public compressFile(image: any, fileName: string) {
    var orientation = -1;
    this.sizeOfOriginalImage = this.imageCompress.byteCount(image) / (1024 * 1024);

    this.imageCompress.compressFile(image, orientation, 50, 50).then(
      result => {
        this.imgResultAfterCompress = result;
        this.localCompressedURl = result;
        this.sizeOFCompressedImage = this.imageCompress.byteCount(result) / (1024 * 1024)
        // create file from byte
        //const imageName = fileName;
        // call method that creates a blob from dataUri
        const imageBlob = this.dataURItoBlob(this.imgResultAfterCompress.split(',')[1]);
        this.ImageVm.imageBase64 = result;
        //const imageFile = new File([result], imageName, { type: 'image/jpeg' });
        //console.log("file size:", imageFile['size'] / (1024 * 1024));
        //return result;
      })
  }
 

  // verfication Email Module
 

  numberOnly(event: KeyboardEvent): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }

}

