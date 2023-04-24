import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';
import { IdValueVm, ResponseVm } from '../../../models/commonModels/commonModels';
import { GetQuotes, ImageVM, Images } from '../../../models/userModels/userModels';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { JobQuotationErrors, BidStatus, httpStatus } from '../../../shared/Enums/enums';
import { NgxImageCompressService } from 'ngx-image-compress';
import { ModalDirective } from 'ngx-bootstrap';

@Component({
  selector: 'step1',
  templateUrl: './step1.component.html',
  styleUrls: ['./step1.component.css']
})
export class Step1 implements OnInit {
  public listOfSkills: IdValueVm[] = [];
  public listOfSubSkills: IdValueVm[] = [];
  public showSubCategoryList: boolean = false;
  public showCategoryList: boolean = false;
  public submitted: boolean = false;
  public imageSubmitted: boolean = false;
  public selectedCategory: string;
  public selectedSubCategory: string;
  public getQuotationsVM: GetQuotes = new GetQuotes();
  public imageVm: ImageVM = new ImageVM();
  public listOfImages: ImageVM[] = []; 
  public jobQuotationForm: FormGroup;
  public categoryError: string;
  public subCategoryError: string;
  public jobTitleError: string;
  public jobDescriptionError: string;
  public imageError: string = JobQuotationErrors.imageError;
  public listofFiles: any[] = [];
  public response: ResponseVm = new ResponseVm();
  public imageContent: [] = [];
  @ViewChild('ImageModal', { static: true }) imageModal: ModalDirective;
  public imageVM: Images;

  predictions: number[];
  imageDataEvent: any;
  localUrl: any;
  localCompressedURl: any;
  isFlip: boolean = false;
  sizeOfOriginalImage: number;
  sizeOFCompressedImage: number;

    
  constructor(private service: CommonService, private formBuilder: FormBuilder, private imageCompress: NgxImageCompressService) {
     
  }

  ngOnInit() {
    this.service.get(this.service.apiRoutes.Jobs.GetTradesmanSkillsByParentId + "?parentId=0").subscribe(result => {
      this.listOfSkills = result.json();

    })

    this.jobQuotationForm = this.formBuilder.group({
      categoryId: ['', Validators.required],
      //subCategoryId: ['', Validators.required],
      workTitle: ['', Validators.required],
      jobDescription: ['', Validators.required]
    })
  }

  get f() { return this.jobQuotationForm.controls; }

  public GoNext() {
    debugger;
    this.getQuotationsVM.imageVMs = [];
    this.submitted = true;
    this.categoryError = JobQuotationErrors.categoryId;
    this.subCategoryError = JobQuotationErrors.subcatgoryId;
    this.jobTitleError = JobQuotationErrors.jobTitle;
    this.jobDescriptionError = JobQuotationErrors.jobDescription;
    this.getQuotationsVM = this.jobQuotationForm.value;
    this.getQuotationsVM.statusId = BidStatus.Pending;
    this.getQuotationsVM.jobstartDateTime = new Date();
    debugger;
    for (var i = 0; i < this.listOfImages.length; i++) {
      this.imageVM = new Images();
      this.imageVM.filePath = "img-" + new Date();
      this.imageVM.imageBase64 = this.listOfImages[i].imageBase64;
      this.imageVM.ImageContent = null;
      this.getQuotationsVM.imageVMs.push(this.imageVM);
    }
    if (this.jobQuotationForm.valid) {
      this.getQuotationsVM.imageVMs[0].IsMain = true;
      this.service.post(this.service.apiRoutes.Customers.JobQuotationsWeb, this.getQuotationsVM).subscribe(result => {
        this.response = result.json();
        if (this.response.status == httpStatus.Ok) {
          this.service.NavigateToRouteWithQueryString(this.service.apiUrls.User.Quotations.getQuotes2, { queryParams: { 'id': this.response.resultData } });
        }
      });
    }
  }

  public CheckingSubcategory(categoryId) {
    if (categoryId > 0) {
      this.showCategoryList = true;
    }
    else {
      this.showCategoryList = false;
    }
    this.getQuotationsVM.categoryId = categoryId;
    this.service.get(this.service.apiRoutes.Jobs.GetTradesmanSkillsByParentId + "?parentId=" + categoryId).subscribe(result => {
      
      var index = 0
      this.selectedSubCategory = "";
      while (this.listOfSkills[index].id != categoryId) {
        index++;
      }
      this.selectedCategory = this.listOfSkills[index].value;
      this.getQuotationsVM.skillName = this.listOfSkills[index].value;
      this.listOfSubSkills = result.json();
      if (this.listOfSubSkills.length > 0) {
        this.showSubCategoryList = true;
      }
      else {
        this.showSubCategoryList = false;
      }
    })
  }
  public SelectSubCategory(subcategoryId) {
    this.showCategoryList = true;
    this.getQuotationsVM.subCategoryId = subcategoryId;
    var index = 0
    
    while (this.listOfSubSkills[index].id != subcategoryId) {
      index++;
    }
    this.selectedSubCategory = "& " + this.listOfSubSkills[index].value;
  }

  OnSelectFile(event) {
    
    this.listOfImages = [];
    this.getQuotationsVM.imageVMs = [];
    if (event.target.files.length > 4) {
      this.imageSubmitted = true;
      return;
    }
    else {
      //image size optimization
      for (var i = 0; i < event.target.files.length; i++) {
        var reader = new FileReader();
        //var file = event.target.files[i];
        //this.imageVm.FilePath = file['name'];
        //console.log(i + " " + this.imageVm.FilePath);
        //reader.readAsDataURL(event.target.files[i]);
        reader.onload = (event: any) => {
          this.listofFiles.push(event.target.result);
          console.log("number of files = " + i);
          this.imageVm.localUrl = event.target.result;
          console.log(this.imageVm.localUrl + " index" + i);
          this.compressFile(this.imageVm.localUrl, this.imageVm.filePath, this.imageVm, i);
        }
        reader.readAsDataURL(event.target.files[i]);
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
  compressFile(image, fileName, imageVms, index) {
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
        this.imageVm = new ImageVM();
        this.imageVm.filePath = imageVms;
        const imageBlob = this.dataURItoBlob(this.imgResultAfterCompress.split(',')[1]);
        this.imageVm.imageBase64 = result;
        //this.getQuotationsVM.ImageVMs.push(this.imageVm);
        this.listOfImages.push(this.imageVm);
        //const imageFile = new File([result], imageName, { type: 'image/jpeg' });
        //console.log("file size:", imageFile['size'] / (1024 * 1024));
        //return result;
      })
  }

  ImagePopUp(imageByte) {
    debugger;
    this.imageModal.show();
    this.imageContent = imageByte;
  }
  Close() {
    this.imageModal.hide();
  }
}
