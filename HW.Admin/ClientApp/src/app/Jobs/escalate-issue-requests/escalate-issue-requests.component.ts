import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { escalateIssueStatus, httpStatus } from '../../Shared/Enums/enums';
import { CommonService } from '../../Shared/HttpClient/_http';
import { SortList } from '../../Shared/Sorting/sortList';

@Component({
  selector: 'app-escalate-issue-requests',
  templateUrl: './escalate-issue-requests.component.html',
  styleUrls: ['./escalate-issue-requests.component.css']
})
export class EscalateIssueRequestsComponent implements OnInit {
  public escalateFilterForm: FormGroup;
  public allowview; 
  public escalateIssueId;
  public escalateFormData: any;
  public escalateIssuesRequestList = [];
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  constructor(
    public _modalService: NgbModal,
    public toastr: ToastrService,
    public httpService: CommonService,
    public Loader: NgxSpinnerService,
    public sortList: SortList,
    public fb: FormBuilder,
    private router: Router) { }

  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Escalate Issue Requests"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.escalateIssueFilterForm();
    this.populateEscalateIssuesData();
  }

  public populateEscalateIssuesData() {
    debugger;
    this.escalateFormData = this.escalateFilterForm.value;
    this.httpService.PostData(this.httpService.apiRoutes.Jobs.EscalateIssuesRequestList, this.escalateFormData, true).then(result => {
      this.escalateIssuesRequestList = result.json();
      if (!this.escalateIssuesRequestList) {
        this.toastr.error("No Data Found","Error");
      }
      this.Loader.hide();
    }, error => {
      this.Loader.show();
      console.log();
    });
  }

  public ShowModal(id, modalName) {
    this._modalService.open(modalName);
    this.escalateIssueId = id;
  }

  public AuthorizeIssueRequest() {
    this.Loader.show();
    this.httpService.get(this.httpService.apiRoutes.Jobs.AuthorizeEscalateIssueRequest + "?escalateIssueId=" + this.escalateIssueId).subscribe(result => {
      let res = result.json();
      if (res.status == httpStatus.Ok) {
        this._modalService.dismissAll();
        this.toastr.success("Escalate Issue Authorize successfully", "Authorize");
        this.populateEscalateIssuesData();
        this.Loader.hide();
      }
    }, error => {
      console.log(error);
    });
  }
  public hideModal() {
    this._modalService.dismissAll();
  }
  public escalateIssueFilterForm() {
    this.escalateFilterForm = this.fb.group({
      customerId: [null],
      jobQuotationId: [null],
      tradesmanId: [null],
    });
  }

  public restForm() {
    this.escalateFilterForm.reset();
    this.populateEscalateIssuesData();
  }

  public numberOnly(event): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }

}
