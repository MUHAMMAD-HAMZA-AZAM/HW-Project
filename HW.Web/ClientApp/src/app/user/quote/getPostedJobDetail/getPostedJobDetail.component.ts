import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonService } from '../../../shared/HttpClient/_http';
import { httpStatus } from '../../../shared/Enums/enums';
import { ActivatedRoute, Params } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { PostedJobDetailVM, PostedJobsVM, ImageVM, GetQuotes } from '../../../models/userModels/userModels';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { INgxMyDpOptions } from 'ngx-mydatepicker';
import { NgxImageCompressService } from 'ngx-image-compress';
@Component({
  selector: 'app-get-posted-job-detail',
  templateUrl: './getPostedJobDetail.component.html',

})
export class GetPostedJobDetailComponent implements OnInit {
  public jobQuotationId: number;
  public submittedForm = false;
  public meridian = true;
  public appValForm: FormGroup;
  public CitiesList = [];
  public SubCategoriesList = [];
  public ImagesList = [];
  public jobStartTime;
  public imageContent: string;
  public readOnly = "readOnly";
  public imgResultBeforeCompress: string;
  public imgResultAfterCompress: string;
  //public postJobDetail: PostedJobDetailVM = new PostedJobDetailVM();
  public postJobDetail: GetQuotes = new GetQuotes();

  @ViewChild('CancelJob', { static: true }) CancelJob: ModalDirective;
  @ViewChild('ImageModel', { static: true }) ImageModel: ModalDirective;
  public myOptions: INgxMyDpOptions = {
    dateFormat: 'dd-mm-yyyy',
    alignSelectorRight: false,
    selectorHeight: 'apx'
  };
  public imageSubmitted: boolean;
  public listOfImages: ImageVM[] = [];
  public listofFiles: any[] = [];
  public imageVm: ImageVM = new ImageVM();
  public sizeOfOriginalImage: number;
  public sizeOFCompressedImage: number;
  public localUrl: any;
  public localCompressedURl: any;
  public selectedTime;



  constructor(
    public common: CommonService,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private imageCompress: NgxImageCompressService,
  ) { }

  ngOnInit() {
    this.appValForm = this.formBuilder.group({
      address: ['', [Validators.required]],
      area: ['', [Validators.required]],
      budget: [0, [Validators.required]],
      catagory: [0],
      cityId: [0],
      jobDescription: ['', [Validators.required]],
      jobStartingTime: ['', [Validators.required]],
      startingDateTime: ['', [Validators.required]],
      subSkillId: [0],
      title: ['', [Validators.required]],
      quotesQuantity: [0],
    });

    this.route.queryParams.subscribe((params: Params) => {
      this.jobQuotationId = params['jobQuotationId'];
      if (this.jobQuotationId > 0)
        this.PopulateData();
    });
  }

  get f() {
    return this.appValForm.controls;
  }
  public DataList: ImageVM[] = [];
  public Save() {
    debugger
    this.DataList = [];
    this.submittedForm = true;
    if (this.appValForm.invalid) {
      return;
    }
    if (this.listOfImages.length > 0) {
      for (var i in this.listOfImages) {
        let as = <ImageVM>{};
        as.id = this.jobQuotationId;
        as.imageBase64 = this.listOfImages[i].imageBase64;
        this.DataList.push(as);
      }
    }
    else {
      this.postJobDetail.imageVMs = [];
    }
    var data = this.appValForm.value;
    this.postJobDetail.imageVMs = this.DataList;
    this.postJobDetail.jobQuotationId = this.jobQuotationId;
    this.postJobDetail.workTitle = data.title;
    this.postJobDetail.jobDescription = data.jobDescription;
    this.postJobDetail.subCategoryId = data.subSkillId;
    this.postJobDetail.numberOfBids = data.quotesQuantity;
    this.postJobDetail.cityId = data.cityId;
    this.postJobDetail.area = data.area;
    this.postJobDetail.town = data.area;
    this.postJobDetail.streetAddress = data.address;
    this.postJobDetail.budget = data.budget;
    this.jobStartTime = data.jobStartingTime;
    var jobStartDate = this.common.formatDate(this.appValForm.controls.startingDateTime.value.jsdate, "YYYY-MM-DD");
    this.postJobDetail.jobstartDateTime = new Date(jobStartDate + " " + this.jobStartTime.hour + ":" + this.jobStartTime.minute + ":" + this.jobStartTime.second);
    this.common.PostData(this.common.apiRoutes.Customers.JobQuotationsWeb, this.postJobDetail, true).then(result => {
      debugger;
      if (status = httpStatus.Ok) {
        var data = result.json();
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    });
  }

  public ToggleMeridian() {
    debugger
    this.meridian = !this.meridian;
  }
  public time;
  public PopulateData() {
    this.common.GetData(this.common.apiRoutes.Jobs.GetPostedJobDetailByJobQuotationId + "?jobQuotationId=" + this.jobQuotationId, true).then(result => {
      debugger;
      if (status = httpStatus.Ok) {
        var data = result.json();
        this.CitiesList = data.citiesList;
        this.SubCategoriesList = data.subCatagory;
        this.ImagesList = data.images;
        console.log(data);
        this.appValForm.patchValue(data);
        this.appValForm.controls.startingDateTime.setValue({ jsdate: new Date(data.startingDateTime) });
        let time = this.common.formatDate(data.startingDateTime, "HH:mm");
        let timeStrings = time.split(':');
        let hours = parseFloat(timeStrings[0]);
        let mint = parseFloat(timeStrings[1]);
        let TimeObjs = {
          hour: hours,
          minute: mint,
          second: 0
        }
        this.appValForm.controls.jobStartingTime.setValue(TimeObjs);
        for (var i = 0; i < this.ImagesList.length; i++) {
          if (this.ImagesList[i].imageContent != null) {
            var imgContent = 'data:image/png;base64,' + this.ImagesList[i].imageContent;
            this.ImagesList[i].imageBase64 = imgContent.toString();
          }
        }
        debugger
        this.listOfImages = this.ImagesList;
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    });
  }

  public DeleteJob() {
    debugger;
    this.common.GetData(this.common.apiRoutes.Users.CancelJob + "?jobQuotationId=" + this.jobQuotationId, true).then(result => {
      debugger;
      if (status = httpStatus.Ok) {
        this.common.NavigateToRoute(this.common.apiUrls.User.GetPostedJobs);
      }
      else {
        this.common.Notification.error("Some thing went wrong.");
      }
    });
  }






  public OnSelectFile(event) {
    debugger
    this.listOfImages = [];
    this.postJobDetail.imageVMs = [];
    if (event.target.files.length > 4) {
      this.imageSubmitted = true;
      this.common.Notification.warning("Limit Exceeded! You can select 4 more image(s).")
      return;
    }
    else {
      for (var i = 0; i < event.target.files.length; i++) {
        var reader = new FileReader();
        var file = event.target.files[i];
        this.imageVm.filePath = file['name'];
        reader.onload = (event: any) => {
          this.listofFiles.push(event.target.result);
          this.imageVm.localUrl = event.target.result;
          this.compressFile(this.imageVm.localUrl, this.imageVm.filePath, this.imageVm, i);
        }
        reader.readAsDataURL(event.target.files[i]);
      }
    }
  }

  public dataURItoBlob(dataURI) {
    const byteString = window.atob(dataURI);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8Array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) {
      int8Array[i] = byteString.charCodeAt(i);
    }
    const blob = new Blob([int8Array], { type: 'image/png' });
    return blob;
  }



  public compressFile(image, fileName, imageVms, index) {
    debugger
    var orientation = -1;
    this.sizeOfOriginalImage = this.imageCompress.byteCount(image) / (1024 * 1024);
    console.log('Size in bytes is now:', this.sizeOfOriginalImage);
    this.imageCompress.compressFile(image, orientation, 50, 50).then(
      result => {
        this.imgResultAfterCompress = result;
        this.localCompressedURl = result;
        this.sizeOFCompressedImage = this.imageCompress.byteCount(result) / (1024 * 1024)
        console.log('Size in bytes after compression:', this.sizeOFCompressedImage);
        this.imageVm = new ImageVM();
        const imageBlob = this.dataURItoBlob(this.imgResultAfterCompress.split(',')[1]);
        this.imageVm.filePath = fileName;
        this.imageVm.imageBase64 = result;
        this.imageVm.localUrl = null;
        this.listOfImages.push(this.imageVm);
      })
  }


  public ConfirmModel() {
    this.CancelJob.show();
  }

  public CloseModel() {
    this.CancelJob.hide();
  }
  public ImagePopUp(image) {
    debugger
    this.imageContent = image;
    this.ImageModel.show()

  }
  public Close() {
    this.ImageModel.hide();
  }
  public EditJob() {
    this.readOnly = "";
  }

}
