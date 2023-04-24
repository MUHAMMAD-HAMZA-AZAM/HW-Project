import { Component, OnInit } from '@angular/core';
import { CommonService } from '../shared/HttpClient/_http';
import { ActivatedRoute, Router } from '@angular/router';
import { AspnetRoles } from '../shared/Enums/enums';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormControl } from '@angular/forms';
import { IIdValue } from '../shared/Interface/tradesman';
import { IdValueVm } from '../models/commonModels/commonModels';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent implements OnInit {
  public loginCheck: boolean = false;
  public subCategory: any;
  public Hidden = false;
  myControl = new FormControl();
  //public selectedItems = [];
  public role: string|null = "";
  public skillList: IIdValue[] = [];
  SelectedSkillsList: IIdValue[] = [];
  skillsdropdownSettings: IDropdownSettings;
  //public imagesUri;
  constructor(public common: CommonService,
    private aRouter: ActivatedRoute,
    public Loader: NgxSpinnerService,
    private router: Router) {
    this.skillsdropdownSettings = {} as IDropdownSettings;
  }
 
  public keyPress(event: any) {
    if (event.length > 0) {
      this.common.get(this.common.apiRoutes.Home.GetAllSubcategory + "?search=" + event).subscribe(data => {
        var result = data ;
        if (result) {
          this.subCategory = data ;
          this.Hidden = true;
        } else this.Hidden = false;

      });
    }
  }
  ngOnInit() {
    if (this.common.IsUserLogIn()) {
      this.role = localStorage.getItem("Role");
    }
    this.skillsdropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };
    this.SelectedSkillsList = [];
    this.populateSkills();
  }
  public populateSkills() {
    this.Loader.show();
    this.common.get(this.common.apiRoutes.Tradesman.GetTradesmanSkills).subscribe(result => {
      this.skillList = <IIdValue[]>result;
      this.Loader.hide();
    },
      error => {
        console.log(error);
        this.Loader.hide();
      });
  }
  public PostJob() {
    if (this.common.loginCheck) {
      this.common.NavigateToRoute(this.common.apiUrls.User.Quotations.getQuotes1);
    }
    else {
      localStorage.setItem("Role", '3');
      localStorage.setItem("Show", 'true');
      this.common.NavigateToRoute(this.common.apiUrls.Login.customer);
    }
  }
  loadList() {
    
    var skills = this.SelectedSkillsList;
    let ids: number[] = [];
    this.SelectedSkillsList.forEach(function (item) {
      ids.push(item.id);
    })
    
    this.router.navigateByUrl('landing-page/tradesmanlist/' + ids.join(","));
  }
  onItemSelectAll(items: any) {
    console.log(items);
    this.SelectedSkillsList = items;
    //this.SelectedSkillsList = [];
    //items.forEach(function (item) {

    //  this.SelectedSkillsList.push(item);


    //});

  }
  OnItemDeSelectALL(items: any) {
    this.SelectedSkillsList = [];
    console.log(items);
  }
  onItemSelect(item: any) {
    this.SelectedSkillsList.push(item);
    console.log(this.SelectedSkillsList);
  }
  OnItemDeSelect(item: any) {

    this.SelectedSkillsList = this.SelectedSkillsList.filter(
      function (value, index, arr) {
        //console.log(value);
        return value.id != item.id;
      }

    );

    console.log(this.SelectedSkillsList);
  }

}
