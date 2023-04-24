import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../../../shared/HttpClient/_http';

@Component({
  selector: 'app-blog-details',
  templateUrl: './blogDetails.component.html',
  styleUrls: ['./blogDetails.component.css']
})
export class BlogDetailsComponent implements OnInit {

  constructor(public common: CommonService) { }

  ngOnInit() {
  }

}
