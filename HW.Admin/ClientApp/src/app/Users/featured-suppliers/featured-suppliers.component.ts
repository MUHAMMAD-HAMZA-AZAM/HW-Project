import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '../../Shared/HttpClient/_http';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";
import { JwtHelperService } from '@auth0/angular-jwt/src/jwthelper.service';
import { ImageCroppedEvent } from 'ngx-image-cropper';
import { ClassGetter } from '@angular/compiler/src/output/output_ast';
import { image } from 'html2canvas/dist/types/css/types/image';
import { fail } from 'assert';
import { Router } from '@angular/router';


@Component({
  selector: 'app-featured-suppliers',
  templateUrl: './featured-suppliers.component.html',
  styleUrls: ['./featured-suppliers.component.css']
})
export class FeaturedSuppliersComponent implements OnInit {
  public supplierForm: FormGroup;
  public supplierList = [];
  public featuredSupplierList = [];
  public jwtHelperService: any;
  public updateImages =  false;
  public image1: any;
  public image2: any;
  public image3: any;
  //public imageId1 = 0;
  //public imageId2 = 0;
  //public imageId3 = 0;

  //City DropDown //
  public cityList = [];
  public selectedCities = [];
  public selectedCity = [];
  public selectedColumn = [];
  public dropdownListForCity = {};
  public dropdownListForColumn = {};
  public base64textString: string;

  ////// Image //////
  public makeDisable: boolean;
  imageChangedEvent1: any = '';
  imageChangedEvent2: any = '';
  imageChangedEvent3: any = '';
  public changedInput: any;
  croppedImage1: any = '';
  croppedImage2: any = '';
  croppedImage3: any = '';
  public noimage: boolean = false;
  public nosupplierselect: boolean = false;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  @ViewChild("imageOne", { static: true }) imageOne: ElementRef
  @ViewChild("is1", { static: true }) is1: ElementRef
  @ViewChild("is2", { static: true }) is2: ElementRef
  @ViewChild("is3", { static: true }) is3: ElementRef
  constructor( public router: Router, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Featured Supplier"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.dropdownListForCity = {
      singleSelection: true,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };
    this.noimage = false;
    this.nosupplierselect = false;
    this.getAllCities();
    this.GetSupplierList();
    this.GetFeaturedSupplierList()
    this.supplierForm = this.fb.group({
      supplierId : [0],
      firstName: ['',[Validators.required, Validators.minLength(1)]],
      lastName: ['', [Validators.required, Validators.minLength(1)]],
      cnic : [''],
      gender : [''],
      companyName : [''],
      mobileNumber : [''],
      registrationNumber : [''],
      selectedCities : [''],
      businessAddress : [''],
      featuredSupplier: [''],
      imageStatus1: [false],
      imageStatus2: [false],
      imageStatus3: [false],
      imageId1: [0],
      imageId2: [0],
      imageId3: [0],
    })

  }
  radioChange1() {
    let checked1 = this.is1.nativeElement;
    let checked2 = this.is2.nativeElement;
    let checked3 = this.is3.nativeElement;
    
    if (checked1.checked == true) {
      checked2.checked = false;
      checked3.checked = false;
      this.supplierForm.value.imageStatus1 = true;
      this.supplierForm.value.imageStatus2 = false;
      this.supplierForm.value.imageStatus3 = false;
      console.log(`s1 ${this.supplierForm.value.imageS}`)
    }
  }
  radioChange2() {
    let checked1 = this.is1.nativeElement;
    let checked2 = this.is2.nativeElement;
    let checked3 = this.is3.nativeElement;
    
   if (checked2.checked == true) {
      checked1.checked = false;
     checked3.checked = false;
     this.supplierForm.value.imageStatus1 = false;
     this.supplierForm.value.imageStatus2 = true;
     this.supplierForm.value.imageStatus3 = false;
     console.log();
    }
  }
  radioChange3() {
    let checked1 = this.is1.nativeElement;
    let checked2 = this.is2.nativeElement;
    let checked3 = this.is3.nativeElement;
    

   if (checked3.checked == true) {
      checked1.checked = false;
     checked2.checked = false;
     this.supplierForm.value.imageStatus1 = false;
     this.supplierForm.value.imageStatus2 = false;
     this.supplierForm.value.imageStatus3 = true;

    }
  }
  save() {
    
    if (!this.updateImages) {
      let action = "saving"
      var imageStatus1 = this.supplierForm.value.imageStatus1;
      var imageStatus2 = this.supplierForm.value.imageStatus2;
      var imageStatus3 = this.supplierForm.value.imageStatus3;
      if (this.croppedImage1 == '' && this.croppedImage2 == '' && this.croppedImage3 == '') {
        this.noimage = true;
        return;
      }
      else {

        this.noimage = false;
      }
      var featuredSupplier = this.supplierForm.value.featuredSupplier
      var supplierId = this.supplierForm.value.supplierId;
      if (supplierId == '' || supplierId == undefined || supplierId == "" || supplierId == null || supplierId == 0) {
        this.nosupplierselect = true;
        return;
      }
      else {
        this.nosupplierselect = false;
      }
      // let  featuredSupplierImages = [{ base64Image1: this.croppedImage1, img1 }, { base64Image2: this.croppedImage2 , img2 }, { base64Image3: this.croppedImage3 , img3 }];
      let base64ImageArray = [this.croppedImage1, this.croppedImage2, this.croppedImage3];
      let obj = {
        action,
        imageStatus1, imageStatus2, imageStatus3,
        supplierId,
        //base64Image1: this.croppedImage1,
        //base64Image2: this.croppedImage2,
        //base64Image3: this.croppedImage3,
        base64ImageArray,
        featuredSupplier,
        //featuredSupplierImages
      }
      console.log(obj);
      this.service.PostData(this.service.apiRoutes.Supplier.FeaturedSupplier, obj, true).then(result => {
        let res = result.json();
        if (res.status == 200) {
          this.GetFeaturedSupplierList();
          this.GetSupplierList();
          this.toastr.success(res.message, "Success");
          this.noimage = false;
          
        }
        else {
          this.toastr.error("Something Went Wrong!", "Success");
        }
      })
      console.log(obj);
    }
    else {
      let action = "updating"
      var imageStatus1 = this.supplierForm.value.imageStatus1;
      var imageStatus2 = this.supplierForm.value.imageStatus2;
      var imageStatus3 = this.supplierForm.value.imageStatus3;
      var imageId1 = this.supplierForm.value.imageId1;
      var imageId2 = this.supplierForm.value.imageId2;
      var imageId3 = this.supplierForm.value.imageId3;
      if (this.croppedImage1 == '' && this.croppedImage2 == '' && this.croppedImage3 == '' && imageStatus1 == false && imageStatus2 == false && imageStatus1 == false) {
        this.noimage = true;
        return;
      }
      else {
        this.noimage = false;
      }
      var featuredSupplier = this.supplierForm.value.featuredSupplier
      var supplierId = this.supplierForm.value.supplierId;
      if (supplierId == '' || supplierId == undefined || supplierId == "" || supplierId == null || supplierId == 0) {
        this.nosupplierselect = true;
        return;
      }
      else {
        this.nosupplierselect = false;
      }
      let base64ImageArray = [this.croppedImage1, this.croppedImage2, this.croppedImage3];
      let obj = {
        action,
        imageId1,imageId2,imageId3,
        imageStatus1, imageStatus2, imageStatus3,
        supplierId,
        base64ImageArray,
        featuredSupplier,
      }
      this.service.PostData(this.service.apiRoutes.Supplier.FeaturedSupplier, obj, true).then(result => {
        let res = result.json();
        if (res.status == 200) {
          this.GetFeaturedSupplierList();
          this.toastr.success(res.message, "Success");
          this.image1 = null;
          this.image2 = null;
          this.image3 = null;
          //this.imageOne.nativeElement.value = "";
          this.noimage = false;
        }
        else {
          this.toastr.error("Something Went Wrong!", "Success");
        }
      })
    }
  }
  GetSupplierList() {
    this.Loader.show()
    this.service.get(this.service.apiRoutes.Supplier.GetSupplierForReport + `?userType=3&mobileType=1&emailType=1`).subscribe(result => {
      this.supplierList = result.json();
      this.Loader.hide();
    })
  }
  GetFeaturedSupplierList() {
    
    this.Loader.show()
    this.service.get(this.service.apiRoutes.Supplier.GetFeaturedSupplierList).subscribe(result => {
      this.featuredSupplierList = result.json();
      this.Loader.hide();
      })
  }
  ////// Image //////
  handleFileSelect(evt) {
    if (evt.target.id == "image1")
      this.changedInput = "image1";
    else if (evt.target.id == "image2")
      this.changedInput = "image2"
    else
      this.changedInput = "image3"
    var files = evt.target.files;
    var file = files[0];

    if (files && file) {
      var reader = new FileReader();

      reader.onload = this._handleReaderLoaded.bind(this);

      reader.readAsBinaryString(file);
    }
  }
  _handleReaderLoaded(readerEvt) {
    
    console.log(readerEvt);
    var binaryString = readerEvt.target.result;
    if (this.changedInput == "image1") {
      this.croppedImage1 = btoa(binaryString);
    }
    else if (this.changedInput == "image2") {
      this.croppedImage2 = btoa(binaryString);
    } else 
      this.croppedImage3 = btoa(binaryString);

    console.log(this.croppedImage1);
    console.log(this.croppedImage2);
    console.log(this.croppedImage2);
  }

  fileChangeEvent(event: any): void {
    console.log(event);
    
    
    if (event.target.id == "image1")
      this.imageChangedEvent1 = event;
    else if (event.target.id == "image2")
      this.imageChangedEvent2 = event
    else
      this.imageChangedEvent3 = event

  }
  imageCropped1(event: ImageCroppedEvent) {
    this.image1 = null;
    this.croppedImage1 = event.base64;
    //this.supplierForm.setValue({
    //  imageId1: this.imageId1
    //});
  }
  imageCropped2(event: ImageCroppedEvent) {
    this.image2 = null;
    this.croppedImage2 = event.base64;
    //this.supplierForm.setValue({
    //  imageId2: this.imageId2
    //});
  }
  imageCropped3(event: ImageCroppedEvent) {
    this.image3 = null;
    this.croppedImage3 = event.base64;
    //this.supplierForm.setValue({
    //  imageId3: this.imageId3
    //});
  }
  updateSupplier(supId) {
    this.supplierForm.patchValue(supId);
    this.image1 = null;
    this.image2 = null;
    this.image3 = null;
  }
  updateFeaturedSupplier(supId) {
    this.supplierForm.patchValue(supId);
    this.image1 = supId.profileImage1;
    this.image2 = supId.profileImage2;
    this.image3 = supId.profileImage3;
    //this.imageId1 = supId.imageId1;
    //this.imageId2 = supId.imageId2;
    //this.imageId3 = supId.imageId3;
    this.updateImages = true;
  }
  resetForm(): void {
    this.supplierForm.reset();
  }
  //  City Drop Setting
  onCitySelectAll(item: any) {
    console.log(item);
    this.selectedCity = item;
  }
  onCityDeSelectALL(item: any) {
    this.selectedCity = [];
    console.log(item);
  }
  onCitySelect(item: any) {
    this.selectedCity.push(item);
    //console.log(this.selectedCategories);
  }
  onCityDeSelect(item: any) {

    this.selectedCity = this.selectedCity.filter(
      function (value, index, arr) {
        return value.id != item.id;
      });
  }
  public getAllCities() {
    this.service.get(this.service.apiRoutes.Common.GetCityList).subscribe(result => {
      this.cityList = result.json();
    })
  }

}
