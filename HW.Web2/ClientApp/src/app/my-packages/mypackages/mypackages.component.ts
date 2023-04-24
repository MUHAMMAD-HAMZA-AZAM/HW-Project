import { Component, OnInit } from '@angular/core';
import { IActiveOrders } from '../../shared/Enums/Interface';
import { CommonService } from '../../shared/HttpClient/_http';

@Component({
  selector: 'app-mypackages',
  templateUrl: './mypackages.component.html',
  styleUrls: ['./mypackages.component.css']
})
export class MypackagesComponent implements OnInit {
  public activeOrders: IActiveOrders[] = [];
  public expiredOrders: IActiveOrders[] = [];
  public pageSize = 50;
  public pageNumber = 1;
  public noActiveOrderFound: string = "";
  public noExpiredPkgFound: string = "";

  constructor(public common: CommonService,) {
  }

  ngOnInit() {
    this.getActiveOrders();
    this.getExpirdeOrders();
  }

  public getActiveOrders() {
    
    this.common.get(this.common.apiRoutes.PackagesAndPayments.GetActiveOrdersList + "?pageSize=" + this.pageSize + "&pageNumber=" + this.pageNumber).subscribe(respone => {
      let res = respone ;
      this.activeOrders = <IActiveOrders[]>res;
      //if (this.activeOrders.length < 0) {
      //  this.noActiveOrderFound = "No package found buy one!";
      //}
    });

  }
  public getExpirdeOrders() {
    this.common.GetData(this.common.apiRoutes.PackagesAndPayments.GetExpiredOrdersList + "?pageSize=" + this.pageSize + "&pageNumber=" + this.pageNumber, true).then(respone => {
      this.expiredOrders = respone ;
      //if (this.expiredOrders.length < 0) {
      //  this.noExpiredPkgFound = "You have not any Epxired package";

      //}
    })

  }
}
