import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../../../shared/HttpClient/_http';

@Component({
  selector: 'app-blog-details2',
  templateUrl: './blogDetails2.component.html',
  styleUrls: ['./blogDetails2.component.css']
})
export class BlogDetails2Component implements OnInit {

  constructor(public common: CommonService) { }

  ngOnInit() {
  }

}
