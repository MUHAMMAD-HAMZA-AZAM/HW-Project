import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Customer, ProfileImage } from '../../../models/userModels/userModels';
import { CommonService } from '../../../shared/HttpClient/_http';
import { ActivatedRoute, Data } from '@angular/router';
import { httpStatus } from '../../../shared/Enums/enums';
import { NgxImageCompressService } from 'ngx-image-compress';

@Component({
  selector: 'app-personal-profile-edit',
  templateUrl: './personalProfileEdit.component.html',
})
export class PersonalProfileEditComponent implements OnInit {

  public submittedForm = false;
  public appValForm: FormGroup;
  public imageBase64;
  public imagePath;
  public BASE64_MARKER = ';base64,';
  public file: any;
  public predictions: number[];
  public imageDataEvent: any;
  public localUrl: any;
  public localCompressedURl: any;
  public isFlip: boolean = false;
  public sizeOfOriginalImage: number;
  public sizeOFCompressedImage: number;
  public id: number;
  public profile: Customer = new Customer();
  public ImageVm: ProfileImage = new ProfileImage();

  constructor(
    public common: CommonService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private imageCompress: NgxImageCompressService
  ) { }

  ngOnInit() {
    this.PopulateData();
    this.appValForm = this.formBuilder.group({
      dateOfBirth: [''],
      firstName: [''],
      lastName: ['',],
      gender: [''],
      mobileNumber: [''],
      email: [''],
      cnic: [''],
    });
  }

  get f() {
    return this.appValForm.controls;
  }

  public Update() {
    debugger
    this.submittedForm = true;
    if (this.appValForm.invalid) {
      return;
    }
    this.profile = this.appValForm.value;
    this.common.PostData(this.common.apiRoutes.Users.CustomerProfileUpdate, this.profile, true).then(result => {
      debugger;
      if (status = httpStatus.Ok) {
        this.profile = result.json();
        this.appValForm.patchValue(this.profile);
        this.appValForm.controls.dateOfBirth.setValue(this.common.formatDate(this.profile.dateOfBirth));
        this.common.PostData(this.common.apiRoutes.Users.AddUpdateUserProfileImage, this.ImageVm).then(result => {
          if (status = httpStatus.Ok) {
            var data = result.json();
          }
        });
        this.common.NavigateToRoute(this.common.apiUrls.User.PersonalProfile);
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    });
  }
  public PopulateData() {
    this.common.GetData(this.common.apiRoutes.Users.CustomerProfile, true).then(result => {
      debugger;
      if (status = httpStatus.Ok) {
        this.profile = result.json();
        this.appValForm.patchValue(this.profile);
        this.ImageVm.imageBase64 = this.profile.profileImage;
        this.ImageVm.imageBase64 = 'data:image/png;base64,' + this.profile.profileImage;
        this.appValForm.controls.dateOfBirth.setValue(this.common.formatDate(this.profile.dateOfBirth));
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    });
  }


  OnSelectFile(event) {
    debugger
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


  imgResultBeforeCompress: string;
  imgResultAfterCompress: string;
  compressFile(image, fileName) {
    var orientation = -1;
    this.sizeOfOriginalImage = this.imageCompress.byteCount(image) / (1024 * 1024);
    console.log('Size in bytes is now:', this.sizeOfOriginalImage);

    this.imageCompress.compressFile(image, orientation, 50, 50).then(
      result => {
        this.imgResultAfterCompress = result;
        this.localCompressedURl = result;
        this.sizeOFCompressedImage = this.imageCompress.byteCount(result) / (1024 * 1024)
        console.log('Size in bytes after compression:', this.sizeOFCompressedImage);
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
}

