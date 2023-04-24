import { Component, OnInit, ViewChild, ElementRef, NgZone } from '@angular/core';
import { BusinessDetailsupdate } from '../../../models/tradesmanModels/tradesmanModels';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '../../../shared/HttpClient/_http';
import { ActivatedRoute } from '@angular/router';
import { httpStatus, AspnetRoles, loginsecurity } from '../../../shared/Enums/enums';
import { JwtHelperService } from '@auth0/angular-jwt';
import { MapsAPILoader } from '@agm/core';
import { ResponseVm } from '../../../models/commonModels/commonModels';
import { pagesUrls } from '../../../shared/ApiRoutes/ApiRoutes';
import { ToastrService } from 'ngx-toastr';
import { IDropdownSettings, IIdValue, ITownListVM } from '../../../shared/Interface/tradesman';

@Component({
  selector: 'app-bussinessdetail',
  templateUrl: './bussinessdetail.component.html',
  styleUrls: ['./bussinessdetail.component.css']
})
export class BussinessdetailComponent implements OnInit {

  public TradesmanSkills: IIdValue[] = [];
  public DeliveryRadiousList: IIdValue[] = [];
  public CitiesList: IIdValue[] = [];
  public submittedForm = false;
  public townChanged: boolean = false;
  public townInvalidInput: boolean = false;
  //public selectedItemsSubCategory = [];
  public fullName: string="";
  //public subCategoriesList;
  //public SelectedSubCategoriesList = [];
  public TradesmanSkillsdropdownSettings: IDropdownSettings;
  public organizationSkillsdropdownSettings: IDropdownSettings;
  public ShowFilter = true;
  public readOnly = "readOnly";
  //public subproductIds = [];
  public isFlip: boolean = false;
  public cityName: string = "";
  public isOrganization: boolean=false;
  public latitude: number = 30.3753;
  public longitude: number = 69.3451;
  public zoom: number = 15;
  public address: string = "";
  public error: boolean = false;
  public whatweoffererror: boolean = false;
  public errorMessage: string="";
  public responseVm: ResponseVm = {} as ResponseVm;
  //public mapType = 'hybrid';
  public geoCoder = new google.maps.Geocoder();
  public townList: ITownListVM[] = [];
  public searchtownList: IIdValue[] = [];
  public getLoggedTradesmanRole: string="";
  townkeyword = 'value';
  @ViewChild('search', { static: true }) public searchElementRef: ElementRef;

  public businessDetailsupdate: BusinessDetailsupdate;
  jwtHelperService: JwtHelperService = new JwtHelperService();

  public appValForm: FormGroup;
  ProductIds: any;
  constructor(
    public common: CommonService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    public mapsAPILoader: MapsAPILoader,
    private toastr: ToastrService,
    public ngZone: NgZone
  ) {
    this.searchElementRef = {} as ElementRef;
    this.appValForm = {} as FormGroup;
    this.TradesmanSkillsdropdownSettings = {} as IDropdownSettings;
    this.organizationSkillsdropdownSettings = {} as IDropdownSettings;
    this.businessDetailsupdate = {} as BusinessDetailsupdate;

    //this.businessDetailsupdate.skillIds = [];
  }

  ngOnInit() {
    
    var userRoleid = localStorage.getItem("TorSrole");
    var token = localStorage.getItem("auth_token");
    var decodedtoken = token ? this.jwtHelperService.decodeToken(token) : "";
    this.getLoggedTradesmanRole = decodedtoken.Role;
    
    this.startLocation();
    if (userRoleid == AspnetRoles.TRole)
      this.isOrganization = false;
    else
      this.isOrganization = true;

    this.appValForm = this.formBuilder.group({
      tradesmanId: [0],
      businessAddress: ['', [Validators.required]],
      town: ['', Validators.required],
      tradesmanSkills: [0, Validators.required],
      travelingDistance: ['', [Validators.required]],
      cityId: [null, Validators.required],
      isOrganization: [null],
      companyName: ['', [Validators.required]],
      companyRegNo: [''],
      addresses: [''],

    });
    this.TradesmanSkillsdropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: this.ShowFilter,
      itemsShowLimit: 2,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true

    };
    this.organizationSkillsdropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'value',
      allowSearchFilter: this.ShowFilter,
      itemsShowLimit: 2,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableCheckAll: true

    };
    this.whatweoffererror = false;
    this.DropDownRadiusAndSkill();
    
  }
  get f() {
    return this.appValForm.controls;
  }

  public DropDownRadiusAndSkill() {
    this.common.GetData(this.common.apiRoutes.Supplier.GetDropDownOptionWeb, true).then(result => {
      if (status = httpStatus.Ok) {
        var data = result ;
        this.CitiesList = data.cities;
        this.DeliveryRadiousList = data.distances;
        this.common.GetData(this.common.apiRoutes.Tradesman.GetTradesmanSkills, true).then(result => {
          if (status = httpStatus.Ok) {
            var data = result ;
            this.TradesmanSkills = data;
          }
        }, error => {
          console.log(error);
        });
      }
    }, error => {
      console.log(error);
    })
  }
 
  public Save() {

    this.submittedForm = true;
    var data = this.appValForm.value;
    if (!this.isOrganization) {
      this.appValForm.get('companyName')?.clearValidators();
      this.appValForm.get('companyName')?.updateValueAndValidity();
    }
    var tradesMainSkills = data.tradesmanSkills;
    if (tradesMainSkills == 0)
      this.whatweoffererror = true;
    if (this.appValForm.invalid) {
      return;
    }
    else if ((tradesMainSkills.length > 2 && this.getLoggedTradesmanRole == loginsecurity.TRole)) {
      this.toastr.error('You Can Select Maximum 2 Skills !!', "Alert !!");
      return;
    }
    this.CitiesList.filter(c => c.id == data.cityId).map(x => {
      this.cityName = x.value;
    });
    let selectedTown = this.appValForm.value.town;
    let filterTown = this.searchtownList.filter(x => x.value == selectedTown.value );
    if (filterTown.length <= 0) {
    
      this.townInvalidInput = true;
      // this.appValForm.controls['town'].setErrors({ incorrect: true, inValidTown: 'Invalid town' });
      setTimeout(() =>{
        this.townInvalidInput = false;
      },3000);
      return;
    }
    this.businessDetailsupdate = {
      town:selectedTown.value,
      businessAddress: this.searchElementRef.nativeElement.value.toString(),
      city:this.cityName,
      companyName: data.companyName.toString(),
      companyRegNo: data.companyRegNo.toString(),
      cityId:Number(data.cityId),
      travelingDistance:Number(data.travelingDistance),
      isOrganization: this.isOrganization,
      locationCoordinates: this.latitude + "," + this.longitude,
      skillIds: [],
      tradesmanSkills: [],
      tradesmanId :0
    };
    var selectedSkillsId = this;
    if (tradesMainSkills.length) {
      for (var i in tradesMainSkills) {
        var pd = tradesMainSkills[i].id;
        selectedSkillsId.businessDetailsupdate.skillIds?.push(pd);
      }
    }
    else {
      selectedSkillsId.businessDetailsupdate.skillIds?.push(pd);
    }

    
    
     //this.businessDetailsupdate.tradesmanSkills = [];

    console.log(this.businessDetailsupdate.skillIds);
    console.log(this.businessDetailsupdate);
    
    this.common.PostData(this.common.apiRoutes.Tradesman.AddEditTradesmanWithSkills, this.businessDetailsupdate, true).then(result => {
      this.responseVm = result ;

      this.common.NavigateToRoute(this.common.apiRoutes.Tradesman.Dashboard);
    }, error => {
      console.log(error);
    });

  }

  public getTownList(cId:number) {
    this.searchtownList = [];
    let cityId = cId.toString();
    this.common.GetData(this.common.apiRoutes.UserManagement.GetTownsByCityId + "?cityId=" + cityId, false).then(res => {
      this.townList = res;
      console.log(this.townList);
      this.townList.forEach((x) => {
        this.searchtownList.push({ value: x.name, id: x.townId });
      })
    });

  }
  public selectTownEvent(item: Event) {
    this.townChanged = true;
  }
  //public unselecttownEvent(item:any) {
  //}

  public onChangetownSearch(val: string) {
    // fetch remote data from here
    // And reassign the 'data' which is binded to 'data' property.
  }
  
  public selectedCity(cityId: number) {
  
    console.log(cityId);
    this.getTownList(cityId);

  }

  public setCurrentLocation(latitude: number, longitude: number) {
    if (latitude != undefined && longitude != undefined) {
      this.latitude = latitude;
      this.longitude = longitude;
      this.getAddress(latitude, longitude);
    }
    else if ('geolocation' in navigator) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.latitude = position.coords.latitude;
        this.longitude = position.coords.longitude;
        this.getAddress(this.latitude, this.longitude);
      });
    }
  }

  public markerDragEnd($event: MouseEvent) {
    console.log($event);
    this.getAddress(this.latitude, this.longitude);
  }

  public getAddress(latitude: number, longitude: number) {

    this.geoCoder.geocode({ 'location': { lat: latitude, lng: longitude } }, (results, status) => {
      if (status === 'OK') {
        if (results[0]) {
          this.address = results[0].formatted_address;
          console.log(this.address);
        } else {
          window.alert('No results found');
        }
      } else {
        window.alert('Geocoder failed due to: ' + status);
      }

    });
  }

  public startLocation() {
    this.mapsAPILoader.load().then(() => {
      this.setCurrentLocation(this.latitude, this.longitude);
      let autocomplete = new google.maps.places.Autocomplete(this.searchElementRef.nativeElement, {
        types: ["address"]
      });
      autocomplete.addListener("place_changed", () => {
        this.ngZone.run(() => {
          //get the place result
          let place: google.maps.places.PlaceResult = autocomplete.getPlace();
          //verify result
          if (place.geometry === undefined || place.geometry === null) {
            return;
          }
          //set latitude, longitude and zoom
          this.latitude = place.geometry.location.lat();
          this.longitude = place.geometry.location.lng();
        });
      });
    });
  }

  public addMarker(lat: number, lng: number) {
    this.latitude = lat;
    this.longitude = lng;
    this.setCurrentLocation(this.latitude, this.longitude);
  }

  public SearchLocation(location:any) {

    this.startLocation();
  }
  onItemSelect() {
    this.whatweoffererror = false;
  }

  numberOnly(event: any): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }
  charOnly(event:any): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if ((charCode > 47 && charCode < 58)) {
      return false;
    }
    return true;
  }




}
