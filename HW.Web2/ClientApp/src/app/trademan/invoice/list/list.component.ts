import { Component, OnInit } from '@angular/core';
import { UserPaymentInformation } from '../../../models/userModels/userModels';
import { CommonService } from '../../../shared/HttpClient/_http';
import { httpStatus } from '../../../shared/Enums/enums';
import { TradesmanPayments } from '../../../models/tradesmanModels/tradesmanModels';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class InvoiceListComponent implements OnInit {

  public payment: TradesmanPayments[] = [];
  public notFound: boolean = false;
  constructor(
    public common: CommonService,
  ) { }

  ngOnInit() {
    this.PopulateData();
  }

  public PopulateData() {
    this.common.GetData(this.common.apiRoutes.Tradesman.GetInvoiceJobReceipts, true).then(result => {
      if (status = httpStatus.Ok) {
          this.payment = <TradesmanPayments[]>result;
        if (this.payment.length > 0) {
          console.log(this.payment);
          this.notFound = false;
        }
        else {
          this.notFound = true;
        }
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    },
      error => {
        console.log(error);
      });
  }

}
