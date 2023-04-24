import { Component, OnInit } from '@angular/core';
import { Notifications, SupplierRatingVM } from '../../../models/userModels/userModels';
import { CommonService } from '../../../shared/HttpClient/_http';
import { httpStatus } from '../../../shared/Enums/enums';

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
    debugger
    this.common.GetData(this.common.apiRoutes.Supplier.GetSuppliersFeedbackBySupplierId, true).then(result => {
      debugger;
      if (status = httpStatus.Ok) {
        this.ratings = result.json();
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    });
  }

}
