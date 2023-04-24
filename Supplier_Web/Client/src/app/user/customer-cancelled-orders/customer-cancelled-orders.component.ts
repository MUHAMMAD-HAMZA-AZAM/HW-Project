import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { IResponseVM, OrderStatus, StatusCode } from 'src/app/Shared/Enums/enum';
import { CommonService } from 'src/app/Shared/HttpClient/HttpClient';
import { ICustomerOrderItems, ICustomerOrdersList, IPageSeoVM } from '../../Shared/Enums/Interface';
import { metaTagsService } from '../../Shared/HttpClient/seo-dynamic.service';

@Component({
  selector: 'app-customer-cancelled-orders',
  templateUrl: './customer-cancelled-orders.component.html',
  styleUrls: ['./customer-cancelled-orders.component.css']
})
export class CustomerCancelledOrdersComponent implements OnInit {
  public noDataFound: boolean = false;
  public userId: string="";
  public customerId: number=0;
  public ordersList: ICustomerOrderItems[] = [];
  public customerOrdersList: ICustomerOrdersList[]  = [];
  public ordersBySupplier = new Array();
  public Orders = OrderStatus;
  public response: IResponseVM;
  constructor(
    public _httpService: CommonService,
    private router: ActivatedRoute,
    public Loader: NgxSpinnerService,
    private toastr: ToastrService,
    private _metaService: metaTagsService,
    private _modalService: NgbModal
  ) {
    this.response = {} as IResponseVM;
  }

  ngOnInit(): void {
    var decodedtoken = this._httpService.decodedToken();
    this.customerId = decodedtoken.Id;
    this.userId = decodedtoken.UserId;
    this.getLoggedUserCancelledOrdersList();
    this.bindMetaTags();
    
  }
 //------------- Show Logged User Canceled Orders List
 public getLoggedUserCancelledOrdersList() {
  let obj = {
    customerId: this.customerId
  };
  this._httpService.PostData(this._httpService.apiUrls.Customer.Order.GetLoggedCustomerCanelledOrdersList, JSON.stringify(obj), true).then(res => {
    this.response = res;
    if (this.response.status == StatusCode.OK) {
      this.ordersList = <any>this.response.resultData;
      if(this.ordersList?.length > 0 ){
        this.ordersList.forEach(item => {
          if (!this.ordersBySupplier.some(x => x.orderId == item.orderId))
          {
            let obj: ICustomerOrdersList = {
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
              shippingCost : item.shippingCost,
              productId:item.productId,
              slug: item.slug,        
              supplierId: item.supplierId,
              supplierName: item.supplierName,
              isPaymentReceived: item.isPaymentReceived,
              orderStatusName: item.orderStatusName,
              reasonName : item.reasonName,
              comments : item.comments
            }
            this.ordersBySupplier.push(obj)
          }
        })
        this.customerOrdersList = this.ordersBySupplier;
      }
      else {
        this.noDataFound = true;
      }
    }

  }, error => {
    console.log(error);
  });
}
//------------- Show Customer OrderedProduct Details
public orderDetails(oderId:number) {
  this._httpService.NavigateToRouteWithQueryString(this._httpService.apiRoutes.User.OrderDetails, { queryParams: { orderId: oderId } });
  }

  public bindMetaTags() {
    this._httpService.get(this._httpService.apiUrls.CMS.GetSeoPageById + "?pageId=27").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description, res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }
}
