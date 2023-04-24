import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {
  public postsList = [];
  public pageNumber = 1;
  public pageSize = 10;
  public postCount = 0;

  constructor(private route: ActivatedRoute, private router: Router,public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) { }

  ngOnInit() {
debugger;
    this.getPostsList();
  }
  public getPostsList() {
    
    this.Loader.show();
    let obj = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
    }
    this.service.PostData(this.service.apiRoutes.CMS.GetPostsList, obj).then(res => {
      
      console.log(res)
      this.postsList = res.json();
      this.postCount = this.postsList[0].postsCount;
      console.log(this.postsList);
      this.Loader.hide();
    })
  }
  getPostByPage(page) {
    console.log(page);
    let obj = {
      pageNumber: page,
      pageSize: this.pageSize,
    }
    this.service.PostData(this.service.apiRoutes.CMS.GetPostsList, obj).then(response => {
      this.postsList = response.json();
      this.postCount = this.postsList[0].postsCount;
    });
  }

}
