import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CommonService } from '../../Shared/HttpClient/_http';
import { IWithDrawalRequestList } from '../../Shared/Enums/Interface';

@Component({
  selector: 'app-withdrawallist',
  templateUrl: './withdrawallist.component.html',
  styleUrls: ['./withdrawallist.component.css']
})
export class WithdrawallistComponent implements OnInit {

jwtHelperService: JwtHelperService = new JwtHelperService();
  public pageNumber: number = 1;
  public decodedToken: any="";
  public userId: string="";
  public dataNotfond: boolean=false;
  public pageSize: number = 50;
  public noOfRecords:number = 0;
  public withdrawalRequestList : IWithDrawalRequestList[]=[];
  public totalWithDrawAmount =0;
  constructor(public service: CommonService) { }

  ngOnInit(): void {
    this.getWithDrawalRequestList(); 
  }

getWithDrawalRequestList() {
    var data = localStorage.getItem('auth_token');
    this.decodedToken = data != null ? this.jwtHelperService.decodeToken(data) : this.decodedToken;
    this.service.GetData(this.service.apiUrls.Supplier.Payment.GetWithdrawalListById + "?Id=" + this.decodedToken.Id).then(response => {
      this.withdrawalRequestList = (<any>response).resultData;
debugger;
    if( this.withdrawalRequestList){
        this.dataNotfond=false;
        this.totalWithDrawAmount = this.withdrawalRequestList[0].totalWithDrawAmount;
    }
    else{
      this.dataNotfond=true;
    }
    })
  }

}
