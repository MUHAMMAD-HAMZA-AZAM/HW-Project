import { Component, OnInit } from '@angular/core';
//import { ModalModule } from 'ngx-bootstrap/modal/public_api';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CommonService } from '../../Shared/HttpClient/_http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
//import { strict } from 'assert';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.css']
})
export class CityComponent implements OnInit {
  public stateList = [];
  public selectedState = [];
  public selectedColumn = [];
  public dropdownListForState = {};
  public dropdownListForColumn = {};
  public cityId: any;

  public cityList: [] = [];
  public statesList: [] = [];
  public modalHeadText: string = "Add New City";
  public cityName = "";
  public cityNameExist: string = "";
  public Name: string = "";
  public cityData: any;
  public confirmDelete: boolean = false;
  public cityDeleteId: any;
  jwtHelperService: JwtHelperService = new JwtHelperService();
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };    


  constructor(public router: Router,public toastrService: ToastrService, public _modalService: NgbModal, public service: CommonService) { }

  ngOnInit() {
      this.userRole = JSON.parse(localStorage.getItem("City"));
      if (!this.userRole.allowView)
        this.router.navigateByUrl('/login');

    this.dropdownListForState = {
      singleSelection: false,
      idField: 'stateId',
      textField: 'name',
      allowSearchFilter: true,
      itemsShowLimit: 3,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      closeDropDownOnSelection  : false,
      enableCheckAll: true
    };
    this.getCitiesList();
    this.getStatesList();
  }


  save() {
   
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    let cityIds;
    cityIds = Number(this.selectedState[0].stateId);
    this.service.get(this.service.apiRoutes.Common.CheckCityAvailability + "?cityName=" + this.cityName).subscribe(result => {
      
      let response = result.json();
      if (response != null && response.name.toLowerCase() == this.cityName.toLowerCase() && response.stateId == cityIds) {
        this.cityNameExist = "City name already exist";
        setTimeout(() => {
          this.cityNameExist = "";
        },3000)
      }
      else {
        
        if (this.cityData == null || this.cityData == "" || this.cityData == undefined) {
          let Obj = {
            Name: this.cityName,
            CreatedBy: decodedtoken.UserId,
            StateId: cityIds,
            IsActive: true,
            ModifiedBy: ""
          }
          this.service.post(this.service.apiRoutes.Common.AddNewCity, Obj).subscribe(result => {
            if (result.status == 200) {
              this.cityName = "";
              this.toastrService.success("Data added Successfully", "Success");
              this._modalService.dismissAll();
              this.getCitiesList();
            }
          })
        }
        else {
          let updateObj = {
            Name: this.cityName,
            CityId: this.cityData.cityId,
            StateId: cityIds,
            IsActive: true,
            ModifiedBy: decodedtoken.UserId,
          } 
          this.service.post(this.service.apiRoutes.Common.UpdateCity, updateObj).subscribe(result => {
            if (result.status == 200) {
              this.cityName = "";
              this.cityData = null;
              this.toastrService.success("Data updated Successfully", "Success");
              this._modalService.dismissAll();
              this.getCitiesList();
            }
          })
        }
      }
    })

  }
  updateCity(city, content) {
    this.modalHeadText = "Update City"
    
    if (city) {
      this.cityName = city.cityName;
      this.cityData = city;
      this.selectedState = [];
      this.selectedState = [{ stateId: city.stateId, name: city.stateName }];
       this.cityId = city.cityId;
      this._modalService.open(content);
    }
  }
  deleteCity(cityId, deleteContent) {
    this.cityDeleteId = String(cityId);
    this._modalService.open(deleteContent);
  }
  confirmDeleteCity() {
    if (this.cityDeleteId != null && this.cityDeleteId !="") {
      this.service.get(this.service.apiRoutes.Common.DeleteCity + "?cityId=" + this.cityDeleteId).subscribe(result => {
        if (result.status == 200) {
          this.toastrService.success("Status changed successfully!", "Success");
          this._modalService.dismissAll();
          this.getCitiesList();
        }
        else {
          this.toastrService.warning("Something went wrong please try again", "Error");
          this._modalService.dismissAll();
        }
      })
    }
  }

  resetForm() {
    this.cityName = null;
    this.selectedState = null;
  }
  public getCitiesList() {
    this.service.get(this.service.apiRoutes.Common.GetCitiesList).subscribe(result => {
      this.cityList = result.json();
      console.log(this.cityList);
    })
  }
  showModal(content) {
    this.selectedState = [];
    this.modalHeadText = "Add New City";
    this._modalService.open(content)
  }

  //  State Drop Setting

  onCitySelectAll(item: any) {
    console.log(item);
    this.selectedState = item;
  }
  onCityDeSelectALL(item: any) {
    this.selectedState = [];
    console.log(item);
  }
  onCitySelect(item: any) {
    console.log(item);
    
    this.selectedState = [];
    this.selectedState.push(item);
  }
  onCityDeSelect(item: any) {
    this.selectedState = this.selectedState.filter(
      function (value, index, arr) {
        return value.stateId != item.stateId;
      });
  }

  onColumnSelectAll(item: any) {
    console.log(item);
    this.selectedColumn = item;
  }
  onColumnDeSelectALL(item: any) {
    this.selectedColumn = [];
    console.log(item);
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
      console.log(this.statesList);
    })
  }

}
