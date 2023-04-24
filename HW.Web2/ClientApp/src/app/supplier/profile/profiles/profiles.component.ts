import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { httpStatus, CommonErrors, RegistrationErrorMessages } from '../../../shared/Enums/enums';
import { CommonService } from '../../../shared/HttpClient/_http';
//import { SupplierProfile, BusinessDetailUpdate, PersonalDetailsUpdate } from '../../../models/supplierModels/supplierModels';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { SupplierProfileImage } from '../../../models/userModels/userModels';
import { NgxImageCompressService } from 'ngx-image-compress';
import { Events } from '../../../common/events';
import { ToastrService } from 'ngx-toastr';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ImageCroppedEvent } from 'ngx-image-cropper';
import { SupplierProfile, BusinessDetailUpdate, PersonalDetailsUpdate } from '../../../models/supplierModels/supplierModels';
import { ResponseVm, BasicRegistration } from '../../../models/commonModels/commonModels';
import { IGetCitiesAndDistance, IIdValue, IProductCategory, ISupplierProfileDetail } from '../../../shared/Enums/Interface';
@Component({
  selector: 'app-profiles',
  templateUrl: './profiles.component.html',
  styleUrls: ['./profiles.component.css']
})
export class ProfilesComponent implements OnInit {
  public submittedForm = false;
  public selectedItemsSubCategory: IIdValue[] = [];
  public appValForm: FormGroup;
  public appVal: FormGroup;
  public profile: ISupplierProfileDetail;
  public basicRegistrationVm: BasicRegistration = {} as BasicRegistration;
  public businessDetailUpdate: BusinessDetailUpdate = new BusinessDetailUpdate();
  public personalDetailsUpdate: PersonalDetailsUpdate = new PersonalDetailsUpdate();
  public response: ResponseVm = {} as ResponseVm;
  public statusMessage: string="";
  public status: boolean = false;
  public code: number=0;
  public statusCode: boolean = false;
  public loginCheck: boolean = false;
  public token: string | null="";
  public verficationCodeErrorMessage: string="";
  public submitt: boolean = false;
  public verificationCode: string = "";
  public responseMessage: string | undefined = "";
  public fullName: string="";
  public subCategoriesList: IIdValue[] =[];
  public SelectedSubCategoriesList: IIdValue[] = [];
  public SubCategoriesdropdownSettings = {};
  public PrimaryTradedList: IProductCategory[] = [];
  public DeliveryRadiousList: IIdValue[] = [];
  public CitiesList: IIdValue[] = [];
  public ShowFilter = true;
  public dropdownReadOnly = true;
  public UpdateBtn: boolean = true;
  public readOnly = true;
  public persnolDetails = true;
  //public subproductIds = [];
  public imageBase64: string="";
  public imagePath: any;
  public file: any;
  public localUrl: any;
  public localCompressedURl: any;
  public isFlip: boolean = false;
  public sizeOfOriginalImage: number=0;
  public sizeOFCompressedImage: number=0;
  public imgResultBeforeCompress: string="";
  public imgResultAfterCompress: string="";
  public profileImageCheck = false;
  public ImageVm: SupplierProfileImage = {} as  SupplierProfileImage;
  public imageChangedEvent: any = '';
  public croppedImage: any = '';
  public replaceUserProfileImage: any;
  public verifyPhone: boolean = false;
  public verifyEmail: boolean = false;
  public phoneNumber: string="";
  public email: string="";
  public noNotCorrect: boolean = false;

  @ViewChild('cropModal', { static: true }) cropImageModel: ModalDirective;
  @ViewChild('varifyAccountModel',{ static: true }) varifyAccountModel: ModalDirective;
  @ViewChild('quotesfileinput', { static: false }) userPhotos: ElementRef;
  constructor(private events: Events,
    public common: CommonService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private imageCompress: NgxImageCompressService,
    private toaster: ToastrService,

  ) {
    this.appVal = {} as FormGroup;
    this.appValForm = {} as FormGroup;
    this.profile = {} as ISupplierProfileDetail;
    this.cropImageModel = {} as ModalDirective;
    this.varifyAccountModel = {} as ModalDirective;
    this.userPhotos = {} as ElementRef;
  }

  ngOnInit() {
    this.token = localStorage.getItem("auth_token");

    if (this.token != null && this.token != '') {
      this.loginCheck = true;
    }
    else {
      this.loginCheck = false;
    }
    this.appValForm = this.formBuilder.group({
      firstName: [''],
      lastName: [''],
      email: [''],
      cnic: [''],
      mobileNumber: [''],
      dateOfBirth: [''],
      gender: [0],
      //primaryTrade: [''],
      companyName: ['', [Validators.required]],
      primaryTradeId: [0, [Validators.required]],
      //tradeName: ['', [Validators.required]],
      registrationNumber: [''],
      deliveryRadius: ['', [Validators.required]],
      city: ['', [Validators.required]],
      businessAddress: ['', [Validators.required]],
      SubCategory: [0, [Validators.required]],
    });
    this.SubCategoriesdropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: this.ShowFilter,
      itemsShowLimit: 1,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };
    this.appVal = this.formBuilder.group(
      {
        verificationCodeNumber: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(6)]],
      }
    );

    this.PopulateData();
   // this.EditProfile();
  }
  get f() {
    return this.appValForm.controls;
  }
  get g() { return this.appVal.controls; }

  fileChangeEvent(event: any): void {
    
    var fileType = event.target.files[0].name.split('.').pop();
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
    this.userPhotos.nativeElement.value = "";

  }
  SaveImage() {
    this.ImageVm.imageBase64 = this.croppedImage;
      this.common.PostData(this.common.apiRoutes.Supplier.AddUpdateSupplierProfileImage, this.ImageVm).then(result => {
        if (status = httpStatus.Ok) {
          var data = result ;
          this.ImageVm.imageBase64 = this.ImageVm.imageBase64 ? this.ImageVm.imageBase64:""
          localStorage.setItem("image", this.ImageVm.imageBase64);
          this.toaster.success("Profile Image Successfully Updated.");
          this.readOnly = true;
          this.UpdateBtn = false;
          this.events.pic_Changed.emit();
        }
      }, error => {
        console.log(error);
        this.common.Notification.error("Some thing went wrong.");
      });
    this.replaceUserProfileImage = this.croppedImage;
    this.cropImageModel.hide();
  }
  imageLoaded() {
    // show cropper
  }
  cropperReady() {
    // cropper ready
  }
  loadImageFailed() {
    // show message
  }
  public hideModal() {
    this.cropImageModel.hide();
    this.varifyAccountModel.hide();
  }

  public Save() {
    
    this.submittedForm = true;
    if (this.appValForm.invalid) {
      return;
    }
    var data = this.appValForm.value;
    console.log(document.getElementById("primaryTrades"));
    var name = document.getElementById("primaryTrades")?.innerText;
    var productValue = data.SubCategory;
    if (productValue.length) {
      for (var i in productValue) {
        var pd = productValue[i].id;
        this.businessDetailUpdate.ProductIds?.push(pd);
      }
    }
    else {
      this.businessDetailUpdate.ProductIds = [];
    }

    if (data.primaryTradeId) {
      this.common.GetData(this.common.apiRoutes.Supplier.GetDropDownOptionWeb, true).then(result => {
        if (status = httpStatus.Ok) {
          var aa: IGetCitiesAndDistance = result ;
          this.PrimaryTradedList = aa.productCategories;
          this.PrimaryTradedList.forEach(res => {
            if (res.id == data.primaryTradeId) {
              this.businessDetailUpdate.PrimaryTrade = res.value;           
            }
          });
        }
    });
    }
    this.businessDetailUpdate.CityId = data.city;
    this.businessDetailUpdate.PrimaryTradeId = data.primaryTradeId ? data.primaryTradeId : 0;
    this.businessDetailUpdate.PrimaryTrade = data.primaryTrade;
    this.businessDetailUpdate.DeliveryRadius = data.deliveryRadius;
    this.businessDetailUpdate.BusinessAddress = data.businessAddress;
    this.businessDetailUpdate.CompanyRegistrationNo = data.registrationNumber;
    this.businessDetailUpdate.CompanyName = data.companyName;
    this.personalDetailsUpdate.FirstName = data.firstName;
    this.personalDetailsUpdate.LastName = data.lastName;
    this.personalDetailsUpdate.MobileNumber = data.mobileNumber;
    this.personalDetailsUpdate.Gender = data.gender;
    this.personalDetailsUpdate.Email = data.email;
    this.personalDetailsUpdate.Cnic = data.cnic;
    this.common.PostData(this.common.apiRoutes.Supplier.UpdatePersonalDetails, this.personalDetailsUpdate, true).then(result => {
      if (status = httpStatus.Ok) {
        this.common.PostData(this.common.apiRoutes.Supplier.AddSupplierBusinessDetails, this.businessDetailUpdate, true).then(result => {
          if (status = httpStatus.Ok) {
            this.toaster.success("Profile Successfully Updated.");
            this.UpdateBtn = false;
            this.readOnly = true;

          }
        }, error => {
          console.log(error);
          this.common.Notification.error("Some thing went wrong.");
        });
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }

  public PopulateData() {
    this.common.GetData(this.common.apiRoutes.Supplier.GetDropDownOptionWeb, true).then(result => {
      if (status = httpStatus.Ok) {
        var data: IGetCitiesAndDistance = result ;
        this.CitiesList = data.cities;
        this.DeliveryRadiousList = data.distances;
        this.PrimaryTradedList = data.productCategories;
        this.common.GetData(this.common.apiRoutes.Supplier.GetBusinessAndPersnalProfileWeb, true).then(result => {
          if (status = httpStatus.Ok) {
            this.profile = result ;
      
            this.appValForm.patchValue(this.profile.persnalDetails);
            this.appValForm.patchValue(this.profile.businessDetails);
            this.subCategoriesList = this.profile.businessDetails.productsubCategory;
            this.fullName = this.profile.persnalDetails.firstName + " " + this.profile.persnalDetails.lastName;
            if (this.subCategoriesList == null || this.subCategoriesList.length == 0) {
              this.subCategoriesList = this.profile.businessDetails.selectedSubCategory;
            }
            this.SelectedSubCategoriesList = this.profile.businessDetails.selectedSubCategory;
            var date = new Date(this.profile.persnalDetails.dateOfBirth);
            this.appValForm.controls.dateOfBirth.setValue(this.common.formatDate(date));
            this.appValForm.controls.SubCategory.setValue(this.profile.businessDetails.selectedSubCategory);
            this.verifyPhone = this.profile.persnalDetails.isNumberConfirmed;
            this.verifyEmail = this.profile.persnalDetails.isEmailConfirmed;
            //this.verifyEmail = this.profile.isEmailConfirmed;
            if (this.profile.persnalDetails.profileImage != null) {
              this.profileImageCheck = true;
              this.ImageVm.imageBase64 = 'data:image/png;base64,' + this.profile.persnalDetails.profileImage;
            }
          }
        });
      }
    });
  }

  public VerifyPhoneNumber() {
    
    this.phoneNumber = this.profile.persnalDetails.mobileNumber;
    let validMobilenNumber = this.profile.persnalDetails.mobileNumber;
    if (this.phoneNumber) {
      this.common.get(this.common.apiRoutes.Supplier.GetPersonalInformation).subscribe(result => {

        this.profile = <ISupplierProfileDetail>result;
        if (validMobilenNumber == this.phoneNumber) {
          if (this.phoneNumber != null) {
            this.email = '';
            this.SentOtpCode();
          }
        }

      });
    }

  }

  public VerifyEmailAddress() {
    this.email = this.profile.persnalDetails.email;
    if (this.email) {
      this.phoneNumber = "";
      this.SentOtpCode();
    }
  }
  public SentOtpCode() {

    this.basicRegistrationVm.email = this.email;
    this.basicRegistrationVm.phoneNumber = this.phoneNumber;
    this.statusMessage = this.basicRegistrationVm.email != "" ? this.basicRegistrationVm.email : this.basicRegistrationVm.phoneNumber;
    this.common.PostData(this.common.apiRoutes.Common.getOtp, this.basicRegistrationVm, true).then(result => {
      if (status = httpStatus.Ok) {
        this.varifyAccountModel.show();
      }

      this.response = result ;
      this.status = true;
      this.responseMessage = this.response.message;
    });
  }

  public VarifyAccount() {

    this.verficationCodeErrorMessage = RegistrationErrorMessages.verificationErrorMessage;
    this.submitt = true;
    if (this.appVal.invalid) {
      return;
    }

    this.code = this.appVal.value.verificationCodeNumber;
    this.common.GetData(this.common.apiRoutes.Common.VerifyOtp + "?code=" + this.code + "&phoneNumber=" + this.phoneNumber + "&email=" + this.email + "&userId=" + this.token, true).then(result => {
      if (status = httpStatus.Ok) {
        this.response = result ;
        if (this.response.status == 400) {
          this.statusCode = true;
        }
        else {
          this.statusCode = false;
          this.appVal.reset();
          this.PopulateData();
          this.varifyAccountModel.hide();
        }
      }
    });
  }
  public ResendCode() {
    this.verificationCode = '';

    this.SentOtpCode();
  }

  numberOnly(event: KeyboardEvent): boolean {
    //this.noNotCorrect = true;
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }

    return true;

  }

  public OnSelectFile(event: Event) {
    this.profileImageCheck = true;
    if ((<HTMLInputElement>event.target).files && (<HTMLInputElement>event.target).files?.[0]) {
      this.file = (<HTMLInputElement>event.target).files?.[0];
      var filename = this.file['name'];
      var reader = new FileReader();
      this.imagePath = (<HTMLInputElement>event.target).files;
      let file = (<HTMLInputElement>event.target).files?.[0];
      file ? reader.readAsDataURL(file):"";
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



  public compressFile(image:string, fileName: string) {
    var orientation = -1;
    this.sizeOfOriginalImage = this.imageCompress.byteCount(image) / (1024 * 1024);
    this.imageCompress.compressFile(image , orientation, 50, 50).then(
      result => {
        this.imgResultAfterCompress = result;
        this.localCompressedURl = result;
        this.sizeOFCompressedImage = this.imageCompress.byteCount(result) / (1024 * 1024)
        const imageBlob = this.dataURItoBlob(this.imgResultAfterCompress.split(',')[1]);
        this.ImageVm.imageBase64 = result;
      })
  }

  public EditProfile() {
    this.readOnly = false;
    this.dropdownReadOnly = false;
    this.UpdateBtn = true;
  }

  public onCategorySelect(productCategoryId: number) {
    
    
    if (productCategoryId > 0) {
      this.common.get(this.common.apiRoutes.Supplier.GetAllProductSubCatagories + "?productId=" + productCategoryId).subscribe(result => {
        this.subCategoriesList = <IIdValue[]>result;
        this.selectedItemsSubCategory = [];
      }, error => {
        console.log(error);
        this.common.Notification.error("Some thing went wrong.");
      });
    }
  }


}
