import { Component, OnInit } from '@angular/core';
import { CommonService } from 'src/app/Shared/HttpClient/_http';
import { ActivatedRoute, Params } from '@angular/router';
import { SpUserProfileVM, SpBusinessProfileVM } from 'src/app/Shared/Models/PrimaryUserModel/PrimaryUserModel';
import { NgxSpinnerService } from "ngx-spinner";
//import { debug } from 'util';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {


  public userId: number;
  public role: string;
  public DistanceTitle: string;
  public subRole: string;
  public pageNumber: number;
  public pageSize: number;
  public totalPages: number;
  public list_Of_Ids: Array<number> = [];
  public listOfIds: Array<number> = [];
  public firstId: number;
  public userProfile: SpUserProfileVM = new SpUserProfileVM;
  public businessProfile: SpBusinessProfileVM = new SpBusinessProfileVM;

  constructor(private service: CommonService, private route: ActivatedRoute, public Loader: NgxSpinnerService) {
    this.route.queryParams.subscribe((params: Params) => {
      this.userId = params['userId'];
      this.role = params['userRole'];
      this.subRole = params['subRole'];
      this.list_Of_Ids = params['list'];
      this.pageNumber = params['PageNumber'];
      this.pageSize = params['PageSize'];
      this.totalPages = params['NumberofPages'];
      this.getUserProfile();
      this.GetBussinessProfile();
    });
  }
  ngOnInit() {


  }
  getUserProfile() {
  
  }
  GetBussinessProfile() {
    this.service.get(this.service.apiRoutes.Users.getBussinessProfile + "?userId=" + this.userId + "&role=" + this.role).subscribe(result => {
      this.businessProfile = result.json();
      if (this.businessProfile.tradeName != null) {
        this.businessProfile.tradeName = this.businessProfile.tradeName.split("&amp;").join(" ");
      }
      this.Loader.hide();

    },
      error => {
        console.log(error);
        alert(error);
        this.Loader.hide();
      });
  }
  PreviousRecord() {
    
    var check = this.list_Of_Ids.indexOf(this.userId);
    var index = --check;
    if (index >= 0) {
      this.userId = this.list_Of_Ids[index];
      this.getUserProfile();
      this.GetBussinessProfile();
    }
    if (this.list_Of_Ids[0] == this.userId && this.pageNumber >= 1) {
      this.pageNumber = --this.pageNumber;
      this.service.get(this.service.apiRoutes.Users.SpGetTradesmanList + '?isOrganisation=' + false + "&pageSize=" + this.pageSize + "&pageNumber=" + this.pageNumber).subscribe(result => {
        var tradesmanList = result.json();
        
        for (var i = 0; i < tradesmanList.length; i++) {
          this.listOfIds.push(tradesmanList[i].tradesmanId);
        }
        this.list_Of_Ids = this.listOfIds;
        this.userId = this.list_Of_Ids[this.list_Of_Ids.length - 1];
        this.getUserProfile();
        //this.GetBussinessProfile();
      },
        error => {
          console.log(error);
        });
    }
  }
  NextRecord() {
    var check = this.list_Of_Ids.indexOf(this.userId);
    var index = ++check;
    if (index < this.list_Of_Ids.length) {
      this.userId = this.list_Of_Ids[index];
      this.getUserProfile();
      this.GetBussinessProfile();
    }
    if (this.list_Of_Ids.length == index && this.pageNumber <= this.totalPages) {
      
      this.pageNumber = ++this.pageNumber;
      this.service.get(this.service.apiRoutes.Users.SpGetTradesmanList + '?isOrganisation=' + false + "&pageSize=" + this.pageSize + "&pageNumber=" + this.pageNumber).subscribe(result => {
        var tradesmanList = result.json();
        
        for (var i = 0; i < tradesmanList.length; i++) {
          this.listOfIds.push(tradesmanList[i].tradesmanId);
        }
        this.list_Of_Ids = this.listOfIds;
        this.firstId = this.userId = this.list_Of_Ids[0];
        this.getUserProfile();
        this.GetBussinessProfile();
      },
        error => {
          console.log(error);
        });
    }
  }
}
