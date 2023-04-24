import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { debounce, debounceTime } from 'rxjs/operators';
import { httpStatus } from '../../Shared/Enums/enums';
import { UploadFilesServiceService } from '../../Shared/HttpClient/upload-files-service.service';
import { CommonService } from '../../Shared/HttpClient/_http';
@Component({
  selector: 'app-supplier-slider',
  templateUrl: './supplier-slider.component.html',
  styleUrls: ['./supplier-slider.component.css']
})
export class SupplierSliderComponent implements OnInit {
  FOLDERPATH = 'photos/mainSlider/'
  public isUpdate: boolean = false;
  public updateData: any;
  public supplierSliderList: any = [];
  public sliderImagesList: any = [];
  public sliderForm: FormGroup;
  public jwtHelperService: JwtHelperService = new JwtHelperService();
  public formData: any;
  public logoImage;
  public selectedFile: any;
  public fileName;
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };

  constructor(public router: Router, public fb: FormBuilder, public toastr: ToastrService, public service: CommonService, public _fileService: UploadFilesServiceService, public Loader: NgxSpinnerService,) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Tooltip Form"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.getsupplierSliderList();
    this.sliderForm = this.fb.group({
      id: [0],
      sliderImage: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      localImageName:[''],
      status: [false],
    })
  }
  public getsupplierSliderList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Supplier.GetSupplierSliderList).pipe(debounceTime(1000)).subscribe(res => {
      let data = JSON.parse(res.json());
      this.supplierSliderList = data.resultData;
                     
      console.log(this.supplierSliderList)
      this.supplierSliderList.forEach((x, i) => {
        this._fileService.getFileByName(x.imagePath + x.imageName).then(file => {
          if (file) {
            this.supplierSliderList[i].imageName = file;
          }
          else {
            this.supplierSliderList[i].imageName = "";
          }
          
        });
         
      });
      
    });
    this.Loader.hide();
  }
  public Save() {
                   
    this.Loader.show();
    var decodedtoken = this.jwtHelperService.decodeToken(localStorage.getItem("auth_token"));
    this.formData = this.sliderForm.value;
    console.log(this.formData);
    if (this.selectedFile) {
      let fileExtension = this.selectedFile.name.split('.').pop();
      this.fileName = `${this.selectedFile.name}${this._fileService.dateTimeInNumber()}.${fileExtension}`;
      this.formData.imageName = this.fileName;
    }
    else {
      this.formData.imageName = this.formData.localImageName;
    }
    this.formData.imagePath = this.FOLDERPATH;
    this.formData.CreatedBy = decodedtoken.UserId;
                   
    this.service.post(this.service.apiRoutes.Supplier.AddUpdateSupplierSlider, JSON.stringify(this.formData)).subscribe(res => {
      let response = res.json();
                     
      if (response.status == httpStatus.Ok) {
        this._fileService.uploadFile(this.selectedFile, this.fileName);
        this.toastr.success(response.message, "Success");
        this.getsupplierSliderList();
        this.resetForm();
        this.Loader.hide();
      }
      else if (response.status == 400) {
        this.toastr.error(response.message, "Error");
        this.resetForm();
        this.Loader.hide();
      }
    });
    this.Loader.hide();
  }
  update(data) {
    console.log(data);
    var datePipe = new DatePipe("en-US");
    data.startDate = datePipe.transform(data.startDate, 'yyyy-MM-dd');
    data.endDate = datePipe.transform(data.endDate, 'yyyy-MM-dd');
    this.logoImage = data.imageName.split(',')[1];
    this.sliderForm.patchValue(data);

    this.sliderForm.controls['sliderImage'].clearValidators();
    this.sliderForm.controls['sliderImage'].updateValueAndValidity();
  }
  

  get f() {
    return this.sliderForm.controls;
  }
  resetForm() {
    this.sliderForm.reset();
    this.sliderForm.controls['id'].setValue(0);
    this.logoImage = '';
  }
  fileChangeEvent(event: any) {
    this.selectedFile = event.target.files[0]
    console.log(this.selectedFile);
    this._fileService.getBase64(event.target.files[0]).then(x => {
      let img = String(x);
      this.logoImage = img.split(',')[1];
    });
  }
  

}

