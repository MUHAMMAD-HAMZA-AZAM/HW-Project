import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '../../../shared/HttpClient/_http';
import { BasicRegistration, IdValueVm, ResponseVm } from '../../../models/commonModels/commonModels';
import { RegistrationErrorMessages, httpStatus } from '../../../shared/Enums/enums';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-registration-basic',
  templateUrl: './registration-basic.component.html',
  styleUrls: ['./registration-basic.component.css']
})
export class RegistrationBasicComponent implements OnInit {
  public appValForm: FormGroup;
  public check: { T: boolean, O: boolean, U: boolean, S: boolean } = { T: false, O: false, U: false, S: false };
  public roleType;
  public response: ResponseVm = new ResponseVm();
  submitted = false;
  public basicRegistrationVm: BasicRegistration = new BasicRegistration();
  public idValueVm: IdValueVm[] = [];
  public firstNameErrorMessage: string;
  public lastNameErrorMessage: string;
  public dataOfBirthErrorMessage: string;
  public passwordErrorMessage: string;
  public genderErrorMessage: string;
  public cityErrorMessage: string;
  public roleErrorMessage: string;
  public termsAndConditionErrorMessage: string;
  public userAvailabilty: boolean = false;
  public userAvailabiltyErrorMessage: string;
  public roleId: any;
  constructor(private formBuilder: FormBuilder, private service: CommonService, private route: ActivatedRoute) {
    
    this.service.get(this.service.apiRoutes.Common.getCityList).subscribe(result => {
      this.idValueVm = result.json();
    })
    this.roleType = localStorage.getItem("Role");
  }

  ngOnInit() {
    
    this.roleId = +this.route.snapshot.paramMap.get('id');
    switch (this.roleType) {
      case '1':
        this.check.T = true;
        this.check.O = false;
        this.check.S = false;
        this.check.U = false;
        break;
      case '2':
        this.check.O = true;
        this.check.T = false;
        this.check.S = false;
        this.check.U = false;
        break;
      case '3':
        this.check.U = true;
        this.check.O = false;
        this.check.T = false;
        this.check.S = false;
        break;
      case '4':
        this.check.S = true;
        this.check.U = false;
        this.check.O = false;
        this.check.T = false;
      default:
    }
    this.appValForm = this.formBuilder.group(
      {
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        dateOfBirth: ['', Validators.required],
        password: ['', [Validators.required, Validators.minLength(8)]],
        gender: ['', Validators.required],
        cnic: [],
        emailAddress: [],
        phoneNumber: ['', [Validators.required, Validators.minLength(11), Validators.maxLength(12)]],
        city: ['', Validators.required],
        role: [],
        termsAndConditions: [false, Validators.required]
      }
    );
  }

  get f() { return this.appValForm.controls; }

  VerifyAndSendOtp() {
    
    this.firstNameErrorMessage = RegistrationErrorMessages.firstNameErrorMessage;
    this.lastNameErrorMessage = RegistrationErrorMessages.lastNameErrorMessage;
    this.dataOfBirthErrorMessage = RegistrationErrorMessages.dataOfBirth;
    this.genderErrorMessage = RegistrationErrorMessages.genderErrorMessage;
    this.cityErrorMessage = RegistrationErrorMessages.cityErrorMessage;
    this.roleErrorMessage = RegistrationErrorMessages.roleErrorMessage;
    this.termsAndConditionErrorMessage = RegistrationErrorMessages.termsAndConditionErrorMessage;
    this.submitted = true;
    this.appValForm.value.role = this.roleType;
    this.basicRegistrationVm = this.appValForm.value;
    if (this.appValForm.valid) {
      this.service.post(this.service.apiRoutes.registration.CheckEmailandPhoneNumberAvailability, this.basicRegistrationVm).subscribe(result => {
        this.response = result.json();
        if (this.response.status == httpStatus.Ok) {
          localStorage.setItem("registrationData", JSON.stringify(this.basicRegistrationVm));
          this.service.NavigateToRouteWithQueryString(this.service.pagesUrl.CommonRegistrationPages.registrationCode);
        }
        else {
          this.userAvailabilty = true;
          this.userAvailabiltyErrorMessage = this.response.message;
        }
      })
    }
  }

  FocusoutOnDateOfBirth(date) {
    //
    //this.service.formatDate(date);
  }
}
