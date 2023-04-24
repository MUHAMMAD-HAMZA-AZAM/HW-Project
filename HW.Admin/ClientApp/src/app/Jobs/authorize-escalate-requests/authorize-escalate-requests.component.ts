import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { escalateIssueStatus } from '../../Shared/Enums/enums';
import { CommonService } from '../../Shared/HttpClient/_http';
import { SortList } from '../../Shared/Sorting/sortList';

@Component({
  selector: 'app-authorize-escalate-requests',
  templateUrl: './authorize-escalate-requests.component.html',
  styleUrls: ['./authorize-escalate-requests.component.css']
})
export class AuthorizeEscalateRequestsComponent implements OnInit {
  public escalateFilterForm: FormGroup;
  public escalateFormData: any;
  public allowview;
  public authorizeEscalateIssuesLists = [];
  public userRole = { allowView: false, allowAdd: false, allowEdit: false, allowExport: false, allowDelete: false };
  constructor(
    public _modalService: NgbModal,
    public toastr: ToastrService,
    public httpService: CommonService,
    public Loader: NgxSpinnerService,
    public fb: FormBuilder,
    public sortList: SortList,
    private router: Router) { }
  ngOnInit() {
    this.userRole = JSON.parse(localStorage.getItem("Authorize Escalate Requests"));
    if (!this.userRole.allowView)
      this.router.navigateByUrl('/login');
    this.authorizeEscalateIssueFilterForm();
    this.populateAuthorizeEscalateIssuesData();
  }

  public populateAuthorizeEscalateIssuesData() {
    this.escalateFormData = this.escalateFilterForm.value;
    this.httpService.PostData(this.httpService.apiRoutes.Jobs.AuthorizeEscalateIssuesList, this.escalateFormData, true).then(result => {
      this.authorizeEscalateIssuesLists = result.json();
      console.log(this.authorizeEscalateIssuesLists);
      if (!this.authorizeEscalateIssuesLists) {
        this.toastr.error("No Data Found", "Error");
        this.Loader.hide();
      }
      this.Loader.hide();
    }, error => {
      this.Loader.show();
      console.log();
    });
  }

  public authorizeEscalateIssueFilterForm() {
    this.escalateFilterForm = this.fb.group({
      customerId: [null],
      jobQuotationId: [null],
      tradesmanId: [null],
    });
  }

  public restForm() {
    this.escalateFilterForm.reset();
    this.populateAuthorizeEscalateIssuesData();
  }

  public numberOnly(event): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }
}
