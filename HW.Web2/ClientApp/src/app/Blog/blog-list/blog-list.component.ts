import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { metaTagsService } from '../../shared/CommonServices/seo-dynamictags.service';
import { CommonService } from '../../shared/HttpClient/_http';
import { IPostVM } from '../../shared/Interface/tradesman';
import { IPageSeoVM } from '../../shared/Enums/Interface';
@Component({
  selector: 'app-blog-list',
  templateUrl: './blog-list.component.html',
  styleUrls: ['./blog-list.component.css']
})
export class BlogListComponent implements OnInit {
  public postList: IPostVM[] = [];
  public postDetails: IPostVM;
  public pageNumber = 1;
  public pageSize = 2;
  public postCount = 0;
  public blogsListBit: boolean = false;
  public notFound: boolean = false;
  constructor(public common: CommonService, private activeRouter: ActivatedRoute, public Loader: NgxSpinnerService, private router: Router, private _metaService: metaTagsService) {
    this.postDetails = {} as IPostVM;
  }

  ngOnInit() {

    this.getPostList();
    this.bindMetaTags();
  }
  public bindMetaTags() {
    //this._metaService.updateTitle("Blog");
    //this._metaService.updateKeyWords("Blog, Post,Lists");
    //this._metaService.updateDescription("Blog for hoomwork");
    this.common.get(this.common.apiRoutes.CMS.GetSeoPageById + "?pageId=9").subscribe(response => {
      debugger;
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description, res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }
  public getPostList() {
    //this.Loader.show();
    let obj = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
    }
    debugger;
    this.common.post(this.common.apiRoutes.Blog.GetBlogList, obj).subscribe(response => {
      this.postList = <IPostVM[]>response;

      if (this.postList.length==0) {
        this.blogsListBit = true;
        this.notFound = true;
      }
      this.postDetails = this.postList[0];
      this.postCount = this.postList[0].postsCount;
      this.Loader.hide();
      //console.log(response.json());
      this.notFound = false;
    });
  }
  getPostByPage(page: number) {
    console.log(page);
    let obj = {
      pageNumber: page,
      pageSize: this.pageSize,
    }
    this.common.PostData(this.common.apiRoutes.Blog.GetBlogList, obj).then(response => {
      this.postList = <IPostVM[]>response ;
      this.postCount = this.postList[0].postsCount;
    });
  }
  //public elem() {
  //  let box = document.createElement("div");
  //  box.innerHTML = this.postDetails.postContent;
  //  console.log(box); 
  //  //box.getElementsByTagName('p');
  //  console.log(box.getElementsByTagName('p'));
  //}


}
