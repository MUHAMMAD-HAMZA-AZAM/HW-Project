import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../../../shared/HttpClient/_http';

@Component({
  selector: 'app-blogs',
  templateUrl: './blogs.component.html',
  styleUrls: ['./blogs.component.css']
})
export class BlogsComponent implements OnInit {

  constructor(
    public common: CommonService,

  ) { }

  ngOnInit() {
  }

  public Blogs(blog: string) {
    if (blog == 'BlogDetails') {
      this.common.NavigateToRoute(this.common.apiUrls.Supplier.BlogDetails)
    }
    else if (blog == 'BlogDetails1') {
      this.common.NavigateToRoute(this.common.apiUrls.Supplier.BlogDetails1)
    }
    else if (blog == 'BlogDetails2') {
      this.common.NavigateToRoute(this.common.apiUrls.Supplier.BlogDetails2)
    }
  }
}
