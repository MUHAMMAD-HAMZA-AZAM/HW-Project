import { Component, OnInit } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal/public_api';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CommonService } from '../../Shared/HttpClient/_http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { strict } from 'assert';
import { Router } from '@angular/router';

@Component({
  selector: 'app-town',
  templateUrl: './town.component.html',
  styleUrls: ['./town.component.css']
})
export class TownComponent implements OnInit {

  public stateList = [];
  public selectedStates = [];
  public selectedCity = [];
  public selectedColumn = [];
  public dropdownListForState = {};
  public dropdownListForColumn = {};

  public cityList: [] = [];
  public townList: [] = [];
  public statesList: [] = [];
  public modalHeadText: string = "Add New Town";
  public townName = "";
  public cityNameExist: string = "";
  public Name: string = "";
  public cityData: any;
  public confirmDelete: boolean = false;
  public cityDeleteId: any;
  public cityDeleteId1: any;
  public sprights = 'false';
  public selectedTownId;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    


  constructor(public router: Router,public toastrService: ToastrService, public _modalService: NgbModal, public service: CommonService) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Town"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.dropdownListForState = {
      singleSelection: true,
      idField: 'cityId',
      textField: 'cityName',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      closeDropDownOnSelection: true,
      enableCheckAll: true
    };
    this.dropdownListForColumn = {
      singleSelection: false,
      idField: 'stateId',
      textField: 'name',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true
    };

    this.sprights = localStorage.getItem("SpectialRights");
    this.gettownList();
    this.getCitiesList();
  }
  save() {
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    let cityIds;
    cityIds = Number(this.selectedCity[0].cityId);
        
        if (this.cityData == null || this.cityData == "" || this.cityData == undefined) {
          let Obj = {
            Name: this.townName,
            CreatedBy: decodedtoken.UserId,
            cityId: cityIds,
            IsActive: true,
          }
          this.service.post(this.service.apiRoutes.Common.AddNewtown, Obj).subscribe(result => {
            
            if (result.status == 200) {
              var res = result.json();
              if (res.message == 'SAVED') {
                this.townName = "";
                this.toastrService.success("Data added Successfully", "Success");
                this._modalService.dismissAll();
                this.gettownList();
              }
              else {
                this.toastrService.error("Town Already Exist", "Error");
              }
            }
          })
        }
        else {
          let updateObj = {
            townId:this.cityData.townId,
            Name: this.townName,
            cityId: cityIds,
            CreatedBy: decodedtoken.UserId,
          }
          this.service.post(this.service.apiRoutes.Common.AddNewtown, updateObj).subscribe(result => {
            
            if (result.status == 200) {
              var res = result.json();
              if (res.message == 'SAVED') {
                this.townName = "";
                this.cityData = null;
                this.toastrService.success("Data updated Successfully", "Success");
                this._modalService.dismissAll();
                this.gettownList();
              }
              else {
                this.toastrService.error("Town Already Exist", "Error");
              }
            }
          })
        }
  }
  updateCity(city, content) {
    this.modalHeadText = "Upade Town"
    
    if (city != null && city != "") {
      console.log(city);
      this.townName = city.name;
      this.cityData = city;
      this.selectedCity = [];
      this.selectedCity = [{ cityId: city.cityId, cityName: city.cityName }];
      console.log(this.cityData);
      this._modalService.open(content);
    }
  }
  deleteCity(town, deleteContent) {
    
    this.selectedTownId = town.townId;
    this.cityDeleteId = town;
    this._modalService.open(deleteContent);
  }
  confirmDeleteCity() {
    
    if (this.cityDeleteId != null && this.cityDeleteId != "") {
      var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"))
      
      if (this.cityDeleteId.isActive==true)
        this.cityDeleteId.isActive = false;
      else
        this.cityDeleteId.isActive = true;
      this.cityDeleteId.CreatedBy = decodedtoken.UserId;
      this.service.post(this.service.apiRoutes.Common.AddNewtown, this.cityDeleteId).subscribe(result => {
        if (result.status == 200) {
          
          this.toastrService.success("Town status changed successfully!", "Status");
          this._modalService.dismissAll();
          this.gettownList();
        }
        else {
          this.toastrService.warning("Something went wrong please try again", "Error");
          this._modalService.dismissAll();
        }
      })
    }
  }
  public gettownList() {
    this.service.get(this.service.apiRoutes.Common.GetAllTown).subscribe(result => {
      this.townList = result.json();
    })
  }
  public getCitiesList() {
    this.service.get(this.service.apiRoutes.Common.GetCitiesList).subscribe(result => {
      this.cityList = result.json();
    });
  }
  showModal(content) {
    this.selectedCity = [];
    this.modalHeadText = "Add New Town";
    this.townName = "";
    this.cityData = [];
    this._modalService.open(content)
  }

  //  State Drop Setting

  onCitySelectAll(item: any) {
    this.selectedCity = item;
  }
  onCityDeSelectALL(item: any) {
    this.selectedCity = [];
  }
  onCitySelect(item: any) {
    this.selectedCity = [];
    this.selectedCity.push(item);
  }
  onCityDeSelect(item: any) {
    this.selectedCity = this.selectedCity.filter(
      function (value, index, arr) {
        return value.stateId != item.stateId;
      });
  }

  onColumnSelectAll(item: any) {
    this.selectedColumn = item;
  }
  onColumnDeSelectALL(item: any) {
    this.selectedColumn = [];
  }
  onColumnSelect(item: any) {
    this.selectedColumn.push(item);
  }
  onColumnDeSelect(item: any) {

    this.selectedColumn = this.selectedColumn.filter(
      function (value, index, arr) {
        return value.stateId != item.stateId;
      });
  }

  public getStatesList() {
    this.service.get(this.service.apiRoutes.Common.GetStatesList).subscribe(result => {
      this.stateList = result.json();
    });
  }

  public closeModal() {
    
    var x = document.getElementById(this.selectedTownId) as HTMLInputElement;
    if (x.checked == false) {
      
      x.checked = true;
      this._modalService.dismissAll();
    }
    else {
      x.checked = false;
      this._modalService.dismissAll();
    }
  }

}
