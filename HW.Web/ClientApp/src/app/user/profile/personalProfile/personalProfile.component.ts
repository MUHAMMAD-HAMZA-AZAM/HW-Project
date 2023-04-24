import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '../../../shared/HttpClient/_http';
import { ActivatedRoute } from '@angular/router';
import { Customer, ProfileImage } from '../../../models/userModels/userModels';
import { httpStatus } from '../../../shared/Enums/enums';

@Component({
  selector: 'app-personal-profile',
  templateUrl: './personalProfile.component.html',
})
export class PersonalProfileComponent implements OnInit {

  public profile: Customer = new Customer();
  constructor(
    public common: CommonService,
  ) { }

  ngOnInit() {
    this.PopulateData();
  }

  public PopulateData() {
    this.common.GetData(this.common.apiRoutes.Users.CustomerProfile, true).then(result => {
      debugger;
      if (status = httpStatus.Ok) {
        this.profile = result.json();
        if (this.profile.profileImage != null) {
          this.profile.profileImage = 'data:image/png;base64,' + this.profile.profileImage;
        }
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    });
  }
}
