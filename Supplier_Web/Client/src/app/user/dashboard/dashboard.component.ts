import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { ResponseVm, StatusCode } from '../../Shared/Enums/enum';
import { IPersonalDetails } from '../../Shared/Enums/Interface';
import { CommonService } from '../../Shared/HttpClient/HttpClient';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  public response: ResponseVm = new ResponseVm();
  public customerId: number=0;
  public userId: string='';
  public userRole:string='';
  public loggedUserDetails: IPersonalDetails;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(
    public _httpService: CommonService,
    private formBuilder: FormBuilder,
    private toaster: ToastrService,
    private route: ActivatedRoute,

  ) {
this.loggedUserDetails={} as  IPersonalDetails;
}


  ngOnInit(): void {
    var decodedtoken = this._httpService.decodedToken();
    this.customerId = decodedtoken.Id;
    this.userId = decodedtoken.UserId;
    this.userRole = decodedtoken.Role;
    this.getLoggedUserDetails(this.userRole, this.userId);
  }
  public getLoggedUserDetails(role:string, userId:string) {

    this._httpService.GetData(this._httpService.apiUrls.Customer.GetUserDetailsByUserRole + `?userId=${userId}&userRole=${role}`,true).then(res => {
      this.response = res;
      if (this.response.status == StatusCode.OK) {
        this.loggedUserDetails = <any>this.response.resultData;
        console.log(this.loggedUserDetails);
      }
    }, error => {
      this._httpService.Loader.show();
      console.log(error);
    });
  }

  public logOut() {
    localStorage.clear();
    this._httpService.NavigateToRoute(this._httpService.apiRoutes.Login.login);
  }
}
