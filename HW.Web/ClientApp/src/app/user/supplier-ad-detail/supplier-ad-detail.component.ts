import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonService } from '../../shared/HttpClient/_http';
import { DomSanitizer } from '@angular/platform-browser';
import { SupplierAdVM } from '../../models/userModels/userModels';
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';

@Component({
  selector: 'app-supplier-ad-detail',
  templateUrl: './supplier-ad-detail.component.html',
  styleUrls: ['./supplier-ad-detail.component.css']
})
export class SupplierAdDetailComponent implements OnInit {
  public resultCategoryProduct: any;
  public supplierAd: SupplierAdVM[] = [];
  public supplierid;
  public resultDetail: any;
  public resultImage: any;
  public saveOrUnSaveContent: any;
  public saveOrUnSaveCheck: boolean;
  HtmlCarousal: string = '';
  sliderLoop: any;
  temparray;

    //SupplierAdVM[] = [];
  //CarousalHtml: string = '<div class="carousel-item active"><div class="row"><div class="col-lg-4 col-sm-6 product-item"><div class="card h-100" ><a href="marketplace-store.html" > <img class="card-img-top" src = "../../../assets/img/pro-dtl1.jpg" alt = "" > </a><div class="card-body" ><h5 class="card-title" ><a href="#.">Prodect Eight</a></h5><p class="card-supplier"><a href="marketplace-brand.html">Can Furniture</a></p><h4 class="card-price"><a href="#.">15,200</a></h4></div></div></div></div></div>';

  constructor(public Loader: Ng4LoadingSpinnerService, private route: ActivatedRoute, private common: CommonService, private sanitizer: DomSanitizer, private router: Router) {
    
    //if (!this.common.loginCheck) {
    //  this.router.navigateByUrl('/login');
    //}
  }

  ngOnInit() {
    debugger;
    if (this.common.IsUserLogIn())
      this.PopulateData();
    else
      this.common.NavigateToRoute(this.common.apiUrls.Login.login);
  }
  ngAfterViewInit() {
    this.PopulateDataCategoryProducts(this.resultDetail.productId);
  }

  PopulateData() {
    debugger;
    const id = +this.route.snapshot.paramMap.get('id');
    this.common.GetData(this.common.apiRoutes.Users.MarketPlace.SupplierAdDetail + "?supplierAdId=" + id, true).then(data => {
      this.resultDetail = data.json();
      this.supplierid = this.resultDetail.supplierId;
      //this.PopulateDataCategoryProducts(this.resultDetail.productId);
      if (this.resultDetail.isSaved == true) {
        this.saveOrUnSaveContent = "Un Save"
        this.saveOrUnSaveCheck = this.resultDetail.isSaved;
      }

      else {
        this.saveOrUnSaveContent = "Save"
        this.saveOrUnSaveCheck = this.resultDetail.isSaved;
      }

      if (this.resultDetail.imageIds.length > 0) {
        this.LoadMediaImages(id);
        //this.LoadMediaVideo(id);
      }
    })

  }
  LoadMediaImages(id: any): void {
    this.Loader.show();

    this.common.get(this.common.apiRoutes.Users.MarketPlace.LoadImage + "?supplierAdsId=" + id).subscribe(data => {
      this.resultImage = data.json();
      this.Loader.hide();

    })

  }

  transform(base64Image) {
    return this.sanitizer.bypassSecurityTrustResourceUrl('data:image/png;base64,'+base64Image);
  }

  SaveAd(el, supplierAdId) {
    debugger;
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

  SaveOrUnSave(id: any, isSaved: any, information: any): void {
    this.common.get(this.common.apiRoutes.Users.MarketPlace.SaveOrUnsave + "?supplierAdId=" + id + "&isSaved=" + isSaved).subscribe(data => {
      debugger;
      this.saveOrUnSaveCheck = isSaved;
      this.saveOrUnSaveContent = information;
    })
  }
  PopulateDataCategoryProducts(productCategoryId) {
    this.Loader.show();

    this.common.get(this.common.apiRoutes.Users.MarketPlace.ProductCategory + "?productCategoryId=" + productCategoryId).subscribe(data => {
      debugger;
      this.resultCategoryProduct = data.json();
      this.supplierAd = this.resultCategoryProduct.supplierAd;
      this.sliderLoop = Array(Math.ceil(this.supplierAd.length / 3)).fill(0).map((x, i) => i);

      this.supplierAd = [];
      var i, j, chunk = 3;
      for (i = 0, j = this.resultCategoryProduct.supplierAd.length; i < j; i += chunk) {
        this.temparray = this.resultCategoryProduct.supplierAd.slice(i, i + chunk);
        this.supplierAd.push(this.temparray);
      }
      this.Loader.hide();

    })
  }

}
