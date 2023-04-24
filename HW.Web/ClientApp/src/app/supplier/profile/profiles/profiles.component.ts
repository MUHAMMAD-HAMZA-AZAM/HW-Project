import { Component, OnInit } from '@angular/core';
import { httpStatus } from '../../../shared/Enums/enums';
import { CommonService } from '../../../shared/HttpClient/_http';
import { SupplierProfile, BusinessDetailUpdate, PersonalDetailsUpdate } from '../../../models/supplierModels/supplierModels';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { INgxMyDpOptions } from 'ngx-mydatepicker';
import { SupplierProfileImage } from '../../../models/userModels/userModels';
import { NgxImageCompressService } from 'ngx-image-compress';
@Component({
  selector: 'app-profiles',
  templateUrl: './profiles.component.html',
  styleUrls: ['./profiles.component.css']
})
export class ProfilesComponent implements OnInit {
  public submittedForm = false;
  selectedItemsSubCategory = [];
  public appValForm: FormGroup;
  public profile: SupplierProfile = new SupplierProfile();
  public businessDetailUpdate: BusinessDetailUpdate = new BusinessDetailUpdate();
  public personalDetailsUpdate: PersonalDetailsUpdate = new PersonalDetailsUpdate();
  public fullName: string;
  public subCategoriesList;
  public SelectedSubCategoriesList = [];
  public SubCategoriesdropdownSettings = {};
  public PrimaryTradedList = [];
  public DeliveryRadiousList = [];
  public CitiesList = [];
  public ShowFilter = true;
  public readOnly = "readOnly";
  public subproductIds = [];
  public imageBase64;
  public imagePath;
  public file: any;
  public localUrl: any;
  public localCompressedURl: any;
  public isFlip: boolean = false;
  public sizeOfOriginalImage: number;
  public sizeOFCompressedImage: number;
  public imgResultBeforeCompress: string;
  public imgResultAfterCompress: string;
  public profileImageCheck=false;
  public ImageVm: SupplierProfileImage = new SupplierProfileImage();

  constructor(
    public common: CommonService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private imageCompress: NgxImageCompressService,
  ) { }

  ngOnInit() {
    this.appValForm = this.formBuilder.group({
      firstName: [''],
      lastName: [''],
      email: [''],
      cnic: [''],
      mobileNumber: [''],
      dateOfBirth: [''],
      gender: [0],
      primaryTrade: [0],
      companyName: [''],
      primaryTradeId: [0],
      tradeName: [''],
      registrationNumber: [''],
      deliveryRadius: [''],
      city: [''],
      businessAddress: [''],
      SubCategory: [''],
    });
    this.SubCategoriesdropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: this.ShowFilter,
      itemsShowLimit: 10,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };

    this.PopulateData();
  }

  get f() {
    return this.appValForm.controls;
  }
  public Save() {
    this.submittedForm = true;
    if (this.appValForm.invalid) {
      return;
    }
    debugger
    var data = this.appValForm.value;
    var productValue = data.SubCategory;
    if (productValue.length) {
      for (var i in productValue) {
        var pd = productValue[i].id;
        this.businessDetailUpdate.ProductIds.push(pd);
      }
    }
    else {
      this.businessDetailUpdate.ProductIds=[];
    }

    this.businessDetailUpdate.CityId = data.city;
    this.businessDetailUpdate.PrimaryTradeId = data.primaryTradeId ? data.primaryTradeId : 0;
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
      debugger
      if (status = httpStatus.Ok) {
        this.common.PostData(this.common.apiRoutes.Supplier.AddSupplierBusinessDetails, this.businessDetailUpdate, true).then(result => {
          debugger
          if (status = httpStatus.Ok) {
            this.common.PostData(this.common.apiRoutes.Supplier.AddUpdateSupplierProfileImage, this.ImageVm).then(result => {
              if (status = httpStatus.Ok) {
                debugger
                var data = result.json();
                this.common.Notification.success("Profile Successfully Updated.")
              }
              else {
                this.common.Notification.error("Some thing went wrong");
              }
            });
          }
          else {
            this.common.Notification.error("Some thing went wrong");
          }
        });
      }
      else {
        this.common.Notification.error("Some thing went wrong");
      }
    });
  }

  public PopulateData() {
    debugger
    this.common.GetData(this.common.apiRoutes.Supplier.GetDropDownOptionWeb, true).then(result => {
      if (status = httpStatus.Ok) {
        var data = result.json();
        this.CitiesList = data.cities;
        this.DeliveryRadiousList = data.distances;
        this.PrimaryTradedList = data.productCategories;
        this.common.GetData(this.common.apiRoutes.Supplier.GetBusinessAndPersnalProfileWeb, true).then(result => {
          if (status = httpStatus.Ok) {
            this.profile = result.json();
            this.appValForm.patchValue(this.profile.persnalDetails);
            this.appValForm.patchValue(this.profile.businessDetails);
            this.subCategoriesList = this.profile.businessDetails.productsubCategory;
            this.fullName = this.profile.persnalDetails.firstName + " " + this.profile.persnalDetails.lastName;
            if (this.subCategoriesList.length == 0) {
              this.subCategoriesList = this.profile.businessDetails.selectedSubCategory;
            }
            this.SelectedSubCategoriesList = this.profile.businessDetails.selectedSubCategory;
            debugger
            this.appValForm.controls.dateOfBirth.setValue(this.common.formatDate(this.profile.persnalDetails.dateOfBirth, "DD MMM YYYY"));
            this.appValForm.controls.SubCategory.setValue(this.profile.businessDetails.selectedSubCategory);

           // this.profileImageCheck = this.profile.persnalDetails.profileImage;
            if (this.profile.persnalDetails.profileImage != null) {
              this.profileImageCheck = true;
              this.ImageVm.imageBase64 = 'data:image/png;base64,' + this.profile.persnalDetails.profileImage;
            }
          }
          else {
            this.common.Notification.error("Some thing went wrong.");
          }
        });
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    });
  }



  OnSelectFile(event) {
    debugger
    this.profileImageCheck = true;
    if (event.target.files && event.target.files[0]) {
      //image size optimization
      this.file = event.target.files[0];
      var filename = this.file['name'];
      console.log("file name:" + filename);
      console.log("file size:" + this.file['size']);
      console.log("file size after division:" + this.file['size'] / (1024 * 1024));
      console.log("file:" + this.file);
      var reader = new FileReader();
      this.imagePath = event.target.files;
      reader.readAsDataURL(event.target.files[0]);
      reader.onload = (event) => {
        this.localUrl = reader.result;
        var test = this.compressFile(this.localUrl, filename);
        //this.ImageVm.imageBase64 = test;
      }
    }
  }

  dataURItoBlob(dataURI) {
    const byteString = window.atob(dataURI);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8Array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) {
      int8Array[i] = byteString.charCodeAt(i);
    }
    const blob = new Blob([int8Array], { type: 'image/png' });
    return blob;
  }



  public compressFile(image, fileName) {
    var orientation = -1;
    this.sizeOfOriginalImage = this.imageCompress.byteCount(image) / (1024 * 1024);
    console.log('Size in bytes is now:', this.sizeOfOriginalImage);

    this.imageCompress.compressFile(image, orientation, 50, 50).then(
      result => {
        this.imgResultAfterCompress = result;
        this.localCompressedURl = result;
        this.sizeOFCompressedImage = this.imageCompress.byteCount(result) / (1024 * 1024)
        console.log('Size in bytes after compression:', this.sizeOFCompressedImage);
        const imageBlob = this.dataURItoBlob(this.imgResultAfterCompress.split(',')[1]);
        debugger
        this.ImageVm.imageBase64 = result;
        //this.profileImageCheck = this.ImageVm.imageBase64;
      })
  }

  EditProfile() {
    this.readOnly = "";
  }

  onCategorySelect(productCategoryId) {
    debugger;
    if (productCategoryId > 0) {
      this.common.get(this.common.apiRoutes.Supplier.GetAllProductSubCatagories + "?productId=" + productCategoryId).subscribe(result => {
        debugger;
        this.subCategoriesList = result.json();
        this.selectedItemsSubCategory = [];
      })
    }
  }

}
