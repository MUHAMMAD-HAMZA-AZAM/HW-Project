import { Component, OnInit, ViewChild, NgZone, ElementRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '../../shared/HttpClient/_http';
import { ActivatedRoute } from '@angular/router';
import { httpStatus, CommonErrors } from '../../shared/Enums/enums';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { MapsAPILoader, MouseEvent } from '@agm/core';
import { ContactUsVm } from '../../models/supplierModels/supplierModels';
import { getTestBed } from '@angular/core/testing';
import { IPageSeoVM } from '../../shared/Enums/Interface';
import { metaTagsService } from '../../shared/CommonServices/seo-dynamictags.service';
@Component({
  selector: 'app-contact-us',
  templateUrl: './contactUs.component.html',
  styleUrls: ['./contactUs.component.css']
})
export class ContactUsComponent implements OnInit {
  public submittedForm = false;
  public appValForm: FormGroup;
  public latitude = 31.4728;
  public longitude = 74.3278;
  public zoom: number = 15;
  public address: string="";
  //public geoCoder = new google.maps.Geocoder();
  @ViewChild('search', { static: true })
  public searchElementRef: ElementRef;
  @ViewChild('ContactSms', { static: true }) ContactSms: ModalDirective;
  public emailVM: ContactUsVm = new ContactUsVm();

  constructor(
    public common: CommonService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,

    public mapsAPILoader: MapsAPILoader,
    public ngZone: NgZone,
    private _metaService: metaTagsService
  ) {
    this.searchElementRef = {} as ElementRef;
    this.ContactSms = {} as ModalDirective;
    this.appValForm = {} as FormGroup;
  }

  ngOnInit() {
    
   // document.getElementById("headerText").innerHTML ="Contact Us";
    this.appValForm = this.formBuilder.group({
      name: ['', Validators.required],
      phone: ['', [Validators.required, Validators.pattern(/^[0-9]*$/)]],
      email: ['', [Validators.required, Validators.email]],
      subject: ['', Validators.required],
      message: ['', Validators.required],
    });
    this.bindMetaTags();
    // this.startLocation();
  }

  get f() {
    return this.appValForm.controls;
  }

  public email: string="";

  public Save() {
    
    this.submittedForm = true;
    if (this.appValForm.invalid) {
      return;
    }
    var data = this.appValForm.value;
    this.emailVM.name = data.name;
    this.emailVM.subject = data.subject;
    this.emailVM.body = data.message
    this.emailVM.phone = data.phone;
    this.emailVM.emailAddresses = [];
    this.emailVM.emailAddresses.push(data.email)

    this.common.PostData(this.common.apiRoutes.Communication.SendUsAMessage, this.emailVM, true).then(result => {
      
      if (status = httpStatus.Ok) {
        this.ContactSms.show();
      }
    },
      error => {
        console.log(error);
        this.common.Notification.error(CommonErrors.commonErrorMessage);
      });
  }

  public Close() {
    this.ContactSms.hide();
    this.appValForm.reset();
    this.appValForm = this.formBuilder.group({
      name: ['', Validators.required],
      phone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      subject: ['', Validators.required],
      message: ['', Validators.required],
    });
    this.submittedForm = false;
  }
  charOnly(event: KeyboardEvent): boolean {
    
    const charCode = (event.which) ? event.which : event.keyCode;
    if ((charCode > 47 && charCode < 58)) {
      return false;

    //if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      
    }

    return true;

  }

  numberOnly(event: KeyboardEvent): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;

  }
  public bindMetaTags() {
    this.common.get(this.common.apiRoutes.CMS.GetSeoPageById + "?pageId=4").subscribe(response => {
      let res: IPageSeoVM = <IPageSeoVM>response.resultData[0];
      this._metaService.updateTags(res.pageName, res.pageTitle, res.description, res.keywords, res.ogTitle, res.ogDescription, res.canonical);
    });
  }

}
