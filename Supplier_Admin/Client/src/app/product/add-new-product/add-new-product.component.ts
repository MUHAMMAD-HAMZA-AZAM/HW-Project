import { HttpClient, HttpStatusCode } from '@angular/common/http';
import { Component, ElementRef, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { IBulkOrdering, IProduct, IProductAttributeValue, IProductCategoryGroupList, IProductCategoryList, IProductSubCategory, ISelectedCategoryList, IVarientDetails } from '../../Shared/Enums/Interface';
import { UploadFileService } from '../../Shared/HttpClient/upload-file.service';
import { CommonService } from '../../Shared/HttpClient/_http';
import { DOC_ORIENTATION, NgxImageCompressService } from 'ngx-image-compress';
import { of, Observable, fromEvent, from } from 'rxjs';
import { debounceTime, distinctUntilChanged, map, pluck, switchMap } from 'rxjs/operators';
import { StatusCode } from '../../Shared/Enums/common';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ValidationMessages } from '../../Shared/ApiRoutes/validation-message';
import { bool } from 'aws-sdk/clients/signer';

@Component({
  selector: 'app-add-new-product',
  templateUrl: './add-new-product.component.html',
  styleUrls: ['./add-new-product.component.css']
})
export class AddNewProductComponent implements OnInit {
  orientation = typeof DOC_ORIENTATION;
  FOLDERPATH = 'photos/product/'
  appFormValue: FormGroup;
  bulkOrderForm: FormGroup;
  detailsFlag: boolean = false;
  selectCategory: boolean = false;
  isChanged: boolean = false;
  showCategoryList: boolean = false
  isSubCategory: boolean = false;
  isSubCategoryGroup: boolean = false;
  public btnSave: boolean = false;
  isCategoryAttributes: boolean = false;
  disableFlag: boolean = true;
  isConfirmedSelection: boolean = false;
  selectedIndex: number = 0;
  selectedSubIndex: number = 0;
  selectedSubGroupIndex: number = 0;
  isMain: number = 0;
  invalidmMinInput: boolean = false;
  invalidMaxInput: boolean = false;
  invalidCompareInput: boolean = false;
  currentindex: number = 0;
  hexCode: string = "";
  varientName: string = "";
  public isUserExist:boolean = false;
  attributeLabels: any = [];
  selectedVairentByIndex: any = [];
  public supplierId: number = 0;
  public minProductImgHeight: number = 600;
  public minProductImgWidth: number = 800;
  productAttributeList: IProduct[] = [];
  bulkOrderList: IBulkOrdering[] = [];
  categoryList: IProductCategoryList[] = [];
  subCategoryList: IProductSubCategory[] = [];
  subCategoryGroupList: IProductCategoryGroupList[] = [];
  /*  subProductsList: any = [];*/
  variantList: IVarientDetails[] = [];
  selectedImageList = new Array();
  selectedFileList = new Array();
  selectedFilesName = new Array();
  loggedUserDetails: any;
  selectedCategory: ISelectedCategoryList = { category: null, categoryId: null, subCategory: null, subCategoryId: null, categoryGroup: '', subCategoryGroupId: null };
  @ViewChild("productAttribute") productAttribute: ElementRef
  @ViewChild("uploadedFile") uploadedFile: ElementRef
  @ViewChild("object0") targetDiv: ElementRef;
  @ViewChild('blockSupplierMessageModal',{static:true}) blockSupplierMessageModal :ElementRef;
  constructor(private imageCompress: NgxImageCompressService, public _modalService: NgbModal, public _toastr: ToastrService, public service: CommonService, public _fileService: UploadFileService, public fb: FormBuilder, public Loader: NgxSpinnerService) {
    this.uploadedFile = {} as ElementRef;
    this.productAttribute = {} as ElementRef;
    this.appFormValue = {} as FormGroup;
    this.bulkOrderForm = {} as FormGroup;
  }

  ngOnInit(): void {
    var decodedtoken = this.service.decodedToken();
    this.supplierId = decodedtoken.Id;
    this.createProductForm();
    this.bulkOrderList = [];
    this.checkLoggedUserExist(this.supplierId);
    //this.createBulkOrderForm();
    //this.removeBulkSkuFromGrop(0);   
    //this.targetDiv.nativeElement.style.display = "none";
    //this.getLoggedUserDetails();
    //this.createDynamicFromControl();
  }

  public checkLoggedUserExist(suppId: number) {
    let obj = {
      id: suppId,
    };
    this.service.PostData(this.service.apiUrls.Supplier.Profile.CheckUserExist, JSON.stringify(obj),true).then(res => {
      let response = res;
      if (response.status == StatusCode.OK) {
        console.log(response.resultData.supplierId);
        if (response.resultData.supplierId) {
          this.isUserExist = true;
          console.log("Logged User Exist and user can add the product ");
        }
        else {
          this.service.logOut();
        }
      }
      this.Loader.hide();
    }, error => {
      console.log(error);
    });
  }


  public getLoggedUserDetails() {
    this.service.GetData(this.service.apiUrls.Supplier.Profile.GetUserDetailsByUserRole + `?userId=${this.service.decodedToken().UserId}&userRole=${this.service.decodedToken().Role}`, false).then(res => {
      if (res.status == StatusCode.OK) {
        this.loggedUserDetails = res.resultData;
      }
    });
  }
  //ngAfterViewInit() {
  //  fromEvent(this.searchTagInput.nativeElement, 'keyup').subscribe(res => {
  //    console.log(res);
  //  })
  //}

  createProductForm() {
    this.appFormValue = this.fb.group({
      title: ['', [Validators.required]],
      searchTag: [null, [Validators.required]],
      selectedCategory: [''],
      description: ['', [Validators.required]],
      youtubeURL: ['', Validators.pattern('^(https?:\/\/)?((w{3}\.|m\.)?)youtube.com\/.*')],
      categoryId: [null, [Validators.required]],
      subCategoryId: [null],
      weight: [null, [Validators.required, Validators.pattern('^[0-9]+(.[0-9]{0,4})?$')
      ]],
      image: [null, Validators.required],
      isMain: [false],
      isActive: [true],
      subCategoryGroupId: [null],
      productAttributes: this.fb.array([]),
      productSku: this.fb.array([this.initSkuFormGroup()])
    })
    this.getCategoryList();
  }

  get f() {
    return this.appFormValue.controls;
  }

  getProductSearchTags = (text: string): Observable<any> => {
    return this.service.get(this.service.apiUrls.Supplier.Product.GetProductSearchTagsList + `?inputText=${text}`).pipe(map((data: any) => (data.resultData)))
  }
  handleInputFileChange(uploadedFiles: any) {
    let fileList = uploadedFiles.target.files;
    const reader = new FileReader();
    //check image format
    for (var i = 0; i < fileList.length; i++) {
      let fileType = (fileList[i].type).toLowerCase();
      if (fileType != "image/jpeg" && fileType != "image/jpg" && fileType != "image/png") {
        this._toastr.warning("You can't be able to upload file except JPEG,JPG and PNG format", "Image Format")
        this.uploadedFile.nativeElement.value = null;
        return;
      }     
      if ((this.selectedFileList.length + fileList.length) > 5) {
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
              that.selectedFilesName = [];
              that.selectedImageList = [];
              for (let j = 0; j < fileList.length; j++) {
                that.selectedFileList.push(fileList[j]);
              }
              let isMain;
              that.selectedFileList.forEach((file, index) => {
                console.log('Old file', file.size);
                let fileExtension = file.name.split('.').pop();
                index == 0 ? isMain = true : isMain = false;
                let obj = { fileName: `${(Math.random() * 10000).toString()}.${fileExtension}`, filePath: that.FOLDERPATH + `${that.service.decodedToken().Id}/`, isMain };
                that.selectedFilesName.push(obj);
                that.imageCompress.getOrientation(file).then(ortn => {
                  that._fileService.getBase64(file).then((image: string) => {
                    that.selectedImageList.push(image);
                    that.imageCompress.compressFile(image, ortn, 100, 20).then(result => {
                      that._fileService.dataUrlToFile(result, (Math.random() * 10000).toString()).then(generatedFile => {
                        that.selectedFileList[index] = generatedFile;
                      })
                    });
                  })
                })
              })
              return true;
            }
          }
        }
      }
    }
   
  
  }
  removeSelectedImage(item: number) {
    if (this.selectedImageList.length == 1) {
      this.appFormValue.controls['image'].setValue(null)
      this.appFormValue.controls['image'].setValidators(Validators.required);
      this.appFormValue.controls['image'].updateValueAndValidity();
    }
    let removedIndex = this.selectedImageList.indexOf(item);
    this.selectedImageList.splice(removedIndex, 1);
    this.selectedFilesName.splice(removedIndex, 1);
    this.selectedFileList.splice(removedIndex, 1);
    if (this.selectedFilesName.length > 0) {
      this.selectedFilesName[0].isMain = true;
    }
  }
  markAsMainImage(index: number) {
    this.isMain = index;
    this.selectedFilesName.forEach(x => x.isMain = false)
    this.selectedFilesName[index].isMain = true;
  }
  save() {
    this.btnSave = true;
    this.Loader.show();
    let pa: IProductAttributeValue[] = [];
    if ((<FormArray>this.appFormValue.get('productAttributes')).controls.length > 0) {
      for (var control = 0; control < (<FormArray>this.appFormValue.get('productAttributes')).controls.length; control++) {
        let obj: IProductAttributeValue = {
          attributeId: this.productAttributeList[control].id,
          attributeValue: (<FormArray>this.appFormValue.get('productAttributes')).controls[control].value
        }
        pa.push(obj);
      }
    }

    let formData = this.appFormValue.value;

    let bulkOrderArray: [] = this.bulkOrderForm.value;

    formData.productAttributes = pa;
    formData.bulkSku = this.bulkOrderList;
    formData.supplierId = this.service.decodedToken().Id;
    formData.slug = this.service.convertToSlug(formData.title);
    formData.weight = Number(formData.weight);
    let obj ={
      id:formData.supplierId
    }

    //formData.searchTag = formData.searchTag.map(tag => tag.display);
      this.service.PostData(this.service.apiUrls.Supplier.Profile.CheckUserExist, JSON.stringify(obj),true).then(res => {
        let response = res;
        if (response.status == StatusCode.OK) {
          console.log(response.resultData.supplierId);
          if (response.resultData.supplierId) {
            this.isUserExist = true;
            console.log("Logged User Exist and user can add the product ");
            this.service.post(this.service.apiUrls.Supplier.Product.AddNewSupplierProduct, formData).subscribe(response => {
              let folderName = `${formData.supplierId}/`
              let res = <any>response;
              if (res.status == HttpStatusCode.Ok) {
                let obj = { files: this.selectedFilesName, id: res.resultData }
                this.service.post(this.service.apiUrls.Supplier.Image.AddSupplierProductImages, obj).subscribe(result => {
                  let response = <any>result;
                  if (response.status == HttpStatusCode.Ok) {
                    this.selectedFileList.forEach((file, i) => {
                      this._fileService.uploadFile(file, this.selectedFilesName[i].fileName, folderName);
                    })
                    this.service.NavigateToRoute(this.service.apiRoutes.Product.ProductList);
                    this._toastr.success(response.message, "Success");
                    this.btnSave = false;
                  }
                });
              }
            },error =>{
              console.log(error);
            })
          }
          else {
            this.service.logOut();
          }
        }
        this.Loader.hide();
      }, error => {
        console.log(error);
      });
   
    this.Loader.hide();
  }
  changeCategorySelection() {
    this.isConfirmedSelection = false;
    this.detailsFlag = false;
    this.selectedCategory.category = '';
    this.selectedCategory.subCategory = '';
    this.selectedCategory.categoryGroup = '';
    this.isSubCategory = false;
    this.isSubCategoryGroup = false;
    this.disableFlag = true;
    this.selectedIndex = 0;
    this.selectedSubIndex = 0;
    this.selectedSubGroupIndex = 0;
    (<FormArray>this.appFormValue.get('productAttributes')).clear();
    (<FormArray>this.appFormValue.get('productSku')).clear();
    (<FormArray>this.appFormValue.get('bulkSku')).clear();
    this.addSkuFormGroup();
    this.addBulkSkuFormGroup();
  }

  addBullkOrder(bulkOrderTemplate: TemplateRef<any>, index: number, varientId: number) {
    this.invalidmMinInput = false;
    this.invalidMaxInput = false;
    this.invalidCompareInput = false;
    this._modalService.open(bulkOrderTemplate);
    let itemExist = this.bulkOrderList.filter(s => s.varientId == varientId);

    this.hexCode = this.variantList.filter(s => s.id == varientId).map(s => s.hexCode).toString();
    this.varientName = this.variantList.filter(s => s.id == varientId).map(s => s.colorName).toString();

    if (itemExist.length > 0) {
      this.bulkOrderForm = this.fb.group({
        bulkSku: this.fb.array([]),
      });
      itemExist.forEach((item) => {
        this.initBulkSkuFormGroup(item)
      })
    }
    else {
      this.bulkOrderForm = this.fb.group({
        bulkSku: this.fb.array([this.fb.group({
          minQuantity: [null, [Validators.required]],
          maxQuantity: [null, [Validators.required]],
          bulkPrice: [null, [Validators.required]],
          bulkDiscount: [null],
          varientId: [varientId],
          index: [index]
        })])
      });
    }
  }

  get formArr() {
    return this.bulkOrderForm.get('bulkSku') as FormArray;
  }

  deleteRow(index: number) {
    this.formArr.removeAt(index);
  }

  initBulkSkuFormGroups(): FormGroup {
    const skuControlsArray = (<FormArray>this.bulkOrderForm.get('bulkSku')).controls;
    return this.fb.group({
      minQuantity: [null, [Validators.required]],
      maxQuantity: [null, [Validators.required]],
      bulkPrice: [null, [Validators.required]],
      bulkDiscount: [null],
      varientId: [(<FormGroup>skuControlsArray[0]).controls['varientId'].value],
      index: [(<FormGroup>skuControlsArray[0]).controls['index'].value]
    })
  }

  initBulkSkuFormGroup(bulkInventory: any = null): FormGroup {
    if (bulkInventory) {
      (<FormArray>this.bulkOrderForm.get('bulkSku')).push(
        this.fb.group({
          minQuantity: [bulkInventory.minQuantity, [Validators.required]],
          maxQuantity: [bulkInventory.maxQuantity, [Validators.required]],
          bulkPrice: [bulkInventory.bulkPrice, [Validators.required]],
          bulkDiscount: [null],
          varientId: [bulkInventory.varientId],
          index: [bulkInventory.index]
        })
      );
    }
    else {
      return
      this.bulkOrderForm = this.fb.group({
        bulkSku: this.fb.array([this.fb.group({
          minQuantity: [null, [Validators.required]],
          maxQuantity: [null, [Validators.required]],
          bulkPrice: [null, [Validators.required]],
          bulkDiscount: [null],
          varientId: [0],
          index: [0]
        })])
      });
    }
  }

  handleFocus() {
    this.showCategoryList = true;
  }
  confirmCategorySelection() {

    this.Loader.show();
    this.isConfirmedSelection = true;
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
    this.getAllProductVariant();
    this.detailsFlag = true;
    this.selectCategory = true;
  }

  get g() {
    return this.bulkOrderForm.controls;
  }
  public confirmBulkOrder() {
    if ((<FormArray>this.bulkOrderForm.get('bulkSku')).controls.length > 0) {
      const skuControlsArray = (<FormArray>this.bulkOrderForm.get('bulkSku')).controls;
      let isExist = this.bulkOrderList.filter(s => s.index == (<FormGroup>skuControlsArray[0]).controls['index'].value);

      if ((<FormGroup>skuControlsArray[0]).controls['minQuantity'].value < 5) {
        this.invalidmMinInput = true;
        this.invalidMaxInput = false;
        this.invalidCompareInput = false;
        return;
      }

      if (isExist.length > 0) {
        this.bulkOrderList = this.bulkOrderList.reduce((p, c) => (c.index !== (<FormGroup>skuControlsArray[0]).controls['index'].value && p.push(c), p), []);
      }
      for (var control = 0; control < (<FormArray>this.bulkOrderForm.get('bulkSku')).controls.length; control++) {
        debugger
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
          varientId: (<FormGroup>skuControlsArray[0]).controls['varientId'].value,
          bulkPrice: (<FormGroup>skuControlsArray[control]).controls['bulkPrice'].value,
          index: (<FormGroup>skuControlsArray[0]).controls['index'].value,
        }
        this.bulkOrderList.push(obj);
      }
    }
    this._modalService.dismissAll();
  }

  // Category Lists
  getCategoryList() {
    this.Loader.show();
    this.service.get(this.service.apiUrls.Supplier.Product.GetActiveProducts).subscribe(res => {
      this.categoryList = <any>res.resultData;
      this.Loader.hide();
    });
  }
  getSubCategoryList(productId: Event) {
    this.Loader.show();
    //this.selectedCategory.categoryId = productId.target.value;
    this.selectedCategory.categoryId = (<HTMLInputElement>productId.target).value;
    this.selectedCategory.category = (<HTMLInputElement>productId.target).textContent
    this.service.GetData(this.service.apiUrls.Supplier.Product.GetProductSubCategoryById + `?productCatgoryId=${this.selectedCategory.categoryId}`).then(res => {
      this.subCategoryList = <any>res;
      this.isSubCategoryGroup = false;
      if (this.subCategoryList.length > 0) {
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
  getProductCategoryGroupList(subCategoryId: any) {
    this.Loader.show();
    this.selectedCategory.subCategoryId = subCategoryId.target.value;
    this.selectedCategory.subCategory = ' / ' + subCategoryId.target.textContent;

    this.service.get(this.service.apiUrls.Supplier.Product.GetProductCategoryGroupListById + `?subCategoryId=${subCategoryId.target.value}`).subscribe(res => {
      this.subCategoryGroupList = (<any>res).resultData;
      if (this.subCategoryGroupList.length > 0) {
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
  getSubCategoryGroup(subCategoryGroupId: any) {
    this.disableFlag = false;
    this.selectedCategory.subCategoryGroupId = subCategoryGroupId.target.value;
    this.selectedCategory.categoryGroup = ' / ' + subCategoryGroupId.target.textContent;
  }
  getAllProductVariant() {
    this.service.get(this.service.apiUrls.Supplier.Product.GetAllProductVariant).subscribe(result => {
      this.variantList = result.filter(x => x.isActive);
    })
  }
  isExsitingVariant(event: Event, index: number) {
    let selectedVariant = (event.target as HTMLInputElement).value;
  
    let obj = {
      index: index,
      varientId: selectedVariant
    }

    this.selectedVairentByIndex.push(obj);
    this.bulkOrderList.filter(s => s.index == index).map(s => s.varientId = Number(selectedVariant));

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

  //Form Creation
  initSkuFormGroup(): FormGroup {
    return this.fb.group({
      quantity: ['', [Validators.required]],
      price: ['', [Validators.required]],
      DiscountInPercentage: [null],
      productVariantId: [null, [Validators.required]],
      availability: [false]
    })
  }
  get productSkuControls() {
    return this.appFormValue ? this.appFormValue.get('productSku') as FormArray : null
  }

  addSkuFormGroup(): void {
    (<FormArray>this.appFormValue.get('productSku')).push(this.initSkuFormGroup());
  }
  removeSkuFromGrop(index: number): void {
    (<FormArray>this.appFormValue.get('productSku')).removeAt(index);
  }

  addBulkSkuFormGroups(bulkInventory: any = null): void {

    //changing
    (<FormArray>this.bulkOrderForm.get('bulkSku')).push(this.initBulkSkuFormGroup(bulkInventory));
  }
  //bulk Form creation

  addBulkSkuFormGroup(): void {
    try {
      (<FormArray>this.bulkOrderForm.get("bulkSku")).push(
        this.initBulkSkuFormGroups()
      );
    }
    catch (error) {
      console.log(error);
    }
  }

  get bulkSkuControls() {
    return this.bulkOrderForm ? this.bulkOrderForm.get('bulkSku') as FormArray : null
  }


  removeBulkSkuFromGrop(index: number): void {
    (<FormArray>this.bulkOrderForm.get('bulkSku')).removeAt(index);
  }
  createDynamicFormControl(attributesList: any) {
    for (var i = 0; i < attributesList.length; i++) {
      (<FormArray>this.appFormValue.get('productAttributes')).push(new FormControl('', Validators.required));
    }
  }
  get productAttributesControl() {
    return this.appFormValue ? this.appFormValue.get('productAttributes') as FormArray : null
  }
  setIndex(index: number) {
    this.selectedCategory.subCategoryId = '0';
    this.selectedCategory.subCategoryGroupId = '0';
    this.selectedIndex = index;
    this.selectedSubIndex = -1;
  }
  setSubIndex(index: number) {
    this.selectedCategory.subCategoryGroupId = '0';
    this.selectedSubIndex = index;
    this.selectedSubGroupIndex = -1;
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
