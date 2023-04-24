import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../../../shared/HttpClient/_http';

@Component({
  selector: 'app-blog-details1',
  templateUrl: './blogDetails1.component.html',
  styleUrls: ['./blogDetails1.component.css']
})
export class BlogDetails1Component implements OnInit {

  constructor(public common: CommonService) { }

  ngOnInit() {
  }

}
