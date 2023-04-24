import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '../../../shared/HttpClient/_http';
import { IdValueVm, ResponseVm } from '../../../models/commonModels/commonModels';
import { PostAdVM } from '../../../models/supplierModels/supplierModels';
import { PostAdErrors, AdsStatus, httpStatus, CommonErrors } from '../../../shared/Enums/enums';
import { NgxImageCompressService } from 'ngx-image-compress';
import { ImageVM, Images } from '../../../models/userModels/userModels';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ActivatedRoute, Params } from '@angular/router';
import { findIndex } from 'rxjs/operators';
//import { ImageCompressor } from 'image-compressor';
import { log } from 'util';
import { IIdValue, IImage, ITown } from '../../../shared/Enums/Interface';
@Component({
  selector: 'app-postad', 
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateAdComponent implements OnInit {
  public appValForm: FormGroup;
  public submittedPostAdForm = false;
  public productCategoryList: IdValueVm[] = [];
  public productSubCategoryList: IdValueVm[] = [];
  public discountList: IdValueVm[] = [{ id: 0, value: "5" }, { id: 1, value: "10" }, { id: 2, value: "15" }, { id: 4, value: "20" }, { id: 5, value: "25" }, { id: 6, value: "30" },
    { id: 7, value: "35" }, { id: 8, value: "40" }, { id: 9, value: "45" }, { id: 10, value: "50" }, { id: 11, value: "55" }, { id: 12, value: "60" }, { id: 13, value: "65" },
    { id: 14, value: "70" }, { id: 15, value: "75" }, { id: 16, value: "80" }, { id: 17, value: "85" }, { id: 18, value: "90" }, { id: 19, value: "95" }, { id: 20, value: "100" }
  ];

  public cityList: IdValueVm[] = [];
  public postAdVm: PostAdVM = new PostAdVM();
  public submitted: boolean = false;
  public errorsList: any;
  public imageVM: Images = {} as  Images;
  public imageVm: ImageVM = {} as  ImageVM;
  public listOfImages: Images[] = [];
  public imageSubmitted: boolean = false;
  public hasImage: boolean = true;
  public listofFiles: IImage[] = [];
  @ViewChild('ImageModal', { static: true }) imageModal: ModalDirective;
  @ViewChild('confirmationModal', { static: false }) confirmationModal: ModalDirective;
  @ViewChild('verifyAccountMessageModal', { static: false }) verifyAccountMessageModal: ModalDirective;
  public sizeOfOriginalImage: number=0;
  public localCompressedURl: string="";
  public sizeOFCompressedImage: number=0;
  public imageContent: any;
  public removeformatAdImage: any;
  public response: ResponseVm = {} as ResponseVm;
  public imgResultBeforeCompress: string="";
  public imgResultAfterCompress: string="";
  public AdId: any;
  public categoryId: any;
  public categoryName: any;
  public selectedSubcategoryName: string | undefined = "";
  public token: string|null="";
  public compressedThumb: string="";
  public imageThumb: string="";
  public townList: ITown[] = [];
  public searchtownList: IIdValue[] = [];
  townkeyword = 'value';
  public incompletedProfile = false;
  public showProductSubCategory= false;
  constructor(
    public common: CommonService,
    private formBuilder: FormBuilder,
    private imageCompress: NgxImageCompressService,
    private route: ActivatedRoute
   
  ) {
    this.appValForm = {} as FormGroup;
    this.imageModal = {} as ModalDirective;
    this.confirmationModal = {} as ModalDirective;
    this.verifyAccountMessageModal = {} as ModalDirective;
  }

  ngOnInit() {
    this.getSupplierBusinessDetails();
    this.route.queryParams.subscribe((params: Params) => {
      this.AdId = params['supplierAdsId'];
      this.categoryId = params['CategoryId'];
      if (this.AdId == undefined)
        this.AdId = 0;
      if (this.categoryId == undefined)
        this.categoryId = 0;
      this.PopulateData();
      if (this.categoryId > 0)
        this.SelectCategroy(this.categoryId);
    });
    this.token = localStorage.getItem("auth_token");
    this.GetCitiesAndCategory();

    this.appValForm = this.formBuilder.group({
      productCategoryId: ['', Validators.required],
      productSubCategoryId: [null, Validators.required],
      postTitle: ['', Validators.required],
      postDiscription: ['', [Validators.required, Validators.maxLength(250), Validators.minLength(10)]],
      price: ['', [Validators.required, Validators.maxLength(10), Validators.minLength(3)]],
      address: ['', Validators.required],
      town: ['',[ Validators.required]],
      cityId: ['', Validators.required],
      discount: ['', Validators.required],
      collectionAvailable: [true, Validators.required],
      deliveryAvailable: [false],
    });
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
      imageError: PostAdErrors.ImagesError
    }
    this.hasImage = false;
    this.getTownList();
  }
  getSupplierBusinessDetails() {
    
      this.common.get(this.common.apiRoutes.Supplier.GetBusinessProfile).subscribe(result => {
        let res = <any>result ;
        if (res != null) {
          if (res.companyName == null || res.companyName == "") {
            this.incompletedProfile = true;
          }
          else {
            this.incompletedProfile = false;
          }
        }
        else {
          this.incompletedProfile = true;
        }
        console.log(result );
      })
  }
  public getTownList() {  
    this.common.GetData(this.common.apiRoutes.UserManagement.GetTownsByCityId + "?cityId=" + "64", false).then(res => {
      this.townList = res ;
      this.townList.forEach((x) => {
        this.searchtownList.push({ value: x.name, id: x.townId });
      })
    });

  }
  public selecttownEvent(item: Event) {

  }

  public unselecttownEvent(item: Event) {
  }

  public onChangetownSearch(val: string) {
    // fetch remote data from here
    // And reassign the 'data' which is binded to 'data' property.
  }

  
  get f() {
    return this.appValForm.controls;
  }
  public GetCitiesAndCategory() {
    this.common.GetData(this.common.apiRoutes.Supplier.GetAllProductCategory, true).then(result => {
      this.productCategoryList = result ;
      if (this.productCategoryList.length > 0) {
        this.showProductSubCategory = true;
      }
      else {
        this.showProductSubCategory = false;
      }
    });
    this.common.GetData(this.common.apiRoutes.Common.getCityList, true).then(result => {
      this.cityList = result ;
      console.log(this.cityList);
    })
  }

  public postAd() {
    
    this.submitted = true;
    if (this.appValForm.invalid) {
      this.appValForm.markAllAsTouched();
    }

    this.common.GetData(this.common.apiRoutes.IdentityServer.GetPhoneNumberVerificationStatus + "?userId=" + this.token, true).then(result => {
      
      if (result  == true) {
       
    this.postAdVm = this.appValForm.value;
    if (this.postAdVm.productCategoryId == 0) {
      this.appValForm.controls.productCategoryId.setValue(this.categoryId);
      this.postAdVm = this.appValForm.value;
        }
        if (this.postAdVm.productSubcategoryId == undefined) {

          this.appValForm.controls.productSubCategoryId.setValue(7);
          this.postAdVm = this.appValForm.value
        }
        this.postAdVm.imageVMs = [];

        if (this.listOfImages.length == 0) {
          this.hasImage = true;
          return
        }
        else {
          this.hasImage = false;
          for (var i = 0; i < this.listOfImages.length; i++) {
            this.imageVM = {} as  Images;
            this.imageVM.filePath = this.listOfImages[i].filePath;
            this.imageVM.imageBase64 = this.listOfImages[i].imageBase64;
            this.imageVM.ImageContent = "";
            if (i == 0) {
              this.imageVM.thumbImage = this.imageThumb;
            } else {
              this.imageVM.thumbImage = ""
            }
            this.postAdVm.imageVMs.push(this.imageVM);

          }
        }
    
    if (this.appValForm.invalid) {
      this.appValForm.markAllAsTouched();
    }
    else {
      if (this.AdId == 0) {
        this.postAdVm.statusId = AdsStatus.Pending;
      }
      if (this.postAdVm.imageVMs != null && this.postAdVm.imageVMs.length > 0) {
        this.postAdVm.imageVMs[0].IsMain = true;
        var imageBytes = this.postAdVm.imageVMs[0].imageBase64?.split(',');
        this.removeformatAdImage = imageBytes?imageBytes[1]:"";
      }
      this.postAdVm.supplierAdId = this.AdId;
      console.log(this.postAdVm);
      let selectedTown = this.appValForm.value;
      this.postAdVm.town = selectedTown.town.value;
      this.common.PostData(this.common.apiRoutes.Supplier.SaveAndUpdateAd, this.postAdVm, true).then(result => {
        this.response = result ;
        if (this.response != null) {
          localStorage.setItem("adImage", this.removeformatAdImage);
          this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Supplier.PromoteAd, { queryParams: { supplierAdsId: this.response, subCategoryValue: this.selectedSubcategoryName, adTitle: this.postAdVm.postTitle, adImage: null } })
        }
      }, error => {
        console.log(error);
        this.common.Notification.error(CommonErrors.commonErrorMessage);
      })
        }
      }
      else {
        this.verifyAccountMessageModal.show();
      }

    });
  }
  public closeVerifyAccountMessageModal() {
    this.verifyAccountMessageModal.hide();
  }
  public verifyAccount() {
    this.common.NavigateToRoute(this.common.apiUrls.Supplier.Profile);
  }

 
  public deleteAd() {
    this.confirmationModal.hide();
    this.common.GetData(this.common.apiRoutes.Supplier.DeleteAd + "?supplierAdId=" + this.AdId).then(data => {
      this.common.NavigateToRoute(this.common.apiUrls.Supplier.ManageAd);
    });
  }

  public openConfirmationModal() {

    this.confirmationModal.show();
  }
  public closeConfirmationModal() {

    this.confirmationModal.hide();
  }

  public SelectCategroy(productCategoryId: number) {
    this.postAdVm.productCategoryId = productCategoryId;
    if (productCategoryId > 0) {
      this.common.get(this.common.apiRoutes.Supplier.GetAllProductSubCatagories + "?productId=" + productCategoryId).subscribe(result => {
        this.productSubCategoryList = <IdValueVm[]>result;
        this.appValForm.controls.productCategoryId.setValue(productCategoryId);
      }, error => {
        console.log(error);
        this.common.Notification.error(CommonErrors.commonErrorMessage);
      })
    }
  }

  public SelectSubCategory(productSubCategroyId: number) {
    this.postAdVm.productSubcategoryId = productSubCategroyId;
    this.productSubCategoryList.forEach(value => {
      if (value.id == this.postAdVm.productSubcategoryId) {
        this.selectedSubcategoryName = value.value;
      }
    }); 
    //var count = 0;
    //while (this.productSubCategoryList[count].id == productSubCategroyId) {
    //  count++;
    //}
  }

  public SelectCity(cityId: string) {
    this.postAdVm.cityId = cityId;
  }

  public selectDiscount(event: Event) {
    this.postAdVm.discount = (<HTMLSelectElement>event.target).options[(<HTMLSelectElement>event.target).options.selectedIndex].text;
  }
  public OnSelectFile(event: Event) {
    
    this.hasImage = false;
    // this.listOfImages = [];
    this.postAdVm.imageVMs = [];
    let fileLength = (<HTMLInputElement>event?.target)?.files?.length
    if (fileLength) {
      var imageCount = (fileLength) + (this.listOfImages.length);
      if (fileLength > 10 || this.listOfImages.length > 10 || imageCount > 10) {
        this.imageSubmitted = true;
        this.common.Notification.error(this.errorsList.imageError);
        return;
      }
      else {
        for (var i = 0; i < fileLength; i++) {

          if (i == 0) {
            var imageRead;
            var fReader = new FileReader();
            var isThumb = (<HTMLInputElement>event.target).files?.[0];
            isThumb ? fReader.readAsDataURL(isThumb):"";
            fReader.onload = (event: any) => {

              imageRead = event.target.result;
              console.log(this.imageCompress.byteCount(imageRead) + "Before")
              this.imageCompress.compressFile(imageRead, -1, 50, 40).then(res => {
                console.log(this.imageCompress.byteCount(res) + "After")
                this.imageThumb = res;
                console.log(this.imageThumb);
              })
            };
          }
          else {
            this.imageThumb = "";
          }
          var reader = new FileReader();
          var file = (<HTMLInputElement>event.target).files?.[i];
          this.imageVm.filePath = file ? file['name']:"";
          reader.onload = (event: any) => {
            this.listofFiles.push(event.target.result);
            this.imageVm.localUrl = event.target.result;
            this.imageVm.localUrl = this.imageVm.localUrl ? this.imageVm.localUrl : "";
            this.compressFile(this.imageVm.localUrl, this.imageVm.filePath, this.imageVm, i);
          }
          let blobFile = (<HTMLInputElement>event.target).files?.[i];
          blobFile ? reader.readAsDataURL(blobFile):"";

        }

      }
    }
  }
  public compressFile(image: string, fileName: string, imageVms: ImageVM, index: number) {
    
    var orientation = -1;
    this.sizeOfOriginalImage = this.imageCompress.byteCount(image) / (1024 * 1024);
    this.imageCompress.compressFile(image, orientation, 50, 50).then(
      result => {
        this.imgResultAfterCompress = result;
        this.localCompressedURl = result;
        this.sizeOFCompressedImage = this.imageCompress.byteCount(result) / (1024 * 1024)
        this.imageVM = {} as  Images;
        this.imageVM.filePath = fileName;
        const imageBlob = this.dataURItoBlob(this.imgResultAfterCompress.split(',')[1]);
        this.imageVM.IsMain = false;
        this.imageVM.imageBase64 = result;
        this.listOfImages.push(this.imageVM);
      })
    console.log(this.listOfImages);
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


  public ImagePopUp(imageByte: string) {
    
    this.imageModal.show();
    this.imageContent = imageByte;
  }

  public Close() {
    this.imageModal.hide();
  }

  public PopulateData() {
    if (this.AdId > 0) {
      this.common.GetData(this.common.apiRoutes.Supplier.GetEditAdDetail + "?supplierAdsId=" + this.AdId, true).then(data => {
        this.postAdVm = data ;
        this.common.GetData(this.common.apiRoutes.Supplier.GetPostAdImagesList + "?supplierAdsId=" + this.AdId, true).then(result => {
          this.listofFiles = result ;
          for (var i = 0; i < this.listofFiles.length; i++) {
            this.imageVM = {} as  ImageVM;
            this.imageVM.imageBase64 = "data:image/png;base64," + this.listofFiles[i].imageContent;
            this.imageVM.filePath = this.listofFiles[i].filePath;
            this.imageVM.IsMain = this.listofFiles[i].isMain;
            this.listOfImages.push(this.imageVM);
          }
        }, error => {
          console.log(error);
        });
        if (this.postAdVm != null) {
          if (this.postAdVm.productSubcategoryId != null) {
            this.common.GetData(this.common.apiRoutes.Supplier.GetAllProductSubCatagories + "?productId=" + this.postAdVm.productCategoryId, true).then(result => {
              this.productSubCategoryList = result ;
              var count = 0;
              while (this.productSubCategoryList[count].id != Number(this.postAdVm.productSubcategoryId)) {
                count++;
              }
              this.selectedSubcategoryName = this.productSubCategoryList[count].value;
            })
          }
          this.appValForm.patchValue(this.postAdVm);
          this.appValForm.controls.productSubCategoryId.setValue(this.postAdVm.productSubcategoryId);

        }
      }, error => {
        console.log(error);
      });
    }
    else {
      this.common.GetData(this.common.apiRoutes.Supplier.GetBusinessProfile, true).then(result => {
        var data = result ;
       // this.appValForm.controls.address.setValue(data.businessAddress);
        this.appValForm.controls.cityId.setValue(data.city);
      }, error => {
        console.log(error)
      });
    }
  }

  charOnly(event: KeyboardEvent): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if ((charCode > 47 && charCode < 58) || ((charCode > 32 && charCode <= 47)) || ((charCode >= 58 && charCode <= 64)) || ((charCode >= 91 && charCode <= 96)) || ((charCode >= 123 && charCode <= 126))) {
      return false;
    }
    return true;
  }

  AllowNonZeroIntegers(e: KeyboardEvent): boolean {

    var val = e.keyCode;
    // var target = event.target ? event.target : event.srcElement;
    if (val == 48 && (<HTMLInputElement>event?.target).value == "" || val == 101 || val == 45 || ((val >= 65 && val <= 90)) || ((val >= 97 && val <= 122))) {
      return false;
    }
    else if ((val >= 48 && val < 58) || ((val > 96 && val < 106)) || val == 46 || val == 8 || val == 127 || val == 189 || val == 109 || val == 9) {
      return true;
    }
    else {
      return false;
    }
  }



  CancelImage(index: number) {
    
    //var index = findIndex(image);
    this.listOfImages.splice(index-1, 1);

   // var index = findIndex.call(ids);
    
    //var ids = document.getElementById("PostImage");
    //ids.remove();
    //var index= id.tabIndex;
    //if (this.listOfImages.length > 0) {
    // // this.listOfImages.length = this.listOfImages.length - 1;
    //}
  }
  //compressThumbFile(img: any) {
  //  
  //  const compressorSettings = {
  //    toWidth: 200,
  //    toHeight: 230,
  //    mimeType: 'image/jpeg',
  //    mode: 'stretch',
  //    quality: 0.6,
  //    grayScale: false,
  //    sepia: true,
  //    threshold: 127,
  //    vReverse: false,
  //    hReverse: false,
  //    speed: 'low'
  //  };
  //  const imageCompressor = new ImageCompressor;
  //  imageCompressor.run(img, compressorSettings, this.proceedImage)

  //}
  //proceedImage(src) {
  //  document.getElementById('imageThumb').setAttribute('src', src);
  //  
  //  this.imageThumb = src;
  //  console.log(src);
  //  console.log(this.imageThumb);
  //}

}
