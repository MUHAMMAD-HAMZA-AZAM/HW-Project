import { Component, OnInit } from '@angular/core';
import { Notifications, SupplierRatingVM } from '../../../models/userModels/userModels';
import { CommonService } from '../../../shared/HttpClient/_http';
import { httpStatus, CommonErrors } from '../../../shared/Enums/enums';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  public ratings: SupplierRatingVM[] = [];
  constructor(
    public common: CommonService,
  ) { }

  ngOnInit() {
    this.PopulateData();
  }

  public PopulateData() {
    this.common.GetData(this.common.apiRoutes.Supplier.GetSuppliersFeedbackBySupplierId, true).then(result => {
      if (status = httpStatus.Ok) {
        this.ratings = result ;
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }

}
