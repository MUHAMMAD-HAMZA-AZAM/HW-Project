import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '../../../shared/HttpClient/_http';
import { IdValueVm, ResponseVm } from '../../../models/commonModels/commonModels';
import { PostAdVM } from '../../../models/supplierModels/supplierModels';
import { PostAdErrors, AdsStatus, httpStatus } from '../../../shared/Enums/enums';
import { NgxImageCompressService } from 'ngx-image-compress';
import { ImageVM, Images } from '../../../models/userModels/userModels';
import { ModalDirective } from 'ngx-bootstrap';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-postad',
  templateUrl: './postad.component.html',
  styleUrls: ['./postad.component.css']
})
export class PostAdComponent implements OnInit {
  public postAdForm: FormGroup;
  public submittedPostAdForm = false;
  public productCategoryList: IdValueVm[] = [];
  public productSubCategoryList: IdValueVm[] = [];
  public cityList: IdValueVm[] = [];
  public postAdVm: PostAdVM = new PostAdVM();
  public submitted: boolean = false;
  public errorsList: any;
  public imageVM: Images = new Images();
  public imageVm: ImageVM = new ImageVM();
  public listOfImages: Images[] = [];
  public imageSubmitted: boolean = false;
  public listofFiles: any[] = [];
  @ViewChild('ImageModal', { static: true }) imageModal: ModalDirective;
  @ViewChild('confirmationModal', { static: false }) confirmationModal: ModalDirective;
  public sizeOfOriginalImage: number;
  public localCompressedURl: string;
  public sizeOFCompressedImage: number;
  public imageContent: any;
  public response: ResponseVm = new ResponseVm();
  imgResultBeforeCompress: string;
  imgResultAfterCompress: string;
  AdId: any;
  selectedSubcategoryName: string;
  constructor(
    public service: CommonService,
    private formBuilder: FormBuilder,
    private imageCompress: NgxImageCompressService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    debugger;
    this.route.queryParams.subscribe((params: Params) => {
      this.AdId = params['supplierAdsId'];
      if (this.AdId == undefined) {
        this.AdId = 0;
      }
      if (this.AdId > 0)
        this.PopulateData();
    });
    this.service.get(this.service.apiRoutes.Supplier.GetAllProductCategory).subscribe(result => {
      this.productCategoryList = result.json();
    });
    this.service.get(this.service.apiRoutes.Common.getCityList).subscribe(result => {
      this.cityList = result.json();
    })
    this.postAdForm = this.formBuilder.group({
      productCategoryId: ['', Validators.required],
      productSubCategoryId: [0, Validators.required],
      postTitle: ['', Validators.required],
      postDiscription: ['', Validators.required],
      price: ['', Validators.required],
      address: ['', Validators.required],
      town: ['', Validators.required],
      cityId: ['', Validators.required],
      collectionAvailable: [false, Validators.requiredTrue],
      deliveryAvailable: [false, Validators.requiredTrue],
    });

  }
  get f() {
    return this.postAdForm.controls;
  }
  public postAd() {
    this.errorsList = {
      categoryIdError: PostAdErrors.categoryIdError,
      subCategoryIdError: PostAdErrors.subCategoryIdError,
      adTitleError: PostAdErrors.postTitleError,
      descriptionError: PostAdErrors.productDescriptionError,
      priceError: PostAdErrors.priceError,
      cityError: PostAdErrors.cityError,
      townError: PostAdErrors.townError,
      addressError: PostAdErrors.addressError,
      collectionError: PostAdErrors.collectionError,
    }
    this.submitted = true;
    debugger;
    
    this.postAdVm = this.postAdForm.value;
    this.postAdVm.imageVMs = [];
    for (var i = 0; i < this.listOfImages.length; i++) {
      this.imageVM = new Images();
      this.imageVM.filePath = this.listOfImages[i].filePath;
      this.imageVM.imageBase64 = this.listOfImages[i].imageBase64;
      this.imageVM.ImageContent = null;
      this.postAdVm.imageVMs.push(this.imageVM);
    }
    if (this.postAdForm.invalid) {
      return;
    }
    else {
      this.postAdVm.statusId = AdsStatus.Pending;
      this.postAdVm.imageVMs[0].IsMain = true;
      this.service.PostData(this.service.apiRoutes.Supplier.SaveAndUpdateAd, this.postAdVm, true).then(result => {
        this.response = result.json();
        if (this.response != null) {
          var removeformat = this.postAdVm.imageVMs[0].imageBase64.split(',');
          console.log(removeformat[1]);
          console.log(this.postAdVm.imageVMs);
          this.service.NavigateToRouteWithQueryString(this.service.apiUrls.Supplier.PromoteAd, { queryParams: { supplierAdsId: this.response, subCategoryValue: this.selectedSubcategoryName, adTitle: this.postAdVm.postTitle, adImage: removeformat[1] } })
        }
      })
    }
    //  this.common.SaveAndNotify
  }

  public deleteAd() {
    debugger;
    this.confirmationModal.hide();
    this.service.GetData(this.service.apiRoutes.Supplier.DeleteAd + "?supplierAdId=" + this.AdId).then(data => {
      debugger;
      this.service.NavigateToRoute(this.service.apiUrls.Supplier.ManageAd);
    });
  }
  openConfirmationModal() {

    this.confirmationModal.show();
  }

  SelectCategroy(productCategoryId) {
    this.postAdVm.productCategoryId = productCategoryId;
    if (productCategoryId > 0) {
      this.service.get(this.service.apiRoutes.Supplier.GetAllProductSubCatagories + "?productId=" + productCategoryId).subscribe(result => {
        this.productSubCategoryList = result.json();
      })
    }
  }
  SelectSubCategory(productSubCategroyId) {
    this.postAdVm.productSubcategoryId = productSubCategroyId;
    var count = 0;
    while (this.productSubCategoryList[count].id == productSubCategroyId) {
      count++;
    }
    this.selectedSubcategoryName = this.productSubCategoryList[count].value;
  }
  SelectCity(cityId) {
    this.postAdVm.cityId = cityId;
  }


  // Image Part

  OnSelectFile(event) {
    debugger;
    this.listOfImages = [];
    this.postAdVm.imageVMs = [];
    if (event.target.files.length > 4) {
      this.imageSubmitted = true;
      return;
    }
    else {
      //image size optimization
      for (var i = 0; i < event.target.files.length; i++) {
        var reader = new FileReader();
        var file = event.target.files[i];
        this.imageVm.filePath = file['name'];
        //console.log(i + " " + this.imageVm.FilePath);
        //reader.readAsDataURL(event.target.files[i]);
        reader.onload = (event: any) => {
          this.listofFiles.push(event.target.result);
          //console.log("number of files = " + i);
          this.imageVm.localUrl = event.target.result;
          //console.log(this.imageVm.localUrl + " index" + i);
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


  
  compressFile(image, fileName, imageVms, index) {
    var orientation = -1;
    this.sizeOfOriginalImage = this.imageCompress.byteCount(image) / (1024 * 1024);
    //console.log('Size in bytes is now:', this.sizeOfOriginalImage);
    this.imageCompress.compressFile(image, orientation, 50, 50).then(
      result => {
        this.imgResultAfterCompress = result;
        //console.log("byte array after compressions" + this.imgResultAfterCompress);
        this.localCompressedURl = result;
        this.sizeOFCompressedImage = this.imageCompress.byteCount(result) / (1024 * 1024)
        //console.log('Size in bytes after compression:', this.sizeOFCompressedImage);
        // create file from byte
        //const imageName = fileName;
        // call method that creates a blob from dataUri
        this.imageVM = new Images();
        this.imageVM.filePath = fileName;
        const imageBlob = this.dataURItoBlob(this.imgResultAfterCompress.split(',')[1]);
        debugger;
        this.imageVM.IsMain = false;
        this.imageVM.imageBase64 = result;
        //this.postAdVm.imageVMs.push(this.imageVM);
        this.listOfImages.push(this.imageVM);
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

  PopulateData() {
    var urls = this.service.apiRoutes.Supplier.GetEditAdDetail + "?supplierAdsId=" + this.AdId;
    this.service.GetData(urls, true).then(data => {
      debugger;
      this.postAdVm = data.json();
      this.service.get(this.service.apiRoutes.Supplier.GetPostAdImagesList + "?supplierAdsId=" + this.AdId).subscribe(result => {
        debugger;
        this.listofFiles = result.json();
        for (var i = 0; i < this.listofFiles.length; i++) {
          //this.listOfImages[i].imageBase64 = this.service.transform(this.listofFiles[i].imageContent)
          this.imageVM = new ImageVM();
          this.imageVM.imageBase64 = "data:image/png;base64," + this.listofFiles[i].imageContent;
          this.imageVM.filePath = this.listofFiles[i].filePath;
          this.imageVM.IsMain = this.listofFiles[i].isMain;
          this.listOfImages.push(this.imageVM);
        }
      })
      if (this.postAdVm != null) {
        debugger;
        if (this.postAdVm.productSubcategoryId != null) {
          this.service.get(this.service.apiRoutes.Supplier.GetAllProductSubCatagories + "?productId=" + this.postAdVm.productCategoryId).subscribe(result => {
            this.productSubCategoryList = result.json();

            var count = 0;
            while (this.productSubCategoryList[count].id != Number(this.postAdVm.productSubcategoryId)) {
              count++;
            }
            this.selectedSubcategoryName = this.productSubCategoryList[count].value;
          })
        }
        this.postAdForm.patchValue(this.postAdVm);
      } 

    });
    
  }
}
