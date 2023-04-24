import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../Shared/HttpClient/_http';
import * as custom from 'src/assets/js/custom.js'

@Component({
  selector: 'app-addpost',
  templateUrl: './addpost.component.html',
  styleUrls: ['./addpost.component.css']
})
export class AddpostComponent implements OnInit {
  public blogForm: FormGroup;
  config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '15rem',
    minHeight: '5rem',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Arial',
    //toolbarHiddenButtons: [
    //  ['textColor'],
    //  ['backgroundColor'],
    //],
    customClasses: [
      {
        name: "quote",
        class: "quote",
      },
      {
        name: 'redText',
        class: 'redText'
      },
      {
        name: "titleText",
        class: "titleText",
        tag: "h1",
      },
    ]
  };
  jwtHelperService: JwtHelperService = new JwtHelperService();

  constructor(public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public Loader: NgxSpinnerService,) {
    this.addNewPostForm();
  }

  ngOnInit() {
  }
  public addNewPostForm() {
    this.blogForm = this.fb.group({
      PostTitle: [''],
      PostContent : [''], 
    })
  }
  public handleSubmit() {
    console.log(this.blogForm.value);
  }
  clickMe() {
    custom.myTest();
  }
}
