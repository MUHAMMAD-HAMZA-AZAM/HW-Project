import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../shared/HttpClient/_http';
declare var $: any;
import { OwlOptions } from 'ngx-owl-carousel-o';
import { IProductCategory } from '../../shared/Enums/Interface';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public productList: IProductCategory[] = [];
  public activeProductList: IProductCategory[] = [];
  public sliderLoaded: boolean = false;
  customOptions: OwlOptions = {
    mouseDrag: false,
    touchDrag: false,
    pullDrag: false,
    autoplay: true,
    margin: 10,
    autoplayHoverPause: true,
    loop: true,
    navSpeed: 700,
    navText: [
      "<i class='fa fa-chevron-left'></i>",
      "<i class='fa fa-chevron-right'></i>"
    ],
    dots: false,
    responsive: {
      0: {
        items: 1
      },
      400: {
        items: 2
      },
      740: {
        items: 3
      },
      940: {
        items: 3
      }
    },
    nav: true
  }
  constructor(public common: CommonService) {

  }

  ngOnInit() {
  }
  ngAfterViewInit() {
    this.populateProducts();
    //this.marketPlaceCategory();
  }
  public populateProducts() {
    this.common.get(this.common.apiRoutes.Supplier.GetActiveProducts + "?productCategoryId=" + 0).subscribe(result => {

      this.productList = <IProductCategory[]>result ;
      this.activeProductList = this.productList.filter(p=> p.isActive);
      this.sliderLoaded = true;
    });
  }
  public Blogs(blog: string) {
    if (blog == 'BlogDetails') {
      this.common.NavigateToRoute(this.common.apiUrls.Supplier.BlogDetails)
    }
    else if (blog == 'BlogDetails1') {
      this.common.NavigateToRoute(this.common.apiUrls.Supplier.BlogDetails1)
    }
    else if (blog == 'BlogDetails2') {
      this.common.NavigateToRoute(this.common.apiUrls.Supplier.BlogDetails2)
    }
  }
  PostAd(categoryId: number) {
    
    this.common.NavigateToRouteWithQueryString(this.common.apiRoutes.Supplier.PostAd, { queryParams: { CategoryId: categoryId } })
  }
  public marketPlaceCategory() {
    setTimeout(() => {
    var dl =  $("#rmp-posts").owlCarousel({
        items: 3,
        autoplay: true,
        margin: 10,
        autoplayHoverPause: true,
        loop: true,
        nav: true,
        navText: [
          "<i class='fa fa-chevron-left'></i>",
          "<i class='fa fa-chevron-right'></i>"
        ],
        dots: false,
        responsiveClass: true,
        responsive: {
          0: {
            items: 1,
          },
          575: {
            items: 2,
          },
          767: {
            items: 2,
          },
          991: {
            items: 3,
          }
        }

      });
    }, 2000)
  }

}
