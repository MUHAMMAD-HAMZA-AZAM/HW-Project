import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { CommonService } from '../../../shared/HttpClient/_http';
import { httpStatus } from '../../../shared/Enums/enums';
import { InProgressJobDetails } from '../../../models/userModels/userModels';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-in-progrees-job-details',
  templateUrl: './inProgreesJobDetails.component.html',
})
export class InProgreesJobDetailsComponent implements OnInit {
  public jobQuotationId: number;
  public inProgressJobImage: any;
  public imageContent: string;
  public inProgressJobDetail: InProgressJobDetails = new InProgressJobDetails();

  @ViewChild('ImageModel', { static: true }) ImageModel: ModalDirective;

  constructor(
    private route: ActivatedRoute,
    public common: CommonService,
  ) { }

  ngOnInit() {
    debugger
    this.route.queryParams.subscribe((params: Params) => {
      this.jobQuotationId = params['jobQuotationId'];
      if (this.jobQuotationId > 0)
        this.PopulateData();
    });
  }
  public PopulateData() {
    debugger
    var urls = this.common.apiRoutes.Jobs.GetInprogressJobDetail + "?jobQuotationId=" + this.jobQuotationId;
    this.common.GetData(urls, true).then(result => {
      debugger;
      if (status = httpStatus.Ok) {
        this.inProgressJobDetail = result.json();
        this.common.GetData(this.common.apiRoutes.Jobs.GetQuoteImagesByJobQuotationIdWeb + "?jobQuotationId=" + this.inProgressJobDetail.jobQuotationId, true).then(result => {
          this.inProgressJobImage = result.json();
          for (var i = 0; i < this.inProgressJobImage.length; i++) {
            if (this.inProgressJobImage[i].imageContent != null) {
              var imgContent = 'data:image/png;base64,' + this.inProgressJobImage[i].imageContent;
              this.inProgressJobImage[i].imageContent = imgContent.toString();
            }
          }
          debugger
          this.inProgressJobDetail.imageList = this.inProgressJobImage;
        })



      }
      else {
        this.common.Notification.error("Some Thing Went Wrong");
      }
    });
  }
  public ImagePopUp(image) {
    debugger
    this.imageContent = image;
    this.ImageModel.show()

  }
  public ViewProfile(tradesmanId) {
    debugger
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.User.TradesmanProfile, { queryParams: { tradesmanId: tradesmanId } });

  }


  public Close() {
    this.ImageModel.hide();
  }
}
