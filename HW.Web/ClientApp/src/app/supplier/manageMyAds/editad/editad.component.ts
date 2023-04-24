import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { CommonService } from '../../../shared/HttpClient/_http';

@Component({
  selector: 'app-editad',
  templateUrl: './editad.component.html',
  styleUrls: ['./editad.component.css']
})
export class EditadComponent implements OnInit {
  public appValForm: FormGroup;
  public AdId: number;
  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private common: CommonService) { }

  ngOnInit() {
    debugger;
    this.route.queryParams.subscribe((params: Params) => {
      this.AdId = params['supplierAdsId'];
      if (this.AdId > 0)
        this.PopulateData();
    });
    this.appValForm = this.formBuilder.group({
      id: [0],
      createdOn: [new Date()],
      createdBy: [0],
      locationName: ['', Validators.required],
      lineAddress1: [''],
      lineAddress2: [''],
      lineAddress3: [''],
      city: ['', Validators.required],
      county: [''],
      countryId: [0],
      postcode: [''],
      contactPerson: [''],
      telephone: ['']
    });
  }

  PopulateData() {
    debugger;
    var urls = this.common.apiRoutes.Supplier.GetEditAdDetail + "?supplierAdsId=" + this.AdId;
    this.common.GetData(urls, true).then(data => {
      var result=data.json();
    });
    }

}
