
import { Component, OnInit, ViewChild, ElementRef, Input} from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { AngularEditorConfig } from '@kolkov/angular-editor';
//import * as CKEDITOR  from "ckeditor4-angular";
//declare var $: any;
import $ from 'jquery';
import * as custom from 'src/assets/js/custom.js'
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../Shared/HttpClient/_http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
@Component({
  selector: 'app-createpost',
  templateUrl: './createpost.component.html',
  styleUrls: ['./createpost.component.css']
})
export class CreatepostComponent implements OnInit {

  public textboxhtml: string;
  public textAreaHtml: string;
  public imageHtml: string;
  public imageAndTextElement: any;
  public imageLeft: any;
  public imageRight: any;
  public pelem: string;
  public showElements: boolean = true;
  public controlid: number = 1;
  public imgPosition = "";
  public catList: any = [];
  public posStatus : number;
  public posCat: number;
  public thumbImage: any = null;
  public allowComments: boolean=false;
  public postTitle: string;
  public postContent: string;
  public seoDescription = "";
  public summary = "";
  public ngThumbImage: any;
  public ngPostCategory: any;
  public ngPostStatus: any;
  public postOtherDetails: any = {};
  //public items = [];
  jwtHelperService: JwtHelperService = new JwtHelperService();
  @ViewChild('allowComment', { static: true }) allowComment: ElementRef;

  
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  @ViewChild('imageTextPositionRef', { static: true }) imageTextPositionRef: ElementRef;
  constructor(public router: Router, private el: ElementRef, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) {
  }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Add New Post"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.service.loadScript("assets/js/custom.js")
    //this.items = ["yes"];
    this.postOtherDetails = {
      ngPostCategory: null,
      ngPostStatus : null,
    }
    this.GetCategoryList();
  }

  public GetCategoryList() {
    
    this.service.get(this.service.apiRoutes.CMS.GetCategoryList).subscribe(response => {
      var res = response.json();
      this.catList = res;
      console.log(res);
    })
  }
  onSelect(event) {

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
    this.imageHtml =`
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
                <label for="crtFile`+this.controlid+`"><i class = "fas fa-upload"></i> Upload Image</label>
                <input type="file" id="crtFile` + this.controlid +`" class="form-control" />
                <div class="drag resize edit-image ui-widget-content invisible" id="imgcrtFile`+ this.controlid +`" >
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
              <label for="crtFile`+ this.controlid +`"><i class = "fas fa-upload"></i> Upload Image</label>
              <input type="file" id="crtFile` + this.controlid +`" class="form-control" />
              <div class="invisible imageControl" id="imgcrtFile`+ this.controlid +`" >
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
      $('#formgroup').append(this.textboxhtml);
    else if (val == 'ta')
      $('#formgroup').append(this.textAreaHtml);
    else if (val == "img")
      $('#formgroup').append(this.imageHtml);
    else if (val == "imgleft") {
      $('#formgroup').append(this.imageLeft);
    }
    else if (val == "imgRight") {
      $('#formgroup').append(this.imageRight);
    }

  }
  addNewElement() {
    this.showElements = !this.showElements;
  }
  public postStatus(e) {
    this.posStatus = Number(e.target.value);
  }
  public postCat(e) {
    this.posCat = Number(e.target.value);
  }
  public thumbImgChange(event) {
    let reader = new FileReader();
    if (event.target.files && event.target.files.length > 0) {
      let file = event.target.files[0];
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.thumbImage = reader.result;
      };
    }
  }
  public onRadioChange(e) {

    this.allowComments = e.target.checked;
  }
  submitData() {
    let createPostObj = {};
    var decodedToken = this.jwtHelperService.decodeToken(localStorage.getItem('auth_token'));
    var postTitle = $("#crt1").val();
    setTimeout(() => {
      this.postContent = $("#showData").html();
      createPostObj = {
        PostTitle: postTitle,
        CategoryId: this.posCat,
        PostStatus: this.posStatus,
        ImageBase64: this.thumbImage,
        PostContent: this.postContent,
        CommentStatus: this.allowComments,
        CreatedBy: decodedToken.UserId,
        PostAction: "add",
        Slug: this.service.convertToSlug(postTitle),
        MetaTags: this.seoDescription,
        Summary: this.summary,
      }
      console.log(createPostObj);
      this.service.PostData(this.service.apiRoutes.CMS.CreateUpdatePost, createPostObj, true).then(response => {
        var res = response.json();
        if (res.status == 200) {
          $("#showData").empty();
          this.service.NavigateToRoute("/cms/postslist")
          this.toastr.success("Post added successfully!", "Success");
        }
        else {
          this.toastr.error("Something went wrong!", "Error");
        }
        console.log(res)
      })
    }, 1000); 
  }
  public clearData() {
    $("#showData").empty();
    this.thumbImage = null;
  this.allowComments = false;
  this.allowComment.nativeElement.checked = false;
 this.showElements =true;
  }
}
