import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../HttpClient/_http';
import { Router } from '@angular/router';
import { NgxSpinnerService } from "ngx-spinner";

@Component({
  selector: 'app-logout-model',
  templateUrl: './app-logout-model.component.html',
  styleUrls: ['./app-logout-model.component.css']
})
export class AppLogoutModelComponent implements OnInit {

  constructor(public httpService: CommonService, private router: Router, public Loader: NgxSpinnerService) { }

  ngOnInit() {
  }
  
}
