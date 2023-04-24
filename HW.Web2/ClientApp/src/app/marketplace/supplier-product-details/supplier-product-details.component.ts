import { Component, OnInit } from '@angular/core';
import { SupplierAdVM, AdDetailsVM } from '../../models/userModels/userModels';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute, NavigationExtras, Router } from '@angular/router';
import { CommonService } from '../../shared/HttpClient/_http';
import { DomSanitizer } from '@angular/platform-browser';
import { CommonErrors } from '../../shared/Enums/enums';
import { IAdDetails, IImage, IProductCategory_Home, ISupplierAd } from '../../shared/Enums/Interface';

@Component({
  selector: 'app-supplier-product-details',
  templateUrl: './supplier-product-details.component.html',
})
export class SupplierProductDetailsComponent implements OnInit {
  public resultCategoryProduct: IProductCategory_Home;
  public supplierAd: ISupplierAd[] = [];
  public supplierid: number=0;
  public resultDetail: IAdDetails;
  public resultImage: IImage[] = [];
  public saveOrUnSaveContent: string="";
  public saveOrUnSaveCheck: boolean = false;
  public HtmlCarousal: string = '';
  public sliderLoop: any;
  public itemCount: number=0;
  public temparray: ISupplierAd[]=[];
  public ImgesCheck: boolean = false;
  public mainImage: any;
  public hasNoImage: boolean = false;
  public slideIndex: number = 1;

  constructor(public Loader: NgxSpinnerService, private route: ActivatedRoute, private common: CommonService, private sanitizer: DomSanitizer, private router: Router) {
    this.resultCategoryProduct = {} as IProductCategory_Home;
    this.resultDetail = {} as IAdDetails;
    //if (!this.common.loginCheck) {
    //  this.router.navigateByUrl('/login');
    //}
  }

  ngOnInit() {
    //if (this.common.IsUserLogIn()) {
      const id = Number(this.route.snapshot.paramMap.get('id'));
      this.PopulateData(id);
    //}

    //else
    //  this.common.NavigateToRoute(this.common.apiUrls.Login.customer);
  }

  ngAfterViewInit() {
    this.PopulateDataCategoryProducts(this.resultDetail.productId);
  }

  public PopulateData(id: number) {
    
    this.common.GetData(this.common.apiRoutes.Users.MarketPlace.SupplierAdDetail + "?supplierAdId=" + id, true).then(data => {
      this.resultDetail = data ;
      this.supplierid = this.resultDetail.supplierId;
      //this.PopulateDataCategoryProducts(this.resultDetail.productId);
      if (this.resultDetail.isSaved == true) {
        this.saveOrUnSaveContent = "  UnSave"
        this.saveOrUnSaveCheck = this.resultDetail.isSaved;
        this.resultImage = [];
      }

      else {
        this.saveOrUnSaveContent = "  Save"
        this.saveOrUnSaveCheck = this.resultDetail.isSaved;
        this.resultImage = [];
      }

      if (this.resultDetail.imageIds.length > 0) {
        this.LoadMediaImages(id);
        //this.LoadMediaVideo(id);
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });

  }

  public LoadMediaImages(id: number): void {
    this.common.GetData(this.common.apiRoutes.Users.MarketPlace.LoadImage + "?supplierAdsId=" + id).then(data => {
      this.resultImage = data ;
      if (this.resultImage.length > 0) {
        //this.ImgesCheck = true;
        this.mainImage = this.transform(this.resultImage[0].imageContent);
      }

    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });

  }

  public transform(base64Image: string) {
    return this.sanitizer.bypassSecurityTrustResourceUrl('data:image/png;base64,' + base64Image);
  }

  public SaveAd(el: boolean, supplierAdId: number) {
    var check = false;
    var information = "";
    if (el == false) {
      check = true;
      information = "Un Save";
    }
    else {
      check = false;
      information = "Save"
    }
    this.SaveOrUnSave(supplierAdId, check, information);
  }

  public SaveOrUnSave(id: number, isSaved: boolean, information: string): void {
    this.common.GetData(this.common.apiRoutes.Users.MarketPlace.SaveOrUnsave + "?supplierAdId=" + id + "&isSaved=" + isSaved, true).then(data => {
      this.saveOrUnSaveCheck = isSaved;
      this.saveOrUnSaveContent = information;
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    })
  }

  public PopulateDataCategoryProducts(productCategoryId: number) {
    this.Loader.show();

    this.common.GetData(this.common.apiRoutes.Users.MarketPlace.ProductCategory + "?productCategoryId=" + productCategoryId, true).then(data => {
      this.resultCategoryProduct = data ;
      this.supplierAd = this.resultCategoryProduct.supplierAd;
      
      this.sliderLoop = Array(Math.ceil(this.supplierAd.length / 3)).fill(0).map((x, i) => i);
      this.itemCount = this.supplierAd.length;
      this.supplierAd = [];
      var i, j, chunk = 3;
      for (i = 0, j = this.resultCategoryProduct.supplierAd.length; i < j; i += chunk) {
        this.temparray = this.resultCategoryProduct.supplierAd.slice(i, i + chunk);
        this.supplierAd.push.apply(this.temparray);
      }
      this.Loader.hide();

    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    })
  }

  public showdetail(adId: NavigationExtras) {
    this.common.NavigateToRoute(this.common.apiUrls.User.AdDetail, adId);
    this.PopulateData(Number(adId));
  }


  public plusSlides(n: number) {
    this.showSlides(this.slideIndex += n);
  }

  public currentSlide(n: number) {
    this.showSlides(this.slideIndex = n);
  }

  public showSlides(n: number) {
    
    var slides = document.getElementsByClassName("lightboxImg");

    if (n > slides.length) { this.slideIndex = 1 }
    if (n < 1) { this.slideIndex = slides.length }

    let img: any = slides[this.slideIndex - 1];
    this.mainImage = img.src;
  }
}

