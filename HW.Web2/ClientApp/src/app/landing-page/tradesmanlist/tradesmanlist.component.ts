import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../shared/HttpClient/_http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { NgxSpinnerService } from "ngx-spinner";
import { IdValueVm } from '../../models/commonModels/commonModels';
import { IIdValue, ITradesmanReportbySkill } from '../../shared/Interface/tradesman';

@Component({
  selector: 'app-tradesmanlist',
  templateUrl: './tradesmanlist.component.html',
  styleUrls: ['./tradesmanlist.component.css']
})
export class TradesmanlistComponent implements OnInit {
  
  //public filterlist;
  //public SelectedSkillsList;
  public tradesmanList: ITradesmanReportbySkill[]=[];
  public tradesmanId: number = 0;
  public showNullMessage: boolean = false;
  public showTable: boolean = false;
  public skill: IIdValue;
  public town: string="";
  public flag: boolean = false;
  public skillList: IIdValue[]=[];
  public searcedSkills: IIdValue[] = [];
  public skillId: any;
  public listOfSkills: IdValueVm[] = [];
  constructor(private spinner: NgxSpinnerService, public service: CommonService, private route: Router, private router: ActivatedRoute, public Loader: NgxSpinnerService) {
    this.skill = {} as IIdValue;
  }

  ngOnInit() {
    this.router.queryParams.subscribe((params: Params) => {
      
      this.skill.id = params['skill'];
      this.town = params['town'];
      this.GetAllTradesmanbySkillTown();
    });
    let skill = localStorage.getItem("skills");
    this.skillList = skill != null ? JSON.parse(skill):"";
    this.GetSkills();
  }
  public GetSkills() {
    this.service.get(this.service.apiRoutes.Jobs.GetTradesmanSkillsByParentId + "?parentId=0").subscribe(result => {
      this.listOfSkills = <IdValueVm[]>result;
    },
      error => {
        console.log(error);
      })
  }

  public GetAllTradesmanbySkillWithInTown(skIdInTown: IIdValue) {
    if (skIdInTown.value == null || skIdInTown.value == undefined) {
      let a = skIdInTown.id;
      skIdInTown = {
        id: a,
        value:""
      };
    }
    this.tradesmanList = [];
    this.spinner.show();
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Tradesman.GetTradesmanBySkillTown + "?skills=" + skIdInTown.id + "&town=" + this.town + "&tradesmanId" + this.tradesmanId).subscribe(result => {
      this.tradesmanList = <ITradesmanReportbySkill[]>result;
      this.Loader.hide();

      if (this.tradesmanList != null || this.tradesmanList != undefined) {
        this.showTable = true;
        this.showNullMessage = false;
        this.spinner.hide();
      }
      else {
        this.showNullMessage = true;
        this.showTable = false;
        this.spinner.hide();

      }

    },
      error => {
        console.log(error);
        this.spinner.hide();
      });



  }

  public onselectClient(client: IIdValue) {
    this.skill.id = client.id;
    this.skill.value = client.value;
    this.flag = false;
  }
  public searchClient(skill: IIdValue, eve: Event) {
    
    this.searcedSkills = [];
    this.skillList.forEach(value => {
      if (value.value.toLowerCase().includes(skill.value.toLowerCase())) {
        let skilltoAdd = { "id": value.id, "value": value.value };
        this.searcedSkills.push(skilltoAdd);
      }
    });
    this.flag = true;
  }

  public skillSelect(skill: IIdValue) {
    this.skill.id = skill.id;
    this.skill.value = skill.value;
    this.GetAllTradesmanbySkillTown();

  }

  public search() {
    this.GetAllTradesmanbySkillTown();
  }

  public GetAllTradesmanbySkillTown() {
   
    
    this.tradesmanList = [];
    this.spinner.show();
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Tradesman.GetTradesmanBySkillTown + "?skills=" + this.skill.id + "&town=" + this.town + "&tradesmanId" + this.tradesmanId).subscribe(result => {
      this.tradesmanList = <ITradesmanReportbySkill[]>result;
      this.Loader.hide();
      if (this.tradesmanList != null || this.tradesmanList != undefined) {
        this.showTable = true;
        this.showNullMessage = false;
        this.spinner.hide();
      }
      else {
        this.showNullMessage = true;
        this.showTable = false;
        this.spinner.hide();
      }
      
    },
      error => {
        console.log(error);
        this.spinner.hide();
      });
  }

  public Navigatetologin() {
    if (this.service.loginCheck) {
      this.service.NavigateToRoute(this.service.apiUrls.User.Quotations.getQuotes1);
    }
    else {
      localStorage.setItem("Role", '3');
      localStorage.setItem("Show", 'true');
      this.service.NavigateToRoute(this.service.apiUrls.Login.customer);
    }
  }

  public tradesmanProfilePage(tradesmanId: number) {
    this.service.NavigateToRouteWithQueryString(this.service.apiUrls.Tradesman.tradesmanProfile, { queryParams: { tradesmanId: tradesmanId } });
  }

  public postJobWithSkillId(skId: number) {
    this.skillId = skId;
    this.service.NavigateToRouteWithQueryString(this.service.apiUrls.User.QuoteStep1, { queryParams: { skillId: this.skillId }});
  }
 

}
