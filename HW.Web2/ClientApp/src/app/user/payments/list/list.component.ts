import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';
import { httpStatus, CommonErrors } from '../../../shared/Enums/enums';
import { UserPaymentInformation } from '../../../models/userModels/userModels';

@Component({
  selector: 'app-payments-information',
  templateUrl: './list.component.html',
})
export class PaymentsListComponent implements OnInit {
  public payment: UserPaymentInformation[] = [];
  public notFound: boolean = false;
  constructor(
    public common: CommonService,
  ) {
 }

  ngOnInit() {
    this.PopulateData();
  }

  public PopulateData() {
    this.common.GetData(this.common.apiRoutes.Users.GetPaymentRecords, true).then(result => {
      if (status = httpStatus.Ok) {
        this.payment = result;
        if (this.payment.length > 0) {
          this.notFound = false;
        }
        else {
          this.notFound = true;
        }
      }
    }, error => {
      console.log(error);
      this.common.Notification.error(CommonErrors.commonErrorMessage);
    });
  }

}
