import { Component, OnInit, Output, EventEmitter, ViewChild, ElementRef } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgxImageCompressService } from 'ngx-image-compress';
import { httpStatus, RegistrationErrorMessages, AspnetRoles, loginsecurity } from '../../../shared/Enums/enums';
import { tradesmanProfile, TradesmanProfileImage, PersonalDetailsUpdate, BusinessDetailsupdate } from '../../../models/tradesmanModels/tradesmanModels';
import { Events } from '../../../common/events';
import { ToastrService } from 'ngx-toastr';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ImageCroppedEvent } from 'ngx-image-cropper';
import { BasicRegistration, ResponseVm } from '../../../models/commonModels/commonModels';
import { JwtHelperService } from '@auth0/angular-jwt';
import { IDropdownSettings, IIdValue, ITownListVM } from '../../../shared/Interface/tradesman';
@Component({
  selector: 'app-profiles',
  templateUrl: './profiles.component.html',
  styleUrls: ['./profiles.component.css'],
})
export class ProfilesComponent implements OnInit {

  public TradesmanSkills = [''];
  public tradesmanSkillsSelected = [''];
  public DeliveryRadiousList: IIdValue[] = [];
  public CitiesList: IIdValue[] = [];
  public submittedForm = false;
  //public selectedItemsSubCategory = [];
  public fullName: string = "";
  //public subCategoriesList;
  //public SelectedSubCategoriesList = [];
  public tradesmanSkillsSettings: IDropdownSettings;
  public organizationSkillsSettings: IDropdownSettings;
  public ShowFilter = true;
  public dropdownReadOnly = true;
  public readOnly = true;
  public personalProfile = false;
  public UpdateBtn: boolean = true;
  //public subproductIds = [];
  public imageBase64: any = "";
  public imagePath: object | null = null;
  public file: any;
  public localUrl: string | null = "";
  public localCompressedURl: string = "";
  public isFlip: boolean = false;
  public sizeOfOriginalImage: number = 0;
  public sizeOFCompressedImage: number = 0;
  public imgResultBeforeCompress: string = "";
  public imgResultAfterCompress: string = "";
  public replaceUserProfileImage: any;
  public profileImageCheck: boolean = false;
  public cityName: string = "";
  public isOrganization: boolean = true;
  public profile: tradesmanProfile = {} as tradesmanProfile;
  public ImageVm: TradesmanProfileImage = {} as TradesmanProfileImage;
  public personalDetailsUpdate: PersonalDetailsUpdate = {} as PersonalDetailsUpdate;
  public businessDetailsupdate: BusinessDetailsupdate = {} as BusinessDetailsupdate;
  public basicRegistrationVm: BasicRegistration = {} as BasicRegistration;
  public response: ResponseVm = {} as ResponseVm;
  //public statusMessage;
  //public responseMessage;
  public status: boolean = false;
  public tradesmanTravelingDistance: string = "";
  public appValForm: FormGroup;
  public townList: ITownListVM[] = [];
  public searchtownList: IIdValue[] = [];
  ProductIds: number = 0;
  public role: string = "";
  public getLoggedTradesmanRole: string = "";
  @Output()
  picChanged = new EventEmitter();
  public imageChangedEvent: any = '';
  public croppedImage: any | undefined = '';
  public townkeyword = 'value';
  @ViewChild('cropModal', { static: true }) cropImageModel: ModalDirective;
  @ViewChild('quotesfileinput', { static: false }) userProfileImage: ElementRef;
  jwtHelperService: JwtHelperService = new JwtHelperService();

  constructor(private events: Events,
    public common: CommonService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private imageCompress: NgxImageCompressService,
    private toastr: ToastrService,
  ) {
    this.appValForm = {} as FormGroup;
    this.tradesmanSkillsSettings = {} as IDropdownSettings;
    this.organizationSkillsSettings = {} as IDropdownSettings;
    this.cropImageModel = {} as ModalDirective;
    this.userProfileImage = {} as ElementRef;
  }

  ngOnInit() {
    var token = localStorage.getItem("auth_token");
    var decodedtoken = token != null ? this.jwtHelperService.decodeToken(token) : "";
    this.role = decodedtoken.Role;
    this.getLoggedTradesmanRole = this.role;
    if (this.getLoggedTradesmanRole == AspnetRoles.TRole) {
      this.isOrganization = false;
    }
    else {
      this.isOrganization = true;
    }

    this.tradesmanSkillsSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: true,
      itemsShowLimit: 2,
      //limitSelection: 2,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };

    this.organizationSkillsSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: true,
      itemsShowLimit: 2,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };

    this.appValForm = this.formBuilder.group({
      tradesmanId: [0],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: [''],
      cnic: [''],
      mobileNumber: [''],
      dateOfBirth: [''],
      gender: [0],
      businessAddress: ['', [Validators.maxLength(100), Validators.required]],
      town: ['', [Validators.required]],
      tradesmanSkills: [0, [Validators.required]],
      travelingDistance: [null, [Validators.required]],
      cityId: ['', [Validators.required]],
      isOrganization: [null],
      companyName: ['', [Validators.required]],
      companyRegNo: ['', [Validators.required]],
    });
    this.PopulateData();
    this.getTownList();

  }
  VerifyPhoneNumber() {
  }
  VerifyEmailAddress() {
  }
  get f() {
    return this.appValForm.controls;
  }
  fileChangeEvent(event: any): void {

    var fileType = event.target.files[0].name.split('.').pop();
    if (fileType == "jpg" || fileType == "jpeg" || fileType == "png") {
      this.imageChangedEvent = event;
      this.cropImageModel.show();
    }
    else {
      this.toastr.error("Invalid File Format", "Please Select PNG,JPG,JPEG Image");
    }

  }
  imageCropped(event: ImageCroppedEvent) {
    this.croppedImage = event.base64;
    this.userProfileImage.nativeElement.value = "";
  }
  public getTownList() {
    this.common.GetData(this.common.apiRoutes.UserManagement.GetTownsByCityId + "?cityId=" + "64", false).then(res => {
      this.townList = res;
      this.townList.forEach((x) => {
        this.searchtownList.push({ value: x.name, id: x.townId });
      })
    });
  }
  public selecttownEvent(item: string) {
  }
  public unselecttownEvent(item: string) {
  }
  public onChangetownSearch(val: string) {
    // fetch remote data from here
    // And reassign the 'data' which is binded to 'data' property.
  }

  public PopulateData() {
    this.common.GetData(this.common.apiRoutes.Supplier.GetDropDownOptionWeb, true).then(result => {
      if (status = httpStatus.Ok) {
        var data = result;
        this.CitiesList = data.cities;
        this.DeliveryRadiousList = data.distances;
        this.common.GetData(this.common.apiRoutes.Tradesman.GetTradesmanSkills, true).then(result => {
          if (status = httpStatus.Ok) {
            var data = result;
            this.TradesmanSkills = data;
            console.log(this.TradesmanSkills);

          }
        },
          error => {
            console.log(error);
          });

        this.common.GetData(this.common.apiRoutes.Tradesman.GetBusinessAndPersnalProfileWeb, true).then(result => {
          if (status = httpStatus.Ok) {
            this.profile = result;
            this.profile.persnalDetails.dateOfBirth = this.profile.persnalDetails.dateOfBirth != undefined ? this.profile.persnalDetails.dateOfBirth : "";
            this.appValForm.patchValue(this.profile.persnalDetails);
            this.appValForm.patchValue(this.profile.businessDetails);
            this.appValForm.controls.cityId.setValue(this.profile.businessDetails.cityId);
            this.appValForm.controls.businessAddress.setValue(this.profile.businessDetails.businessAddress);
            this.isOrganization = this.profile.businessDetails.isOrganization;
            var date = new Date(this.profile.persnalDetails.dateOfBirth);
            this.appValForm.controls.dateOfBirth.setValue(this.common.formatDate(date));
            this.fullName = this.profile.persnalDetails.firstName + " " + this.profile.persnalDetails.lastName;
            this.tradesmanSkillsSelected = this.profile.businessDetails.tradesmanSkills ? this.profile.businessDetails.tradesmanSkills : [];
            this.appValForm.controls.tradesmanSkills.setValue(this.profile.businessDetails.tradesmanSkills);
            if (this.profile.businessDetails.travelingDistance == 0) {
              this.appValForm.controls.travelingDistance.setValue(this.tradesmanTravelingDistance);
            }
            if (this.profile.persnalDetails.profileImage != null) {
              this.profileImageCheck = true;
              this.ImageVm.imageBase64 = 'data:image/png;base64,' + this.profile.persnalDetails.profileImage;
            }

          }
          else {
            this.common.Notification.error("Some thing went wrong.");
          }
        },
          error => {
            console.log(error);
          });
      }
    },
      error => {
        console.log(error);
      });
  }



  public Save() {

    var data = this.appValForm.value;
    if (!data.isOrganization) {
      this.appValForm.get('companyName')?.clearValidators();
      this.appValForm.get('companyName')?.updateValueAndValidity();
      this.appValForm.get('companyRegNo')?.clearValidators();
      this.appValForm.get('companyRegNo')?.updateValueAndValidity();
    }
    this.submittedForm = true;
    var tradesMainSkills = data.tradesmanSkills;

    if (this.appValForm.invalid) {
      return;
    }
    else if ((tradesMainSkills.length > 2 && this.getLoggedTradesmanRole == loginsecurity.TRole)) {
      this.toastr.error('You Can Select Maximum 2 Skills !!', "Alert !!");
      return;
    }

    this.businessDetailsupdate.skillIds = [];
    if (tradesMainSkills.length) {
      for (var i in tradesMainSkills) {
        var pd = tradesMainSkills[i].id;
        this.businessDetailsupdate.skillIds.push(pd);
      }
    }

    this.businessDetailsupdate.businessAddress = data.businessAddress
    this.businessDetailsupdate.city = this.cityName;
    let city = this.CitiesList.filter(x => x.id == data.cityId);
    this.businessDetailsupdate.city = city[0].value;
    this.businessDetailsupdate.town = data.town instanceof Object == false ? data.town : data.town.value;
    this.businessDetailsupdate.travelingDistance = data.travelingDistance;
    this.businessDetailsupdate.isOrganization = this.isOrganization;
    this.businessDetailsupdate.companyRegNo = data.companyRegNo;
    this.businessDetailsupdate.companyName = data.companyName;
    this.businessDetailsupdate.tradesmanSkills = data.tradesmanSkills;

    this.personalDetailsUpdate.cnic = data.cnic;
    this.personalDetailsUpdate.dateOfBirth = data.dateOfBirth;
    this.personalDetailsUpdate.email = data.email;
    this.personalDetailsUpdate.firstName = data.firstName;
    this.personalDetailsUpdate.lastName = data.lastName;
    this.personalDetailsUpdate.gender = data.gender;
    this.personalDetailsUpdate.mobileNumber = data.mobileNumber;

    this.common.PostData(this.common.apiRoutes.Tradesman.UpdatePersonalDetails, this.personalDetailsUpdate, true).then(result => {
      if (status = httpStatus.Ok) {
        this.common.PostData(this.common.apiRoutes.Tradesman.AddEditTradesmanWithSkills, this.businessDetailsupdate, true).then(result => {
          if (status = httpStatus.Ok) {
            this.readOnly = true;
            this.toastr.success("Profile Successfully Updated.");
            this.UpdateBtn = false;
          }
        },
          error => {
            console.log(error);
          });
      }
    });
  }

  SaveImage() {
    this.ImageVm.imageBase64 = this.croppedImage;
    this.common.PostData(this.common.apiRoutes.Tradesman.AddUpdateTradesmanProfileImage, this.ImageVm).then(result => {
      if (status = httpStatus.Ok) {
        var data = result;
        this.ImageVm.imageBase64 = this.ImageVm.imageBase64 ? this.ImageVm.imageBase64 : "";
        localStorage.setItem("image", this.ImageVm.imageBase64);
        this.toastr.success("Profile Image Successfully Updated.");
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
  public OnSelectFile(event: Event) {
    this.profileImageCheck = true;
    if ((<HTMLInputElement>event.target).files && (<HTMLInputElement>event.target).files?.[0]) {
      this.file = (<HTMLInputElement>event.target).files?.[0];
      var filename = this.file['name'];
      var reader = new FileReader();
      this.imagePath = (<HTMLInputElement>event.target).files;
      let file = (<HTMLInputElement>event.target).files?.[0];
      file != null ? reader.readAsDataURL(file) : "";
      reader.onload = (event) => {
        this.localUrl = reader.result != null ? reader.result.toString() : "";
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
        const imageBlob = this.dataURItoBlob(this.imgResultAfterCompress.split(',')[1]);
        this.ImageVm.imageBase64 = result;
      })
  }

  public CityValue(event: Event) {
    let selectElementText = event != undefined && event.target != null ? (<HTMLSelectElement>event.target)['options']
    [(<HTMLSelectElement>event.target)['options'].selectedIndex].text : "";
    this.cityName = selectElementText;
  }

  public EditProfile() {
    this.readOnly = false;
    this.personalProfile = true;
    this.dropdownReadOnly = false;
    this.UpdateBtn = true;
  }

  public hideModal() {
    this.cropImageModel.hide();
  }

  charOnly(event: KeyboardEvent): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if ((charCode > 47 && charCode < 58) || ((charCode > 32 && charCode <= 47)) || ((charCode >= 58 && charCode <= 64)) || ((charCode >= 91 && charCode <= 96)) || ((charCode >= 123 && charCode <= 126))) {
      return false;
    }
    return true;
  }


}
