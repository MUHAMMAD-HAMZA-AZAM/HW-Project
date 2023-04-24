import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { CommonService } from '../../../shared/HttpClient/_http';
import { httpStatus, CommonErrors } from '../../../shared/Enums/enums';
import { InProgressJobDetails } from '../../../models/userModels/userModels';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { IInProgressJobDetails } from '../../../shared/Enums/Interface';


@Component({
  selector: 'app-in-progrees-job-details',
  templateUrl: './detail.component.html',
})
export class InProgressDetailComponent implements OnInit {
  public jobQuotationId: number=0;
  public inProgressJobImage: any;
  public imageContent: string="";
  //public inProgressJobDetail: InProgressJobDetails = new InProgressJobDetails();
  public inProgressJobDetail: IInProgressJobDetails ;
  public ImageCheck = false;
  public hasNoImage: boolean = false;
  public showSubCategory: boolean = true;
  public slideIndex: number = 1;
  @ViewChild('ImageModel', { static: true }) ImageModel: ModalDirective;

  constructor(
    private route: ActivatedRoute,
    public common: CommonService,
  ) {
    this.inProgressJobDetail = {} as IInProgressJobDetails;
    this.ImageModel = {} as ModalDirective;
 }

  ngOnInit() {
    this.route.queryParams.subscribe((params: Params) => {
      this.jobQuotationId = params['jobQuotationId'];
      if (this.jobQuotationId > 0)
        this.PopulateData();
    });
  }

  public PopulateData() {
    
    var urls = this.common.apiRoutes.Jobs.GetInprogressJobDetail + "?jobQuotationId=" + this.jobQuotationId;
    this.common.GetData(urls, true).then(result => {
      if (status = httpStatus.Ok) {
        this.inProgressJobDetail = result ;
        if (this.inProgressJobDetail.subCatagoryName == null || this.inProgressJobDetail.subCatagoryName == '') {
          this.showSubCategory = false;
        }
        console.log(this.inProgressJobDetail);
        this.common.GetData(this.common.apiRoutes.Jobs.GetQuoteImagesByJobQuotationIdWeb + "?jobQuotationId=" + this.inProgressJobDetail.jobQuotationId, true).then(result => {
          this.inProgressJobImage = result;
          if (this.inProgressJobImage != null) {
            for (var i = 0; i < this.inProgressJobImage.length; i++) {
              this.ImageCheck = true;
              if (this.inProgressJobImage[i].imageContent != null) {
                var imgContent = 'data:image/png;base64,' + this.inProgressJobImage[i].imageContent;
                this.inProgressJobImage[i].imageContent = imgContent.toString();
              }
            }
          }
          this.inProgressJobDetail.imageList = this.inProgressJobImage;
        }, error => {
          console.log(error);
          this.common.Notification.error(CommonErrors.commonErrorMessage);
        })
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }

  public ImagePopUp(image: string) {
    this.imageContent = image;
    this.ImageModel.show()

  }

  public ViewProfile(tradesmanId: number) {
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.TradesmanProfile, { queryParams: { tradesmanId: tradesmanId } });

  }

  

  public Close() {
    this.ImageModel.hide();
  }

  public openModal() {
    const element = document.getElementById("myModalLightbox");
    if (element != null) {
      element.style.display = "block";
    }
  }

  public closeModal() {
    const element = document.getElementById("myModalLightbox");
    if (element != null) {
      element.style.display = "none";
    }
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
    this.imageContent = img.src;
  }

}
