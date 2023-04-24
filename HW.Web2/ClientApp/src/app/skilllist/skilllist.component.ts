import { Component, OnInit } from '@angular/core';
import { ISkill } from '../shared/Enums/Interface';
import { CommonService } from '../shared/HttpClient/_http';

@Component({
  selector: 'app-skilllist',
  templateUrl: './skilllist.component.html',
  styleUrls: ['./skilllist.component.css']
})
export class SkilllistComponent implements OnInit {
  public activeskillList: ISkill[] = [];
  constructor(private common: CommonService) { }

  ngOnInit() {
    this.populateTradesmanSkills();
  }
  populateTradesmanSkills() {
    this.common.get(this.common.apiRoutes.Tradesman.GetSkillList + "?skillId=" + 0).subscribe(result => {
      this.activeskillList = <ISkill[]>result;
      
    });
  }

}
