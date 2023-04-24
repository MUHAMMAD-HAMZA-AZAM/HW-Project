import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '../../Shared/HttpClient/_http';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";

@Component({
  selector: 'app-commission',
  templateUrl: './commission.component.html',
  styleUrls: ['./commission.component.css']
})
export class CommissionComponent implements OnInit {
  public commForm: FormGroup;
  constructor(public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService, ) { }

  ngOnInit() {
    this.commForm = this.fb.group({
      id: [],
      title: [],
      commInNumber: [],
    })
  }

}
