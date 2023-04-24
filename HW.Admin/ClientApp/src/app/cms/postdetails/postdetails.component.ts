import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-postdetails',
  templateUrl: './postdetails.component.html',
  styleUrls: ['./postdetails.component.css'],
})
export class PostdetailsComponent implements OnInit {
  public postId: number = 0;
  public postDetails: any;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  constructor(public toastr: ToastrService,private route: ActivatedRoute, private router: Router, public service: CommonService, public Loader: NgxSpinnerService) {
    this.postId = Number(this.route.snapshot.paramMap.get('id'));
  }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("All Posts"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.getPostDetails();
  }
  public deletePost(postId) {
    let createPostObj = {
      PostId: postId,
      PostAction : "delete",
    };
    console.log(createPostObj);
    this.service.PostData(this.service.apiRoutes.CMS.CreateUpdatePost, createPostObj, true).then(response => {
      var res = response.json();
      if (res.status == 200) {
        this.toastr.success("Post deleted successfully!", "Success");
        setTimeout(() => {
          this.router.navigateByUrl('/cms/postslist');
        }, 1000);
      }
      else {
        this.toastr.error("Something went wrong!", "Error");
      }
    })
  }
  public getPostDetails() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.CMS.GetPostDetails + "?postId=" + this.postId).subscribe(res => {
      
      this.postDetails = res.json();
      console.log(this.postDetails);
      this.Loader.hide();

    })
  }
}
