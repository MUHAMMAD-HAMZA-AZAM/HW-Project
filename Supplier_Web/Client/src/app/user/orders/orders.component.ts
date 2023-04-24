import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, FormGroupName, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ICustomerOrderItems, IPageSeoVM, IDetails } from '../../Shared/Enums/Interface';
import { AspnetRoles, AspnetUserRoles, IResponseVM, OrderStatus, ResponseVm, StatusCode, TrackingStatusType } from '../../Shared/Enums/enum';
import { CommonService } from '../../Shared/HttpClient/HttpClient';
import { UploadFileService } from '../../Shared/HttpClient/upload-file.service';
import { metaTagsService } from '../../Shared/HttpClient/seo-dynamic.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
  public appValForm: FormGroup;
  public activePageNumber = 1;
  public pageSize = 10000;
  public userId: string = "";
  public customerId: number = 0;
  public totalCustomerOrders: number = 0;
  public ordersList: ICustomerOrderItems[] = [];
  public customerOrdersList = new Array();
  public ordersBySupplier = new Array();
  public orderCancellationReasonsList: any = [];
  public userRole: string = "";
  public Orders = OrderStatus;
  public dataNotFound: boolean = false;
  public orderId: number = 0;
  public trackingRecord: IDetails[] = [];
  public response: IResponseVM;
  constructor(
    public _httpService: CommonService,
    private router: ActivatedRoute,
    public Loader: NgxSpinnerService,
    private toastr: ToastrService,
    public _fileService: UploadFileService,
    private _modalService: NgbModal,
    public fb: FormBuilder,
    private _metaService: metaTagsService,

  ) {
    this.response = {} as IResponseVM;
    this.appValForm = {} as FormGroup
  }

  ngOnInit(): void {

    var decodedtoken = this._httpService.decodedToken();
    this.customerId = decodedtoken.Id;
    this.userId = decodedtoken.UserId;
    this.userRole = decodedtoken.Role;
    this.getLoggedUserOrdersList();
    //----- Customer Reason Form
    this.appValForm = this.fb.group({
      reasonId: [null, [Validators.required]],
      comments: ['', [Validators.required]],
      cancellationPolicy: [true, [Validators.required]]
    });
    this.bindMetaTags();
  }

  get f() {
    return this.appValForm.controls;
  }

  //------------- Show Logged User Orders List
  public getLoggedUserOrdersList() {
    this.activePageNumber = 1;
    let obj = {
      pageNumber: this.activePageNumber,
      pageSize: this.pageSize,
      customerId: this.customerId
    };
    this._httpService.PostData(this._httpService.apiUrls.Customer.Order.GetLoggedCustomerOrdersList, JSON.stringify(obj), true).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        this.ordersList = <any>this.response.resultData;
        if (this.ordersList) {
          this.ordersList.forEach(item => {

            if (!this.ordersBySupplier.some(x => x.orderId == item.orderId)) {
              let checkOrderStatus = this.ordersList.filter(y => y.orderId == item.orderId)
                .some(m => m.orderStatus == this.Orders.PackedAndShipped);
              if (checkOrderStatus) {
                item.orderStatusName = 'Packed&Shipped';
                item.orderStatus = this.Orders.PackedAndShipped;
              }

              let obj = {
                noOfRecords: item.noOfRecords,
                orderId: item.orderId,
                customerId: item.customerId,
                orderDate: item.orderDate,
                quantity: item.quantity,
                orderStatus: item.orderStatus,
                customerName: item.customerName,
                payableAmount: item.payableAmount,
                contactNumber: item.contactNumber,
                shippingAdress: item.shippingAddress,
                shippingCost: item.shippingCost,
                productId: item.productId,
                slug: item.slug,
                supplierId: item.supplierId,
                supplierName: item.supplierName,
                isPaymentReceived: item.isPaymentReceived,
                orderStatusName: item.orderStatusName,
                isPaymentModeConfirm: item.isPaymentModeConfirm
              }
              this.ordersBySupplier.push(obj)
            }
          });
          this.customerOrdersList = this.ordersBySupplier;
          console.log(this.customerOrdersList);
          this.totalCustomerOrders = this.ordersBySupplier[0].noOfRecords;
        }
        else {
          this.dataNotFound = true;
        }

      }

    }, error => {
      console.log(error);
    });
  }

  //------------- Show Orders List By Active Page from Pagination
  public getOrderListByPage(page: number) {

    this.activePageNumber = page;
    let obj = {
      pageNumber: this.activePageNumber,
      pageSize: this.pageSize,
      customerId: this.customerId
    };
    this._httpService.PostData(this._httpService.apiUrls.Customer.Order.GetLoggedCustomerOrdersList, JSON.stringify(obj), true).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        this.ordersList = <any>this.response.resultData;
        this.ordersList.forEach(item => {
          if (!this.ordersBySupplier.some(x => x.orderId == item.orderId)) {
            let obj = {
              noOfRecords: item.noOfRecords,
              orderId: item.orderId,
              customerId: item.customerId,
              orderDate: item.orderDate,
              quantity: item.quantity,
              customerName: item.customerName,
              orderFrom: item.orderFrom,
              price: item.price,
              actualPrice: item.actualPrice,
              discountedAmount: item.discountedAmount,
              promotionAmount: item.promotionAmount,
              payableAmount: item.payableAmount,
              contactNumber: item.contactNumber,
              shippingAdress: item.shippingAddress,
              productId: item.productId,
              slug: item.slug,
              supplierId: item.supplierId,

            }
            this.ordersBySupplier.push(obj)
          }
        })
        this.customerOrdersList = this.ordersBySupplier;
        this.totalCustomerOrders = this.ordersBySupplier[0].noOfRecords;
      }
      else {

      }

    }, error => {
      console.log(error);
    });
  }

  //------------- Show Customer OrderedProduct Details
  public orderDetails(orderId: number) {
    this._httpService.NavigateToRouteWithQueryString(this._httpService.apiRoutes.User.OrderDetails, { queryParams: { orderId: orderId } });
  }

  //------------- Show Order CancellationReasons List
  public getOrderCancellationReasonsList() {
    this._httpService.GetData1(this._httpService.apiUrls.Customer.Order.GetOrderCancellationReasonsList + `?userRole=${AspnetRoles.CRole}`).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        this.orderCancellationReasonsList = this.response.resultData;
      }
    }, error => {
      console.log(error);
    });
  }
  //------------- Show Cancel Order Modal
  public showModal(orderId: number, orderCancelModal: TemplateRef<any>) {
    // get Order Cancellation reasons
    this.getOrderCancellationReasonsList();
    this._modalService.open(orderCancelModal, { centered: true, size: 'lg', backdrop: 'static', keyboard: false });
    this.orderId = orderId;
  }

  //------------- Cancel Customer Order
  public cancelOrder(): void {
    if (this.appValForm.invalid) {
      return this.appValForm.markAllAsTouched();
    }
    let formData = this.appValForm.value;
    formData.orderId = this.orderId;
    formData.orderStatus = this.Orders.Cancelled;
    formData.reasonId = formData?.reasonId ? parseInt(formData.reasonId) : 0;
    formData.createdBy = this.userId;
    formData.userRole = parseInt(AspnetRoles.CRole);

    console.log(formData);
    this._httpService.PostData(this._httpService.apiUrls.Customer.Order.CancelCustomerOrder, JSON.stringify(formData)).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        this._modalService.dismissAll();
        this.customerOrdersList = this.customerOrdersList.filter(x => x.orderId != this.orderId);
        this.toastr.warning(this.response.message, "Alert !");
        this.appValForm.reset();
        this.Loader.hide();
      }

    }, error => {
      console.log(error);
    });
  }

   //------------- Show Customer  Order Track Details 
  public orderTrack(orderId: number) {
    this._httpService.NavigateToRouteWithQueryString(this._httpService.apiRoutes.User.OrderTrack, { queryParams: { orderId: orderId } });
  }

  //------------- Close Confirmation Modal
  public closeModal(): void {
    this._modalService.dismissAll();
    this.appValForm.reset();

  }

  public bindMetaTags() {
    this._httpService.get(this._httpService.apiUrls.CMS.GetSeoPageById + "?pageId=31").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response?.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description, res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }
}
