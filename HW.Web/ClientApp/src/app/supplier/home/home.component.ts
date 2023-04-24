import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../shared/HttpClient/_http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private common: CommonService) {
  }

  ngOnInit() {
  }
  public Blogs(blog) {
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
