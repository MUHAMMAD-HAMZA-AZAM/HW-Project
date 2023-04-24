import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { NgxSpinnerService } from "ngx-spinner";
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReportDatesValidation } from '../../Shared/Enums/enums';
import { ReportDateVm, ResponseVm } from '../../Shared/Models/HomeModel/HomeModel';
import { CustomerReportVM } from '../../Shared/Models/UserModel/SpTradesmanVM';
import html2canvas from 'html2canvas';
import { SupplieradsReport } from '../../Shared/Models/UserModel/SpTradesmanVM';
import { ExportToCsv } from 'export-to-csv';
import { ToastrService } from 'ngx-toastr';

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { jsPDF } from 'jspdf';
import 'jspdf-autotable';
import { UserOptions } from 'jspdf-autotable';
import { ModalDirective } from 'ngx-bootstrap/modal/public_api';
import { IdValueVm } from '../../Shared/Models/UserModel/SpSupplierVM';
import { PostAdVM, Images, ImageVM } from '../../Shared/Models/PrimaryUserModel/PrimaryUserModel';
import { CommonService } from '../../Shared/HttpClient/_http';
import { DynamicScriptLoaderService } from '../../Shared/CommonServices/dynamicScriptLoaderService';
import { ExcelFileService } from '../../Shared/CommonServices/excel-file.service';
import { DatePipe } from '@angular/common';
import { NgxImageCompressService } from 'ngx-image-compress';
import { UserDetailData } from '../../Shared/Models/PackagesAndPayments/PackagesModel';
import { JwtHelperService } from '@auth0/angular-jwt';

//declare const jsPDF: any;
interface jsPDFWithPlugin extends jsPDF {
  autoTable: (options: UserOptions) => jsPDF;
}


@Component({
  selector: 'app-manual-reports-posted-ads',
  templateUrl: './manual-reports-posted-ads.component.html',
  styleUrls: ['./manual-reports-posted-ads.component.css']
})
export class ManualReportsPostedAdsComponent implements OnInit {
  public appValForm: FormGroup;
  public promoForm: FormGroup;
  public startDate: Date;
  public endDate: Date;
  public startDate1: Date;
  public endDate1: Date;
  public startDateErrorMessage: string;
  public endDateErrorMessage: string;
  public cityid: "";
  public lastActive = false;
  public location = "";
  public submittedApplicationForm = false;
  public showTable = false;
  public showNullMessage = false;

  //public adsList: SupplieradsReport[] = [];
  public adsList = [];
  public excelFileList = []

  public cityList = [];
  public dropdownListForCity = {};
  public selectedCity = [];
  public selectedColumn = [];
  public columnList = [];
  public supplierName = "";
  public selectedCities = [];
 
  public pipe;
  public adId = "";
  public supplierSearch = 1;

  public customerAddressList = [];
  public searchFromAddress = [];
  public searchedAddress = [];
  public isAddress = false;
  public locationSearchKeyMatch = false;
  public reportFilter = false;
  public allowview;

  public userdata: UserDetailData[];
  public userdatafiltred: UserDetailData[];
  public submittedPostAdForm = false;
  public productCategoryList: IdValueVm[] = [];
  public productSubCategoryList: IdValueVm[] = [];
  public cityList1: IdValueVm[] = [];
  public postAdVm: PostAdVM = new PostAdVM();
  public submitted: boolean = false;
  public errorsList: any;
  public imageVM: Images = new Images();
  public imageVm: ImageVM = new ImageVM();
  public listOfImages: Images[] = [];
  public imageSubmitted: boolean = false;
  public listofFiles: any[] = [];
  @ViewChild('ImageModal', { static: true }) imageModal: ModalDirective;
  @ViewChild('confirmationModal') confirmationModal: ModalDirective;
  public sizeOfOriginalImage: number;
  public localCompressedURl: string;
  public sizeOFCompressedImage: number;
  public imageContent: any;
  public removeformatAdImage: any;
  public response: ResponseVm = new ResponseVm();
  public imgResultBeforeCompress: string;
  public imgResultAfterCompress: string;
  public AdId: any;
  public categoryId: any;
  public categoryName: any;
  public selectedSubcategoryName: string;





  @ViewChild('restChecked', { static: true }) restChecked: ElementRef;
  @ViewChild('dataTable', { static: true }) dataTable: ElementRef;
  @ViewChild('addNewSupplier', { static: true }) addNewSupplier: ModalDirective;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    
  constructor(
    public toastrService: ToastrService,
    public service: CommonService,
    public Loader: NgxSpinnerService,
    private router: Router,
    private formBuilder: FormBuilder,
    public dynamicScripts: DynamicScriptLoaderService,
    public excelService: ExcelFileService,
    private imageCompress: NgxImageCompressService,
    public _modalService: NgbModal
  ) {
    //this.appValForm = this.formBuilder.group({
    //  startDate: [null , Validators.required],
    //  endDate: [null , Validators.required]
    //})
  }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Posted Ad"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.appValForm = this.formBuilder.group({
          supplierSearch: [''],

    });
    this.populateCustomerAddress();
    this.dropdownListForCity = {
      singleSelection: false,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };
    this.promoForm = this.formBuilder.group({
      productCategoryId: ['', Validators.required],
      productSubCategoryId: ['', Validators.required],
      postTitle: ['', Validators.required],
      postDiscription: ['', Validators.required],
      price: ['', Validators.required],
      address: ['', Validators.required],
      town: ['', Validators.required],
      cityId: ['', Validators.required],
      collectionAvailable: [true, Validators.required],
      deliveryAvailable: [false],
      supplierId: ['', Validators.required],
    });
    //this.errorsList = {
    //  categoryIdError: PostAdErrors.categoryIdError,
    //  subCategoryIdError: PostAdErrors.subCategoryIdError,
    //  adTitleError: PostAdErrors.postTitleError,
    //  descriptionError: PostAdErrors.productDescriptionError,
    //  priceError: PostAdErrors.priceError,
    //  cityError: PostAdErrors.cityError,
    //  townError: PostAdErrors.townError,
    //  addressError: PostAdErrors.addressError,
    //  collectionError: PostAdErrors.collectionError,
    //  imageError: PostAdErrors.ImagesError
    //}
   
    this.selectedCity = [];
    this.selectedColumn = [];
   // this.CustomerCustomeReportS();
    this.getAllCities();


    this.columnList = [{ Id: 1, value: "CNIC" },
    { Id: 2, value: "Last Modified" },
    { Id: 3, value: "City" },
    { Id: 4, value: "Town" },
    { Id: 5, value: "Last Modified" }
    ];
    console.log(this.columnList);
    this.GetCitiesAndCategory();
  }
  resetForm() {
    
    this.location = "";
    this.endDate = null;
    this.startDate = null;
    this.supplierName = "";
    this.selectedCities = []
    this.selectedCity = [];
    this.adId = "";
    this.restChecked.nativeElement.checked = true;

  }
  funReportFilter() {
    this.reportFilter = !this.reportFilter;
  }
  exportCSV() {
    const options = {
      fieldSeparator: ',',
      quoteStrings: '"',
      decimalSeparator: '.',
      showLabels: true,
      showTitle: true,
      title: 'PostedAdsCSV',
      useTextFile: false,
      useBom: true,
      useKeysAsHeaders: true,
    };
    const csvExporter = new ExportToCsv(options);
    csvExporter.generateCsv(this.excelFileList);
  }

  DownloadXlsx() {
    this.excelService.exportAsExcelFile(this.excelFileList, "PostedAdsReport")
  }
  populateCustomerAddress() {
    
    this.service.get(this.service.apiRoutes.Supplier.GetSupplierAdsAddressList).subscribe(result => {
      console.log(result.json());
      this.customerAddressList = result.json();
      this.customerAddressList.forEach(value => {
        let add = { "customerAddress": value.address }
        this.searchFromAddress.push(add);
        
      })
    })
  }
  serachAddress(input) {
    if (input.length > 0) {
      this.searchedAddress = [];
      if (this.searchFromAddress.length > 0) {
        this.searchFromAddress.forEach(value => {
          if (value.customerAddress.toLowerCase().includes(input.toLowerCase())) {
            let add = { "customerAddress": value.customerAddress };
            this.searchedAddress.push(add);
          }
          if (this.searchedAddress.length > 0) {
            this.locationSearchKeyMatch = true;
          }
          else {
            this.locationSearchKeyMatch = false;
          }
        })
        this.isAddress = true;
      }
      else {
        this.isAddress = true;
        this.locationSearchKeyMatch = false;

      }
    } else {
      this.searchedAddress = [];
      this.isAddress = false;
    }
  }
  setAddressValue(e) {
    this.location = e.target.text;
    this.isAddress = false;
  }

  getRadioValue(e) {
    this.supplierSearch = e.target.value;
   }
  get f() {
    return this.appValForm.controls;
  }

  //loadStatusdropdown() {
  //  this.Loader.show();
  //  this.service.get(this.service.apiRoutes.Jobs.GetJobStatusForDropdown).subscribe(result => {
  //    
  //    this.statusList = result.json();
  //    console.log(this.statusList);
  //    this.completeStatusList = this.statusList.length;

  //    this.Loader.hide();
  //  },
  //    error => {
  //      console.log(error);
  //      this.Loader.hide();
  //    });

  //}
  //CustomerCustomeReportS() {
  //  
  //  //this.submittedApplicationForm = true;
  //  this.Loader.show();
  //  this.service.get(this.service.apiRoutes.Users.getAllCustonmersDropdown).subscribe(result => {
  //    
  //    this.JobsList = result.json();
  //    console.log(this.JobsList);
  //    this.completeList = this.JobsList.length;
  //    if (this.JobsList.length > 0) {

  //      this.loadStatusdropdown();
  //    }
  //    else {

  //      this.loadStatusdropdown();
  //    }
  //    this.Loader.hide();
  //  },
  //    error => {
  //      console.log(error);
  //      this.Loader.hide();
  //      this.loadStatusdropdown();
  //    });

  //}
  DownloadPdf() {

    const doc = new jsPDF('l', 'px', 'a4') as jsPDFWithPlugin;
    const pdfTable = this.dataTable.nativeElement;
    doc.autoTable({
      styles: { fontSize: 8 },
      html: pdfTable
    });
    doc.save('table.pdf')

    //
    //this.Loader.show();
    //this.dynamicScripts.load("pdf").then(data => {
    //  var htmltable = document.getElementById('dataTable');
    //  var shtmltable = document.getElementById('summary');
    //  html2canvas(htmltable).then(canvas => {
    //    // Few necessary setting options  
    //    var imgWidth = 208;
    //    var pageHeight = 295;
    //    var imgHeight = canvas.height * imgWidth / canvas.width;
    //    var heightLeft = imgHeight;

    //    const contentDataURL = canvas.toDataURL('image/png')
    //    let pdf = new jsPDF('p', 'mm'); // A4 size page of PDF  
    //    var position = 0;
    //    pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight)
    //    pdf.save('PostedAdsManualReport.pdf'); // Generated PDF  
    //    this.Loader.hide();
    //  });

    //})

  }
  public jobList = [];
  public jobcount = 0;
  public skipDate: boolean = false;

  ////////////
  public loadJobs() {
    var supplierpra = "";
    
    var data = this.appValForm.value;
    this.jobList = [];
    this.jobcount = 0;
    let cids = [];
    let sids = [];
    let ctids = [];
    //this.selectedCustomers.forEach(function (item) {
    //  cids.push(item.id);
    //});
    //this.selectedStatus.forEach(function (item) {
    //  sids.push(item.statusId);
    //});
    this.selectedCity.forEach(function (item) {

      ctids.push(item.id);
    });
    console.log(this.selectedCity);
    console.log(this.location);
    console.log(this.lastActive);

    if (this.supplierName != "" && this.supplierName != undefined) {
      if (this.supplierSearch == 1) {
        supplierpra = this.supplierName + "%";
      }
      else if (this.supplierSearch == 2) {
        supplierpra = "%" + this.supplierName + "%";
      }
      else if (this.supplierSearch == 3) {
        supplierpra = "%" + this.supplierName;
      }
    }

    this.pipe = new DatePipe('en-US');
    this.startDate1 = this.pipe.transform(this.startDate, 'MM/dd/yyyy');
    this.endDate1 = this.pipe.transform(this.endDate, 'MM/dd/yyyy');
    console.log(this.startDate);
    let query = ""

   // query = this.service.apiRoutes.Users.getPostedAdsForDynamicReport + "?StartDate=" + this.startDate1 + "&EndDate=" + this.endDate1 + "&supplier=" + supplierpra + "&city=" + ctids + "&lastActive=" + this.lastActive + "&location=" + this.location + "&adId=" + this.adId;
    query = this.service.apiRoutes.Users.getPostedAdsForDynamicReport + "?pageSize=" + 100000 + "&pageNumber=" + 1 + "&dataOrderBy=" +'DESC' + "&StartDate=" + this.startDate1 + "&EndDate=" + this.endDate1 + "&supplier=" + supplierpra + "&city=" + ctids + "&lastActive=" + this.lastActive + "&location=" + this.location + "&adId=" + this.adId;

    console.log(query);
    this.Loader.show();
    this.service.get(query).subscribe(result => {
      
      this.jobList = [];
      this.adsList = result.json();

      console.log(this.excelFileList)
      if (this.adsList) {
        this.excelFileList = this.adsList.map((ad) => {
          return {
            supplierAdsId: ad.supplierAdsId,
            supplierName: ad.supplierName,
            productName: ad.productName,
            subCategoryName: ad.subCategoryName,
            adTitle: ad.adTitle,
            addStatus: ad.addStatus,
            price: ad.price,
            cityName: ad.cityName,
            createdOn: new Date(ad.createdOn).toLocaleDateString(),
            town: ad.town,
            address: ad.address,
          }
        })
        this.reportFilter = true;
        this.showTable = true;
        this.showNullMessage = false;
        this.jobcount = this.adsList.length;

      }
      else {
        this.toastrService.error("No record found !", "Search");
        this.showNullMessage = false;
        this.showTable = false;

      }
      this.Loader.hide();
    },
      error => {
        console.log(error);
        this.Loader.hide();

      });
  }
  //onCustomerSelectAll(items: any) {
  //  console.log(items);
  //  this.selectedCustomers = items;


  //}
  //onCustomerDeSelectALL(items: any) {
  //  this.selectedCustomers = [];
  //  console.log(items);
  //}
  //onCustomerSelect(item: any) {
  //  this.selectedCustomers.push(item);
  //  //console.log(this.selectedCategories);
  //}
  //onCustomerDeSelect(item: any) {

  //  this.selectedCustomers = this.selectedCustomers.filter(
  //    function (value, index, arr) {
  //      //console.log(value);
  //      return value.id != item.id;
  //    }

  //  );

  //}
  //onStatusSelectAll(items: any) {
  //  console.log(items);
  //  this.selectedStatus = items;


  //}
  //onStatusDeSelectALL(items: any) {
  //  this.selectedStatus = [];
  //  console.log(items);
  //}
  //onStatusSelect(items: any) {
  //  this.selectedStatus.push(items);
  //  //console.log(this.selectedCategories);
  //}
  //onStatusDeSelect(items: any) {

  //  this.selectedStatus = this.selectedStatus.filter(
  //    function (value, index, arr) {
  //      //console.log(value);
  //      return value.id != items.id;
  //    }

  //  );

  //}
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

  onColumnSelectAll(item: any) {
    console.log(item);
    this.selectedColumn = item;
  }
  onColumnDeSelectALL(item: any) {
    this.selectedColumn = [];
    console.log(item);
  }
  onColumnSelect(item: any) {
    this.selectedColumn.push(item);
    //console.log(this.selectedCategories);
  }
  onColumnDeSelect(item: any) {

    this.selectedColumn = this.selectedColumn.filter(
      function (value, index, arr) {
        return value.id != item.id;
      });
  }

  public getAllCities() {
    this.service.get(this.service.apiRoutes.Customers.getCityList).subscribe(result => {
      this.cityList = result.json();

    })
  }
  public GetCitiesAndCategory() {
    this.service.get(this.service.apiRoutes.Supplier.GetAllProductCategory).subscribe(result => {
      this.productCategoryList = result.json();
    });
    //this.service.GetData(this.service.apiRoutes.Customers.getCityList, true).then(result => {
    //  this.cityList = result.json();
    //})
  }

  public postAd() {
    
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    this.submitted = true;
    if (this.promoForm.invalid) {
      return;
    }
    this.postAdVm = this.promoForm.value;
    if (this.postAdVm.productCategoryId == "") {
      this.promoForm.controls.productCategoryId.setValue(this.categoryId);
      this.postAdVm = this.promoForm.value;
      this.postAdVm.createdBy = decodedtoken.UserId;
    }
    this.postAdVm.imageVMs = [];
    for (var i = 0; i < this.listOfImages.length; i++) {
      this.imageVM = new Images();
      this.imageVM.filePath = this.listOfImages[i].filePath;
      this.imageVM.imageBase64 = this.listOfImages[i].imageBase64;
      this.imageVM.ImageContent = null;
      this.postAdVm.imageVMs.push(this.imageVM);
    }
    if (this.promoForm.invalid) {
      return;
    }
    else {
      if (this.AdId == 0) {
        //this.postAdVm.statusId = AdsStatus.Pending;
      }
      if (this.postAdVm.imageVMs != null && this.postAdVm.imageVMs.length > 0) {
        this.postAdVm.imageVMs[0].IsMain = true;
        var imageBytes = this.postAdVm.imageVMs[0].imageBase64.split(',');
        this.removeformatAdImage = imageBytes[1];
      }
      this.postAdVm.supplierAdId = this.AdId;
      this.service.PostData(this.service.apiRoutes.Supplier.SaveAndUpdateAd, this.postAdVm, true).then(result => {
        this.response = result.json();
        if (this.response != null) {
          //  localStorage.setItem("adImage", this.removeformatAdImage);
          this.promoForm.reset();
          this._modalService.dismissAll();
          this.toastrService.success("Data added Successfully", "Success");
        //  this.service.NavigateToRouteWithQueryString(this.service.apiRoutes.Supplier.PromoteAd, { queryParams: { supplierAdsId: this.response, subCategoryValue: this.selectedSubcategoryName, adTitle: this.postAdVm.postTitle, adImage: null } })
        }
      }, error => {
        console.log(error);
       // this.service.Notification.error(serviceErrors.serviceErrorMessage);
      })
    }
  }

  //public deleteAd() {
  //  this.confirmationModal.hide();
  //  this.service.GetData(this.service.apiRoutes.Supplier.DeleteAd + "?supplierAdId=" + this.AdId).then(data => {
  //    this.service.NavigateToRoute(this.service.apiUrls.Supplier.ManageAd);
  //  });
  //}

  public openConfirmationModal() {

    this.confirmationModal.show();
  }
  public closeConfirmationModal() {

    this.confirmationModal.hide();
  }

  public SelectCategroy(productCategoryId) {
    this.postAdVm.productCategoryId = productCategoryId;
    if (productCategoryId > 0) {
      this.service.get(this.service.apiRoutes.Supplier.GetAllProductSubCatagories + "?productId=" + productCategoryId).subscribe(result => {
        this.productSubCategoryList = result.json();
        this.promoForm.controls.productCategoryId.setValue(productCategoryId);

      }, error => {
        console.log(error);
       // this.service.Notification.error(serviceErrors.serviceErrorMessage);
      })
    }
  }

  public SelectSubCategory(productSubCategroyId) {
    this.postAdVm.productSubcategoryId = productSubCategroyId;
    var count = 0;
    while (this.productSubCategoryList[count].id == productSubCategroyId) {
      count++;
    }
    this.selectedSubcategoryName = this.productSubCategoryList[count].value;
  }

  public SelectCity(cityId) {
    this.postAdVm.cityId = cityId;
  }

  public OnSelectFile(event) {
    
    // this.listOfImages = [];
    this.postAdVm.imageVMs = [];
    var imageCount = (event.target.files.length) + (this.listOfImages.length);
    if (event.target.files.length > 4 || this.listOfImages.length > 4 || imageCount > 4) {
      this.imageSubmitted = true;
      //this.service.Notification.error(this.errorsList.imageError);
      return;
    }
    else {
      for (var i = 0; i < event.target.files.length; i++) {
        var reader = new FileReader();
        var file = event.target.files[i];
        this.imageVm.filePath = file['name'];
        reader.onload = (event: any) => {
          this.listofFiles.push(event.target.result);
          this.imageVm.localUrl = event.target.result;
          this.compressFile(this.imageVm.localUrl, this.imageVm.filePath, this.imageVm, i);
        }
        reader.readAsDataURL(event.target.files[i]);

      }

    }
  }

  public dataURItoBlob(dataURI) {
    const byteString = window.atob(dataURI);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8Array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) {
      int8Array[i] = byteString.charCodeAt(i);
    }
    const blob = new Blob([int8Array], { type: 'image/png' });
    return blob;
  }

  public compressFile(image, fileName, imageVms, index) {
    
    var orientation = -1;
    this.sizeOfOriginalImage = this.imageCompress.byteCount(image) / (1024 * 1024);
    this.imageCompress.compressFile(image, orientation, 50, 50).then(
      result => {
        this.imgResultAfterCompress = result;
        this.localCompressedURl = result;
        this.sizeOFCompressedImage = this.imageCompress.byteCount(result) / (1024 * 1024)
        this.imageVM = new Images();
        this.imageVM.filePath = fileName;
        const imageBlob = this.dataURItoBlob(this.imgResultAfterCompress.split(',')[1]);
        this.imageVM.IsMain = false;
        this.imageVM.imageBase64 = result;
        this.listOfImages.push(this.imageVM);
      })
  }

  public ImagePopUp(imageByte) {
    
    this.imageModal.show();
    this.imageContent = imageByte;
  }

  public Close() {
    this.imageModal.hide();
  }

  //public PopulateData() {
  //  if (this.AdId > 0) {
  //    this.service.GetData(this.service.apiRoutes.Supplier.GetEditAdDetail + "?supplierAdsId=" + this.AdId, true).then(data => {
  //      this.postAdVm = data.json();
  //      this.service.GetData(this.service.apiRoutes.Supplier.GetPostAdImagesList + "?supplierAdsId=" + this.AdId, true).then(result => {
  //        this.listofFiles = result.json();
  //        for (var i = 0; i < this.listofFiles.length; i++) {
  //          this.imageVM = new ImageVM();
  //          this.imageVM.imageBase64 = "data:image/png;base64," + this.listofFiles[i].imageContent;
  //          this.imageVM.filePath = this.listofFiles[i].filePath;
  //          this.imageVM.IsMain = this.listofFiles[i].isMain;
  //          this.listOfImages.push(this.imageVM);
  //        }
  //      }, error => {
  //        console.log(error);
  //      });
  //      if (this.postAdVm != null) {
  //        if (this.postAdVm.productSubcategoryId != null) {
  //          this.service.GetData(this.service.apiRoutes.Supplier.GetAllProductSubCatagories + "?productId=" + this.postAdVm.productCategoryId, true).then(result => {
  //            this.productSubCategoryList = result.json();
  //            var count = 0;
  //            while (this.productSubCategoryList[count].id != Number(this.postAdVm.productSubcategoryId)) {
  //              count++;
  //            }
  //            this.selectedSubcategoryName = this.productSubCategoryList[count].value;
  //          })
  //        }
  //        this.appValForm.patchValue(this.postAdVm);
  //        this.appValForm.controls.productSubCategoryId.setValue(this.postAdVm.productSubcategoryId);

  //      }
  //    }, error => {
  //      console.log(error);
  //    });
  //  }
  //  else {
  //    this.service.GetData(this.service.apiRoutes.Supplier.GetBusinessProfile, true).then(result => {
  //      var data = result.json();
  //      this.appValForm.controls.address.setValue(data.businessAddress);
  //      this.appValForm.controls.cityId.setValue(data.city);
  //    }, error => {
  //      console.log(error)
  //    });
  //  }
  //}
  CancelImage(index) {
    
    //var index = findIndex(image);
    this.listOfImages.splice(index - 1, 1);

    // var index = findIndex.call(ids);

    //var ids = document.getElementById("PostImage");
    //ids.remove();
    //var index= id.tabIndex;
    //if (this.listOfImages.length > 0) {
    // // this.listOfImages.length = this.listOfImages.length - 1;
    //}
  }
  public showModalAdd1(content) {
    this._modalService.open(content);
    this.bindusernames();
  }
  public bindusernames() {
    
    //var data1 = this.pkgfilters.value;
    //var type = data1.UserRoleIdfilter;
    //if (type == 3)
    this.service.get(this.service.apiRoutes.Customers.GetCustomer).subscribe(result => {
      
      this.userdata = result.json();
      this.userdatafiltred = this.userdata.filter(x => x.roleId == 4);
      //this.userdata = this.userdata.filter(obj => obj.firsrName.length> 0)
    });
  }
}
