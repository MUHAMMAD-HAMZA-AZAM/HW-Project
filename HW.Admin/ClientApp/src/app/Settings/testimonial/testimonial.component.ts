import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { IResponseVM, StatusCode } from '../../Shared/Enums/enums';
import { CommonService } from '../../Shared/HttpClient/_http';

@Component({
  selector: 'app-testimonial',
  templateUrl: './testimonial.component.html',
  styleUrls: ['./testimonial.component.css']
})
export class TestimonialComponent implements OnInit {

  public modelName: string;
  public btnReset: boolean = false;
  public formData: any;
  public appValForm: FormGroup;
  public decodedtoken: any;
  public banklist: any = [];
  public testimonialslist: any = [];
  public testimonialsTypeslist: any = [];
  public response: IResponseVM;
  public allowview;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  jwtHelperService: JwtHelperService = new JwtHelperService();
  constructor(
    public _modalService: NgbModal,
    public toaster: ToastrService,
    public _httpService: CommonService,
    public Loader: NgxSpinnerService,
    public fb: FormBuilder,
    private router: Router) { }

  ngOnInit(): void {
    this.userRole = JSON.parse(localStorage.getItem("Testimonials"));
    this.decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    this.appValForm = this.fb.group({
      testimonialsId: [0],
      title: ['', [Validators.required]],
      url: ['', [Validators.required]],
      userType: [null, [Validators.required]],
      description: [''],
      active: [''],
      testimonialtype: [null, [Validators.required]],
      createdOn: [''],
      createdBy: [''],
      modifiedOn: [''],
      modifiedBy: ['']
    });
    debugger;
    this.getTimonialsTypes();
    this.getTestimonialsList();
  }

  /* ..................Get getTestimonialsList List......................... */
  public getTestimonialsList() {
    debugger;
    this.Loader.show();
    this._httpService.get(this._httpService.apiRoutes.UserManagement.GetTestimonialsListForAdmin).subscribe(result => {
      this.response = result.json();
      if (this.response.status == StatusCode.OK) {
       this.testimonialslist = this.response.resultData;
        this.Loader.hide();
      }

    })
  }

  /* ..................Get  Testimonials Types List......................... */
  public getTimonialsTypes() {
    debugger;
    this.Loader.show();
    this._httpService.get(this._httpService.apiRoutes.UserManagement.GetTimonialsTypesListForAdmin).subscribe(result => {
      this.response = result.json();
      if (this.response.status == StatusCode.OK) {
        this.testimonialsTypeslist = this.response.resultData;
        this.Loader.hide();
      }

    })
  }
  /*  ....................get controls...................*/
  get f() {
    return this.appValForm.controls;
  }

  public delete(id) {
    debugger;
    /*  this.decodedtoken.UserId */
    this.Loader.show();
    let obj = {
      testimonialsId: id,
      userId: this.decodedtoken.UserId

    };
    this._httpService.PostData(this._httpService.apiRoutes.UserManagement.DeleteTesimoaialsStatus, JSON.stringify(obj), true).then(res => {
      this.response = res.json();
      if (this.response.status == StatusCode.OK) {
        this._modalService.dismissAll();
        this.getTestimonialsList();
        this.resetForm();
        this.btnReset = false;
        this.Loader.hide();
        this.toaster.success(this.response.message, "Success");
      }
    }, error => {
      this.Loader.show();
      console.log(error);
    });

  }

  //-------------------  Show Modal For Add Testimonials/........................
  public showModal(content) {
    this.modelName = "Add  new Testimonials";
    this._modalService.open(content, {  size: 'lg', backdrop: 'static', keyboard: false });
  }

  //-------------------  Show Modal For Update Testimonials.......................//
  public update(data, content) {
    debugger;
    this.Loader.show();
    this.btnReset = true;
    this.showModal(content);
    this.modelName = "Update Testimonials";
    this.appValForm.patchValue(data);
    this.Loader.hide();
  }
  /* ................. add update Testimonials.................*/
  public saveAndUpdateTestimonials() {
    debugger;
    this.formData = this.appValForm.value;
    this.formData.userId = this.decodedtoken.UserId;
    this.Loader.show();
    this._httpService.PostData(this._httpService.apiRoutes.UserManagement.AddUpdateTestinomials, JSON.stringify(this.formData), true).then(res => {
      this.response = res.json();
      if (this.response.status == StatusCode.OK) {
       
          this.toaster.success(this.response.message, "Success");
    
        this._modalService.dismissAll();
    this.getTestimonialsList();

        this.resetForm();
        this.btnReset = false;
        this.Loader.hide();
      }
    }, error => {
      this.Loader.show();
      console.log(error);
    });

  }
  //-------------------  Reset Form...................
  public resetForm() {
    this.appValForm.reset();
    this.appValForm.controls.testimonialsId.setValue(0);
  }


  //-------------------  Close Modal............
  public closeModal() {
    this._modalService.dismissAll();
    this.resetForm();
    this.btnReset = false;
  }
}

