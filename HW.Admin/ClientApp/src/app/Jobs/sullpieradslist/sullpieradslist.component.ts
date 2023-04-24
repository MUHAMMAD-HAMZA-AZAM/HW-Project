import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { NgxSpinnerService } from "ngx-spinner";
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ResponseVm } from '../../Shared/Models/HomeModel/HomeModel';
import { ExportToCsv } from 'export-to-csv';
import { ToastrService } from 'ngx-toastr';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { jsPDF } from 'jspdf';
import 'jspdf-autotable';
import { UserOptions } from 'jspdf-autotable';
import { ModalDirective, ModalOptions } from 'ngx-bootstrap/modal/public_api';
import { IdValueVm } from '../../Shared/Models/UserModel/SpSupplierVM';
import { PostAdVM, Images, ImageVM } from '../../Shared/Models/PrimaryUserModel/PrimaryUserModel';
import { CommonService } from '../../Shared/HttpClient/_http';
import { DynamicScriptLoaderService } from '../../Shared/CommonServices/dynamicScriptLoaderService';
import { ExcelFileService } from '../../Shared/CommonServices/excel-file.service';
import { DatePipe } from '@angular/common';
import { NgxImageCompressService } from 'ngx-image-compress';
import { UserDetailData } from '../../Shared/Models/PackagesAndPayments/PackagesModel';
import { JwtHelperService } from '@auth0/angular-jwt';
import { SortList } from '../../Shared/Sorting/sortList';
import { from } from 'rxjs';
//declare const jsPDF: any;
interface jsPDFWithPlugin extends jsPDF {
  autoTable: (options: UserOptions) => jsPDF;
}

@Component({
  selector: 'app-sullpieradslist',
  templateUrl: './sullpieradslist.component.html',
  styleUrls: ['./sullpieradslist.component.css']
})
export class SullpieradslistComponent implements OnInit {

  public appValForm: FormGroup;
  public promoForm: FormGroup;
  public startDate: Date;
  public endDate: Date;
  public startDate1: Date;
  public endDate1: Date;
  public startDateErrorMessage: string;
  public endDateErrorMessage: string;
  public lastActive = false;
  public location = "";
  public submittedApplicationForm = false;
  public showTable = false;
  public showNullMessage = false;
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
  public allowDelete;
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

  public data = [];
  public totalRecoards = 101;
  public pageing1 :any;
  public recoardNoFrom = 0;
  public recoardNoTo = 50;
  public totalJobs: number;
  public totalPages: number;
  public selectedPage: number;
  public pageNumber: number = 1;
  public pageSize: number = 50;
  public dataOrderBy = "DESC";
  public noOfPages;
  public adDetails : any;
  public selectedAdId;
  public deletedByUserId: any;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  @ViewChild('restChecked', { static: true }) restChecked: ElementRef;
  @ViewChild('dataTable', { static: true }) dataTable: ElementRef;
  @ViewChild('addNewSupplier', { static: true }) addNewSupplier: ModalDirective;
  @ViewChild('contentAdDetails', { static: true }) contentAdDetails: ElementRef;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(
    public toastrService: ToastrService,
    public service: CommonService,
    public Loader: NgxSpinnerService,
    private router: Router,
    private formBuilder: FormBuilder,
    public dynamicScripts: DynamicScriptLoaderService,
    public excelService: ExcelFileService,
    private imageCompress: NgxImageCompressService,
    public _modalService: NgbModal,
    public sortBy: SortList
  )
  {

  }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Posted Ads"));
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
   
    this.selectedCity = [];
    this.selectedColumn = [];
    this.getAllCities();
    this.loadJobs();
    this.pageNumber = 1;
    this.pageSize = 50;

    this.dataOrderBy = "DESC";

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
    this.listOfImages = [];
    this.adId = "";
    this.restChecked.nativeElement.checked = true;
    this.submitted = false;
    this.promoForm.markAsUntouched();
    this.promoForm.clearValidators();
    

  }
  public showModal(content , adId) {
    this.supplierAdDetails(adId);
    const config: ModalOptions = { class: "modal-lg" };
    
    this._modalService.open(content, { size: 'lg' });
    console.log(this.contentAdDetails.nativeElement);
    console.log(content);
 
  }
  supplierAdDetails(adId) {
    
    this.service.get(this.service.apiRoutes.Supplier.GetSupplierAdDetails + "?adId=" + adId).subscribe(res => {
      console.log(res.json());
      this.adDetails = res.json();
    })
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
  get f1() {
    return this.promoForm.controls;
  }
  DownloadPdf() {
    
    
    const doc = new jsPDF('l', 'px', 'a4') as jsPDFWithPlugin;
    const pdfTable = this.dataTable.nativeElement;
    doc.autoTable({
      styles: { fontSize: 8 },
      html: pdfTable
    });
    doc.save('table.pdf')
  }
  public jobList = [];
  public jobcount = 0;
  public skipDate: boolean = false;

  public loadJobs() {
    var supplierpra = "";
    
    var data = this.appValForm.value;
    this.jobList = [];
    this.jobcount = 0;
    let cids = [];
    let sids = [];
    let ctids = [];
 
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
      else if (this.supplierSearch == 3) {
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

    query = this.service.apiRoutes.Users.getPostedAdsForDynamicReport + "?pageSize=" + this.pageSize + "&pageNumber=" + this.pageNumber + "&dataOrderBy=" + this.dataOrderBy + "&StartDate=" + this.startDate1 + "&EndDate=" + this.endDate1 + "&supplier=" + supplierpra + "&city=" + ctids + "&lastActive=" + this.lastActive + "&location=" + this.location + "&adId=" + this.adId;

    console.log(query);
    this.Loader.show();
    this.service.get(query).subscribe(result => {
      
      this.jobList = [];
      this.adsList = result.json();

      this.totalRecoards = this.adsList[0].noOfRecoards;
      this.noOfPages = this.adsList[0].noOfRecoards / this.pageSize;
      this.noOfPages = Math.floor(this.noOfPages);
      if (this.adsList[0].noOfRecoards > this.noOfPages) {
        this.noOfPages = this.noOfPages + 1;
      }
      this.pageing1 = [];
      for (var x = 1; x <= this.noOfPages; x++) {
        this.pageing1.push(
          x
        );
      }
      this.recoardNoFrom = (this.pageSize * this.pageNumber) - this.pageSize + 1;
      this.recoardNoTo = (this.pageSize * this.pageNumber);
      if (this.recoardNoTo > this.adsList[0].noOfRecoards)
        this.recoardNoTo = this.adsList[0].noOfRecoards;

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
        this.recoardNoFrom = 0;
        this.recoardNoTo = 0;
        this.noOfPages = 0;
        this.totalRecoards = 0;
        this.adsList = [];

      }
      this.Loader.hide();
    },
      error => {
        console.log(error);
        this.Loader.hide();

      });
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
  }

  public postAd() {
    
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    this.submitted = true;
    if (this.promoForm.valid) {
      
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
      if (this.postAdVm.imageVMs != null && this.postAdVm.imageVMs.length > 0) {
        this.postAdVm.imageVMs[0].IsMain = true;
        var imageBytes = this.postAdVm.imageVMs[0].imageBase64.split(',');
        this.removeformatAdImage = imageBytes[1];
      }
      
      this.postAdVm.supplierAdId = this.AdId;
      this.service.PostData(this.service.apiRoutes.Supplier.SaveAndUpdateAd, this.postAdVm, true).then(result => {
        this.response = result.json();
        if (this.response != null) {
          this.promoForm.reset();
          this._modalService.dismissAll();
          this.toastrService.success("Data added Successfully", "Success");
          this.loadJobs();
          
        }
      }, error => {
        console.log(error);
      })
    }
    else if (this.promoForm.invalid) {
      
      this.promoForm.markAllAsTouched();
    }
    else {
     
    }
  }

  public openConfirmationModal() {

    this.confirmationModal.show();
  }
  public closeConfirmationModal() {

    this.confirmationModal.hide();
  }

  public SelectCategroy(productCategoryId) {
    this.postAdVm.productCategoryId = productCategoryId;
    if (productCategoryId > 0 ) {
      this.service.get(this.service.apiRoutes.Supplier.GetAllProductSubCatagories + "?productId=" + productCategoryId).subscribe(result => {
        this.productSubCategoryList = result.json();
        this.promoForm.controls.productCategoryId.setValue(productCategoryId);

      }, error => {
        console.log(error);
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
    
    this.postAdVm.imageVMs = [];
    var imageCount = (event.target.files.length) + (this.listOfImages.length);
    if (event.target.files.length > 10 || this.listOfImages.length > 10 || imageCount > 10) {
      this.imageSubmitted = true;
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
    
    this.imageContent = imageByte;
  }

  public Close() {
    this.imageModal.hide();
  }
  CancelImage(index) {
    
    this.listOfImages.splice(index - 1, 1);
  }
  public showModalAdd1(content) {
    this._modalService.open(content, { size:'lg' });
    this.bindusernames();
  }
  public bindusernames() {
    
    this.service.get(this.service.apiRoutes.Customers.GetCustomer).subscribe(result => {
      
      this.userdata = result.json();
      this.userdatafiltred = this.userdata.filter(x => x.roleId == 4);
      this.userdatafiltred = this.sortBy.transform(this.userdatafiltred, "firstName", 'asc');
    });
  }


  clickchange() {
    this.loadJobs();
  }
  PageSizeChange() {

    this.pageNumber = 1;
    this.loadJobs();
  }
  PriviousClick() {
    if (this.pageNumber > 1) {
      this.pageNumber = parseInt(this.pageNumber.toString()) - 1;
      this.loadJobs();
    }

  }
  NextClick() {
    if (this.pageNumber < this.noOfPages) {
      this.pageNumber = parseInt(this.pageNumber.toString()) + 1;
      this.loadJobs();
    }

  }
  LoadNewestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "DESC";
    this.loadJobs();

  }
  LoadOldestRecoards() {
    this.pageNumber = 1;
    this.dataOrderBy = "ASC";
    this.loadJobs();

  }
  NumberOfPages() {
    this.totalPages = Math.ceil(this.totalJobs / this.pageSize);
  }
  changePageSize() {
    this.pageNumber = 1;
    this.NumberOfPages();
    this.loadJobs();
  }
 
  DeleteAdByAdId(AdIdforDelete, deleteContent) {
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    
    this.deletedByUserId = decodedtoken.UserId;
    
    this.selectedAdId = AdIdforDelete;
    this._modalService.open(deleteContent);
    
  }

  confirmDeleteAd() {
    
    this.service.get(this.service.apiRoutes.Supplier.deleteAdWithAdId + "?AdIdforDelete=" + this.selectedAdId + "&deletedByUserId=" + this.deletedByUserId).subscribe(result => {

      if (result.status == 200) {
        
        this.selectedAdId = "";
        this.toastrService.error("Record deleted successfully", "Delete");
        this._modalService.dismissAll();
        this.loadJobs();
      }
      else {
        this.toastrService.warning("Something went wrong please try again", "Error");
        this._modalService.dismissAll();
      }

    }
    );
  }
}
