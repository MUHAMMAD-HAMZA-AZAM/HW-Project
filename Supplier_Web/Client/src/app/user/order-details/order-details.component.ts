import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AspnetUserRoles, IResponseVM, OrderStatus, ResponseVm, StatusCode, TrackingStatusType } from '../../Shared/Enums/enum';
import { ICustomerOrderItems, IPageSeoVM, IDetails } from '../../Shared/Enums/Interface';
import { CommonService } from '../../Shared/HttpClient/HttpClient';
import { UploadFileService } from '../../Shared/HttpClient/upload-file.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { metaTagsService } from '../../Shared/HttpClient/seo-dynamic.service';
import { IDetail } from '../../Shared/Interfaces/OrderTrack/Interface';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
})
export class OrderDetailsComponent implements OnInit {
  public trackingNumber: number | undefined = 0;
  public activePageNumber = 1;
  public pageSize: number = 100000;
  public orderId: number = 0;
  public supplierId: number = 0;
  //public totalOrderedProductsList: any;
  public customerOrderedProductsList: ICustomerOrderItems[] = [];
  public orderedProductsList: ICustomerOrderItems[] = [];
  public Orders = OrderStatus;
  public ordersByProducts = new Array();
  public response: ResponseVm = new ResponseVm();
  public res: IResponseVM = {} as IResponseVM;
  public trackingRecord: IDetails = {} as IDetails;
  constructor(
    public _httpService: CommonService,
    private router: ActivatedRoute,
    private toastr: ToastrService,
    public _fileService: UploadFileService,
    private _metaService: metaTagsService,
    public _modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.router.queryParams.subscribe(params => {
      this.orderId = params['orderId'];

      //  this.supplierId = params['supplierId'];
      this.getLoggedUserOrderedProductsList();
    });
    this.bindMetaTags();
  }
  orderTracking(itemId: number, template: TemplateRef<any>) {
    this._modalService.open(template, { centered: true, size: 'xl' });
  }
  //------------- Show Logged User Orders List
  public getLoggedUserOrderedProductsList() {
    this.activePageNumber = 1;
    let obj = {
      pageNumber: this.activePageNumber,
      pageSize: this.pageSize,
      orderId: this.orderId
    };
    this._httpService.PostData(this._httpService.apiUrls.Customer.Order.GetLoggedCustomerOrderedProductsList, JSON.stringify(obj), true).then(result => {
      this.res = result;
      if (this.res.status == StatusCode.OK) {
        this.orderedProductsList = <any>this.res.resultData;
        this.orderedProductsList.forEach(item => {
          if (!this.ordersByProducts.some(x => x.productId == item.productId && x.variantId == item.variantId)) {
            let obj = {
              noOfRecords: item.noOfRecords,
              orderId: item.orderId,
              trackingNumber: item.trackingNumber,
              customerId: item.customerId,
              orderDate: item.orderDate,
              quantity: item.quantity,
              orderStatus: item.orderStatus,
              orderStatusName: item.orderStatusName,
              customerName: item.customerName,
              price: item.price,
              actualPrice: item.actualPrice,
              discountedAmount: item.discountedAmount,
              promotionAmount: item.promotionAmount,
              payableAmount: item.payableAmount,
              contactNumber: item.contactNumber,
              shippingAdress: item.shippingAddress,
              shippingCost: item.shippingCost,
              productId: item.productId,
              productName: item.productName,
              varientColour: item.varientColour,
              slug: item.slug,
              supplierId: item.supplierId,
              supplierName: item.supplierName
            }
            this.ordersByProducts.push(obj)
          }
        })
        this.customerOrderedProductsList = this.ordersByProducts;
        console.log(this.customerOrderedProductsList)
      }

    }, error => {
      console.log(error);
    });
  }

   //------------- Show Customer  Order Item Track Details 
  public orderItemTrack(trackId: string) {
    this.trackingNumber = Number(trackId);
    this._httpService.NavigateToRouteWithQueryString(this._httpService.apiRoutes.User.OrderTrack, { queryParams: { trackId: this.trackingNumber } });
  }

  public bindMetaTags() {
    this._httpService.get(this._httpService.apiUrls.CMS.GetSeoPageById + "?pageId=28").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description, res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }
}
