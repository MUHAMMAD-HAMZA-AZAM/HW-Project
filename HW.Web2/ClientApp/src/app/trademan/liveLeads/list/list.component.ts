import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CommonService } from '../../../shared/HttpClient/_http';
import { httpStatus, AspnetRoles, loginsecurity } from '../../../shared/Enums/enums';
import { tradesmanProfile, BusinessDetailsupdate } from '../../../models/tradesmanModels/tradesmanModels';
import { IJobLeadsWeb } from '../../../shared/Interface/tradesman';
import { resolveAny } from 'dns';
import { strict } from 'assert';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { MessagingService } from '../../../shared/CommonServices/messaging.service';
@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class LeadsListComponent implements OnInit {
  public count = 1;
  public liveLeads: IJobLeadsWeb[] = [];
  public liveLeadsConcate: IJobLeadsWeb[] = [];
  //public townList = [];
  public incompletedProfile: boolean = false;
  public notFound: boolean = false;
  public getloggedTradesmanRole: string = "";
  public townId: number = 0;
  public profile: tradesmanProfile = {} as tradesmanProfile;;
  public businessDetailsupdate: BusinessDetailsupdate = {} as BusinessDetailsupdate;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  @ViewChild('updateSkillsMessageModal', { static: false }) updateSkillsMessageModal: ModalDirective;

  constructor(public common: CommonService, private _messagingService: MessagingService) {
    this.updateSkillsMessageModal = {} as ModalDirective;
  }

  ngOnInit() {
    var token = localStorage.getItem("auth_token");
    var decodedtoken = token != null ? this.jwtHelperService.decodeToken(token) : "";
    this.getloggedTradesmanRole = decodedtoken.Role;
    if (this.getloggedTradesmanRole == loginsecurity.TRole) {
      this.getLoggedTradesmanProfile();
    }
    else {
      this.populateData(this.count);
    }
    //this._messagingService.receiveMessage();
    //this.getTrademanProfile();
  }

  public getLoggedTradesmanProfile() {
    this.common.GetData(this.common.apiRoutes.Tradesman.GetBusinessAndPersnalProfileWeb, true).then(result => {
      if (status == httpStatus.Ok) {
        this.profile = result;
        this._messagingService.requestPermission(this.profile.persnalDetails.firebaseClientId);
        let tradesmanSkills = this.profile.businessDetails?.tradesmanSkills?.length != undefined ? this.profile.businessDetails.tradesmanSkills.length : 0;
        if (tradesmanSkills > 2) {
          this.businessDetailsupdate = this.profile.businessDetails;
          this.businessDetailsupdate.tradesmanSkills = [];
          this.businessDetailsupdate.skillIds = [0];
          this.common.PostData(this.common.apiRoutes.Tradesman.AddEditTradesmanWithSkills, this.businessDetailsupdate, true).then(result => {
            if (status = httpStatus.Ok) {
              var data = result;
              this.updateSkillsMessageModal.show();
              this.populateData(this.count);
            }
          },
            error => {
              console.log(error);
            });
        }
        else {
          this.populateData(this.count);
          if (tradesmanSkills == 0) {
            this.updateSkillsMessageModal.show();
          }
        }
      }

    })
  }

  public updateProfile() {
    this.common.NavigateToRoute(this.common.apiUrls.Tradesman.Profile);
  }

  public closeUpdateSkillsMessageModal() {
    this.updateSkillsMessageModal.hide();
  }
  //public getTrademanProfile() {
  //  this.common.get(this.common.apiRoutes.Tradesman.GetBusinessDetailsStatus).subscribe(result => {
  //    let res = <any>result;
  //    console.log(res);
  //    if (res.status == 200) {
  //      this.populateData(this.count);
  //      this.incompletedProfile = false;
  //    }
  //    else {
  //      this.incompletedProfile = true;
  //    }
  //  })
  //}
  public populateData(count: number) {
    this.common.GetData(this.common.apiRoutes.Tradesman.GetJobLeadsWebByTradesmanId + "?pageNumber=" + count + "&pageSize=" + 10, true).then(data => {
      this.liveLeads = data;
      if (this.liveLeads != null) {
        this.liveLeadsConcate = [...this.liveLeadsConcate, ...this.liveLeads];
        this.notFound = false;
      }
      else {
        this.notFound = true;
      }
    },
      error => {
        console.log(error);
      });
  }
  onScroll() {

    this.populateData(++this.count);
  }
  public JobDetail(jqId: number, Liveleads: string) {
    this.common.NavigateToRouteWithQueryString(this.common.apiUrls.Tradesman.LiveLeadsDeatils, { queryParams: { jobDetailId: jqId, PageName: Liveleads } });
  }
}
