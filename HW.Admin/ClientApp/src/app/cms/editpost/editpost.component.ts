import { Component, HostListener, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from "ngx-spinner";
import { CommonService } from '../../Shared/HttpClient/_http';
import $ from 'jquery';
import { ToastrService } from 'ngx-toastr';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-editpost',
  templateUrl: './editpost.component.html',
  styleUrls: ['./editpost.component.css']
})
export class EditpostComponent implements OnInit {
  @HostListener('change', ['$event']) change(event) {
    this.handleFileChange(event);
  }
  public postId: number = 0;
  public postDetails: any;
  public postContents: any;
  public imgSrcId: any;

  public posStatus: number;
  public posCat: number;
  public thumbImage: any = null;
  public editThumbImage: any;
  public allowComments: boolean;
  public catList: any = [];
  public postContent: any;
  // public items = [];
  public seoDescription = "";
  public postTitle = "";
  public summary = "";

  public textboxhtml: string;
  public textAreaHtml: string;
  public imageHtml: string;
  public imageAndTextElement: any;
  public imageLeft: any;
  public imageRight: any;
  public controlid: number = 1;
  public showElements: boolean = true;
  jwtHelperService: JwtHelperService = new JwtHelperService();

  constructor(public toastr: ToastrService,private route: ActivatedRoute, private router: Router, public service: CommonService, public Loader: NgxSpinnerService) {
    this.postId = Number(this.route.snapshot.paramMap.get('id'));
  }

  ngOnInit() {
    this.getPostDetails();
    this.getCategoryList();
  }
  addNewElement() {
    this.showElements = !this.showElements;
  }
  public createform(e) {
    this.controlid += 1;
    this.textboxhtml = '<div class="row"> <div class="col-xl-12 elementHtml"><div class="form-group"> <input type = "text" class="form-control getControlData" autocomplete="off" id="crt' + this.controlid + '" (focus)="handleFoucus($event)" placeholder="Start writing or typing" /></div></div></div>';
    this.textAreaHtml = `
    <div class="removeCreatedElement">
      <div class="row">
        <i class="fas fa-times clsRemoveElement"></i>
        <div class="col-xl-12">
          <div class="form-group">
            <label class='control-lable-name'>Textarea</label>
          </div>
        </div>
      </div>
      <div class="row mtb15">
        <div class="col-xl-12 elementHtml">
          <div class="form-group">
            <textarea rows="3" name="crt` + this.controlid + `"  placeholder="Start writing or typing" class="form-control getControlData" id="crt` + this.controlid + `"></textarea>
          </div>
        </div>
      </div>
    </div>`
    this.imageHtml = `
      <div class="removeCreatedElement">
          <div class="row">
            <i class="fas fa-times clsRemoveElement"></i>
            <div class="col-xl-12">
              <div class="form-group">
                <label class='control-lable-name'>Image</label>
              </div>
            </div>
          </div>
          <div class="row mtb15">
            <div class="col-xl-12 elementHtml">
              <div class="form-group onchangeEvent">
                <label for="crtFile`+ this.controlid + `"><i class = "fas fa-upload"></i> Upload Image</label>
                <input type="file" id="crtFile` + this.controlid + `" class="form-control" />
                <div class="drag resize edit-image ui-widget-content invisible" id="imgcrtFile`+ this.controlid + `" >
                  <img src="#" class="getControlData" style="width:100%;height:700px;object-fit:cover;padding-bottom:25px;" alt="Uplaoded Image"/>
                </div>
              </div>
             </div>
          </div>
      </div>`
    this.imageLeft = `
      <div class="imageTextPosition getControlData imgLeft mtb15 removeCreatedElement" id="imgLeft">
        <i class="fas fa-times clsRemoveElement"></i>
        <div class="row">
          <div class="col-xl-12">
            <div class="form-group">
              <label class='control-lable-name'>Image Left text right</label>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-xl-6">
            <div class="imagePosition onchangeEvent">
              <label for="crtFile`+ this.controlid + `"><i class = "fas fa-upload"></i> Upload Image</label>
              <input type="file" id="crtFile` + this.controlid + `" class="form-control" />
              <div class="invisible imageControl" id="imgcrtFile`+ this.controlid + `" >
                <img src="#" alt="Uplaoded Image" />
              </div>
            </div>
          </div>
          <div class="col-xl-6">
            <div class="textPosition">
              <textarea rows="3" name="crt` + this.controlid + `"  placeholder="Start writing or typing" class="form-control" id="crt` + this.controlid + `"> </textarea>
            </div>
          </div>
        </div>
      </div>`
    this.imageRight = `
      <div class="imageTextPosition getControlData imgRight mtb15 removeCreatedElement" id="imgRight">
        <i class="fas fa-times clsRemoveElement"></i>
        <div class="row">
          <div class="col-xl-12">
            <div class="form-group">
              <label class='control-lable-name'>Image right text left</label>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-xl-6">
            <div class="textPosition">
              <textarea rows="3" name="crt` + this.controlid + `"  placeholder="Start writing or typing" class="form-control" id="crt` + this.controlid + `"> </textarea>
            </div>
          </div>
          <div class="col-xl-6">
            <div class="imagePosition onchangeEvent">
              <label for="crtFile`+ this.controlid + `"><i class = "fas fa-upload"></i> Upload Image</label>
              <input type="file" id="crtFile` + this.controlid + `" class="form-control" />
              <div class="invisible imageControl" id="imgcrtFile`+ this.controlid + `" >
                <img src="#" alt="Uplaoded Image" />
              </div>
            </div>
          </div>
        </div>
      </div>
     `
    var val = e.target.value;
    if (val == "tb")
      $('#editFormGroup').append(this.textboxhtml);
    else if (val == 'ta')
      $('#editFormGroup').append(this.textAreaHtml);
    else if (val == "img")
      $('#editFormGroup').append(this.imageHtml);
    else if (val == "imgleft") {
      $('#editFormGroup').append(this.imageLeft);
    }
    else if (val == "imgRight") {
      $('#editFormGroup').append(this.imageRight);
    }

  }

  public handleFileChange(event) {
    this.imgSrcId = "#eimg" + event.target.id;
    this.readImage(event , "jq"); //Function to read file data 
  }
  public postStatus(e) {
    this.posStatus = Number(e.target.value);
  }
  public postCat(e) {
    this.posCat = Number(e.target.value);
  }
  public onRadioChange(e) {
    
    this.allowComments = e.target.checked;
  }
  submitData() {
    let createPostObj = {};
    var decodedToken = this.jwtHelperService.decodeToken(localStorage.getItem('auth_token'));
    var postTitle = $("#eTitleId").val();
    setTimeout(() => {
         
      this.postContent = $("#showData").html();
      createPostObj = {
        PostId: this.postDetails.postId,
        PostTitle: postTitle,
        CategoryId: this.posCat,
        PostStatus: this.posStatus,
        ImageBase64: this.editThumbImage,
        PostContent: this.postContent,
        CommentStatus: this.allowComments,
        ModifiedBy: decodedToken.UserId,
        PostAction: "edit",
        Slug: this.service.convertToSlug(postTitle),
        MetaTags: this.seoDescription,
        Summary: this.summary,
      }
      this.service.PostData(this.service.apiRoutes.CMS.CreateUpdatePost, createPostObj, true).then(response => {
        var res = response.json();
        if (res.status == 200) {
          this.toastr.success("Post updated successfully!", "Success");
          $("#showData").empty();
          this.router.navigate(['/cms/postdetails/', this.postDetails.postId]);
        }
        else {
          this.toastr.error("Something went wrong!", "Error");  
        }
        console.log(res)
      })
    }, 1000);
  }

  public setValuesToDropDowns() {
    
    this.thumbImage = this.service.transform(this.postDetails.headerImage);
    this.editThumbImage = "data:image/jpeg;base64," + this.postDetails.headerImage;
    $("#postCategory option[value=" + this.postDetails.categoryId + "]").prop("selected", true);
    $("#postStatus option[value=" + this.postDetails.postStatus + "]").prop("selected", true);
    $("#allowComment").prop('checked', this.postDetails.commentStatus);
    this.allowComments = this.postDetails.commentStatus;
    this.posCat = this.postDetails.categoryId;
    this.posStatus = this.postDetails.postStatus;
  }
  public thumbImgChange(event) {
    this.readImage(event , "");
  }
  public onSelect(e) {

  }
  public getCategoryList() {
    this.service.get(this.service.apiRoutes.CMS.GetCategoryList).subscribe(response => {
      var res = response.json();
      this.catList = res;
      //console.log(res);
    })
  }
  public getPostDetails() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.CMS.GetPostDetails + "?postId=" + this.postId).subscribe(res => {
      this.postDetails = res.json();
      this.postContents = this.postDetails.postContent;
      this.seoDescription = this.postDetails.metaTags;
      this.summary = this.postDetails.summary;
      this.postTitle = this.postDetails.postTitle;
      $("#editPostData").html(this.postContents);
      this.setValuesToDropDowns();
      this.Loader.hide();
      this.service.loadScript("assets/js/custom.js");
    })
  }
  public readImage(event, readeType:string) {
    
    let reader = new FileReader();
    if (event.target.files && event.target.files.length > 0) {
      let file = event.target.files[0];
      reader.readAsDataURL(file);
      reader.onload = () => {
        if (readeType == "jq") {
          $(this.imgSrcId).find('img').attr('src', reader.result);
        }
        else {
          this.thumbImage = reader.result;
          this.editThumbImage = reader.result;
        }
      };
    }
  }

}
