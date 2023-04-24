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
import { SortList } from '../../Shared/Sorting/sortList';
import { UserOptions } from 'jspdf-autotable';
import { ExportToCsv } from 'export-to-csv';
import { jsPDF } from 'jspdf';
import 'jspdf-autotable';
import { JwtHelperService } from '@auth0/angular-jwt';
import { IDetails, IOrderTrack } from 'src/app/Shared/Interfaces/OrderTrack/Interface';
import { ShippingService } from 'src/app/Shared/HttpClient/shipping.service';
interface jsPDFWithPlugin extends jsPDF {
  autoTable: (options: UserOptions) => jsPDF;
}

@Component({
  selector: 'app-suppliers-orders-list',
  templateUrl: './suppliers-orders-list.component.html',
  styleUrls: ['./suppliers-orders-list.component.css']
})
export class SuppliersOrdersListComponent implements OnInit {
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
  public orderItemsPaymentAndDispatchStatus: boolean = false;
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
  jwtHelperService: JwtHelperService = new JwtHelperService();
  @ViewChild('dataTable', { static: true }) dataTable: ElementRef;
  @ViewChild('proceedModal', { static: true }) proceedModal: ElementRef;
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
      customerName: [''],
      customerId: [null],
      orderId: [null],
      startDate: [null],
      endDate: [null],
      city: [null],
    });
    //----- Customer Reason Form
    this.orderCancelForm = this.fb.group({
      reasonId: [null, [Validators.required]],
      comments: ['', [Validators.required]],
    });
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


    this.suppliersOrderlist();
    this.getAllCities();
  }
  updateOrder(status, orderId, supplierId, customerId,customerFirebaseClientId) {

    let newOrderStatus = (status == Orders.PackedAndShipped) ? Orders.Delivered : Orders.Completed;
    let data = {
      orderStatus: newOrderStatus,
      orderId: orderId,
      supplierId,
      customerId,
      firebaseClientId: customerFirebaseClientId,
      isFromWeb:true
    };
    console.log(data);

    this.service.PostData(this.service.apiRoutes.Supplier.UpdateOrderStatus, JSON.stringify(data)).then(result => {
      if (result.status == httpStatus.Ok) {
        this.toaster.success("Order status updated", "Success");
        this.suppliersOrderlist();
      }
    });

  }
  suppliersOrderlist() {
    let ctids = '';
    let orderstatusids = '';
    let formData = this.appValForm.value;
    if (formData.city != null) {
      formData.city.forEach(function (item) {
        ctids = ctids + ',' + item.id;
      });
      formData.city = ctids;
    }
    formData.type = TrackingStatusType.GeneralTracking,
      formData.userRole = loginsecurity.Role,
      this.Loader.show();
    this.service.post(this.service.apiRoutes.Supplier.GetSuppliersOrderlist, formData).subscribe(result => {
      this.responce = JSON.parse(result.json());
      if (this.responce.status == StatusCode.OK) {
        this.GetSuppliersOrderlist = this.responce.resultData;
        if (this.GetSuppliersOrderlist?.length > 0) {
          this.ordersByCustomers = [];
          this.GetSuppliersOrderlist.forEach(item => {
            if (!this.ordersByCustomers.some(x => x.orderId == item.orderId)) {

              let ordersById = this.GetSuppliersOrderlist.filter(o => o.orderId == item.orderId);
              if (ordersById.some(s => s.orderStatusId == this.OrderStatus.PackedAndShipped)) {
                item.orderStatusName = 'Packed&Shipped';
                item.orderStatusId = this.OrderStatus.PackedAndShipped;
              }
              let obj = {
                totalAmount: item.totalAmount,
                trackingNumber: item.trackingNumber,
                orderId: item.orderId,
                customerName: item.customerName,
                customerId: item.customerId,
                customerFirebaseClientId: item.customerFirebaseClientId,
                cityName: item.cityName,
                createdOn: item.createdOn,
                orderStatusName: item.orderStatusName,
                orderStatusId: item.orderStatusId,
                supplierId: item.supplierId,
                paymentReceivedStatus: item.paymentReceivedStatus,
                supplierPaymentStatus: item.supplierPaymentStatus

              }
              this.ordersByCustomers.push(obj);
            }

          });
          this.excelFileList = this.ordersByCustomers;
          this.ordercount = this.ordersByCustomers.length;
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
    this.excelService.exportAsExcelFile(this.excelFileList, "SupplierOrderList")
  }
  exportCSV() {
    const options = {
      fieldSeparator: ',',
      quoteStrings: '"',
      decimalSeparator: '.',
      showLabels: true,
      showTitle: true,
      title: 'SupplierOrderList',
      useTextFile: false,
      useBom: true,
      useKeysAsHeaders: true,
    };
    const csvExporter = new ExportToCsv(options);
    csvExporter.generateCsv(this.excelFileList);
  }
  getAllCities() {
    this.service.get(this.service.apiRoutes.Customers.getCityList).subscribe(result => {
      this.cityList = result.json();
    })
  }
  GetSupplierOrderDetails(id, ref) {
    this.Loader.show();
    this.productsByCustomer = [];
    this.tracking_number = [];
    this.service.get(this.service.apiRoutes.Supplier.GetSuppliersOrderlistById + "?id=" + id).subscribe(res => {
      this.productsByCustomer = [];
      this.responce = JSON.parse(res.json());
      if (this.responce.status == StatusCode.OK) {
        this.orderDetailsList = this.responce.resultData;
        if (this.orderDetailsList.length > 0) {
          if (this.orderDetailsList[0].orderDetailId) {
            this.orderDetailsList.forEach(item => {

              if (!this.productsByCustomer.some(x => x.productId == item.productId && x.orderDetailId == item.orderDetailId)) {
                let obj = {
                  colorName: item.colorName,
                  createdOn: item.createdOn,
                  customerId:item.customerId,
                  customerName: item.customerName,
                  customerFirebaseClientId: item.customerFirebaseClientId,
                  customerAddress: item.customerAddress,
                  customerShippingAddress: item.customerShippingAddress,
                  discountedAmount: item.discountedAmount,
                  dispatchPaymentStatus: item.dispatchPaymentStatus,
                  itemId: item.itemId,
                  orderId: item.orderId,
                  trackingNumber: item.trackingNumber,
                  orderStatusId: item.orderStatusId,
                  mobileNumber: item.mobileNumber,
                  orderDetailId: item.orderDetailId,
                  orderStatus: item.orderStatus,
                  paymentReceivedStatus: item.paymentReceivedStatus,
                  productId: item.productId,
                  productName: item.productName,
                  productPrice: item.productPrice,
                  quantity: item.quantity,
                  total: item.total,
                  productDiscountAmount: item.productDiscountAmount,
                  productPromotionAmount: item.productPromotionAmount,
                  totalPayable: item.totalPayable,
                  shippingAmount: item.shippingAmount,
                  shippingCity: item.shippingCity,
                  suplierName: item.suplierName,
                  supplierId: item.supplierId,
                  variantId: item.variantId,
                  shippingStatus: "UnPaid"
                }
                this.productsByCustomer.push(obj);

              }
              this.tracking_number = [];
              for (var i = 0; i < this.productsByCustomer.length; i++) {
                this.tracking_number.push(Number(this.productsByCustomer[i].trackingNumber));
              }

            });
            let obj = { "tracking_number": this.tracking_number };
            this.shippingService.PostData('https://sonic.pk/api/payments', obj).subscribe(res => {
              let response = <any>res;
              let trackingItems = Object.keys(response.payments);
              for (var i = 0; i < this.productsByCustomer.length; i++) {
                if (trackingItems[i] == this.productsByCustomer[i].trackingNumber) {
                  this.productsByCustomer[i].shippingStatus = response.payments[trackingItems[i]][0].payment_status;
                }
              }
            });
            // console.log(this.productsByCustomer);
            for (var i = 0; i < this.productsByCustomer.length; i++) {
              if ((this.productsByCustomer[i].paymentReceivedStatus && this.productsByCustomer[i].dispatchPaymentStatus)) {
                this.isProceed = true;
              }
              else {
                this.isProceed = false;
                break;
              }
            }
          }
          else {
            this.orderDetailsList.forEach(item => {
              if (!this.productsByCustomer.some(x => x.productId == item.productId && x.variantId == item.variantId)) {
                let obj = {
                  colorName: item.colorName,
                  createdOn: item.createdOn,
                  customerAddress: item.customerAddress,
                  customerName: item.customerName,
                  customerShippingAddress: item.customerShippingAddress,
                  discountedAmount: item.discountedAmount,
                  dispatchPaymentStatus: item.dispatchPaymentStatus,
                  itemId: item.itemId,
                  orderId: item.orderId,
                  orderStatusId: item.orderStatusId,
                  mobileNumber: item.mobileNumber,
                  orderDetailId: item.orderDetailId,
                  orderStatus: item.orderStatus,
                  paymentReceivedStatus: item.paymentReceivedStatus,
                  productId: item.productId,
                  productName: item.productName,
                  productPrice: item.productPrice,
                  quantity: item.quantity,
                  total: item.total,
                  productDiscountAmount: item.productDiscountAmount,
                  productPromotionAmount: item.productPromotionAmount,
                  totalPayable: item.totalPayable,
                  shippingAmount: item.shippingAmount,
                  shippingCity: item.shippingCity,
                  suplierName: item.suplierName,
                  supplierId: item.supplierId,
                  variantId: item.variantId
                }
                this.productsByCustomer.push(obj);
              }

            });
            this.isProceed = true;

          }
          this._modalService.open(ref, { size: 'xl', backdrop: 'static' });
          this.Loader.hide();
        }
      }

    });
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
    doc.save('SupplierOderList.pdf')
  }
  paymentReceivedStatus(event, supplierId) {

    let status = event.target.checked;
    let id = event.target.id;
    //this.service.get(this.service.apiRoutes.Customers.BlockCustomer + "?customerId=" + customerId + "&userId=" + userId + "&status=" + status).subscribe(result => {
    //  let response = result.json();
    //  if (response.status == 200) {
    //    if (status) {
    //      this.toastr.success("Cutomer Unblocked successfully!", "Success");
    //    }
    //    else {
    //      this.toastr.warning("Cutomer blocked successfully!", "Warning");
    //    }
    //    this.populateListData();
    //  }
    //})
  }
  dispatchPaymentStatus(event, supplierId) {

    let status = event.target.checked;
    let id = event.target.id;
    //this.service.get(this.service.apiRoutes.Customers.BlockCustomer + "?customerId=" + customerId + "&userId=" + userId + "&status=" + status).subscribe(result => {
    //  let response = result.json();
    //  if (response.status == 200) {
    //    if (status) {
    //      this.toastr.success("Cutomer Unblocked successfully!", "Success");
    //    }
    //    else {
    //      this.toastr.warning("Cutomer blocked successfully!", "Warning");
    //    }
    //    this.populateListData();
    //  }
    //})
  }
  Proceed(item) {
    this.shippingData = item;
    this.isProceed = true;
    console.log(this.shippingData);
    for (var i = 0; i < this.shippingData.length; i++) {
      if ((this.shippingData[i].paymentReceivedStatus && this.shippingData[i].dispatchPaymentStatus)) {
        this.orderItemsPaymentAndDispatchStatus = true;
        break;
      }
      else {
        this.orderItemsPaymentAndDispatchStatus = false;
       
      }
    }
    if (this.orderItemsPaymentAndDispatchStatus) {
      console.log("You can Proceed the payment");
      this.service.post(this.service.apiRoutes.PackagesAndPayments.UpdateShippingChargesAndPaymentStatus, JSON.stringify(this.shippingData)).subscribe(result => {
        this.responce = result.json();
        if (this.responce.status == StatusCode.OK) {
          this.toaster.success("Successfully Update!!", "Update")
          this._modalService.dismissAll();
        }
        else {
          this.toaster.error("!!", "Not Update")
        }
      });
    }
    else {
      this._modalService.open(this.proceedModal);
      setTimeout(() => {
        this._modalService.dismissAll();
      },5000);
      console.log("You cannot Proceed the payment");
    }
  }
  get g() {
    return this.orderCancelForm.controls;
  }
  //------------- Show Order CancellationReasons List
  public getOrderCancellationReasonsList(orderCancelModal:TemplateRef<any>) {
    this.service.get(this.service.apiRoutes.Orders.GetOrderCancellationReasonsList + `?userRole=${aspNetUserRoles.Admin}`).subscribe(res => {
      this.responce = JSON.parse(res.json());
      if (this.responce.status == StatusCode.OK) {
        this.orderCancellationReasonsList = this.responce.resultData;
        this._modalService.open(orderCancelModal, { centered: true, size: 'lg', backdrop: 'static', keyboard: false });
        //console.log(this.orderCancellationReasonsList);
      }
    }, error => {
      console.log(error);
    });
  }
  //------------- Show Cancel Order Modal
  public showModal(orderId: number, orderCancelModal: TemplateRef<any>) {
    // get Order Cancellation reasons
    this.getOrderCancellationReasonsList(orderCancelModal);
    this.cancelOrderId = orderId;
  }
  //------------- Cancel Customer Order
  public cancelOrder(): void {
    let formData = this.orderCancelForm.value;
    formData.orderId = this.cancelOrderId;
    formData.orderStatus = Orders.Cancelled;
    formData.reasonId = (formData?.reasonId) ? parseInt(formData.reasonId) : 0;
    formData.createdBy = this.userId;
    formData.userRole = parseInt(aspNetUserRoles.Admin);
    this.service.PostData(this.service.apiRoutes.Orders.OrderCancelByAdmin, JSON.stringify(formData)).then(res => {
      this.responce = res.json();
      if (this.responce.status == StatusCode.OK) {
        this._modalService.dismissAll();
        this.toaster.warning("Order has been cancelled successfully!", "Alert");
        this.suppliersOrderlist();
        this.orderCancelForm.reset();
        this.Loader.hide();
      }

    }, error => {
      console.log(error);
    });
  }
  //------------- Show Order Track Modal
  public showTrackModal(orderId: number, orderTrackModal: TemplateRef<any>) {
    this.trackingOrderRecord.details = [];
    this.orderId = orderId;
    this.getOrderTrackingForAdmin(orderTrackModal);
   
  }
  //-------------- For Order Tracking
  public getOrderTrackingForAdmin(orderTrackModal: TemplateRef<any>) {
    let obj = {
      order_id: this.orderId,
      type: TrackingStatusType.ShipperRelatedTracking,
      userRole: loginsecurity.Role,
    };
    this.service.Loader.show();
    this.service.PostData(this.service.apiRoutes.Supplier.GetOrderTrackingForAdmin, JSON.stringify(obj)).then(res => {
      this.responce = JSON.parse(res.json())
      if (this.responce.status == StatusCode.OK) {
        this.trackingOrderRecord.details = <any>this.responce.resultData;
        this._modalService.open(orderTrackModal, { centered: true, size: 'xl', backdrop: 'static', keyboard: false, scrollable:true });
      }
      this.service.Loader.hide();
    });
  }
  //------------------- Show Modal For OrderedItem Tracking
  public showOrderItemModal(trackId: string, modalName: TemplateRef<any>): void {
    this.trackingNumber = 0;
    this.trackingNumber = Number(trackId);
    this.getOrderedItemTrackingForAdmin(modalName);
  }
  //------------------- For Shippers' Tracking
  public getOrderedItemTrackingForAdmin(modalName: TemplateRef<any>) {
    let obj = {
      tracking_number: this.trackingNumber,
      type: TrackingStatusType.ShipperRelatedTracking,
      userRole: loginsecurity.Role,
    };
    this.service.Loader.show();
    this.service.PostData(this.service.apiRoutes.Supplier.GetOrderItemTrackingForAdmin, JSON.stringify(obj)).then(res => {
      this.responce = JSON.parse(res.json())
      if (this.responce.status == StatusCode.OK) {
        this.trackingOrderItemRecord = <any>this.responce.resultData;
        this._modalService.open(modalName, { centered: true, size: 'lg', backdrop: 'static', keyboard: false });
        console.log(this.trackingOrderItemRecord);
        this.service.Loader.hide();
      }
      this.service.Loader.hide();
    });
  }
  closeModal() {
    this._modalService.dismissAll();
    this.isProceed = false;
    this.orderCancelForm.reset();
  }
}
