import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpResponse } from 'aws-sdk';
import { ToastrService } from 'ngx-toastr';
import { StatusCode } from '../../Shared/Enums/enum';
import { IProductDetail, IProductVM } from '../../Shared/Enums/Interface';
import { CommonService } from '../../Shared/HttpClient/HttpClient';
import { UploadFileService } from '../../Shared/HttpClient/upload-file.service';

@Component({
  selector: 'app-customer-feedback',
  templateUrl: './customer-feedback.component.html',
  styleUrls: ['./customer-feedback.component.css']
})
export class CustomerFeedbackComponent implements OnInit {
  productDetails: IProductVM[] = [];
  public imageLoader: boolean = false;
  selectedFileName: string = "";
  productDetail: any = "";
  customerId: number = 0;
  userName: string = "";
  productId: number = 0;
  public decodedToken: any = "";
  public appValForm: FormGroup;
  constructor(
    public _httpService: CommonService,
    private router: ActivatedRoute,
    public route: ActivatedRoute,
    private toastr: ToastrService,
    public _fileService: UploadFileService
    , public formBuilder: FormBuilder  ) {

    this.appValForm = {} as FormGroup;
  }

  selectFile(filePath: string, fileName: string) {
    this.selectedFileName = this._httpService.AWSPATH + filePath + fileName;
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(x => {
      this.productId = x.productId;
      this.getProductDetailWeb(x.productId);
    });
    this.appValForm = this.formBuilder.group({
      description: ['', [Validators.required, Validators.pattern("[a-zA-Z][a-zA-Z ]+[a-zA-Z ]$")]],
      overallRating: [Validators.required]
    });
  }
  getProductDetailWeb(productId: string) {
    this._httpService.GetData1(this._httpService.apiUrls.Supplier.Product.GetProductDetailWeb + `?productId=${productId}`).then(response => {
      this.productDetails = (<any>response).resultData;
      console.log(this.productDetails);
      let productDetail = this.productDetails.reduce((accumulated: IProductDetail[], { id, name, slug, price, discountedPrice, discount, attributeName, attributeValue, shopName, description, youtubeURL, weight, traxCityId, supplierId, firebaseClientId, availability, categoryName, categoryId, subCategoryName, subCategoryId, categoryGroupId, categoryGroupName, variantId, hexCode, isFreeShipping }) => {
        if (!accumulated.some(o => o.id == id)) {
          let groupItem = new Array();
          let attributes = new Array();
          let images = new Array();
          let fileIds = new Array();
          for (var i = 0; i < this.productDetails.length; i++) {

            if (this.productDetails[i].fileId) {
              if (!fileIds.some(y => y.fileId == this.productDetails[i].fileId)) {
                fileIds.push({
                  fileId: this.productDetails[i].fileId,
                  filePath: this.productDetails[i].filePath,
                  fileName: this.productDetails[i].fileName,
                  isMain: this.productDetails[i].isMain
                });
                images = fileIds;
              }
            }
          }
          accumulated.push({ id, name, slug, price, discountedPrice, discount, variantId, hexCode, shopName, attributeName, attributeValue, youtubeURL, weight, traxCityId, description, supplierId, firebaseClientId, availability, categoryId, categoryName, subCategoryName, subCategoryId, categoryGroupId, categoryGroupName, groupItem, attributes, images, isFreeShipping });
        }
        this.imageLoader = true;
        return accumulated;
      }, []);

      this.productDetail = productDetail[0];
    });
  }

  leaveFeedBack() {
    this.decodedToken = this._httpService.decodedToken();
    this.customerId = this.decodedToken?.Id;
    debugger
    let obj = {
      productId: this.productId,
      customerId: this.customerId,
      description: this.appValForm.value.description,
      rating: this.appValForm.value.overallRating
    }
    this._httpService.PostData(this._httpService.apiUrls.Supplier.Product.AddCustomerFeedBack, JSON.stringify(obj)).then(response => {
      var res = response;
      debugger
      if (response.status == StatusCode.OK) {
        this.appValForm.reset();
        this.toastr.success("Leave FeedBack Successfully", "Added");
      }
      else {
        this.toastr.error("Something went wrong please try again!", "Added");
      }
    });
  }
}
