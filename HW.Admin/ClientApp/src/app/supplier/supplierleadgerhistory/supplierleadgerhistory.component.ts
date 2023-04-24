import { Component, ElementRef, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { DynamicScriptLoaderService } from '../../Shared/CommonServices/dynamicScriptLoaderService';
import { ExcelFileService } from '../../Shared/CommonServices/excel-file.service';
import { aspNetUserRoles, httpStatus, IResponseVM, loginsecurity, Orders, Payments, StatusCode, TrackingStatusType } from '../../Shared/Enums/enums';
import { CommonService } from '../../Shared/HttpClient/_http';
import { ResponseVm } from '../../Shared/Models/HomeModel/HomeModel';
import { SortList } from '../../Shared/Sorting/sortList';
import { DatePipe } from '@angular/common';
import { UserOptions } from 'jspdf-autotable';
import { ExportToCsv } from 'export-to-csv';
import { jsPDF } from 'jspdf';
import 'jspdf-autotable';
import { stringify } from '@angular/compiler/src/util';
import { STATUS_CODES } from 'http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { IDetails, IOrderTrack } from 'src/app/Shared/Interfaces/OrderTrack/Interface';
import { ShippingService } from 'src/app/Shared/HttpClient/shipping.service';
interface jsPDFWithPlugin extends jsPDF {
  autoTable: (options: UserOptions) => jsPDF;
}

@Component({
  selector: 'app-supplierleadgerhistory',
  templateUrl: './supplierleadgerhistory.component.html',
  styleUrls: ['./supplierleadgerhistory.component.css']
})
export class SupplierleadgerhistoryComponent implements OnInit {
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  constructor(public toaster: ToastrService,
    public service: CommonService,
    public Loader: NgxSpinnerService,
    public shippingService: ShippingService,
    private router: Router,
    private fb: FormBuilder,
    public dynamicScripts: DynamicScriptLoaderService,
    public excelService: ExcelFileService,
    public _modalService: NgbModal,
    public sortBy: SortList) { }
  public responce: IResponseVM;
  public GetSuppliersOrderlist: any = [];
  public cityList = [];
  public OrderStatusList = []
  public dropdownListForCity = {};
  public appValForm: FormGroup;
  public orderCancelForm: FormGroup;
  public dropdownListForOrderStatus = {};
  public searchOrderlistForm: any;
  public excelFileList = [];
  public orderDetailsList: any = [];
  public ordersByCustomers: any = [];
  public productsByCustomer: any = [];
  public tracking_number: any = [];
  public orderCancellationReasonsList: any = [];
  public pdfrequest: boolean = true;
  public showtable: boolean = false;
  public ordercount = 0;
  public shippingAmount;
  public shippingData = [];
  public isProceed: boolean = false;
  public userId: string;
  public loggedUserRole: string;
  public cancelOrderId: number;
  public OrderStatus = Orders;
  public paymentStatus = Payments;
  public trackingNumber: number | undefined = 0;
  public orderId: number | undefined = 0;
  public trackingOrderRecord: IOrderTrack = {} as IOrderTrack;
  public trackingOrderItemRecord: IDetails = {} as IDetails;
  public decodedtoken: any;
  public totalCommission = 0;
  public totalPayable = 0;
  public totalShipping = 0;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  @ViewChild('dataTable', { static: true }) dataTable: ElementRef;

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Suppliers OrdersList"));
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    console.log(this.decodedtoken);
    this.userId = this.decodedtoken.UserId;
    this.loggedUserRole = this.decodedtoken.Role;
    if (!this.userRole.allowView) {
      this.router.navigateByUrl('/login');
    }
    this.appValForm = this.fb.group({
      customerId: [null],
      orderId: [null],
      startDate: [null],
      endDate: [null],
    });
    this.suppliersOrderlist();
  }

  suppliersOrderlist() {
    let formData = this.appValForm.value;
    this.Loader.show();
    this.service.post(this.service.apiRoutes.Supplier.GetSuppliersLeadgerlist, formData).subscribe(result => {
      this.responce = JSON.parse(result.json());
      if (this.responce.status == StatusCode.OK) {
        this.GetSuppliersOrderlist = this.responce.resultData;
        this.excelFileList = this.GetSuppliersOrderlist;
        if (this.GetSuppliersOrderlist?.length > 0) {
          this.ordercount = this.GetSuppliersOrderlist.length;
          this.GetSuppliersOrderlist.forEach(item => {
            this.totalCommission += item.commission;
            this.totalShipping += item.totalShippingCost;
            this.totalPayable += item.totalPayableToSupplier;
            });
          this.showtable = false;
        }
        else {
          this.showtable = true;
          this.toaster.error("opps data found!!", "Not Found")
        }
        this.Loader.hide();
      }
    }, error => {
      console.log(error);
    });
  }
  DownloadXlsx() {
    this.excelService.exportAsExcelFile(this.excelFileList, "SupplierLeadgerList")
  }
  exportCSV() {
    const options = {
      fieldSeparator: ',',
      quoteStrings: '"',
      decimalSeparator: '.',
      showLabels: true,
      showTitle: true,
      title: 'SupplierLeadgerList',
      useTextFile: false,
      useBom: true,
      useKeysAsHeaders: true,
    };
    const csvExporter = new ExportToCsv(options);
    csvExporter.generateCsv(this.excelFileList);
  }

  ResetForm() {
    this.appValForm.reset();
  }
  DownloadPdf() {
    const doc = new jsPDF('l', 'px', 'a4') as jsPDFWithPlugin;
    const pdfTable = this.dataTable.nativeElement;
    doc.autoTable({
      styles: { fontSize: 8 },
      html: pdfTable
    });
    doc.save('SupplierLeadgerList.pdf')
  }




}
