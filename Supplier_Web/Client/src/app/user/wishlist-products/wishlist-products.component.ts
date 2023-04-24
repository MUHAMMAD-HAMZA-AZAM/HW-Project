import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IResponseVM, ResponseVm, StatusCode, wishListBasket } from '../../Shared/Enums/enum';
import { IPageSeoVM, IProductsWishList } from '../../Shared/Enums/Interface';
import { CommonService } from '../../Shared/HttpClient/HttpClient';
import { metaTagsService } from '../../Shared/HttpClient/seo-dynamic.service';
import { UploadFileService } from '../../Shared/HttpClient/upload-file.service';

@Component({
  selector: 'app-wishlist-products',
  templateUrl: './wishlist-products.component.html',
  styleUrls: ['./wishlist-products.component.css']
})
export class WishlistProductsComponent implements OnInit {
  public activePageNumber:number = 1;
  public pageSize:number = 8;
  public noDataFound: boolean = false;
  public userId: string="";
  public customerId: number=0;
  public totalCustomerProducts: number=0;
  public wishlistProducts:  IProductsWishList[] = [];
  //public favouriteProductDetail: any;
  public wishListItem: any = wishListBasket;
  public response: IResponseVM;
  constructor(
    public _httpService: CommonService,
    private router: ActivatedRoute,
    private toastr: ToastrService,
    public _fileService: UploadFileService,
    private _metaService: metaTagsService
  ) { }

  ngOnInit(): void {
    var decodedtoken = this._httpService.decodedToken();
    this.customerId = decodedtoken.Id;
    this.userId = decodedtoken.UserId;
    this.getLoggedUserWishListProducts();
    this.bindMetaTags()
  }

    //------------- Show Logged User Wishlist Products
  public getLoggedUserWishListProducts() {
    this.activePageNumber = 1;
    this._httpService.GetData1(this._httpService.apiUrls.Customer.GetCustomerWishListProducts + "?customerId=" + this.customerId + "&pageNumber=" + this.activePageNumber + "&pageSize=" + this.pageSize , true).then(res => {
      this.response = res;
      debugger;
      console.log(this.response);
      if (this.response.status == StatusCode.OK) {
        this.wishlistProducts = <any>this.response.resultData;
        console.log(this.wishlistProducts )
        if (this.wishlistProducts?.length > 0) {
          //this.wishlistProducts.forEach((x, i) => {
          //  if (x.fileName) {
          //    this._fileService.getFileByName(x.filePath + x.fileName).then(file => {
          //      this.wishlistProducts[i].fileName = file;
          //    })
          //  }
          //  else
          //    this.wishlistProducts[i].fileName = '';
          //});
          this.totalCustomerProducts = this.wishlistProducts[0].noOfRecords;
        }
        else {
          this.noDataFound = true;
        }
      }

    }, error => {
      console.log(error);
    });
  }

  //------------- Show Products List By Active Page from Pagination
  public getWishListProductByPage(page: number) {
    this.activePageNumber = page;
    this._httpService.GetData1(this._httpService.apiUrls.Customer.GetCustomerWishListProducts + "?customerId=" + this.customerId + "&pageNumber=" + this.activePageNumber + "&pageSize=" + this.pageSize, true).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        this.wishlistProducts = <any>this.response.resultData;
        console.log(this.wishlistProducts);
        if (this.wishlistProducts?.length > 0) {
          //this.wishlistProducts.forEach((x, i) => {
          //  if (x.fileName) {
          //    this._fileService.getFileByName(x.filePath + x.fileName).then(file => {
          //      this.wishlistProducts[i].fileName = file;
          //    })
          //  }
          //  else
          //    this.wishlistProducts[i].fileName = '';
          //});
          this.totalCustomerProducts = this.wishlistProducts[0].noOfRecords;
        }
      }

    }, error => {
    });
  }

  //------------- remove Product From Customer Wishlist 
  public removeProductFromWishList(recordId:number)
  {
      let obj = {
        id: recordId,
      };
      this._httpService.PostData(this._httpService.apiUrls.Customer.SaveAndRemoveProductsInWishlist, JSON.stringify(obj), true).then(res => {
        this.response = res;
        if (this.response.status == StatusCode.OK) {
          this.toastr.error("Item removed from your wishList ", "Removed");
          this.getLoggedUserWishListProducts();
        }
      }, error => {
        this._httpService.Loader.show();
      });

  }
  public bindMetaTags() {
    this._httpService.get(this._httpService.apiUrls.CMS.GetSeoPageById + "?pageId=32").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description, res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }
}
