import { Component, OnInit } from '@angular/core';
import { CommonService } from '../Shared/HttpClient/_http';
import { NgxSpinnerService } from "ngx-spinner";
import { Router } from '@angular/router';
import { error } from 'pdf-lib';
import { requestCallBackVM } from '../Shared/Models/UserModel/requestCallBackVM';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ResponseVm } from '../Shared/Models/HomeModel/HomeModel';
import { httpStatus } from '../Shared/Enums/enums';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-request-call-back',
  templateUrl: './request-call-back.component.html',
  styleUrls: ['./request-call-back.component.css']
})
export class RequestCallBackComponent implements OnInit {
  public requestCallerId: any;
  public responseVm: ResponseVm;
  public requestCallBackList: requestCallBackVM[] = [];
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  constructor(public httpService: CommonService, public toastr: ToastrService, public Loader: NgxSpinnerService, private router: Router, public _modalService: NgbModal) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Request CallBack"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.getRequestCallBackList();
  }
  public getRequestCallBackList() {

    this.Loader.show();
    this.httpService.get(this.httpService.apiRoutes.Communication.requestCallBackList).subscribe(result => {
      this.requestCallBackList = result.json();
      // this.activeRequestCallBackList = this.requestCallBackList.filter(item => item.Active);
      this.Loader.hide();
    }, error => {
      console.log(error);
    });

  }

  public showDeleteModal(requestId, deleteRequestCallerModal) {
    this.requestCallerId = requestId;
    this._modalService.open(deleteRequestCallerModal);
  }

  public deleteRequestCaller() {
    this.httpService.get(this.httpService.apiRoutes.Communication.deleteRequestCaller + "?requestCallerId=" + this.requestCallerId).subscribe(result => {
      this.responseVm = result.json();
      if (this.responseVm.status == httpStatus.Ok) {
        this.toastr.error("Request Caller Deleted Successfully", "Deleted");
        this._modalService.dismissAll();
        this.getRequestCallBackList();
      }
    }, error => {
      console.log(error);
    });
  }
}
