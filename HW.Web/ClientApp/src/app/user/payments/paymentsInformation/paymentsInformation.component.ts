import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';
import { httpStatus } from '../../../shared/Enums/enums';
import { UserPaymentInformation } from '../../../models/userModels/userModels';

@Component({
  selector: 'app-payments-information',
  templateUrl: './paymentsInformation.component.html',
})
export class PaymentsInformationComponent implements OnInit {
  public payment: UserPaymentInformation[] =[];
  constructor(
    public common: CommonService,
  ) { }

  ngOnInit() {
    this.PopulateData();
  }

  public PopulateData() {
    debugger
    this.common.GetData(this.common.apiRoutes.Users.GetPaymentRecords, true).then(result => {
      debugger;
      if (status = httpStatus.Ok) {
        this.payment = result.json();
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    });
  }

}
