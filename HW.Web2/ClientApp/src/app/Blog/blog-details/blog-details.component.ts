import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ShareService } from 'ngx-sharebuttons';
import { CommonService } from '../../shared/HttpClient/_http';
import { IPostVM } from '../../shared/Interface/tradesman';
import { Meta, Title } from '@angular/platform-browser';
import { metaTagsService } from '../../shared/CommonServices/seo-dynamictags.service';

@Component({
  selector: 'app-blog-details',
  templateUrl: './blog-details.component.html',
  styleUrls: ['./blog-details.component.css']
})
export class BlogDetailsComponent implements OnInit {
  public postDetails: IPostVM;
  public postId: number = 0;
  constructor(private _metaService: metaTagsService,public common: CommonService, private route: ActivatedRoute, public Loader: NgxSpinnerService, private router: Router, private share: ShareService) {
    this.postId = Number(this.route.snapshot.paramMap.get('id'));
    this.postDetails = {} as IPostVM;
  }

  ngOnInit() {
    this.share.addButton("Facebook", {
      type: 'customButton', 
      text: 'My Custom Button',
      icon: ['fas', 'fas-comments-dollar'],
    })
    this.getPostDetails();
  }
  public getPostDetails() {
    this.common.get(this.common.apiRoutes.Blog.GetPostDetails + "?postId=" + this.postId).subscribe(response => {
      this.postDetails = <IPostVM>response;
      this._metaService.updateTags(this.postDetails.postTitle, this.postDetails.postTitle, this.postDetails.metaTags,this.postDetails.postTitle, this.postDetails.postTitle, this.postDetails.metaTags);
    });
  }

}
