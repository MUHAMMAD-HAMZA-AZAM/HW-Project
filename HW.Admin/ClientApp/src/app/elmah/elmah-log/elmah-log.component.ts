import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../Shared/HttpClient/_http';
import { NgxSpinnerService } from "ngx-spinner";
import { SortList } from '../../Shared/Sorting/sortList';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { elmahModel } from '../../Shared/Models/elmahModel/elmahModel';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxXml2jsonService } from 'ngx-xml2json';
import { DomSanitizer } from '@angular/platform-browser';
import * as vkbeautify from 'vkbeautify';
//import { DeviceDetectorService } from 'ngx-device-detector';

@Component({
  selector: 'app-elmah-log',
  templateUrl: './elmah-log.component.html',
  styleUrls: ['./elmah-log.component.css']
})
export class ElmahLogComponent implements OnInit {

  public pipe;
  public errorId: string;
  public application: string;
  public host: string;
  public type: string;
  public source: string;
  public message: string;
  public user: string;
  public statusCode: string;
  public timeUTC: string;
  public sequence: string;
  public allXml: string;
  public clientIPAddress: string;
  public xmlformatstring: any;
  public xml: any;

  public errorID: any;

  public allXmlDetails= [];

  public errorsList=[];

  public errorDetailsModel: elmahModel = new elmahModel();
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };

  constructor(public domSantizer: DomSanitizer,
    private xamToJson: NgxXml2jsonService,
    public toaster: ToastrService,
    public service: CommonService,
    public Loader: NgxSpinnerService,
    public sortList: SortList,
    private router: Router,
    private _modalService: NgbModal) {
   
  }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Error Log"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.getElmahErrorsLogList();
  }

  public getElmahErrorsLogList() {
    this.Loader.show();
    this.service.get(this.service.apiRoutes.Elmah.ElmahErrorlogList).subscribe(result => {
      this.errorsList = result.json();
      console.log(this.errorsList);
      this.Loader.hide();
    },
      error => {
        //this.Loader.show();
        console.log(error);
        
      });
  }

  public showModal(errorID, elmahDetailsModel) {
    this._modalService.open(elmahDetailsModel, { size: 'xl' });
    this.getErrorDetails(errorID);
  }

  public getErrorDetails(getErrorId) {
    this.errorID = getErrorId;
    this.service.get(this.service.apiRoutes.Elmah.ElmahErrorDetailsById + "?errorID=" + this.errorID).subscribe(result => {
      console.log(result);
      this.errorDetailsModel = result.json();
      const xxml = this.errorDetailsModel.allXml;
      const parser = new DOMParser();
      const xml = parser.parseFromString(this.xmlformatstring, 'application/xml');
       this.xml = xml.documentElement;
      this.xmlformatstring = this.domSantizer.bypassSecurityTrustHtml(xxml);
      //this.xmlformatstring = vkbeautify.xml(xxml)
      //console.log(this.xmlformatstring);
      //console.log(this.xamToJson.xmlToJson());

    });

  }
 

}
