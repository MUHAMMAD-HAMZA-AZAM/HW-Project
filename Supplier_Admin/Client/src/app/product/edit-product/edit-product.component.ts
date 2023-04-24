import { HttpStatusCode } from '@angular/common/http';
import { Component, ElementRef, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxImageCompressService } from 'ngx-image-compress';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { from, Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged, map, switchMap } from 'rxjs/operators';
import { IBulkDetails, IBulkOrdering, IProduct, IProductAttributeValue, IProductCategoryGroupList, IProductCategoryList, IProductSubCategory, ISelectedCategoryList, IVarientDetails } from '../../Shared/Enums/Interface';
import { UploadFileService } from '../../Shared/HttpClient/upload-file.service';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent implements OnInit {
  FOLDERPATH = 'photos/product/'
  appFormValue: FormGroup;
  bulkOrderForm: FormGroup;
  detailsFlag: boolean = false;
  imageRequiredFlag: boolean = false;
  isChanged: boolean = false;
  showCategoryList: boolean = false
  isSubCategory: boolean = false;
  isSubCategoryGroup: boolean = false;
  isCategoryAttributes: boolean = false;
  disableFlag: boolean = true;
  inputFileChanged: boolean = false;
  isConfirmedSelection: boolean = false;
  selectedIndex: number = 0;
  hexCode: string = "";
  varientName: string = "";
  invalidmMinInput: boolean = false;
  invalidMaxInput: boolean = false;
  invalidCompareInput: boolean = false;
  currentindex: number = 0;
  selectedSubIndex: number = 0;
  currentVarientId: number = 0;
  selectedSubGroupIndex: number = 0;
  public isUserExist: boolean = false;
  public minProductImgHeight: number = 600;
  public minProductImgWidth: number = 800;
  attributeLabels: any = [];
  productAttributeList: any = [];
  bulkOrderList: IBulkOrdering[] = [];
  categoryList: IProductCategoryList[] = [];
  subCategoryList: IProductSubCategory[] = [];
  subCategoryGroupList: IProductCategoryGroupList[] = [];
  productDetails: IProduct[] = [];
  variantList: IVarientDetails[] = [];
  selectedImageList = new Array();
  productImageList = new Array();
  fileIdList = new Array();
  selectedFileList = new Array();
  selectedFilesName = new Array();
  product: any;
  markAsMainObj = {productId:null , fileName:""};
  productId: number = 0;
  selectedCategory: ISelectedCategoryList = { category: '', categoryId: null, subCategory: '', subCategoryId: null, categoryGroup: '', subCategoryGroupId: null };
  @ViewChild("productAttribute") productAttribute: ElementRef;
  @ViewChild("uploadedFile") uploadedFile: ElementRef;
  constructor(private imageCompress: NgxImageCompressService, public _modalService: NgbModal, public _toastr: ToastrService, public service: CommonService, public _fileService: UploadFileService, public Loader: NgxSpinnerService, private route: ActivatedRoute, public fb: FormBuilder) {
    this.uploadedFile = {} as ElementRef;
    this.productAttribute = {} as ElementRef;
    this.appFormValue = {} as FormGroup;
    this.bulkOrderForm = {} as FormGroup;
  }

  ngOnInit(): void {
    let that = this;
    this.createProductForm();
    this.createBulkOrderForm();
    this.removeBulkSkuFromGrop(0);
    this.route.params.subscribe(params => {
      this.productId = params['id'];
      this.getSupplierProductDetail(this.productId);
      that.getCategoryList()
    });
  }

  createBulkOrderForm() {
    this.bulkOrderForm = this.fb.group({
      bulkSku:this.fb.array([])
    })
  }

  createProductForm() {
    this.appFormValue = this.fb.group({
      id: [0],
      title: ['', [Validators.required]],
      searchTag: [null, [Validators.required]],
      image: [null],
      selectedCategory: [null],
      description: ['', [Validators.required]],
      youtubeURL: [''],
      categoryId: [null, [Validators.required]],
      subCategoryId: [null],
      weight: [null, [Validators.required, Validators.pattern('^[0-9]+(.[0-9]{0,4})?$')]],
      isActive: [true],
      subCategoryGroupId: [null],
      productAttributes: this.fb.array([]),
      productSku: this.fb.array([]),
      bulkSku: this.fb.array([])
    })
  }
  get f() {
    return this.appFormValue.controls;
  }
  getProductSearchTags = (text: string): Observable<any> => {
    return from(text).pipe(
      map(x => x),
      debounceTime(5000),
      distinctUntilChanged(),
      switchMap(value => this.service.get(this.service.apiUrls.Supplier.Product.GetProductSearchTagsList + `?inputText=${value}`).pipe(map((data: any) => (data.resultData))))
    )
  }
  
  handleInputFileChange(uploadedFiles: any) {
    let fileList = uploadedFiles.target.files;
    const reader = new FileReader();
    for (var i = 0; i < fileList.length; i++) {
      let fileType = (fileList[i].type).toLowerCase();
      //let fileName = ((fileList[i].name).toLowerCase()).substring(fileList[i].name.length - 5).includes('.jfif');
      if (fileType != "image/jpeg" && fileType != "image/jpg" && fileType != "image/png") {
        this._toastr.warning("You can't be able to upload file except JPEG,JPG and PNG format", "Image Format")
        this.uploadedFile.nativeElement.value = null;
        return;
      }
      if ((this.selectedImageList.length + fileList.length) > 5) {
        this._toastr.warning("You can add upto 5 images only", "Images")
        return;
      }
      else {
        let that = this;
        reader.readAsDataURL(fileList[i]);
        reader.onload = (e: any) => {
          const image = new Image();
          image.src = e.target.result;
          image.onload = function (event) {
            if (event.currentTarget['height'] < that.minProductImgHeight && event.currentTarget['width'] < that.minProductImgWidth) {
              that._toastr.error(`Please upload image with minimum these dimensions Width ${that.minProductImgWidth}px  and Height ${that.minProductImgHeight}px`, "Error", { timeOut: 5000 });
              that.uploadedFile.nativeElement.value = null;
              return false;
            }
            else {
              that.selectedFileList = [];
              that.inputFileChanged = true;
              for (var j = 0; j < fileList.length; j++) {
                that.selectedFileList.push(fileList[j]);
              }
              let base64List = that.selectedImageList.map(x => x.file);
              that.convertToFile(base64List);
            }
          }
        }
      }
    }
    
  }
  convertToFile(base64List: any) {
    this._fileService.base64ToFile(base64List).then(response => {
      this.selectedFileList = [...this.selectedFileList, ...response];
      this.selectedImageList = [];
      this.selectedFilesName = [];
      const fileListCount = this.selectedFileList.length;
      for (let i = 0; i < fileListCount; i++) {
        let fileExtension = this.selectedFileList[i].name.split('.').pop();
        let obj = { fileName: `${(Math.random() * 10000).toString()}.${fileExtension}`, filePath: this.FOLDERPATH + `${this.service.decodedToken().Id}/`, isMain: i == 0 ? true : false };
        this.selectedFilesName.push(obj);
        this.imageCompress.getOrientation(this.selectedFileList[i]).then(ortn => {
          this._fileService.getBase64(this.selectedFileList[i]).then((image: string) => {
            this.selectedImageList.push({ file: image, fileName: obj.fileName });
            this.imageCompress.compressFile(image, ortn, 100, 20).then(result => {
              this._fileService.dataUrlToFile(result, (Math.random() * 10000).toString()).then(generatedFile => {
              })
            });
          })
        })
      }
    })

  }

  removeSelectedImage(item: any) {
    if (this.selectedImageList.length > 1) {
      this.inputFileChanged = true;
      let removedIndex = this.selectedImageList.findIndex(x => x.file == item.file);
      this.selectedImageList.splice(removedIndex, 1);
      let base64List = this.selectedImageList.map(x => x.file);
      this.selectedFileList = [];
      this.convertToFile(base64List)
    }
    else {
      this.imageRequiredFlag = true;
      setTimeout(() => {
        this.imageRequiredFlag = false;
      }, 5000)
    }

  }
  markAsMainImage(imgObj) {
    const idx = this.selectedImageList.findIndex(selectedImage => selectedImage.file == imgObj.file);
    const count = this.selectedFilesName.length;
    if (count > 0) {
      this.selectedFilesName.map(x => x.isMain = false);
      const index = this.selectedFilesName.findIndex(file => file.fileName == imgObj.fileName);
      if (count >= 0) {
        this.selectedFilesName[index].isMain = true;
        this.selectedImageList.map(x => x.isMain = false);
        this.selectedImageList[idx].isMain = true;
      }
    }
    else {
      this.selectedImageList.map(x => x.isMain = false);
      this.selectedImageList[idx].isMain = true;
       this.markAsMainObj = {
        productId: this.productId,
        fileName: imgObj.fileName
      }
    }
  }

  getSupplierProductDetail(productId: Number) {

    this.service.get(this.service.apiUrls.Supplier.Product.GetSupplierProductDetail + `?productId=${productId}`).subscribe(response => {
      this.productDetails = (<any>response).resultData;
      let productInventory = new Array();
      let bulkInventory = new Array();
      let productAttributes = new Array();
      let images = new Array();
      let fileIds = new Array();
      const tags = [];
      for (let i = 0; i < this.productDetails.length; i++) {
        if (i == 0) {
          this.product = {
            id: this.productDetails[i].id,
            name: this.productDetails[i].name,
            description: this.productDetails[i].description,
            youtubeURL: this.productDetails[i].youtubeURL,
            isActive: this.productDetails[i].isActive,
            categoryName: this.productDetails[i].categoryName,
            categoryId: this.productDetails[i].categoryId,
            weight: this.productDetails[i].weight,
            subCategoryName: (this.productDetails[i].subCategoryName) ? ' / ' + (this.productDetails[i].subCategoryName) : '',
            subCategoryId: this.productDetails[i].subCategoryId,
            categoryGroupId: this.productDetails[i].categoryGroupId,
            categoryGroupName: (this.productDetails[i].categoryGroupName) ? ' / ' + (this.productDetails[i].categoryGroupName) : '',
          }
        }
        if (!fileIds.some(fileId => fileId == this.productDetails[i].fileId)) {
          fileIds.push(this.productDetails[i].fileId);
          this._fileService.getFileByName(this.productDetails[i].filePath + this.productDetails[i].fileName, { fileId: this.productDetails[i].fileId, isMain: this.productDetails[i].isMain, fileName: this.productDetails[i].fileName, filePath: this.productDetails[i].filePath })
            .then(data => {
              this.selectedImageList.push({
                file: data.file,
                isMain: data.isMain,
                fileName: data.fileName,
                filePath: data.filePath,
                fileId: data.fileId
              })
           })
        }
        if ((!productAttributes.some(m => m.attributeId == this.productDetails[i].attributeId) && this.productDetails[i].attributeId != null)) {
          productAttributes.push({
            attributeId: this.productDetails[i].attributeId,
            attributeValue: this.productDetails[i].attributeValue,
            attributeName: this.productDetails[i].attributeName,
          });
        }
        if (!productInventory.some(n => n.variantId == this.productDetails[i].variantId)) {
          productInventory.push({
            productId: this.productDetails[i].id,
            quantity: this.productDetails[i].quantity,
            discountInPercentage: this.productDetails[i].discountInPercentage,
            price: this.productDetails[i].price,
            variantId: this.productDetails[i].variantId,
            availability: this.productDetails[i].availability
          });
        }
        if (!tags.some(t => t.tagId == this.productDetails[i].tagId) && this.productDetails[i].tagId > 0) {
            tags.push({ value: Math.floor(Math.random() * 1000), tagId: this.productDetails[i].tagId, display: this.productDetails[i].display })
        }
        if (!bulkInventory.some(n => n.bulkId == this.productDetails[i].bulkId)) {
          bulkInventory.push({
            bulkId: this.productDetails[i].bulkId,
            minQuantity: this.productDetails[i].minQuantity,
            maxQuantity: this.productDetails[i].maxQuantity,
            bulkdiscount: this.productDetails[i].bulkdiscount,
            bulkPrice: this.productDetails[i].bulkPrice,
            bulkVarientId: this.productDetails[i].bulkVarientId
          });
        }
      }
      this.bulkOrderList = bulkInventory;
      this.product.productAttributes = productAttributes;
      this.product.productInventory = productInventory;
      this.product.bulkInventory = bulkInventory;
      this.product.searchTags = tags;
      this.productImageList = this.selectedImageList;
      this.service.get(this.service.apiUrls.Supplier.Product.GetAllProductVariant).subscribe(result => {
        this.variantList = <any>result;
        this.bindProductData(this.product);
        //this.getSubProductList(this.product.categoryId,this.product.categoryName);
      })
    })
  }

  bindProductData(product: IProduct) {
    //changing
    let formData = {
      id: product.id,
      title: product.name,
      selectedCategory: product.categoryName + product.subCategoryName + product.categoryGroupName,
      categoryId: product.categoryId,
      subCategoryId: product.subCategoryId,
      subCategoryGroupId: product.categoryGroupId,
      description: product.description,
      weight: product.weight,
      youtubeURL: product.youtubeURL,
      isActive: product.isActive,
      searchTag: product?.searchTags.map(tag => { return { value: tag.value, display: tag.display } })
    };
    this.appFormValue.patchValue(formData);
    if (product.productAttributes.length > 0) {
      this.isCategoryAttributes = true;
      this.createDynamicFormControl(product.productAttributes);
    }
    if (product.productInventory) {
      this.detailsFlag = true;
      for (var i = 0; i < product.productInventory.length; i++) {
        this.addSkuFormGroup(product.productInventory[i])
      }
    }
    this.removeBulkSkuFromGrop(0);
    if (product.bulkInventory[0].bulkId) {
      this.detailsFlag = true;
      for (var i = 0; i < product.bulkInventory.length; i++) {
        this.addBulkSkuFormGroup(product.bulkInventory[i])
      }
    }
  }
  save() {
    let pa: IProductAttributeValue[] = [];
    if ((<FormArray>this.appFormValue.get('productAttributes')).controls.length > 0) {
      for (var control = 0; control < (<FormArray>this.appFormValue.get('productAttributes')).controls.length; control++) {
        let obj: IProductAttributeValue = {
          attributeId: this.productAttributeList.length > 0 ? this.productAttributeList[control].id : this.product.productAttributes[control].attributeId,
          attributeValue: (<FormArray>this.appFormValue.get('productAttributes')).controls[control].value
        }
        pa.push(obj);
      }
    }
    this.bulkOrderList = this.bulkOrderList.reduce((p, c) => (c.minQuantity !=null && p.push(c), p), []);

    let formData = this.appFormValue.value;
    formData.productAttributes = pa;
    formData.bulkSku = this.bulkOrderList;
    formData.slug = this.service.convertToSlug(formData.title);
    formData.weight= Number(formData.weight);
    let deleteFileList = new Array();
    this.productImageList.map(productImage => {
      deleteFileList.push({ Key: productImage.filePath + productImage.fileName })
    })
    let obj ={
      id:this.service.decodedToken().Id
    }
    console.log(obj);
    this.service.PostData(this.service.apiUrls.Supplier.Profile.CheckUserExist, JSON.stringify(obj), true).then(res => {
      let response = res;
      if (response.resultData.supplierId) {
        this.isUserExist = true;
        console.log("Logged User Exist and user can add the product ");}
        this.service.post(this.service.apiUrls.Supplier.Product.UpdateSupplierProduct, formData).subscribe(data => {
          let res = <any>data;
          if (res.status == HttpStatusCode.Ok) {
            if (this.inputFileChanged) {
              //this.productImageList.forEach(x => this.fileIdList.push(x.fileId));
              let obj = { files: this.selectedFilesName, id: this.productId, action: "edit" }
              this.service.post(this.service.apiUrls.Supplier.Image.AddSupplierProductImages, obj).subscribe(result => {
                let folderName = `${this.service.decodedToken().Id}/`
                let response = <any>result;
                if (response.status == HttpStatusCode.Ok) {
                  this.selectedFileList.forEach((file, i) => {
                    this._fileService.uploadFile(file, this.selectedFilesName[i].fileName, folderName).then(res => {
                      if ((this.selectedFileList.length) - 1 == i) {
                        this._fileService.deleteMultipleFile(deleteFileList).then(deletedFile => {
                          this.service.NavigateToRoute(this.service.apiRoutes.Product.ProductList);
                          this._toastr.success(response.message, "Success");
                        });
                      }
                    });
                  })
                }
              })
            }
            else if (!this.inputFileChanged && this.markAsMainObj.fileName) {
              this.service.post(this.service.apiUrls.Supplier.Image.MarkProductImageAsMain, this.markAsMainObj).subscribe(res => {
                let response = <any>res;
                if (response.status) {
                  this.service.NavigateToRoute(this.service.apiRoutes.Product.ProductList);
                }
                //this._toastr.success(res.message, "Success");
              })
            }
            else {
              this.service.NavigateToRoute(this.service.apiRoutes.Product.ProductList);
              this._toastr.success(res.message, "Success");
            }
          }
          this.Loader.hide();
        })

    }, error => {
      console.log(error);
    });

  }

  public confirmBulkOrder() {
    if ((<FormArray>this.bulkOrderForm.get('bulkSku')).controls.length > 0) {
      const skuControlsArray = (<FormArray>this.bulkOrderForm.get('bulkSku')).controls;
      let isExist = this.bulkOrderList.filter(s => s.bulkVarientId == (<FormGroup>skuControlsArray[0]).controls['bulkVarientId'].value);

      if ((<FormGroup>skuControlsArray[0]).controls['minQuantity'].value < 5) {
        this.invalidmMinInput = true;
        this.invalidMaxInput = false;
        this.invalidCompareInput = false;
        return;
      }

      if (isExist.length > 0) {
        this.bulkOrderList = this.bulkOrderList.reduce((p, c) => (c.bulkVarientId !== (<FormGroup>skuControlsArray[0]).controls['bulkVarientId'].value && p.push(c), p), []);
      }
      for (var control = 0; control < (<FormArray>this.bulkOrderForm.get('bulkSku')).controls.length; control++) {
        if (Number((<FormGroup>skuControlsArray[control]).controls['minQuantity'].value) >= Number((<FormGroup>skuControlsArray[control]).controls['maxQuantity'].value)) {
          this.invalidMaxInput = true;
          this.invalidCompareInput = false;
          this.invalidmMinInput = false;
          this.currentindex = control;
          return;
        }

        if (control > 0) {
          if (Number((<FormGroup>skuControlsArray[control]).controls['minQuantity'].value) <= Number((<FormGroup>skuControlsArray[control - 1]).controls['maxQuantity'].value)) {
            this.invalidCompareInput = true;
            this.invalidmMinInput = false;
            this.invalidMaxInput = false;
            this.currentindex = control;
            return;
          }
        }

        this.invalidMaxInput = false;
        this.invalidmMinInput = false;
        this.invalidCompareInput = false;
        let obj: IBulkOrdering = {
          minQuantity: (<FormGroup>skuControlsArray[control]).controls['minQuantity'].value,
          maxQuantity: (<FormGroup>skuControlsArray[control]).controls['maxQuantity'].value,
          bulkVarientId: (<FormGroup>skuControlsArray[0]).controls['bulkVarientId'].value,
          bulkPrice: (<FormGroup>skuControlsArray[control]).controls['bulkPrice'].value,
          index: 0,
        }
        this.bulkOrderList.push(obj);
      }
    }
    else {
      debugger
      const skuControlsArray = (<FormArray>this.bulkOrderForm.get('bulkSku')).controls;
      this.bulkOrderList = this.bulkOrderList.reduce((p, c) => (c.bulkVarientId !== this.currentVarientId && p.push(c), p), []);
    }
    this._modalService.dismissAll();
  }

  changeCategorySelection() {
    //changing
    this.isConfirmedSelection = true;
    this.isSubCategory = false;
    this.isSubCategoryGroup = false;
    this.disableFlag = true;
    this.selectedIndex = 0;
    this.selectedSubIndex = 0;
    this.selectedSubGroupIndex = 0;
    //(<FormArray>this.appFormValue.get('productAttributes')).clear();
    //(<FormArray>this.appFormValue.get('productSku')).clear();
    //this.addSkuFormGroup();
  }
  cancelCategorySelection() {
    this.isConfirmedSelection = false;
  }
  confirmCategorySelection() {
    this.Loader.show();
    this.isConfirmedSelection = false;
    if (this.productAttribute) {
      (<FormArray>this.appFormValue.get('productAttributes')).clear();

    }
    if (this.selectedCategory.category) {
      this.appFormValue.patchValue({
        categoryId: this.selectedCategory.categoryId,
        subCategoryId: this.selectedCategory.subCategoryId,
        subCategoryGroupId: this.selectedCategory.subCategoryGroupId,
        selectedCategory: this.selectedCategory.category + this.selectedCategory.subCategory + this.selectedCategory.categoryGroup,
      })
    }

    let formData = this.appFormValue.value;
    let categoryAttribute, categoryLevel;
    if (formData.subCategoryId == null || formData.subCategoryId == 0) {
      categoryAttribute = formData.categoryId
      categoryLevel = "levelOne"
    }
    else if (formData.subCategoryGroupId == null || formData.subCategoryGroupId == 0) {
      categoryAttribute = formData.subCategoryId
      categoryLevel = "levelTwo"
    }
    else {
      categoryAttribute = formData.subCategoryGroupId
      categoryLevel = "levelThree"
    }
    this.service.get(this.service.apiUrls.Supplier.Product.GetProductAttributeListByCategoryId + `?categoryId=${categoryAttribute}&categoryLevel=${categoryLevel}`).subscribe(response => {
      this.productAttributeList = (<any>response).resultData;
      if (this.productAttributeList) {
        this.isCategoryAttributes = true;
        this.createDynamicFormControl(this.productAttributeList);
      }
      else {
        this.isCategoryAttributes = false;
      }
      this.Loader.hide();
    });
    this.detailsFlag = true;
  }

  // Category Lists
  getCategoryList() {

    this.Loader.show();
    this.service.get(this.service.apiUrls.Supplier.Product.GetActiveProducts).subscribe(res => {
      this.categoryList = <any>res.resultData;
      this.getSubProductList(this.product.categoryId,this.product.categoryName);
      this.Loader.hide();
    });
  }
  getSubProductList(catId: any, catName: any) {
    debugger;
    this.Loader.show();
    var i = this.categoryList.findIndex(x => x.id == catId);
    this.setIndex(i);
    this.selectedCategory.categoryId = catId;
    this.selectedCategory.category = catName;
    this.service.get(this.service.apiUrls.Supplier.Product.GetProductSubCategoryById + `?productCatgoryId=${this.selectedCategory.categoryId}`).subscribe(res => {
      this.subCategoryList = <any>res;
      this.isSubCategoryGroup = false;
      if (this.subCategoryList.length > 0) {
        var j = this.subCategoryList.findIndex(x => x.productSubCategoryId == this.product.subCategoryId);
        this.setSubIndex(j);
        this.getProductCategoryGroupList(this.product.subCategoryId,this.product.subCategoryName);
        this.isSubCategory = true;
        this.disableFlag = true;
      }
      else {
        this.isSubCategory = false;
        this.disableFlag = false;
      }
      this.Loader.hide();
    });
  }
  getProductCategoryGroupList(subCatId: any, subCatName: any) {
    this.Loader.show();
    var i = this.subCategoryList.findIndex(x => x.productSubCategoryId == subCatId);
    this.setSubGroupIndex(i);

    this.selectedCategory.subCategoryId = subCatId;
    this.selectedCategory.subCategory = ' / ' + subCatName

    this.service.get(this.service.apiUrls.Supplier.Product.GetProductCategoryGroupListById + `?subCategoryId=${this.selectedCategory.subCategoryId}`).subscribe(res => {
      this.subCategoryGroupList = (<any>res).resultData;
      if (this.subCategoryGroupList.length > 0) {
        var j = this.subCategoryGroupList.findIndex(x => x.id == this.product.categoryGroupId);
        this.setSubGroupIndex(j);
        this.isSubCategoryGroup = true;
        this.disableFlag = true;
      }
      else {
        this.isSubCategoryGroup = false;
        this.disableFlag = false;
      }
      this.Loader.hide();
    });
  }
  getSubCategoryGroup(subCategoryGroupId: Event) {
    this.disableFlag = false;
    this.selectedCategory.subCategoryGroupId = (<HTMLInputElement>subCategoryGroupId.target).value;
    this.selectedCategory.categoryGroup = ' / ' + (<HTMLInputElement>subCategoryGroupId.target).textContent;
  }
  //Form Creation
  removeSkuFromGrop(index: number): void {

    //changing
    (<FormArray>this.appFormValue.get('productSku')).removeAt(index);
  }
  addSkuFormGroup(productInventory: null): void {

    //changing
    (<FormArray>this.appFormValue.get('productSku')).push(this.initSkuFormGroup(productInventory));
  }
  isExsitingVariant(event: Event, index: number) {
    let selectedVariant = (event.target as HTMLInputElement).value;
    const existingVariant = [];
    const skuControlsArray = (<FormArray>this.appFormValue.get('productSku')).controls;
    for (let i = 0; i < skuControlsArray.length; i++) {
      existingVariant.push((<FormGroup>skuControlsArray[i]).controls['productVariantId'].value);
    }
    const matchedVariant = existingVariant.filter(variant => variant == selectedVariant);
    if (matchedVariant.length > 1) {
      (<FormGroup>(<FormArray>this.appFormValue.get('productSku')).at(index)).controls['productVariantId'].setValue(null);
      this._toastr.error("Variant already exists in current SKU!", "Error");
    }
  }
  get productSkuControls() {
    return this.appFormValue ? this.appFormValue.get('productSku') as FormArray : null
  }
  initSkuFormGroup(productInventory: any): FormGroup {
    //changing
    if (productInventory) {
      return this.fb.group({
        quantity: [productInventory.quantity, [Validators.required]],
        price: [productInventory.price, [Validators.required]],
        discountInPercentage: [productInventory.discountInPercentage],
        productVariantId: [productInventory.variantId, [Validators.required]],
        availability: [productInventory.availability]
      })
    }
    else {
      return this.fb.group({
        quantity: ['', [Validators.required]],
        price: ['', [Validators.required]],
        discountInPercentage: [''],
        productVariantId: [null, [Validators.required]],
        availability: [false]
      })
    }
  }



  //Form Creation
  removeBulkSkuFromGrop(index: number): void {
    //changing
    (<FormArray>this.bulkOrderForm.get('bulkSku')).removeAt(index);
  }
  addBulkSkuFormGroup(bulkInventory): void {

    //changing
    (<FormArray>this.bulkOrderForm.get('bulkSku')).push(this.initBulkSkuFormGroup(bulkInventory));
  }
  get bulkSkuControls() {
    return this.bulkOrderForm ? this.bulkOrderForm.get('bulkSku') as FormArray : null
  }

  initBulkSkuFormGroup(bulkInventory: any): FormGroup {
    //changing
    if (bulkInventory) {
      return this.fb.group({
        bulkId: [bulkInventory.bulkId],
        minQuantity: [bulkInventory.minQuantity, [Validators.required]],
        maxQuantity: [bulkInventory.maxQuantity, [Validators.required]],
        bulkPrice: [bulkInventory.bulkPrice, [Validators.required]],
        bulkdiscount: [bulkInventory.bulkdiscount],
        bulkVarientId: [bulkInventory.bulkVarientId]
      })
    }
    else {
      return this.fb.group({
        bulkId: [''],
        minQuantity: ['', [Validators.required]],
        maxQuantity: ['', [Validators.required]],
        bulkPrice: ['', [Validators.required]],
        bulkdiscount: [''],
        bulkVarientId: [this.currentVarientId]
      })
    }
  }

  addBullkOrder(bulkOrderTemplate: TemplateRef<any>, index: number, varientId: number) {
    this.Loader.show();
    this.currentVarientId = varientId;
    let bulkVarient = this.bulkOrderList.filter(s => s.bulkVarientId == varientId);
    this.hexCode = this.variantList.filter(s => s.id == varientId).map(s => s.hexCode).toString();
    this.varientName = this.variantList.filter(s => s.id == varientId).map(s => s.colorName).toString();
    if (bulkVarient.length > 0) {
      this.bulkOrderForm = this.fb.group({
        bulkSku: this.fb.array([]),
      });
      bulkVarient.forEach((item) => {
        this.addBulkSkuFormGroup(item)
      })
    }
    else {
      this.bulkOrderForm = this.fb.group({
        bulkSku: this.fb.array([]),
      });
      this.addBulkSkuFormGroup(null)
    }
    this._modalService.open(bulkOrderTemplate);
    this.Loader.hide();
  }

  createDynamicFormControl(attributesList: any) {
    //changing
    for (var i = 0; i < attributesList.length; i++) {
      (<FormArray>this.appFormValue.get('productAttributes')).push(new FormControl(attributesList[i].attributeValue, Validators.required));
    }
  }
  get productAttributesControl() {
    return this.appFormValue ? this.appFormValue.get('productAttributes') as FormArray : null
  }
  setIndex(index: number) {
    debugger;
    this.selectedIndex = index;
  }
  setSubIndex(index: number) {
    this.selectedSubIndex = index;
  }
  setSubGroupIndex(index: number) {
    this.selectedSubGroupIndex = index;
  }
  numberOnly(event: KeyboardEvent): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }
}
