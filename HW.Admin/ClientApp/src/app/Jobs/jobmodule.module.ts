import { NgModule} from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ActiveJobsComponent } from './active-jobs/active-jobs.component';
import { CompletedJobsComponent } from './completed-jobs/completed-jobs.component';
import { InprogressJobsComponent } from './inprogress-jobs/inprogress-jobs.component';
import { CommonModule } from '@angular/common';
import { JobDetailsComponent } from './job-details/job-details.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RatingModule } from 'ng-starrating';
import { ReciveBidsComponent } from './recive-bids/recive-bids.component';
import { ReciveBidsDetailComponent } from './recive-bids-detail/recive-bids-detail.component';
import { DynamicScriptLoaderService } from '../Shared/CommonServices/dynamicScriptLoaderService';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PendingJobsComponent } from './pending-jobs/pending-jobs.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PopoverModule } from 'ngx-bootstrap/popover';
//import { Ng4LoadingSpinnerModule } from 'ng4-loading-spinner';
import { NgxSpinnerModule  } from "ngx-spinner";
import {CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { SullpieradslistComponent } from './sullpieradslist/sullpieradslist.component';
import { ClickOutsideModule } from 'ng-click-outside';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';

import { NgxImageCompressService } from 'ngx-image-compress';
import { ApprovejobComponent } from './approvejob/approvejob.component';
import { JobAuthorizerComponent } from './job-authorizer/job-authorizer.component';
import { BookNowComponent } from './book-now/book-now.component';
import { WithdrawalrequestComponent } from './withdrawalrequest/withdrawalrequest.component';
import { DeletedJobsComponent } from './deleted-jobs/deleted-jobs.component';
import { PaidpaymentslistComponent } from './paidpaymentslist/paidpaymentslist.component';
import { DeclinedWithdrawRequestComponent } from './declined-withdraw-request/declined-withdraw-request.component';
import { EscalateIssueRequestsComponent } from './escalate-issue-requests/escalate-issue-requests.component';
import { AuthorizeEscalateRequestsComponent } from './authorize-escalate-requests/authorize-escalate-requests.component';
import { JobsByCategoryComponent } from './jobs-by-category/jobs-by-category.component';
import { NotificationsComponent } from './notifications/notifications.component';
import { AddEscalateOptionsComponent } from './add-escalate-options/add-escalate-options.component';

//import { createPdf, fonts, tableLayouts, TCreatedPdf, vfs } from 'pdfmake/build/pdfmake';
//import { pdfMake } from 'pdfmake/build/vfs_fonts';



const routes: Routes = [
  { path: 'app-active-jobs', component: ActiveJobsComponent },
  { path: 'app-completed-jobs', component: CompletedJobsComponent },
  { path: 'app-inprogress-jobs', component: InprogressJobsComponent },
  { path: 'app-pending-jobs', component: PendingJobsComponent },
  { path: 'AppJobDetails', component: JobDetailsComponent},
  { path: 'app-recive-bids', component: ReciveBidsComponent},
  { path: 'app-recive-bids-detail', component: ReciveBidsDetailComponent},
  { path: 'supplierads-list', component: SullpieradslistComponent},
  { path: 'app-approvejob', component: ApprovejobComponent},
  { path: 'job-authorizer', component: JobAuthorizerComponent },
  { path: 'book-now', component: BookNowComponent },
  { path: 'withdrawalrequest', component: WithdrawalrequestComponent },
  { path: 'paidpaymentslist', component: PaidpaymentslistComponent },
  { path: 'app-deleted-jobs', component: DeletedJobsComponent },
  { path: 'declined-withdraw-request', component: DeclinedWithdrawRequestComponent },
  { path: 'jobs-by-category', component: JobsByCategoryComponent },
  { path: 'notifications', component: NotificationsComponent },
  { path: 'escalate-issue-requests', component: EscalateIssueRequestsComponent },
  { path: 'authorize-escalate-requests', component: AuthorizeEscalateRequestsComponent },
  { path: 'add-escalate-options', component: AddEscalateOptionsComponent },
  { path: 'jobs-by-category', component: JobsByCategoryComponent },
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgMultiSelectDropDownModule,
    RouterModule.forChild(routes),
    RatingModule,
    ModalModule.forRoot(),
    PopoverModule.forRoot(),
    NgxSpinnerModule,
    NgbModule,
    ClickOutsideModule,
    ReactiveFormsModule,
    InfiniteScrollModule
  ],
  declarations: [
    ActiveJobsComponent,
    CompletedJobsComponent,
    InprogressJobsComponent,
    JobDetailsComponent,
    ReciveBidsComponent,
    ReciveBidsDetailComponent,
    PendingJobsComponent,
    SullpieradslistComponent,
    ApprovejobComponent,
    JobAuthorizerComponent,
    BookNowComponent,
    WithdrawalrequestComponent,
    DeletedJobsComponent,
    PaidpaymentslistComponent,
    DeclinedWithdrawRequestComponent,
    JobsByCategoryComponent,
    NotificationsComponent,
    EscalateIssueRequestsComponent,
    AuthorizeEscalateRequestsComponent,
    AddEscalateOptionsComponent

  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  exports: [RouterModule],
  providers: [DynamicScriptLoaderService, [NgxImageCompressService]]
})
export class JobmoduleModule { }
