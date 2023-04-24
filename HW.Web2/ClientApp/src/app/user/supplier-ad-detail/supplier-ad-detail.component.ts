import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationExtras, Router } from '@angular/router';
import { CommonService } from '../../shared/HttpClient/_http';
import { DomSanitizer } from '@angular/platform-browser';
import { SupplierAdVM, AdDetailsVM } from '../../models/userModels/userModels';
import { NgxSpinnerService } from 'ngx-spinner';
import { CommonErrors, httpStatus } from '../../shared/Enums/enums';
import { IImage } from '../../shared/Enums/Interface';

@Component({
  selector: 'app-supplier-ad-detail',
  templateUrl: './supplier-ad-detail.component.html',
  styleUrls: ['./supplier-ad-detail.component.css']
})
export class SupplierAdDetailComponent implements OnInit {
  public resultCategoryProduct: any;
  public supplierAd: SupplierAdVM[] = [];
  public supplierid: number | undefined = 0;
  public resultDetail: AdDetailsVM = {} as  AdDetailsVM;
  public resultImage: IImage[]=[];
  public saveOrUnSaveContent: string = "";
  public saveOrUnSaveCheck: boolean | undefined = false;
  public HtmlCarousal: string = '';
  public sliderLoop: any;
  public itemCount: number=0;
  public temparray :any;
  public ImgesCheck: boolean = false;
  public mainImage: any;
  public hasNoImage: boolean = false;
  public slideIndex: number = 1;

  constructor(public Loader: NgxSpinnerService, private route: ActivatedRoute, private common: CommonService, private sanitizer: DomSanitizer, private router: Router) {

    //if (!this.common.loginCheck) {
    //  this.router.navigateByUrl('/login');
    //}
  }

  ngOnInit() {
    if (this.common.IsUserLogIn()) {
      const id = Number(this.route.snapshot.paramMap.get('id'));
      this.PopulateData(id);
    }

    else
      this.common.NavigateToRoute(this.common.apiUrls.Login.customer);
  }

  ngAfterViewInit() {
    this.PopulateDataCategoryProducts(this.resultDetail?.productId);
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
      let length = this.resultDetail?.imageIds?.length ? this.resultDetail?.imageIds?.length : 0;
      if (length > 0) {
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
        
        //this.mainImage = this.transform(this.resultImage[0].imageContent);
        this.mainImage = this.transform(this.resultImage[0].imageContent[0]);
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
    var information:string = "";
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

  public PopulateDataCategoryProducts(productCategoryId: number | undefined) {
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
        this.supplierAd.push(this.temparray);
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
